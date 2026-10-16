using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000673 RID: 1651
	internal static class Shaders
	{
		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06003039 RID: 12345 RVA: 0x000B658E File Offset: 0x000B478E
		public static Material runtimeMaterial
		{
			get
			{
				return Shaders.GetOrCreateMaterial(ref Shaders.s_RuntimeMaterial, Shaders.k_Runtime);
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x0600303A RID: 12346 RVA: 0x000B659F File Offset: 0x000B479F
		public static Material runtimeWorldMaterial
		{
			get
			{
				return Shaders.GetOrCreateMaterial(ref Shaders.s_RuntimeWorldMaterial, Shaders.k_RuntimeWorld);
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x000B65B0 File Offset: 0x000B47B0
		public static Material editorMaterial
		{
			get
			{
				return Shaders.GetOrCreateMaterial(ref Shaders.s_EditorMaterial, Shaders.k_Editor);
			}
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000B65C4 File Offset: 0x000B47C4
		private static Material GetOrCreateMaterial(ref Material material, string shaderName)
		{
			bool flag = material == null;
			if (flag)
			{
				Shader shader = Shader.Find(shaderName);
				bool flag2 = shader == null;
				if (flag2)
				{
					Debug.LogError("Could not find shader '" + shaderName + "'");
					return null;
				}
				material = new Material(shader);
				material.hideFlags = HideFlags.DontSave;
			}
			return material;
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000B6625 File Offset: 0x000B4825
		public static void Acquire()
		{
			Shaders.s_RefCount++;
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000B6634 File Offset: 0x000B4834
		public static void Release()
		{
			Shaders.s_RefCount--;
			Debug.Assert(Shaders.s_RefCount >= 0, "UIR materials acquire/release don't match.");
			bool flag = Shaders.s_RefCount < 1;
			if (flag)
			{
				Shaders.s_RefCount = 0;
				UIRUtility.Destroy(Shaders.s_RuntimeMaterial);
				UIRUtility.Destroy(Shaders.s_RuntimeWorldMaterial);
				UIRUtility.Destroy(Shaders.s_EditorMaterial);
				Shaders.s_RuntimeMaterial = null;
				Shaders.s_RuntimeWorldMaterial = null;
				Shaders.s_EditorMaterial = null;
			}
		}

		// Token: 0x0400178D RID: 6029
		public static readonly string k_AtlasBlit = "Hidden/Internal-UIRAtlasBlitCopy";

		// Token: 0x0400178E RID: 6030
		public static readonly string k_Editor = "Hidden/UIElements/EditorUIE";

		// Token: 0x0400178F RID: 6031
		public static readonly string k_Runtime = "Hidden/Internal-UIRDefault";

		// Token: 0x04001790 RID: 6032
		public static readonly string k_RuntimeWorld = "Hidden/Internal-UIRDefaultWorld";

		// Token: 0x04001791 RID: 6033
		public static readonly string k_ColorConversionBlit = "Hidden/Internal-UIE-ColorConversionBlit";

		// Token: 0x04001792 RID: 6034
		public static readonly string k_ForceGammaKeyword = "UIE_FORCE_GAMMA";

		// Token: 0x04001793 RID: 6035
		private static Material s_RuntimeMaterial;

		// Token: 0x04001794 RID: 6036
		private static Material s_RuntimeWorldMaterial;

		// Token: 0x04001795 RID: 6037
		private static Material s_EditorMaterial;

		// Token: 0x04001796 RID: 6038
		private static int s_RefCount;
	}
}
