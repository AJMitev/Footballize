namespace Footballize.Web.Areas.Administration.ViewModels.Pitches
{
    using Abstractions;

    public class PitchesListViewModel : PaginationViewModel<PitchIndexViewModel>
    {
        public const int ItemsPerPage = 10;
    }
}