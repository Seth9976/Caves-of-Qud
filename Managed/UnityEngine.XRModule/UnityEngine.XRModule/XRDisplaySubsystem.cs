using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000018 RID: 24
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystem.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeConditional("ENABLE_XR")]
	public class XRDisplaySubsystem : IntegratedSubsystem<XRDisplaySubsystemDescriptor>
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060000F5 RID: 245 RVA: 0x00004B08 File Offset: 0x00002D08
		// (remove) Token: 0x060000F6 RID: 246 RVA: 0x00004B40 File Offset: 0x00002D40
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<bool> displayFocusChanged;

		// Token: 0x060000F7 RID: 247 RVA: 0x00004B78 File Offset: 0x00002D78
		[RequiredByNativeCode]
		private void InvokeDisplayFocusChanged(bool focus)
		{
			bool flag = this.displayFocusChanged != null;
			if (flag)
			{
				this.displayFocusChanged(focus);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004BA0 File Offset: 0x00002DA0
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00004BC0 File Offset: 0x00002DC0
		[Obsolete("singlePassRenderingDisabled{get;set;} is deprecated. Use textureLayout and supportedTextureLayouts instead.", false)]
		public bool singlePassRenderingDisabled
		{
			get
			{
				return (this.textureLayout & XRDisplaySubsystem.TextureLayout.Texture2DArray) == (XRDisplaySubsystem.TextureLayout)0;
			}
			set
			{
				if (value)
				{
					this.textureLayout = XRDisplaySubsystem.TextureLayout.SeparateTexture2Ds;
				}
				else
				{
					bool flag = (this.supportedTextureLayouts & XRDisplaySubsystem.TextureLayout.Texture2DArray) > (XRDisplaySubsystem.TextureLayout)0;
					if (flag)
					{
						this.textureLayout = XRDisplaySubsystem.TextureLayout.Texture2DArray;
					}
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public bool displayOpaque
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_displayOpaque_Injected(intPtr);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004C1C File Offset: 0x00002E1C
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00004C40 File Offset: 0x00002E40
		public bool contentProtectionEnabled
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_contentProtectionEnabled_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_contentProtectionEnabled_Injected(intPtr, value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004C64 File Offset: 0x00002E64
		public float appliedViewportScale
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_appliedViewportScale_Injected(intPtr);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004C88 File Offset: 0x00002E88
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00004CAC File Offset: 0x00002EAC
		public float scaleOfAllViewports
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_scaleOfAllViewports_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_scaleOfAllViewports_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004CD0 File Offset: 0x00002ED0
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00004CF4 File Offset: 0x00002EF4
		public float scaleOfAllRenderTargets
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_scaleOfAllRenderTargets_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_scaleOfAllRenderTargets_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004D18 File Offset: 0x00002F18
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00004D3C File Offset: 0x00002F3C
		public float zNear
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_zNear_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_zNear_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004D60 File Offset: 0x00002F60
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00004D84 File Offset: 0x00002F84
		public float zFar
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_zFar_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_zFar_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004DA8 File Offset: 0x00002FA8
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00004DCC File Offset: 0x00002FCC
		public bool sRGB
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_sRGB_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_sRGB_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004DF0 File Offset: 0x00002FF0
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00004E14 File Offset: 0x00003014
		public float occlusionMaskScale
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_occlusionMaskScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_occlusionMaskScale_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004E38 File Offset: 0x00003038
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00004E5C File Offset: 0x0000305C
		public float foveatedRenderingLevel
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_foveatedRenderingLevel_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_foveatedRenderingLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004E80 File Offset: 0x00003080
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00004EA4 File Offset: 0x000030A4
		public XRDisplaySubsystem.FoveatedRenderingFlags foveatedRenderingFlags
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_foveatedRenderingFlags_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_foveatedRenderingFlags_Injected(intPtr, value);
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004EC8 File Offset: 0x000030C8
		public void MarkTransformLateLatched(Transform transform, XRDisplaySubsystem.LateLatchNode nodeType)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			XRDisplaySubsystem.MarkTransformLateLatched_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Transform>(transform), nodeType);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00004EF4 File Offset: 0x000030F4
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00004F18 File Offset: 0x00003118
		public XRDisplaySubsystem.TextureLayout textureLayout
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_textureLayout_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_textureLayout_Injected(intPtr, value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004F3C File Offset: 0x0000313C
		public XRDisplaySubsystem.TextureLayout supportedTextureLayouts
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_supportedTextureLayouts_Injected(intPtr);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004F60 File Offset: 0x00003160
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00004F84 File Offset: 0x00003184
		public XRDisplaySubsystem.ReprojectionMode reprojectionMode
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_reprojectionMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_reprojectionMode_Injected(intPtr, value);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004FA8 File Offset: 0x000031A8
		public void SetFocusPlane(Vector3 point, Vector3 normal, Vector3 velocity)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			XRDisplaySubsystem.SetFocusPlane_Injected(intPtr, ref point, ref normal, ref velocity);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004FD0 File Offset: 0x000031D0
		public void SetMSAALevel(int level)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			XRDisplaySubsystem.SetMSAALevel_Injected(intPtr, level);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004FF4 File Offset: 0x000031F4
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00005018 File Offset: 0x00003218
		public bool disableLegacyRenderer
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystem.get_disableLegacyRenderer_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRDisplaySubsystem.set_disableLegacyRenderer_Injected(intPtr, value);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000503C File Offset: 0x0000323C
		public int GetRenderPassCount()
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.GetRenderPassCount_Injected(intPtr);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005060 File Offset: 0x00003260
		public void GetRenderPass(int renderPassIndex, out XRDisplaySubsystem.XRRenderPass renderPass)
		{
			bool flag = !this.Internal_TryGetRenderPass(renderPassIndex, out renderPass);
			if (flag)
			{
				throw new IndexOutOfRangeException("renderPassIndex");
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000508C File Offset: 0x0000328C
		[NativeMethod("TryGetRenderPass")]
		private bool Internal_TryGetRenderPass(int renderPassIndex, out XRDisplaySubsystem.XRRenderPass renderPass)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.Internal_TryGetRenderPass_Injected(intPtr, renderPassIndex, out renderPass);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000050B0 File Offset: 0x000032B0
		public void EndRecordingIfLateLatched(Camera camera)
		{
			bool flag = !this.Internal_TryEndRecordingIfLateLatched(camera);
			if (flag)
			{
				bool flag2 = camera == null;
				if (flag2)
				{
					throw new ArgumentNullException("camera");
				}
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000050E8 File Offset: 0x000032E8
		[NativeMethod("TryEndRecordingIfLateLatched")]
		private bool Internal_TryEndRecordingIfLateLatched(Camera camera)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.Internal_TryEndRecordingIfLateLatched_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Camera>(camera));
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005110 File Offset: 0x00003310
		public void BeginRecordingIfLateLatched(Camera camera)
		{
			bool flag = !this.Internal_TryBeginRecordingIfLateLatched(camera);
			if (flag)
			{
				bool flag2 = camera == null;
				if (flag2)
				{
					throw new ArgumentNullException("camera");
				}
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005148 File Offset: 0x00003348
		[NativeMethod("TryBeginRecordingIfLateLatched")]
		private bool Internal_TryBeginRecordingIfLateLatched(Camera camera)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.Internal_TryBeginRecordingIfLateLatched_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Camera>(camera));
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005170 File Offset: 0x00003370
		public void GetCullingParameters(Camera camera, int cullingPassIndex, out ScriptableCullingParameters scriptableCullingParameters)
		{
			bool flag = !this.Internal_TryGetCullingParams(camera, cullingPassIndex, out scriptableCullingParameters);
			if (!flag)
			{
				return;
			}
			bool flag2 = camera == null;
			if (flag2)
			{
				throw new ArgumentNullException("camera");
			}
			throw new IndexOutOfRangeException("cullingPassIndex");
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000051B4 File Offset: 0x000033B4
		[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableCulling.h")]
		[NativeMethod("TryGetCullingParams")]
		private bool Internal_TryGetCullingParams(Camera camera, int cullingPassIndex, out ScriptableCullingParameters scriptableCullingParameters)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.Internal_TryGetCullingParams_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Camera>(camera), cullingPassIndex, out scriptableCullingParameters);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000051E0 File Offset: 0x000033E0
		[NativeMethod("TryGetAppGPUTimeLastFrame")]
		public bool TryGetAppGPUTimeLastFrame(out float gpuTimeLastFrame)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.TryGetAppGPUTimeLastFrame_Injected(intPtr, out gpuTimeLastFrame);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005204 File Offset: 0x00003404
		[NativeMethod("TryGetCompositorGPUTimeLastFrame")]
		public bool TryGetCompositorGPUTimeLastFrame(out float gpuTimeLastFrameCompositor)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.TryGetCompositorGPUTimeLastFrame_Injected(intPtr, out gpuTimeLastFrameCompositor);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005228 File Offset: 0x00003428
		[NativeMethod("TryGetDroppedFrameCount")]
		public bool TryGetDroppedFrameCount(out int droppedFrameCount)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.TryGetDroppedFrameCount_Injected(intPtr, out droppedFrameCount);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000524C File Offset: 0x0000344C
		[NativeMethod("TryGetFramePresentCount")]
		public bool TryGetFramePresentCount(out int framePresentCount)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.TryGetFramePresentCount_Injected(intPtr, out framePresentCount);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005270 File Offset: 0x00003470
		[NativeMethod("TryGetDisplayRefreshRate")]
		public bool TryGetDisplayRefreshRate(out float displayRefreshRate)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.TryGetDisplayRefreshRate_Injected(intPtr, out displayRefreshRate);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005294 File Offset: 0x00003494
		[NativeMethod("TryGetMotionToPhoton")]
		public bool TryGetMotionToPhoton(out float motionToPhoton)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.TryGetMotionToPhoton_Injected(intPtr, out motionToPhoton);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000052B8 File Offset: 0x000034B8
		[NativeConditional("ENABLE_XR")]
		[NativeMethod(Name = "UnityXRRenderTextureIdToRenderTexture", IsThreadSafe = false)]
		public RenderTexture GetRenderTexture(uint unityXrRenderTextureId)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<RenderTexture>(XRDisplaySubsystem.GetRenderTexture_Injected(intPtr, unityXrRenderTextureId));
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000052E0 File Offset: 0x000034E0
		[NativeMethod(Name = "GetTextureForRenderPass", IsThreadSafe = false)]
		[NativeConditional("ENABLE_XR")]
		public RenderTexture GetRenderTextureForRenderPass(int renderPass)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<RenderTexture>(XRDisplaySubsystem.GetRenderTextureForRenderPass_Injected(intPtr, renderPass));
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005308 File Offset: 0x00003508
		[NativeConditional("ENABLE_XR")]
		[NativeMethod(Name = "GetSharedDepthTextureForRenderPass", IsThreadSafe = false)]
		public RenderTexture GetSharedDepthTextureForRenderPass(int renderPass)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<RenderTexture>(XRDisplaySubsystem.GetSharedDepthTextureForRenderPass_Injected(intPtr, renderPass));
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005330 File Offset: 0x00003530
		[NativeConditional("ENABLE_XR")]
		[NativeMethod(Name = "GetPreferredMirrorViewBlitMode", IsThreadSafe = false)]
		public int GetPreferredMirrorBlitMode()
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.GetPreferredMirrorBlitMode_Injected(intPtr);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005354 File Offset: 0x00003554
		[NativeConditional("ENABLE_XR")]
		[NativeMethod(Name = "SetPreferredMirrorViewBlitMode", IsThreadSafe = false)]
		public void SetPreferredMirrorBlitMode(int blitMode)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			XRDisplaySubsystem.SetPreferredMirrorBlitMode_Injected(intPtr, blitMode);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005378 File Offset: 0x00003578
		[Obsolete("GetMirrorViewBlitDesc(RenderTexture, out XRMirrorViewBlitDesc) is deprecated. Use GetMirrorViewBlitDesc(RenderTexture, out XRMirrorViewBlitDesc, int) instead.", false)]
		public bool GetMirrorViewBlitDesc(RenderTexture mirrorRt, out XRDisplaySubsystem.XRMirrorViewBlitDesc outDesc)
		{
			return this.GetMirrorViewBlitDesc(mirrorRt, out outDesc, -1);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005394 File Offset: 0x00003594
		[NativeConditional("ENABLE_XR")]
		[NativeMethod(Name = "QueryMirrorViewBlitDesc", IsThreadSafe = false)]
		public bool GetMirrorViewBlitDesc(RenderTexture mirrorRt, out XRDisplaySubsystem.XRMirrorViewBlitDesc outDesc, int mode)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.GetMirrorViewBlitDesc_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(mirrorRt), out outDesc, mode);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000053C0 File Offset: 0x000035C0
		[Obsolete("AddGraphicsThreadMirrorViewBlit(CommandBuffer, bool) is deprecated. Use AddGraphicsThreadMirrorViewBlit(CommandBuffer, bool, int) instead.", false)]
		public bool AddGraphicsThreadMirrorViewBlit(CommandBuffer cmd, bool allowGraphicsStateInvalidate)
		{
			return this.AddGraphicsThreadMirrorViewBlit(cmd, allowGraphicsStateInvalidate, -1);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000053DC File Offset: 0x000035DC
		[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
		[NativeConditional("ENABLE_XR")]
		[NativeMethod(Name = "AddGraphicsThreadMirrorViewBlit", IsThreadSafe = false)]
		public bool AddGraphicsThreadMirrorViewBlit(CommandBuffer cmd, bool allowGraphicsStateInvalidate, int mode)
		{
			IntPtr intPtr = XRDisplaySubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystem.AddGraphicsThreadMirrorViewBlit_Injected(intPtr, (cmd == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(cmd), allowGraphicsStateInvalidate, mode);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00005410 File Offset: 0x00003610
		public HDROutputSettings hdrOutputSettings
		{
			get
			{
				bool flag = this.m_HDROutputSettings == null;
				if (flag)
				{
					this.m_HDROutputSettings = new HDROutputSettings(-1);
				}
				return this.m_HDROutputSettings;
			}
		}

		// Token: 0x06000132 RID: 306
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_displayOpaque_Injected(IntPtr _unity_self);

		// Token: 0x06000133 RID: 307
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_contentProtectionEnabled_Injected(IntPtr _unity_self);

		// Token: 0x06000134 RID: 308
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_contentProtectionEnabled_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000135 RID: 309
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_appliedViewportScale_Injected(IntPtr _unity_self);

		// Token: 0x06000136 RID: 310
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_scaleOfAllViewports_Injected(IntPtr _unity_self);

		// Token: 0x06000137 RID: 311
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_scaleOfAllViewports_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000138 RID: 312
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_scaleOfAllRenderTargets_Injected(IntPtr _unity_self);

		// Token: 0x06000139 RID: 313
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_scaleOfAllRenderTargets_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600013A RID: 314
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_zNear_Injected(IntPtr _unity_self);

		// Token: 0x0600013B RID: 315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_zNear_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600013C RID: 316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_zFar_Injected(IntPtr _unity_self);

		// Token: 0x0600013D RID: 317
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_zFar_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600013E RID: 318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_sRGB_Injected(IntPtr _unity_self);

		// Token: 0x0600013F RID: 319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sRGB_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000140 RID: 320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_occlusionMaskScale_Injected(IntPtr _unity_self);

		// Token: 0x06000141 RID: 321
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_occlusionMaskScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000142 RID: 322
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_foveatedRenderingLevel_Injected(IntPtr _unity_self);

		// Token: 0x06000143 RID: 323
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_foveatedRenderingLevel_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000144 RID: 324
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern XRDisplaySubsystem.FoveatedRenderingFlags get_foveatedRenderingFlags_Injected(IntPtr _unity_self);

		// Token: 0x06000145 RID: 325
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_foveatedRenderingFlags_Injected(IntPtr _unity_self, XRDisplaySubsystem.FoveatedRenderingFlags value);

		// Token: 0x06000146 RID: 326
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MarkTransformLateLatched_Injected(IntPtr _unity_self, IntPtr transform, XRDisplaySubsystem.LateLatchNode nodeType);

		// Token: 0x06000147 RID: 327
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern XRDisplaySubsystem.TextureLayout get_textureLayout_Injected(IntPtr _unity_self);

		// Token: 0x06000148 RID: 328
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_textureLayout_Injected(IntPtr _unity_self, XRDisplaySubsystem.TextureLayout value);

		// Token: 0x06000149 RID: 329
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern XRDisplaySubsystem.TextureLayout get_supportedTextureLayouts_Injected(IntPtr _unity_self);

		// Token: 0x0600014A RID: 330
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern XRDisplaySubsystem.ReprojectionMode get_reprojectionMode_Injected(IntPtr _unity_self);

		// Token: 0x0600014B RID: 331
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reprojectionMode_Injected(IntPtr _unity_self, XRDisplaySubsystem.ReprojectionMode value);

		// Token: 0x0600014C RID: 332
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFocusPlane_Injected(IntPtr _unity_self, [In] ref Vector3 point, [In] ref Vector3 normal, [In] ref Vector3 velocity);

		// Token: 0x0600014D RID: 333
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMSAALevel_Injected(IntPtr _unity_self, int level);

		// Token: 0x0600014E RID: 334
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_disableLegacyRenderer_Injected(IntPtr _unity_self);

		// Token: 0x0600014F RID: 335
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_disableLegacyRenderer_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000150 RID: 336
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRenderPassCount_Injected(IntPtr _unity_self);

		// Token: 0x06000151 RID: 337
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_TryGetRenderPass_Injected(IntPtr _unity_self, int renderPassIndex, out XRDisplaySubsystem.XRRenderPass renderPass);

		// Token: 0x06000152 RID: 338
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_TryEndRecordingIfLateLatched_Injected(IntPtr _unity_self, IntPtr camera);

		// Token: 0x06000153 RID: 339
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_TryBeginRecordingIfLateLatched_Injected(IntPtr _unity_self, IntPtr camera);

		// Token: 0x06000154 RID: 340
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_TryGetCullingParams_Injected(IntPtr _unity_self, IntPtr camera, int cullingPassIndex, out ScriptableCullingParameters scriptableCullingParameters);

		// Token: 0x06000155 RID: 341
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetAppGPUTimeLastFrame_Injected(IntPtr _unity_self, out float gpuTimeLastFrame);

		// Token: 0x06000156 RID: 342
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetCompositorGPUTimeLastFrame_Injected(IntPtr _unity_self, out float gpuTimeLastFrameCompositor);

		// Token: 0x06000157 RID: 343
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetDroppedFrameCount_Injected(IntPtr _unity_self, out int droppedFrameCount);

		// Token: 0x06000158 RID: 344
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetFramePresentCount_Injected(IntPtr _unity_self, out int framePresentCount);

		// Token: 0x06000159 RID: 345
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetDisplayRefreshRate_Injected(IntPtr _unity_self, out float displayRefreshRate);

		// Token: 0x0600015A RID: 346
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetMotionToPhoton_Injected(IntPtr _unity_self, out float motionToPhoton);

		// Token: 0x0600015B RID: 347
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetRenderTexture_Injected(IntPtr _unity_self, uint unityXrRenderTextureId);

		// Token: 0x0600015C RID: 348
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetRenderTextureForRenderPass_Injected(IntPtr _unity_self, int renderPass);

		// Token: 0x0600015D RID: 349
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetSharedDepthTextureForRenderPass_Injected(IntPtr _unity_self, int renderPass);

		// Token: 0x0600015E RID: 350
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPreferredMirrorBlitMode_Injected(IntPtr _unity_self);

		// Token: 0x0600015F RID: 351
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPreferredMirrorBlitMode_Injected(IntPtr _unity_self, int blitMode);

		// Token: 0x06000160 RID: 352
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetMirrorViewBlitDesc_Injected(IntPtr _unity_self, IntPtr mirrorRt, out XRDisplaySubsystem.XRMirrorViewBlitDesc outDesc, int mode);

		// Token: 0x06000161 RID: 353
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool AddGraphicsThreadMirrorViewBlit_Injected(IntPtr _unity_self, IntPtr cmd, bool allowGraphicsStateInvalidate, int mode);

		// Token: 0x040000B1 RID: 177
		private HDROutputSettings m_HDROutputSettings;

		// Token: 0x02000019 RID: 25
		[Flags]
		public enum FoveatedRenderingFlags
		{
			// Token: 0x040000B3 RID: 179
			None = 0,
			// Token: 0x040000B4 RID: 180
			GazeAllowed = 1
		}

		// Token: 0x0200001A RID: 26
		public enum LateLatchNode
		{
			// Token: 0x040000B6 RID: 182
			Head,
			// Token: 0x040000B7 RID: 183
			LeftHand,
			// Token: 0x040000B8 RID: 184
			RightHand
		}

		// Token: 0x0200001B RID: 27
		[Flags]
		public enum TextureLayout
		{
			// Token: 0x040000BA RID: 186
			Texture2DArray = 1,
			// Token: 0x040000BB RID: 187
			SingleTexture2D = 2,
			// Token: 0x040000BC RID: 188
			SeparateTexture2Ds = 4
		}

		// Token: 0x0200001C RID: 28
		public enum ReprojectionMode
		{
			// Token: 0x040000BE RID: 190
			Unspecified,
			// Token: 0x040000BF RID: 191
			PositionAndOrientation,
			// Token: 0x040000C0 RID: 192
			OrientationOnly,
			// Token: 0x040000C1 RID: 193
			None
		}

		// Token: 0x0200001D RID: 29
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRRenderParameter
		{
			// Token: 0x040000C2 RID: 194
			public Matrix4x4 view;

			// Token: 0x040000C3 RID: 195
			public Matrix4x4 projection;

			// Token: 0x040000C4 RID: 196
			public Rect viewport;

			// Token: 0x040000C5 RID: 197
			public Mesh occlusionMesh;

			// Token: 0x040000C6 RID: 198
			public int textureArraySlice;

			// Token: 0x040000C7 RID: 199
			public Matrix4x4 previousView;

			// Token: 0x040000C8 RID: 200
			public bool isPreviousViewValid;
		}

		// Token: 0x0200001E RID: 30
		[NativeHeader("Runtime/Graphics/RenderTextureDesc.h")]
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
		public struct XRRenderPass
		{
			// Token: 0x06000162 RID: 354 RVA: 0x0000544C File Offset: 0x0000364C
			[NativeMethod(Name = "XRRenderPassScriptApi::GetRenderParameter", IsFreeFunction = true, HasExplicitThis = true, ThrowsException = true)]
			[NativeConditional("ENABLE_XR")]
			public void GetRenderParameter(Camera camera, int renderParameterIndex, out XRDisplaySubsystem.XRRenderParameter renderParameter)
			{
				XRDisplaySubsystem.XRRenderPass.GetRenderParameter_Injected(ref this, Object.MarshalledUnityObject.Marshal<Camera>(camera), renderParameterIndex, out renderParameter);
			}

			// Token: 0x06000163 RID: 355
			[NativeMethod(Name = "XRRenderPassScriptApi::GetRenderParameterCount", IsFreeFunction = true, HasExplicitThis = true)]
			[NativeConditional("ENABLE_XR")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern int GetRenderParameterCount();

			// Token: 0x06000164 RID: 356
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetRenderParameter_Injected(ref XRDisplaySubsystem.XRRenderPass _unity_self, IntPtr camera, int renderParameterIndex, out XRDisplaySubsystem.XRRenderParameter renderParameter);

			// Token: 0x040000C9 RID: 201
			private IntPtr displaySubsystemInstance;

			// Token: 0x040000CA RID: 202
			public int renderPassIndex;

			// Token: 0x040000CB RID: 203
			public RenderTargetIdentifier renderTarget;

			// Token: 0x040000CC RID: 204
			public RenderTextureDescriptor renderTargetDesc;

			// Token: 0x040000CD RID: 205
			public bool hasMotionVectorPass;

			// Token: 0x040000CE RID: 206
			public RenderTargetIdentifier motionVectorRenderTarget;

			// Token: 0x040000CF RID: 207
			public RenderTextureDescriptor motionVectorRenderTargetDesc;

			// Token: 0x040000D0 RID: 208
			public bool shouldFillOutDepth;

			// Token: 0x040000D1 RID: 209
			public bool spaceWarpRightHandedNDC;

			// Token: 0x040000D2 RID: 210
			public int cullingPassIndex;

			// Token: 0x040000D3 RID: 211
			public IntPtr foveatedRenderingInfo;
		}

		// Token: 0x0200001F RID: 31
		[NativeHeader("Runtime/Graphics/RenderTexture.h")]
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRBlitParams
		{
			// Token: 0x040000D4 RID: 212
			public RenderTexture srcTex;

			// Token: 0x040000D5 RID: 213
			public int srcTexArraySlice;

			// Token: 0x040000D6 RID: 214
			public Rect srcRect;

			// Token: 0x040000D7 RID: 215
			public Rect destRect;

			// Token: 0x040000D8 RID: 216
			public IntPtr foveatedRenderingInfo;

			// Token: 0x040000D9 RID: 217
			public bool srcHdrEncoded;

			// Token: 0x040000DA RID: 218
			public ColorGamut srcHdrColorGamut;

			// Token: 0x040000DB RID: 219
			public int srcHdrMaxLuminance;
		}

		// Token: 0x02000020 RID: 32
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRMirrorViewBlitDesc
		{
			// Token: 0x06000165 RID: 357
			[NativeConditional("ENABLE_XR")]
			[NativeMethod(Name = "XRMirrorViewBlitDescScriptApi::GetBlitParameter", IsFreeFunction = true, HasExplicitThis = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern void GetBlitParameter(int blitParameterIndex, out XRDisplaySubsystem.XRBlitParams blitParameter);

			// Token: 0x040000DC RID: 220
			private IntPtr displaySubsystemInstance;

			// Token: 0x040000DD RID: 221
			public bool nativeBlitAvailable;

			// Token: 0x040000DE RID: 222
			public bool nativeBlitInvalidStates;

			// Token: 0x040000DF RID: 223
			public int blitParamsCount;
		}

		// Token: 0x02000021 RID: 33
		internal new static class BindingsMarshaller
		{
			// Token: 0x06000166 RID: 358 RVA: 0x00005467 File Offset: 0x00003667
			public static IntPtr ConvertToNative(XRDisplaySubsystem xrDisplaySubsystem)
			{
				return xrDisplaySubsystem.m_Ptr;
			}
		}
	}
}
