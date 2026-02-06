using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Base;

namespace WebStore.Domain.Identity
{
    public class User: IdentityUser
    {
        public User()
        {
            regDate = DateTime.Today;
        }
        public DateTime regDate { get; set; }
        public string? user_Name { get; set; }

        ICollection<Blog> Blogs { get; set; } = new HashSet<Blog>();
        public override string ToString() => UserName!;
        
    }

}
