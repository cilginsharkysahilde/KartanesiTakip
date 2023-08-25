namespace Takip.Web.Code
{
    public class Repo
    {
        public static class Session
        {

            public static string? Email
            {
                get
                {
                    string eMail = (new HttpContextAccessor()).HttpContext.Session.GetString("Email");
                    return eMail;
                }
                set
                {
                    (new HttpContextAccessor()).HttpContext.Session.SetString("Email", value ?? "");
                }
            }
            public static string? Token
            {
                get
                {
                    string token = (new HttpContextAccessor()).HttpContext.Session.GetString("Token");
                    return token;
                }
                set
                {
                    (new HttpContextAccessor()).HttpContext.Session.SetString("Token", value ?? "");
                }
            }
            public static string? Role
            {
                get
                {
                    string role = (new HttpContextAccessor()).HttpContext.Session.GetString("Role");
                    return role;
                }
                set
                {
                    (new HttpContextAccessor()).HttpContext.Session.SetString("Role", value ?? "");
                }
            }

            public static string? Name
            {
                get
                {
                    string name = (new HttpContextAccessor()).HttpContext.Session.GetString("Name");
                    return name;
                }
                set
                {
                    (new HttpContextAccessor()).HttpContext.Session.SetString("Name", value ?? "");
                }
            }
        }
    }
}
