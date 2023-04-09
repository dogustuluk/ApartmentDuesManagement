using GCA_Business.Helpers;
using GCA_Business.HelperServices;
using GCA_Business.Interfaces;
using GCA_DataAccess.DataModels;
using GCA_DataAccess.Entities;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GCA_Web.Controllers
{
	[Authorize]
	public class _BaseController : Controller
	{
		private readonly IServiceManager _serviceManager;
		private readonly EMailSMSService _EMailSMSService;
		private readonly AAAService _AAAService;

		public _BaseController(IServiceManager serviceManager, EMailSMSService EMailSMSService, AAAService AAAService)
		{
			_serviceManager = serviceManager;
			_EMailSMSService = EMailSMSService;
			_AAAService = AAAService;
		}

		public JsonResult GetCityID_DDL()
		{
			List<DDL> MyReturnList = _serviceManager.Cities.GetDDL(a=> a.CityID>0, false, "Seçiniz", "0", "0", 100,"");

			return Json(MyReturnList);
		}

		[AllowAnonymous]
		public JsonResult GetAjax_DDL(string WHAT, string Params, string ID)
		{
			List<DDL> MyReturnList = new();

			if (WHAT == "Cities")
			{
				MyReturnList = _serviceManager.Cities.GetDDL(a => a.CityID > 0, false, "Seçiniz", "0", "0", 100, "");
			}
			else if (WHAT == "Counties")
			{
				int AID = Convert.ToInt32(ID);
				MyReturnList = _serviceManager.Counties.GetDDL(a => a.CityID == AID, false, "Seçiniz", "0", "0", 100,"");
			}
			else if (WHAT == "Companies")
			{
				string DefaultText = (!string.IsNullOrEmpty(Params) && Params.Contains("DefaultText")) ? "Seçiniz" : "";
				MyReturnList = _serviceManager.Companies.GetDDL(a => a.isActive == true, true, DefaultText, "0", "0", 100,"");
			}
			else if (WHAT == "LogisticFirms")
			{
				bool isGuid = !string.IsNullOrEmpty(Params) && Params.Contains("isGuid");
				string DefaultText = (!string.IsNullOrEmpty(Params) && Params.Contains("DefaultText")) ? "Seçiniz" : "";
				MyReturnList = _serviceManager.LogisticFirms.GetDDL(a => a.isActive == true, isGuid, DefaultText, "0", "0", 100, "");
			}
			else if (WHAT == "ForkLifts")
			{
				bool isGuid = !string.IsNullOrEmpty(Params) && Params.Contains("isGuid");
				string DefaultText = (!string.IsNullOrEmpty(Params) && Params.Contains("DefaultText")) ? "Seçiniz" : "";
				MyReturnList = _serviceManager.ForkLifts.GetDDL(a => a.isActive == true, isGuid, DefaultText, "0", "0", 100, "");
			}
			else if (WHAT == "WorkShifts")
			{
				bool isGuid = false;
				string DefaultText = (!string.IsNullOrEmpty(Params) && Params.Contains("DefaultText")) ? "Seçiniz" : "";
				MyReturnList = _serviceManager.VW_WorkShifts.GetDDL(a => a.isActive == true, isGuid, DefaultText, "0", "0", 100, "");
			}
			else if (WHAT == "WorkShiftForkLifts")
			{
				bool isGuid = false;
				int WorkShiftID = Convert.ToInt32(ID);
				string DefaultText = (!string.IsNullOrEmpty(Params) && Params.Contains("DefaultText")) ? "Seçiniz" : "";
				MyReturnList = _serviceManager.VW_WorkShiftForkLifts.GetDDL(a => (WorkShiftID > 0 ? a.WorkShiftID == WorkShiftID : a.WorkShiftID > 0), isGuid, DefaultText, "0", "", 100, "");
			}
			else if (WHAT == "ForkLiftDrivers")
			{
				bool isGuid = !string.IsNullOrEmpty(Params) && Params.Contains("isGuid");
				string DefaultText = (!string.IsNullOrEmpty(Params) && Params.Contains("DefaultText")) ? "Seçiniz" : "";
				MyReturnList = _serviceManager.Members.GetDDL(a => a.StatusID == 10 && a.ItemType==1301, isGuid, DefaultText, "0", "0", 100, "");
			}
			return Json(MyReturnList);
		}

		public int GetNewSortorderNo(int ID, string Where)
		{
			int MyResult = 0;

			if (Where == "DBParameter")
			{
				MyResult = _AAAService.Get_Top1IntValue("DBParameter", "SortOrderNo", "DBParameterTypeID=" + ID + " ORDER BY SortOrderNo DESC") + 10;
			}

			return MyResult;
		}

		[HttpPost]
		public string UploadFile([FromForm] FileUpload_VM model)
		{
			string MyResult = "Error";

			//if (model.MyFile != null)
			//{
			//	string FilePrefix = "F";
			//	string Folder = "Temp";
			//	int ID = 0;

			//	if (model.ItemType == 1101)
			//	{
			//		FilePrefix = "V";
			//		Folder = "Member";
			//		ID = _serviceManager.Members.GetEntity(0, model.GUID).Result.MemberID;
			//	}
			//	else if (model.ItemType == 1201)
			//	{
			//		FilePrefix = "CF";
			//		Folder = "Company";
			//		ID = _serviceManager.Companies.GetEntity(0,model.GUID).Result.CompanyID;
			//	}
			//	else if (model.ItemType == 1301)
			//	{
			//		FilePrefix = "GF";
			//		Folder = "Gift";
			//		ID = _serviceManager.Gifts.GetByGUID(model.GUID).GiftID;
			//	}
			//	else if (model.ItemType == 1701)
			//	{
			//		FilePrefix = "HT";
			//		Folder = "HelTicket";
			//		ID = _serviceManager.HelpTickets.GetByGUID(model.GUID).HelpTicketID;
			//	}

			//	string FilePath = _AAAService.UploadFile(model.MyFile, 1200, FilePrefix, Folder);

			//	if (FilePath != "")
			//	{
			//		MyResult = FilePath;

			//		if (model.isFileSave)
			//		{
			//			double FileSize = Math.Round(Convert.ToDouble(((model.MyFile.Length / 1024) / 1024)), 2);

			//			var data = new FFile()
			//			{
			//				GUID = Guid.NewGuid(),
			//				M_ItemType = model.ItemType,
			//				M_ItemID = ID,
			//				FileTypeID = 0,
			//				FileNo = RandomHelper.RandomString(10, false),
			//				FileURL = FilePath,
			//				FileDesc = "",
			//				FileSize = FileSize,
			//				FileDate1 = DateTime.Now,
			//				isHidden = false,
			//				CreatedBy = User.GetMemberID(),
			//				CreatedDate = DateTime.Now,
			//				UpdatedBy = User.GetMemberID(),
			//				UpdatedDate = DateTime.Now
			//			};
			//			_serviceManager.FFiles.Add(data);
			//			_serviceManager.SaveChanges();
			//		}

			//		if (ID > 0)
			//		{
			//			if (model.ItemType == 1201)
			//			{
			//				_AAAService.ExecuteScalar("UPDATE Member SET UserImage='" + MyResult + "' WHERE MemberID=" + ID);
			//			}
			//		}
			//	}
			//}

			return MyResult;
		}

		public string GetParams(int ID, string Where)
		{
			string MyResult = "";

			if (Where == "DBParameterType")
			{
				MyResult = _AAAService.Get_StringValue("DBParameterType", "Params", "DBParameterTypeID=" + ID);
			}

			return MyResult;
		}

		public JsonResult Search_Select2(string TableName, string Param1, string prefix)
		{
			List<MySelect> MyResult = new();
			if (prefix != null && prefix.Length >= 1)
			{
				if (TableName == "Company")
				{
					MyResult = _serviceManager.Companies.GetDDL(a => a.CompanyName.Contains(prefix), true, "", "", "", 10, null)
					.Select(a => new MySelect()
					{
						text = a.Text,
						id = a.Value
					}).ToList();
				}
				else if (TableName == "Member")
				{
					MyResult = _serviceManager.Members.GetDDL(a => a.NameSurname.Contains(prefix), false, "", "", "", 10, null)
						.Select(a => new MySelect()
						{
							text = a.Text,
							id = a.Value
						}).ToList();
				}
			}

			return Json(MyResult);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public string SendSMS(string ID, string Function)
		{
			if (Function == "SendSMS_MemberProfile")
			{
				_EMailSMSService.SendSMS_MemberProfile(Guid.Parse(ID));
			}
			if (Function == "SendSMS_MemberShipConfirmed")
			{
				_EMailSMSService.SendSMS_MemberShipConfirmed(Convert.ToInt32(ID));
			}
			if (Function == "SendSMS_MemberShipConfirmed")
			{
				_EMailSMSService.SendSMS_MemberShipConfirmed(Convert.ToInt32(ID));
			}

			return "";
		}

		#region ----------------------------------------------------------------------------------------------------
		public class FileUpload_VM
		{
			public int ItemType { get; set; }
			public Guid GUID { get; set; }
			public IFormFile? MyFile { get; set; }
			public bool isFileSave { get; set; } = false;
		}
		#endregion
	}
}
