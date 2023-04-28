using Hwyeom.Services.Bridges;
using Hwyeom.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hwyeom.Services.Service
{
    public class PasswordHasher : IPasswordHasher
    {
        #region private
        private string GetGUIDSalt()
        {
            return Guid.NewGuid().ToString();
        }

        private string GetRNGSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
        private string GetPasswordHash(string userId, string password, string rngSalt, string guidSalt)
        {
            //User Id는 PW검증 시 소문자 처리
            //KeyDerivation = 패키지 Microsoft.AspNetCore.Cryptography.KeyDerivation
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: rngSalt + userId.ToLower() + password + guidSalt,
                salt: Encoding.UTF8.GetBytes(rngSalt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 25000,
                numBytesRequested: 256 / 8));
        }

        private bool MacthTheUserInfo(string userId, string password, string rngSalt, string guidSalt, string passwordHash)
        {
            string inputPasswordHash = GetPasswordHash(userId, password, rngSalt, guidSalt);
            return inputPasswordHash.Equals(passwordHash);
        }

        private PasswordHashInfo SetPasswordInfo(string userid, string password)
        {
            string guidSalt = GetGUIDSalt();
            string rngSalt = GetRNGSalt();

            return new PasswordHashInfo()
            {
                GUIDSalt = guidSalt,
                RNGSalt = rngSalt,
                PasswordHash = GetPasswordHash(userid, password, rngSalt, guidSalt)
            };
        }

        #endregion

        string IPasswordHasher.GetGUIDSalt()
        {
            return GetGUIDSalt();
        }

        string IPasswordHasher.GetPasswordHash(string userId, string password, string rngSalt, string guidSalt)
        {
            return GetPasswordHash(userId, password, rngSalt, guidSalt);
        }

        string IPasswordHasher.GetRNGSalt()
        {
            return GetRNGSalt();
        }

        bool IPasswordHasher.MacthTheUserInfo(string userId, string password, string rngSalt, string guidSalt, string passwordHash)
        {
            return MacthTheUserInfo(userId, password, rngSalt, guidSalt, passwordHash);
        }

        PasswordHashInfo IPasswordHasher.SetPasswordInfo(string userid, string password)
        {
            return SetPasswordInfo(userid, password);
        }
    }
}
