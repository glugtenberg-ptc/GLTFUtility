using UnityEngine;
#if !UNITY_2020_2_OR_NEWER
using UnityEditor.Experimental.AssetImporters;
#else
using UnityEditor.AssetImporters;
#endif

namespace Siccity.GLTFUtility {
	// PTC HACK, set this importer to target .ptcglb instead of .glb so that it
	// does not conflict with MRTK
	// This prevents the errors from showing up in the console when opening the project.
	// It retains the ability for PTC to use this importer if desired.
	// Note that today we do not import GLBs as assets into a Unity Project, we load them
	// at run time.
	[ScriptedImporter(1, "ptcglb")]
	public class GLBImporter : GLTFImporter {

		public override void OnImportAsset(AssetImportContext ctx) {
			// Load asset
			AnimationClip[] animations;
			if (importSettings == null) importSettings = new ImportSettings();
			GameObject root = Importer.LoadFromFile(ctx.assetPath, importSettings, out animations, Format.GLB);
			// Save asset
			GLTFAssetUtility.SaveToAsset(root, animations, ctx, importSettings);
		}
	}
}