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
            var redChannelPreviewVisualElement =
                inputsVisualElement.Q<VisualElement>(RedChannelPreviewVisualElementName);
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
            var greenChannelPreviewVisualElement =
                inputsVisualElement.Q<VisualElement>(GreenChannelPreviewVisualElementName);
            var greenChannelAdvancedOptionsFoldout =
                inputsVisualElement.Q<Foldout>(GreenChannelAdvancedOptionsFoldoutName);
            var greenChannelUseAllChannelsToggle = inputsVisualElement.Q<Toggle>(GreenChannelUseAllChannelsToggleName);
            var greenChannelUseRedChannelToggle = inputsVisualElement.Q<Toggle>(GreenChannelUseRedChannelToggleName);
            var greenChannelUseGreenChannelToggle =
                inputsVisualElement.Q<Toggle>(GreenChannelUseGreenChannelToggleName);
            var greenChannelUseBlueChannelToggle = inputsVisualElement.Q<Toggle>(GreenChannelUseBlueChannelToggleName);
            var greenChannelUseAlphaChannelToggle =
                inputsVisualElement.Q<Toggle>(GreenChannelUseAlphaChannelToggleName);
            var greenChannelDimensionsLabel = inputsVisualElement.Q<Label>(GreenChannelDimensionsLabelName);
            // Inputs - Blue Channel
            var blueChannelToggle = inputsVisualElement.Q<Toggle>(BlueChannelToggleName);
            var blueChannelVisualElement = inputsVisualElement.Q<VisualElement>(BlueChannelVisualElementName);
            var blueChannelObjectField = inputsVisualElement.Q<ObjectField>(BlueChannelObjectFieldName);
            var blueChannelPreviewVisualElement =
                inputsVisualElement.Q<VisualElement>(BlueChannelPreviewVisualElementName);
            var blueChannelAdvancedOptionsFoldout =
                inputsVisualElement.Q<Foldout>(BlueChannelAdvancedOptionsFoldoutName);
            var blueChannelUseAllChannelsToggle = inputsVisualElement.Q<Toggle>(BlueChannelUseAllChannelsToggleName);
            var blueChannelUseRedChannelToggle = inputsVisualElement.Q<Toggle>(BlueChannelUseRedChannelToggleName);
            var blueChannelUseGreenChannelToggle = inputsVisualElement.Q<Toggle>(BlueChannelUseGreenChannelToggleName);
            var blueChannelUseBlueChannelToggle = inputsVisualElement.Q<Toggle>(BlueChannelUseBlueChannelToggleName);
            var blueChannelUseAlphaChannelToggle = inputsVisualElement.Q<Toggle>(BlueChannelUseAlphaChannelToggleName);
            var blueChannelDimensionsLabel = inputsVisualElement.Q<Label>(BlueChannelDimensionsLabelName);
            // Inputs - Alpha Channel
            var alphaChannelToggle = inputsVisualElement.Q<Toggle>(AlphaChannelToggleName);
            var alphaChannelVisualElement = inputsVisualElement.Q<VisualElement>(AlphaChannelVisualElementName);
            var alphaChannelObjectField = inputsVisualElement.Q<ObjectField>(AlphaChannelObjectFieldName);
            var alphaChannelPreviewVisualElement =
                inputsVisualElement.Q<VisualElement>(AlphaChannelPreviewVisualElementName);
            var alphaChannelAdvancedOptionsFoldout =
                inputsVisualElement.Q<Foldout>(AlphaChannelAdvancedOptionsFoldoutName);
            var alphaChannelUseAllChannelsToggle = inputsVisualElement.Q<Toggle>(AlphaChannelUseAllChannelsToggleName);
            var alphaChannelUseRedChannelToggle = inputsVisualElement.Q<Toggle>(AlphaChannelUseRedChannelToggleName);
            var alphaChannelUseGreenChannelToggle =
                inputsVisualElement.Q<Toggle>(AlphaChannelUseGreenChannelToggleName);
            var alphaChannelUseBlueChannelToggle = inputsVisualElement.Q<Toggle>(AlphaChannelUseBlueChannelToggleName);
            var alphaChannelUseAlphaChannelToggle =
                inputsVisualElement.Q<Toggle>(AlphaChannelUseAlphaChannelToggleName);
            var alphaChannelDimensionsLabel = inputsVisualElement.Q<Label>(AlphaChannelDimensionsLabelName);
            // Clear 
            var clearButton = inputsVisualElement.Q<Button>(ClearButtonName);

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

            // Red Channel Texture Input
            var isRedChannelActiveProperty = texPackSerObj.FindProperty(IsRedChannelActivePropName);
            if (isRedChannelActiveProperty != null)
            {
                redChannelToggle.BindProperty(isRedChannelActiveProperty);
            }

            var redChannelTextureProperty = texPackSerObj.FindProperty(RedChannelTexturePropName);
            if (redChannelTextureProperty != null)
            {
                redChannelObjectField.BindProperty(redChannelTextureProperty);
            }

            var redChannelTextureUseAllChannelsProperty =
                texPackSerObj.FindProperty(IsRedChannelTextureUseAllChannelsPropName);
            if (redChannelTextureUseAllChannelsProperty != null)
            {
                redChannelUseAllChannelsToggle.BindProperty(redChannelTextureUseAllChannelsProperty);
            }

            var redChannelTextureUseRedChannelProperty =
                texPackSerObj.FindProperty(IsRedChannelTextureUseRedChannelPropName);
            if (redChannelTextureUseRedChannelProperty != null)
            {
                redChannelUseRedChannelToggle.BindProperty(redChannelTextureUseRedChannelProperty);
            }
            
            var redChannelTextureUseGreenChannelProperty =
                texPackSerObj.FindProperty(IsRedChannelTextureUseGreenChannelPropName);
            if (redChannelTextureUseGreenChannelProperty != null)
            {
                redChannelUseGreenChannelToggle.BindProperty(redChannelTextureUseGreenChannelProperty);
            }
            
            var redChannelTextureUseBlueChannelProperty =
                texPackSerObj.FindProperty(IsRedChannelTextureUseBlueChannelPropName);
            if (redChannelTextureUseBlueChannelProperty != null)
            {
                redChannelUseBlueChannelToggle.BindProperty(redChannelTextureUseBlueChannelProperty);
            }
            
            var redChannelTextureUseAlphaChannelProperty =
                texPackSerObj.FindProperty(IsRedChannelTextureUseAlphaChannelPropName);
            if (redChannelTextureUseAlphaChannelProperty != null)
            {
                redChannelUseAlphaChannelToggle.BindProperty(redChannelTextureUseAlphaChannelProperty);
            }
            
            // Green Channel Texture Input
            var isGreenChannelActiveProperty = texPackSerObj.FindProperty(IsGreenChannelActivePropName);
            if (isGreenChannelActiveProperty != null)
            {
                greenChannelToggle.BindProperty(isGreenChannelActiveProperty);
            }

            var greenChannelTextureProperty = texPackSerObj.FindProperty(GreenChannelTexturePropName);
            if (greenChannelTextureProperty != null)
            {
                greenChannelObjectField.BindProperty(greenChannelTextureProperty);
            }

            var greenChannelTextureUseAllChannelsProperty =
                texPackSerObj.FindProperty(IsGreenChannelTextureUseAllChannelsPropName);
            if (greenChannelTextureUseAllChannelsProperty != null)
            {
                greenChannelUseAllChannelsToggle.BindProperty(greenChannelTextureUseAllChannelsProperty);
            }

            var greenChannelTextureUseRedChannelProperty =
                texPackSerObj.FindProperty(IsGreenChannelTextureUseRedChannelPropName);
            if (greenChannelTextureUseRedChannelProperty != null)
            {
                greenChannelUseRedChannelToggle.BindProperty(greenChannelTextureUseRedChannelProperty);
            }
            
            var greenChannelTextureUseGreenChannelProperty =
                texPackSerObj.FindProperty(IsGreenChannelTextureUseGreenChannelPropName);
            if (greenChannelTextureUseGreenChannelProperty != null)
            {
                greenChannelUseGreenChannelToggle.BindProperty(greenChannelTextureUseGreenChannelProperty);
            }
            
            var greenChannelTextureUseBlueChannelProperty =
                texPackSerObj.FindProperty(IsGreenChannelTextureUseBlueChannelPropName);
            if (greenChannelTextureUseBlueChannelProperty != null)
            {
                greenChannelUseBlueChannelToggle.BindProperty(greenChannelTextureUseBlueChannelProperty);
            }
            
            var greenChannelTextureUseAlphaChannelProperty =
                texPackSerObj.FindProperty(IsGreenChannelTextureUseAlphaChannelPropName);
            if (greenChannelTextureUseAlphaChannelProperty != null)
            {
                greenChannelUseAlphaChannelToggle.BindProperty(greenChannelTextureUseAlphaChannelProperty);
            }
            
            // Blue Channel Texture Input
            var isBlueChannelActiveProperty = texPackSerObj.FindProperty(IsBlueChannelActivePropName);
            if (isBlueChannelActiveProperty != null)
            {
                blueChannelToggle.BindProperty(isBlueChannelActiveProperty);
            }

            var blueChannelTextureProperty = texPackSerObj.FindProperty(BlueChannelTexturePropName);
            if (blueChannelTextureProperty != null)
            {
                blueChannelObjectField.BindProperty(blueChannelTextureProperty);
            }

            var blueChannelTextureUseAllChannelsProperty =
                texPackSerObj.FindProperty(IsBlueChannelTextureUseAllChannelsPropName);
            if (blueChannelTextureUseAllChannelsProperty != null)
            {
                blueChannelUseAllChannelsToggle.BindProperty(blueChannelTextureUseAllChannelsProperty);
            }

            var blueChannelTextureUseRedChannelProperty =
                texPackSerObj.FindProperty(IsBlueChannelTextureUseRedChannelPropName);
            if (blueChannelTextureUseRedChannelProperty != null)
            {
                blueChannelUseRedChannelToggle.BindProperty(blueChannelTextureUseRedChannelProperty);
            }
            
            var blueChannelTextureUseGreenChannelProperty =
                texPackSerObj.FindProperty(IsBlueChannelTextureUseGreenChannelPropName);
            if (blueChannelTextureUseGreenChannelProperty != null)
            {
                blueChannelUseGreenChannelToggle.BindProperty(blueChannelTextureUseGreenChannelProperty);
            }
            
            var blueChannelTextureUseBlueChannelProperty =
                texPackSerObj.FindProperty(IsBlueChannelTextureUseBlueChannelPropName);
            if (blueChannelTextureUseBlueChannelProperty != null)
            {
                blueChannelUseBlueChannelToggle.BindProperty(blueChannelTextureUseBlueChannelProperty);
            }
            
            var blueChannelTextureUseAlphaChannelProperty =
                texPackSerObj.FindProperty(IsBlueChannelTextureUseAlphaChannelPropName);
            if (blueChannelTextureUseAlphaChannelProperty != null)
            {
                blueChannelUseAlphaChannelToggle.BindProperty(blueChannelTextureUseAlphaChannelProperty);
            }

            // Alpha Channel Texture Input
            var isAlphaChannelActiveProperty = texPackSerObj.FindProperty(IsAlphaChannelActivePropName);
            if (isAlphaChannelActiveProperty != null)
            {
                alphaChannelToggle.BindProperty(isAlphaChannelActiveProperty);
            }

            var alphaChannelTextureProperty = texPackSerObj.FindProperty(AlphaChannelTexturePropName);
            if (alphaChannelTextureProperty != null)
            {
                alphaChannelObjectField.BindProperty(alphaChannelTextureProperty);
            }

            var alphaChannelTextureUseAllChannelsProperty =
                texPackSerObj.FindProperty(IsAlphaChannelTextureUseAllChannelsPropName);
            if (alphaChannelTextureUseAllChannelsProperty != null)
            {
                alphaChannelUseAllChannelsToggle.BindProperty(alphaChannelTextureUseAllChannelsProperty);
            }

            var alphaChannelTextureUseRedChannelProperty =
                texPackSerObj.FindProperty(IsAlphaChannelTextureUseRedChannelPropName);
            if (alphaChannelTextureUseRedChannelProperty != null)
            {
                alphaChannelUseRedChannelToggle.BindProperty(alphaChannelTextureUseRedChannelProperty);
            }
            
            var alphaChannelTextureUseGreenChannelProperty =
                texPackSerObj.FindProperty(IsAlphaChannelTextureUseGreenChannelPropName);
            if (alphaChannelTextureUseGreenChannelProperty != null)
            {
                alphaChannelUseGreenChannelToggle.BindProperty(alphaChannelTextureUseGreenChannelProperty);
            }
            
            var alphaChannelTextureUseBlueChannelProperty =
                texPackSerObj.FindProperty(IsAlphaChannelTextureUseBlueChannelPropName);
            if (alphaChannelTextureUseBlueChannelProperty != null)
            {
                alphaChannelUseBlueChannelToggle.BindProperty(alphaChannelTextureUseBlueChannelProperty);
            }
            
            var alphaChannelTextureUseAlphaChannelProperty =
                texPackSerObj.FindProperty(IsAlphaChannelTextureUseAlphaChannelPropName);
            if (alphaChannelTextureUseAlphaChannelProperty != null)
            {
                alphaChannelUseAlphaChannelToggle.BindProperty(alphaChannelTextureUseAlphaChannelProperty);
            }
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
                    redChannelPreviewVisualElement.style.backgroundImage = null;
                    redChannelAdvancedOptionsFoldout.value = false;
                    return;
                }

                var texture = (Texture2D) redChannelObjectField.value;
                if (texture == null)
                {
                    redChannelDimensionsLabel.text = "";
                    redChannelPreviewVisualElement.style.backgroundImage = null;
                    redChannelAdvancedOptionsFoldout.value = false;
                    return;
                }

                redChannelPreviewVisualElement.style.backgroundImage = texture;
                redChannelDimensionsLabel.text =
                    texture.width.ToString() + PixelDimensionsX + texture.height.ToString();
            });
            
            redChannelAdvancedOptionsFoldout.value = false;
            redChannelAdvancedOptionsFoldout.RegisterValueChangedCallback(evt =>
            {
                if (redChannelAdvancedOptionsFoldout.value == true)
                {
                    CollapseFoldouts(new List<Foldout>()
                    {
                        greenChannelAdvancedOptionsFoldout,
                        blueChannelAdvancedOptionsFoldout,
                        alphaChannelAdvancedOptionsFoldout
                    });
                }
            });
            
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
                    greenChannelPreviewVisualElement.style.backgroundImage = null;
                    return;
                }
                
                var texture = (Texture2D) greenChannelObjectField.value;
                if (texture == null)
                {
                    greenChannelDimensionsLabel.text = "";
                    greenChannelPreviewVisualElement.style.backgroundImage = null;
                    return;
                }
                greenChannelPreviewVisualElement.style.backgroundImage = texture;
                greenChannelDimensionsLabel.text = texture.width.ToString() + PixelDimensionsX + texture.height.ToString();
            });
            
            greenChannelAdvancedOptionsFoldout.value = false;
            greenChannelAdvancedOptionsFoldout.RegisterValueChangedCallback(evt =>
            {
                if (greenChannelAdvancedOptionsFoldout.value == true)
                {
                    CollapseFoldouts(new List<Foldout>()
                    {
                        redChannelAdvancedOptionsFoldout,
                        blueChannelAdvancedOptionsFoldout,
                        alphaChannelAdvancedOptionsFoldout
                    });
                }
            });
            
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
            
            // Inputs - Blue Channel
            blueChannelVisualElement.SetEnabled(blueChannelToggle.value);
            blueChannelToggle.RegisterValueChangedCallback(evt =>
            {
                blueChannelVisualElement.SetEnabled(blueChannelToggle.value);
            });
            
            blueChannelObjectField.objectType = typeof(Texture2D);
            blueChannelObjectField.RegisterValueChangedCallback(evt =>
            {
                if (blueChannelObjectField.value == null)
                {
                    blueChannelDimensionsLabel.text = "";
                    blueChannelPreviewVisualElement.style.backgroundImage = null;
                    return;
                }
                
                var texture = (Texture2D) blueChannelObjectField.value;
                if (texture == null)
                {
                    blueChannelDimensionsLabel.text = "";
                    blueChannelPreviewVisualElement.style.backgroundImage = null;
                    return;
                }
                blueChannelPreviewVisualElement.style.backgroundImage = texture;
                blueChannelDimensionsLabel.text = texture.width.ToString() + PixelDimensionsX + texture.height.ToString();
            });
            
            blueChannelAdvancedOptionsFoldout.value = false;
            blueChannelAdvancedOptionsFoldout.RegisterValueChangedCallback(evt =>
            {
                if (blueChannelAdvancedOptionsFoldout.value == true)
                {
                    CollapseFoldouts(new List<Foldout>()
                    {
                        redChannelAdvancedOptionsFoldout,
                        greenChannelAdvancedOptionsFoldout,
                        alphaChannelAdvancedOptionsFoldout
                    });
                }
            });
            
            blueChannelUseAllChannelsToggle.RegisterValueChangedCallback(evt =>
            {
                if (!blueChannelUseAllChannelsToggle.value)
                {
                    return;
                }
                blueChannelUseRedChannelToggle.value = false; 
                blueChannelUseGreenChannelToggle.value = false; 
                blueChannelUseBlueChannelToggle.value = false; 
                blueChannelUseAlphaChannelToggle.value = false;
            });
            
            blueChannelUseRedChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!blueChannelUseRedChannelToggle.value)
                {
                    return;
                }
                blueChannelUseAllChannelsToggle.value = false; 
                blueChannelUseGreenChannelToggle.value = false; 
                blueChannelUseBlueChannelToggle.value = false; 
                blueChannelUseAlphaChannelToggle.value = false;
            });
            
            blueChannelUseGreenChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!blueChannelUseGreenChannelToggle.value)
                {
                    return;
                }
                blueChannelUseAllChannelsToggle.value = false; 
                blueChannelUseRedChannelToggle.value = false; 
                blueChannelUseBlueChannelToggle.value = false; 
                blueChannelUseAlphaChannelToggle.value = false;
            });
            
            blueChannelUseBlueChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!blueChannelUseBlueChannelToggle.value)
                {
                    return;
                }
                blueChannelUseAllChannelsToggle.value = false; 
                blueChannelUseRedChannelToggle.value = false; 
                blueChannelUseGreenChannelToggle.value = false; 
                blueChannelUseAlphaChannelToggle.value = false;
            });
            
            blueChannelUseAlphaChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!blueChannelUseAlphaChannelToggle.value)
                {
                    return;
                }
                blueChannelUseAllChannelsToggle.value = false; 
                blueChannelUseRedChannelToggle.value = false; 
                blueChannelUseGreenChannelToggle.value = false; 
                blueChannelUseBlueChannelToggle.value = false;
            });
            
            // Inputs - Alpha Channel
            alphaChannelVisualElement.SetEnabled(alphaChannelToggle.value);
            alphaChannelToggle.RegisterValueChangedCallback(evt =>
            {
                alphaChannelVisualElement.SetEnabled(alphaChannelToggle.value);
            });
            
            alphaChannelObjectField.objectType = typeof(Texture2D);
            alphaChannelObjectField.RegisterValueChangedCallback(evt =>
            {
                if (alphaChannelObjectField.value == null)
                {
                    alphaChannelDimensionsLabel.text = "";
                    alphaChannelPreviewVisualElement.style.backgroundImage = null;
                    return;
                }
                
                var texture = (Texture2D) alphaChannelObjectField.value;
                if (texture == null)
                {
                    alphaChannelDimensionsLabel.text = "";
                    alphaChannelPreviewVisualElement.style.backgroundImage = null;
                    return;
                }
                alphaChannelPreviewVisualElement.style.backgroundImage = texture;
                alphaChannelDimensionsLabel.text = texture.width.ToString() + PixelDimensionsX + texture.height.ToString();
            });
            
            alphaChannelAdvancedOptionsFoldout.value = false;
            alphaChannelAdvancedOptionsFoldout.RegisterValueChangedCallback(evt =>
            {
                if (alphaChannelAdvancedOptionsFoldout.value == true)
                {
                    CollapseFoldouts(new List<Foldout>()
                    {
                        redChannelAdvancedOptionsFoldout,
                        greenChannelAdvancedOptionsFoldout,
                        blueChannelAdvancedOptionsFoldout
                    });
                }
            });
            
            alphaChannelUseAllChannelsToggle.RegisterValueChangedCallback(evt =>
            {
                if (!alphaChannelUseAllChannelsToggle.value)
                {
                    return;
                }
                alphaChannelUseRedChannelToggle.value = false; 
                alphaChannelUseGreenChannelToggle.value = false; 
                alphaChannelUseBlueChannelToggle.value = false; 
                alphaChannelUseAlphaChannelToggle.value = false;
            });
            
            alphaChannelUseRedChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!alphaChannelUseRedChannelToggle.value)
                {
                    return;
                }
                alphaChannelUseAllChannelsToggle.value = false; 
                alphaChannelUseGreenChannelToggle.value = false; 
                alphaChannelUseBlueChannelToggle.value = false; 
                alphaChannelUseAlphaChannelToggle.value = false;
            });
            
            alphaChannelUseGreenChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!alphaChannelUseGreenChannelToggle.value)
                {
                    return;
                }
                alphaChannelUseAllChannelsToggle.value = false; 
                alphaChannelUseRedChannelToggle.value = false; 
                alphaChannelUseBlueChannelToggle.value = false; 
                alphaChannelUseAlphaChannelToggle.value = false;
            });
            
            alphaChannelUseBlueChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!blueChannelUseBlueChannelToggle.value)
                {
                    return;
                }
                alphaChannelUseAllChannelsToggle.value = false; 
                alphaChannelUseRedChannelToggle.value = false; 
                alphaChannelUseGreenChannelToggle.value = false; 
                alphaChannelUseAlphaChannelToggle.value = false;
            });
            
            alphaChannelUseAlphaChannelToggle.RegisterValueChangedCallback(evt =>
            {
                if (!alphaChannelUseAlphaChannelToggle.value)
                {
                    return;
                }
                alphaChannelUseAllChannelsToggle.value = false; 
                alphaChannelUseRedChannelToggle.value = false; 
                alphaChannelUseGreenChannelToggle.value = false; 
                alphaChannelUseBlueChannelToggle.value = false;
            });
            
            // Clear 
            clearButton.clickable.clicked += () =>
            {
                // Clear Data
                _texturePackerData.ClearInputs();
                // Collapse Options
                CollapseFoldouts(new List<Foldout>()
                {
                    redChannelAdvancedOptionsFoldout,
                    greenChannelAdvancedOptionsFoldout,
                    blueChannelAdvancedOptionsFoldout,
                    alphaChannelAdvancedOptionsFoldout
                });
            };
            
            // Export - Type
            // ...
            
            // Export - Type
            // ...
            
            // Pack
            generatePackSingleButton.clickable.clicked += () =>
            {
                // Pack
                
                
                // Collapse Options
                CollapseFoldouts(new List<Foldout>()
                {
                    redChannelAdvancedOptionsFoldout,
                    greenChannelAdvancedOptionsFoldout,
                    blueChannelAdvancedOptionsFoldout,
                    alphaChannelAdvancedOptionsFoldout
                });
            };
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

        private void CollapseFoldouts(List<Foldout> foldouts)
        {
            foreach (var f in foldouts)
            {
                f.value = false;
            }
        }
    }
}


