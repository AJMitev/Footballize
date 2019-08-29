namespace Footballize.Web.Areas.Administration.ViewModels.Countries
{
    using Models;
    using Services.Mapping;

    public class CountryNameAndIdViewModel : IMapFrom<Country>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as CountryNameAndIdViewModel;

            return this.Id.Equals(other?.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}