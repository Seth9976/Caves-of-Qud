using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200059C RID: 1436
	[NativeHeader("Runtime/Graphics/GraphicsFormatUtility.bindings.h")]
	[NativeHeader("Runtime/Graphics/TextureFormat.h")]
	[NativeHeader("Runtime/Graphics/Format.h")]
	public class GraphicsFormatUtility
	{
		// Token: 0x0600370E RID: 14094 RVA: 0x0006C934 File Offset: 0x0006AB34
		[FreeFunction("GetGraphicsFormat_Native_Texture")]
		internal static GraphicsFormat GetFormat([NotNull] Texture texture)
		{
			if (texture == null)
			{
				ThrowHelper.ThrowArgumentNullException(texture, "texture");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture>(texture);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(texture, "texture");
			}
			return GraphicsFormatUtility.GetFormat_Injected(intPtr);
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x0006C96C File Offset: 0x0006AB6C
		public static GraphicsFormat GetGraphicsFormat(TextureFormat format, bool isSRGB)
		{
			return GraphicsFormatUtility.GetGraphicsFormat_Native_TextureFormat(format, isSRGB);
		}

		// Token: 0x06003710 RID: 14096
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetGraphicsFormat_Native_TextureFormat(TextureFormat format, bool isSRGB);

		// Token: 0x06003711 RID: 14097 RVA: 0x0006C988 File Offset: 0x0006AB88
		public static TextureFormat GetTextureFormat(GraphicsFormat format)
		{
			return GraphicsFormatUtility.GetTextureFormat_Native_GraphicsFormat(format);
		}

		// Token: 0x06003712 RID: 14098
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureFormat GetTextureFormat_Native_GraphicsFormat(GraphicsFormat format);

		// Token: 0x06003713 RID: 14099 RVA: 0x0006C9A0 File Offset: 0x0006ABA0
		public static GraphicsFormat GetGraphicsFormat(RenderTextureFormat format, bool isSRGB)
		{
			return GraphicsFormatUtility.GetGraphicsFormat_Native_RenderTextureFormat(format, isSRGB);
		}

		// Token: 0x06003714 RID: 14100
		[FreeFunction(IsThreadSafe = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetGraphicsFormat_Native_RenderTextureFormat(RenderTextureFormat format, bool isSRGB);

		// Token: 0x06003715 RID: 14101 RVA: 0x0006C9BC File Offset: 0x0006ABBC
		public static GraphicsFormat GetGraphicsFormat(RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			bool flag = QualitySettings.activeColorSpace == ColorSpace.Linear;
			bool flag2 = ((readWrite == RenderTextureReadWrite.Default) ? flag : (readWrite == RenderTextureReadWrite.sRGB));
			return GraphicsFormatUtility.GetGraphicsFormat(format, flag2);
		}

		// Token: 0x06003716 RID: 14102
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetDepthStencilFormatFromBitsLegacy_Native(int minimumDepthBits);

		// Token: 0x06003717 RID: 14103 RVA: 0x0006C9EC File Offset: 0x0006ABEC
		public static GraphicsFormat GetDepthStencilFormat(int depthBits)
		{
			return GraphicsFormatUtility.GetDepthStencilFormatFromBitsLegacy_Native(depthBits);
		}

		// Token: 0x06003718 RID: 14104
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetDepthBits(GraphicsFormat format);

		// Token: 0x06003719 RID: 14105 RVA: 0x0006CA04 File Offset: 0x0006AC04
		public static GraphicsFormat GetDepthStencilFormat(int minimumDepthBits, int minimumStencilBits)
		{
			bool flag = minimumDepthBits == 0 && minimumStencilBits == 0;
			GraphicsFormat graphicsFormat;
			if (flag)
			{
				graphicsFormat = GraphicsFormat.None;
			}
			else
			{
				bool flag2 = minimumDepthBits < 0 || minimumStencilBits < 0;
				if (flag2)
				{
					throw new ArgumentException("Number of bits in DepthStencil format can't be negative.");
				}
				bool flag3 = minimumDepthBits > 32;
				if (flag3)
				{
					throw new ArgumentException("Number of depth buffer bits cannot exceed 32.");
				}
				bool flag4 = minimumStencilBits > 8;
				if (flag4)
				{
					throw new ArgumentException("Number of stencil buffer bits cannot exceed 8.");
				}
				bool flag5 = minimumDepthBits == 0;
				if (flag5)
				{
					minimumDepthBits = 0;
				}
				else
				{
					bool flag6 = minimumDepthBits <= 16;
					if (flag6)
					{
						minimumDepthBits = 16;
					}
					else
					{
						bool flag7 = minimumDepthBits <= 24;
						if (flag7)
						{
							minimumDepthBits = 24;
						}
						else
						{
							minimumDepthBits = 32;
						}
					}
				}
				bool flag8 = minimumStencilBits != 0;
				if (flag8)
				{
					minimumStencilBits = 8;
				}
				Debug.Assert(GraphicsFormatUtility.tableNoStencil.Length == GraphicsFormatUtility.tableStencil.Length);
				GraphicsFormat[] array = ((minimumStencilBits > 0) ? GraphicsFormatUtility.tableStencil : GraphicsFormatUtility.tableNoStencil);
				int num = minimumDepthBits / 8;
				for (int i = num; i < array.Length; i++)
				{
					GraphicsFormat graphicsFormat2 = array[i];
					bool flag9 = SystemInfo.IsFormatSupported(graphicsFormat2, GraphicsFormatUsage.Render);
					if (flag9)
					{
						return graphicsFormat2;
					}
				}
				graphicsFormat = GraphicsFormat.None;
			}
			return graphicsFormat;
		}

		// Token: 0x0600371A RID: 14106
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSRGBFormat(GraphicsFormat format);

		// Token: 0x0600371B RID: 14107
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSwizzleFormat(GraphicsFormat format);

		// Token: 0x0600371C RID: 14108 RVA: 0x0006CB24 File Offset: 0x0006AD24
		public static bool IsSwizzleFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsSwizzleFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x0600371D RID: 14109
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat GetSRGBFormat(GraphicsFormat format);

		// Token: 0x0600371E RID: 14110
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat GetLinearFormat(GraphicsFormat format);

		// Token: 0x0600371F RID: 14111
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern RenderTextureFormat GetRenderTextureFormat(GraphicsFormat format);

		// Token: 0x06003720 RID: 14112
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetColorComponentCount(GraphicsFormat format);

		// Token: 0x06003721 RID: 14113 RVA: 0x0006CB44 File Offset: 0x0006AD44
		public static uint GetColorComponentCount(TextureFormat format)
		{
			return GraphicsFormatUtility.GetColorComponentCount(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003722 RID: 14114
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetAlphaComponentCount(GraphicsFormat format);

		// Token: 0x06003723 RID: 14115 RVA: 0x0006CB64 File Offset: 0x0006AD64
		public static uint GetAlphaComponentCount(TextureFormat format)
		{
			return GraphicsFormatUtility.GetAlphaComponentCount(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003724 RID: 14116
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetComponentCount(GraphicsFormat format);

		// Token: 0x06003725 RID: 14117 RVA: 0x0006CB84 File Offset: 0x0006AD84
		public static uint GetComponentCount(TextureFormat format)
		{
			return GraphicsFormatUtility.GetComponentCount(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x0006CBA4 File Offset: 0x0006ADA4
		[FreeFunction(IsThreadSafe = true)]
		public static string GetFormatString(GraphicsFormat format)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				GraphicsFormatUtility.GetFormatString_Injected(format, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x0006CBD4 File Offset: 0x0006ADD4
		[FreeFunction(IsThreadSafe = true)]
		private static string GetFormatString_Native_TextureFormat(TextureFormat format)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				GraphicsFormatUtility.GetFormatString_Native_TextureFormat_Injected(format, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x0006CC04 File Offset: 0x0006AE04
		public static string GetFormatString(TextureFormat format)
		{
			return GraphicsFormatUtility.GetFormatString_Native_TextureFormat(format);
		}

		// Token: 0x06003729 RID: 14121
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsCompressedFormat(GraphicsFormat format);

		// Token: 0x0600372A RID: 14122
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsCompressedFormat_Native_TextureFormat(TextureFormat format);

		// Token: 0x0600372B RID: 14123 RVA: 0x0006CC1C File Offset: 0x0006AE1C
		[Obsolete("IsCompressedTextureFormat is obsolete, please use IsCompressedFormat instead.")]
		internal static bool IsCompressedTextureFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsCompressedFormat(format);
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x0006CC34 File Offset: 0x0006AE34
		public static bool IsCompressedFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsCompressedFormat_Native_TextureFormat(format);
		}

		// Token: 0x0600372D RID: 14125
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanDecompressFormat(GraphicsFormat format, bool wholeImage);

		// Token: 0x0600372E RID: 14126 RVA: 0x0006CC4C File Offset: 0x0006AE4C
		internal static bool CanDecompressFormat(GraphicsFormat format)
		{
			return GraphicsFormatUtility.CanDecompressFormat(format, true);
		}

		// Token: 0x0600372F RID: 14127
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsPackedFormat(GraphicsFormat format);

		// Token: 0x06003730 RID: 14128 RVA: 0x0006CC68 File Offset: 0x0006AE68
		public static bool IsPackedFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsPackedFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003731 RID: 14129
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Is16BitPackedFormat(GraphicsFormat format);

		// Token: 0x06003732 RID: 14130 RVA: 0x0006CC88 File Offset: 0x0006AE88
		public static bool Is16BitPackedFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.Is16BitPackedFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003733 RID: 14131
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat ConvertToAlphaFormat(GraphicsFormat format);

		// Token: 0x06003734 RID: 14132
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureFormat ConvertToAlphaFormat_Native_TextureFormat(TextureFormat format);

		// Token: 0x06003735 RID: 14133 RVA: 0x0006CCA8 File Offset: 0x0006AEA8
		public static TextureFormat ConvertToAlphaFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.ConvertToAlphaFormat_Native_TextureFormat(format);
		}

		// Token: 0x06003736 RID: 14134
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsAlphaOnlyFormat(GraphicsFormat format);

		// Token: 0x06003737 RID: 14135
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAlphaOnlyFormat_Native_TextureFormat(TextureFormat format);

		// Token: 0x06003738 RID: 14136 RVA: 0x0006CCC0 File Offset: 0x0006AEC0
		public static bool IsAlphaOnlyFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsAlphaOnlyFormat_Native_TextureFormat(format);
		}

		// Token: 0x06003739 RID: 14137
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsAlphaTestFormat(GraphicsFormat format);

		// Token: 0x0600373A RID: 14138 RVA: 0x0006CCD8 File Offset: 0x0006AED8
		public static bool IsAlphaTestFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsAlphaTestFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x0600373B RID: 14139
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasAlphaChannel(GraphicsFormat format);

		// Token: 0x0600373C RID: 14140
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasAlphaChannel_Native_TextureFormat(TextureFormat format);

		// Token: 0x0600373D RID: 14141 RVA: 0x0006CCF8 File Offset: 0x0006AEF8
		public static bool HasAlphaChannel(TextureFormat format)
		{
			return GraphicsFormatUtility.HasAlphaChannel_Native_TextureFormat(format);
		}

		// Token: 0x0600373E RID: 14142
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsDepthFormat(GraphicsFormat format);

		// Token: 0x0600373F RID: 14143
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsStencilFormat(GraphicsFormat format);

		// Token: 0x06003740 RID: 14144
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsDepthStencilFormat(GraphicsFormat format);

		// Token: 0x06003741 RID: 14145
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsIEEE754Format(GraphicsFormat format);

		// Token: 0x06003742 RID: 14146
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsFloatFormat(GraphicsFormat format);

		// Token: 0x06003743 RID: 14147
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsHalfFormat(GraphicsFormat format);

		// Token: 0x06003744 RID: 14148
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsUnsignedFormat(GraphicsFormat format);

		// Token: 0x06003745 RID: 14149 RVA: 0x0006CD10 File Offset: 0x0006AF10
		public static bool IsUnsignedFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsUnsignedFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003746 RID: 14150
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSignedFormat(GraphicsFormat format);

		// Token: 0x06003747 RID: 14151 RVA: 0x0006CD30 File Offset: 0x0006AF30
		public static bool IsSignedFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsSignedFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003748 RID: 14152
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsNormFormat(GraphicsFormat format);

		// Token: 0x06003749 RID: 14153
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsUNormFormat(GraphicsFormat format);

		// Token: 0x0600374A RID: 14154
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSNormFormat(GraphicsFormat format);

		// Token: 0x0600374B RID: 14155
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsIntegerFormat(GraphicsFormat format);

		// Token: 0x0600374C RID: 14156
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsUIntFormat(GraphicsFormat format);

		// Token: 0x0600374D RID: 14157
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSIntFormat(GraphicsFormat format);

		// Token: 0x0600374E RID: 14158
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsXRFormat(GraphicsFormat format);

		// Token: 0x0600374F RID: 14159
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsDXTCFormat(GraphicsFormat format);

		// Token: 0x06003750 RID: 14160 RVA: 0x0006CD50 File Offset: 0x0006AF50
		public static bool IsDXTCFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsDXTCFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003751 RID: 14161
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsRGTCFormat(GraphicsFormat format);

		// Token: 0x06003752 RID: 14162 RVA: 0x0006CD70 File Offset: 0x0006AF70
		public static bool IsRGTCFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsRGTCFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003753 RID: 14163
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsBPTCFormat(GraphicsFormat format);

		// Token: 0x06003754 RID: 14164 RVA: 0x0006CD90 File Offset: 0x0006AF90
		public static bool IsBPTCFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsBPTCFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003755 RID: 14165
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsBCFormat(GraphicsFormat format);

		// Token: 0x06003756 RID: 14166 RVA: 0x0006CDB0 File Offset: 0x0006AFB0
		public static bool IsBCFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsBCFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003757 RID: 14167
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsPVRTCFormat(GraphicsFormat format);

		// Token: 0x06003758 RID: 14168 RVA: 0x0006CDD0 File Offset: 0x0006AFD0
		public static bool IsPVRTCFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsPVRTCFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003759 RID: 14169
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsETCFormat(GraphicsFormat format);

		// Token: 0x0600375A RID: 14170 RVA: 0x0006CDF0 File Offset: 0x0006AFF0
		public static bool IsETCFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsETCFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x0600375B RID: 14171
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsEACFormat(GraphicsFormat format);

		// Token: 0x0600375C RID: 14172 RVA: 0x0006CE10 File Offset: 0x0006B010
		public static bool IsEACFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsEACFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x0600375D RID: 14173
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsASTCFormat(GraphicsFormat format);

		// Token: 0x0600375E RID: 14174 RVA: 0x0006CE30 File Offset: 0x0006B030
		public static bool IsASTCFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsASTCFormat(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x0600375F RID: 14175
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsHDRFormat(GraphicsFormat format);

		// Token: 0x06003760 RID: 14176
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsHDRFormat_Native_TextureFormat(TextureFormat format);

		// Token: 0x06003761 RID: 14177 RVA: 0x0006CE50 File Offset: 0x0006B050
		public static bool IsHDRFormat(TextureFormat format)
		{
			return GraphicsFormatUtility.IsHDRFormat_Native_TextureFormat(format);
		}

		// Token: 0x06003762 RID: 14178
		[FreeFunction("IsCompressedCrunchTextureFormat", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsCrunchFormat(TextureFormat format);

		// Token: 0x06003763 RID: 14179
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FormatSwizzle GetSwizzleR(GraphicsFormat format);

		// Token: 0x06003764 RID: 14180 RVA: 0x0006CE68 File Offset: 0x0006B068
		public static FormatSwizzle GetSwizzleR(TextureFormat format)
		{
			return GraphicsFormatUtility.GetSwizzleR(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003765 RID: 14181
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FormatSwizzle GetSwizzleG(GraphicsFormat format);

		// Token: 0x06003766 RID: 14182 RVA: 0x0006CE88 File Offset: 0x0006B088
		public static FormatSwizzle GetSwizzleG(TextureFormat format)
		{
			return GraphicsFormatUtility.GetSwizzleG(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003767 RID: 14183
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FormatSwizzle GetSwizzleB(GraphicsFormat format);

		// Token: 0x06003768 RID: 14184 RVA: 0x0006CEA8 File Offset: 0x0006B0A8
		public static FormatSwizzle GetSwizzleB(TextureFormat format)
		{
			return GraphicsFormatUtility.GetSwizzleB(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003769 RID: 14185
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FormatSwizzle GetSwizzleA(GraphicsFormat format);

		// Token: 0x0600376A RID: 14186 RVA: 0x0006CEC8 File Offset: 0x0006B0C8
		public static FormatSwizzle GetSwizzleA(TextureFormat format)
		{
			return GraphicsFormatUtility.GetSwizzleA(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x0600376B RID: 14187
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetBlockSize(GraphicsFormat format);

		// Token: 0x0600376C RID: 14188 RVA: 0x0006CEE8 File Offset: 0x0006B0E8
		public static uint GetBlockSize(TextureFormat format)
		{
			return GraphicsFormatUtility.GetBlockSize(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x0600376D RID: 14189
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetBlockWidth(GraphicsFormat format);

		// Token: 0x0600376E RID: 14190 RVA: 0x0006CF08 File Offset: 0x0006B108
		public static uint GetBlockWidth(TextureFormat format)
		{
			return GraphicsFormatUtility.GetBlockWidth(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x0600376F RID: 14191
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetBlockHeight(GraphicsFormat format);

		// Token: 0x06003770 RID: 14192 RVA: 0x0006CF28 File Offset: 0x0006B128
		public static uint GetBlockHeight(TextureFormat format)
		{
			return GraphicsFormatUtility.GetBlockHeight(GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x0006CF48 File Offset: 0x0006B148
		public static uint ComputeMipmapSize(int width, int height, GraphicsFormat format)
		{
			return GraphicsFormatUtility.ComputeMipChainSize_Native_2D(width, height, format, 1);
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x0006CF64 File Offset: 0x0006B164
		public static uint ComputeMipmapSize(int width, int height, TextureFormat format)
		{
			return GraphicsFormatUtility.ComputeMipmapSize(width, height, GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003773 RID: 14195
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint ComputeMipChainSize_Native_2D(int width, int height, GraphicsFormat format, int mipCount);

		// Token: 0x06003774 RID: 14196 RVA: 0x0006CF84 File Offset: 0x0006B184
		public static uint ComputeMipChainSize(int width, int height, GraphicsFormat format, [DefaultValue("-1")] int mipCount = -1)
		{
			return GraphicsFormatUtility.ComputeMipChainSize_Native_2D(width, height, format, mipCount);
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x0006CFA0 File Offset: 0x0006B1A0
		public static uint ComputeMipChainSize(int width, int height, TextureFormat format, [DefaultValue("-1")] int mipCount = -1)
		{
			return GraphicsFormatUtility.ComputeMipChainSize_Native_2D(width, height, GraphicsFormatUtility.GetGraphicsFormat(format, false), mipCount);
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x0006CFC4 File Offset: 0x0006B1C4
		public static uint ComputeMipmapSize(int width, int height, int depth, GraphicsFormat format)
		{
			return GraphicsFormatUtility.ComputeMipChainSize_Native_3D(width, height, depth, format, 1);
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x0006CFE0 File Offset: 0x0006B1E0
		public static uint ComputeMipmapSize(int width, int height, int depth, TextureFormat format)
		{
			return GraphicsFormatUtility.ComputeMipmapSize(width, height, depth, GraphicsFormatUtility.GetGraphicsFormat(format, false));
		}

		// Token: 0x06003778 RID: 14200
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint ComputeMipChainSize_Native_3D(int width, int height, int depth, GraphicsFormat format, int mipCount);

		// Token: 0x06003779 RID: 14201 RVA: 0x0006D004 File Offset: 0x0006B204
		public static uint ComputeMipChainSize(int width, int height, int depth, GraphicsFormat format, [DefaultValue("-1")] int mipCount = -1)
		{
			return GraphicsFormatUtility.ComputeMipChainSize_Native_3D(width, height, depth, format, mipCount);
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x0006D024 File Offset: 0x0006B224
		public static uint ComputeMipChainSize(int width, int height, int depth, TextureFormat format, [DefaultValue("-1")] int mipCount = -1)
		{
			return GraphicsFormatUtility.ComputeMipChainSize_Native_3D(width, height, depth, GraphicsFormatUtility.GetGraphicsFormat(format, false), mipCount);
		}

		// Token: 0x0600377D RID: 14205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetFormat_Injected(IntPtr texture);

		// Token: 0x0600377E RID: 14206
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetFormatString_Injected(GraphicsFormat format, out ManagedSpanWrapper ret);

		// Token: 0x0600377F RID: 14207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetFormatString_Native_TextureFormat_Injected(TextureFormat format, out ManagedSpanWrapper ret);

		// Token: 0x040013B0 RID: 5040
		private static readonly GraphicsFormat[] tableNoStencil = new GraphicsFormat[]
		{
			GraphicsFormat.None,
			GraphicsFormat.D16_UNorm,
			GraphicsFormat.D16_UNorm,
			GraphicsFormat.D24_UNorm,
			GraphicsFormat.D32_SFloat
		};

		// Token: 0x040013B1 RID: 5041
		private static readonly GraphicsFormat[] tableStencil = new GraphicsFormat[]
		{
			GraphicsFormat.S8_UInt,
			GraphicsFormat.D16_UNorm_S8_UInt,
			GraphicsFormat.D16_UNorm_S8_UInt,
			GraphicsFormat.D24_UNorm_S8_UInt,
			GraphicsFormat.D32_SFloat_S8_UInt
		};
	}
}
