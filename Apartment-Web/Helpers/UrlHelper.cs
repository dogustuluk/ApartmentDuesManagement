using System.Web;

namespace Apartment_Web.Helpers
{
    public class UrlHelper
    {
        private readonly IHttpContextAccessor _accessor;

        public UrlHelper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Get_RecentURL()
        {
            var absoluteUri = string.Concat(
                        _accessor.HttpContext.Request.Scheme,
                        "://",
                        _accessor.HttpContext.Request.Host.ToUriComponent(),
                        _accessor.HttpContext.Request.PathBase.ToUriComponent(),
                        _accessor.HttpContext.Request.Path.ToUriComponent(),
                        _accessor.HttpContext.Request.QueryString.ToUriComponent());

            return absoluteUri;
        }

        public string RemoveQueryStringByKey(string url, string key)
        {
            var uri = new Uri(url);

            // this gets all the query string key value pairs as a collection
            var newQueryString = HttpUtility.ParseQueryString(uri.Query);

            // this removes the key if exists
            newQueryString.Remove(key);

            // this gets the page path from root without QueryString
            string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path);

            return newQueryString.Count > 0
                ? String.Format("{0}?{1}", pagePathWithoutQueryString, newQueryString)
                : pagePathWithoutQueryString;
        }
    }
}
