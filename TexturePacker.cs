using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityTextureRgbPacker
{
    public class TexturePacker
    {
        public static UnityEngine.Texture2D GetCompositeTextureRgb(
            string compositeTextureName,
            Texture2D textureRedChannel,
            Texture2D textureGreenChannel,
            Texture2D textureBlueChannel,
            Texture2D textureAlphaChannel)
        {
            if (!textureRedChannel && !textureGreenChannel && !textureBlueChannel && !textureAlphaChannel)
            {
                throw new System.Exception("At least one texture input must be valid.");
            }

            // If the texture input was provided, add it to a list
            var validTextureList = new List<Texture2D>();
            if (textureRedChannel)
            {
                validTextureList.Add(textureRedChannel);
            }
            if (textureGreenChannel)
            {
                validTextureList.Add(textureGreenChannel);
            }
            if (textureBlueChannel)
            {
                validTextureList.Add(textureBlueChannel);
            }

            if (textureAlphaChannel)
            {
                validTextureList.Add(textureAlphaChannel);
            }

            // Determine the smallest texture size
            var smallestTexture = TextureUtilities.GetSmallestSizedTexture(validTextureList);
            var smallestTextureWidth = smallestTexture.width;
            var smallestTextureHeight = smallestTexture.height;
            
            // Create "empty" black textures for the ones that are missing
            if (!textureRedChannel)
            {
                textureRedChannel = CreateTextureOfColor(smallestTextureWidth, smallestTextureHeight, Color.black);
            }
            if (!textureGreenChannel)
            {
                textureGreenChannel = CreateTextureOfColor(smallestTextureWidth, smallestTextureHeight, Color.black);
            }
            if (!textureBlueChannel)
            {
                textureBlueChannel = CreateTextureOfColor(smallestTextureWidth, smallestTextureHeight, Color.black);
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
                    compositeTextureName,
                    textureRedChannel,
                    textureGreenChannel,
                    textureBlueChannel,
                    textureAlphaChannel);

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
            string compositeTextureName,
            Texture2D textureRedChannel,
            Texture2D textureGreenChannel,
            Texture2D textureBlueChannel,
            Texture2D textureAlphaChannel)
        {
            var packedTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
            packedTexture.name = compositeTextureName;

            var rwCopyTextureRedChannel = TextureUtilities.GetRwTextureCopy(textureRedChannel);
            var rwCopyTextureGreenChannel = TextureUtilities.GetRwTextureCopy(textureGreenChannel);
            var rwCopyTextureBlueChannel = TextureUtilities.GetRwTextureCopy(textureBlueChannel);

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (textureAlphaChannel)
                    {
                        packedTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
                        var rwCopyTextureAlphaChannel = TextureUtilities.GetRwTextureCopy(textureAlphaChannel);
                        packedTexture.SetPixel(
                            i, j,
                            new Color(
                                rwCopyTextureRedChannel.GetPixel(i, j).grayscale,
                                rwCopyTextureGreenChannel.GetPixel(i, j).grayscale,
                                rwCopyTextureBlueChannel.GetPixel(i, j).grayscale,
                                rwCopyTextureAlphaChannel.GetPixel(i, j).grayscale)
                            );
                    }
                    else
                    {
                        packedTexture.SetPixel(
                        i, j,
                        new Color(
                            rwCopyTextureRedChannel.GetPixel(i, j).grayscale,
                            rwCopyTextureGreenChannel.GetPixel(i, j).grayscale,
                            rwCopyTextureBlueChannel.GetPixel(i, j).grayscale));
                    }
                }
            }
            packedTexture.Apply();
            return packedTexture;
        }
        
        private static UnityEngine.Texture2D CreateTextureOfColor(int width, int height, Color color)
        {
            var texture = new Texture2D(width, height);

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    texture.SetPixel(i, j, color);
                }
            }
            texture.Apply();
            return texture;
        }
    }
}


