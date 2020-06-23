# Unity Texture RGB(A) Packer [![License](https://img.shields.io/badge/License-MIT-lightgrey.svg?style=flat)](http://mit-license.org)
This is a Unity Texture Packer that allows the user to pack 1 to 4 textures in any of the Red, Green, Blue and Alpha channels of a newly created texture that gets saved to disk and can be used in the Editor and Engine.

Verified on the following versions of Unity:
- 2019.4
- 2019.3

_Important to note: input textures must be the same size; if the input textures are not the same size, the code will scale the larger textures to match the smaller textures and artifacts can occur in the compression_

![](https://github.com/razluta/UnityTextureRgbPacker/blob/master/Screenshots/UnityTextureRgbPacker.png)

*  *  *  *  *

## Setup
##### Option A) Clone or download the repository and drop it in your Unity project.
##### Option B) Add the repository to the package manifest (go in YourProject/Packages/ and open the "manifest.json" file and add "com..." line in the depenencies section). If you don't have Git installed, Unity will require you to install it.
```
{
  "dependencies": {
      ...
      "com.razluta.unitytexturergbpacker": "https://github.com/razluta/UnityTextureRgbPacker.git"
      ...
  }
}
```
*  *  *  *  *

## Automatic Generation using the API
If you are using this repository to automate the creation of the packed texture in your own code, you'll need to use the:
```
UnityextureRgbPacker.TexturePacker.GetCompositeTextureRgb(
            string compositeTextureName,
            Texture2D textureRedChannel,
            Texture2D textureGreenChannel,
            Texture2D textureBlueChannel,
            Texture2D textureAlphaChannel = null,
            int width = 0, 
            int height = 0)
# compositeTextureName = a string representing the name of the texture
# textureRedChannel = a Texture2D asset representing the input for the Red channel
# textureGreenChannel = a Texture2D asset representing the input for the Green channel
# textureBlueChannel = a Texture2D asset representing the input for the Blue channel
# textureAlphaChannel = (optional) a Texture2D asset representing the input for the Alpha channel
# width = (optional) an integer representing the desired output texture width
# height = (optional) an integer representing the desired output texture height
# If the width or height are left at 0, the the smallest input texture will be used for sizing.
```
To save the file to disk, you can use the:
```
UnityextureRgbPacker.TextureUtilities.SaveTextureToPath(
                texture,
                path,
                format)
# texture = a Texture2D asset representing the object you want to save
# path = a string representing the absolute path of the texture
# format = one of the accepted UnityextureRgbPacker.TextureUtilities.TextureUtilietiesFormats texture formats
```
*  *  *  *  *

## Manual generation using the UI
### Step 001
Launch the _**Unity Texture RGB(A) Packer**_ from the top menu bar under **Texture RGB(A) Packer > Open Texture Packer**.

### Step 002
Find and input the textures you want in each channel.
Rules:
 - At least one channel must be used. 
 - If at least one of the R, G or B channels is used, the others will be filled with black if the input is left empty.
 - If the A channel is left empty, the textures will be saved as an RGB texture, otherwise it will be saved as an RGBA texture.

### Step 003
Choose export formats. The currently supported formats are .tga and .png.

### Step 004
Fill in "Name Identifier". Set to **Composite** by default.
The code will figure out the common name prefix between all the input textures and use it to add the "Name Identifier" as a suffix. If no common prefix is found, the texture will just be named by the value of the "Name Identifier".
For example, if the source textures are called "Env_Tree_Roughness" and "Env_Tree_Metallic", the result will be called "Env_Tree_Composite".

### Step 005
For sizing, if the settings are left at default, the tool will determine which of the input textures is smalles and use its width and heigh to scale down the other input textures.
If you want to set your own size, enable the **(override) scale to specific size** flag and input the X (width) and Y (height) values of the target texture. 
_It is important to note that scaling textures can introduce artifacts, so we recommend that the inputs are all the same size and you avoid using the scaling, unless it is highly necessary._

### Step 006
Press "Generated Packed Texture".
This button will generate the texture in the same folder as the first valid input (if the texture for the Red channel is the first valid input, the code will save the need composite texture in that same folder).
After the image is saved, the preview will update with the newly generate image and the code will attempt to select in the Project window.

![](https://github.com/razluta/UnityTextureRgbPacker/blob/master/Screenshots/UnityTextureRgbPackerUsage.gif)
