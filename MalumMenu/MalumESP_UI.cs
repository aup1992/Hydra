using UnityEngine;

namespace MalumMenu
{
    // Simple IMGUI-based window that toggles with Insert and can enable/disable
    // all ESP-related CheatToggles from MalumESP in one click.
    public class MalumESP_UI : MonoBehaviour
    {
        private static bool _visible = false;
        private Rect _windowRect = new Rect(10, 100, 260, 260);

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Init()
        {
            // Ensure there's a single persistent GameObject that hosts this UI
            var go = new GameObject("MalumESP_UI");
            DontDestroyOnLoad(go);
            go.AddComponent<MalumESP_UI>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                _visible = !_visible;
            }
        }

        private void OnGUI()
        {
            if (!_visible) return;
            _windowRect = GUI.Window(987654, _windowRect, WindowFunc, "Malum ESP");
        }

        private void WindowFunc(int id)
        {
            GUILayout.BeginVertical();

            if (GUILayout.Button("Enable ESP (All)"))
            {
                EnableAll();
            }

            if (GUILayout.Button("Disable ESP (All)"))
            {
                DisableAll();
            }

            GUILayout.Space(8);
            GUILayout.Label("Individual toggles:");

            // Individual toggles update the underlying CheatToggles directly so
            // other systems (like your existing MalumESP) react immediately.
            // If any of these fields don't exist in your project, you'll get a
            // compile error: adjust the names to match your CheatToggles class.
            try
            {
                CheatToggles.noShadows = GUILayout.Toggle(CheatToggles.noShadows, "No Shadows (Fullbright)");
                CheatToggles.zoomOut = GUILayout.Toggle(CheatToggles.zoomOut, "Zoom Out");
                CheatToggles.seeRoles = GUILayout.Toggle(CheatToggles.seeRoles, "See Roles");
                CheatToggles.seePlayerInfo = GUILayout.Toggle(CheatToggles.seePlayerInfo, "See Player Info");
                CheatToggles.seeGhosts = GUILayout.Toggle(CheatToggles.seeGhosts, "See Ghosts");
                CheatToggles.freecam = GUILayout.Toggle(CheatToggles.freecam, "Freecam");
            }
            catch
            {
                GUILayout.Label("Warning: CheatToggles fields not found. Ensure CheatToggles class exists.");
            }

            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        private void EnableAll()
        {
            try
            {
                CheatToggles.noShadows = true;
                CheatToggles.zoomOut = true;
                CheatToggles.seeRoles = true;
                CheatToggles.seePlayerInfo = true;
                CheatToggles.seeGhosts = true;
                CheatToggles.freecam = true;
            }
            catch {}
        }

        private void DisableAll()
        {
            try
            {
                CheatToggles.noShadows = false;
                CheatToggles.zoomOut = false;
                CheatToggles.seeRoles = false;
                CheatToggles.seePlayerInfo = false;
                CheatToggles.seeGhosts = false;
                CheatToggles.freecam = false;
            }
            catch {}
        }
    }
}
