using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityTextureRgbPacker;

public class UnityTextureRgbPackerEditorWindow : EditorWindow
{
    private VisualElement _root;
    private ObjectField _channelRedTextureObjectField;
    private ObjectField _channelGreenTextureObjectField;
    private ObjectField _channelBlueTextureObjectField;
    private ObjectField _channelAlphaTextureObjectField;
    private IntegerField _widthIntField;
    private IntegerField _heightIntField;
    private Toggle _tgaToggle;
    private Toggle _pngToggle;
    private TextField _nameIdentifierTextField;
    private Button _generatePackedTextureButton;
    private VisualElement _previewImageRedChannelVisualElement;
    private VisualElement _previewImageGreenChannelVisualElement;
    private VisualElement _previewImageBlueChannelVisualElement;
    private VisualElement _previewImageAlphaChannelVisualElement;
    private VisualElement _previewImageResultVisualElement;

    [MenuItem("Texture RGB(A) Packer/Open Texture Packer")]
    public static void ShowWindow()
    {
        // Opens the window, otherwise focuses it if it’s already open.
        var window = GetWindow<UnityTextureRgbPackerEditorWindow>();

        // Adds a title to the window.
        window.titleContent = new GUIContent("Texture RGB(A) Packer");

        // Sets a minimum and maximum size to the window.
        window.minSize = new Vector2(350, 600);
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
        var textureInputVisualTree = Resources.Load<VisualTreeAsset>("TextureInput");
        mainVisualTree.CloneTree(_root);
        
        // Query the needed fields to get the Visual Elements references
        _channelRedTextureObjectField = _root.Q<ObjectField>("OF_RedChannelTexture");
        _channelGreenTextureObjectField = _root.Q<ObjectField>("OF_GreenChannelTexture");
        _channelBlueTextureObjectField = _root.Q<ObjectField>("OF_BlueChannelTexture");
        _channelAlphaTextureObjectField = _root.Q<ObjectField>("OF_AlphaChannelTexture");
        _widthIntField = _root.Q<IntegerField>("IF_Width");
        _heightIntField = _root.Q<IntegerField>("IF_Height");
        _tgaToggle = _root.Q<Toggle>("TG_Tga");
        _pngToggle = _root.Q<Toggle>("TG_Png");
        _nameIdentifierTextField = _root.Q<TextField>("TF_NameIdentifier");
        _generatePackedTextureButton = _root.Q<Button>("BT_GeneratePackedTexture");
        _previewImageRedChannelVisualElement = _root.Q<VisualElement>("VE_PreviewImageRedChannel");
        _previewImageGreenChannelVisualElement = _root.Q<VisualElement>("VE_PreviewImageGreenChannel");
        _previewImageBlueChannelVisualElement = _root.Q<VisualElement>("VE_PreviewImageBlueChannel");
        _previewImageAlphaChannelVisualElement = _root.Q<VisualElement>("VE_PreviewImageAlphaChannel");
        _previewImageResultVisualElement = _root.Q<VisualElement>("VE_PreviewImageResult");

        // Set the object field inputs as Texture2D
        _channelRedTextureObjectField.objectType = typeof(Texture2D);
        _channelGreenTextureObjectField.objectType = typeof(Texture2D);
        _channelBlueTextureObjectField.objectType = typeof(Texture2D);
        _channelAlphaTextureObjectField.objectType = typeof(Texture2D);
        
        // Set the button
        _generatePackedTextureButton.clickable.clicked += () => GeneratePackedTexture();
    }

