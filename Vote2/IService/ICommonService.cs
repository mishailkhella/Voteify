using Vote2.ViewModels;

namespace Vote2.IService
{
    public interface ICommonService
    {
        List<ItemDropdownListViewModel> GetddlFaculties();
        List<ItemDropdownListViewModel> GetddlLevels();
        Task<List<ItemDropdownListViewModel>> GetddlDepartementsByFacultyId(Int64 FacultyId);
        Task<List<ItemDropdownListViewModel>> GetddlSectionsByDepartementId(Int64 DepartementId);

    }
}
