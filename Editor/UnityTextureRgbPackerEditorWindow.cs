using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UnityTextureRgbPackerEditorWindow : EditorWindow
{
    private VisualElement _root;
    
    [MenuItem("Texture RGB Packer/Open Texture Packer")]
    public static void ShowWindow()
    {
        // Opens the window, otherwise focuses it if it’s already open.
        var window = GetWindow<UnityTextureRgbPackerEditorWindow>();

        // Adds a title to the window.
        window.titleContent = new GUIContent("Texture RGB Packer");

        // Sets a minimum and maximum size to the window.
        window.minSize = new Vector2(550, 350);
    }
    
    private void OnEnable()
    {
        // Reference to the root of the window.
        _root = rootVisualElement;
        // Associates a stylesheet to our root. Thanks to inheritance, all root’s
        // children will have access to it.
        _root.styleSheets.Add(Resources.Load<StyleSheet>("UnityTextureRgbPacker"));

        // Loads and clones our VisualTree (eg. our UXML structure) inside the root.
        var mainVisualTree = Resources.Load<VisualTreeAsset>("UnityTextureRgbPacker");
        mainVisualTree.CloneTree(_root);
    }
}
