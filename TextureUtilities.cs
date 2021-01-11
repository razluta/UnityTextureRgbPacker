using System.Collections.Generic;
using UnityEngine;

namespace UnityTextureRgbPacker 
{
    public static class TextureUtilities
    {
        public const string TgaExtension = ".tga";
        public const string PngExtension = ".png";
            
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
            var rwTexture = new Texture2D(sourceTexture.width, sourceTexture.height);
            rwTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            rwTexture.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTexture);
            return rwTexture;
        }
        
        public static Texture2D ScaleTexture(Texture2D texture,int textureWidth,int textureHeight, 
            int textureDepth=24)
        {
            var renderTexture = new RenderTexture(textureWidth, textureHeight,textureDepth);
            RenderTexture.active = renderTexture;
            Graphics.Blit(texture, renderTexture);
            var scaledTexture = new Texture2D(textureWidth,textureHeight);
            scaledTexture.ReadPixels(new Rect(0,0,textureWidth,textureHeight),0,0);
            scaledTexture.Apply();
            return scaledTexture;
        }

        public static Texture2D GetSmallestSizedTexture(List<Texture2D> textures)
        {
            var smallestTexture = textures[0];
            var smallestTextureArea = textures[0].width * textures[0].height;
            
            foreach (var texture in textures)
            {
                var textureArea = texture.width * texture.height;
                if (textureArea < smallestTextureArea)
                {
                    smallestTexture = texture;
                    smallestTextureArea = textureArea;
                }
            }
            
            return smallestTexture;
        }
        
        public static UnityEngine.Texture2D GetSingleChannelTexture(Texture2D texture, TextureChannel textureChannel)
        {
            var textureWidth = texture.width;
            var textureHeight = texture.height;
            var singleChannelTexture = new Texture2D(textureWidth, textureHeight);
            var readableTexture = GetRwTextureCopy(texture);

            for (var x = 0; x < textureWidth; x++)
            {
                for (var y = 0; y < textureHeight; y++)
                {
                    var color = readableTexture.GetPixel(x, y);
                    var newColor = Color.black;
                    switch (textureChannel)
                    {
                        case TextureChannel.R:
                            newColor = new Color(color.r, color.r, color.r);
                            break;
                        case TextureChannel.G:
                            newColor = new Color(color.g, color.g, color.g);
                            break;
                        case TextureChannel.B:
                            newColor = new Color(color.b, color.b, color.b);
                            break;
                        case TextureChannel.A:
                            newColor = new Color(color.a, color.a, color.a);
                            break;
                    }
                    
                    singleChannelTexture.SetPixel(x, y, newColor);
                }
            }
            
            singleChannelTexture.Apply();
            return singleChannelTexture;
        }
        
        public enum TextureChannel
        {
            R,
            G,
            B,
            A
        };

    }
}