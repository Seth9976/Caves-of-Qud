using System;
using System.Linq;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000047 RID: 71
	public static class ShaderUtilities
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00012454 File Offset: 0x00010654
		internal static Shader ShaderRef_MobileSDF
		{
			get
			{
				if (ShaderUtilities.k_ShaderRef_MobileSDF == null)
				{
					ShaderUtilities.k_ShaderRef_MobileSDF = Shader.Find("TextMeshPro/Mobile/Distance Field");
				}
				return ShaderUtilities.k_ShaderRef_MobileSDF;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00012477 File Offset: 0x00010677
		internal static Shader ShaderRef_MobileBitmap
		{
			get
			{
				if (ShaderUtilities.k_ShaderRef_MobileBitmap == null)
				{
					ShaderUtilities.k_ShaderRef_MobileBitmap = Shader.Find("TextMeshPro/Mobile/Bitmap");
				}
				return ShaderUtilities.k_ShaderRef_MobileBitmap;
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001249C File Offset: 0x0001069C
		static ShaderUtilities()
		{
			ShaderUtilities.GetShaderPropertyIDs();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00012524 File Offset: 0x00010724
		public static void GetShaderPropertyIDs()
		{
			if (!ShaderUtilities.isInitialized)
			{
				ShaderUtilities.isInitialized = true;
				ShaderUtilities.ID_MainTex = Shader.PropertyToID("_MainTex");
				ShaderUtilities.ID_FaceTex = Shader.PropertyToID("_FaceTex");
				ShaderUtilities.ID_FaceColor = Shader.PropertyToID("_FaceColor");
				ShaderUtilities.ID_FaceDilate = Shader.PropertyToID("_FaceDilate");
				ShaderUtilities.ID_Shininess = Shader.PropertyToID("_FaceShininess");
				ShaderUtilities.ID_OutlineOffset1 = Shader.PropertyToID("_OutlineOffset1");
				ShaderUtilities.ID_OutlineOffset2 = Shader.PropertyToID("_OutlineOffset2");
				ShaderUtilities.ID_OutlineOffset3 = Shader.PropertyToID("_OutlineOffset3");
				ShaderUtilities.ID_OutlineMode = Shader.PropertyToID("_OutlineMode");
				ShaderUtilities.ID_IsoPerimeter = Shader.PropertyToID("_IsoPerimeter");
				ShaderUtilities.ID_Softness = Shader.PropertyToID("_Softness");
				ShaderUtilities.ID_UnderlayColor = Shader.PropertyToID("_UnderlayColor");
				ShaderUtilities.ID_UnderlayOffsetX = Shader.PropertyToID("_UnderlayOffsetX");
				ShaderUtilities.ID_UnderlayOffsetY = Shader.PropertyToID("_UnderlayOffsetY");
				ShaderUtilities.ID_UnderlayDilate = Shader.PropertyToID("_UnderlayDilate");
				ShaderUtilities.ID_UnderlaySoftness = Shader.PropertyToID("_UnderlaySoftness");
				ShaderUtilities.ID_UnderlayOffset = Shader.PropertyToID("_UnderlayOffset");
				ShaderUtilities.ID_UnderlayIsoPerimeter = Shader.PropertyToID("_UnderlayIsoPerimeter");
				ShaderUtilities.ID_WeightNormal = Shader.PropertyToID("_WeightNormal");
				ShaderUtilities.ID_WeightBold = Shader.PropertyToID("_WeightBold");
				ShaderUtilities.ID_OutlineTex = Shader.PropertyToID("_OutlineTex");
				ShaderUtilities.ID_OutlineWidth = Shader.PropertyToID("_OutlineWidth");
				ShaderUtilities.ID_OutlineSoftness = Shader.PropertyToID("_OutlineSoftness");
				ShaderUtilities.ID_OutlineColor = Shader.PropertyToID("_OutlineColor");
				ShaderUtilities.ID_Outline2Color = Shader.PropertyToID("_Outline2Color");
				ShaderUtilities.ID_Outline2Width = Shader.PropertyToID("_Outline2Width");
				ShaderUtilities.ID_Padding = Shader.PropertyToID("_Padding");
				ShaderUtilities.ID_GradientScale = Shader.PropertyToID("_GradientScale");
				ShaderUtilities.ID_ScaleX = Shader.PropertyToID("_ScaleX");
				ShaderUtilities.ID_ScaleY = Shader.PropertyToID("_ScaleY");
				ShaderUtilities.ID_PerspectiveFilter = Shader.PropertyToID("_PerspectiveFilter");
				ShaderUtilities.ID_Sharpness = Shader.PropertyToID("_Sharpness");
				ShaderUtilities.ID_TextureWidth = Shader.PropertyToID("_TextureWidth");
				ShaderUtilities.ID_TextureHeight = Shader.PropertyToID("_TextureHeight");
				ShaderUtilities.ID_BevelAmount = Shader.PropertyToID("_Bevel");
				ShaderUtilities.ID_LightAngle = Shader.PropertyToID("_LightAngle");
				ShaderUtilities.ID_EnvMap = Shader.PropertyToID("_Cube");
				ShaderUtilities.ID_EnvMatrix = Shader.PropertyToID("_EnvMatrix");
				ShaderUtilities.ID_EnvMatrixRotation = Shader.PropertyToID("_EnvMatrixRotation");
				ShaderUtilities.ID_GlowColor = Shader.PropertyToID("_GlowColor");
				ShaderUtilities.ID_GlowOffset = Shader.PropertyToID("_GlowOffset");
				ShaderUtilities.ID_GlowPower = Shader.PropertyToID("_GlowPower");
				ShaderUtilities.ID_GlowOuter = Shader.PropertyToID("_GlowOuter");
				ShaderUtilities.ID_GlowInner = Shader.PropertyToID("_GlowInner");
				ShaderUtilities.ID_MaskCoord = Shader.PropertyToID("_MaskCoord");
				ShaderUtilities.ID_ClipRect = Shader.PropertyToID("_ClipRect");
				ShaderUtilities.ID_UseClipRect = Shader.PropertyToID("_UseClipRect");
				ShaderUtilities.ID_MaskSoftnessX = Shader.PropertyToID("_MaskSoftnessX");
				ShaderUtilities.ID_MaskSoftnessY = Shader.PropertyToID("_MaskSoftnessY");
				ShaderUtilities.ID_VertexOffsetX = Shader.PropertyToID("_VertexOffsetX");
				ShaderUtilities.ID_VertexOffsetY = Shader.PropertyToID("_VertexOffsetY");
				ShaderUtilities.ID_StencilID = Shader.PropertyToID("_Stencil");
				ShaderUtilities.ID_StencilOp = Shader.PropertyToID("_StencilOp");
				ShaderUtilities.ID_StencilComp = Shader.PropertyToID("_StencilComp");
				ShaderUtilities.ID_StencilReadMask = Shader.PropertyToID("_StencilReadMask");
				ShaderUtilities.ID_StencilWriteMask = Shader.PropertyToID("_StencilWriteMask");
				ShaderUtilities.ID_ShaderFlags = Shader.PropertyToID("_ShaderFlags");
				ShaderUtilities.ID_ScaleRatio_A = Shader.PropertyToID("_ScaleRatioA");
				ShaderUtilities.ID_ScaleRatio_B = Shader.PropertyToID("_ScaleRatioB");
				ShaderUtilities.ID_ScaleRatio_C = Shader.PropertyToID("_ScaleRatioC");
				if (ShaderUtilities.k_ShaderRef_MobileSDF == null)
				{
					ShaderUtilities.k_ShaderRef_MobileSDF = Shader.Find("TextMeshPro/Mobile/Distance Field");
				}
				if (ShaderUtilities.k_ShaderRef_MobileBitmap == null)
				{
					ShaderUtilities.k_ShaderRef_MobileBitmap = Shader.Find("TextMeshPro/Mobile/Bitmap");
				}
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00012900 File Offset: 0x00010B00
		public static void UpdateShaderRatios(Material mat)
		{
			bool flag = !mat.shaderKeywords.Contains(ShaderUtilities.Keyword_Ratios);
			if (!mat.HasProperty(ShaderUtilities.ID_GradientScale) || !mat.HasProperty(ShaderUtilities.ID_FaceDilate))
			{
				return;
			}
			float @float = mat.GetFloat(ShaderUtilities.ID_GradientScale);
			float float2 = mat.GetFloat(ShaderUtilities.ID_FaceDilate);
			float float3 = mat.GetFloat(ShaderUtilities.ID_OutlineWidth);
			float float4 = mat.GetFloat(ShaderUtilities.ID_OutlineSoftness);
			float num = Mathf.Max(mat.GetFloat(ShaderUtilities.ID_WeightNormal), mat.GetFloat(ShaderUtilities.ID_WeightBold)) / 4f;
			float num2 = Mathf.Max(1f, num + float2 + float3 + float4);
			float num3 = (flag ? ((@float - ShaderUtilities.m_clamp) / (@float * num2)) : 1f);
			mat.SetFloat(ShaderUtilities.ID_ScaleRatio_A, num3);
			if (mat.HasProperty(ShaderUtilities.ID_GlowOffset))
			{
				float float5 = mat.GetFloat(ShaderUtilities.ID_GlowOffset);
				float float6 = mat.GetFloat(ShaderUtilities.ID_GlowOuter);
				float num4 = (num + float2) * (@float - ShaderUtilities.m_clamp);
				num2 = Mathf.Max(1f, float5 + float6);
				float num5 = (flag ? (Mathf.Max(0f, @float - ShaderUtilities.m_clamp - num4) / (@float * num2)) : 1f);
				mat.SetFloat(ShaderUtilities.ID_ScaleRatio_B, num5);
			}
			if (mat.HasProperty(ShaderUtilities.ID_UnderlayOffsetX))
			{
				float float7 = mat.GetFloat(ShaderUtilities.ID_UnderlayOffsetX);
				float float8 = mat.GetFloat(ShaderUtilities.ID_UnderlayOffsetY);
				float float9 = mat.GetFloat(ShaderUtilities.ID_UnderlayDilate);
				float float10 = mat.GetFloat(ShaderUtilities.ID_UnderlaySoftness);
				float num6 = (num + float2) * (@float - ShaderUtilities.m_clamp);
				num2 = Mathf.Max(1f, Mathf.Max(Mathf.Abs(float7), Mathf.Abs(float8)) + float9 + float10);
				float num7 = (flag ? (Mathf.Max(0f, @float - ShaderUtilities.m_clamp - num6) / (@float * num2)) : 1f);
				mat.SetFloat(ShaderUtilities.ID_ScaleRatio_C, num7);
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00012B06 File Offset: 0x00010D06
		public static Vector4 GetFontExtent(Material material)
		{
			return Vector4.zero;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00012B10 File Offset: 0x00010D10
		public static bool IsMaskingEnabled(Material material)
		{
			return !(material == null) && material.HasProperty(ShaderUtilities.ID_ClipRect) && (material.shaderKeywords.Contains(ShaderUtilities.Keyword_MASK_SOFT) || material.shaderKeywords.Contains(ShaderUtilities.Keyword_MASK_HARD) || material.shaderKeywords.Contains(ShaderUtilities.Keyword_MASK_TEX));
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00012B70 File Offset: 0x00010D70
		public static float GetPadding(Material material, bool enableExtraPadding, bool isBold)
		{
			if (!ShaderUtilities.isInitialized)
			{
				ShaderUtilities.GetShaderPropertyIDs();
			}
			if (material == null)
			{
				return 0f;
			}
			int num = (enableExtraPadding ? 4 : 0);
			if (!material.HasProperty(ShaderUtilities.ID_GradientScale))
			{
				if (material.HasProperty(ShaderUtilities.ID_Padding))
				{
					num += (int)material.GetFloat(ShaderUtilities.ID_Padding);
				}
				return (float)num + 1f;
			}
			if (material.HasProperty(ShaderUtilities.ID_IsoPerimeter))
			{
				return ShaderUtilities.ComputePaddingForProperties(material) + 0.25f + (float)num;
			}
			Vector4 vector = Vector4.zero;
			Vector4 zero = Vector4.zero;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			float num8 = 0f;
			float num9 = 0f;
			ShaderUtilities.UpdateShaderRatios(material);
			string[] shaderKeywords = material.shaderKeywords;
			if (material.HasProperty(ShaderUtilities.ID_ScaleRatio_A))
			{
				num5 = material.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
			}
			if (material.HasProperty(ShaderUtilities.ID_FaceDilate))
			{
				num2 = material.GetFloat(ShaderUtilities.ID_FaceDilate) * num5;
			}
			if (material.HasProperty(ShaderUtilities.ID_OutlineSoftness))
			{
				num3 = material.GetFloat(ShaderUtilities.ID_OutlineSoftness) * num5;
			}
			if (material.HasProperty(ShaderUtilities.ID_OutlineWidth))
			{
				num4 = material.GetFloat(ShaderUtilities.ID_OutlineWidth) * num5;
			}
			float num10 = num4 + num3 + num2;
			if (material.HasProperty(ShaderUtilities.ID_GlowOffset) && shaderKeywords.Contains(ShaderUtilities.Keyword_Glow))
			{
				if (material.HasProperty(ShaderUtilities.ID_ScaleRatio_B))
				{
					num6 = material.GetFloat(ShaderUtilities.ID_ScaleRatio_B);
				}
				num8 = material.GetFloat(ShaderUtilities.ID_GlowOffset) * num6;
				num9 = material.GetFloat(ShaderUtilities.ID_GlowOuter) * num6;
			}
			num10 = Mathf.Max(num10, num2 + num8 + num9);
			if (material.HasProperty(ShaderUtilities.ID_UnderlaySoftness) && shaderKeywords.Contains(ShaderUtilities.Keyword_Underlay))
			{
				if (material.HasProperty(ShaderUtilities.ID_ScaleRatio_C))
				{
					num7 = material.GetFloat(ShaderUtilities.ID_ScaleRatio_C);
				}
				float num11 = 0f;
				float num12 = 0f;
				float num13 = 0f;
				float num14 = 0f;
				if (material.HasProperty(ShaderUtilities.ID_UnderlayOffset))
				{
					Vector2 vector2 = material.GetVector(ShaderUtilities.ID_UnderlayOffset);
					num11 = vector2.x;
					num12 = vector2.y;
					num13 = material.GetFloat(ShaderUtilities.ID_UnderlayDilate);
					num14 = material.GetFloat(ShaderUtilities.ID_UnderlaySoftness);
				}
				else if (material.HasProperty(ShaderUtilities.ID_UnderlayOffsetX))
				{
					num11 = material.GetFloat(ShaderUtilities.ID_UnderlayOffsetX) * num7;
					num12 = material.GetFloat(ShaderUtilities.ID_UnderlayOffsetY) * num7;
					num13 = material.GetFloat(ShaderUtilities.ID_UnderlayDilate) * num7;
					num14 = material.GetFloat(ShaderUtilities.ID_UnderlaySoftness) * num7;
				}
				vector.x = Mathf.Max(vector.x, num2 + num13 + num14 - num11);
				vector.y = Mathf.Max(vector.y, num2 + num13 + num14 - num12);
				vector.z = Mathf.Max(vector.z, num2 + num13 + num14 + num11);
				vector.w = Mathf.Max(vector.w, num2 + num13 + num14 + num12);
			}
			vector.x = Mathf.Max(vector.x, num10);
			vector.y = Mathf.Max(vector.y, num10);
			vector.z = Mathf.Max(vector.z, num10);
			vector.w = Mathf.Max(vector.w, num10);
			vector.x += (float)num;
			vector.y += (float)num;
			vector.z += (float)num;
			vector.w += (float)num;
			vector.x = Mathf.Min(vector.x, 1f);
			vector.y = Mathf.Min(vector.y, 1f);
			vector.z = Mathf.Min(vector.z, 1f);
			vector.w = Mathf.Min(vector.w, 1f);
			zero.x = ((zero.x < vector.x) ? vector.x : zero.x);
			zero.y = ((zero.y < vector.y) ? vector.y : zero.y);
			zero.z = ((zero.z < vector.z) ? vector.z : zero.z);
			zero.w = ((zero.w < vector.w) ? vector.w : zero.w);
			float @float = material.GetFloat(ShaderUtilities.ID_GradientScale);
			vector *= @float;
			num10 = Mathf.Max(vector.x, vector.y);
			num10 = Mathf.Max(vector.z, num10);
			num10 = Mathf.Max(vector.w, num10);
			return num10 + 1.25f;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00013044 File Offset: 0x00011244
		private static float ComputePaddingForProperties(Material mat)
		{
			Vector4 vector = mat.GetVector(ShaderUtilities.ID_IsoPerimeter);
			Vector2 vector2 = mat.GetVector(ShaderUtilities.ID_OutlineOffset1);
			Vector2 vector3 = mat.GetVector(ShaderUtilities.ID_OutlineOffset2);
			Vector2 vector4 = mat.GetVector(ShaderUtilities.ID_OutlineOffset3);
			bool flag = mat.GetFloat(ShaderUtilities.ID_OutlineMode) != 0f;
			Vector4 vector5 = mat.GetVector(ShaderUtilities.ID_Softness);
			float @float = mat.GetFloat(ShaderUtilities.ID_GradientScale);
			float num = Mathf.Max(0f, vector.x + vector5.x * 0.5f);
			if (!flag)
			{
				num = Mathf.Max(num, vector.y + vector5.y * 0.5f + Mathf.Max(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y)));
				num = Mathf.Max(num, vector.z + vector5.z * 0.5f + Mathf.Max(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y)));
				num = Mathf.Max(num, vector.w + vector5.w * 0.5f + Mathf.Max(Mathf.Abs(vector4.x), Mathf.Abs(vector4.y)));
			}
			else
			{
				float num2 = Mathf.Max(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y));
				float num3 = Mathf.Max(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y));
				num = Mathf.Max(num, vector.y + vector5.y * 0.5f + num2);
				num = Mathf.Max(num, vector.z + vector5.z * 0.5f + num3);
				float num4 = Mathf.Max(num2, num3);
				num += Mathf.Max(0f, vector.w + vector5.w * 0.5f - Mathf.Max(0f, num - num4));
			}
			Vector2 vector6 = mat.GetVector(ShaderUtilities.ID_UnderlayOffset);
			float float2 = mat.GetFloat(ShaderUtilities.ID_UnderlayDilate);
			float float3 = mat.GetFloat(ShaderUtilities.ID_UnderlaySoftness);
			num = Mathf.Max(num, float2 + float3 * 0.5f + Mathf.Max(Mathf.Abs(vector6.x), Mathf.Abs(vector6.y)));
			return num * @float;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000132B0 File Offset: 0x000114B0
		public static float GetPadding(Material[] materials, bool enableExtraPadding, bool isBold)
		{
			if (!ShaderUtilities.isInitialized)
			{
				ShaderUtilities.GetShaderPropertyIDs();
			}
			if (materials == null)
			{
				return 0f;
			}
			int num = (enableExtraPadding ? 4 : 0);
			if (materials[0].HasProperty(ShaderUtilities.ID_Padding))
			{
				return (float)num + materials[0].GetFloat(ShaderUtilities.ID_Padding);
			}
			Vector4 vector = Vector4.zero;
			Vector4 zero = Vector4.zero;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			float num8 = 0f;
			float num9 = 0f;
			float num10;
			for (int i = 0; i < materials.Length; i++)
			{
				ShaderUtilities.UpdateShaderRatios(materials[i]);
				string[] shaderKeywords = materials[i].shaderKeywords;
				if (materials[i].HasProperty(ShaderUtilities.ID_ScaleRatio_A))
				{
					num5 = materials[i].GetFloat(ShaderUtilities.ID_ScaleRatio_A);
				}
				if (materials[i].HasProperty(ShaderUtilities.ID_FaceDilate))
				{
					num2 = materials[i].GetFloat(ShaderUtilities.ID_FaceDilate) * num5;
				}
				if (materials[i].HasProperty(ShaderUtilities.ID_OutlineSoftness))
				{
					num3 = materials[i].GetFloat(ShaderUtilities.ID_OutlineSoftness) * num5;
				}
				if (materials[i].HasProperty(ShaderUtilities.ID_OutlineWidth))
				{
					num4 = materials[i].GetFloat(ShaderUtilities.ID_OutlineWidth) * num5;
				}
				num10 = num4 + num3 + num2;
				if (materials[i].HasProperty(ShaderUtilities.ID_GlowOffset) && shaderKeywords.Contains(ShaderUtilities.Keyword_Glow))
				{
					if (materials[i].HasProperty(ShaderUtilities.ID_ScaleRatio_B))
					{
						num6 = materials[i].GetFloat(ShaderUtilities.ID_ScaleRatio_B);
					}
					num8 = materials[i].GetFloat(ShaderUtilities.ID_GlowOffset) * num6;
					num9 = materials[i].GetFloat(ShaderUtilities.ID_GlowOuter) * num6;
				}
				num10 = Mathf.Max(num10, num2 + num8 + num9);
				if (materials[i].HasProperty(ShaderUtilities.ID_UnderlaySoftness) && shaderKeywords.Contains(ShaderUtilities.Keyword_Underlay))
				{
					if (materials[i].HasProperty(ShaderUtilities.ID_ScaleRatio_C))
					{
						num7 = materials[i].GetFloat(ShaderUtilities.ID_ScaleRatio_C);
					}
					float num11 = materials[i].GetFloat(ShaderUtilities.ID_UnderlayOffsetX) * num7;
					float num12 = materials[i].GetFloat(ShaderUtilities.ID_UnderlayOffsetY) * num7;
					float num13 = materials[i].GetFloat(ShaderUtilities.ID_UnderlayDilate) * num7;
					float num14 = materials[i].GetFloat(ShaderUtilities.ID_UnderlaySoftness) * num7;
					vector.x = Mathf.Max(vector.x, num2 + num13 + num14 - num11);
					vector.y = Mathf.Max(vector.y, num2 + num13 + num14 - num12);
					vector.z = Mathf.Max(vector.z, num2 + num13 + num14 + num11);
					vector.w = Mathf.Max(vector.w, num2 + num13 + num14 + num12);
				}
				vector.x = Mathf.Max(vector.x, num10);
				vector.y = Mathf.Max(vector.y, num10);
				vector.z = Mathf.Max(vector.z, num10);
				vector.w = Mathf.Max(vector.w, num10);
				vector.x += (float)num;
				vector.y += (float)num;
				vector.z += (float)num;
				vector.w += (float)num;
				vector.x = Mathf.Min(vector.x, 1f);
				vector.y = Mathf.Min(vector.y, 1f);
				vector.z = Mathf.Min(vector.z, 1f);
				vector.w = Mathf.Min(vector.w, 1f);
				zero.x = ((zero.x < vector.x) ? vector.x : zero.x);
				zero.y = ((zero.y < vector.y) ? vector.y : zero.y);
				zero.z = ((zero.z < vector.z) ? vector.z : zero.z);
				zero.w = ((zero.w < vector.w) ? vector.w : zero.w);
			}
			float @float = materials[0].GetFloat(ShaderUtilities.ID_GradientScale);
			vector *= @float;
			num10 = Mathf.Max(vector.x, vector.y);
			num10 = Mathf.Max(vector.z, num10);
			num10 = Mathf.Max(vector.w, num10);
			return num10 + 0.25f;
		}

		// Token: 0x040002B3 RID: 691
		public static int ID_MainTex;

		// Token: 0x040002B4 RID: 692
		public static int ID_FaceTex;

		// Token: 0x040002B5 RID: 693
		public static int ID_FaceColor;

		// Token: 0x040002B6 RID: 694
		public static int ID_FaceDilate;

		// Token: 0x040002B7 RID: 695
		public static int ID_Shininess;

		// Token: 0x040002B8 RID: 696
		public static int ID_OutlineOffset1;

		// Token: 0x040002B9 RID: 697
		public static int ID_OutlineOffset2;

		// Token: 0x040002BA RID: 698
		public static int ID_OutlineOffset3;

		// Token: 0x040002BB RID: 699
		public static int ID_OutlineMode;

		// Token: 0x040002BC RID: 700
		public static int ID_IsoPerimeter;

		// Token: 0x040002BD RID: 701
		public static int ID_Softness;

		// Token: 0x040002BE RID: 702
		public static int ID_UnderlayColor;

		// Token: 0x040002BF RID: 703
		public static int ID_UnderlayOffsetX;

		// Token: 0x040002C0 RID: 704
		public static int ID_UnderlayOffsetY;

		// Token: 0x040002C1 RID: 705
		public static int ID_UnderlayDilate;

		// Token: 0x040002C2 RID: 706
		public static int ID_UnderlaySoftness;

		// Token: 0x040002C3 RID: 707
		public static int ID_UnderlayOffset;

		// Token: 0x040002C4 RID: 708
		public static int ID_UnderlayIsoPerimeter;

		// Token: 0x040002C5 RID: 709
		public static int ID_WeightNormal;

		// Token: 0x040002C6 RID: 710
		public static int ID_WeightBold;

		// Token: 0x040002C7 RID: 711
		public static int ID_OutlineTex;

		// Token: 0x040002C8 RID: 712
		public static int ID_OutlineWidth;

		// Token: 0x040002C9 RID: 713
		public static int ID_OutlineSoftness;

		// Token: 0x040002CA RID: 714
		public static int ID_OutlineColor;

		// Token: 0x040002CB RID: 715
		public static int ID_Outline2Color;

		// Token: 0x040002CC RID: 716
		public static int ID_Outline2Width;

		// Token: 0x040002CD RID: 717
		public static int ID_Padding;

		// Token: 0x040002CE RID: 718
		public static int ID_GradientScale;

		// Token: 0x040002CF RID: 719
		public static int ID_ScaleX;

		// Token: 0x040002D0 RID: 720
		public static int ID_ScaleY;

		// Token: 0x040002D1 RID: 721
		public static int ID_PerspectiveFilter;

		// Token: 0x040002D2 RID: 722
		public static int ID_Sharpness;

		// Token: 0x040002D3 RID: 723
		public static int ID_TextureWidth;

		// Token: 0x040002D4 RID: 724
		public static int ID_TextureHeight;

		// Token: 0x040002D5 RID: 725
		public static int ID_BevelAmount;

		// Token: 0x040002D6 RID: 726
		public static int ID_GlowColor;

		// Token: 0x040002D7 RID: 727
		public static int ID_GlowOffset;

		// Token: 0x040002D8 RID: 728
		public static int ID_GlowPower;

		// Token: 0x040002D9 RID: 729
		public static int ID_GlowOuter;

		// Token: 0x040002DA RID: 730
		public static int ID_GlowInner;

		// Token: 0x040002DB RID: 731
		public static int ID_LightAngle;

		// Token: 0x040002DC RID: 732
		public static int ID_EnvMap;

		// Token: 0x040002DD RID: 733
		public static int ID_EnvMatrix;

		// Token: 0x040002DE RID: 734
		public static int ID_EnvMatrixRotation;

		// Token: 0x040002DF RID: 735
		public static int ID_MaskCoord;

		// Token: 0x040002E0 RID: 736
		public static int ID_ClipRect;

		// Token: 0x040002E1 RID: 737
		public static int ID_MaskSoftnessX;

		// Token: 0x040002E2 RID: 738
		public static int ID_MaskSoftnessY;

		// Token: 0x040002E3 RID: 739
		public static int ID_VertexOffsetX;

		// Token: 0x040002E4 RID: 740
		public static int ID_VertexOffsetY;

		// Token: 0x040002E5 RID: 741
		public static int ID_UseClipRect;

		// Token: 0x040002E6 RID: 742
		public static int ID_StencilID;

		// Token: 0x040002E7 RID: 743
		public static int ID_StencilOp;

		// Token: 0x040002E8 RID: 744
		public static int ID_StencilComp;

		// Token: 0x040002E9 RID: 745
		public static int ID_StencilReadMask;

		// Token: 0x040002EA RID: 746
		public static int ID_StencilWriteMask;

		// Token: 0x040002EB RID: 747
		public static int ID_ShaderFlags;

		// Token: 0x040002EC RID: 748
		public static int ID_ScaleRatio_A;

		// Token: 0x040002ED RID: 749
		public static int ID_ScaleRatio_B;

		// Token: 0x040002EE RID: 750
		public static int ID_ScaleRatio_C;

		// Token: 0x040002EF RID: 751
		public static string Keyword_Bevel = "BEVEL_ON";

		// Token: 0x040002F0 RID: 752
		public static string Keyword_Glow = "GLOW_ON";

		// Token: 0x040002F1 RID: 753
		public static string Keyword_Underlay = "UNDERLAY_ON";

		// Token: 0x040002F2 RID: 754
		public static string Keyword_Ratios = "RATIOS_OFF";

		// Token: 0x040002F3 RID: 755
		public static string Keyword_MASK_SOFT = "MASK_SOFT";

		// Token: 0x040002F4 RID: 756
		public static string Keyword_MASK_HARD = "MASK_HARD";

		// Token: 0x040002F5 RID: 757
		public static string Keyword_MASK_TEX = "MASK_TEX";

		// Token: 0x040002F6 RID: 758
		public static string Keyword_Outline = "OUTLINE_ON";

		// Token: 0x040002F7 RID: 759
		public static string ShaderTag_ZTestMode = "unity_GUIZTestMode";

		// Token: 0x040002F8 RID: 760
		public static string ShaderTag_CullMode = "_CullMode";

		// Token: 0x040002F9 RID: 761
		private static float m_clamp = 1f;

		// Token: 0x040002FA RID: 762
		public static bool isInitialized = false;

		// Token: 0x040002FB RID: 763
		private static Shader k_ShaderRef_MobileSDF;

		// Token: 0x040002FC RID: 764
		private static Shader k_ShaderRef_MobileBitmap;
	}
}
