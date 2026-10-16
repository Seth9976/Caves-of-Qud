using System;
using UnityEngine.SceneManagement;

namespace LaundryBear
{
	// Token: 0x02000008 RID: 8
	public static class SceneExtensions
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000023E0 File Offset: 0x000005E0
		public static SceneExtensions.SceneLoadType GetSceneLoadType(this Scene scene)
		{
			if (!scene.isLoaded)
			{
				return SceneExtensions.SceneLoadType.Unloaded;
			}
			if (scene.buildIndex == -1)
			{
				return SceneExtensions.SceneLoadType.AssetBundle;
			}
			if (scene.buildIndex >= SceneManager.sceneCountInBuildSettings)
			{
				return SceneExtensions.SceneLoadType.NotInBuildSettings;
			}
			return SceneExtensions.SceneLoadType.Build;
		}

		// Token: 0x0200001F RID: 31
		public enum SceneLoadType
		{
			// Token: 0x0400004E RID: 78
			Unloaded,
			// Token: 0x0400004F RID: 79
			Build,
			// Token: 0x04000050 RID: 80
			AssetBundle,
			// Token: 0x04000051 RID: 81
			NotInBuildSettings
		}
	}
}
