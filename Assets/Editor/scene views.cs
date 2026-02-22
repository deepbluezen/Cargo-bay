using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class SceneViewBookmarkTool : EditorWindow
{
    [System.Serializable]
    public class Bookmark
    {
        public Vector3 position;
        public Quaternion rotation;
        public string name;
    }

    [System.Serializable]
    public class BookmarkList
    {
        public List<Bookmark> bookmarks = new List<Bookmark>();
    }

    private static List<Bookmark> bookmarks = new List<Bookmark>();
    private static string lastSceneName = "";

    // For renaming
    private int renameIndex = -1;
    private string renameText = "";

    private static string GetPrefsKey(string sceneName)
    {
        return "SceneViewBookmarks_" + sceneName;
    }

    [MenuItem("Window/Custom/Scene View Bookmarks")]
    public static void ShowWindow()
    {
        var window = GetWindow<SceneViewBookmarkTool>("Scene Bookmarks");
        window.minSize = new Vector2(260, 60);
        window.maxSize = new Vector2(260, 240);
        LoadBookmarks();
        window.Show();
    }

    private void OnEnable()
    {
        lastSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        LoadBookmarks();
        UnityEditor.SceneManagement.EditorSceneManager.activeSceneChangedInEditMode += OnSceneChanged;
    }

    private void OnDisable()
    {
        SaveBookmarks();
        UnityEditor.SceneManagement.EditorSceneManager.activeSceneChangedInEditMode -= OnSceneChanged;
    }

    private void OnSceneChanged(UnityEngine.SceneManagement.Scene oldScene, UnityEngine.SceneManagement.Scene newScene)
    {
        SaveBookmarks(lastSceneName); // Save old scene's bookmarks
        lastSceneName = newScene.name;
        LoadBookmarks(lastSceneName); // Load new scene's bookmarks
        renameIndex = -1;
        renameText = "";
        Repaint();
    }

    private static void LoadBookmarks(string sceneName = null)
    {
        if (sceneName == null)
            sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string key = GetPrefsKey(sceneName);
        string json = EditorPrefs.GetString(key, "");
        if (!string.IsNullOrEmpty(json))
        {
            var list = JsonUtility.FromJson<BookmarkList>(json);
            bookmarks = list != null ? list.bookmarks : new List<Bookmark>();
        }
        else
        {
            bookmarks = new List<Bookmark>();
        }
    }

    private static void SaveBookmarks(string sceneName = null)
    {
        if (sceneName == null)
            sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string key = GetPrefsKey(sceneName);
        var list = new BookmarkList { bookmarks = bookmarks };
        string json = JsonUtility.ToJson(list);
        EditorPrefs.SetString(key, json);
    }

    private void OnGUI()
    {
        GUILayout.Label("Scene View Bookmarks", EditorStyles.boldLabel);

        if (GUILayout.Button("Bookmark Current Scene View"))
        {
            var sceneView = SceneView.lastActiveSceneView;
            if (sceneView != null)
            {
                bookmarks.Add(new Bookmark
                {
                    position = sceneView.camera.transform.position,
                    rotation = sceneView.camera.transform.rotation,
                    name = "Bookmark " + (bookmarks.Count + 1)
                });
                SaveBookmarks();
                Repaint();
            }
            else
            {
                EditorGUILayout.HelpBox("No active SceneView found.", MessageType.Warning);
            }
        }

        GUILayout.Space(10);

        for (int i = 0; i < bookmarks.Count; i++)
        {
            GUILayout.BeginHorizontal();

            if (renameIndex == i)
            {
                renameText = GUILayout.TextField(renameText, GUILayout.Width(120));
                if (GUILayout.Button("Save", GUILayout.Width(40)))
                {
                    bookmarks[i].name = string.IsNullOrWhiteSpace(renameText) ? bookmarks[i].name : renameText;
                    renameIndex = -1;
                    renameText = "";
                    SaveBookmarks();
                    Repaint();
                }
                if (GUILayout.Button("Cancel", GUILayout.Width(50)))
                {
                    renameIndex = -1;
                    renameText = "";
                    Repaint();
                }
            }
            else
            {
                if (GUILayout.Button(bookmarks[i].name, GUILayout.Width(120)))
                {
                    var sceneView = SceneView.lastActiveSceneView;
                    if (sceneView != null)
                    {
                        sceneView.pivot = bookmarks[i].position;
                        sceneView.rotation = bookmarks[i].rotation;
                        sceneView.Repaint();
                    }
                }
                if (GUILayout.Button("Rename", GUILayout.Width(50)))
                {
                    renameIndex = i;
                    renameText = bookmarks[i].name;
                }
            }

            if (GUILayout.Button("Delete", GUILayout.Width(50)))
            {
                bookmarks.RemoveAt(i);
                SaveBookmarks();
                renameIndex = -1;
                renameText = "";
                Repaint();
                GUILayout.EndHorizontal();
                break; // Avoid GUI errors after modifying list
            }

            GUILayout.EndHorizontal();
        }
    }
}
