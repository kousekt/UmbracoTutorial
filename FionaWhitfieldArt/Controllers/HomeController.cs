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

// RSS feed example - https://www.youtube.com/watch?v=vW4UWibtLHY

// Search - https://codeshare.co.uk/blog/how-to-search-by-document-type-and-property-in-umbraco/
// https://www.youtube.com/watch?v=B9NeqMN9jIw&t=2597s

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
            
            // short cut version to get you the home page.
            IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");
            string title = homePage.GetPropertyValue<string>("latestBlogPostsTitle");
            string intro = homePage.GetPropertyValue("latestBlogPostsIntroduction").ToString(); // don't want the html
            LatestBlogPost model = new LatestBlogPost()
            {
                Title = title,
                Introduction = intro
            };
            return PartialView(PARTIAL_VIEW_FOLDER + "_Blog.cshtml", model);
        }

        public ActionResult RenderTestimonials()
        {
            IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");
            string title = homePage.GetPropertyValue<string>("testimonialsTitle");
            string intro = homePage.GetPropertyValue("testimonialsIntroduction").ToString(); // don't want the html

            List<TestimonialModel> testimonials = new List<TestimonialModel>();
            ArchetypeModel testimonialsList = homePage.GetPropertyValue<ArchetypeModel>("testimonialList");
            if (testimonialsList != null)
            {
                foreach (ArchetypeFieldsetModel fsm in testimonialsList.Take(3))
                {
                    string name = fsm.GetValue<string>("name");
                    string quote = fsm.GetValue<string>("quote");
                    testimonials.Add(new TestimonialModel()
                    {
                        Name = name,
                        Quote = quote
                    });
                }
            }

            TestimonialsModel model = new TestimonialsModel()
            {
                Title = title,
                Introduction = intro,
                Testimonials = testimonials
            };
            return PartialView(PARTIAL_VIEW_FOLDER + "_Testimonials.cshtml", model);
        }
    }
}