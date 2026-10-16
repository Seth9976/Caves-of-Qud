using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000025 RID: 37
	internal class MaterialReferenceManager
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004E14 File Offset: 0x00003014
		public static MaterialReferenceManager instance
		{
			get
			{
				bool flag = MaterialReferenceManager.s_Instance == null;
				if (flag)
				{
					MaterialReferenceManager.s_Instance = new MaterialReferenceManager();
				}
				return MaterialReferenceManager.s_Instance;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004E41 File Offset: 0x00003041
		public static void AddFontAsset(FontAsset fontAsset)
		{
			MaterialReferenceManager.instance.AddFontAssetInternal(fontAsset);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004E50 File Offset: 0x00003050
		private void AddFontAssetInternal(FontAsset fontAsset)
		{
			bool flag = this.m_FontAssetReferenceLookup.ContainsKey(fontAsset.hashCode);
			if (!flag)
			{
				this.m_FontAssetReferenceLookup.Add(fontAsset.hashCode, fontAsset);
				this.m_FontMaterialReferenceLookup.Add(fontAsset.materialHashCode, fontAsset.material);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004EA0 File Offset: 0x000030A0
		public static void AddSpriteAsset(SpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(spriteAsset);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004EB0 File Offset: 0x000030B0
		private void AddSpriteAssetInternal(SpriteAsset spriteAsset)
		{
			bool flag = this.m_SpriteAssetReferenceLookup.ContainsKey(spriteAsset.hashCode);
			if (!flag)
			{
				this.m_SpriteAssetReferenceLookup.Add(spriteAsset.hashCode, spriteAsset);
				this.m_FontMaterialReferenceLookup.Add(spriteAsset.hashCode, spriteAsset.material);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004F00 File Offset: 0x00003100
		public static void AddSpriteAsset(int hashCode, SpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(hashCode, spriteAsset);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004F10 File Offset: 0x00003110
		private void AddSpriteAssetInternal(int hashCode, SpriteAsset spriteAsset)
		{
			bool flag = this.m_SpriteAssetReferenceLookup.ContainsKey(hashCode);
			if (!flag)
			{
				this.m_SpriteAssetReferenceLookup.Add(hashCode, spriteAsset);
				this.m_FontMaterialReferenceLookup.Add(hashCode, spriteAsset.material);
				bool flag2 = spriteAsset.hashCode == 0;
				if (flag2)
				{
					spriteAsset.hashCode = hashCode;
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004F66 File Offset: 0x00003166
		public static void AddFontMaterial(int hashCode, Material material)
		{
			MaterialReferenceManager.instance.AddFontMaterialInternal(hashCode, material);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004F76 File Offset: 0x00003176
		private void AddFontMaterialInternal(int hashCode, Material material)
		{
			this.m_FontMaterialReferenceLookup.Add(hashCode, material);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004F87 File Offset: 0x00003187
		public static void AddColorGradientPreset(int hashCode, TextColorGradient spriteAsset)
		{
			MaterialReferenceManager.instance.AddColorGradientPreset_Internal(hashCode, spriteAsset);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004F98 File Offset: 0x00003198
		private void AddColorGradientPreset_Internal(int hashCode, TextColorGradient spriteAsset)
		{
			bool flag = this.m_ColorGradientReferenceLookup.ContainsKey(hashCode);
			if (!flag)
			{
				this.m_ColorGradientReferenceLookup.Add(hashCode, spriteAsset);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004FC8 File Offset: 0x000031C8
		public bool Contains(FontAsset font)
		{
			return this.m_FontAssetReferenceLookup.ContainsKey(font.hashCode);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004FEC File Offset: 0x000031EC
		public bool Contains(SpriteAsset sprite)
		{
			return this.m_FontAssetReferenceLookup.ContainsKey(sprite.hashCode);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005010 File Offset: 0x00003210
		public static bool TryGetFontAsset(int hashCode, out FontAsset fontAsset)
		{
			return MaterialReferenceManager.instance.TryGetFontAssetInternal(hashCode, out fontAsset);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005030 File Offset: 0x00003230
		private bool TryGetFontAssetInternal(int hashCode, out FontAsset fontAsset)
		{
			fontAsset = null;
			return this.m_FontAssetReferenceLookup.TryGetValue(hashCode, out fontAsset);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005054 File Offset: 0x00003254
		public static bool TryGetSpriteAsset(int hashCode, out SpriteAsset spriteAsset)
		{
			return MaterialReferenceManager.instance.TryGetSpriteAssetInternal(hashCode, out spriteAsset);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005074 File Offset: 0x00003274
		private bool TryGetSpriteAssetInternal(int hashCode, out SpriteAsset spriteAsset)
		{
			spriteAsset = null;
			return this.m_SpriteAssetReferenceLookup.TryGetValue(hashCode, out spriteAsset);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005098 File Offset: 0x00003298
		public static bool TryGetColorGradientPreset(int hashCode, out TextColorGradient gradientPreset)
		{
			return MaterialReferenceManager.instance.TryGetColorGradientPresetInternal(hashCode, out gradientPreset);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000050B8 File Offset: 0x000032B8
		private bool TryGetColorGradientPresetInternal(int hashCode, out TextColorGradient gradientPreset)
		{
			gradientPreset = null;
			return this.m_ColorGradientReferenceLookup.TryGetValue(hashCode, out gradientPreset);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000050DC File Offset: 0x000032DC
		public static bool TryGetMaterial(int hashCode, out Material material)
		{
			return MaterialReferenceManager.instance.TryGetMaterialInternal(hashCode, out material);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000050FC File Offset: 0x000032FC
		private bool TryGetMaterialInternal(int hashCode, out Material material)
		{
			material = null;
			return this.m_FontMaterialReferenceLookup.TryGetValue(hashCode, out material);
		}

		// Token: 0x040000BB RID: 187
		private static MaterialReferenceManager s_Instance;

		// Token: 0x040000BC RID: 188
		private Dictionary<int, Material> m_FontMaterialReferenceLookup = new Dictionary<int, Material>();

		// Token: 0x040000BD RID: 189
		private Dictionary<int, FontAsset> m_FontAssetReferenceLookup = new Dictionary<int, FontAsset>();

		// Token: 0x040000BE RID: 190
		private Dictionary<int, SpriteAsset> m_SpriteAssetReferenceLookup = new Dictionary<int, SpriteAsset>();

		// Token: 0x040000BF RID: 191
		private Dictionary<int, TextColorGradient> m_ColorGradientReferenceLookup = new Dictionary<int, TextColorGradient>();
	}
}
