using System;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

namespace UnityEngine
{
	// Token: 0x02000040 RID: 64
	internal class RuntimeTextSettings : TextSettings
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00011528 File Offset: 0x0000F728
		internal static RuntimeTextSettings defaultTextSettings
		{
			get
			{
				bool flag = RuntimeTextSettings.s_DefaultTextSettings == null;
				if (flag)
				{
					RuntimeTextSettings.s_DefaultTextSettings = ScriptableObject.CreateInstance<RuntimeTextSettings>();
				}
				return RuntimeTextSettings.s_DefaultTextSettings;
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001155C File Offset: 0x0000F75C
		internal override List<FontAsset> GetStaticFallbackOSFontAsset()
		{
			return RuntimeTextSettings.s_FallbackOSFontAssetIMGUIInternal;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00011573 File Offset: 0x0000F773
		internal override void SetStaticFallbackOSFontAsset(List<FontAsset> fontAssets)
		{
			RuntimeTextSettings.s_FallbackOSFontAssetIMGUIInternal = fontAssets;
		}

		// Token: 0x0400013C RID: 316
		private static RuntimeTextSettings s_DefaultTextSettings;

		// Token: 0x0400013D RID: 317
		private static List<FontAsset> s_FallbackOSFontAssetIMGUIInternal;
	}
}
