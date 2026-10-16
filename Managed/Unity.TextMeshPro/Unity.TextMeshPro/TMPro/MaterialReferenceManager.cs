using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000011 RID: 17
	public class MaterialReferenceManager
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000250D File Offset: 0x0000070D
		public static MaterialReferenceManager instance
		{
			get
			{
				if (MaterialReferenceManager.s_Instance == null)
				{
					MaterialReferenceManager.s_Instance = new MaterialReferenceManager();
				}
				return MaterialReferenceManager.s_Instance;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002525 File Offset: 0x00000725
		public static void AddFontAsset(TMP_FontAsset fontAsset)
		{
			MaterialReferenceManager.instance.AddFontAssetInternal(fontAsset);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002532 File Offset: 0x00000732
		private void AddFontAssetInternal(TMP_FontAsset fontAsset)
		{
			if (this.m_FontAssetReferenceLookup.ContainsKey(fontAsset.hashCode))
			{
				return;
			}
			this.m_FontAssetReferenceLookup.Add(fontAsset.hashCode, fontAsset);
			this.m_FontMaterialReferenceLookup.Add(fontAsset.materialHashCode, fontAsset.material);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002571 File Offset: 0x00000771
		public static void AddSpriteAsset(TMP_SpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(spriteAsset);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000257E File Offset: 0x0000077E
		private void AddSpriteAssetInternal(TMP_SpriteAsset spriteAsset)
		{
			if (this.m_SpriteAssetReferenceLookup.ContainsKey(spriteAsset.hashCode))
			{
				return;
			}
			this.m_SpriteAssetReferenceLookup.Add(spriteAsset.hashCode, spriteAsset);
			this.m_FontMaterialReferenceLookup.Add(spriteAsset.hashCode, spriteAsset.material);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000025BD File Offset: 0x000007BD
		public static void AddSpriteAsset(int hashCode, TMP_SpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(hashCode, spriteAsset);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000025CB File Offset: 0x000007CB
		private void AddSpriteAssetInternal(int hashCode, TMP_SpriteAsset spriteAsset)
		{
			if (this.m_SpriteAssetReferenceLookup.ContainsKey(hashCode))
			{
				return;
			}
			this.m_SpriteAssetReferenceLookup.Add(hashCode, spriteAsset);
			this.m_FontMaterialReferenceLookup.Add(hashCode, spriteAsset.material);
			if (spriteAsset.hashCode == 0)
			{
				spriteAsset.hashCode = hashCode;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000260A File Offset: 0x0000080A
		public static void AddFontMaterial(int hashCode, Material material)
		{
			MaterialReferenceManager.instance.AddFontMaterialInternal(hashCode, material);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002618 File Offset: 0x00000818
		private void AddFontMaterialInternal(int hashCode, Material material)
		{
			this.m_FontMaterialReferenceLookup.Add(hashCode, material);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002627 File Offset: 0x00000827
		public static void AddColorGradientPreset(int hashCode, TMP_ColorGradient spriteAsset)
		{
			MaterialReferenceManager.instance.AddColorGradientPreset_Internal(hashCode, spriteAsset);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002635 File Offset: 0x00000835
		private void AddColorGradientPreset_Internal(int hashCode, TMP_ColorGradient spriteAsset)
		{
			if (this.m_ColorGradientReferenceLookup.ContainsKey(hashCode))
			{
				return;
			}
			this.m_ColorGradientReferenceLookup.Add(hashCode, spriteAsset);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002653 File Offset: 0x00000853
		public bool Contains(TMP_FontAsset font)
		{
			return this.m_FontAssetReferenceLookup.ContainsKey(font.hashCode);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002666 File Offset: 0x00000866
		public bool Contains(TMP_SpriteAsset sprite)
		{
			return this.m_FontAssetReferenceLookup.ContainsKey(sprite.hashCode);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002679 File Offset: 0x00000879
		public static bool TryGetFontAsset(int hashCode, out TMP_FontAsset fontAsset)
		{
			return MaterialReferenceManager.instance.TryGetFontAssetInternal(hashCode, out fontAsset);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002687 File Offset: 0x00000887
		private bool TryGetFontAssetInternal(int hashCode, out TMP_FontAsset fontAsset)
		{
			fontAsset = null;
			return this.m_FontAssetReferenceLookup.TryGetValue(hashCode, out fontAsset);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002699 File Offset: 0x00000899
		public static bool TryGetSpriteAsset(int hashCode, out TMP_SpriteAsset spriteAsset)
		{
			return MaterialReferenceManager.instance.TryGetSpriteAssetInternal(hashCode, out spriteAsset);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000026A7 File Offset: 0x000008A7
		private bool TryGetSpriteAssetInternal(int hashCode, out TMP_SpriteAsset spriteAsset)
		{
			spriteAsset = null;
			return this.m_SpriteAssetReferenceLookup.TryGetValue(hashCode, out spriteAsset);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000026B9 File Offset: 0x000008B9
		public static bool TryGetColorGradientPreset(int hashCode, out TMP_ColorGradient gradientPreset)
		{
			return MaterialReferenceManager.instance.TryGetColorGradientPresetInternal(hashCode, out gradientPreset);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000026C7 File Offset: 0x000008C7
		private bool TryGetColorGradientPresetInternal(int hashCode, out TMP_ColorGradient gradientPreset)
		{
			gradientPreset = null;
			return this.m_ColorGradientReferenceLookup.TryGetValue(hashCode, out gradientPreset);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000026D9 File Offset: 0x000008D9
		public static bool TryGetMaterial(int hashCode, out Material material)
		{
			return MaterialReferenceManager.instance.TryGetMaterialInternal(hashCode, out material);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000026E7 File Offset: 0x000008E7
		private bool TryGetMaterialInternal(int hashCode, out Material material)
		{
			material = null;
			return this.m_FontMaterialReferenceLookup.TryGetValue(hashCode, out material);
		}

		// Token: 0x0400001E RID: 30
		private static MaterialReferenceManager s_Instance;

		// Token: 0x0400001F RID: 31
		private Dictionary<int, Material> m_FontMaterialReferenceLookup = new Dictionary<int, Material>();

		// Token: 0x04000020 RID: 32
		private Dictionary<int, TMP_FontAsset> m_FontAssetReferenceLookup = new Dictionary<int, TMP_FontAsset>();

		// Token: 0x04000021 RID: 33
		private Dictionary<int, TMP_SpriteAsset> m_SpriteAssetReferenceLookup = new Dictionary<int, TMP_SpriteAsset>();

		// Token: 0x04000022 RID: 34
		private Dictionary<int, TMP_ColorGradient> m_ColorGradientReferenceLookup = new Dictionary<int, TMP_ColorGradient>();
	}
}
