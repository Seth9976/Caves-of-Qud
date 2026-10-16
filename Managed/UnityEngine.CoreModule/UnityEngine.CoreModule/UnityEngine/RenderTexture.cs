using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001F9 RID: 505
	[NativeHeader("Runtime/Camera/Camera.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/RenderBufferManager.h")]
	public class RenderTexture : Texture
	{
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060019CA RID: 6602 RVA: 0x0002D440 File Offset: 0x0002B640
		// (set) Token: 0x060019CB RID: 6603 RVA: 0x0002D464 File Offset: 0x0002B664
		public override int width
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_width_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_width_Injected(intPtr, value);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x0002D488 File Offset: 0x0002B688
		// (set) Token: 0x060019CD RID: 6605 RVA: 0x0002D4AC File Offset: 0x0002B6AC
		public override int height
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_height_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_height_Injected(intPtr, value);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x0002D4D0 File Offset: 0x0002B6D0
		// (set) Token: 0x060019CF RID: 6607 RVA: 0x0002D4F4 File Offset: 0x0002B6F4
		public override TextureDimension dimension
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_dimension_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_dimension_Injected(intPtr, value);
			}
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0002D518 File Offset: 0x0002B718
		[NativeName("GetColorFormat")]
		private GraphicsFormat GetColorFormat(bool suppressWarnings)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RenderTexture.GetColorFormat_Injected(intPtr, suppressWarnings);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0002D53C File Offset: 0x0002B73C
		[NativeName("SetColorFormat")]
		private void SetColorFormat(GraphicsFormat format)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.SetColorFormat_Injected(intPtr, format);
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x0002D560 File Offset: 0x0002B760
		// (set) Token: 0x060019D3 RID: 6611 RVA: 0x0002D579 File Offset: 0x0002B779
		public new GraphicsFormat graphicsFormat
		{
			get
			{
				return this.GetColorFormat(true);
			}
			set
			{
				this.SetColorFormat(value);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x0002D584 File Offset: 0x0002B784
		// (set) Token: 0x060019D5 RID: 6613 RVA: 0x0002D5A8 File Offset: 0x0002B7A8
		[NativeProperty("MipMap")]
		public bool useMipMap
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_useMipMap_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_useMipMap_Injected(intPtr, value);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x0002D5CC File Offset: 0x0002B7CC
		[NativeProperty("SRGBReadWrite")]
		public bool sRGB
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_sRGB_Injected(intPtr);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x0002D5F0 File Offset: 0x0002B7F0
		// (set) Token: 0x060019D8 RID: 6616 RVA: 0x0002D614 File Offset: 0x0002B814
		[NativeProperty("VRUsage")]
		public VRTextureUsage vrUsage
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_vrUsage_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_vrUsage_Injected(intPtr, value);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x0002D638 File Offset: 0x0002B838
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x0002D65C File Offset: 0x0002B85C
		[NativeProperty("Memoryless")]
		public RenderTextureMemoryless memorylessMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_memorylessMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_memorylessMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x0002D680 File Offset: 0x0002B880
		// (set) Token: 0x060019DC RID: 6620 RVA: 0x0002D6C4 File Offset: 0x0002B8C4
		public RenderTextureFormat format
		{
			get
			{
				bool flag = this.graphicsFormat > GraphicsFormat.None;
				RenderTextureFormat renderTextureFormat;
				if (flag)
				{
					renderTextureFormat = GraphicsFormatUtility.GetRenderTextureFormat(this.graphicsFormat);
				}
				else
				{
					renderTextureFormat = ((this.GetDescriptor().shadowSamplingMode != ShadowSamplingMode.None) ? RenderTextureFormat.Shadowmap : RenderTextureFormat.Depth);
				}
				return renderTextureFormat;
			}
			set
			{
				bool flag = value == RenderTextureFormat.Depth || value == RenderTextureFormat.Shadowmap;
				if (flag)
				{
					bool flag2 = this.depthStencilFormat == GraphicsFormat.None;
					if (flag2)
					{
						RenderTexture.WarnAboutFallbackTo16BitsDepth(value);
						this.depthStencilFormat = GraphicsFormat.D16_UNorm;
					}
					bool flag3 = value == RenderTextureFormat.Shadowmap;
					if (flag3)
					{
						this.SetShadowSamplingMode(ShadowSamplingMode.CompareDepths);
					}
				}
				this.graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(value, this.sRGB);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x0002D728 File Offset: 0x0002B928
		// (set) Token: 0x060019DE RID: 6622 RVA: 0x0002D74C File Offset: 0x0002B94C
		public GraphicsFormat stencilFormat
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_stencilFormat_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_stencilFormat_Injected(intPtr, value);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x0002D770 File Offset: 0x0002B970
		// (set) Token: 0x060019E0 RID: 6624 RVA: 0x0002D794 File Offset: 0x0002B994
		public GraphicsFormat depthStencilFormat
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_depthStencilFormat_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_depthStencilFormat_Injected(intPtr, value);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x0002D7B8 File Offset: 0x0002B9B8
		// (set) Token: 0x060019E2 RID: 6626 RVA: 0x0002D7DC File Offset: 0x0002B9DC
		public bool autoGenerateMips
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_autoGenerateMips_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_autoGenerateMips_Injected(intPtr, value);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x0002D800 File Offset: 0x0002BA00
		// (set) Token: 0x060019E4 RID: 6628 RVA: 0x0002D824 File Offset: 0x0002BA24
		public int volumeDepth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_volumeDepth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_volumeDepth_Injected(intPtr, value);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x0002D848 File Offset: 0x0002BA48
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x0002D86C File Offset: 0x0002BA6C
		public int antiAliasing
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_antiAliasing_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_antiAliasing_Injected(intPtr, value);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x0002D890 File Offset: 0x0002BA90
		// (set) Token: 0x060019E8 RID: 6632 RVA: 0x0002D8B4 File Offset: 0x0002BAB4
		public bool bindTextureMS
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_bindTextureMS_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_bindTextureMS_Injected(intPtr, value);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x0002D8D8 File Offset: 0x0002BAD8
		// (set) Token: 0x060019EA RID: 6634 RVA: 0x0002D8FC File Offset: 0x0002BAFC
		public bool enableRandomWrite
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_enableRandomWrite_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_enableRandomWrite_Injected(intPtr, value);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x0002D920 File Offset: 0x0002BB20
		// (set) Token: 0x060019EC RID: 6636 RVA: 0x0002D944 File Offset: 0x0002BB44
		public bool useDynamicScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_useDynamicScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_useDynamicScale_Injected(intPtr, value);
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x0002D968 File Offset: 0x0002BB68
		// (set) Token: 0x060019EE RID: 6638 RVA: 0x0002D98C File Offset: 0x0002BB8C
		public bool useDynamicScaleExplicit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_useDynamicScaleExplicit_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_useDynamicScaleExplicit_Injected(intPtr, value);
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0002D9B0 File Offset: 0x0002BBB0
		public void ApplyDynamicScale()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.ApplyDynamicScale_Injected(intPtr);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0002D9D4 File Offset: 0x0002BBD4
		private bool GetIsPowerOfTwo()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RenderTexture.GetIsPowerOfTwo_Injected(intPtr);
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x0002D9F8 File Offset: 0x0002BBF8
		// (set) Token: 0x060019F2 RID: 6642 RVA: 0x00002925 File Offset: 0x00000B25
		public bool isPowerOfTwo
		{
			get
			{
				return this.GetIsPowerOfTwo();
			}
			set
			{
			}
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0002DA10 File Offset: 0x0002BC10
		[FreeFunction("RenderTexture::GetActiveAsRenderTexture")]
		private static RenderTexture GetActive()
		{
			return Unmarshal.UnmarshalUnityObject<RenderTexture>(RenderTexture.GetActive_Injected());
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0002DA28 File Offset: 0x0002BC28
		[FreeFunction("RenderTextureScripting::SetActive")]
		private static void SetActive(RenderTexture rt)
		{
			RenderTexture.SetActive_Injected(Object.MarshalledUnityObject.Marshal<RenderTexture>(rt));
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x0002DA40 File Offset: 0x0002BC40
		// (set) Token: 0x060019F6 RID: 6646 RVA: 0x0002DA57 File Offset: 0x0002BC57
		public static RenderTexture active
		{
			get
			{
				return RenderTexture.GetActive();
			}
			set
			{
				RenderTexture.SetActive(value);
			}
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0002DA64 File Offset: 0x0002BC64
		[FreeFunction(Name = "RenderTextureScripting::GetColorBuffer", HasExplicitThis = true)]
		private RenderBuffer GetColorBuffer()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderBuffer renderBuffer;
			RenderTexture.GetColorBuffer_Injected(intPtr, out renderBuffer);
			return renderBuffer;
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0002DA8C File Offset: 0x0002BC8C
		[FreeFunction(Name = "RenderTextureScripting::GetDepthBuffer", HasExplicitThis = true)]
		private RenderBuffer GetDepthBuffer()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderBuffer renderBuffer;
			RenderTexture.GetDepthBuffer_Injected(intPtr, out renderBuffer);
			return renderBuffer;
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0002DAB4 File Offset: 0x0002BCB4
		private void SetMipMapCount(int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.SetMipMapCount_Injected(intPtr, count);
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x0002DAD8 File Offset: 0x0002BCD8
		internal void SetShadowSamplingMode(ShadowSamplingMode samplingMode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.SetShadowSamplingMode_Injected(intPtr, samplingMode);
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x0002DAFC File Offset: 0x0002BCFC
		public RenderBuffer colorBuffer
		{
			get
			{
				return this.GetColorBuffer();
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x0002DB14 File Offset: 0x0002BD14
		public RenderBuffer depthBuffer
		{
			get
			{
				return this.GetDepthBuffer();
			}
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0002DB2C File Offset: 0x0002BD2C
		public IntPtr GetNativeDepthBufferPtr()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RenderTexture.GetNativeDepthBufferPtr_Injected(intPtr);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0002DB50 File Offset: 0x0002BD50
		public void DiscardContents(bool discardColor, bool discardDepth)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.DiscardContents_Injected(intPtr, discardColor, discardDepth);
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0002DB74 File Offset: 0x0002BD74
		[Obsolete("This function has no effect.", false)]
		public void MarkRestoreExpected()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.MarkRestoreExpected_Injected(intPtr);
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0002DB96 File Offset: 0x0002BD96
		public void DiscardContents()
		{
			this.DiscardContents(true, true);
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x0002DBA4 File Offset: 0x0002BDA4
		[NativeName("ResolveAntiAliasedSurface")]
		private void ResolveAA()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.ResolveAA_Injected(intPtr);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
		[NativeName("ResolveAntiAliasedSurface")]
		private void ResolveAATo(RenderTexture rt)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.ResolveAATo_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(rt));
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0002DBF0 File Offset: 0x0002BDF0
		public void ResolveAntiAliasedSurface()
		{
			this.ResolveAA();
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0002DBFA File Offset: 0x0002BDFA
		public void ResolveAntiAliasedSurface(RenderTexture target)
		{
			this.ResolveAATo(target);
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0002DC08 File Offset: 0x0002BE08
		[FreeFunction(Name = "RenderTextureScripting::SetGlobalShaderProperty", HasExplicitThis = true)]
		public unsafe void SetGlobalShaderProperty(string propertyName)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(propertyName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = propertyName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				RenderTexture.SetGlobalShaderProperty_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0002DC6C File Offset: 0x0002BE6C
		public bool Create()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RenderTexture.Create_Injected(intPtr);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x0002DC90 File Offset: 0x0002BE90
		public void Release()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.Release_Injected(intPtr);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0002DCB4 File Offset: 0x0002BEB4
		public bool IsCreated()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RenderTexture.IsCreated_Injected(intPtr);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0002DCD8 File Offset: 0x0002BED8
		public void GenerateMips()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.GenerateMips_Injected(intPtr);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0002DCFC File Offset: 0x0002BEFC
		[NativeThrows]
		public void ConvertToEquirect(RenderTexture equirect, Camera.MonoOrStereoscopicEye eye = Camera.MonoOrStereoscopicEye.Mono)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.ConvertToEquirect_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(equirect), eye);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0002DD28 File Offset: 0x0002BF28
		internal void SetSRGBReadWrite(bool srgb)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.SetSRGBReadWrite_Injected(intPtr, srgb);
		}

		// Token: 0x06001A0C RID: 6668
		[FreeFunction("RenderTextureScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] RenderTexture rt);

		// Token: 0x06001A0D RID: 6669 RVA: 0x0002DD4C File Offset: 0x0002BF4C
		[FreeFunction("RenderTextureSupportsStencil")]
		public static bool SupportsStencil(RenderTexture rt)
		{
			return RenderTexture.SupportsStencil_Injected(Object.MarshalledUnityObject.Marshal<RenderTexture>(rt));
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0002DD64 File Offset: 0x0002BF64
		[NativeName("SetRenderTextureDescFromScript")]
		private void SetRenderTextureDescriptor(RenderTextureDescriptor desc)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTexture.SetRenderTextureDescriptor_Injected(intPtr, ref desc);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0002DD88 File Offset: 0x0002BF88
		[NativeName("GetRenderTextureDesc")]
		private RenderTextureDescriptor GetDescriptor()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RenderTextureDescriptor renderTextureDescriptor;
			RenderTexture.GetDescriptor_Injected(intPtr, out renderTextureDescriptor);
			return renderTextureDescriptor;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0002DDB0 File Offset: 0x0002BFB0
		[FreeFunction("GetRenderBufferManager().GetTextures().GetTempBuffer")]
		private static RenderTexture GetTemporary_Internal(RenderTextureDescriptor desc)
		{
			return Unmarshal.UnmarshalUnityObject<RenderTexture>(RenderTexture.GetTemporary_Internal_Injected(ref desc));
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0002DDCC File Offset: 0x0002BFCC
		[FreeFunction("GetRenderBufferManager().GetTextures().ReleaseTempBuffer")]
		public static void ReleaseTemporary(RenderTexture temp)
		{
			RenderTexture.ReleaseTemporary_Injected(Object.MarshalledUnityObject.Marshal<RenderTexture>(temp));
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x0002DDE4 File Offset: 0x0002BFE4
		// (set) Token: 0x06001A13 RID: 6675 RVA: 0x0002DE08 File Offset: 0x0002C008
		public int depth
		{
			[FreeFunction("RenderTextureScripting::GetDepth", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RenderTexture.get_depth_Injected(intPtr);
			}
			[FreeFunction("RenderTextureScripting::SetDepth", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RenderTexture.set_depth_Injected(intPtr, value);
			}
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0002DE2B File Offset: 0x0002C02B
		[RequiredByNativeCode]
		protected internal RenderTexture()
		{
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x0002DE35 File Offset: 0x0002C035
		public RenderTexture(RenderTextureDescriptor desc)
		{
			RenderTexture.ValidateRenderTextureDesc(ref desc);
			RenderTexture.Internal_Create(this);
			this.SetRenderTextureDescriptor(desc);
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0002DE58 File Offset: 0x0002C058
		public RenderTexture(RenderTexture textureToCopy)
		{
			bool flag = textureToCopy == null;
			if (flag)
			{
				throw new ArgumentNullException("textureToCopy");
			}
			RenderTextureDescriptor descriptor = textureToCopy.descriptor;
			RenderTexture.ValidateRenderTextureDesc(ref descriptor);
			RenderTexture.Internal_Create(this);
			this.SetRenderTextureDescriptor(descriptor);
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0002DEA4 File Offset: 0x0002C0A4
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, DefaultFormat format)
			: this(width, height, RenderTexture.GetDefaultColorFormat(format), RenderTexture.GetDefaultDepthStencilFormat(format, depth), Texture.GenerateAllMips)
		{
			bool flag = this != null;
			if (flag)
			{
				this.SetShadowSamplingMode(RenderTexture.GetShadowSamplingModeForFormat(format));
			}
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0002DEE8 File Offset: 0x0002C0E8
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, GraphicsFormat format)
			: this(width, height, depth, format, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0002DEFC File Offset: 0x0002C0FC
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, GraphicsFormat format, int mipCount)
		{
			bool flag = format != GraphicsFormat.None && !base.ValidateFormat(format, GraphicsFormatUsage.Render);
			if (!flag)
			{
				RenderTexture.Internal_Create(this);
				this.depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(depth, format);
				this.width = width;
				this.height = height;
				this.graphicsFormat = format;
				this.SetMipMapCount(mipCount);
				this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(format));
			}
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0002DF74 File Offset: 0x0002C174
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat, int mipCount)
		{
			bool flag = colorFormat != GraphicsFormat.None && !base.ValidateFormat(colorFormat, GraphicsFormatUsage.Render);
			if (!flag)
			{
				RenderTexture.Internal_Create(this);
				this.width = width;
				this.height = height;
				this.depthStencilFormat = depthStencilFormat;
				this.graphicsFormat = colorFormat;
				this.SetMipMapCount(mipCount);
				this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(colorFormat));
			}
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0002DFDF File Offset: 0x0002C1DF
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat)
			: this(width, height, colorFormat, depthStencilFormat, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0002DFF3 File Offset: 0x0002C1F3
		public RenderTexture(int width, int height, int depth, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite)
		{
			this.Initialize(width, height, depth, format, readWrite, Texture.GenerateAllMips);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0002E010 File Offset: 0x0002C210
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format)
			: this(width, height, depth, format, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0002E024 File Offset: 0x0002C224
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth)
			: this(width, height, depth, RenderTextureFormat.Default)
		{
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0002E032 File Offset: 0x0002C232
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format, int mipCount)
		{
			this.Initialize(width, height, depth, format, RenderTextureReadWrite.Default, mipCount);
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0002E04C File Offset: 0x0002C24C
		private void Initialize(int width, int height, int depth, RenderTextureFormat format, RenderTextureReadWrite readWrite, int mipCount)
		{
			GraphicsFormat compatibleFormat = RenderTexture.GetCompatibleFormat(format, readWrite);
			GraphicsFormat depthStencilFormatLegacy = RenderTexture.GetDepthStencilFormatLegacy(depth, format, false);
			bool flag = compatibleFormat > GraphicsFormat.None;
			if (flag)
			{
				bool flag2 = !base.ValidateFormat(compatibleFormat, GraphicsFormatUsage.Render);
				if (flag2)
				{
					return;
				}
			}
			RenderTexture.Internal_Create(this);
			this.width = width;
			this.height = height;
			this.depthStencilFormat = depthStencilFormatLegacy;
			this.graphicsFormat = compatibleFormat;
			this.SetMipMapCount(mipCount);
			this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(compatibleFormat));
			this.SetShadowSamplingMode(RenderTexture.GetShadowSamplingModeForFormat(format));
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0002E0D8 File Offset: 0x0002C2D8
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, GraphicsFormat colorFormat)
		{
			return RenderTexture.GetDepthStencilFormatLegacy(depthBits, false);
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0002E0F4 File Offset: 0x0002C2F4
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, RenderTextureFormat format, bool disableFallback = false)
		{
			bool flag = !disableFallback && (format == RenderTextureFormat.Depth || format == RenderTextureFormat.Shadowmap) && depthBits < 16;
			if (flag)
			{
				RenderTexture.WarnAboutFallbackTo16BitsDepth(format);
				depthBits = 16;
			}
			return RenderTexture.GetDepthStencilFormatLegacy(depthBits, format == RenderTextureFormat.Shadowmap);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0002E134 File Offset: 0x0002C334
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, DefaultFormat format)
		{
			return RenderTexture.GetDepthStencilFormatLegacy(depthBits, format == DefaultFormat.Shadow);
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0002E150 File Offset: 0x0002C350
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, ShadowSamplingMode shadowSamplingMode)
		{
			return RenderTexture.GetDepthStencilFormatLegacy(depthBits, shadowSamplingMode != ShadowSamplingMode.None);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0002E170 File Offset: 0x0002C370
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, bool requestedShadowMap)
		{
			GraphicsFormat graphicsFormat = (requestedShadowMap ? GraphicsFormatUtility.GetDepthStencilFormat(depthBits, 0) : GraphicsFormatUtility.GetDepthStencilFormat(depthBits));
			bool flag = depthBits > 16 && graphicsFormat == GraphicsFormat.None && requestedShadowMap;
			GraphicsFormat graphicsFormat2;
			if (flag)
			{
				Debug.LogWarning(string.Format("No compatible shadow map depth format with {0} or more depth bits has been found. Changing to a 16 bit depth buffer.", depthBits));
				graphicsFormat2 = GraphicsFormat.D16_UNorm;
			}
			else
			{
				graphicsFormat2 = graphicsFormat;
			}
			return graphicsFormat2;
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x0002E1C4 File Offset: 0x0002C3C4
		// (set) Token: 0x06001A27 RID: 6695 RVA: 0x0002E1DC File Offset: 0x0002C3DC
		public RenderTextureDescriptor descriptor
		{
			get
			{
				return this.GetDescriptor();
			}
			set
			{
				RenderTexture.ValidateRenderTextureDesc(ref value);
				this.SetRenderTextureDescriptor(value);
			}
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0002E1F0 File Offset: 0x0002C3F0
		private static void ValidateRenderTextureDesc(ref RenderTextureDescriptor desc)
		{
			bool flag = desc.graphicsFormat == GraphicsFormat.None && desc.depthStencilFormat == GraphicsFormat.None;
			if (flag)
			{
				RenderTexture.WarnAboutFallbackTo16BitsDepth(desc.colorFormat);
				desc.depthStencilFormat = GraphicsFormat.D16_UNorm;
			}
			bool flag2 = desc.graphicsFormat != GraphicsFormat.None && !SystemInfo.IsFormatSupported(desc.graphicsFormat, GraphicsFormatUsage.Render);
			if (flag2)
			{
				throw new ArgumentException("RenderTextureDesc graphicsFormat must be a supported GraphicsFormat. " + desc.graphicsFormat.ToString() + " is not supported on this platform.", "desc.graphicsFormat");
			}
			bool flag3 = desc.depthStencilFormat != GraphicsFormat.None && !GraphicsFormatUtility.IsDepthStencilFormat(desc.depthStencilFormat);
			if (flag3)
			{
				throw new ArgumentException("RenderTextureDesc depthStencilFormat must be a supported depth/stencil GraphicsFormat. " + desc.depthStencilFormat.ToString() + " is not supported on this platform.", "desc.depthStencilFormat");
			}
			bool flag4 = desc.width <= 0;
			if (flag4)
			{
				throw new ArgumentException("RenderTextureDesc width must be greater than zero.", "desc.width");
			}
			bool flag5 = desc.height <= 0;
			if (flag5)
			{
				throw new ArgumentException("RenderTextureDesc height must be greater than zero.", "desc.height");
			}
			bool flag6 = desc.volumeDepth <= 0;
			if (flag6)
			{
				throw new ArgumentException("RenderTextureDesc volumeDepth must be greater than zero.", "desc.volumeDepth");
			}
			bool flag7 = desc.msaaSamples != 1 && desc.msaaSamples != 2 && desc.msaaSamples != 4 && desc.msaaSamples != 8;
			if (flag7)
			{
				throw new ArgumentException("RenderTextureDesc msaaSamples must be 1, 2, 4, or 8.", "desc.msaaSamples");
			}
			bool flag8 = desc.dimension == TextureDimension.CubeArray && desc.volumeDepth % 6 != 0;
			if (flag8)
			{
				throw new ArgumentException("RenderTextureDesc volumeDepth must be a multiple of 6 when dimension is CubeArray", "desc.volumeDepth");
			}
			bool flag9 = GraphicsFormatUtility.IsDepthStencilFormat(desc.graphicsFormat);
			if (flag9)
			{
				throw new ArgumentException("RenderTextureDesc graphicsFormat must not be a depth/stencil format. " + desc.graphicsFormat.ToString() + " is not supported.", "desc.graphicsFormat");
			}
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0002E3D8 File Offset: 0x0002C5D8
		internal static GraphicsFormat GetDefaultColorFormat(DefaultFormat format)
		{
			GraphicsFormat graphicsFormat;
			if (format - DefaultFormat.DepthStencil > 1)
			{
				graphicsFormat = SystemInfo.GetGraphicsFormat(format);
			}
			else
			{
				graphicsFormat = GraphicsFormat.None;
			}
			return graphicsFormat;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0002E400 File Offset: 0x0002C600
		internal static GraphicsFormat GetDefaultDepthStencilFormat(DefaultFormat format, int depth)
		{
			GraphicsFormat graphicsFormat;
			if (format - DefaultFormat.DepthStencil > 1)
			{
				graphicsFormat = RenderTexture.GetDepthStencilFormatLegacy(depth, format);
			}
			else
			{
				graphicsFormat = SystemInfo.GetGraphicsFormat(format);
			}
			return graphicsFormat;
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0002E430 File Offset: 0x0002C630
		internal static ShadowSamplingMode GetShadowSamplingModeForFormat(RenderTextureFormat format)
		{
			return (format == RenderTextureFormat.Shadowmap) ? ShadowSamplingMode.CompareDepths : ShadowSamplingMode.None;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0002E44C File Offset: 0x0002C64C
		internal static ShadowSamplingMode GetShadowSamplingModeForFormat(DefaultFormat format)
		{
			return (format == DefaultFormat.Shadow) ? ShadowSamplingMode.CompareDepths : ShadowSamplingMode.None;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0002E466 File Offset: 0x0002C666
		internal static void WarnAboutFallbackTo16BitsDepth(RenderTextureFormat format)
		{
			Debug.LogWarning(string.Format("{0} RenderTexture requested without a depth buffer. Changing to a 16 bit depth buffer. To resolve this warning, please specify the desired number of depth bits when creating the render texture.", format));
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0002E480 File Offset: 0x0002C680
		internal static GraphicsFormat GetCompatibleFormat(RenderTextureFormat renderTextureFormat, RenderTextureReadWrite readWrite)
		{
			GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(renderTextureFormat, readWrite);
			GraphicsFormat compatibleFormat = SystemInfo.GetCompatibleFormat(graphicsFormat, GraphicsFormatUsage.Render);
			bool flag = graphicsFormat == compatibleFormat;
			GraphicsFormat graphicsFormat2;
			if (flag)
			{
				graphicsFormat2 = graphicsFormat;
			}
			else
			{
				Debug.LogWarning(string.Format("'{0}' is not supported. RenderTexture::GetTemporary fallbacks to {1} format on this platform. Use 'SystemInfo.IsFormatSupported' C# API to check format support.", graphicsFormat.ToString(), compatibleFormat.ToString()));
				graphicsFormat2 = compatibleFormat;
			}
			return graphicsFormat2;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0002E4DC File Offset: 0x0002C6DC
		public static RenderTexture GetTemporary(RenderTextureDescriptor desc)
		{
			RenderTexture.ValidateRenderTextureDesc(ref desc);
			desc.createdFromScript = true;
			return RenderTexture.GetTemporary_Internal(desc);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0002E508 File Offset: 0x0002C708
		private static RenderTexture GetTemporaryImpl(int width, int height, GraphicsFormat depthStencilFormat, GraphicsFormat colorFormat, int antiAliasing = 1, RenderTextureMemoryless memorylessMode = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, bool useDynamicScale = false, ShadowSamplingMode shadowSamplingMode = ShadowSamplingMode.None)
		{
			return RenderTexture.GetTemporary(new RenderTextureDescriptor(width, height, colorFormat, depthStencilFormat)
			{
				msaaSamples = antiAliasing,
				memoryless = memorylessMode,
				vrUsage = vrUsage,
				useDynamicScale = useDynamicScale,
				shadowSamplingMode = shadowSamplingMode
			});
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0002E560 File Offset: 0x0002C760
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, [DefaultValue("1")] int antiAliasing, [DefaultValue("RenderTextureMemoryless.None")] RenderTextureMemoryless memorylessMode, [DefaultValue("VRTextureUsage.None")] VRTextureUsage vrUsage, [DefaultValue("false")] bool useDynamicScale)
		{
			ShadowSamplingMode shadowSamplingMode = ShadowSamplingMode.None;
			return RenderTexture.GetTemporaryImpl(width, height, RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, shadowSamplingMode), format, antiAliasing, memorylessMode, vrUsage, useDynamicScale, ShadowSamplingMode.None);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0002E58C File Offset: 0x0002C78C
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing, RenderTextureMemoryless memorylessMode, VRTextureUsage vrUsage)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, antiAliasing, memorylessMode, vrUsage, false);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0002E5B0 File Offset: 0x0002C7B0
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing, RenderTextureMemoryless memorylessMode)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, antiAliasing, memorylessMode, VRTextureUsage.None);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0002E5D0 File Offset: 0x0002C7D0
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, antiAliasing, RenderTextureMemoryless.None);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0002E5F0 File Offset: 0x0002C7F0
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, 1);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0002E60C File Offset: 0x0002C80C
		public static RenderTexture GetTemporary(int width, int height, [DefaultValue("0")] int depthBuffer, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite, [DefaultValue("1")] int antiAliasing, [DefaultValue("RenderTextureMemoryless.None")] RenderTextureMemoryless memorylessMode, [DefaultValue("VRTextureUsage.None")] VRTextureUsage vrUsage, [DefaultValue("false")] bool useDynamicScale)
		{
			GraphicsFormat compatibleFormat = RenderTexture.GetCompatibleFormat(format, readWrite);
			GraphicsFormat depthStencilFormatLegacy = RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, format, false);
			ShadowSamplingMode shadowSamplingModeForFormat = RenderTexture.GetShadowSamplingModeForFormat(format);
			return RenderTexture.GetTemporaryImpl(width, height, depthStencilFormatLegacy, compatibleFormat, antiAliasing, memorylessMode, vrUsage, useDynamicScale, shadowSamplingModeForFormat);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0002E64C File Offset: 0x0002C84C
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, RenderTextureMemoryless memorylessMode, VRTextureUsage vrUsage)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing, memorylessMode, vrUsage, false);
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0002E670 File Offset: 0x0002C870
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, RenderTextureMemoryless memorylessMode)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing, memorylessMode, VRTextureUsage.None);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0002E694 File Offset: 0x0002C894
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing, RenderTextureMemoryless.None);
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0002E6B4 File Offset: 0x0002C8B4
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, 1);
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0002E6D4 File Offset: 0x0002C8D4
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, RenderTextureReadWrite.Default);
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x0002E6F0 File Offset: 0x0002C8F0
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, RenderTextureFormat.Default);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0002E70C File Offset: 0x0002C90C
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height)
		{
			return RenderTexture.GetTemporary(width, height, 0);
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x0002E728 File Offset: 0x0002C928
		// (set) Token: 0x06001A3F RID: 6719 RVA: 0x0002E743 File Offset: 0x0002C943
		[Obsolete("Use RenderTexture.dimension instead.", false)]
		public bool isCubemap
		{
			get
			{
				return this.dimension == TextureDimension.Cube;
			}
			set
			{
				this.dimension = (value ? TextureDimension.Cube : TextureDimension.Tex2D);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x0002E754 File Offset: 0x0002C954
		// (set) Token: 0x06001A41 RID: 6721 RVA: 0x0002E76F File Offset: 0x0002C96F
		[Obsolete("Use RenderTexture.dimension instead.", false)]
		public bool isVolume
		{
			get
			{
				return this.dimension == TextureDimension.Tex3D;
			}
			set
			{
				this.dimension = (value ? TextureDimension.Tex3D : TextureDimension.Tex2D);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x0002E780 File Offset: 0x0002C980
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x00002925 File Offset: 0x00000B25
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("RenderTexture.enabled is always now, no need to use it.", false)]
		public static bool enabled
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x0002E794 File Offset: 0x0002C994
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("GetTexelOffset always returns zero now, no point in using it.", false)]
		public Vector2 GetTexelOffset()
		{
			return Vector2.zero;
		}

		// Token: 0x06001A45 RID: 6725
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_width_Injected(IntPtr _unity_self);

		// Token: 0x06001A46 RID: 6726
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_width_Injected(IntPtr _unity_self, int value);

		// Token: 0x06001A47 RID: 6727
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_height_Injected(IntPtr _unity_self);

		// Token: 0x06001A48 RID: 6728
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_height_Injected(IntPtr _unity_self, int value);

		// Token: 0x06001A49 RID: 6729
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureDimension get_dimension_Injected(IntPtr _unity_self);

		// Token: 0x06001A4A RID: 6730
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_dimension_Injected(IntPtr _unity_self, TextureDimension value);

		// Token: 0x06001A4B RID: 6731
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat GetColorFormat_Injected(IntPtr _unity_self, bool suppressWarnings);

		// Token: 0x06001A4C RID: 6732
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetColorFormat_Injected(IntPtr _unity_self, GraphicsFormat format);

		// Token: 0x06001A4D RID: 6733
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useMipMap_Injected(IntPtr _unity_self);

		// Token: 0x06001A4E RID: 6734
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useMipMap_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001A4F RID: 6735
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_sRGB_Injected(IntPtr _unity_self);

		// Token: 0x06001A50 RID: 6736
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VRTextureUsage get_vrUsage_Injected(IntPtr _unity_self);

		// Token: 0x06001A51 RID: 6737
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_vrUsage_Injected(IntPtr _unity_self, VRTextureUsage value);

		// Token: 0x06001A52 RID: 6738
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderTextureMemoryless get_memorylessMode_Injected(IntPtr _unity_self);

		// Token: 0x06001A53 RID: 6739
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_memorylessMode_Injected(IntPtr _unity_self, RenderTextureMemoryless value);

		// Token: 0x06001A54 RID: 6740
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat get_stencilFormat_Injected(IntPtr _unity_self);

		// Token: 0x06001A55 RID: 6741
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stencilFormat_Injected(IntPtr _unity_self, GraphicsFormat value);

		// Token: 0x06001A56 RID: 6742
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsFormat get_depthStencilFormat_Injected(IntPtr _unity_self);

		// Token: 0x06001A57 RID: 6743
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_depthStencilFormat_Injected(IntPtr _unity_self, GraphicsFormat value);

		// Token: 0x06001A58 RID: 6744
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoGenerateMips_Injected(IntPtr _unity_self);

		// Token: 0x06001A59 RID: 6745
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoGenerateMips_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001A5A RID: 6746
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_volumeDepth_Injected(IntPtr _unity_self);

		// Token: 0x06001A5B RID: 6747
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_volumeDepth_Injected(IntPtr _unity_self, int value);

		// Token: 0x06001A5C RID: 6748
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_antiAliasing_Injected(IntPtr _unity_self);

		// Token: 0x06001A5D RID: 6749
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_antiAliasing_Injected(IntPtr _unity_self, int value);

		// Token: 0x06001A5E RID: 6750
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_bindTextureMS_Injected(IntPtr _unity_self);

		// Token: 0x06001A5F RID: 6751
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bindTextureMS_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001A60 RID: 6752
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableRandomWrite_Injected(IntPtr _unity_self);

		// Token: 0x06001A61 RID: 6753
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableRandomWrite_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001A62 RID: 6754
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useDynamicScale_Injected(IntPtr _unity_self);

		// Token: 0x06001A63 RID: 6755
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useDynamicScale_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001A64 RID: 6756
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useDynamicScaleExplicit_Injected(IntPtr _unity_self);

		// Token: 0x06001A65 RID: 6757
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useDynamicScaleExplicit_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001A66 RID: 6758
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ApplyDynamicScale_Injected(IntPtr _unity_self);

		// Token: 0x06001A67 RID: 6759
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIsPowerOfTwo_Injected(IntPtr _unity_self);

		// Token: 0x06001A68 RID: 6760
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetActive_Injected();

		// Token: 0x06001A69 RID: 6761
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetActive_Injected(IntPtr rt);

		// Token: 0x06001A6A RID: 6762
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetColorBuffer_Injected(IntPtr _unity_self, out RenderBuffer ret);

		// Token: 0x06001A6B RID: 6763
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDepthBuffer_Injected(IntPtr _unity_self, out RenderBuffer ret);

		// Token: 0x06001A6C RID: 6764
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMipMapCount_Injected(IntPtr _unity_self, int count);

		// Token: 0x06001A6D RID: 6765
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetShadowSamplingMode_Injected(IntPtr _unity_self, ShadowSamplingMode samplingMode);

		// Token: 0x06001A6E RID: 6766
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetNativeDepthBufferPtr_Injected(IntPtr _unity_self);

		// Token: 0x06001A6F RID: 6767
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DiscardContents_Injected(IntPtr _unity_self, bool discardColor, bool discardDepth);

		// Token: 0x06001A70 RID: 6768
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MarkRestoreExpected_Injected(IntPtr _unity_self);

		// Token: 0x06001A71 RID: 6769
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveAA_Injected(IntPtr _unity_self);

		// Token: 0x06001A72 RID: 6770
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveAATo_Injected(IntPtr _unity_self, IntPtr rt);

		// Token: 0x06001A73 RID: 6771
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalShaderProperty_Injected(IntPtr _unity_self, ref ManagedSpanWrapper propertyName);

		// Token: 0x06001A74 RID: 6772
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Create_Injected(IntPtr _unity_self);

		// Token: 0x06001A75 RID: 6773
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Release_Injected(IntPtr _unity_self);

		// Token: 0x06001A76 RID: 6774
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsCreated_Injected(IntPtr _unity_self);

		// Token: 0x06001A77 RID: 6775
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GenerateMips_Injected(IntPtr _unity_self);

		// Token: 0x06001A78 RID: 6776
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ConvertToEquirect_Injected(IntPtr _unity_self, IntPtr equirect, Camera.MonoOrStereoscopicEye eye);

		// Token: 0x06001A79 RID: 6777
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSRGBReadWrite_Injected(IntPtr _unity_self, bool srgb);

		// Token: 0x06001A7A RID: 6778
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsStencil_Injected(IntPtr rt);

		// Token: 0x06001A7B RID: 6779
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTextureDescriptor_Injected(IntPtr _unity_self, [In] ref RenderTextureDescriptor desc);

		// Token: 0x06001A7C RID: 6780
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDescriptor_Injected(IntPtr _unity_self, out RenderTextureDescriptor ret);

		// Token: 0x06001A7D RID: 6781
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTemporary_Internal_Injected([In] ref RenderTextureDescriptor desc);

		// Token: 0x06001A7E RID: 6782
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseTemporary_Injected(IntPtr temp);

		// Token: 0x06001A7F RID: 6783
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_depth_Injected(IntPtr _unity_self);

		// Token: 0x06001A80 RID: 6784
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_depth_Injected(IntPtr _unity_self, int value);
	}
}
