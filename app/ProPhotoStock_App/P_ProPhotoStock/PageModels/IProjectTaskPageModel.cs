using CommunityToolkit.Mvvm.Input;
using P_ProPhotoStock.Models;

namespace P_ProPhotoStock.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}