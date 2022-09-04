using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using SinaraTest.Data;

namespace Test;

public class UserTest
{
    [Test]
    public void ValidationSimple()
    {
        var user = new User
        {
            Name = "Иван",
            Surname = "Иванов",
            Patronymic = "Иванович",
            Username = "Sinara\\IIVanov",
        };
        var validationContext = new ValidationContext(user);
        Validator.ValidateObject(user, validationContext);
    }

    [TestCase(null, "Иванов", "Иванович", "Sinara\\IIVanov")]
    [TestCase("Иван", null, "Иванович", "Sinara\\IIVanov")]
    [TestCase("Иван", "Иванов", "Иванович", null)]
    [TestCase("", "Иванов", "Иванович", "Sinara\\IIVanov")]
    [TestCase("Иван", "", "Иванович", "Sinara\\IIVanov")]
    [TestCase("Иван", "Иванов", "Иванович", "")]
    public void ValidationEmptyOrNull(string name, string surmame, string patronymic, string userName)
    {
        var user = new User
        {
            Name = name,
            Surname = surmame,
            Patronymic = patronymic,
            Username = userName,
        };
        var validationContext = new ValidationContext(user);
        Assert.Throws<ValidationException>(delegate { Validator.ValidateObject(user, validationContext); });
    }

    [TestCase("Sinara\\IIVanov", true)]
    [TestCase("Cat", false)]
    [TestCase("SinaraIvanov", false)]
    [TestCase("\\SinaraIvanov", false)]
    [TestCase("Cat\\d", false)]
    [TestCase(null, false)]
    public void ValidationUserName(string username, bool isValid)
    {
        var user = new User
        {
            Username = username,
        };

        var validationContext = new ValidationContext(user)
        {
            MemberName = "Username"
        };

        if (isValid)
            Validator.ValidateProperty(user.Username, validationContext);
        else
            Assert.Throws<ValidationException>(delegate { Validator.ValidateProperty(user.Username, validationContext); });
    }
}