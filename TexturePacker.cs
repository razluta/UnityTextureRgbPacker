using System.Collections.Generic;
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
            Texture2D textureAlphaChannel = null,
            int width = 0, 
            int height = 0)
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
            
            // Determine the smallest texture size or use the input if one is provided
            int resizeWidth;
            int resizeHeight;
            if (width == 0 || height == 0)
            {
                var resizeTargetTexture = TextureUtilities.GetSmallestSizedTexture(validTextureList);
                resizeWidth = resizeTargetTexture.width;
                resizeHeight = resizeTargetTexture.height;
            }
            else
            {
                resizeWidth = width;
                resizeHeight = height;
            }

            // Create "empty" black textures for the ones that are missing
            if (!textureRedChannel)
            {
                textureRedChannel = CreateTextureOfColor(resizeWidth, resizeHeight, Color.black);
            }
            if (!textureGreenChannel)
            {
                textureGreenChannel = CreateTextureOfColor(resizeWidth, resizeHeight, Color.black);
            }
            if (!textureBlueChannel)
            {
                textureBlueChannel = CreateTextureOfColor(resizeWidth, resizeHeight, Color.black);
            }
            if (!textureAlphaChannel)
            {
                textureAlphaChannel = CreateTextureOfColor(resizeWidth, resizeHeight, Color.black);
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
            
            // Resize all the textures to the needed size
            textureRedChannel = TextureUtilities.GetRwTextureCopy(textureRedChannel);
            if (resizeWidth != textureRedChannelWidth && resizeHeight != textureRedChannelHeight)
            {
                textureRedChannel = TextureUtilities.ScaleTexture(
                    textureRedChannel, resizeWidth, resizeHeight);
            }
            textureGreenChannel = TextureUtilities.GetRwTextureCopy(textureGreenChannel);
            if (resizeWidth != textureGreenChannelWidth && resizeHeight != textureGreenChannelHeight)
            {
                textureGreenChannel = TextureUtilities.ScaleTexture(
                    textureGreenChannel, resizeWidth, resizeHeight);
            }
            textureBlueChannel = TextureUtilities.GetRwTextureCopy(textureBlueChannel);
            if (resizeWidth != textureBlueChannelWidth && resizeHeight != textureBlueChannelHeight)
            {
                textureBlueChannel = TextureUtilities.ScaleTexture(
                    textureBlueChannel, resizeWidth, resizeHeight);
            }
            textureAlphaChannel = TextureUtilities.GetRwTextureCopy(textureAlphaChannel);
            if (resizeWidth != textureAlphaChannelWidth && resizeHeight != textureAlphaChannelHeight)
            {
                textureAlphaChannel = TextureUtilities.ScaleTexture(
                    textureAlphaChannel, resizeWidth, resizeHeight);
            }

            // If there was an alpha input, use it. If not, create a texture without it.
            if (hasAlphaInput)
            {
                var packedTextureWithAlpha = CreatePackedTexture(
                    resizeWidth, resizeHeight,
                    compositeTextureName,
                    textureRedChannel,
                    textureGreenChannel,
                    textureBlueChannel,
                    textureAlphaChannel);
                return packedTextureWithAlpha;
            }
            var packedTextureWithoutAlpha = CreatePackedTexture(
                resizeWidth, resizeHeight,
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