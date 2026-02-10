using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Base.Interfaces;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class StarsRateViewComponent: ViewComponent
    {
        private readonly IBlogUserRate bur;

        public StarsRateViewComponent(IBlogUserRate _bur)
        {
            bur = _bur;
        }
        public IViewComponentResult Invoke(int BlogID, string UserID)
        {
            var srvm = new StarsRateViewModel
            {
                VotesAmount = bur.VoteNumber(BlogID),
                AverageRate = bur.AverageRate(BlogID),
                Rate = bur.UserVote(UserID, BlogID)
            };
            return View(srvm);
        }
    }
}
