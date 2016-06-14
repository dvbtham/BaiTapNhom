using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace itforum_teamwork.Common
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int? RoleID { get; set; }
    }
}