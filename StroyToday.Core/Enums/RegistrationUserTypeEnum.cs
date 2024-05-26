using System.ComponentModel.DataAnnotations;

namespace StroyToday.Core.Enums
{
    public enum RegistrationUserTypeEnum
    {
        [Display(Name = "Заказчик")]
        Client = 1,

        [Display(Name = "Исполнитель")]
        Executor = 2
    }
}
