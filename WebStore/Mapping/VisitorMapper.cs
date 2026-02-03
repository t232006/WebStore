using System.Collections;
using WebStore.Domain.Base;
using WebStore.Domain.Identity;
using WebStore.ViewModels;

namespace WebStore.Mapping
{
    public static class VisitorMapper
    {
        public static VisitorsViewModel? ToView(Visitor? v) => v is null ?
            null :
            new VisitorsViewModel
            {
                ID = v.ID,
                password = v.password,
                Name = v.Name,
                e_mail = v.e_mail,
                login = v.login,
            };
        //public static IEnumerable<VisitorsViewModel> ToView 
        public static EditUserViewModel? ToView(this User? u) => u is null ?
            null :
            new EditUserViewModel
            {
                Email = u.Email,
                UserName = u.UserName,
                user_Name = u.user_Name,
                NumberID = u.Id,
                regDate = u.regDate,
            };
        public static User? FromView(this EditUserViewModel? eu) => eu is null ?
           null :
           new User
           {
               Email = eu.Email,
               UserName = eu.UserName,
               user_Name = eu.user_Name,
               Id = eu.NumberID,
               NormalizedUserName = eu.UserName.ToUpper(),
               regDate = eu.regDate
           };

    }
}
