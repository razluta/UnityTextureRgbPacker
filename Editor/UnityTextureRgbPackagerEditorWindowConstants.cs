using UnityEngine;

namespace UnityTextureRgbPacker.Editor
{
    public static class UnityTextureRgbPackagerEditorWindowConstants
    {
        public const string TexturePackerName = "Texture RGB(A) Packer";
        public static readonly Vector2 MinWindowSize = new Vector2(387, 935);
        
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