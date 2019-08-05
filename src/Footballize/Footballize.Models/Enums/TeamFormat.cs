namespace Footballize.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum TeamFormat
    {
        [Display(Name = "4 + 1")]
        FourPlusOne = 1,
        [Display(Name = "5 + 1")]
        FivePlusOne = 2,
        [Display(Name = "6 + 1")]
        SixPlusOne = 3,
        [Display(Name = "11 Players")]
        ElevenPlayers = 4
    }
}