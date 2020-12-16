using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityTextureRgbPacker.Editor.UnityTextureRgbPackagerEditorWindowConstants;
using static UnityTextureRgbPacker.Editor.TexturePackerDataConstants;

namespace UnityTextureRgbPacker.Editor
{
    public class UnityTextureRgbPackerEditorWindow : EditorWindow
    {
        private VisualElement _root;
        private TexturePackerData _texturePackerData;

        [MenuItem("Raz's Tools/Texture RGB(A) Packer")]
        public static void ShowWindow()
        {
            var window = GetWindow<UnityTextureRgbPackerEditorWindow>();
            window.titleContent = new GUIContent(TexturePackerName);
            window.minSize = MinWindowSize;
        }

        private void OnEnable()
        {
            _root = rootVisualElement;

            #region INITIALIZATION AND QUERY
            // Process Type
            var processTypeVisualTreeAsset = Resources.Load<VisualTreeAsset>(ProcessTypeUxmlPath);
            processTypeVisualTreeAsset.CloneTree(_root);
            var processTypeEnumField = _root.Q<EnumField>(ProcessTypeEnumFieldName);
            
            // Single vs Batch
            var tabButtonsVisualTreeAsset = Resources.Load<VisualTreeAsset>(TabButtonsUxmlPath);
            tabButtonsVisualTreeAsset.CloneTree(_root);
            
            // Inputs
            var inputsVisualTreeAsset = Resources.Load<VisualTreeAsset>(InputsSingleUxmlPath);
            inputsVisualTreeAsset.CloneTree(_root);
            
            // Export
            var exportVisualTreeAsset = Resources.Load<VisualTreeAsset>(ExportUxmlPath);
            exportVisualTreeAsset.CloneTree(_root);

            // Preview
            var previewVisualTreeAsset = Resources.Load<VisualTreeAsset>(PreviewUxmlPath);
            previewVisualTreeAsset.CloneTree(_root);
            
            // Generate Button
            var generateButtonVisualTreeAsset = Resources.Load<VisualTreeAsset>(GenerateUxmlPath);
            generateButtonVisualTreeAsset.CloneTree(_root);

            #endregion

            #region BINDINGS
            // Setup
            _texturePackerData = ScriptableObject.CreateInstance<TexturePackerData>();
            var texPackSerObj = new UnityEditor.SerializedObject(_texturePackerData);
            
            // Process Type
            var processTypeProperty = texPackSerObj.FindProperty(ProcessTypePropName);
            if (processTypeProperty != null)
            {
                processTypeEnumField.BindProperty(processTypeProperty);
            }
            // processTypeEnumField.style.display = DisplayStyle.None;

            #endregion
        }

    }
}


