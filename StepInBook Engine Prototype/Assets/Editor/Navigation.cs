
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class Navigation : EditorWindow
{
    #region functions

    [MenuItem("Editor/Scene navigation\t..")]
    static void Setup()
    {
        Navigation window = (Navigation)EditorWindow.GetWindow(typeof(Navigation), false, "Navigation");

        window.titleContent = new GUIContent("<color=#00cc00>Navigation</color>", Resources.Load("navigationIcon", typeof(Texture2D)) as Texture2D);

        window.minSize = new Vector2(10, 10);

        window.Show();
    }

    void OnGUI()
    {
        OnGUI_Panel();

        OnGUI_Scene();
    }

    #endregion

    #region functions

    void OnGUI_Panel()
    {
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        flagShowFullName = GUILayout.Toggle(flagShowFullName, "Show pathname", EditorStyles.toolbarButton);
        flagHideDisabled = GUILayout.Toggle(flagHideDisabled, "Hide disabled", EditorStyles.toolbarButton);
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Player settings", EditorStyles.toolbarButton))
        {
            EditorApplication.ExecuteMenuItem("Edit/Project Settings/Player");
        }

        GUILayout.EndHorizontal();
        GUILayout.Space(-1);
    }

    void OnGUI_Scene()
    {
        string m_colored = "";

        for (int i = 0, c = 0, m = EditorBuildSettings.scenes.Length; i < m; i++)
        {
            if (flagHideDisabled && EditorBuildSettings.scenes[i].enabled == false)
            {
                continue;
            }

            if (EditorBuildSettings.scenes[i].path == SceneManager.GetActiveScene().path)
            {
                GUI.color = Color.green;
                m_colored = "<color=black>";
            }
            else
            {
                GUI.color = new Color(0.3f, 0.3f, 0.3f, 1.0f);
                m_colored = "<color=white>";
            }

            GUILayout.BeginHorizontal(styleBackground);

            if (GUILayout.Button(m_colored + (flagShowFullName ? EditorBuildSettings.scenes[i].path : EditorBuildSettings.scenes[i].path.Substring(EditorBuildSettings.scenes[i].path.LastIndexOf("/") + 1).Split('.')[0]) + "</color>", styleButton))
            {
                if (Application.isPlaying)
                {
                    SceneManager.LoadScene(EditorBuildSettings.scenes[i].path);
                }
                else
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorSceneManager.OpenScene(EditorBuildSettings.scenes[i].path);
                    }
                }
            }

            GUILayout.Button(m_colored + (EditorBuildSettings.scenes[i].enabled ? ((c++).ToString()) : "disabled") + "</color>", styleIndex);
            GUILayout.EndHorizontal();
            GUILayout.Space(-1);
        }
    }

    #endregion

    #region variables

    bool flagShowFullName = false;

    bool flagHideDisabled = false;

    #endregion

    #region variables

    GUIStyle _styleButton = null;
    GUIStyle styleButton
    {
        get
        {
            if (true)
            {
                _styleButton = new GUIStyle();
                _styleButton.stretchWidth = true;
                _styleButton.richText = true;
                _styleButton.margin = new RectOffset(10, 0, 0, 0);
                _styleButton.alignment = TextAnchor.MiddleLeft;
                _styleButton.fixedHeight = 25;
            }

            return (_styleButton);
        }
    }

    GUIStyle _styleBackground = null;
    GUIStyle styleBackground
    {
        get
        {
            if (true)
            {
                _styleBackground = new GUIStyle("box");
                _styleBackground.normal.background = Resources.Load("background", typeof(Texture2D)) as Texture2D;
                _styleBackground.margin = new RectOffset(0, 0, 0, 0);
                _styleBackground.stretchWidth = true;
            }

            return (_styleBackground);
        }
    }

    GUIStyle _styleIndex = null;
    GUIStyle styleIndex
    {
        get
        {
            if (true)
            {
                _styleIndex = new GUIStyle();
                _styleIndex.alignment = TextAnchor.MiddleRight;
                _styleIndex.richText = true;
                _styleIndex.margin = new RectOffset(0, 10, 0, 0);
                _styleIndex.fixedHeight = 25;
                _styleIndex.stretchWidth = false;
            }

            return (_styleIndex);
        }
    }

    #endregion
}
