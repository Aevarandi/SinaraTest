using SinaraTest.Data;

namespace SinaraTest;

public static class SampleData
{
    public static void InitData(IUsersRepository usersRepository)
    {
        var user = new User
        {
            Name = "Иван",
            Surname = "Иванов",
            Patronymic = "Иванович",
            Username = "Sinara\\IIVanov",
        };
        usersRepository.SaveUser(user);
    }
}