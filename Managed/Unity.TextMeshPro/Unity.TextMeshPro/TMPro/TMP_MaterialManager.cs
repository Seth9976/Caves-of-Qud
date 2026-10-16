using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200003B RID: 59
	public static class TMP_MaterialManager
	{
		// Token: 0x060002BA RID: 698 RVA: 0x0000FCD7 File Offset: 0x0000DED7
		static TMP_MaterialManager()
		{
			Canvas.willRenderCanvases += TMP_MaterialManager.OnPreRender;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000FD12 File Offset: 0x0000DF12
		private static void OnPreRender()
		{
			if (TMP_MaterialManager.isFallbackListDirty)
			{
				TMP_MaterialManager.CleanupFallbackMaterials();
				TMP_MaterialManager.isFallbackListDirty = false;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000FD28 File Offset: 0x0000DF28
		public static Material GetStencilMaterial(Material baseMaterial, int stencilID)
		{
			if (!baseMaterial.HasProperty(ShaderUtilities.ID_StencilID))
			{
				Debug.LogWarning("Selected Shader does not support Stencil Masking. Please select the Distance Field or Mobile Distance Field Shader.");
				return baseMaterial;
			}
			int instanceID = baseMaterial.GetInstanceID();
			for (int i = 0; i < TMP_MaterialManager.m_materialList.Count; i++)
			{
				if (TMP_MaterialManager.m_materialList[i].baseMaterial.GetInstanceID() == instanceID && TMP_MaterialManager.m_materialList[i].stencilID == stencilID)
				{
					TMP_MaterialManager.m_materialList[i].count++;
					return TMP_MaterialManager.m_materialList[i].stencilMaterial;
				}
			}
			Material material = new Material(baseMaterial);
			material.hideFlags = HideFlags.HideAndDontSave;
			material.shaderKeywords = baseMaterial.shaderKeywords;
			ShaderUtilities.GetShaderPropertyIDs();
			material.SetFloat(ShaderUtilities.ID_StencilID, (float)stencilID);
			material.SetFloat(ShaderUtilities.ID_StencilComp, 4f);
			TMP_MaterialManager.MaskingMaterial maskingMaterial = new TMP_MaterialManager.MaskingMaterial();
			maskingMaterial.baseMaterial = baseMaterial;
			maskingMaterial.stencilMaterial = material;
			maskingMaterial.stencilID = stencilID;
			maskingMaterial.count = 1;
			TMP_MaterialManager.m_materialList.Add(maskingMaterial);
			return material;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000FE2C File Offset: 0x0000E02C
		public static void ReleaseStencilMaterial(Material stencilMaterial)
		{
			int instanceID = stencilMaterial.GetInstanceID();
			int i = 0;
			while (i < TMP_MaterialManager.m_materialList.Count)
			{
				if (TMP_MaterialManager.m_materialList[i].stencilMaterial.GetInstanceID() == instanceID)
				{
					if (TMP_MaterialManager.m_materialList[i].count > 1)
					{
						TMP_MaterialManager.m_materialList[i].count--;
						return;
					}
					global::UnityEngine.Object.DestroyImmediate(TMP_MaterialManager.m_materialList[i].stencilMaterial);
					TMP_MaterialManager.m_materialList.RemoveAt(i);
					stencilMaterial = null;
					return;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000FEC0 File Offset: 0x0000E0C0
		public static Material GetBaseMaterial(Material stencilMaterial)
		{
			int num = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (num == -1)
			{
				return null;
			}
			return TMP_MaterialManager.m_materialList[num].baseMaterial;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000FF07 File Offset: 0x0000E107
		public static Material SetStencil(Material material, int stencilID)
		{
			material.SetFloat(ShaderUtilities.ID_StencilID, (float)stencilID);
			if (stencilID == 0)
			{
				material.SetFloat(ShaderUtilities.ID_StencilComp, 8f);
			}
			else
			{
				material.SetFloat(ShaderUtilities.ID_StencilComp, 4f);
			}
			return material;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000FF3C File Offset: 0x0000E13C
		public static void AddMaskingMaterial(Material baseMaterial, Material stencilMaterial, int stencilID)
		{
			int num = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (num == -1)
			{
				TMP_MaterialManager.MaskingMaterial maskingMaterial = new TMP_MaterialManager.MaskingMaterial();
				maskingMaterial.baseMaterial = baseMaterial;
				maskingMaterial.stencilMaterial = stencilMaterial;
				maskingMaterial.stencilID = stencilID;
				maskingMaterial.count = 1;
				TMP_MaterialManager.m_materialList.Add(maskingMaterial);
				return;
			}
			stencilMaterial = TMP_MaterialManager.m_materialList[num].stencilMaterial;
			TMP_MaterialManager.m_materialList[num].count++;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
		public static void RemoveStencilMaterial(Material stencilMaterial)
		{
			int num = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (num != -1)
			{
				TMP_MaterialManager.m_materialList.RemoveAt(num);
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00010014 File Offset: 0x0000E214
		public static void ReleaseBaseMaterial(Material baseMaterial)
		{
			int num = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.baseMaterial == baseMaterial);
			if (num == -1)
			{
				Debug.Log("No Masking Material exists for " + baseMaterial.name);
				return;
			}
			if (TMP_MaterialManager.m_materialList[num].count > 1)
			{
				TMP_MaterialManager.m_materialList[num].count--;
				Debug.Log(string.Concat(new string[]
				{
					"Removed (1) reference to ",
					TMP_MaterialManager.m_materialList[num].stencilMaterial.name,
					". There are ",
					TMP_MaterialManager.m_materialList[num].count.ToString(),
					" references left."
				}));
				return;
			}
			Debug.Log("Removed last reference to " + TMP_MaterialManager.m_materialList[num].stencilMaterial.name + " with ID " + TMP_MaterialManager.m_materialList[num].stencilMaterial.GetInstanceID().ToString());
			global::UnityEngine.Object.DestroyImmediate(TMP_MaterialManager.m_materialList[num].stencilMaterial);
			TMP_MaterialManager.m_materialList.RemoveAt(num);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00010150 File Offset: 0x0000E350
		public static void ClearMaterials()
		{
			if (TMP_MaterialManager.m_materialList.Count == 0)
			{
				Debug.Log("Material List has already been cleared.");
				return;
			}
			for (int i = 0; i < TMP_MaterialManager.m_materialList.Count; i++)
			{
				global::UnityEngine.Object.DestroyImmediate(TMP_MaterialManager.m_materialList[i].stencilMaterial);
			}
			TMP_MaterialManager.m_materialList.Clear();
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000101A8 File Offset: 0x0000E3A8
		public static int GetStencilID(GameObject obj)
		{
			int num = 0;
			Transform transform = obj.transform;
			Transform transform2 = TMP_MaterialManager.FindRootSortOverrideCanvas(transform);
			if (transform == transform2)
			{
				return num;
			}
			Transform transform3 = transform.parent;
			List<Mask> list = TMP_ListPool<Mask>.Get();
			while (transform3 != null)
			{
				transform3.GetComponents<Mask>(list);
				for (int i = 0; i < list.Count; i++)
				{
					Mask mask = list[i];
					if (mask != null && mask.MaskEnabled() && mask.graphic.IsActive())
					{
						num++;
						break;
					}
				}
				if (transform3 == transform2)
				{
					break;
				}
				transform3 = transform3.parent;
			}
			TMP_ListPool<Mask>.Release(list);
			return Mathf.Min((1 << num) - 1, 255);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00010264 File Offset: 0x0000E464
		public static Material GetMaterialForRendering(MaskableGraphic graphic, Material baseMaterial)
		{
			if (baseMaterial == null)
			{
				return null;
			}
			List<IMaterialModifier> list = TMP_ListPool<IMaterialModifier>.Get();
			graphic.GetComponents<IMaterialModifier>(list);
			Material material = baseMaterial;
			for (int i = 0; i < list.Count; i++)
			{
				material = list[i].GetModifiedMaterial(material);
			}
			TMP_ListPool<IMaterialModifier>.Release(list);
			return material;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000102B4 File Offset: 0x0000E4B4
		private static Transform FindRootSortOverrideCanvas(Transform start)
		{
			List<Canvas> list = TMP_ListPool<Canvas>.Get();
			start.GetComponentsInParent<Canvas>(false, list);
			Canvas canvas = null;
			for (int i = 0; i < list.Count; i++)
			{
				canvas = list[i];
				if (canvas.overrideSorting)
				{
					break;
				}
			}
			TMP_ListPool<Canvas>.Release(list);
			if (!(canvas != null))
			{
				return null;
			}
			return canvas.transform;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0001030C File Offset: 0x0000E50C
		internal static Material GetFallbackMaterial(TMP_FontAsset fontAsset, Material sourceMaterial, int atlasIndex)
		{
			long instanceID = (long)sourceMaterial.GetInstanceID();
			Texture texture = fontAsset.atlasTextures[atlasIndex];
			int instanceID2 = texture.GetInstanceID();
			long num = (instanceID << 32) | (long)((ulong)instanceID2);
			TMP_MaterialManager.FallbackMaterial fallbackMaterial;
			if (!TMP_MaterialManager.m_fallbackMaterials.TryGetValue(num, out fallbackMaterial))
			{
				Material material = new Material(sourceMaterial);
				material.SetTexture(ShaderUtilities.ID_MainTex, texture);
				material.hideFlags = HideFlags.HideAndDontSave;
				fallbackMaterial = new TMP_MaterialManager.FallbackMaterial();
				fallbackMaterial.fallbackID = num;
				fallbackMaterial.sourceMaterial = fontAsset.material;
				fallbackMaterial.sourceMaterialCRC = sourceMaterial.ComputeCRC();
				fallbackMaterial.fallbackMaterial = material;
				fallbackMaterial.count = 0;
				TMP_MaterialManager.m_fallbackMaterials.Add(num, fallbackMaterial);
				TMP_MaterialManager.m_fallbackMaterialLookup.Add(material.GetInstanceID(), num);
				return material;
			}
			int num2 = sourceMaterial.ComputeCRC();
			if (num2 == fallbackMaterial.sourceMaterialCRC)
			{
				return fallbackMaterial.fallbackMaterial;
			}
			TMP_MaterialManager.CopyMaterialPresetProperties(sourceMaterial, fallbackMaterial.fallbackMaterial);
			fallbackMaterial.sourceMaterialCRC = num2;
			return fallbackMaterial.fallbackMaterial;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000103EC File Offset: 0x0000E5EC
		public static Material GetFallbackMaterial(Material sourceMaterial, Material targetMaterial)
		{
			long instanceID = (long)sourceMaterial.GetInstanceID();
			Texture texture = targetMaterial.GetTexture(ShaderUtilities.ID_MainTex);
			int instanceID2 = texture.GetInstanceID();
			long num = (instanceID << 32) | (long)((ulong)instanceID2);
			TMP_MaterialManager.FallbackMaterial fallbackMaterial;
			if (!TMP_MaterialManager.m_fallbackMaterials.TryGetValue(num, out fallbackMaterial))
			{
				Material material;
				if (sourceMaterial.HasProperty(ShaderUtilities.ID_GradientScale) && targetMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
				{
					material = new Material(sourceMaterial);
					material.hideFlags = HideFlags.HideAndDontSave;
					material.SetTexture(ShaderUtilities.ID_MainTex, texture);
					material.SetFloat(ShaderUtilities.ID_GradientScale, targetMaterial.GetFloat(ShaderUtilities.ID_GradientScale));
					material.SetFloat(ShaderUtilities.ID_TextureWidth, targetMaterial.GetFloat(ShaderUtilities.ID_TextureWidth));
					material.SetFloat(ShaderUtilities.ID_TextureHeight, targetMaterial.GetFloat(ShaderUtilities.ID_TextureHeight));
					material.SetFloat(ShaderUtilities.ID_WeightNormal, targetMaterial.GetFloat(ShaderUtilities.ID_WeightNormal));
					material.SetFloat(ShaderUtilities.ID_WeightBold, targetMaterial.GetFloat(ShaderUtilities.ID_WeightBold));
				}
				else
				{
					material = new Material(targetMaterial);
					material.hideFlags = HideFlags.HideAndDontSave;
				}
				fallbackMaterial = new TMP_MaterialManager.FallbackMaterial();
				fallbackMaterial.fallbackID = num;
				fallbackMaterial.sourceMaterial = sourceMaterial;
				fallbackMaterial.sourceMaterialCRC = sourceMaterial.ComputeCRC();
				fallbackMaterial.fallbackMaterial = material;
				fallbackMaterial.count = 0;
				TMP_MaterialManager.m_fallbackMaterials.Add(num, fallbackMaterial);
				TMP_MaterialManager.m_fallbackMaterialLookup.Add(material.GetInstanceID(), num);
				return material;
			}
			int num2 = sourceMaterial.ComputeCRC();
			if (num2 == fallbackMaterial.sourceMaterialCRC)
			{
				return fallbackMaterial.fallbackMaterial;
			}
			TMP_MaterialManager.CopyMaterialPresetProperties(sourceMaterial, fallbackMaterial.fallbackMaterial);
			fallbackMaterial.sourceMaterialCRC = num2;
			return fallbackMaterial.fallbackMaterial;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00010570 File Offset: 0x0000E770
		public static void AddFallbackMaterialReference(Material targetMaterial)
		{
			if (targetMaterial == null)
			{
				return;
			}
			int instanceID = targetMaterial.GetInstanceID();
			long num;
			TMP_MaterialManager.FallbackMaterial fallbackMaterial;
			if (TMP_MaterialManager.m_fallbackMaterialLookup.TryGetValue(instanceID, out num) && TMP_MaterialManager.m_fallbackMaterials.TryGetValue(num, out fallbackMaterial))
			{
				fallbackMaterial.count++;
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000105BC File Offset: 0x0000E7BC
		public static void RemoveFallbackMaterialReference(Material targetMaterial)
		{
			if (targetMaterial == null)
			{
				return;
			}
			int instanceID = targetMaterial.GetInstanceID();
			long num;
			TMP_MaterialManager.FallbackMaterial fallbackMaterial;
			if (TMP_MaterialManager.m_fallbackMaterialLookup.TryGetValue(instanceID, out num) && TMP_MaterialManager.m_fallbackMaterials.TryGetValue(num, out fallbackMaterial))
			{
				fallbackMaterial.count--;
				if (fallbackMaterial.count < 1)
				{
					TMP_MaterialManager.m_fallbackCleanupList.Add(fallbackMaterial);
				}
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001061C File Offset: 0x0000E81C
		public static void CleanupFallbackMaterials()
		{
			if (TMP_MaterialManager.m_fallbackCleanupList.Count == 0)
			{
				return;
			}
			for (int i = 0; i < TMP_MaterialManager.m_fallbackCleanupList.Count; i++)
			{
				TMP_MaterialManager.FallbackMaterial fallbackMaterial = TMP_MaterialManager.m_fallbackCleanupList[i];
				if (fallbackMaterial.count < 1)
				{
					Material fallbackMaterial2 = fallbackMaterial.fallbackMaterial;
					TMP_MaterialManager.m_fallbackMaterials.Remove(fallbackMaterial.fallbackID);
					TMP_MaterialManager.m_fallbackMaterialLookup.Remove(fallbackMaterial2.GetInstanceID());
					global::UnityEngine.Object.DestroyImmediate(fallbackMaterial2);
				}
			}
			TMP_MaterialManager.m_fallbackCleanupList.Clear();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0001069C File Offset: 0x0000E89C
		public static void ReleaseFallbackMaterial(Material fallbackMaterial)
		{
			if (fallbackMaterial == null)
			{
				return;
			}
			int instanceID = fallbackMaterial.GetInstanceID();
			long num;
			TMP_MaterialManager.FallbackMaterial fallbackMaterial2;
			if (TMP_MaterialManager.m_fallbackMaterialLookup.TryGetValue(instanceID, out num) && TMP_MaterialManager.m_fallbackMaterials.TryGetValue(num, out fallbackMaterial2))
			{
				fallbackMaterial2.count--;
				if (fallbackMaterial2.count < 1)
				{
					TMP_MaterialManager.m_fallbackCleanupList.Add(fallbackMaterial2);
				}
			}
			TMP_MaterialManager.isFallbackListDirty = true;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00010700 File Offset: 0x0000E900
		public static void CopyMaterialPresetProperties(Material source, Material destination)
		{
			if (!source.HasProperty(ShaderUtilities.ID_GradientScale) || !destination.HasProperty(ShaderUtilities.ID_GradientScale))
			{
				return;
			}
			Texture texture = destination.GetTexture(ShaderUtilities.ID_MainTex);
			float @float = destination.GetFloat(ShaderUtilities.ID_GradientScale);
			float float2 = destination.GetFloat(ShaderUtilities.ID_TextureWidth);
			float float3 = destination.GetFloat(ShaderUtilities.ID_TextureHeight);
			float float4 = destination.GetFloat(ShaderUtilities.ID_WeightNormal);
			float float5 = destination.GetFloat(ShaderUtilities.ID_WeightBold);
			destination.shader = source.shader;
			destination.CopyPropertiesFromMaterial(source);
			destination.shaderKeywords = source.shaderKeywords;
			destination.SetTexture(ShaderUtilities.ID_MainTex, texture);
			destination.SetFloat(ShaderUtilities.ID_GradientScale, @float);
			destination.SetFloat(ShaderUtilities.ID_TextureWidth, float2);
			destination.SetFloat(ShaderUtilities.ID_TextureHeight, float3);
			destination.SetFloat(ShaderUtilities.ID_WeightNormal, float4);
			destination.SetFloat(ShaderUtilities.ID_WeightBold, float5);
		}

		// Token: 0x040001BE RID: 446
		private static List<TMP_MaterialManager.MaskingMaterial> m_materialList = new List<TMP_MaterialManager.MaskingMaterial>();

		// Token: 0x040001BF RID: 447
		private static Dictionary<long, TMP_MaterialManager.FallbackMaterial> m_fallbackMaterials = new Dictionary<long, TMP_MaterialManager.FallbackMaterial>();

		// Token: 0x040001C0 RID: 448
		private static Dictionary<int, long> m_fallbackMaterialLookup = new Dictionary<int, long>();

		// Token: 0x040001C1 RID: 449
		private static List<TMP_MaterialManager.FallbackMaterial> m_fallbackCleanupList = new List<TMP_MaterialManager.FallbackMaterial>();

		// Token: 0x040001C2 RID: 450
		private static bool isFallbackListDirty;

		// Token: 0x020000AA RID: 170
		private class FallbackMaterial
		{
			// Token: 0x04000660 RID: 1632
			public long fallbackID;

			// Token: 0x04000661 RID: 1633
			public Material sourceMaterial;

			// Token: 0x04000662 RID: 1634
			internal int sourceMaterialCRC;

			// Token: 0x04000663 RID: 1635
			public Material fallbackMaterial;

			// Token: 0x04000664 RID: 1636
			public int count;
		}

		// Token: 0x020000AB RID: 171
		private class MaskingMaterial
		{
			// Token: 0x04000665 RID: 1637
			public Material baseMaterial;

			// Token: 0x04000666 RID: 1638
			public Material stencilMaterial;

			// Token: 0x04000667 RID: 1639
			public int count;

			// Token: 0x04000668 RID: 1640
			public int stencilID;
		}
	}
}
