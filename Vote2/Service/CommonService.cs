using Vote2.IService;
using Vote2.ViewModels;

namespace Vote2.Service
{
    public class CommonService : ICommonService
    {
        private readonly ApplicationDbContext _Context;

        public CommonService(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        public List<ItemDropdownListViewModel> GetddlFaculties()
        {
            List<ItemDropdownListViewModel> itemDropdownListViewModel = new List<ItemDropdownListViewModel>();
            var GetFaculty = _Context.Faculties.ToList();

            itemDropdownListViewModel = GetFaculty.Select(x => new ItemDropdownListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return itemDropdownListViewModel;
        }
    }
}
