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
        public List<ItemDropdownListViewModel> GetddlLevels()
        {
            List<ItemDropdownListViewModel> itemDropdownListViewModel = new List<ItemDropdownListViewModel>();
            var Levels = _Context.Levels.OrderBy(i => i.levelDegree).ToList();

            itemDropdownListViewModel = Levels.Select(x => new ItemDropdownListViewModel()
            {
                Id = x.Id,
                Name = x.Level,
            }).ToList();

            return itemDropdownListViewModel;
        }
        public async Task<List<ItemDropdownListViewModel>> GetddlDepartementsByFacultyId(Int64 FacultyId)
        {
            List<ItemDropdownListViewModel> itemDropdownListViewModel = new List<ItemDropdownListViewModel>();
            var Departments = await _Context.Departments.Where(i => i.FacultyId == FacultyId).ToListAsync();

            itemDropdownListViewModel = Departments.Select(x => new ItemDropdownListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return itemDropdownListViewModel;
        }
        public async Task<List<ItemDropdownListViewModel>> GetddlSectionsByDepartementId(Int64 DepartementId)
        {
            List<ItemDropdownListViewModel> itemDropdownListViewModel = new List<ItemDropdownListViewModel>();
            var Sections = await _Context.Sections.Where(i => i.DepartmentId == DepartementId && i.Cancelled == false).ToListAsync();

            itemDropdownListViewModel = Sections.Select(x => new ItemDropdownListViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return itemDropdownListViewModel;
        }
    }
}
