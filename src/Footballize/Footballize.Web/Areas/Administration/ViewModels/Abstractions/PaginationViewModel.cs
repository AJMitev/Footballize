﻿namespace Footballize.Web.Areas.Administration.ViewModels.Abstractions
{
    using System.Collections.Generic;

    public abstract class PaginationViewModel<T> where T : class
    {

        public IEnumerable<T> Items { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int ItemsCount { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;
    }
}