using System.Linq;
using System.Web.Mvc;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            using (TestOAContext context = new TestOAContext())
            {
                return this.View(context.Categories.ToList());
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
