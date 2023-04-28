using Hwyeom.Data.DataModels;
using Hwyeom.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hwyeom.Services.Database
{
    public class MariaDbInitializer
    {
        private MariaDbContext _context;
        private IPasswordHasher _hasher;

        public MariaDbInitializer(MariaDbContext context, IPasswordHasher hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        /// <summary>
        /// 초기 데이터 입력
        /// </summary>
        public int PlantSeedData()
        {
            int rowAffeced = 0;
            string userId = "hwyeom";
            string password = "123123";
            var passwordHasher = _hasher.SetPasswordInfo(userId, password);
            var utcNow = DateTime.UtcNow;

            //DataBase 없으면 자동생성 (혹시 모르니.. 미리 생성할 것)
            //_context.Database.EnsureCreated();

            //User Table에 값이 없으면
            if (!_context.User.Any())
            {
                var users = new List<User>()
                {
                    new User()
                    {
                        UserId = userId.ToLower(),
                        UserName = "Seed 사용자",
                        UserEmail = "hwyeom@fdxnetworks.com",
                        GUIDSalt = passwordHasher.GUIDSalt,
                        RNGSalt = passwordHasher.RNGSalt,
                        PasswordHash = passwordHasher.PasswordHash,
                        AccessFailedCount = 0,
                        IsMembershipWithdrawn = false,
                        JoinDateUTC = utcNow
                    }
                };

                _context.User.AddRange(users);
                rowAffeced += _context.SaveChanges();
            }
            
            return rowAffeced;
        }
    }
}
