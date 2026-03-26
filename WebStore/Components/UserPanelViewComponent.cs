using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Components
{
    public class UserPanelViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke() => User.Identity!.IsAuthenticated
            ? View("UserPanel")
            : View();
    }
}
