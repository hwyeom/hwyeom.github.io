using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hwyeom.Web.Extensions
{
    /// <summary>
    /// 세션 확장 메서드
    /// </summary>
    public static class SessionExtensions
    {
        //세션에 값을 넣기 위한 메서드
        public static void Set<T>(this ISession session, string key, List<T> values)
        {
            session.SetString(key, JsonConvert.SerializeObject(values));
        }

        //세션에 값을 넣기 위한 메서드
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var sessionValue = session.GetString(key);

            //default 참조형식 null, 숫자형식 0
            return sessionValue == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionValue);
        }
    }
}
