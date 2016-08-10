using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace DataAccess.HelperClasses
{
    public static class SessionService
    {

        public static void Add(object o, string key)
        {
            HttpContext.Current.Session.Add(key, o);
        }
        public static void AddCookie(string o, string key)
        {
            //if (HttpContext.Current.Request.Cookies.AllKeys.Contains(key))
            //{
            //    HttpContext.Current.Request.Cookies.Clear();
            //}
            //HttpContext.Current.Request.Cookies.Clear();
            HttpContext.Current.Request.Cookies.Remove(key);
            var cook = new HttpCookie(key, o);
            cook.Expires = DateTime.Now.AddMonths(1);
            cook.Domain = ".socialclick411.com";
            
            HttpContext.Current.Response.Cookies.Add(cook);


        }
        public static long GetCookie(string key)
        {
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains(key))
            {
                 var cook= HttpContext.Current.Request.Cookies[key];
                 HttpContext.Current.Response.Cookies[key].Domain = ".socialclick411.com";
                return Convert.ToInt64(HttpContext.Current.Request.Cookies[key].Value);
            }
            return 0;
        }

        public static bool HasKey(string key)
        {
            if (HttpContext.Current.Session[key] == null)
                return false;
            else
                return true;
        }

        public static T Pull<T>(string key)
        {
            if (HasKey(key)) return (T)HttpContext.Current.Session[key];
            else return default(T);
        }
        public static long Pull(string key)
        {
            if (HasKey(key)) return Convert.ToInt64(HttpContext.Current.Session[key]);
            else return default(long);
        }
        public static T Pull_Remove<T>(string key)
        {
            var obj = Pull<T>(key);
            Remove(key);
            return obj;
        }

        public static void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }
    }
}
