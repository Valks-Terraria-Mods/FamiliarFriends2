namespace FamiliarFriends;

public class UserInterfaceRegistry(UserInterfaceService uiService)
{
    /// <summary>
    /// Registers all the ui elements in this mod.
    /// </summary>
    public void RegisterAll()
    {
        uiService.Insert("Test", "H", () => new CharacterCreatorPanel());
    }
}
