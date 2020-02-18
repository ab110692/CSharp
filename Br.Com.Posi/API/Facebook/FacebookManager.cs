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
            maps.Add("client_id", /*"254016804756509"*/appID);
            maps.Add("redirect_uri", "http://localhost:49496/home/retornofb");
            maps.Add("response_type", "code");
            maps.Add("display", "page");
            maps.Add("extendedPermissions", "user_about_me,read_stream,publish_stream");
            maps.Add("scope", "extendedPermissions");
            /* parameters = new dynamic();
            parameters.client_id = "254016804756509";
            parameters.redirect_uri = "http://localhost:49496/home/retornofb";
            parameters.response_type = "code";
            parameters.display = "page";
            string extendedPermissions = "user_about_me,read_stream,publish_stream";
            parameters.scope = extendedPermissions;*/
            var url = client.GetLoginUrl(new
            {
                client_id = appID,
                client_secret = "d417ef6d72b9cdb430f938eb19c1b929",
                redirect_uri = "http://localhost:49496/home/retornofb",
                response_type = "code",
                scope = "user_birthday, email"
            });
            return url.ToString();
        }
    }
}
