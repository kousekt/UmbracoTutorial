using Archetype.Models;
using FionaWhitfieldArt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace FionaWhitfieldArt.Controllers
{
    public class BlogController : SurfaceController
    {
        private const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Blog/";

        public ActionResult RenderPostList(int maxNumberOfItems)
        {
            List<BlogPreview> model = new List<BlogPreview>();

            // get to the home page
            IPublishedContent blogPage = CurrentPage.AncestorOrSelf(1).DescendantsOrSelf().Where(x => x.DocumentTypeAlias == "blog").FirstOrDefault();
           // get all those fieldsets and put them in featured items
            
            foreach (IPublishedContent page in blogPage.Children.OrderByDescending(x=>x.UpdateDate).Take(maxNumberOfItems))
            {
                var imageId = page.GetPropertyValue<int>("articleimage");
                var mediaItem = Umbraco.Media(imageId);

                model.Add(new BlogPreview()
                {
                    Name = page.Name,
                    Introduction = page.GetPropertyValue<string>("articleintro"),
                    ImageUrl = mediaItem.Url,
                    LinkUrl = page.Url
                });
            }

            return PartialView(PARTIAL_VIEW_FOLDER + "_PostList.cshtml", model);
        }
    }
}