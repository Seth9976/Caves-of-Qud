using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000024 RID: 36
	internal struct MaterialReference
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00004C1F File Offset: 0x00002E1F
		public MaterialReference(int index, FontAsset fontAsset, SpriteAsset spriteAsset, Material material, float padding)
		{
			this.index = index;
			this.fontAsset = fontAsset;
			this.spriteAsset = spriteAsset;
			this.material = material;
			this.isFallbackMaterial = false;
			this.fallbackMaterial = null;
			this.padding = padding;
			this.referenceCount = 0;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004C5C File Offset: 0x00002E5C
		public static bool Contains(MaterialReference[] materialReferences, FontAsset fontAsset)
		{
			int hashCode = fontAsset.GetHashCode();
			int num = 0;
			while (num < materialReferences.Length && materialReferences[num].fontAsset != null)
			{
				bool flag = materialReferences[num].fontAsset.GetHashCode() == hashCode;
				if (flag)
				{
					return true;
				}
				num++;
			}
			return false;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004CC0 File Offset: 0x00002EC0
		public static int AddMaterialReference(Material material, FontAsset fontAsset, ref MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
		{
			int hashCode = material.GetHashCode();
			int count;
			bool flag = materialReferenceIndexLookup.TryGetValue(hashCode, out count);
			int num;
			if (flag)
			{
				num = count;
			}
			else
			{
				count = materialReferenceIndexLookup.Count;
				materialReferenceIndexLookup[hashCode] = count;
				bool flag2 = count >= materialReferences.Length;
				if (flag2)
				{
					Array.Resize<MaterialReference>(ref materialReferences, Mathf.NextPowerOfTwo(count + 1));
				}
				materialReferences[count].index = count;
				materialReferences[count].fontAsset = fontAsset;
				materialReferences[count].spriteAsset = null;
				materialReferences[count].material = material;
				materialReferences[count].referenceCount = 0;
				num = count;
			}
			return num;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004D64 File Offset: 0x00002F64
		public static int AddMaterialReference(Material material, SpriteAsset spriteAsset, ref MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
		{
			int hashCode = material.GetHashCode();
			int count;
			bool flag = materialReferenceIndexLookup.TryGetValue(hashCode, out count);
			int num;
			if (flag)
			{
				num = count;
			}
			else
			{
				count = materialReferenceIndexLookup.Count;
				materialReferenceIndexLookup[hashCode] = count;
				bool flag2 = count >= materialReferences.Length;
				if (flag2)
				{
					Array.Resize<MaterialReference>(ref materialReferences, Mathf.NextPowerOfTwo(count + 1));
				}
				materialReferences[count].index = count;
				materialReferences[count].fontAsset = materialReferences[0].fontAsset;
				materialReferences[count].spriteAsset = spriteAsset;
				materialReferences[count].material = material;
				materialReferences[count].referenceCount = 0;
				num = count;
			}
			return num;
		}

		// Token: 0x040000B3 RID: 179
		public int index;

		// Token: 0x040000B4 RID: 180
		public FontAsset fontAsset;

		// Token: 0x040000B5 RID: 181
		public SpriteAsset spriteAsset;

		// Token: 0x040000B6 RID: 182
		public Material material;

		// Token: 0x040000B7 RID: 183
		public bool isFallbackMaterial;

		// Token: 0x040000B8 RID: 184
		public Material fallbackMaterial;

		// Token: 0x040000B9 RID: 185
		public float padding;

		// Token: 0x040000BA RID: 186
		public int referenceCount;
	}
}
