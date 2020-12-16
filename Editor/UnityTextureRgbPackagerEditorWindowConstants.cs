using UnityEngine;

namespace UnityTextureRgbPacker.Editor
{
    public static class UnityTextureRgbPackagerEditorWindowConstants
    {
        public const string TexturePackerName = "Texture RGB(A) Packer";
        public static readonly Vector2 MinWindowSize = new Vector2(387, 1000);
        public const string PixelDimensionsX = "x";
        
        // Process Type
        public const string ProcessTypeUxmlPath = "CS_Process";
        public const string ProcessTypeEnumFieldName = "EF_ProcessType";
        // Tabs
        public const string TabButtonsUxmlPath = "CS_TabButtons";
        public const string TabButtonsVisualElementName = "VE_SingleBatchButtons";
        public const string TabButtonsPackVisualElementName = "VE_PackSingleBatchButtons";
        public const string TabButtonsSingleName = "BT_Single";
        public const string TabButtonsSinglePackName = "BT_SinglePack";
        public const string TabButtonsBatchName = "BT_Batch";
        public const string TabButtonsBatchPackName = "BT_BatchPack";
        // Inputs
        public const string InputsSingleUxmlPath = "CS_InputsSingle";
        public const string InputsSingleVisualElementName = "VE_InputsSingle";
        public const string RedChannelToggleName = "TG_RedChannel";
        public const string RedChannelVisualElementName = "VE_RedChannelInput";
        public const string RedChannelObjectFieldName = "OF_RedChannelTexture";
        public const string RedChannelPreviewVisualElementName = "VE_RedChannelPreview";
        public const string RedChannelAdvancedOptionsFoldoutName = "FO_RedChannelAdvancedOptions";
        public const string RedChannelUseAllChannelsToggleName = "TG_RedUseAllChannels";
        public const string RedChannelUseRedChannelToggleName = "TG_RedUseRedChannel";
        public const string RedChannelUseGreenChannelToggleName = "TG_RedUseGreenChannel";
        public const string RedChannelUseBlueChannelToggleName = "TG_RedUseBlueChannel";
        public const string RedChannelUseAlphaChannelToggleName = "TG_RedUseAlphaChannel";
        public const string RedChannelDimensionsLabelName = "LB_RedChannelDimensions";
        public const string GreenChannelToggleName = "TG_GreenChannel";
        public const string GreenChannelVisualElementName = "VE_GreenChannelInput";
        public const string GreenChannelObjectFieldName = "OF_GreenChannelTexture";
        public const string GreenChannelPreviewVisualElementName = "VE_GreenChannelPreview";
        public const string GreenChannelAdvancedOptionsFoldoutName = "FO_GreenChannelAdvancedOptions";
        public const string GreenChannelUseAllChannelsToggleName = "TG_GreenUseAllChannels";
        public const string GreenChannelUseRedChannelToggleName = "TG_GreenUseRedChannel";
        public const string GreenChannelUseGreenChannelToggleName = "TG_GreenUseGreenChannel";
        public const string GreenChannelUseBlueChannelToggleName = "TG_GreenUseBlueChannel";
        public const string GreenChannelUseAlphaChannelToggleName = "TG_GreenUseAlphaChannel";
        public const string GreenChannelDimensionsLabelName = "LB_GreenChannelDimensions";
        
        // Export
        public const string ExportUxmlPath = "CS_Export";
        public const string ExportVisualElementName = "VE_Export";
        public const string ExportSingleVisualElementName = "VE_ExportSingle";
        // Preview
        public const string PreviewUxmlPath = "CS_Preview";
        public const string PreviewVisualElement = "VE_Preview";
        // Generate
        public const string GenerateUxmlPath = "CS_GenerateButton";
        public const string GenerateVisualElementName = "VE_GenerateButton";
        public const string GeneratePackSingleVisualElementName = "VE_GeneratePackSingleButton";
        public const string GenerateButtonName = "BT_GenerateButton";
        public const string GeneratePackSingleButtonName = "BT_GeneratePackSingleButton";
        public const string GeneratePackSingleButtonLabel = "Pack";
        // Styles
        public static readonly Color SelectedTabColor = new Color(0.5f, 0.5f, 0.5f);
        public static readonly Color UnselectedTabColor = new Color(0.365f, 0.365f, 0.365f);
        public static readonly Color HoverTabColor = new Color(0.6f, 0.6f, 0.6f);
    }
}