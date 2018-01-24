using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FionaWhitfieldArt.Models
{
    public class TestimonialsModel
    {
        public string Title { get; set; }
        public string Introduction { get; set; }
        public List<TestimonialModel> Testimonials { get; set; }
        public string ColumnClass
        {
            get
            {
                switch (Testimonials.Count)
                {
                    case 1:
                        return "col-md-12";
                    case 2:
                        return "col-md-6";
                    case 3:
                        return "col-md-4";
                    case 4:
                        return "col-md-3";
                    default:
                        return "col-md-12";
                }
            }
        }
        public bool HasTestimonials
        {
            get
            {
                return Testimonials != null && Testimonials.Count > 0;
            }
        }
    }
}