using UnityEditor;

public class ImageAssetPreprocessor : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        if (assetPath.Contains("Sprites"))
        {
            TextureImporter textureImporter = (TextureImporter) assetImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
        }
    }
}
