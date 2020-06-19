using UnityEngine;

namespace UnityTextureRgbPacker
{
    public class TexturePacker
    {
        public static UnityEngine.Texture2D GetCompositeTextureRgb(
            Texture2D textureRedChannel,
            Texture2D textureGreenChannel,
            Texture2D textureBlueChannel,
            string compositeTextureName)
        {
            if (!textureRedChannel || !textureGreenChannel || !textureBlueChannel)
            {
                throw new System.Exception("Input textures cannot be null.");
            }

            // Obtaining the texture sizes
            var textureRedChannelWidth = textureRedChannel.width;
            var textureRedChannelHeight = textureRedChannel.height;
            var textureGreenChannelWidth = textureGreenChannel.width;
            var textureGreenChannelHeight = textureGreenChannel.height;
            var textureBlueChannelWidth = textureBlueChannel.width;
            var textureBlueChannelHeight = textureBlueChannel.height;
            
            // If the textures are the same size, combine them...
            if ((textureRedChannelWidth == textureGreenChannelWidth && 
                 textureRedChannelWidth == textureBlueChannelWidth) &&
                (textureRedChannelHeight == textureGreenChannelHeight && 
                 textureRedChannelHeight == textureBlueChannelHeight))
            {
                var packedTexture = CreatePackedTexture(
                    textureRedChannelWidth, textureRedChannelHeight,
                    textureRedChannel,
                    textureGreenChannel,
                    textureBlueChannel,
                    compositeTextureName);

                return packedTexture;
            }
            else
            {
                throw new System.Exception("The input textures need to be the same size.");
            }
            
            // TODO: ... if the textures are not the same size ...
            // TODO: ... find the smallest size, reduce the others and combine them
            
        }

        private static UnityEngine.Texture2D CreatePackedTexture(
            int width, int height,
            Texture2D textureRedChannel,
            Texture2D textureGreenChannel,
            Texture2D textureBlueChannel,
            string compositeTextureName)
        {
            var packedTexture = new Texture2D(width, height);
            packedTexture.name = compositeTextureName;

            var rwCopyTextureRedChannel = TextureUtilities.GetRwTextureCopy(textureRedChannel);
            var rwCopyTextureGreenChannel = TextureUtilities.GetRwTextureCopy(textureGreenChannel);
            var rwCopyTextureBlueChannel = TextureUtilities.GetRwTextureCopy(textureBlueChannel);

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    packedTexture.SetPixel(
                        i, j,
                        new Color(
                            rwCopyTextureRedChannel.GetPixel(i, j).grayscale,
                            rwCopyTextureGreenChannel.GetPixel(i, j).grayscale,
                            rwCopyTextureBlueChannel.GetPixel(i, j).grayscale));
                }
            }
            
            return packedTexture;
        }
    }
}


