using System.Collections;
using WebStore.Domain.Base;
using WebStore.ViewModels;

namespace WebStore.Mapping
{
    public class VisitorMapper
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
    }
}
