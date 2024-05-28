using Microsoft.EntityFrameworkCore;
using Vote2.IService;
using Vote2.Models;
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
            var GetQuestion = _Context.Questions.Where(c=>c.QuestionTypeId==2 && c.Cancelled == false).ToList();

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
        public async Task<List<ItemDropdownListViewModel>> GetddlQuestionsByVoteId(Int64 VoteId)
        {
            List<ItemDropdownListViewModel> itemDropdownListViewModel = new List<ItemDropdownListViewModel>();
            var Question = await _Context.Questions.Where(i => i.VoteId == VoteId).ToListAsync();

            itemDropdownListViewModel = Question.Select(x => new ItemDropdownListViewModel()
            {
                Id = x.Id,
                Name = x.QuestionName,
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
                       join _Faculty in _Context.Faculties
                       on _Votes.FacultyId equals _Faculty.Id
                       join _Department in _Context.Departments
                       on _Votes.DepartmentId equals _Department.Id
                       join _Section in _Context.Sections
                       on _Votes.SectionId equals _Section.Id
                       join _Level in _Context.Levels   
                       on _Votes.LevelId equals _Level.Id

                        where _Votes.Cancelled == false 
                        select new VoteInfoViewModel
                        {
                            Id = _Votes.Id,
                            FacultyName = _Faculty.Name,
                            DepartmentName = _Department.Name,
                            SectionName = _Section.Name,
                            LevelName  = _Level.Level, 
                            FacultyId = _Votes.FacultyId,
                            DepartmentId = _Votes.DepartmentId,
                            StartDate = _Votes.StartDate,
                            EndDate = _Votes.EndDate,
                            IsActive = _Votes.IsActive,
                            LevelId = _Votes.LevelId,
                            VoteName = _Votes.VoteName,
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
                        join _Vote in _Context.Votes
                        on _Question.VoteId equals _Vote.Id
                        where _Question.Cancelled == false
                        select new QuestionViewModel
                        {
                            Id = _Question.Id,
                            VoteName = _Vote.VoteName,
                            VoteId = _Question.VoteId,
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
        public IQueryable<QuestionAnswerViewModel> GetAllQuestionAnswer()
        {
            try
            {
                return (from _QuestionAnswer in _Context.QuestionAnswer
                        join _Vote in _Context.Votes
                        on _QuestionAnswer.VoteId equals _Vote.Id
                        join _Question in _Context.Questions
                        on _QuestionAnswer.QuestionId equals _Question.Id
                        where _QuestionAnswer.Cancelled == false
                        select new QuestionAnswerViewModel
                        {
                            Id = _QuestionAnswer.Id,
                            VoteName = _Vote.VoteName,
                            QuestionName = _Question.QuestionName,
                           
                            VoteId = _QuestionAnswer.VoteId,
                            AnswerName = _QuestionAnswer.AnswerName,
                            QuestionId = _QuestionAnswer.QuestionId,
                            ModifiedBy = _QuestionAnswer.ModifiedBy,
                            CreatedBy = _QuestionAnswer.CreatedBy,
                            ModifiedDate = _QuestionAnswer.ModifiedDate,
                            CreatedDate = _QuestionAnswer.CreatedDate,
                            Cancelled = _QuestionAnswer.Cancelled,
                        }).OrderByDescending(x => x.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
