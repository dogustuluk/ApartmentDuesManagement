using Azure;
using DocumentFormat.OpenXml.ExtendedProperties;
using GCA_Business.Helpers;
using GCA_Business.HelperServices;
using GCA_Business.Interfaces;
using GCA_DataAccess.Data;
using GCA_DataAccess.DataModels;
using GCA_DataAccess.Entities;
using GCA_DataAccess.Interfaces;
using GCALojistik.Controllers;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GCA_Web.Controllers
{
	[Authorize]
	public class CompanyController : Controller
    {
        private readonly IServiceManager _serviceManager;
		private readonly AuthorityService _AuthorityService;

		public CompanyController(IServiceManager serviceManager, AuthorityService AuthorityService)
		{
			_serviceManager = serviceManager;
			_AuthorityService = AuthorityService;
		}

		[Authorize(Policy = "Sistem")]
		public async Task<IActionResult> Index
        (
           int PageIndex = 1,
           string SearchText = "",
           int CompanyTypeID = 0,
           int ActiveStatus = 1,
           string OrderBy = "CompanyName_ASC")
        {
			var MyAuth = _AuthorityService.Get_MemberTypeAuthority(1201).Result;
			if (!MyAuth.isCanView)
			{
				return Redirect("/Auth/AccessDenied?isFancy=false&returnURL=Index");
			}

			var predicate1 = PredicateBuilder.New<VW_Company>(true);

            if (!string.IsNullOrEmpty(SearchText))
            {
                predicate1 = predicate1.And(a => a.CompanyName.Contains(SearchText) || a.InvoiceName.Contains(SearchText) 
				|| !a.MainContactName.Contains(SearchText) || !a.NotificationGSMNo.Contains(SearchText));
            }
            if (CompanyTypeID != 0)
            {
                predicate1 = predicate1.And(a => a.CompanyTypeID == CompanyTypeID);
            }          
            if (ActiveStatus != 0)
            {
                predicate1 = predicate1.And(a => a.isActive == (ActiveStatus == 1 ? true : false));
            }

            PaginatedList<VW_Company> MyGridData = await _serviceManager.VW_Companies.GetDataPaged(predicate1, PageIndex, 25, OrderBy);

            Pagination MyPG = new()
            {
                PageIndex = PageIndex,
                pageSize = 25,
                TotalPages = MyGridData.TotalPages,
                TotalRecords = MyGridData.TotalRecords,
                HasPreviousPage = MyGridData.HasPreviousPage,
                HasNextPage = MyGridData.HasNextPage
            };
say
            Dictionary<string, object> Parameters = new()
            {
                { "PageIndex", PageIndex },
                { "SearchText", SearchText },
                { "CompanyTypeID", CompanyTypeID },
                { "ActiveStatus", ActiveStatus },
                { "OrderBy", OrderBy }
            };

            Index_VM MYRESULT = new()
            {
                PageTitle = "Firma",
                SubPageTitle = "Tanımlı Firmalar",
                MyGridData = MyGridData,
                CompanyTypeID_DDL = _serviceManager.DBParameters.GetDDL(a => a.DBParameterTypeID == 3 && a.isActive == true, false, "Bütün Tipler", "0", CompanyTypeID.ToString(), 100,""),
				Parameters = Parameters,
                MyPagination = MyPG,
				MyAuth = MyAuth
			};

            return View(MYRESULT);
        }


		[HttpGet]
		[Authorize(Policy = "Sistem")]
		public IActionResult AddNew()
		{
			var MyAuth = _AuthorityService.Get_MemberTypeAuthority(1201).Result;
			if (!MyAuth.isCanInsert)
			{
				return Redirect("/Auth/AccessDenied?isFancy=false&returnURL=Company_AddNew");
			}

			var MYRESULT = new Company_AddNew_DM()
			{
				
				CompanyTypeID_DDL = _serviceManager.DBParameters.GetDDL(a => a.DBParameterTypeID == 3 && a.isActive == true, false, "Bütün Tipler", "0", "0", 100, ""),
				isNewMember = true,
				CityID = 3401,
				CountyID = 425,
				CityID_DDL = _serviceManager.Cities.GetDDL(a=> a.CityID>0, false, "", "0", "3401", 100, ""),
				CountyID_DDL = _serviceManager.Counties.GetDDL(a=> a.CityID==3401, false, "", "0", "425", 100, ""),
				MainContact =new Contact_AddNew_DM_S()
			};

			return View(MYRESULT);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Policy = "Sistem")]
		public async Task<IActionResult> AddNew(Company_AddNew_DM Model)
		{
			var MyAuth = _AuthorityService.Get_MemberTypeAuthority(1201).Result;
			if (!MyAuth.isCanInsert)
			{
				return Redirect("/Auth/AccessDenied?isFancy=false&returnURL=Company_AddNew");
			}

			OptResult MyResult = new();

			MyResult.isOk = false;
			MyResult.ResultText = "İşlem sırasında bir hata oluştu, lütfen tekrar deneyiniz.";
			MyResult.ListURL = "/Company/Index";
			MyResult.ListText = "Firmalar Sayfasına Geri Dön";
			MyResult.AddURL = "/Company/AddNew";
			MyResult.AddText = "Firma Ekle";
			MyResult.DetailsURL = "";
			MyResult.DetailsText = "";
			MyResult.isFancy = false;

			if (ModelState.IsValid)
			{
				try
				{
					string ControlResult = "";

					if (_serviceManager.Companies.GetData(a => a.CompanyName == Model.CompanyName, 10, "CompanyID_DESC").Result.Any())
					{
						ControlResult += "Girdiğiniz firma ismi ile kayıt bulunuyor$";
					}
					if (ControlResult != "")
					{
						MyResult.ResultText = ControlResult.TrimEnd('$').Replace("$", "<br>");

						Model.MyResult = MyResult;

						return View(Model);
					}

					string ResultText = "Bilgiler başarılı bir şekilde eklendi.";
					int CUserID = User.GetMemberID();

					var newcompany = new GCA_DataAccess.Entities.Company()
					{
						GUID = Guid.NewGuid(),
						CompanyName = Model.CompanyName,
						CompanyTypeID = Model.CompanyTypeID,
						CityID = Model.CityID,
						CountyID = Model.CountyID,
						Adress = Model.Adress ?? "",
						InvoiceName = Model.InvoiceName,
						InvoiceInfo = Model.InvoiceInfo ?? "",
						TaxPlace = Model.TaxPlace,
						TaxNo = Model.TaxNo,
						NotificationGSMNo = Model.NotificationGSMNo,
						isActive = true,
						CreatedBy = CUserID,
						CreatedDate = DateTime.Now,
						UpdatedBy = CUserID,
						UpdatedDate = DateTime.Now
					};
					await _serviceManager.Companies.Add(newcompany);

					if (!string.IsNullOrEmpty(Model.MainContact.ContactName))
					{
						Contact newcontact = new Contact()
						{
							GUID = Guid.NewGuid(),
							CompanyID = newcompany.CompanyID,
							ContactType = "Şahıs",
							ContactName = Model.MainContact.ContactName,
							ContactTitle = Model.MainContact.ContactTitle ?? "",
							ContactEMail1 = Model.MainContact.ContactEMail1 ?? "",
							ContactGSM = Model.MainContact.ContactGSM ?? "",
							ContactPhone = "",
							ContactAdress = "",
							ContactDesc = "",
							isActive = true,
							CreatedBy = CUserID,
							CreatedDate = DateTime.Now,
							UpdatedBy = CUserID,
							UpdatedDate = DateTime.Now
						};
						await _serviceManager.Contacts.Add(newcontact);

						var companyupdate = _serviceManager.Companies.GetEntity(newcompany.CompanyID, null).Result;
						companyupdate.MainContactID = newcontact.ContactID;
						await _serviceManager.Companies.Update(companyupdate);

						#region ---------------- New member
						if(Model.isNewMember && !string.IsNullOrEmpty(Model.MainContact.ContactEMail1))
						{
							string Password = "GcAKullanici34";
							Member newmember = new Member()
							{
								GUID = Guid.NewGuid(),
								ItemType = 1201,
								ItemID = newcompany.CompanyID,
								NameSurname = Model.MainContact.ContactName,
								EMail = Model.MainContact.ContactEMail1,
								GSM = Model.MainContact.ContactGSM ?? "",
								MemberTypeID = 10,
								PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password),
								TCNo = "",
								CityID = newcompany.CityID,
								CountyID = newcompany.CountyID,
								Adress = newcompany.Adress,
								StatusID = 10,
								isPromotionEMail = true,
								isPromotionSMS = true,
								isEMailConfirmed = true,
								isSMSConfirmed = true,
								LockOutInfo = "",
								CreatedBy = CUserID,
								CreatedDate = DateTime.Now,
								UpdatedBy = CUserID,
								UpdatedDate = DateTime.Now,
								LastLoginDate=DateTime.Now
							};

							await _serviceManager.Members.Add(newmember);

							ResultText += " Yeni firma yöneticisi oluşturuldu, e-posta: " + newmember.EMail + ", Şifre: " + Password;
						}
						#endregion
					}

					MyResult.isOk = true;
					MyResult.ResultText = ResultText;
					MyResult.DetailsURL = "/Company/Update?guid=" + newcompany.GUID;
					MyResult.DetailsText = "Firma Detayına Git";
				}
				catch (Exception ex)
				{
					GCA_DataAccess.Entities.Error error = new()
					{
						ErrorURL = Request.Path.Value + "/",
						ErrorDesc = ex.ToString(),
						Operation = "Firma Ekleme",
						ErrorPlace = "Company",
						MemberID = User.GetMemberID(),
						CreatedDate = DateTime.Now
					};

					await _serviceManager.Errors.Add(error);
				}
			}
			else
			{
				string Hatalar = "";
				foreach (var modelState in ModelState.Values)
				{
					foreach (var modelError in modelState.Errors)
					{
						Hatalar += modelError.ErrorMessage + "#";
					}
				}
				MyResult.ResultText += "<br>" + Hatalar;
			}

			Model.MyResult = MyResult;

			return View(Model);
		}


		[HttpGet]
		public IActionResult Update(Guid GUID)
		{
			if (GUID == Guid.Empty)
			{
				return RedirectToAction("Index");
			}
			var MyAuth = _AuthorityService.Get_MemberTypeAuthority(1201).Result;
			if (!MyAuth.isCanUpdate)
			{
				return Redirect("/Auth/AccessDenied?isFancy=false&returnURL=Company_Update");
			}

            GCA_DataAccess.Entities.Company company = _serviceManager.Companies.GetEntity(0,GUID).Result;

			var MYRESULT = new Company_Update_DM();

			if (company != null)
			{
				var MainContactDD = _serviceManager.Contacts.GetEntity(company.MainContactID,null).Result;
				var SalesContactDD = _serviceManager.Contacts.GetEntity(company.SalesContactID, null).Result;
				var FinanceContactDD = _serviceManager.Contacts.GetEntity(company.FinanceContactID, null).Result;

				Contact_Update_DM_S MainContact = new();
				Contact_Update_DM_S SalesContact = new();
				Contact_Update_DM_S FinanceContact = new();

				if (MainContactDD != null)
				{
					MainContact.ContactGUID = MainContactDD.GUID;
					MainContact.ContactName = MainContactDD.ContactName;
					MainContact.ContactTitle = MainContactDD.ContactTitle;
					MainContact.ContactEMail1 = MainContactDD.ContactEMail1;
					MainContact.ContactGSM = MainContactDD.ContactGSM;
				}
				if (SalesContactDD != null)
				{
					SalesContact.ContactGUID = SalesContactDD.GUID;
					SalesContact.ContactName = SalesContactDD.ContactName;
					SalesContact.ContactTitle = SalesContactDD.ContactTitle;
					SalesContact.ContactEMail1 = SalesContactDD.ContactEMail1;
					SalesContact.ContactGSM = SalesContactDD.ContactGSM;
				}
				if (FinanceContactDD != null)
				{
					FinanceContact.ContactGUID = FinanceContactDD.GUID;
					FinanceContact.ContactName = FinanceContactDD.ContactName;
					FinanceContact.ContactTitle = FinanceContactDD.ContactTitle;
					FinanceContact.ContactEMail1 = FinanceContactDD.ContactEMail1;
					FinanceContact.ContactGSM = FinanceContactDD.ContactGSM;
				}

				MYRESULT = new()
				{
					CompanyID = company.CompanyID,
					GUID = company.GUID,
					CompanyName = company.CompanyName,
					CompanyTypeID = company.CompanyTypeID,
					CityID = company.CityID,
					CountyID = company.CountyID,
					Adress = company.Adress,
					InvoiceName = company.InvoiceName,
					InvoiceInfo = company.InvoiceInfo,
					TaxPlace = company.TaxPlace,
					TaxNo = company.TaxNo,
					NotificationGSMNo = company.NotificationGSMNo,
					MainContactID = company.MainContactID,
					MainContact = MainContact,
					SalesContactID = company.SalesContactID,
					SalesContact = SalesContact,
					FinanceContactID = company.FinanceContactID,
					FinanceContact = FinanceContact,
					isActive = company.isActive,
					CreatedByName = _serviceManager.Members.GetValue("NameSurname",company.CreatedBy,null).Result,
					CreatedDate = company.CreatedDate,
					UpdatedByName = _serviceManager.Members.GetValue("NameSurname", company.UpdatedBy, null).Result,
					UpdatedDate = company.UpdatedDate,
					CompanyTypeID_DDL = _serviceManager.DBParameters.GetDDL(a => a.DBParameterTypeID == 3 && a.isActive == true, false, "Bütün Tipler", "0", "0", 100, ""),
					CityID_DDL = _serviceManager.Cities.GetDDL(a => a.CityID > 0, false, "", "0", company.CityID.ToString(), 100, ""),
					CountyID_DDL = _serviceManager.Counties.GetDDL(a => a.CityID == company.CityID, false, "", "0", company.CountyID.ToString(), 100, ""),
					ContactID_DDL = _serviceManager.Contacts.GetDDL(a => a.CompanyID == company.CompanyID && a.isActive ==true, false, "Kullanıcı tanımlı değil", "0", "0", 100,""),
				};
			}

			return View(MYRESULT);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(Company_Update_DM Model)
		{
			var MyAuth = _AuthorityService.Get_MemberTypeAuthority(1201).Result;
			if (!MyAuth.isCanUpdate)
			{
				return Redirect("/Auth/AccessDenied?isFancy=false&returnURL=Company_Update");
			}
			OptResult MyResult = new();

			MyResult.isOk = false;
			MyResult.ResultText = "İşlem sırasında bir hata oluştu, lütfen tekrar deneyiniz.";
			MyResult.ListURL = "/Company/Index";
			MyResult.ListText = "Firmalar Sayfasına Geri Dön";
			MyResult.AddURL = "";
			MyResult.AddText = "";
			MyResult.DetailsURL = "";
			MyResult.DetailsText = "";
			MyResult.isFancy = false;

			if (ModelState.IsValid)
			{
				int CUserID = User.GetMemberID();

				try
				{
					var companyupdate = _serviceManager.Companies.GetEntity(0, Model.GUID).Result;
					if (companyupdate != null)
					{
						string ControlResult = "";
						if (Model.CompanyName != companyupdate.CompanyName)
						{
							if (_serviceManager.Companies.GetData(a => a.CompanyName == Model.CompanyName, 10, "CompanyID_DESC").Result.Any())
							{
								ControlResult += "Girdiğiniz firma adı ile kayıt bulunuyor$";
							}
						}
						if (ControlResult != "")
						{
							MyResult.ResultText = ControlResult.TrimEnd('$').Replace("$", "<br>");

							Model.MyResult = MyResult;

							return View(Model);
						}

						string ResultText = "Bilgiler başarılı bir şekilde güncellendi.";

						companyupdate.CompanyName = Model.CompanyName;
						companyupdate.CompanyTypeID = Model.CompanyTypeID;
						companyupdate.CityID = Model.CityID;
						companyupdate.CountyID = Model.CountyID;
						companyupdate.Adress = Model.Adress ?? "";
						companyupdate.InvoiceName = Model.InvoiceName;
						companyupdate.InvoiceInfo = Model.InvoiceInfo ?? "";
						companyupdate.TaxPlace = Model.TaxPlace;
						companyupdate.TaxNo = Model.TaxNo;
						companyupdate.NotificationGSMNo = Model.NotificationGSMNo;
						companyupdate.MainContactID = Model.MainContactID;
						companyupdate.SalesContactID = Model.SalesContactID;
						companyupdate.FinanceContactID = Model.FinanceContactID;
						companyupdate.isActive = Model.isActive;
						companyupdate.UpdatedBy = CUserID;
						companyupdate.UpdatedDate = DateTime.Now;

						await _serviceManager.Companies.Update(companyupdate);

						if (Model.MainContactID == 0 && !string.IsNullOrEmpty(Model.MainContact.ContactName))
						{
							#region --------------MainContactID
							Contact newcontact = new Contact()
							{
								GUID = Guid.NewGuid(),
								CompanyID = companyupdate.CompanyID,
								ContactType = "Şahıs",
								ContactName = Model.MainContact.ContactName,
								ContactTitle = Model.MainContact.ContactTitle ?? "",
								ContactEMail1 = Model.MainContact.ContactEMail1 ?? "",
								ContactGSM = Model.MainContact.ContactGSM ?? "",
								ContactPhone = "",
								ContactAdress = "",
								ContactDesc = "",
								isActive = true,
								CreatedBy = CUserID,
								CreatedDate = DateTime.Now,
								UpdatedBy = CUserID,
								UpdatedDate = DateTime.Now
							};
							companyupdate.MainContactID = newcontact.ContactID;
							await _serviceManager.Companies.Update(companyupdate);
							#endregion
						}
						if (Model.SalesContactID == 0 && !string.IsNullOrEmpty(Model.SalesContact.ContactName))
						{
							#region --------------SalesContactID
							Contact newcontact = new Contact()
							{
								GUID = Guid.NewGuid(),
								CompanyID = companyupdate.CompanyID,
								ContactType = "Şahıs",
								ContactName = Model.MainContact.ContactName,
								ContactTitle = Model.MainContact.ContactTitle ?? "",
								ContactEMail1 = Model.MainContact.ContactEMail1 ?? "",
								ContactGSM = Model.MainContact.ContactGSM ?? "",
								ContactPhone = "",
								ContactAdress = "",
								ContactDesc = "",
								isActive = true,
								CreatedBy = CUserID,
								CreatedDate = DateTime.Now,
								UpdatedBy = CUserID,
								UpdatedDate = DateTime.Now
							};
							companyupdate.SalesContactID = newcontact.ContactID;
							await _serviceManager.Companies.Update(companyupdate);
							#endregion
						}
						if (Model.FinanceContactID == 0 && !string.IsNullOrEmpty(Model.FinanceContact.ContactName))
						{
							#region --------------SalesContactID
							Contact newcontact = new Contact()
							{
								GUID = Guid.NewGuid(),
								CompanyID = companyupdate.CompanyID,
								ContactType = "Şahıs",
								ContactName = Model.MainContact.ContactName,
								ContactTitle = Model.MainContact.ContactTitle ?? "",
								ContactEMail1 = Model.MainContact.ContactEMail1 ?? "",
								ContactGSM = Model.MainContact.ContactGSM ?? "",
								ContactPhone = "",
								ContactAdress = "",
								ContactDesc = "",
								isActive = true,
								CreatedBy = CUserID,
								CreatedDate = DateTime.Now,
								UpdatedBy = CUserID,
								UpdatedDate = DateTime.Now
							};
							companyupdate.FinanceContactID = newcontact.ContactID;
							await _serviceManager.Companies.Update(companyupdate);
							#endregion
						}

						MyResult.isOk = true;
						MyResult.ResultText = ResultText;
						MyResult.DetailsURL = "/Company/Update?guid=" + companyupdate.GUID;
						MyResult.DetailsText = "Firma Detayına Git";

					}
				}
				catch (Exception ex)
				{
					GCA_DataAccess.Entities.Error error = new()
					{
						ErrorURL = Request.Path.Value + "/",
						ErrorDesc = ex.ToString(),
						Operation = "Firma Güncelleme",
						ErrorPlace = "Company",
						MemberID = CUserID,
						CreatedDate = DateTime.Now
					};

					await _serviceManager.Errors.Add(error);
				}
			}
			else
			{
				string Hatalar = "";
				foreach (var modelState in ModelState.Values)
				{
					foreach (var modelError in modelState.Errors)
					{
						Hatalar += modelError.ErrorMessage + "#";
					}
				}
				MyResult.ResultText += "<br>" + Hatalar;
			}

			Model.MyResult = MyResult;

			return View(Model);
		}

		#region ----------------------------------------------------------------------------------------------------
		public class Index_VM
		{
			public string? PageTitle { get; set; }
			public string? SubPageTitle { get; set; }
			public List<VW_Company>? MyGridData { get; set; }
			public List<DDL>? CompanyTypeID_DDL { get; set; }
			public Dictionary<string, object>? Parameters { get; set; }
			public Pagination? MyPagination { get; set; }
			public MemberTypeAuthority MyAuth { get; set; }
		} 
		#endregion
	}
}