    private void GeneratePackedTexture()
    {
        var redChannelInput = _channelRedTextureObjectField.value;
        var greenChannelInput = _channelGreenTextureObjectField.value;
        var blueChannelInput = _channelBlueTextureObjectField.value;
        var alphaChannelInput = _channelAlphaTextureObjectField.value;
        var width = _widthIntField.value;
        var height = _heightIntField.value;

        var redChannelTexture = (Texture2D) redChannelInput;
        var greenChannelTexture = (Texture2D) greenChannelInput;
        var blueChannelTexture = (Texture2D) blueChannelInput;
        var alphaChannelTexture = (Texture2D) alphaChannelInput;

        // Create the composite texture
        var compositeTexture = TexturePacker.GetCompositeTextureRgb(
            _nameIdentifierTextField.text,
            redChannelTexture,
            greenChannelTexture,
            blueChannelTexture,
            alphaChannelTexture);

        // If inputs are valid, add their names to a list
        var validInputsNameList = new List<string>();
        if (redChannelTexture)
        {
            validInputsNameList.Add(redChannelTexture.name);
        }
        if (greenChannelTexture)
        {
            validInputsNameList.Add(greenChannelTexture.name);
        }
        if (blueChannelTexture)
        {
            validInputsNameList.Add(blueChannelTexture.name);
        }
        if (alphaChannelTexture)
        {
            validInputsNameList.Add(alphaChannelTexture.name);
        }
        
        // Find the common substring in the names and add the identifier
        var compositeTextureName = StringUtilities.GetCommonPrefix(validInputsNameList) + _nameIdentifierTextField.text;
        
        // Find the first valid input and create the path based on that 
        var listOfTextureInputs = new List<Texture2D>()
        {
            redChannelTexture,
            redChannelTexture,
            redChannelTexture,
            alphaChannelTexture
        };
        var relativeCompositeTexturePath =
            Path.Combine(
                Path.GetDirectoryName(AssetDatabase.GetAssetPath(
                    GetFirstValidTextureInput(listOfTextureInputs))), 
                compositeTextureName);

        // Save the composite texture
        var hasSaved = false;
        if (_tgaToggle.value)
        {
            var newRelativeCompositeTexturePath = relativeCompositeTexturePath + _tgaToggle.label;
            var absoluteCompositeTexturePath = Path.Combine(
                Directory.GetParent(Application.dataPath).FullName, 
                newRelativeCompositeTexturePath);
            TextureUtilities.SaveTextureToPath(
                compositeTexture,
                absoluteCompositeTexturePath,
                TextureUtilities.TextureUtilitiesFormats.Tga);
            hasSaved = true;
        }
        if (_pngToggle.value)
        {
            var newRelativeCompositeTexturePath = relativeCompositeTexturePath + _pngToggle.label;
            var absoluteCompositeTexturePath = Path.Combine(
                Directory.GetParent(Application.dataPath).FullName, 
                newRelativeCompositeTexturePath);
            TextureUtilities.SaveTextureToPath(
                compositeTexture,
                absoluteCompositeTexturePath,
                TextureUtilities.TextureUtilitiesFormats.Png);
            hasSaved = true;
        }

        // Update the previews for the provided images
        _previewImageRedChannelVisualElement.style.backgroundImage = redChannelTexture;
        _previewImageGreenChannelVisualElement.style.backgroundImage = greenChannelTexture;
        _previewImageBlueChannelVisualElement.style.backgroundImage = blueChannelTexture;
        _previewImageAlphaChannelVisualElement.style.backgroundImage = alphaChannelTexture;
        
        // If a new texture has been saved to disk, display it and select in the Project Window
        if (hasSaved)
        {
            AssetDatabase.Refresh();
            compositeTexture.Apply();
            _previewImageResultVisualElement.style.backgroundImage = compositeTexture;
            Selection.activeObject = AssetDatabase.LoadAssetAtPath(relativeCompositeTexturePath, typeof(Texture2D));
        }
    }

    private Texture2D GetFirstValidTextureInput(List<Texture2D> textureInputs)
    {
        var textureInputCount = textureInputs.Count;
        var firstValidInput = textureInputs[textureInputCount - 1];

        for (var i = 0; i < textureInputCount; i++)
        {
            if (textureInputs[i])
            {
                firstValidInput = textureInputs[i];
                return firstValidInput;
            }
        }
        
        return firstValidInput;
    }
}
