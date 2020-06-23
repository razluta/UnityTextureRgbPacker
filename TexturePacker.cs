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
            Texture2D textureAlphaChannel = null)
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

            var hasAlphaInput = false;
            if (textureAlphaChannel)
            {
                validTextureList.Add(textureAlphaChannel);
                hasAlphaInput = true;
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
            if (!textureAlphaChannel)
            {
                textureAlphaChannel = CreateTextureOfColor(smallestTextureWidth, smallestTextureHeight, Color.black);
            }
            
            // Obtaining the texture sizes
            var textureRedChannelWidth = textureRedChannel.width;
            var textureRedChannelHeight = textureRedChannel.height;
            var textureGreenChannelWidth = textureGreenChannel.width;
            var textureGreenChannelHeight = textureGreenChannel.height;
            var textureBlueChannelWidth = textureBlueChannel.width;
            var textureBlueChannelHeight = textureBlueChannel.height;
            var textureAlphaChannelWidth = textureAlphaChannel.width;
            var textureAlphaChannelHeight = textureAlphaChannel.height;
            
            // Resize all the textures to the smallest input
            textureRedChannel = TextureUtilities.GetRwTextureCopy(textureRedChannel);
            if (smallestTextureWidth != textureRedChannelWidth && smallestTextureHeight != textureRedChannelHeight)
            {
                textureRedChannel = TextureUtilities.ScaleTexture(
                    textureRedChannel, smallestTextureWidth, smallestTextureHeight);
            }
            textureGreenChannel = TextureUtilities.GetRwTextureCopy(textureGreenChannel);
            if (smallestTextureWidth != textureGreenChannelWidth && smallestTextureHeight != textureGreenChannelHeight)
            {
                textureGreenChannel = TextureUtilities.ScaleTexture(
                    textureGreenChannel, smallestTextureWidth, smallestTextureHeight);
            }
            textureBlueChannel = TextureUtilities.GetRwTextureCopy(textureBlueChannel);
            if (smallestTextureWidth != textureBlueChannelWidth && smallestTextureHeight != textureBlueChannelHeight)
            {
                textureBlueChannel = TextureUtilities.ScaleTexture(
                    textureBlueChannel, smallestTextureWidth, smallestTextureHeight);
            }
            textureAlphaChannel = TextureUtilities.GetRwTextureCopy(textureAlphaChannel);
            if (smallestTextureWidth != textureAlphaChannelWidth && smallestTextureHeight != textureAlphaChannelHeight)
            {
                textureAlphaChannel = TextureUtilities.ScaleTexture(
                    textureAlphaChannel, smallestTextureWidth, smallestTextureHeight);
            }

            // If there was an alpha input, use it. If not, create a texture without it.
            if (hasAlphaInput)
            {
                var packedTextureWithAlpha = CreatePackedTexture(
                    smallestTextureWidth, smallestTextureHeight,
                    compositeTextureName,
                    textureRedChannel,
                    textureGreenChannel,
                    textureBlueChannel,
                    textureAlphaChannel);
                return packedTextureWithAlpha;
            }
            var packedTextureWithoutAlpha = CreatePackedTexture(
                smallestTextureWidth, smallestTextureHeight,
                compositeTextureName,
                textureRedChannel,
                textureGreenChannel,
                textureBlueChannel,
                null);
            return packedTextureWithoutAlpha;
        }

        private static UnityEngine.Texture2D CreatePackedTexture(
            int width, int height,
            string compositeTextureName,
            Texture2D textureRedChannel,
            Texture2D textureGreenChannel,
            Texture2D textureBlueChannel,
            Texture2D textureAlphaChannel = null)
        {
            var packedTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
            packedTexture.name = compositeTextureName;

            if (textureAlphaChannel)
            {
                packedTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);

                for (var i = 0; i < width; i++)
                {
                    for (var j = 0; j < height; j++)
                    {
                        packedTexture.SetPixel(
                            i, j,
                            new Color(
                                textureRedChannel.GetPixel(i, j).grayscale,
                                textureGreenChannel.GetPixel(i, j).grayscale,
                                textureBlueChannel.GetPixel(i, j).grayscale,
                                textureAlphaChannel.GetPixel(i, j).grayscale)
                        );
                    }
                }
            }
            else
            {
                for (var i = 0; i < width; i++)
                {
                    for (var j = 0; j < height; j++)
                    {
                        packedTexture.SetPixel(
                            i, j,
                            new Color(
                                textureRedChannel.GetPixel(i, j).grayscale,
                                textureGreenChannel.GetPixel(i, j).grayscale,
                                textureBlueChannel.GetPixel(i, j).grayscale));
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