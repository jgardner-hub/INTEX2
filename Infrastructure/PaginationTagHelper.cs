using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using INTEX2.Models.ViewModels;

namespace INTEX2.Infrastructure
{
    [HtmlTargetElement("div", Attributes ="page-blah")]
    public class PaginationTagHelper : TagHelper
    {
        //Dynamically create the page links for us

        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        //Different than the view context
        public PageInfo PageBlah { get; set; }
        public string PageAction { get; set; }

        public override void Process (TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");

            int pagebuttonsides = 2;


            // CASE: Early Pages
            if (PageBlah.CurrentPage - pagebuttonsides <= 0)
            {
                for (int i = 1; i <= (1 + (pagebuttonsides * 2)); i++)
                {
                    TagBuilder tb = new TagBuilder("a");

                    tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });

                    if (PageBlah.CurrentPage == i)
                    {
                        tb.Attributes["class"] = "btn btn-primary";
                    }
                    else
                    {
                        tb.Attributes["class"] = "btn btn-secondary";
                    }
                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);
                }
            }
            //CASE: Late Pages
            else if(PageBlah.CurrentPage + pagebuttonsides >= PageBlah.TotalPages)
            {
                for (int i = 1; i <= (1 + (pagebuttonsides * 2)); i++)
                {
                    TagBuilder tb = new TagBuilder("a");

                    tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });

                    if (PageBlah.CurrentPage == i)
                    {
                        tb.Attributes["class"] = "btn btn-primary";
                    }
                    else
                    {
                        tb.Attributes["class"] = "btn btn-secondary";
                    }
                    tb.InnerHtml.Append(i.ToString());

                    final.InnerHtml.AppendHtml(tb);
                }
            }
            //Case: Everything in the middle
            else
            {
                for (int i = (PageBlah.CurrentPage - pagebuttonsides); i < PageBlah.CurrentPage; i++)
                {
                    TagBuilder tb = new TagBuilder("a");
                    tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });
                    tb.Attributes["class"] = "btn btn-secondary";
                    tb.InnerHtml.Append((i).ToString());
                    final.InnerHtml.AppendHtml(tb);
                }

                for (int i = (PageBlah.CurrentPage); i == PageBlah.CurrentPage; i++)
                {
                    TagBuilder tb = new TagBuilder("a");
                    tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });
                    tb.Attributes["class"] = "btn btn-primary";
                    tb.InnerHtml.Append((i).ToString());
                    final.InnerHtml.AppendHtml(tb);
                }

                for (int i = (PageBlah.CurrentPage + 1); i <= (PageBlah.CurrentPage + pagebuttonsides); i++)
                {
                    TagBuilder tb = new TagBuilder("a");
                    tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });
                    tb.Attributes["class"] = "btn btn-secondary";
                    tb.InnerHtml.Append((i).ToString());
                    final.InnerHtml.AppendHtml(tb);
                }
            }
       

            tho.Content.AppendHtml(final.InnerHtml);

        }
    }
}
