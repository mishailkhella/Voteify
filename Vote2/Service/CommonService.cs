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
        public List<ItemDropdownListViewModel> GetddlQuestions()
        {
            List<ItemDropdownListViewModel> itemDropdownListViewModel = new List<ItemDropdownListViewModel>();
            var GetQuestion = _Context.Questions.ToList();

            itemDropdownListViewModel = GetQuestion.Select(x => new ItemDropdownListViewModel()
            {
                Id = x.Id,
                Name = x.QuestionName,
            }).ToList();

            return itemDropdownListViewModel;
        }
        public List<ItemDropdownListViewModel> GetddlQuestionType()
        {
            List<ItemDropdownListViewModel> itemDropdownListViewModel = new List<ItemDropdownListViewModel>();
            var GetQuestion = _Context.Questions.ToList();

            itemDropdownListViewModel = GetQuestion.Select(x => new ItemDropdownListViewModel()
            {
                Id = x.QuestionTypeId,
                Name = x.QuestionName,
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
        public IQueryable<QuestionViewModel> GetAllQuestion()
        {
            try
            {
                return (from _Question in _Context.Questions
                        where _Question.Cancelled == false
                        select new QuestionViewModel
                        {
                            Id = _Question.Id,
                            VotedId = _Question.VoteId,
                            QuestionTypeId = _Question.QuestionTypeId,
                            QuestionName= _Question.QuestionName,
                            ModifiedBy = _Question.ModifiedBy,
                            CreatedBy = _Question.CreatedBy,
                            ModifiedDate = _Question.ModifiedDate,
                            CreatedDate = _Question.CreatedDate,
                            Cancelled = _Question.Cancelled,
                        }).OrderByDescending(x => x.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }
}
