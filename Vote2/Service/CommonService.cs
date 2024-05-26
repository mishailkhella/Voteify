using Microsoft.EntityFrameworkCore;
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
        public IQueryable<VoteInfoViewModel> GetAllVotes()
        {
            try
            {
                return (from _Votes in _Context.Votes
                        where _Votes.Cancelled == false
                        select new VoteInfoViewModel
                        {
                            Id = _Votes.Id,
                            FacultyId = _Votes.FacultyId,
                            DepartmentId = _Votes.DepartmentId,
                            UserId = _Votes.UserId,
                            QuestionId = _Votes.QuestionId,
                            LevelId = _Votes.LevelId,              
                            SectionId = _Votes.SectionId,
                            ModifiedBy = _Votes.ModifiedBy,
                            CreatedBy = _Votes.CreatedBy,
                            ModifiedDate = _Votes.ModifiedDate,
                            CreatedDate = _Votes.CreatedDate,
                            Cancelled = _Votes.Cancelled,
                        }).OrderByDescending(x => x.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
