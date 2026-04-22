namespace FamiliarFriends;

public static class UserInterfaceRegistry
{
    public static void RegisterAll(UserInterfaceService uiService)
    {
        uiService.Add("Test", "H", new TestPanel());
        uiService.Add("Test2", "J", new TestPanel2());
    }
}
