public static class Utils
{
    // Indicates whether we're in a lobby context. Conservative default: false.
    public static bool isLobby => false;

    // No-op safe default for adjusting resolution; provided for compatibility with Malum code.
    public static void AdjustResolution() { }

    // Best-effort name tag generator. Accepts either a NetworkedPlayerInfo-like object or a simple fallback.
    public static string GetNameTag(object playerData, string fallback, bool chatFormat = false)
    {
        if (playerData == null) return fallback ?? string.Empty;
        try
        {
            var type = playerData.GetType();

            // Try common properties used for player name
            var defaultOutfitProp = type.GetProperty("DefaultOutfit") ?? type.GetProperty("defaultOutfit");
            if (defaultOutfitProp != null)
            {
                var outfit = defaultOutfitProp.GetValue(playerData);
                if (outfit != null)
                {
                    var pnProp = outfit.GetType().GetProperty("PlayerName") ?? outfit.GetType().GetProperty("playerName");
                    if (pnProp != null)
                    {
                        var name = pnProp.GetValue(outfit) as string;
                        if (!string.IsNullOrEmpty(name)) return name;
                    }
                }
            }

            var nameProp = type.GetProperty("PlayerName") ?? type.GetProperty("playerName") ?? type.GetProperty("Name") ?? type.GetProperty("name");
            if (nameProp != null)
            {
                var name = nameProp.GetValue(playerData) as string;
                if (!string.IsNullOrEmpty(name)) return name;
            }

            // Fallback to ToString if nothing else
            return playerData.ToString() ?? fallback ?? string.Empty;
        }
        catch
        {
            return fallback ?? string.Empty;
        }
    }
}
