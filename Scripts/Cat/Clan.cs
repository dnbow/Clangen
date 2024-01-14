using Clangen.Managers;

namespace Clangen.Cats
{
    /// <summary>
    /// Represents the current gamemode
    /// </summary>
    public enum GamemodeType : byte
    {
        Classic, Expanded, Cruel
    }

    /// <summary>
    /// Represents the current loaded Save and Clan
    /// </summary>
    public class Clan
    {
        public GamemodeType Gamemode;

        public CatManager Cats = new CatManager();
    }
}
