using Hwyeom.Services.Bridges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hwyeom.Services.Interfaces
{
    public interface IPasswordHasher
    {
        string GetGUIDSalt();
        string GetRNGSalt();
        string GetPasswordHash(string userId, string password, string rngSalt, string guidSalt);

        bool MacthTheUserInfo(string userId, string password, string rngSalt, string guidSalt, string passwordHash);

        /// <summary>
        /// 사용자 가입을 위해 Pwd 정보를 생성 후 반환함
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        PasswordHashInfo SetPasswordInfo(string userid, string password);
    }
}
