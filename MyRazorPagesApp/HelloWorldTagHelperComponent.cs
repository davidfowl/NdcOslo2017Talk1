using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRazorPagesApp
{
    public class HelloWorldTagHelperComponent : ITagHelperComponent
    {
        public int Order => 1;

        public void Init(TagHelperContext context)
        {
            
        }

        public Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (string.Equals(context.TagName, "body", StringComparison.OrdinalIgnoreCase))
            {
                output.PreContent.AppendHtml("<h1>Hello, World!</h1>");
            }

            return Task.CompletedTask;
        }
    }
}
