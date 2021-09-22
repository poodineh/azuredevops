using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;

namespace UI.Tests.Utilities
{
    public class HtmlHelpers
    {
        public static async Task<IHtmlDocument> GetDocumentAsync(HttpResponseMessage response)
        {
            //Use the default configuration for AngleSharp
            var config = Configuration.Default;
            var content = await response.Content.ReadAsStringAsync();
            var document = await BrowsingContext.New(config)
                .OpenAsync(request => {
                    request.Content(content);
                }, CancellationToken.None);            
            return (IHtmlDocument)document;
        }
    }
}