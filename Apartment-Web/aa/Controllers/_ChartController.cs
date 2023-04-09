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
	public class _ChartController : Controller
	{
		private readonly IServiceManager _serviceManager;
		private readonly AAAService _AAAService;

		public _ChartController(IServiceManager serviceManager, AAAService AAAService)
		{
			_serviceManager = serviceManager;
			_AAAService = AAAService;
		}

		[HttpGet]
		public JsonResult Get_ChartData(string DataName, string? Params)
		{
			List<ChartData_DM> MYRESULT = new List<ChartData_DM>();

			if (DataName == "Res_Monthly")
			{
				for (int i = 1; i <= 12; i++)
				{
					MYRESULT.Add(new ChartData_DM() { x = (new DateTime(2023, i, 1)).ToString("yy.MM"), y = RandomHelper.RandomNumber(400, 1100) });
				}
			}
			else if (DataName == "Res_Daily")
			{
				for (int i = 1; i <= 30; i++)
				{
					MYRESULT.Add(new ChartData_DM() { x = DateTime.Now.AddDays(i).ToString("yy.MM"), y = RandomHelper.RandomNumber(20, 50) });
				}
			}
			else if (DataName == "Company_Totals")
			{
				var MyCompanies = _serviceManager.VW_Companies.GetData(a => a.isActive == true, 10, "TotalReservations_DESC").Result;
				foreach(var cc in MyCompanies)
				{
					MYRESULT.Add(new ChartData_DM() { x = cc.CompanyName, y = Convert.ToInt32(cc.TotalWorkReservations) });
				}
			}

			return Json(MYRESULT);
		}
	}
}
