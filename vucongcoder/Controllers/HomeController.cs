using vucongcoder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace vucongcoder.Controllers
{
    public class HomeController : Controller
    {
        public class SoftwareERP
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string IconClass { get; set; } 
            public string Url { get; set; }       
        }
        private static List<SoftwareERP> softwares = new List<SoftwareERP>()
{
    new SoftwareERP { Id = 1, Name = "SAP ERP", Description = "Enterprise resource planning software by SAP.", IconClass = "fas fa-cogs", Url = "#" },
    new SoftwareERP { Id = 2, Name = "Oracle ERP", Description = "Cloud ERP software by Oracle.", IconClass = "fas fa-cloud", Url = "#" },
    new SoftwareERP { Id = 3, Name = "Microsoft Dynamics 365", Description = "ERP & CRM solution from Microsoft.", IconClass = "fas fa-database", Url = "#" },
    new SoftwareERP { Id = 4, Name = "Odoo", Description = "Open-source ERP & CRM platform.", IconClass = "fas fa-rocket", Url = "#" },
    new SoftwareERP { Id = 5, Name = "NetSuite", Description = "Cloud ERP by Oracle NetSuite.", IconClass = "fas fa-network-wired", Url = "#" },
    new SoftwareERP { Id = 6, Name = "MessageAnalysis", Description = "Analyze messages for sentiment, trends, and customer insights.", IconClass = "fas fa-comment-dots", Url = "/Home/MessageAnalysis" }
};
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string Username, string Password)
        {
            if (Username == "admin" && Password == "vucongcoder@")
            {
                return RedirectToAction("ControlPage", "Home");
            }
            else
            {
                // Đăng nhập sai, hiện lỗi lên View
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }
        public ActionResult ControlPage()
        {
            return View(softwares);
        }
        public ActionResult MessageAnalysis()
        {
            string rssUrl = "https://xskt.com.vn/rss-feed/mien-bac-xsmb.rss";

            using (XmlReader reader = XmlReader.Create(rssUrl))
            {
                var feed = SyndicationFeed.Load(reader);
                var latest = feed.Items.FirstOrDefault();

                if (latest != null)
                {
                    ViewBag.Title = latest.Title.Text;
                    ViewBag.Content = latest.Summary.Text;
                    ViewBag.Date = latest.PublishDate.DateTime.ToString("dd/MM/yyyy");
                }
            }
            return View();
        }

        public ActionResult urlResult(string input)
        {
            string rssUrl = "https://xskt.com.vn/rss-feed/mien-bac-xsmb.rss";

            using (XmlReader reader = XmlReader.Create(rssUrl))
            {
                var feed = SyndicationFeed.Load(reader);
                var latest = feed.Items.FirstOrDefault();

                if (latest != null)
                {
                    ViewBag.Title = latest.Title.Text;
                    ViewBag.Content = latest.Summary.Text;
                    ViewBag.Date = latest.PublishDate.DateTime.ToString("dd/MM/yyyy");
                }
            }
            return View();
        }
        public static string NormalizeMessage(string input)
        {
            // Tách phần số tiền (x...)
            var parts = input.Split('x');
            if (parts.Length != 2) return input; // Không đúng định dạng thì trả về y nguyên

            string numbers = parts[0]; // vd: "545-151-252"
            string bet = "x" + parts[1]; // vd: "x30"

            List<string> results = new List<string>();

            // Nếu có nhiều cụm số ngăn cách bởi "-"
            var groups = numbers.Split('-');

            foreach (var group in groups)
            {
                if (group.Length == 3)
                {
                    // Lấy 2 cặp số liên tiếp
                    results.Add(group.Substring(0, 2) + bet);
                    results.Add(group.Substring(1, 2) + bet);
                }
                else if (group.Length == 2)
                {
                    // Nếu chỉ có 2 số thì giữ nguyên
                    results.Add(group + bet);
                }
            }

            return string.Join(" ", results);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}