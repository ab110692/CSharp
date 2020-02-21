using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;

namespace Br.Com.Posi.API.Facebook
{
    public class FacebookManager
    {

        private static FacebookManager _instance;

        public static FacebookManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FacebookManager();
            }
            return _instance;
        }

        public string Login(string appID, string appSecret)
        {
            FacebookClient client = new FacebookClient();
            client.AppId = appID;
            client.AppSecret = appSecret;

            Dictionary<string, object> maps = new Dictionary<string, object>();
            maps.Add("client_id", appID);
            maps.Add("redirect_uri", "http://localhost:49496/home/retornofb");
            maps.Add("response_type", "code");
            maps.Add("display", "page");
            maps.Add("extendedPermissions", "user_about_me,read_stream,publish_stream,manage_pages");
            maps.Add("scope", "extendedPermissions");
            var url = client. GetLoginUrl(new
            {
                client_id = appID,
                client_secret = "d417ef6d72b9cdb430f938eb19c1b929",
                redirect_uri = "http://localhost:2460/home/index",
                response_type = "code",
                scope = "user_birthday, email"                
            });
            return url.ToString();
        }

        public string GetPostPage()
        {
            return string.Empty;
        }
    }
}
