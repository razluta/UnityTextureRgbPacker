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
        // Inputs
        public const string InputsSingleUxmlPath = "CS_InputsSingle";
        // Export
        public const string ExportUxmlPath = "CS_Export";
        // Preview
        public const string PreviewUxmlPath = "CS_Preview";
        // Generate
        public const string GenerateUxmlPath = "CS_GenerateButton";
        public const string GenerateButtonName = "BT_GenerateButton";
    }
}