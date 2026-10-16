using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000013 RID: 19
	public struct MaterialReference
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002730 File Offset: 0x00000930
		public MaterialReference(int index, TMP_FontAsset fontAsset, TMP_SpriteAsset spriteAsset, Material material, float padding)
		{
			this.index = index;
			this.fontAsset = fontAsset;
			this.spriteAsset = spriteAsset;
			this.material = material;
			this.isDefaultMaterial = material.GetInstanceID() == fontAsset.material.GetInstanceID();
			this.isFallbackMaterial = false;
			this.fallbackMaterial = null;
			this.padding = padding;
			this.referenceCount = 0;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002794 File Offset: 0x00000994
		public static bool Contains(MaterialReference[] materialReferences, TMP_FontAsset fontAsset)
		{
			int instanceID = fontAsset.GetInstanceID();
			int num = 0;
			while (num < materialReferences.Length && materialReferences[num].fontAsset != null)
			{
				if (materialReferences[num].fontAsset.GetInstanceID() == instanceID)
				{
					return true;
				}
				num++;
			}
			return false;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000027E4 File Offset: 0x000009E4
		public static int AddMaterialReference(Material material, TMP_FontAsset fontAsset, ref MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
		{
			int instanceID = material.GetInstanceID();
			int count;
			if (materialReferenceIndexLookup.TryGetValue(instanceID, out count))
			{
				return count;
			}
			count = materialReferenceIndexLookup.Count;
			materialReferenceIndexLookup[instanceID] = count;
			if (count >= materialReferences.Length)
			{
				Array.Resize<MaterialReference>(ref materialReferences, Mathf.NextPowerOfTwo(count + 1));
			}
			materialReferences[count].index = count;
			materialReferences[count].fontAsset = fontAsset;
			materialReferences[count].spriteAsset = null;
			materialReferences[count].material = material;
			materialReferences[count].isDefaultMaterial = instanceID == fontAsset.material.GetInstanceID();
			materialReferences[count].referenceCount = 0;
			return count;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000288C File Offset: 0x00000A8C
		public static int AddMaterialReference(Material material, TMP_SpriteAsset spriteAsset, ref MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
		{
			int instanceID = material.GetInstanceID();
			int count;
			if (materialReferenceIndexLookup.TryGetValue(instanceID, out count))
			{
				return count;
			}
			count = materialReferenceIndexLookup.Count;
			materialReferenceIndexLookup[instanceID] = count;
			if (count >= materialReferences.Length)
			{
				Array.Resize<MaterialReference>(ref materialReferences, Mathf.NextPowerOfTwo(count + 1));
			}
			materialReferences[count].index = count;
			materialReferences[count].fontAsset = materialReferences[0].fontAsset;
			materialReferences[count].spriteAsset = spriteAsset;
			materialReferences[count].material = material;
			materialReferences[count].isDefaultMaterial = true;
			materialReferences[count].referenceCount = 0;
			return count;
		}

		// Token: 0x04000025 RID: 37
		public int index;

		// Token: 0x04000026 RID: 38
		public TMP_FontAsset fontAsset;

		// Token: 0x04000027 RID: 39
		public TMP_SpriteAsset spriteAsset;

		// Token: 0x04000028 RID: 40
		public Material material;

		// Token: 0x04000029 RID: 41
		public bool isDefaultMaterial;

		// Token: 0x0400002A RID: 42
		public bool isFallbackMaterial;

		// Token: 0x0400002B RID: 43
		public Material fallbackMaterial;

		// Token: 0x0400002C RID: 44
		public float padding;

		// Token: 0x0400002D RID: 45
		public int referenceCount;
	}
}
