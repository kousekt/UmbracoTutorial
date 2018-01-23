using FionaWhitfieldArt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Archetype.Models;

namespace FionaWhitfieldArt.Controllers
{
    // https://www.youtube.com/watch?v=6WzNPlKks-0&t=1256s
    public class HomeController : SurfaceController
    {
        private const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Home/";
        public ActionResult RenderFeatured()
        {
            List<FeaturedItem> model = new List<FeaturedItem>();

            // get to the home page
            IPublishedContent homePage = CurrentPage.AncestorOrSelf(1).DescendantsOrSelf().Where(x => x.DocumentTypeAlias == "home").FirstOrDefault();
            ArchetypeModel featuredItems = homePage.GetPropertyValue<ArchetypeModel>("featuredItems");

            // get all those fieldsets and put them in featured items

          
            // https://github.com/kgiszewski/Archetype/issues/413
            foreach (ArchetypeFieldsetModel fsm in featuredItems)
            {                               
                var mediaItem = fsm.GetValue<IPublishedContent>("image");
                string imageUrl = mediaItem.Url;

                var linkedPage = fsm.GetValue<IPublishedContent>("page");
                string pageUrl = linkedPage.Url;
                
                model.Add(new FeaturedItem()
                {
                    Name = fsm.GetValue<string>("name"),
                    Category = fsm.GetValue<string>("category"),
                    ImageUrl = imageUrl,
                    LinkUrl = pageUrl
                });
            }

            return PartialView(PARTIAL_VIEW_FOLDER + "_Featured.cshtml", model);
        }

        public ActionResult RenderServices()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Services.cshtml");
        }

        public ActionResult RenderBlog()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Blog.cshtml");
        }

        public ActionResult RenderClients()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Clients.cshtml");
        }
    }
}