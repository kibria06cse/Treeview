using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Treeview_2.Models;
using System.Data.Entity;

namespace Treeview_2.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public JsonResult GetData()
        {
            var data = db.Treeviews.ToList().Select(s => new Tree { Id= s.Id, label= s.Label, ParentId= s.ParentId});
            //var result = GenerateTree(data);
            var result = BuildTree(data);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        private IList<Tree> BuildTree(IEnumerable<Tree> data)
        {
             var groups = data.GroupBy(i => i.ParentId);

            var roots = groups.FirstOrDefault(g => g.Key.HasValue == false).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                for (int i = 0; i < roots.Count; i++)
                    AddChildren(roots[i], dict);
            }

            return roots;

        }

        private void AddChildren(Tree node, IDictionary<int, List<Tree>> source)
        {
            if (source.ContainsKey(node.Id))
            {
                node.children = source[node.Id];
                for (int i = 0; i < node.children.Count; i++)
                    AddChildren(node.children[i], source);
            }
            else
            {
                node.children = new List<Tree>();
            }
        }
        public class Tree
        {
            public int Id { get; set; }
            public string label { get; set; }
            public int? ParentId { get; set; }
            public List<Tree> children { get; set; }
        }

        public ActionResult ChangeUrl(int Id)
        {
            var treeview = db.Treeviews.Find(Id);
            if(treeview.HierarchyTypeId== (int)Enums.Base)
            {
                return RedirectToAction("DivisionByBase", "Base", new { id = treeview.KeyOfThatHierarchy });                
            }
            else if (treeview.HierarchyTypeId == (int)Enums.Division)
            {
                return RedirectToAction("DistrictsByDivision", "Division", new { id = treeview.KeyOfThatHierarchy });
            }
           else if (treeview.HierarchyTypeId == (int)Enums.District)
            {
                return RedirectToAction("ThanaByDistrict", "District", new { id = treeview.KeyOfThatHierarchy });
            }
            if (treeview.HierarchyTypeId == (int)Enums.Thana)
            {
                return RedirectToAction("UnionByThana", "Thana", new { id = treeview.KeyOfThatHierarchy });
            }
            else  if (treeview.HierarchyTypeId == (int)Enums.Union)
            {
                return RedirectToAction("VillageByUnion", "Union", new { id = treeview.KeyOfThatHierarchy });
            }
            else if (treeview.HierarchyTypeId == (int)Enums.Village)
            {
                return RedirectToAction("SchoolByArea", "Village", new { id = treeview.KeyOfThatHierarchy });
            }
           
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}