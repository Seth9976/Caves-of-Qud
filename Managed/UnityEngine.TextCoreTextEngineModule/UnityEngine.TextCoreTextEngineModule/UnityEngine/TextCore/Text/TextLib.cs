using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200005B RID: 91
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "Unity.UIElements.PlayModeTests" })]
	[NativeHeader("Modules/TextCoreTextEngine/Native/TextLib.h")]
	[StructLayout(LayoutKind.Sequential)]
	internal class TextLib
	{
		// Token: 0x0600029D RID: 669 RVA: 0x00022523 File Offset: 0x00020723
		public TextLib(byte[] icuData)
		{
			this.m_Ptr = TextLib.GetInstance(icuData);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0002253C File Offset: 0x0002073C
		private unsafe static IntPtr GetInstance(byte[] icuData)
		{
			Span<byte> span = new Span<byte>(icuData);
			IntPtr instance_Injected;
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				instance_Injected = TextLib.GetInstance_Injected(ref managedSpanWrapper);
			}
			return instance_Injected;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00022574 File Offset: 0x00020774
		public NativeTextInfo GenerateText(NativeTextGenerationSettings settings, IntPtr textGenerationInfo)
		{
			Debug.Assert((settings.fontStyle & FontStyles.Bold) == FontStyles.Normal);
			return this.GenerateTextInternal(settings, textGenerationInfo);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000225A4 File Offset: 0x000207A4
		public void ProcessMeshInfos(NativeTextInfo textInfo, NativeTextGenerationSettings settings)
		{
			Span<ATGMeshInfo> span = textInfo.meshInfos.AsSpan<ATGMeshInfo>();
			for (int i = 0; i < span.Length; i++)
			{
				ref ATGMeshInfo ptr = ref span[i];
				FontAsset fontAssetByID = FontAsset.GetFontAssetByID(ptr.fontAssetId);
				ptr.fontAsset = fontAssetByID;
				ptr.textElementInfoIndicesByAtlas = new List<List<int>>(fontAssetByID.atlasTextures.Length);
				for (int j = 0; j < fontAssetByID.atlasTextures.Length; j++)
				{
					ptr.textElementInfoIndicesByAtlas.Add(new List<int>());
				}
				float num = (float)settings.vertexPadding / 64f;
				float num2 = 1f / (float)fontAssetByID.atlasWidth;
				float num3 = 1f / (float)fontAssetByID.atlasHeight;
				bool flag = false;
				Color? color = null;
				for (int k = 0; k < ptr.textElementInfos.Length; k++)
				{
					ref NativeTextElementInfo ptr2 = ref ptr.textElementInfos[k];
					int glyphID = ptr2.glyphID;
					Glyph glyph;
					bool flag2 = fontAssetByID.TryAddGlyphInternal((uint)glyphID, out glyph);
					bool flag3 = !flag2;
					if (!flag3)
					{
						Color32 color2 = ptr2.topLeft.color;
						bool flag4 = color != null && color.Value != color2;
						if (flag4)
						{
							flag = true;
						}
						color = new Color?(color2);
						GlyphRect glyphRect = glyph.glyphRect;
						while (ptr.textElementInfoIndicesByAtlas.Count < fontAssetByID.atlasTextures.Length)
						{
							ptr.textElementInfoIndicesByAtlas.Add(new List<int>());
						}
						ptr.textElementInfoIndicesByAtlas[glyph.atlasIndex].Add(k);
						bool flag5 = (ptr2.bottomLeft.uv0.x == 0f || ptr2.bottomLeft.uv0.x == 1f) && (ptr2.bottomLeft.uv0.y == 0f || ptr2.bottomLeft.uv0.y == 1f) && (ptr2.topLeft.uv0.x == 0f || ptr2.topLeft.uv0.x == 1f) && (ptr2.topLeft.uv0.y == 0f || ptr2.topLeft.uv0.y == 1f) && (ptr2.topRight.uv0.x == 0f || ptr2.topRight.uv0.x == 1f) && (ptr2.topRight.uv0.y == 0f || ptr2.topRight.uv0.y == 1f) && (ptr2.bottomRight.uv0.x == 0f || ptr2.bottomRight.uv0.x == 1f) && (ptr2.bottomRight.uv0.y == 0f || ptr2.bottomRight.uv0.y == 1f);
						bool flag6 = flag5;
						if (flag6)
						{
							float num4 = ((float)glyphRect.x - num) * num2;
							float num5 = ((float)glyphRect.y - num) * num3;
							float num6 = ((float)(glyphRect.x + glyphRect.width) + num) * num2;
							float num7 = ((float)(glyphRect.y + glyphRect.height) + num) * num3;
							ptr2.bottomLeft.uv0 = new Vector2(num4, num5);
							ptr2.topLeft.uv0 = new Vector2(num4, num7);
							ptr2.topRight.uv0 = new Vector2(num6, num7);
							ptr2.bottomRight.uv0 = new Vector2(num6, num5);
						}
						else
						{
							Vector2 vector = new Vector2(((float)glyphRect.x - num) * num2, ((float)glyphRect.y - num) * num3);
							Vector2 vector2 = new Vector2(vector.x, ((float)(glyphRect.y + glyphRect.height) + num) * num3);
							Vector2 vector3 = new Vector2(((float)(glyphRect.x + glyphRect.width) + num) * num2, vector2.y);
							ptr2.bottomLeft.uv0 = vector3 * ptr2.bottomLeft.uv0 + vector * (Vector2.one - ptr2.bottomLeft.uv0);
							ptr2.topLeft.uv0 = vector3 * ptr2.topLeft.uv0 + vector * (Vector2.one - ptr2.topLeft.uv0);
							ptr2.topRight.uv0 = vector3 * ptr2.topRight.uv0 + vector * (Vector2.one - ptr2.topRight.uv0);
							ptr2.bottomRight.uv0 = vector3 * ptr2.bottomRight.uv0 + vector * (Vector2.one - ptr2.bottomRight.uv0);
						}
					}
				}
				ptr.hasMultipleColors = flag;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00022B24 File Offset: 0x00020D24
		[NativeMethod(Name = "TextLib::GenerateTextMesh", IsThreadSafe = true)]
		private NativeTextInfo GenerateTextInternal(NativeTextGenerationSettings settings, IntPtr textGenerationInfo)
		{
			IntPtr intPtr = TextLib.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			NativeTextInfo nativeTextInfo;
			TextLib.GenerateTextInternal_Injected(intPtr, ref settings, textGenerationInfo, out nativeTextInfo);
			return nativeTextInfo;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00022B4C File Offset: 0x00020D4C
		[NativeMethod(Name = "TextLib::MeasureText")]
		public Vector2 MeasureText(NativeTextGenerationSettings settings, IntPtr textGenerationInfo)
		{
			IntPtr intPtr = TextLib.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			TextLib.MeasureText_Injected(intPtr, ref settings, textGenerationInfo, out vector);
			return vector;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00022B74 File Offset: 0x00020D74
		[NativeMethod(Name = "TextLib::FindIntersectingLink")]
		public static int FindIntersectingLink(Vector2 point, IntPtr textGenerationInfo)
		{
			return TextLib.FindIntersectingLink_Injected(ref point, textGenerationInfo);
		}

		// Token: 0x060002A4 RID: 676
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetInstance_Injected(ref ManagedSpanWrapper icuData);

		// Token: 0x060002A5 RID: 677
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GenerateTextInternal_Injected(IntPtr _unity_self, [In] ref NativeTextGenerationSettings settings, IntPtr textGenerationInfo, out NativeTextInfo ret);

		// Token: 0x060002A6 RID: 678
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MeasureText_Injected(IntPtr _unity_self, [In] ref NativeTextGenerationSettings settings, IntPtr textGenerationInfo, out Vector2 ret);

		// Token: 0x060002A7 RID: 679
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int FindIntersectingLink_Injected([In] ref Vector2 point, IntPtr textGenerationInfo);

		// Token: 0x0400035C RID: 860
		private readonly IntPtr m_Ptr;

		// Token: 0x0400035D RID: 861
		public static Func<TextAsset> GetICUAssetEditorDelegate;

		// Token: 0x0200005C RID: 92
		internal static class BindingsMarshaller
		{
			// Token: 0x060002A8 RID: 680 RVA: 0x00022B89 File Offset: 0x00020D89
			public static IntPtr ConvertToNative(TextLib textLib)
			{
				return textLib.m_Ptr;
			}
		}
	}
}
