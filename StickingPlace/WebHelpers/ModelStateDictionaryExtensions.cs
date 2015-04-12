using System.Collections.Generic;
using System.Linq;
using System.Web.ModelBinding;

namespace StickingPlace
{
    public static class ModelStateDictionaryExtensions
    {
        public static IEnumerable<string> GetAllErrors(this ModelStateDictionary @this)
        {
            return @this.SelectMany(x => x.Value.Errors).Select(e => e.ErrorMessage);
        }
    }
}