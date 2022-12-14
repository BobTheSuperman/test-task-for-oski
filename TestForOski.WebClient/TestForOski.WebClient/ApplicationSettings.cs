using System.Collections.Generic;

namespace TestForOski.WebClient
{
    public class ApplicationSettings
    {
        public CorsSettings Cors { get; set; }

        public UrlsSettings Urls { get; set; }

        public CookiesSettings Cookies { get; set; }
    }

    public class UrlsSettings
    {
        public ApiSettings Api { get; set; }

        public ApiEndpoints ApiEndpoints { get; set; }
    }

    public class ApiSettings
    {
        public string Http { get; set; }

        public string Https { get; set; }
    }

    #region ApiEndpoints

    public class ApiEndpoints
    {
        public AccountsApiEndpoints Accounts { get; set; }
    }

    public class AccountsApiEndpoints 
    {
        public string SignIn { get; set; }
    }

    #endregion 

    public class CookiesSettings
    {
        public string SecureKey { get; set; }
    }

    public class CorsSettings
    {
        public CorsSettings()
        {
            AllowedOrigins = new List<string>();
        }

        public List<string> AllowedOrigins { get; set; }
    }

}
