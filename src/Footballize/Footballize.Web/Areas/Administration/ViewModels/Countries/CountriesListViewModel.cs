namespace Footballize.Web.Areas.Administration.ViewModels.Countries
{
    using Abstractions;

    public class CountriesListViewModel : PaginationViewModel<CountryIndexViewModel>
    {
        public const int ItemsPerPage = 10;
    }
}