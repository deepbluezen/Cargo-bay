using UnityEngine;
using UnityEditor;
using System.Reflection;

public class MaximizeWindowEditor : EditorWindow
{
    private static EditorWindow previouslyFocusedWindow;

    // Add a menu item that maximizes the currently focused editor window
    [MenuItem("Window/Custom/Maximize Current Window %m")] // Ctrl/Cmd + M shortcut
    public static void MaximizeCurrentWindow()
    {
        EditorWindow targetWindow = previouslyFocusedWindow;

        if (targetWindow != null)
        {
            // Try to use the old internal method first
            MethodInfo toggleMaximized = typeof(EditorWindow).GetMethod("ToggleMaximized", BindingFlags.Instance | BindingFlags.NonPublic);
            if (toggleMaximized != null)
            {
                toggleMaximized.Invoke(targetWindow, null);
            }
            else
            {
                // Fallback: use the 'maximized' property via reflection
                PropertyInfo maximizedProp = typeof(EditorWindow).GetProperty("maximized", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (maximizedProp != null && maximizedProp.CanWrite)
                {
                    bool isMaximized = (bool)maximizedProp.GetValue(targetWindow);
                    maximizedProp.SetValue(targetWindow, !isMaximized);
                }
                else
                {
                    Debug.LogWarning("Could not find a way to maximize the window. Unity 6 API might have changed.");
                }
            }
        }
        else
        {
            Debug.LogWarning("No previous window to maximize. Click on a window before using the panel.");
        }
    }

    // Optional: create a button in a small floating window for convenience
    [MenuItem("Window/Custom/Maximize Current Window Panel")]
    public static void ShowWindowPanel()
    {
        // Store the currently focused window before opening the panel
        previouslyFocusedWindow = EditorWindow.focusedWindow;
        MaximizeWindowEditor window = GetWindow<MaximizeWindowEditor>("Maximize");
        window.minSize = new Vector2(250, 80); // Set minimum size just bigger than button
        window.maxSize = new Vector2(250, 80); // Fix maximum size to match
        window.Show();
    }

    private void OnInspectorUpdate()
    {
        // Poll for focus changes, but ignore if this panel is focused
        var focused = EditorWindow.focusedWindow;
        if (focused != null && focused != this && focused != previouslyFocusedWindow)
        {
            previouslyFocusedWindow = focused;
            Repaint();
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Maximize Last Selected Window", EditorStyles.boldLabel);
        if (previouslyFocusedWindow != null)
        {
            GUILayout.Label("Target: " + previouslyFocusedWindow.titleContent.text);
        }
        else
        {
            GUILayout.Label("No window selected.");
        }
        if (GUILayout.Button("Toggle Maximize Selected Window", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
        {
            MaximizeCurrentWindow();
        }
    }
}
