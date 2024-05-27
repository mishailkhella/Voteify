using Vote2.ViewModels;

namespace Vote2.IService
{
    public interface ICommonService
    {
        List<ItemDropdownListViewModel> GetddlFaculties();
        List<ItemDropdownListViewModel> GetddlLevels();
        List<ItemDropdownListViewModel> GetddlQuestions();
        List<ItemDropdownListViewModel> GetddlQuestionType();
        Task<List<ItemDropdownListViewModel>> GetddlDepartementsByFacultyId(Int64 FacultyId);
        Task<List<ItemDropdownListViewModel>> GetddlSectionsByDepartementId(Int64 DepartementId);
        Task<List<ItemDropdownListViewModel>> GetddlQuestionsByVoteId(Int64 VoteId);
        IQueryable<VoteInfoViewModel> GetAllVotes();
        IQueryable<QuestionViewModel> GetAllQuestion();
        IQueryable<QuestionAnswerViewModel> GetAllQuestionAnswer();

    }
}
