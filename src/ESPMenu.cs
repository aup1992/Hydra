using UnityEngine;

namespace MalumMenu
{
    // This component creates a small in-game menu window with an "ESP" submenu and a toggle/button.
    // It uses OnGUI so it doesn't depend on the mod's existing menu system. If you have a central menu
    // you can copy the submenu code below into it, or let this component run alongside.
    public class ESPMenu : MonoBehaviour
    {
        private static bool _menuVisible = false;
        private static bool _espSubmenuVisible = false;
        private Rect _windowRect = new Rect(10, 50, 220, 140);

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Init()
        {
            var go = new GameObject("Hydra_ESP_Menu");
            GameObject.DontDestroyOnLoad(go);
            go.AddComponent<ESPMenu>();
        }

        private void OnGUI()
        {
            // Small top-left toggle button to show/hide the Hydra menu
            if (GUI.Button(new Rect(10, 10, 80, 30), "Hydra"))
            {
                _menuVisible = !_menuVisible;
            }

            if (!_menuVisible) return;

            _windowRect = GUI.Window(786512, _windowRect, DrawWindow, "Hydra Menu");
        }

        private void DrawWindow(int id)
        {
            GUILayout.BeginVertical();

            if (GUILayout.Button("ESP"))
            {
                _espSubmenuVisible = !_espSubmenuVisible;
            }

            if (_espSubmenuVisible)
            {
                GUILayout.BeginVertical("box");

                // ESP Toggle (controls MalumESP.Enabled)
                bool enabled = MalumESP.Enabled;
                bool toggled = GUILayout.Toggle(enabled, "Malum ESP");
                if (toggled != enabled)
                {
                    MalumESP.Enabled = toggled;
                }

                // You can add more ESP-related buttons here

                GUILayout.EndVertical();
            }

            GUILayout.EndVertical();

            // Allow dragging the window
            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }
    }
}
