namespace Footballize.Services.Data
{
    using System.Collections.Generic;

    public interface IGatherServices
    {
        ICollection<TViewModel> GetGathers<TViewModel>();
    }
}