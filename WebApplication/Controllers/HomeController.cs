using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.CriminalServiceReference;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //
        // GET: /Home/Search
        public ActionResult Search()
        {
            return View("Index", "Home");
        }

        //
        // POST: /Home/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Search(CriminalViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!ValidateFields(model))
                {
                    ModelState.AddModelError("", "At least one field must be filled.");
                    return View("Index");
                }
                CriminalServiceReference.CriminalServiceClient client = new CriminalServiceReference.CriminalServiceClient();

                try
                {
                    SearchFilter search = GetSearchFilter(model);
                    if (search != null)
                    {
                        if (client.SearchProfiles(search))
                        {
                            //To show success as script alert:
                            //@Html.Raw(TempData["msg"]) //-> this must be on view
                            //TempData["msg"] = "<script>alert('Your search result will be available in your email soon.');</script>";
                            //return RedirectToAction("Index", "Home");

                            TempData["msg"] = "Success, your search result will be available in your email soon.";
                            return View("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "No results found with parameters selected.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unexpected error while parsing parameters.");
                    }
                    
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View("Index");
                }
                finally
                {
                    client.Close();

                }

            }
            return View("Index");
        }

        private bool ValidateFields(CriminalViewModel model)
        {
            //Check if there's at least one parameter filled:
            return (model.Gender != null || model.MaxAge != null || model.MaxHeight != null ||
                    model.MaxResults != null || model.MaxWeight != null || model.MinAge != null ||
                    model.MinHeight != null || model.MinWeight != null || model.Name != null ||
                    model.Nationality != null);
                    
        }

        private SearchFilter GetSearchFilter(CriminalViewModel model)
        {
            SearchFilter search = new SearchFilter();
            try
            {
                string[] arrName;
                if (model.Name != null)
                    arrName = new string[] { model.Name };
                else arrName = new string[0];
                string[] arrNationality;
                if (model.Nationality != null)
                    arrNationality = new string[] { model.Nationality };
                else arrNationality = new string[0];
                search.Email = model.Email;
                search.Gender = model.Gender??'A';
                search.MaxAge = model.MaxAge??0;
                search.MaxHeight = model.MaxHeight ?? 0;
                search.MaxResults = model.MaxResults ?? 0;
                search.MaxWeight = model.MaxWeight ?? 0;
                search.MinAge = model.MinAge ?? 0;
                search.MinHeight = model.MinHeight ?? 0;
                search.MinWeight = model.MinWeight ?? 0;
                search.Names = arrName;
                search.Nationalities = arrNationality;
                return search;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}