using Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services.Contracts;

namespace StoreApp.Infrastructure.TagHelpers
{

    [HtmlTargetElement("div", Attributes = "products")]
    public class LastestProductTagHelper : TagHelper
    {
        private readonly IServiceManager _manager;

        [HtmlAttributeName("number")]
        public int Number { get; set; }

        public LastestProductTagHelper(IServiceManager manager)
        {
            _manager = manager;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("class", "my-3");

            TagBuilder h6 = new TagBuilder("h6");
            h6.Attributes.Add("class", "lead");

            TagBuilder i = new TagBuilder("i");
            i.Attributes.Add("class", "fa fa-box text-secondary");

            h6.InnerHtml.AppendHtml(i);
            h6.InnerHtml.AppendHtml(" Latest Products");


            TagBuilder ul = new TagBuilder("ul");

            var products = _manager.ProductService.GetLastestProduct(Number, false);
            foreach (Product product in products)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                a.Attributes.Add("href",$"/product/get/{product.ProductId}");
                a.InnerHtml.AppendHtml(product.ProductName);
                li.InnerHtml.AppendHtml(a);
                ul.InnerHtml.AppendHtml(li);

            }

            div.InnerHtml.AppendHtml(h6);
            div.InnerHtml.AppendHtml(ul);
            output.Content.AppendHtml(div);
        }

    }
}