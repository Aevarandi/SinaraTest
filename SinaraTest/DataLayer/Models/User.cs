using System.ComponentModel.DataAnnotations;

namespace SinaraTest.Data;

//Модельки с данными не рекомендуют прокидывать на фронт, но для этого небольшого сервиса мне кажется излишним 
//заводить еще одну модель пользователя
public class User
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Не указано имя пользователя")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Длина имени от 1 до 100 символов")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Не указана фамилия пользователя")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Длина фамилии от 1 до 100 символов")]
    public string Surname { get; set; }

    public string Patronymic { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\-\.]{0,61}[a-zA-Z]\\\w[\w\.\- ]+$",
        ErrorMessage = "Username должен содержать домен и быть не меньше 7 символов длинной")]
    public string Username { get; set; }

    public bool Deleted { get; set; }
}