using Microsoft.AspNetCore.Mvc;
using WebStore.Servises.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData msd;
        public SectionsViewComponent(IProductData MSD) => msd = MSD;

        public IViewComponentResult Invoke()
        {
            var sections = msd.GetSections();
            var parent_section = sections.Where(s => s.ParentID is null).OrderBy(s => s.Order);
            var parent_section_views=parent_section.Select(s => new SectionsViewModel
            {
                ID=s.ID,
                Name=s.Name
            }).ToArray();
            foreach(var psv in parent_section_views)
            {
                var childs = sections.Where(s => s.ParentID == psv.ID).OrderBy(s => s.Order);
                foreach(var ch in childs)
                {
                    psv.ChildSection.Add(new SectionsViewModel
                    {
                        ID = ch.ID,
                        Name = ch.Name
                    });
                }
            }
            return View(parent_section_views);
        }
    }
}
