using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

public class JsonLocalizer
{
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JsonLocalizer(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _env = env;
        _httpContextAccessor = httpContextAccessor;
    }

    public string CurrentCulture
    {
        get
        {
            var cookie = _httpContextAccessor.HttpContext?.Request.Cookies[Microsoft.AspNetCore.Localization.CookieRequestCultureProvider.DefaultCookieName];
            if (!string.IsNullOrEmpty(cookie) && cookie.Contains("ro"))
            {
                return "ro";
            }
            return "en";
        }
    }

    public string this[string key]
    {
        get
        {
            var culture = CurrentCulture;

            var filePath = Path.Combine(_env.ContentRootPath, "Resources", $"{culture}.json");

            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(_env.ContentRootPath, "Resources", "en.json");
            }

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty(key, out var val))
                {
                    return val.GetString() ?? key;
                }
            }

            return key;
        }
    }
}