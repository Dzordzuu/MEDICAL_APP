using MEDICAL_APP.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MEDICAL_APP.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
           
            return View(CheckBoxesModel());
        }

        [HttpPost]
        public ActionResult Add (Medical medical)
        {
            var isExist = IsEmailExist(medical.MedicalName);

            if (isExist)
            {
                ModelState.AddModelError("MedicalExist", "Is already exist in base");
                return View("Index",CheckBoxesModel());
            }
        
            using (Database1Entities dc = new Database1Entities())
            {
                var user = HttpContext.User.Identity;

                dc.Medicals.Add(new Medicals { MedicalName = medical.MedicalName,MedicalId=GenerateId()});
                dc.SaveChanges();
 
                foreach (var item in medical.Monday)
                {
                    if (item.IsSelected)
                    {
                        dc.IntakeTime.Add(new IntakeTime { Day = 1, Hour = Int32.Parse(item.Hour) });
                      
                    }
                }
                foreach (var item in medical.Tuesday)
                {
                    if (item.IsSelected)
                    {
                        dc.IntakeTime.Add(new IntakeTime { Day = 1, Hour = Int32.Parse(item.Hour),PrescriptionsId=GenerateId() });

                    }
                }
                foreach (var item in medical.Wednesday)
                {
                    if (item.IsSelected)
                    {
                        dc.IntakeTime.Add(new IntakeTime { Day = 1, Hour = Int32.Parse(item.Hour) });
                    }
                }
                foreach (var item in medical.Thuersday)
                {
                    if (item.IsSelected)
                    {
                        dc.IntakeTime.Add(new IntakeTime { Day = 1, Hour = Int32.Parse(item.Hour) });
                    }
                }
                foreach (var item in medical.Friday)
                {
                    if (item.IsSelected)
                    {
                        dc.IntakeTime.Add(new IntakeTime { Day = 1, Hour = Int32.Parse(item.Hour) });
                    }
                }
                foreach (var item in medical.Saturday)
                {
                    if (item.IsSelected)
                    {
                        dc.IntakeTime.Add(new IntakeTime { Day = 1, Hour = Int32.Parse(item.Hour) });
                    }
                }
                foreach (var item in medical.Sunday)
                {
                    if (item.IsSelected)
                    {
                        dc.IntakeTime.Add(new IntakeTime { Day = 1, Hour = Int32.Parse(item.Hour) });
                    }
                }
                dc.SaveChanges();
                CheckBoxesModel();
                return View("Index",CheckBoxesModel());

            }
        }
        [NonAction]
        public bool IsEmailExist(string MedicalName)
        {
            using (Database1Entities dc = new Database1Entities())
            {
                var v = dc.Medicals.Where(a => a.MedicalName== MedicalName).FirstOrDefault();
                return v != null;
            }
        }
        [NonAction]
        public int GenerateId()
        {
            using (Database1Entities dc = new Database1Entities())
            {
                var v = dc.Medicals.Max(x => x.MedicalId);

                return v + 1;
            }
        }
        [NonAction]
        public Medical CheckBoxesModel()
        {
            var model = new Medical();
            model.Monday = new List<CheckBoxListItem>();
            for (int i = 1; i < 25; i++)
            {
                model.Monday.Add(new CheckBoxListItem() { IsSelected = false, Hour = i.ToString() });

            };
            model.Tuesday = new List<CheckBoxListItem>();
            for (int i = 1; i < 25; i++)
            {
                model.Tuesday.Add(new CheckBoxListItem() { IsSelected = false, Hour = i.ToString() });

            };
            model.Wednesday = new List<CheckBoxListItem>();
            for (int i = 1; i < 25; i++)
            {
                model.Wednesday.Add(new CheckBoxListItem() { IsSelected = false, Hour = i.ToString() });

            };
            model.Thuersday = new List<CheckBoxListItem>();
            for (int i = 1; i < 25; i++)
            {
                model.Thuersday.Add(new CheckBoxListItem() { IsSelected = false, Hour = i.ToString() });

            };
            model.Friday = new List<CheckBoxListItem>();
            for (int i = 1; i < 25; i++)
            {
                model.Friday.Add(new CheckBoxListItem() { IsSelected = false, Hour = i.ToString() });

            };
            model.Saturday = new List<CheckBoxListItem>();
            for (int i = 1; i < 25; i++)
            {
                model.Saturday.Add(new CheckBoxListItem() { IsSelected = false, Hour = i.ToString() });

            };
            model.Sunday = new List<CheckBoxListItem>();
            for (int i = 1; i < 25; i++)
            {
                model.Sunday.Add(new CheckBoxListItem() { IsSelected = false, Hour = i.ToString() });

            };
            return model;
        }
    }
}