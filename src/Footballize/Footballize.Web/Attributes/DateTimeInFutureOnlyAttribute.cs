namespace Footballize.Web.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateTimeInFutureOnlyAttribute : ValidationAttribute
    {
        public DateTimeInFutureOnlyAttribute()
        {
        }

        public DateTimeInFutureOnlyAttribute(string errorMessage) : base(errorMessage)
        {
        }

        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date > DateTime.Now;
            }

            return false;
        }
    }
}