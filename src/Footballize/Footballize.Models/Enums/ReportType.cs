namespace Footballize.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum ReportType
    {
        [Display(Name = "Did not show")]
        DidNotShow = 1,
        Abusing = 2,
    }
}