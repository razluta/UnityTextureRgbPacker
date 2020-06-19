using UnityEngine;

namespace UnityTextureRgbPacker
{
    public class TextureUtilities
    {
        public enum TextureUtilitiesFormats
        {
            Png,
            Tga
        }
        
        public static void SaveTextureToPath(
            Texture2D texture, 
            string path, 
            TextureUtilitiesFormats format)
        {
            var bytes = new byte[0];

            switch (format)
            {
                case TextureUtilitiesFormats.Png:
                    bytes = texture.EncodeToPNG();
                    break;
                case TextureUtilitiesFormats.Tga:
                    bytes = texture.EncodeToTGA();
                    break;
                default:
                    break;
            }

            System.IO.File.WriteAllBytes(path, bytes);
        }
        
        public static Texture2D GetRwTextureCopy(Texture2D sourceTexture)
        {
            var renderTexture = RenderTexture.GetTemporary(
                sourceTexture.width,
                sourceTexture.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);
 
            Graphics.Blit(sourceTexture, renderTexture);
            var previous = RenderTexture.active;
            RenderTexture.active = renderTexture;
            var readableText = new Texture2D(sourceTexture.width, sourceTexture.height);
            readableText.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            readableText.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTexture);
            return readableText;
        }
    }
}