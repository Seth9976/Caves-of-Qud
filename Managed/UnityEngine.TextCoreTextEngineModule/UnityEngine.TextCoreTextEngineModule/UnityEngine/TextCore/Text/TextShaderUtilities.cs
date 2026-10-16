using System;
using System.Linq;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000070 RID: 112
	[ExcludeFromDocs]
	public static class TextShaderUtilities
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00026334 File Offset: 0x00024534
		internal static Shader ShaderRef_MobileSDF
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			get
			{
				bool flag = TextShaderUtilities.k_ShaderRef_MobileSDF == null;
				if (flag)
				{
					TextShaderUtilities.k_ShaderRef_MobileSDF = Shader.Find(TextShaderUtilities.k_SDFText);
				}
				return TextShaderUtilities.k_ShaderRef_MobileSDF;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00026368 File Offset: 0x00024568
		internal static Shader ShaderRef_MobileBitmap
		{
			get
			{
				bool flag = TextShaderUtilities.k_ShaderRef_MobileBitmap == null;
				if (flag)
				{
					TextShaderUtilities.k_ShaderRef_MobileBitmap = Shader.Find(TextShaderUtilities.k_BitmapText);
				}
				return TextShaderUtilities.k_ShaderRef_MobileBitmap;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000263A0 File Offset: 0x000245A0
		internal static Shader ShaderRef_Sprite
		{
			get
			{
				bool flag = TextShaderUtilities.k_ShaderRef_Sprite == null;
				if (flag)
				{
					TextShaderUtilities.k_ShaderRef_Sprite = Shader.Find("Text/Sprite");
					bool flag2 = TextShaderUtilities.k_ShaderRef_Sprite == null;
					if (flag2)
					{
						TextShaderUtilities.k_ShaderRef_Sprite = Shader.Find(TextShaderUtilities.k_SpriteText);
					}
				}
				return TextShaderUtilities.k_ShaderRef_Sprite;
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000263F8 File Offset: 0x000245F8
		static TextShaderUtilities()
		{
			TextShaderUtilities.GetShaderPropertyIDs();
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000264A0 File Offset: 0x000246A0
		internal static void GetShaderPropertyIDs()
		{
			bool flag = !TextShaderUtilities.isInitialized;
			if (flag)
			{
				TextShaderUtilities.isInitialized = true;
				TextShaderUtilities.ID_MainTex = Shader.PropertyToID("_MainTex");
				TextShaderUtilities.ID_FaceTex = Shader.PropertyToID("_FaceTex");
				TextShaderUtilities.ID_FaceColor = Shader.PropertyToID("_FaceColor");
				TextShaderUtilities.ID_FaceDilate = Shader.PropertyToID("_FaceDilate");
				TextShaderUtilities.ID_Shininess = Shader.PropertyToID("_FaceShininess");
				TextShaderUtilities.ID_OutlineOffset1 = Shader.PropertyToID("_OutlineOffset1");
				TextShaderUtilities.ID_OutlineOffset2 = Shader.PropertyToID("_OutlineOffset2");
				TextShaderUtilities.ID_OutlineOffset3 = Shader.PropertyToID("_OutlineOffset3");
				TextShaderUtilities.ID_OutlineMode = Shader.PropertyToID("_OutlineMode");
				TextShaderUtilities.ID_IsoPerimeter = Shader.PropertyToID("_IsoPerimeter");
				TextShaderUtilities.ID_Softness = Shader.PropertyToID("_Softness");
				TextShaderUtilities.ID_UnderlayColor = Shader.PropertyToID("_UnderlayColor");
				TextShaderUtilities.ID_UnderlayOffsetX = Shader.PropertyToID("_UnderlayOffsetX");
				TextShaderUtilities.ID_UnderlayOffsetY = Shader.PropertyToID("_UnderlayOffsetY");
				TextShaderUtilities.ID_UnderlayDilate = Shader.PropertyToID("_UnderlayDilate");
				TextShaderUtilities.ID_UnderlaySoftness = Shader.PropertyToID("_UnderlaySoftness");
				TextShaderUtilities.ID_UnderlayOffset = Shader.PropertyToID("_UnderlayOffset");
				TextShaderUtilities.ID_UnderlayIsoPerimeter = Shader.PropertyToID("_UnderlayIsoPerimeter");
				TextShaderUtilities.ID_WeightNormal = Shader.PropertyToID("_WeightNormal");
				TextShaderUtilities.ID_WeightBold = Shader.PropertyToID("_WeightBold");
				TextShaderUtilities.ID_OutlineTex = Shader.PropertyToID("_OutlineTex");
				TextShaderUtilities.ID_OutlineWidth = Shader.PropertyToID("_OutlineWidth");
				TextShaderUtilities.ID_OutlineSoftness = Shader.PropertyToID("_OutlineSoftness");
				TextShaderUtilities.ID_OutlineColor = Shader.PropertyToID("_OutlineColor");
				TextShaderUtilities.ID_Outline2Color = Shader.PropertyToID("_Outline2Color");
				TextShaderUtilities.ID_Outline2Width = Shader.PropertyToID("_Outline2Width");
				TextShaderUtilities.ID_Padding = Shader.PropertyToID("_Padding");
				TextShaderUtilities.ID_GradientScale = Shader.PropertyToID("_GradientScale");
				TextShaderUtilities.ID_ScaleX = Shader.PropertyToID("_ScaleX");
				TextShaderUtilities.ID_ScaleY = Shader.PropertyToID("_ScaleY");
				TextShaderUtilities.ID_PerspectiveFilter = Shader.PropertyToID("_PerspectiveFilter");
				TextShaderUtilities.ID_Sharpness = Shader.PropertyToID("_Sharpness");
				TextShaderUtilities.ID_TextureWidth = Shader.PropertyToID("_TextureWidth");
				TextShaderUtilities.ID_TextureHeight = Shader.PropertyToID("_TextureHeight");
				TextShaderUtilities.ID_BevelAmount = Shader.PropertyToID("_Bevel");
				TextShaderUtilities.ID_LightAngle = Shader.PropertyToID("_LightAngle");
				TextShaderUtilities.ID_EnvMap = Shader.PropertyToID("_Cube");
				TextShaderUtilities.ID_EnvMatrix = Shader.PropertyToID("_EnvMatrix");
				TextShaderUtilities.ID_EnvMatrixRotation = Shader.PropertyToID("_EnvMatrixRotation");
				TextShaderUtilities.ID_GlowColor = Shader.PropertyToID("_GlowColor");
				TextShaderUtilities.ID_GlowOffset = Shader.PropertyToID("_GlowOffset");
				TextShaderUtilities.ID_GlowPower = Shader.PropertyToID("_GlowPower");
				TextShaderUtilities.ID_GlowOuter = Shader.PropertyToID("_GlowOuter");
				TextShaderUtilities.ID_GlowInner = Shader.PropertyToID("_GlowInner");
				TextShaderUtilities.ID_MaskCoord = Shader.PropertyToID("_MaskCoord");
				TextShaderUtilities.ID_ClipRect = Shader.PropertyToID("_ClipRect");
				TextShaderUtilities.ID_UseClipRect = Shader.PropertyToID("_UseClipRect");
				TextShaderUtilities.ID_MaskSoftnessX = Shader.PropertyToID("_MaskSoftnessX");
				TextShaderUtilities.ID_MaskSoftnessY = Shader.PropertyToID("_MaskSoftnessY");
				TextShaderUtilities.ID_VertexOffsetX = Shader.PropertyToID("_VertexOffsetX");
				TextShaderUtilities.ID_VertexOffsetY = Shader.PropertyToID("_VertexOffsetY");
				TextShaderUtilities.ID_StencilID = Shader.PropertyToID("_Stencil");
				TextShaderUtilities.ID_StencilOp = Shader.PropertyToID("_StencilOp");
				TextShaderUtilities.ID_StencilComp = Shader.PropertyToID("_StencilComp");
				TextShaderUtilities.ID_StencilReadMask = Shader.PropertyToID("_StencilReadMask");
				TextShaderUtilities.ID_StencilWriteMask = Shader.PropertyToID("_StencilWriteMask");
				TextShaderUtilities.ID_ShaderFlags = Shader.PropertyToID("_ShaderFlags");
				TextShaderUtilities.ID_ScaleRatio_A = Shader.PropertyToID("_ScaleRatioA");
				TextShaderUtilities.ID_ScaleRatio_B = Shader.PropertyToID("_ScaleRatioB");
				TextShaderUtilities.ID_ScaleRatio_C = Shader.PropertyToID("_ScaleRatioC");
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0002684C File Offset: 0x00024A4C
		private static void UpdateShaderRatios(Material mat)
		{
			bool flag = !mat.shaderKeywords.Contains(TextShaderUtilities.Keyword_Ratios);
			bool flag2 = !mat.HasProperty(TextShaderUtilities.ID_GradientScale) || !mat.HasProperty(TextShaderUtilities.ID_FaceDilate);
			if (!flag2)
			{
				float @float = mat.GetFloat(TextShaderUtilities.ID_GradientScale);
				float float2 = mat.GetFloat(TextShaderUtilities.ID_FaceDilate);
				float float3 = mat.GetFloat(TextShaderUtilities.ID_OutlineWidth);
				float float4 = mat.GetFloat(TextShaderUtilities.ID_OutlineSoftness);
				float num = Mathf.Max(mat.GetFloat(TextShaderUtilities.ID_WeightNormal), mat.GetFloat(TextShaderUtilities.ID_WeightBold)) / 4f;
				float num2 = Mathf.Max(1f, num + float2 + float3 + float4);
				float num3 = (flag ? ((@float - TextShaderUtilities.m_clamp) / (@float * num2)) : 1f);
				mat.SetFloat(TextShaderUtilities.ID_ScaleRatio_A, num3);
				bool flag3 = mat.HasProperty(TextShaderUtilities.ID_GlowOffset);
				if (flag3)
				{
					float float5 = mat.GetFloat(TextShaderUtilities.ID_GlowOffset);
					float float6 = mat.GetFloat(TextShaderUtilities.ID_GlowOuter);
					float num4 = (num + float2) * (@float - TextShaderUtilities.m_clamp);
					num2 = Mathf.Max(1f, float5 + float6);
					float num5 = (flag ? (Mathf.Max(0f, @float - TextShaderUtilities.m_clamp - num4) / (@float * num2)) : 1f);
					mat.SetFloat(TextShaderUtilities.ID_ScaleRatio_B, num5);
				}
				bool flag4 = mat.HasProperty(TextShaderUtilities.ID_UnderlayOffsetX);
				if (flag4)
				{
					float float7 = mat.GetFloat(TextShaderUtilities.ID_UnderlayOffsetX);
					float float8 = mat.GetFloat(TextShaderUtilities.ID_UnderlayOffsetY);
					float float9 = mat.GetFloat(TextShaderUtilities.ID_UnderlayDilate);
					float float10 = mat.GetFloat(TextShaderUtilities.ID_UnderlaySoftness);
					float num6 = (num + float2) * (@float - TextShaderUtilities.m_clamp);
					num2 = Mathf.Max(1f, Mathf.Max(Mathf.Abs(float7), Mathf.Abs(float8)) + float9 + float10);
					float num7 = (flag ? (Mathf.Max(0f, @float - TextShaderUtilities.m_clamp - num6) / (@float * num2)) : 1f);
					mat.SetFloat(TextShaderUtilities.ID_ScaleRatio_C, num7);
				}
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00026A70 File Offset: 0x00024C70
		internal static Vector4 GetFontExtent(Material material)
		{
			return Vector4.zero;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00026A88 File Offset: 0x00024C88
		internal static bool IsMaskingEnabled(Material material)
		{
			bool flag = material == null || !material.HasProperty(TextShaderUtilities.ID_ClipRect);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = material.shaderKeywords.Contains(TextShaderUtilities.Keyword_MASK_SOFT) || material.shaderKeywords.Contains(TextShaderUtilities.Keyword_MASK_HARD) || material.shaderKeywords.Contains(TextShaderUtilities.Keyword_MASK_TEX);
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00026AFC File Offset: 0x00024CFC
		internal static float GetPadding(Material material, bool enableExtraPadding, bool isBold)
		{
			bool flag = !TextShaderUtilities.isInitialized;
			if (flag)
			{
				TextShaderUtilities.GetShaderPropertyIDs();
			}
			bool flag2 = material == null;
			float num;
			if (flag2)
			{
				num = 0f;
			}
			else
			{
				int num2 = (enableExtraPadding ? 4 : 0);
				bool flag3 = !material.HasProperty(TextShaderUtilities.ID_GradientScale);
				if (flag3)
				{
					bool flag4 = material.HasProperty(TextShaderUtilities.ID_Padding);
					if (flag4)
					{
						num2 += (int)material.GetFloat(TextShaderUtilities.ID_Padding);
					}
					num = (float)num2 + 1f;
				}
				else
				{
					bool flag5 = material.HasProperty(TextShaderUtilities.ID_IsoPerimeter);
					if (flag5)
					{
						num = TextShaderUtilities.ComputePaddingForProperties(material) + 0.25f + (float)num2;
					}
					else
					{
						Vector4 vector = Vector4.zero;
						Vector4 zero = Vector4.zero;
						float num3 = 0f;
						float num4 = 0f;
						float num5 = 0f;
						float num6 = 0f;
						float num7 = 0f;
						float num8 = 0f;
						float num9 = 0f;
						float num10 = 0f;
						TextShaderUtilities.UpdateShaderRatios(material);
						string[] shaderKeywords = material.shaderKeywords;
						bool flag6 = material.HasProperty(TextShaderUtilities.ID_ScaleRatio_A);
						if (flag6)
						{
							num6 = material.GetFloat(TextShaderUtilities.ID_ScaleRatio_A);
						}
						bool flag7 = material.HasProperty(TextShaderUtilities.ID_FaceDilate);
						if (flag7)
						{
							num3 = material.GetFloat(TextShaderUtilities.ID_FaceDilate) * num6;
						}
						bool flag8 = material.HasProperty(TextShaderUtilities.ID_OutlineSoftness);
						if (flag8)
						{
							num4 = material.GetFloat(TextShaderUtilities.ID_OutlineSoftness) * num6;
						}
						bool flag9 = material.HasProperty(TextShaderUtilities.ID_OutlineWidth);
						if (flag9)
						{
							num5 = material.GetFloat(TextShaderUtilities.ID_OutlineWidth) * num6;
						}
						float num11 = num5 + num4 + num3;
						bool flag10 = material.HasProperty(TextShaderUtilities.ID_GlowOffset) && shaderKeywords.Contains(TextShaderUtilities.Keyword_Glow);
						if (flag10)
						{
							bool flag11 = material.HasProperty(TextShaderUtilities.ID_ScaleRatio_B);
							if (flag11)
							{
								num7 = material.GetFloat(TextShaderUtilities.ID_ScaleRatio_B);
							}
							num9 = material.GetFloat(TextShaderUtilities.ID_GlowOffset) * num7;
							num10 = material.GetFloat(TextShaderUtilities.ID_GlowOuter) * num7;
						}
						num11 = Mathf.Max(num11, num3 + num9 + num10);
						bool flag12 = material.HasProperty(TextShaderUtilities.ID_UnderlaySoftness) && shaderKeywords.Contains(TextShaderUtilities.Keyword_Underlay);
						if (flag12)
						{
							bool flag13 = material.HasProperty(TextShaderUtilities.ID_ScaleRatio_C);
							if (flag13)
							{
								num8 = material.GetFloat(TextShaderUtilities.ID_ScaleRatio_C);
							}
							float num12 = 0f;
							float num13 = 0f;
							float num14 = 0f;
							float num15 = 0f;
							bool flag14 = material.HasProperty(TextShaderUtilities.ID_UnderlayOffset);
							if (flag14)
							{
								Vector2 vector2 = material.GetVector(TextShaderUtilities.ID_UnderlayOffset);
								num12 = vector2.x;
								num13 = vector2.y;
								num14 = material.GetFloat(TextShaderUtilities.ID_UnderlayDilate);
								num15 = material.GetFloat(TextShaderUtilities.ID_UnderlaySoftness);
							}
							else
							{
								bool flag15 = material.HasProperty(TextShaderUtilities.ID_UnderlayOffsetX);
								if (flag15)
								{
									num12 = material.GetFloat(TextShaderUtilities.ID_UnderlayOffsetX) * num8;
									num13 = material.GetFloat(TextShaderUtilities.ID_UnderlayOffsetY) * num8;
									num14 = material.GetFloat(TextShaderUtilities.ID_UnderlayDilate) * num8;
									num15 = material.GetFloat(TextShaderUtilities.ID_UnderlaySoftness) * num8;
								}
							}
							vector.x = Mathf.Max(vector.x, num3 + num14 + num15 - num12);
							vector.y = Mathf.Max(vector.y, num3 + num14 + num15 - num13);
							vector.z = Mathf.Max(vector.z, num3 + num14 + num15 + num12);
							vector.w = Mathf.Max(vector.w, num3 + num14 + num15 + num13);
						}
						vector.x = Mathf.Max(vector.x, num11);
						vector.y = Mathf.Max(vector.y, num11);
						vector.z = Mathf.Max(vector.z, num11);
						vector.w = Mathf.Max(vector.w, num11);
						vector.x += (float)num2;
						vector.y += (float)num2;
						vector.z += (float)num2;
						vector.w += (float)num2;
						vector.x = Mathf.Min(vector.x, 1f);
						vector.y = Mathf.Min(vector.y, 1f);
						vector.z = Mathf.Min(vector.z, 1f);
						vector.w = Mathf.Min(vector.w, 1f);
						zero.x = ((zero.x < vector.x) ? vector.x : zero.x);
						zero.y = ((zero.y < vector.y) ? vector.y : zero.y);
						zero.z = ((zero.z < vector.z) ? vector.z : zero.z);
						zero.w = ((zero.w < vector.w) ? vector.w : zero.w);
						float @float = material.GetFloat(TextShaderUtilities.ID_GradientScale);
						vector *= @float;
						num11 = Mathf.Max(vector.x, vector.y);
						num11 = Mathf.Max(vector.z, num11);
						num11 = Mathf.Max(vector.w, num11);
						num = num11 + 1.25f;
					}
				}
			}
			return num;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00027040 File Offset: 0x00025240
		private static float ComputePaddingForProperties(Material mat)
		{
			Vector4 vector = mat.GetVector(TextShaderUtilities.ID_IsoPerimeter);
			Vector2 vector2 = mat.GetVector(TextShaderUtilities.ID_OutlineOffset1);
			Vector2 vector3 = mat.GetVector(TextShaderUtilities.ID_OutlineOffset2);
			Vector2 vector4 = mat.GetVector(TextShaderUtilities.ID_OutlineOffset3);
			bool flag = mat.GetFloat(TextShaderUtilities.ID_OutlineMode) != 0f;
			Vector4 vector5 = mat.GetVector(TextShaderUtilities.ID_Softness);
			float @float = mat.GetFloat(TextShaderUtilities.ID_GradientScale);
			float num = Mathf.Max(0f, vector.x + vector5.x * 0.5f);
			bool flag2 = !flag;
			if (flag2)
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
			Vector2 vector6 = mat.GetVector(TextShaderUtilities.ID_UnderlayOffset);
			float float2 = mat.GetFloat(TextShaderUtilities.ID_UnderlayDilate);
			float float3 = mat.GetFloat(TextShaderUtilities.ID_UnderlaySoftness);
			num = Mathf.Max(num, float2 + float3 * 0.5f + Mathf.Max(Mathf.Abs(vector6.x), Mathf.Abs(vector6.y)));
			return num * @float;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000272C4 File Offset: 0x000254C4
		internal static float GetPadding(Material[] materials, bool enableExtraPadding, bool isBold)
		{
			bool flag = !TextShaderUtilities.isInitialized;
			if (flag)
			{
				TextShaderUtilities.GetShaderPropertyIDs();
			}
			bool flag2 = materials == null;
			float num;
			if (flag2)
			{
				num = 0f;
			}
			else
			{
				int num2 = (enableExtraPadding ? 4 : 0);
				bool flag3 = materials[0].HasProperty(TextShaderUtilities.ID_Padding);
				if (flag3)
				{
					num = (float)num2 + materials[0].GetFloat(TextShaderUtilities.ID_Padding);
				}
				else
				{
					Vector4 vector = Vector4.zero;
					Vector4 zero = Vector4.zero;
					float num3 = 0f;
					float num4 = 0f;
					float num5 = 0f;
					float num6 = 0f;
					float num7 = 0f;
					float num8 = 0f;
					float num9 = 0f;
					float num10 = 0f;
					float num11;
					for (int i = 0; i < materials.Length; i++)
					{
						TextShaderUtilities.UpdateShaderRatios(materials[i]);
						string[] shaderKeywords = materials[i].shaderKeywords;
						bool flag4 = materials[i].HasProperty(TextShaderUtilities.ID_ScaleRatio_A);
						if (flag4)
						{
							num6 = materials[i].GetFloat(TextShaderUtilities.ID_ScaleRatio_A);
						}
						bool flag5 = materials[i].HasProperty(TextShaderUtilities.ID_FaceDilate);
						if (flag5)
						{
							num3 = materials[i].GetFloat(TextShaderUtilities.ID_FaceDilate) * num6;
						}
						bool flag6 = materials[i].HasProperty(TextShaderUtilities.ID_OutlineSoftness);
						if (flag6)
						{
							num4 = materials[i].GetFloat(TextShaderUtilities.ID_OutlineSoftness) * num6;
						}
						bool flag7 = materials[i].HasProperty(TextShaderUtilities.ID_OutlineWidth);
						if (flag7)
						{
							num5 = materials[i].GetFloat(TextShaderUtilities.ID_OutlineWidth) * num6;
						}
						num11 = num5 + num4 + num3;
						bool flag8 = materials[i].HasProperty(TextShaderUtilities.ID_GlowOffset) && shaderKeywords.Contains(TextShaderUtilities.Keyword_Glow);
						if (flag8)
						{
							bool flag9 = materials[i].HasProperty(TextShaderUtilities.ID_ScaleRatio_B);
							if (flag9)
							{
								num7 = materials[i].GetFloat(TextShaderUtilities.ID_ScaleRatio_B);
							}
							num9 = materials[i].GetFloat(TextShaderUtilities.ID_GlowOffset) * num7;
							num10 = materials[i].GetFloat(TextShaderUtilities.ID_GlowOuter) * num7;
						}
						num11 = Mathf.Max(num11, num3 + num9 + num10);
						bool flag10 = materials[i].HasProperty(TextShaderUtilities.ID_UnderlaySoftness) && shaderKeywords.Contains(TextShaderUtilities.Keyword_Underlay);
						if (flag10)
						{
							bool flag11 = materials[i].HasProperty(TextShaderUtilities.ID_ScaleRatio_C);
							if (flag11)
							{
								num8 = materials[i].GetFloat(TextShaderUtilities.ID_ScaleRatio_C);
							}
							float num12 = materials[i].GetFloat(TextShaderUtilities.ID_UnderlayOffsetX) * num8;
							float num13 = materials[i].GetFloat(TextShaderUtilities.ID_UnderlayOffsetY) * num8;
							float num14 = materials[i].GetFloat(TextShaderUtilities.ID_UnderlayDilate) * num8;
							float num15 = materials[i].GetFloat(TextShaderUtilities.ID_UnderlaySoftness) * num8;
							vector.x = Mathf.Max(vector.x, num3 + num14 + num15 - num12);
							vector.y = Mathf.Max(vector.y, num3 + num14 + num15 - num13);
							vector.z = Mathf.Max(vector.z, num3 + num14 + num15 + num12);
							vector.w = Mathf.Max(vector.w, num3 + num14 + num15 + num13);
						}
						vector.x = Mathf.Max(vector.x, num11);
						vector.y = Mathf.Max(vector.y, num11);
						vector.z = Mathf.Max(vector.z, num11);
						vector.w = Mathf.Max(vector.w, num11);
						vector.x += (float)num2;
						vector.y += (float)num2;
						vector.z += (float)num2;
						vector.w += (float)num2;
						vector.x = Mathf.Min(vector.x, 1f);
						vector.y = Mathf.Min(vector.y, 1f);
						vector.z = Mathf.Min(vector.z, 1f);
						vector.w = Mathf.Min(vector.w, 1f);
						zero.x = ((zero.x < vector.x) ? vector.x : zero.x);
						zero.y = ((zero.y < vector.y) ? vector.y : zero.y);
						zero.z = ((zero.z < vector.z) ? vector.z : zero.z);
						zero.w = ((zero.w < vector.w) ? vector.w : zero.w);
					}
					float @float = materials[0].GetFloat(TextShaderUtilities.ID_GradientScale);
					vector *= @float;
					num11 = Mathf.Max(vector.x, vector.y);
					num11 = Mathf.Max(vector.z, num11);
					num11 = Mathf.Max(vector.w, num11);
					num = num11 + 0.25f;
				}
			}
			return num;
		}

		// Token: 0x04000473 RID: 1139
		public static int ID_MainTex;

		// Token: 0x04000474 RID: 1140
		public static int ID_FaceTex;

		// Token: 0x04000475 RID: 1141
		public static int ID_FaceColor;

		// Token: 0x04000476 RID: 1142
		public static int ID_FaceDilate;

		// Token: 0x04000477 RID: 1143
		public static int ID_Shininess;

		// Token: 0x04000478 RID: 1144
		public static int ID_OutlineOffset1;

		// Token: 0x04000479 RID: 1145
		public static int ID_OutlineOffset2;

		// Token: 0x0400047A RID: 1146
		public static int ID_OutlineOffset3;

		// Token: 0x0400047B RID: 1147
		public static int ID_OutlineMode;

		// Token: 0x0400047C RID: 1148
		public static int ID_IsoPerimeter;

		// Token: 0x0400047D RID: 1149
		public static int ID_Softness;

		// Token: 0x0400047E RID: 1150
		public static int ID_UnderlayColor;

		// Token: 0x0400047F RID: 1151
		public static int ID_UnderlayOffsetX;

		// Token: 0x04000480 RID: 1152
		public static int ID_UnderlayOffsetY;

		// Token: 0x04000481 RID: 1153
		public static int ID_UnderlayDilate;

		// Token: 0x04000482 RID: 1154
		public static int ID_UnderlaySoftness;

		// Token: 0x04000483 RID: 1155
		public static int ID_UnderlayOffset;

		// Token: 0x04000484 RID: 1156
		public static int ID_UnderlayIsoPerimeter;

		// Token: 0x04000485 RID: 1157
		public static int ID_WeightNormal;

		// Token: 0x04000486 RID: 1158
		public static int ID_WeightBold;

		// Token: 0x04000487 RID: 1159
		public static int ID_OutlineTex;

		// Token: 0x04000488 RID: 1160
		public static int ID_OutlineWidth;

		// Token: 0x04000489 RID: 1161
		public static int ID_OutlineSoftness;

		// Token: 0x0400048A RID: 1162
		public static int ID_OutlineColor;

		// Token: 0x0400048B RID: 1163
		public static int ID_Outline2Color;

		// Token: 0x0400048C RID: 1164
		public static int ID_Outline2Width;

		// Token: 0x0400048D RID: 1165
		public static int ID_Padding;

		// Token: 0x0400048E RID: 1166
		public static int ID_GradientScale;

		// Token: 0x0400048F RID: 1167
		public static int ID_ScaleX;

		// Token: 0x04000490 RID: 1168
		public static int ID_ScaleY;

		// Token: 0x04000491 RID: 1169
		public static int ID_PerspectiveFilter;

		// Token: 0x04000492 RID: 1170
		public static int ID_Sharpness;

		// Token: 0x04000493 RID: 1171
		public static int ID_TextureWidth;

		// Token: 0x04000494 RID: 1172
		public static int ID_TextureHeight;

		// Token: 0x04000495 RID: 1173
		public static int ID_BevelAmount;

		// Token: 0x04000496 RID: 1174
		public static int ID_GlowColor;

		// Token: 0x04000497 RID: 1175
		public static int ID_GlowOffset;

		// Token: 0x04000498 RID: 1176
		public static int ID_GlowPower;

		// Token: 0x04000499 RID: 1177
		public static int ID_GlowOuter;

		// Token: 0x0400049A RID: 1178
		public static int ID_GlowInner;

		// Token: 0x0400049B RID: 1179
		public static int ID_LightAngle;

		// Token: 0x0400049C RID: 1180
		public static int ID_EnvMap;

		// Token: 0x0400049D RID: 1181
		public static int ID_EnvMatrix;

		// Token: 0x0400049E RID: 1182
		public static int ID_EnvMatrixRotation;

		// Token: 0x0400049F RID: 1183
		public static int ID_MaskCoord;

		// Token: 0x040004A0 RID: 1184
		public static int ID_ClipRect;

		// Token: 0x040004A1 RID: 1185
		public static int ID_MaskSoftnessX;

		// Token: 0x040004A2 RID: 1186
		public static int ID_MaskSoftnessY;

		// Token: 0x040004A3 RID: 1187
		public static int ID_VertexOffsetX;

		// Token: 0x040004A4 RID: 1188
		public static int ID_VertexOffsetY;

		// Token: 0x040004A5 RID: 1189
		public static int ID_UseClipRect;

		// Token: 0x040004A6 RID: 1190
		public static int ID_StencilID;

		// Token: 0x040004A7 RID: 1191
		public static int ID_StencilOp;

		// Token: 0x040004A8 RID: 1192
		public static int ID_StencilComp;

		// Token: 0x040004A9 RID: 1193
		public static int ID_StencilReadMask;

		// Token: 0x040004AA RID: 1194
		public static int ID_StencilWriteMask;

		// Token: 0x040004AB RID: 1195
		public static int ID_ShaderFlags;

		// Token: 0x040004AC RID: 1196
		public static int ID_ScaleRatio_A;

		// Token: 0x040004AD RID: 1197
		public static int ID_ScaleRatio_B;

		// Token: 0x040004AE RID: 1198
		public static int ID_ScaleRatio_C;

		// Token: 0x040004AF RID: 1199
		public static string Keyword_Bevel = "BEVEL_ON";

		// Token: 0x040004B0 RID: 1200
		public static string Keyword_Glow = "GLOW_ON";

		// Token: 0x040004B1 RID: 1201
		public static string Keyword_Underlay = "UNDERLAY_ON";

		// Token: 0x040004B2 RID: 1202
		public static string Keyword_Ratios = "RATIOS_OFF";

		// Token: 0x040004B3 RID: 1203
		public static string Keyword_MASK_SOFT = "MASK_SOFT";

		// Token: 0x040004B4 RID: 1204
		public static string Keyword_MASK_HARD = "MASK_HARD";

		// Token: 0x040004B5 RID: 1205
		public static string Keyword_MASK_TEX = "MASK_TEX";

		// Token: 0x040004B6 RID: 1206
		public static string Keyword_Outline = "OUTLINE_ON";

		// Token: 0x040004B7 RID: 1207
		public static string ShaderTag_ZTestMode = "unity_GUIZTestMode";

		// Token: 0x040004B8 RID: 1208
		public static string ShaderTag_CullMode = "_CullMode";

		// Token: 0x040004B9 RID: 1209
		private static float m_clamp = 1f;

		// Token: 0x040004BA RID: 1210
		public static bool isInitialized = false;

		// Token: 0x040004BB RID: 1211
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static readonly string k_SDFText = "Hidden/TextCore/Distance Field SSD";

		// Token: 0x040004BC RID: 1212
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static readonly string k_BitmapText = "Hidden/Internal-GUITextureClipText";

		// Token: 0x040004BD RID: 1213
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static readonly string k_SpriteText = "Hidden/TextCore/Sprite";

		// Token: 0x040004BE RID: 1214
		private static Shader k_ShaderRef_MobileSDF;

		// Token: 0x040004BF RID: 1215
		private static Shader k_ShaderRef_MobileBitmap;

		// Token: 0x040004C0 RID: 1216
		private static Shader k_ShaderRef_Sprite;
	}
}
