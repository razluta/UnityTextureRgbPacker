﻿using System.Collections.Generic;
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
            var redChannelPreviewVisualElement = inputsVisualElement.Q<VisualElement>(RedChannelPreviewVisualElementName);
            var redChannelAdvancedOptionsFoldout = inputsVisualElement.Q<Foldout>(RedChannelAdvancedOptionsFoldoutName);
            var redChannelUseAllChannelsToggle = inputsVisualElement.Q<Toggle>(RedChannelUseAllChannelsToggleName);
            var redChannelUseRedChannelToggle = inputsVisualElement.Q<Toggle>(RedChannelUseRedChannelToggleName);
            var redChannelUseGreenChannelToggle = inputsVisualElement.Q<Toggle>(RedChannelUseGreenChannelToggleName);
            var redChannelUseBlueChannelToggle = inputsVisualElement.Q<Toggle>(RedChannelUseBlueChannelToggleName);
            var redChannelUseAlphaChannelToggle = inputsVisualElement.Q<Toggle>(RedChannelUseAlphaChannelToggleName);
            var redChannelDimensionsLabel = inputsVisualElement.Q<Label>(RedChannelDimensionsLabelName);
            // Inputs - Green Channel
            var greenChannelToggle = inputsVisualElement.Q<Toggle>(GreenChannelToggleName);
            var greenChannelVisualElement = inputsVisualElement.Q<VisualElement>(GreenChannelVisualElementName);
            var greenChannelObjectField = inputsVisualElement.Q<ObjectField>(GreenChannelObjectFieldName);
            var greenChannelPreviewVisualElement = inputsVisualElement.Q<VisualElement>(GreenChannelPreviewVisualElementName);
            var greenChannelAdvancedOptionsFoldout = inputsVisualElement.Q<Foldout>(GreenChannelAdvancedOptionsFoldoutName);
            var greenChannelUseAllChannelsToggle = inputsVisualElement.Q<Toggle>(GreenChannelUseAllChannelsToggleName);
            var greenChannelUseRedChannelToggle = inputsVisualElement.Q<Toggle>(GreenChannelUseRedChannelToggleName);
            var greenChannelUseGreenChannelToggle = inputsVisualElement.Q<Toggle>(GreenChannelUseGreenChannelToggleName);
            var greenChannelUseBlueChannelToggle = inputsVisualElement.Q<Toggle>(GreenChannelUseBlueChannelToggleName);
            var greenChannelUseAlphaChannelToggle = inputsVisualElement.Q<Toggle>(GreenChannelUseAlphaChannelToggleName);
            var greenChannelDimensionsLabel = inputsVisualElement.Q<Label>(GreenChannelDimensionsLabelName);
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
            redChannelObjectField.RegisterValueChangedCallback(evt =>
            {
                if (redChannelObjectField.value == null)
                {
                    redChannelDimensionsLabel.text = "";
                    return;
                }
                
                var texture = (Texture2D) redChannelObjectField.value;
                if (texture == null)
                {
                    redChannelDimensionsLabel.text = "";
                    return;
                }
                redChannelPreviewVisualElement.style.backgroundImage = texture;
                redChannelDimensionsLabel.text = texture.width.ToString() + PixelDimensionsX + texture.height.ToString();
            });
            
            redChannelAdvancedOptionsFoldout.value = false;
            
            redChannelUseAllChannelsToggle.RegisterValueChangedCallback(evt =>
            {
                if (!redChannelUseAllChannelsToggle.value)
                {
                    return;
                }
                redChannelUseRedChannelToggle.value = false; 
                redChannelUseGreenChannelToggle.value = false; 
                redChannelUseBlueChannelToggle.value = false; 
                redChannelUseAlphaChannelToggle.value = false;
            });
            
            redChannelUseRedChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!redChannelUseRedChannelToggle.value)
                {
                    return;
                }
                redChannelUseAllChannelsToggle.value = false; 
                redChannelUseGreenChannelToggle.value = false; 
                redChannelUseBlueChannelToggle.value = false; 
                redChannelUseAlphaChannelToggle.value = false;
            });
            
            redChannelUseGreenChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!redChannelUseGreenChannelToggle.value)
                {
                    return;
                }
                redChannelUseAllChannelsToggle.value = false; 
                redChannelUseRedChannelToggle.value = false; 
                redChannelUseBlueChannelToggle.value = false; 
                redChannelUseAlphaChannelToggle.value = false;
            });
            
            redChannelUseBlueChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!redChannelUseBlueChannelToggle.value)
                {
                    return;
                }
                redChannelUseAllChannelsToggle.value = false; 
                redChannelUseRedChannelToggle.value = false; 
                redChannelUseGreenChannelToggle.value = false; 
                redChannelUseAlphaChannelToggle.value = false;
            });
            
            redChannelUseAlphaChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!redChannelUseAlphaChannelToggle.value)
                {
                    return;
                }
                redChannelUseAllChannelsToggle.value = false; 
                redChannelUseRedChannelToggle.value = false; 
                redChannelUseGreenChannelToggle.value = false; 
                redChannelUseBlueChannelToggle.value = false;
            });
            
            // Inputs - Green Channel
            greenChannelVisualElement.SetEnabled(greenChannelToggle.value);
            greenChannelToggle.RegisterValueChangedCallback(evt =>
            {
                greenChannelVisualElement.SetEnabled(greenChannelToggle.value);
            });
            
            greenChannelObjectField.objectType = typeof(Texture2D);
            greenChannelObjectField.RegisterValueChangedCallback(evt =>
            {
                if (greenChannelObjectField.value == null)
                {
                    greenChannelDimensionsLabel.text = "";
                    return;
                }
                
                var texture = (Texture2D) greenChannelObjectField.value;
                if (texture == null)
                {
                    greenChannelDimensionsLabel.text = "";
                    return;
                }
                greenChannelPreviewVisualElement.style.backgroundImage = texture;
                greenChannelDimensionsLabel.text = texture.width.ToString() + PixelDimensionsX + texture.height.ToString();
            });
            
            greenChannelAdvancedOptionsFoldout.value = false;
            
            greenChannelUseAllChannelsToggle.RegisterValueChangedCallback(evt =>
            {
                if (!greenChannelUseAllChannelsToggle.value)
                {
                    return;
                }
                greenChannelUseRedChannelToggle.value = false; 
                greenChannelUseGreenChannelToggle.value = false; 
                greenChannelUseBlueChannelToggle.value = false; 
                greenChannelUseAlphaChannelToggle.value = false;
            });
            
            greenChannelUseRedChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!greenChannelUseRedChannelToggle.value)
                {
                    return;
                }
                greenChannelUseAllChannelsToggle.value = false; 
                greenChannelUseGreenChannelToggle.value = false; 
                greenChannelUseBlueChannelToggle.value = false; 
                greenChannelUseAlphaChannelToggle.value = false;
            });
            
            greenChannelUseGreenChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!greenChannelUseGreenChannelToggle.value)
                {
                    return;
                }
                greenChannelUseAllChannelsToggle.value = false; 
                greenChannelUseRedChannelToggle.value = false; 
                greenChannelUseBlueChannelToggle.value = false; 
                greenChannelUseAlphaChannelToggle.value = false;
            });
            
            greenChannelUseBlueChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!greenChannelUseBlueChannelToggle.value)
                {
                    return;
                }
                greenChannelUseAllChannelsToggle.value = false; 
                greenChannelUseRedChannelToggle.value = false; 
                greenChannelUseGreenChannelToggle.value = false; 
                greenChannelUseAlphaChannelToggle.value = false;
            });
            
            greenChannelUseAlphaChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!greenChannelUseAlphaChannelToggle.value)
                {
                    return;
                }
                greenChannelUseAllChannelsToggle.value = false; 
                greenChannelUseRedChannelToggle.value = false; 
                greenChannelUseGreenChannelToggle.value = false; 
                greenChannelUseBlueChannelToggle.value = false;
            });
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


