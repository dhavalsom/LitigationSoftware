using System;
using System.Collections.Generic;

namespace LS.Models
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string IPAddress { get; set; }
        public int? RoleId { get; set; }
        public int? UserId { get; set; }
        public string GetCodeMethod { get; set; }
        public List<Role> RoleList { get; set; }
    }

    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsDefault { get; set; }
    }
    public class SignInResponse
    {
        public int? UserId { get; set; }
        public bool IsPasswordVerified { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public int? UserDeviceId { get; set; }
        public bool TwoFactorAuthDone { get; set; }
        public DateTime? TwoFactorAuthTimestamp { get; set; }
        public string SessionId { get; set; }
        public bool IsUserActive { get; set; }
    }


}
