using System.ComponentModel.DataAnnotations;

namespace App.Enum
{
    public enum AttendeeType
    {
        [Display(Name = "Eraisik")]
        Person = 1,

        [Display(Name = "Ettevõte")]
        Company
    }
}
