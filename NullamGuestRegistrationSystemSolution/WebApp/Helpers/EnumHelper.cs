using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Helpers
{
    public static class EnumExtensions
    {
        // <summary>
        /// Method that retrieves the name specified in the Display attribute for an enum value.
        /// </summary>
        /// <param name="enumValue">The enum value for which the Display name is requested.</param>
        /// <returns>The name specified in the Display attribute, or the original enum name if no Display attribute is set.</returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                                            .GetField(enumValue.ToString())
                                            .GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.Name ?? enumValue.ToString();
        }
    }
}
