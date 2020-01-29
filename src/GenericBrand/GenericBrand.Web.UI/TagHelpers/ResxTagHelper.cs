using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericBrand.Web.UI.Controllers;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GenericBrand.Web.UI.TagHelpers
{
    [HtmlTargetElement("resx")]
    public class ResxTagHelper : TagHelper
    {
        public enum ResxGroup : int
        {
            Shared,
            Home,
            NoResource
        }

        public enum DisplayType : int
        {
            Html,
            Text
        }

        #region Attribute Properties
        [HtmlAttributeName("resx-group")]
        public ResxGroup ResourceGroup { get; set; } = ResxGroup.Shared;

        [HtmlAttributeName("resx-name")]
        public string ResourceName { get; set; } = "";

        [HtmlAttributeName("display-format")]
        public DisplayType Display { get; set; } = DisplayType.Html;

        /// <summary>
        /// Adds the value to the string: This is a parameter {0}
        /// </summary>
        [HtmlAttributeName("resx-param-0")]
        public string Parameter0 { get; set; } = "";

        /// <summary>
        /// Adds the value to the string: This is a parameter {1}
        /// </summary>
        [HtmlAttributeName("resx-param-1")]
        public string Parameter1 { get; set; } = "";

        /// <summary>
        /// Adds the value to the string: This is a parameter {2}
        /// </summary>
        [HtmlAttributeName("resx-param-2")]
        public string Parameter2 { get; set; } = "";

        /// <summary>
        /// Adds the value to the string: This is a parameter {3}
        /// </summary>
        [HtmlAttributeName("resx-param-3")]
        public string Parameter3 { get; set; } = "";

        /// <summary>
        /// Adds the value to the string: This is a parameter {4}
        /// </summary>
        [HtmlAttributeName("resx-param-4")]
        public string Parameter4 { get; set; } = "";

        #endregion

        #region View Context
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }    // Injected

        protected IHtmlGenerator Generator { get; }
        protected IDictionary<ResxGroup, IHtmlLocalizer> Localizers { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="generator">Injected IHtmlGenerator</param>
        public ResxTagHelper(IHtmlGenerator generator,
            IHtmlLocalizer<SharedResource> sharedLocalizer,
            IHtmlLocalizer<HomeController> homeLocalizer)
        {
            Generator = generator;

            Localizers = new Dictionary<ResxGroup, IHtmlLocalizer>();
            Localizers.Add(ResxGroup.Shared, sharedLocalizer);
            Localizers.Add(ResxGroup.Home, homeLocalizer);
        }
        #endregion

        #region Process
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            GenerateTag(context, output);
            await base.ProcessAsync(context, output);
        }

        /// <summary>
        /// Generate Anchor for Web Site
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        private void GenerateTag(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            if (ResourceGroup == ResxGroup.NoResource) { return; }
            LocalizedHtmlOutput(output);

            // If content isn't replaced, text defined in page is used
        }
        #endregion

        #region Output
        /// <summary>
        /// Localized Html String
        /// </summary>
        /// <param name="localizedString"></param>
        /// <param name="output"></param>
        private async void LocalizedHtmlOutput(TagHelperOutput output)
        {
            LocalizedHtmlString localizedString = Localizers[ResourceGroup][ResourceName];
            if (!localizedString.IsResourceNotFound)
            {
                if (!string.IsNullOrWhiteSpace(localizedString.Value))
                {
                    string value = localizedString.Value;
                    string formatted = string.Format(value, Parameter0, Parameter1, Parameter2, Parameter3, Parameter4);

                    if (Display == DisplayType.Html)
                    {
                        output.Content.SetHtmlContent(formatted);
                    }
                    else
                    {
                        output.Content.SetContent(formatted);
                    }

                    return;
                }
            }

            // If resource is not found use the original content.
            TagHelperContent childContent = await output.GetChildContentAsync();
            string originalContent = childContent.GetContent();
            string reformatted = string.Format(originalContent, Parameter0, Parameter1, Parameter2, Parameter3, Parameter4);

            if (Display == DisplayType.Html)
            {
                output.Content.SetHtmlContent(reformatted);
            }
            else
            {
                output.Content.SetContent(reformatted);
            }
        }
        #endregion
    }
}
