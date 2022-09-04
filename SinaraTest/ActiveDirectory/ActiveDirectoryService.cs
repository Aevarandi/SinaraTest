namespace SinaraTest;

public class ActiveDirectoryService : IActiveDirectoryService
{
    public bool IsUserExist(string username)
    {
        return username.Contains("Sinara", StringComparison.InvariantCultureIgnoreCase);
    }
}