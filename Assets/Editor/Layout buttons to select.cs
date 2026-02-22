using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
// seems limited as only built in think use existing one in top right of ui for now
//does save floating custom windows with normal layout save
public class LayoutButtonsWindow : EditorWindow
{
    private List<string> layoutNames = new List<string>();
    private string newLayoutName = "";

    [MenuItem("Window/Custom/Layout Buttons")]
    public static void ShowWindow()
    {
        var window = GetWindow<LayoutButtonsWindow>("Layouts");
        window.minSize = new Vector2(260, 60);
        window.maxSize = new Vector2(260, 600);
        window.LoadLayouts();
        window.Show();
    }

    private void OnEnable()
    {
        LoadLayouts();
    }

    private void OnFocus()
    {
        LoadLayouts();
        Repaint();
    }

    private void OnGUI()
    {
        GUILayout.Label("Editor Layouts", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        newLayoutName = GUILayout.TextField(newLayoutName, GUILayout.Width(120));
        if (GUILayout.Button("Save Layout", GUILayout.Width(120)))
        {
            if (string.IsNullOrWhiteSpace(newLayoutName))
            {
                EditorUtility.DisplayDialog("Error", "Please enter a layout name.", "OK");
            }
            else
            {
                // Open Unity's built-in Save Layout dialog
                EditorApplication.ExecuteMenuItem("Window/Layouts/Save Layout...");
                // User must enter the same name in the dialog
                // Layout will appear after Unity saves it
                newLayoutName = "";
                // Refresh after saving
                LoadLayouts();
                Repaint();
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        foreach (var layoutName in layoutNames)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(layoutName, GUILayout.Width(120)))
            {
                EditorApplication.ExecuteMenuItem("Window/Layouts/" + layoutName);
            }
            if (GUILayout.Button("Delete", GUILayout.Width(50)))
            {
                DeleteLayout(layoutName);
                LoadLayouts();
                Repaint();
                GUILayout.EndHorizontal();
                break;
            }
            GUILayout.EndHorizontal();
        }
    }

    private void LoadLayouts()
    {
        layoutNames.Clear();
        string layoutsFolder = Path.Combine(EditorApplication.applicationContentsPath, "Resources", "Layouts");
        string userLayoutsFolder = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Unity", "Editor", "Layouts");
        var layoutFiles = new List<string>();

        if (Directory.Exists(layoutsFolder))
            layoutFiles.AddRange(Directory.GetFiles(layoutsFolder, "*.wlt"));

        if (Directory.Exists(userLayoutsFolder))
            layoutFiles.AddRange(Directory.GetFiles(userLayoutsFolder, "*.wlt"));

        foreach (var file in layoutFiles)
        {
            string name = Path.GetFileNameWithoutExtension(file);
            if (!layoutNames.Contains(name))
                layoutNames.Add(name);
        }
    }

    private void DeleteLayout(string layoutName)
    {
        string userLayoutsFolder = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "Unity", "Editor", "Layouts");
        string layoutPath = Path.Combine(userLayoutsFolder, layoutName + ".wlt");
        if (File.Exists(layoutPath))
        {
            File.Delete(layoutPath);
        }
        else
        {
            EditorUtility.DisplayDialog("Error", "Layout file not found in user folder.", "OK");
        }
    }
}
