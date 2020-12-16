using System.Collections.Generic;
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
            
            // Pack - Single vs Batch
            var tabButtonsVisualTreeAsset = Resources.Load<VisualTreeAsset>(TabButtonsUxmlPath);
            tabButtonsVisualTreeAsset.CloneTree(_root);
            var tabPackButtonsVisualElement = _root.Q<VisualElement>(TabButtonsVisualElementName);
            tabPackButtonsVisualElement.name = TabButtonsPackVisualElementName;
            tabPackButtonsVisualElement = _root.Q<VisualElement>(TabButtonsPackVisualElementName);
            var tabPackSingleButton = _root.Q<Button>(TabButtonsSingleName);
            tabPackSingleButton.name = TabButtonsSinglePackName;
            tabPackSingleButton = _root.Q<Button>(TabButtonsSinglePackName);
            var tabPackBatchButton = _root.Q<Button>(TabButtonsBatchName);
            tabPackBatchButton.name = TabButtonsBatchPackName;
            tabPackBatchButton = _root.Q<Button>(TabButtonsBatchPackName);
            
            // Inputs
            var inputsVisualTreeAsset = Resources.Load<VisualTreeAsset>(InputsSingleUxmlPath);
            inputsVisualTreeAsset.CloneTree(_root);
            var inputsVisualElement = _root.Q<VisualElement>(InputsSingleVisualElementName);
            // Inputs - Red Channel
            var redChannelToggle = inputsVisualElement.Q<Toggle>(RedChannelToggleName);
            var redChannelVisualElement = inputsVisualElement.Q<VisualElement>(RedChannelVisualElementName);
            var redChannelObjectField = inputsVisualElement.Q<ObjectField>(RedChannelObjectFieldName);
            var redChannelPreviewVisualElement = inputsVisualElement.Q<ObjectField>(RedChannelPreviewVisualElementName);
            
            // Export
            var exportVisualTreeAsset = Resources.Load<VisualTreeAsset>(ExportUxmlPath);
            exportVisualTreeAsset.CloneTree(_root);
            var exportVisualElement = _root.Q<VisualElement>(ExportVisualElementName);
            exportVisualElement.name = ExportSingleVisualElementName;
            exportVisualElement = _root.Q<VisualElement>(ExportSingleVisualElementName);

            // Preview
            var previewVisualTreeAsset = Resources.Load<VisualTreeAsset>(PreviewUxmlPath);
            previewVisualTreeAsset.CloneTree(_root);
            var previewVisualElement = _root.Q<VisualElement>(PreviewVisualElement);
            
            // Single Pack - Generate Button
            var generateButtonVisualTreeAsset = Resources.Load<VisualTreeAsset>(GenerateUxmlPath);
            generateButtonVisualTreeAsset.CloneTree(_root);
            var generateSinglePackVisualElement = _root.Q<VisualElement>(GenerateVisualElementName);
            generateSinglePackVisualElement.name = GeneratePackSingleVisualElementName;
            generateSinglePackVisualElement = _root.Q<VisualElement>(GeneratePackSingleVisualElementName);
            var generatePackSingleButton = _root.Q<Button>(GenerateButtonName);
            generatePackSingleButton.name = GeneratePackSingleButtonName;
            generatePackSingleButton = _root.Q<Button>(GeneratePackSingleButtonName);
            generatePackSingleButton.text = GeneratePackSingleButtonLabel;
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

            // Pack - Single vs Batch
            var packIsSingleProperty = texPackSerObj.FindProperty(IsPackSinglePropName);
            var packIsBatchProperty = texPackSerObj.FindProperty(IsPackBatchPropName);
            #endregion

            #region BEHAVIOR
            // Process Type
            processTypeEnumField.RegisterValueChangedCallback(evt =>
            {
                switch (_texturePackerData.ProcessType)
                {
                    case TexturePackerData.ProcessTypeCategories.Pack:
                    {
                        DisableDisplayContents(new List<VisualElement>()
                        {
                            
                        });
                        EnableDisplayContents(new List<VisualElement>()
                        {
                            tabPackButtonsVisualElement
                        });
                    }
                        break;
                }
            });
            
            // Pack - Single vs Batch
            SetSelectedStyle(new List<VisualElement>() {tabPackSingleButton});
            SetUnselectedStyle(new List<VisualElement>() {tabPackBatchButton});
            tabPackSingleButton.clickable.clicked += () =>
            {
                // Set States
                _texturePackerData.IsPackSingle = true;
                _texturePackerData.IsPackBatch = false;
                
                // Change Style
                SetSelectedStyle(new List<VisualElement>() {tabPackSingleButton});
                SetUnselectedStyle(new List<VisualElement>() {tabPackBatchButton});
                
                // Toggle Content
                DisableDisplayContents(new List<VisualElement>()
                {
                    
                });
                EnableDisplayContents(new List<VisualElement>()
                {
                    // Pack > Single
                    inputsVisualElement,
                    exportVisualElement,
                    previewVisualElement,
                    generateSinglePackVisualElement
                });
            };
            tabPackSingleButton.RegisterCallback<MouseEnterEvent>(evt =>
            {
                (evt.target as VisualElement).style.backgroundColor = HoverTabColor;
            });
            tabPackSingleButton.RegisterCallback<MouseLeaveEvent>(evt =>
            {
                (evt.target as VisualElement).style.backgroundColor =
                    _texturePackerData.IsPackSingle ? SelectedTabColor : UnselectedTabColor;
            });
            
            tabPackBatchButton.clickable.clicked += () =>
            {
                // Set States
                _texturePackerData.IsPackSingle = false;
                _texturePackerData.IsPackBatch = true;
                
                // Change Style
                SetSelectedStyle(new List<VisualElement>() {tabPackBatchButton});
                SetUnselectedStyle(new List<VisualElement>() {tabPackSingleButton});
                
                // Toggle Content
                DisableDisplayContents(new List<VisualElement>()
                {
                    // Pack > Single
                    inputsVisualElement,
                    exportVisualElement,
                    previewVisualElement,
                    generateSinglePackVisualElement
                });
                EnableDisplayContents(new List<VisualElement>()
                {
                    
                });
            };
            tabPackBatchButton.RegisterCallback<MouseEnterEvent>(evt =>
            {
                (evt.target as VisualElement).style.backgroundColor = HoverTabColor;
            });
            tabPackBatchButton.RegisterCallback<MouseEnterEvent>(evt =>
            {
                (evt.target as VisualElement).style.backgroundColor = HoverTabColor;
            });
            tabPackBatchButton.RegisterCallback<MouseLeaveEvent>(evt =>
            {
                (evt.target as VisualElement).style.backgroundColor =
                    _texturePackerData.IsPackBatch ? SelectedTabColor : UnselectedTabColor;
            });
            
            // Inputs - Red Channel
            redChannelVisualElement.SetEnabled(redChannelToggle.value);
            redChannelToggle.RegisterValueChangedCallback(evt =>
            {
                redChannelVisualElement.SetEnabled(redChannelToggle.value);
            });
            redChannelObjectField.objectType = typeof(Texture2D);
            #endregion
        }

        private void SetSelectedStyle(List<VisualElement> visualElements)
        {
            foreach (var ve in visualElements)
            {
                ve.style.backgroundColor = SelectedTabColor;
            }
        }

        private void SetUnselectedStyle(List<VisualElement> visualElements)
        {
            foreach (var ve in visualElements)
            {
                ve.style.backgroundColor = UnselectedTabColor;
            }
        }

        private void DisableDisplayContents(List<VisualElement> visualElements)
        {
            foreach (var ve in visualElements)
            {
                ve.style.display = DisplayStyle.None;
            }
        }

        private void EnableDisplayContents(List<VisualElement> visualElements)
        {
            foreach (var ve in visualElements)
            {
                ve.style.display = DisplayStyle.Flex;
            }
        }
    }
}


