using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200011D RID: 285
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Misc/GameObjectUtility.h")]
	[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
	[NativeHeader("Runtime/Camera/Camera.h")]
	[NativeHeader("Runtime/Camera/RenderManager.h")]
	[NativeHeader("Runtime/Shaders/Shader.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public sealed class Camera : Behaviour
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0000BE5C File Offset: 0x0000A05C
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x0000BE80 File Offset: 0x0000A080
		[NativeProperty("Near")]
		public float nearClipPlane
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_nearClipPlane_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_nearClipPlane_Injected(intPtr, value);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x0000BEC8 File Offset: 0x0000A0C8
		[NativeProperty("Far")]
		public float farClipPlane
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_farClipPlane_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_farClipPlane_Injected(intPtr, value);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0000BEEC File Offset: 0x0000A0EC
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x0000BF10 File Offset: 0x0000A110
		[NativeProperty("VerticalFieldOfView")]
		public float fieldOfView
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_fieldOfView_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_fieldOfView_Injected(intPtr, value);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0000BF34 File Offset: 0x0000A134
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x0000BF58 File Offset: 0x0000A158
		public RenderingPath renderingPath
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_renderingPath_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_renderingPath_Injected(intPtr, value);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0000BF7C File Offset: 0x0000A17C
		public RenderingPath actualRenderingPath
		{
			[NativeName("CalculateRenderingPath")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_actualRenderingPath_Injected(intPtr);
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		public void Reset()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.Reset_Injected(intPtr);
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0000BFC4 File Offset: 0x0000A1C4
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
		public bool allowHDR
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_allowHDR_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_allowHDR_Injected(intPtr, value);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0000C00C File Offset: 0x0000A20C
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x0000C030 File Offset: 0x0000A230
		public bool allowMSAA
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_allowMSAA_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_allowMSAA_Injected(intPtr, value);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0000C054 File Offset: 0x0000A254
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x0000C078 File Offset: 0x0000A278
		public bool allowDynamicResolution
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_allowDynamicResolution_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_allowDynamicResolution_Injected(intPtr, value);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0000C09C File Offset: 0x0000A29C
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0000C0C0 File Offset: 0x0000A2C0
		[NativeProperty("ForceIntoRT")]
		public bool forceIntoRenderTexture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_forceIntoRenderTexture_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_forceIntoRenderTexture_Injected(intPtr, value);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x0000C108 File Offset: 0x0000A308
		public float orthographicSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_orthographicSize_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_orthographicSize_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0000C12C File Offset: 0x0000A32C
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x0000C150 File Offset: 0x0000A350
		public bool orthographic
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_orthographic_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_orthographic_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0000C174 File Offset: 0x0000A374
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x0000C198 File Offset: 0x0000A398
		public OpaqueSortMode opaqueSortMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_opaqueSortMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_opaqueSortMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0000C1BC File Offset: 0x0000A3BC
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0000C1E0 File Offset: 0x0000A3E0
		public TransparencySortMode transparencySortMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_transparencySortMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_transparencySortMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0000C204 File Offset: 0x0000A404
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x0000C22C File Offset: 0x0000A42C
		public Vector3 transparencySortAxis
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Camera.get_transparencySortAxis_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_transparencySortAxis_Injected(intPtr, ref value);
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0000C250 File Offset: 0x0000A450
		public void ResetTransparencySortSettings()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetTransparencySortSettings_Injected(intPtr);
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0000C274 File Offset: 0x0000A474
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x0000C298 File Offset: 0x0000A498
		public float depth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_depth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_depth_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		public float aspect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_aspect_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_aspect_Injected(intPtr, value);
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000C304 File Offset: 0x0000A504
		public void ResetAspect()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetAspect_Injected(intPtr);
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0000C328 File Offset: 0x0000A528
		public Vector3 velocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Camera.get_velocity_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0000C350 File Offset: 0x0000A550
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0000C374 File Offset: 0x0000A574
		public int cullingMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_cullingMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_cullingMask_Injected(intPtr, value);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0000C398 File Offset: 0x0000A598
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0000C3BC File Offset: 0x0000A5BC
		public int eventMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_eventMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_eventMask_Injected(intPtr, value);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
		public bool layerCullSpherical
		{
			get
			{
				return this.layerCullSphericalInternal;
			}
			set
			{
				bool flag = GraphicsSettings.currentRenderPipeline != null;
				if (flag)
				{
					Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.layerCullSpherical only with the built-in renderer.");
				}
				this.layerCullSphericalInternal = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0000C42C File Offset: 0x0000A62C
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x0000C450 File Offset: 0x0000A650
		[NativeProperty("LayerCullSpherical")]
		internal bool layerCullSphericalInternal
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_layerCullSphericalInternal_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_layerCullSphericalInternal_Injected(intPtr, value);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0000C474 File Offset: 0x0000A674
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x0000C498 File Offset: 0x0000A698
		public CameraType cameraType
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_cameraType_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_cameraType_Injected(intPtr, value);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		internal Material skyboxMaterial
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Material>(Camera.get_skyboxMaterial_Injected(intPtr));
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x0000C508 File Offset: 0x0000A708
		[NativeConditional("UNITY_EDITOR")]
		public ulong overrideSceneCullingMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_overrideSceneCullingMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_overrideSceneCullingMask_Injected(intPtr, value);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0000C52C File Offset: 0x0000A72C
		[NativeConditional("UNITY_EDITOR")]
		internal ulong sceneCullingMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_sceneCullingMask_Injected(intPtr);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0000C550 File Offset: 0x0000A750
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0000C574 File Offset: 0x0000A774
		[NativeConditional("UNITY_EDITOR")]
		internal bool useInteractiveLightBakingData
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_useInteractiveLightBakingData_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_useInteractiveLightBakingData_Injected(intPtr, value);
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0000C598 File Offset: 0x0000A798
		[FreeFunction("CameraScripting::GetLayerCullDistances", HasExplicitThis = true)]
		private float[] GetLayerCullDistances()
		{
			float[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Camera.GetLayerCullDistances_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				float[] array;
				blittableArrayWrapper.Unmarshal<float>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000C5DC File Offset: 0x0000A7DC
		[FreeFunction("CameraScripting::SetLayerCullDistances", HasExplicitThis = true)]
		private unsafe void SetLayerCullDistances([NotNull] float[] d)
		{
			if (d == null)
			{
				ThrowHelper.ThrowArgumentNullException(d, "d");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(d);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Camera.SetLayerCullDistances_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0000C634 File Offset: 0x0000A834
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0000C64C File Offset: 0x0000A84C
		public float[] layerCullDistances
		{
			get
			{
				return this.GetLayerCullDistances();
			}
			set
			{
				bool flag = value.Length != 32;
				if (flag)
				{
					throw new UnityException("Array needs to contain exactly 32 floats for layerCullDistances.");
				}
				this.SetLayerCullDistances(value);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0000C67C File Offset: 0x0000A87C
		[Obsolete("PreviewCullingLayer is obsolete. Use scene culling masks instead.", false)]
		internal static int PreviewCullingLayer
		{
			get
			{
				return 31;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0000C690 File Offset: 0x0000A890
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		public bool useOcclusionCulling
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_useOcclusionCulling_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_useOcclusionCulling_Injected(intPtr, value);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x0000C700 File Offset: 0x0000A900
		public Matrix4x4 cullingMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Camera.get_cullingMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_cullingMatrix_Injected(intPtr, ref value);
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0000C724 File Offset: 0x0000A924
		public void ResetCullingMatrix()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetCullingMatrix_Injected(intPtr);
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0000C748 File Offset: 0x0000A948
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x0000C770 File Offset: 0x0000A970
		public Color backgroundColor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				Camera.get_backgroundColor_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_backgroundColor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0000C794 File Offset: 0x0000A994
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x0000C7B8 File Offset: 0x0000A9B8
		public CameraClearFlags clearFlags
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_clearFlags_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_clearFlags_Injected(intPtr, value);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x0000C800 File Offset: 0x0000AA00
		public DepthTextureMode depthTextureMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_depthTextureMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_depthTextureMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0000C824 File Offset: 0x0000AA24
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x0000C848 File Offset: 0x0000AA48
		public bool clearStencilAfterLightingPass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_clearStencilAfterLightingPass_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_clearStencilAfterLightingPass_Injected(intPtr, value);
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0000C86C File Offset: 0x0000AA6C
		public unsafe void SetReplacementShader(Shader shader, string replacementTag)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<Shader>(shader);
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(replacementTag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = replacementTag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Camera.SetReplacementShader_Injected(intPtr, intPtr2, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000C8D8 File Offset: 0x0000AAD8
		public void ResetReplacementShader()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetReplacementShader_Injected(intPtr);
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0000C8FC File Offset: 0x0000AAFC
		internal Camera.ProjectionMatrixMode projectionMatrixMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_projectionMatrixMode_Injected(intPtr);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0000C920 File Offset: 0x0000AB20
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x0000C944 File Offset: 0x0000AB44
		public bool usePhysicalProperties
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_usePhysicalProperties_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_usePhysicalProperties_Injected(intPtr, value);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0000C968 File Offset: 0x0000AB68
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x0000C98C File Offset: 0x0000AB8C
		public int iso
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_iso_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_iso_Injected(intPtr, value);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		public float shutterSpeed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_shutterSpeed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_shutterSpeed_Injected(intPtr, value);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		public float aperture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_aperture_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_aperture_Injected(intPtr, value);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0000CA40 File Offset: 0x0000AC40
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x0000CA64 File Offset: 0x0000AC64
		public float focusDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_focusDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_focusDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0000CA88 File Offset: 0x0000AC88
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x0000CAAC File Offset: 0x0000ACAC
		public float focalLength
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_focalLength_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_focalLength_Injected(intPtr, value);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0000CAF4 File Offset: 0x0000ACF4
		public int bladeCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_bladeCount_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_bladeCount_Injected(intPtr, value);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0000CB18 File Offset: 0x0000AD18
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0000CB40 File Offset: 0x0000AD40
		public Vector2 curvature
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Camera.get_curvature_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_curvature_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0000CB64 File Offset: 0x0000AD64
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0000CB88 File Offset: 0x0000AD88
		public float barrelClipping
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_barrelClipping_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_barrelClipping_Injected(intPtr, value);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0000CBAC File Offset: 0x0000ADAC
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		public float anamorphism
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_anamorphism_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_anamorphism_Injected(intPtr, value);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0000CBF4 File Offset: 0x0000ADF4
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x0000CC1C File Offset: 0x0000AE1C
		public Vector2 sensorSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Camera.get_sensorSize_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_sensorSize_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0000CC40 File Offset: 0x0000AE40
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x0000CC68 File Offset: 0x0000AE68
		public Vector2 lensShift
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Camera.get_lensShift_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_lensShift_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
		public Camera.GateFitMode gateFit
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_gateFit_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_gateFit_Injected(intPtr, value);
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0000CCD4 File Offset: 0x0000AED4
		public float GetGateFittedFieldOfView()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Camera.GetGateFittedFieldOfView_Injected(intPtr);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
		public Vector2 GetGateFittedLensShift()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			Camera.GetGateFittedLensShift_Injected(intPtr, out vector);
			return vector;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000CD20 File Offset: 0x0000AF20
		internal Vector3 GetLocalSpaceAim()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Camera.GetLocalSpaceAim_Injected(intPtr, out vector);
			return vector;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0000CD48 File Offset: 0x0000AF48
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x0000CD70 File Offset: 0x0000AF70
		[NativeProperty("NormalizedViewportRect")]
		public Rect rect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rect rect;
				Camera.get_rect_Injected(intPtr, out rect);
				return rect;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_rect_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0000CD94 File Offset: 0x0000AF94
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0000CDBC File Offset: 0x0000AFBC
		[NativeProperty("ScreenViewportRect")]
		public Rect pixelRect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rect rect;
				Camera.get_pixelRect_Injected(intPtr, out rect);
				return rect;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_pixelRect_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		public int pixelWidth
		{
			[FreeFunction("CameraScripting::GetPixelWidth", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_pixelWidth_Injected(intPtr);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0000CE04 File Offset: 0x0000B004
		public int pixelHeight
		{
			[FreeFunction("CameraScripting::GetPixelHeight", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_pixelHeight_Injected(intPtr);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0000CE28 File Offset: 0x0000B028
		public int scaledPixelWidth
		{
			[FreeFunction("CameraScripting::GetScaledPixelWidth", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_scaledPixelWidth_Injected(intPtr);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0000CE4C File Offset: 0x0000B04C
		public int scaledPixelHeight
		{
			[FreeFunction("CameraScripting::GetScaledPixelHeight", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_scaledPixelHeight_Injected(intPtr);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0000CE70 File Offset: 0x0000B070
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x0000CE98 File Offset: 0x0000B098
		public RenderTexture targetTexture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<RenderTexture>(Camera.get_targetTexture_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_targetTexture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(value));
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0000CEC0 File Offset: 0x0000B0C0
		public RenderTexture activeTexture
		{
			[NativeName("GetCurrentTargetTexture")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<RenderTexture>(Camera.get_activeTexture_Injected(intPtr));
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0000CEE8 File Offset: 0x0000B0E8
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0000CF0C File Offset: 0x0000B10C
		public int targetDisplay
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_targetDisplay_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_targetDisplay_Injected(intPtr, value);
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0000CF30 File Offset: 0x0000B130
		[FreeFunction("CameraScripting::SetTargetBuffers", HasExplicitThis = true)]
		private void SetTargetBuffersImpl(RenderBuffer color, RenderBuffer depth)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.SetTargetBuffersImpl_Injected(intPtr, ref color, ref depth);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0000CF56 File Offset: 0x0000B156
		public void SetTargetBuffers(RenderBuffer colorBuffer, RenderBuffer depthBuffer)
		{
			this.SetTargetBuffersImpl(colorBuffer, depthBuffer);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0000CF64 File Offset: 0x0000B164
		[FreeFunction("CameraScripting::SetTargetBuffers", HasExplicitThis = true)]
		private unsafe void SetTargetBuffersMRTImpl(RenderBuffer[] color, RenderBuffer depth)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<RenderBuffer> span = new Span<RenderBuffer>(color);
			fixed (RenderBuffer* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Camera.SetTargetBuffersMRTImpl_Injected(intPtr, ref managedSpanWrapper, ref depth);
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0000CFAE File Offset: 0x0000B1AE
		public void SetTargetBuffers(RenderBuffer[] colorBuffer, RenderBuffer depthBuffer)
		{
			this.SetTargetBuffersMRTImpl(colorBuffer, depthBuffer);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000CFBC File Offset: 0x0000B1BC
		internal string[] GetCameraBufferWarnings()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Camera.GetCameraBufferWarnings_Injected(intPtr);
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0000CFE0 File Offset: 0x0000B1E0
		public Matrix4x4 cameraToWorldMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Camera.get_cameraToWorldMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0000D008 File Offset: 0x0000B208
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x0000D030 File Offset: 0x0000B230
		public Matrix4x4 worldToCameraMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Camera.get_worldToCameraMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_worldToCameraMatrix_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0000D054 File Offset: 0x0000B254
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x0000D07C File Offset: 0x0000B27C
		public Matrix4x4 projectionMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Camera.get_projectionMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_projectionMatrix_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0000D0C8 File Offset: 0x0000B2C8
		public Matrix4x4 nonJitteredProjectionMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Camera.get_nonJitteredProjectionMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_nonJitteredProjectionMatrix_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0000D0EC File Offset: 0x0000B2EC
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0000D110 File Offset: 0x0000B310
		[NativeProperty("UseJitteredProjectionMatrixForTransparent")]
		public bool useJitteredProjectionMatrixForTransparentRendering
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_useJitteredProjectionMatrixForTransparentRendering_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_useJitteredProjectionMatrixForTransparentRendering_Injected(intPtr, value);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0000D134 File Offset: 0x0000B334
		public Matrix4x4 previousViewProjectionMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Camera.get_previousViewProjectionMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0000D15C File Offset: 0x0000B35C
		public void ResetWorldToCameraMatrix()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetWorldToCameraMatrix_Injected(intPtr);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0000D180 File Offset: 0x0000B380
		public void ResetProjectionMatrix()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetProjectionMatrix_Injected(intPtr);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0000D1A4 File Offset: 0x0000B3A4
		[FreeFunction("CameraScripting::CalculateObliqueMatrix", HasExplicitThis = true)]
		public Matrix4x4 CalculateObliqueMatrix(Vector4 clipPlane)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			Camera.CalculateObliqueMatrix_Injected(intPtr, ref clipPlane, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0000D1CC File Offset: 0x0000B3CC
		public Vector3 WorldToScreenPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Camera.WorldToScreenPoint_Injected(intPtr, ref position, eye, out vector);
			return vector;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0000D1F4 File Offset: 0x0000B3F4
		public Vector3 WorldToViewportPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Camera.WorldToViewportPoint_Injected(intPtr, ref position, eye, out vector);
			return vector;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0000D21C File Offset: 0x0000B41C
		public Vector3 ViewportToWorldPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Camera.ViewportToWorldPoint_Injected(intPtr, ref position, eye, out vector);
			return vector;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0000D244 File Offset: 0x0000B444
		public Vector3 ScreenToWorldPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Camera.ScreenToWorldPoint_Injected(intPtr, ref position, eye, out vector);
			return vector;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0000D26C File Offset: 0x0000B46C
		public Vector3 WorldToScreenPoint(Vector3 position)
		{
			return this.WorldToScreenPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0000D288 File Offset: 0x0000B488
		public Vector3 WorldToViewportPoint(Vector3 position)
		{
			return this.WorldToViewportPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
		public Vector3 ViewportToWorldPoint(Vector3 position)
		{
			return this.ViewportToWorldPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		public Vector3 ScreenToWorldPoint(Vector3 position)
		{
			return this.ScreenToWorldPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0000D2DC File Offset: 0x0000B4DC
		public Vector3 ScreenToViewportPoint(Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Camera.ScreenToViewportPoint_Injected(intPtr, ref position, out vector);
			return vector;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0000D304 File Offset: 0x0000B504
		public Vector3 ViewportToScreenPoint(Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Camera.ViewportToScreenPoint_Injected(intPtr, ref position, out vector);
			return vector;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0000D32C File Offset: 0x0000B52C
		internal Vector2 GetFrustumPlaneSizeAt(float distance)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			Camera.GetFrustumPlaneSizeAt_Injected(intPtr, distance, out vector);
			return vector;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0000D354 File Offset: 0x0000B554
		private Ray ViewportPointToRay(Vector2 pos, Camera.MonoOrStereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Ray ray;
			Camera.ViewportPointToRay_Injected(intPtr, ref pos, eye, out ray);
			return ray;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0000D37C File Offset: 0x0000B57C
		public Ray ViewportPointToRay(Vector3 pos, Camera.MonoOrStereoscopicEye eye)
		{
			return this.ViewportPointToRay(pos, eye);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0000D39C File Offset: 0x0000B59C
		public Ray ViewportPointToRay(Vector3 pos)
		{
			return this.ViewportPointToRay(pos, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		private Ray ScreenPointToRay(Vector2 pos, Camera.MonoOrStereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Ray ray;
			Camera.ScreenPointToRay_Injected(intPtr, ref pos, eye, out ray);
			return ray;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0000D3E0 File Offset: 0x0000B5E0
		public Ray ScreenPointToRay(Vector3 pos, Camera.MonoOrStereoscopicEye eye)
		{
			return this.ScreenPointToRay(pos, eye);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0000D400 File Offset: 0x0000B600
		public Ray ScreenPointToRay(Vector3 pos)
		{
			return this.ScreenPointToRay(pos, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0000D41C File Offset: 0x0000B61C
		[FreeFunction("CameraScripting::CalculateViewportRayVectors", HasExplicitThis = true)]
		private unsafe void CalculateFrustumCornersInternal(Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, [Out] Vector3[] outCorners)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (outCorners != null)
				{
					fixed (Vector3[] array = outCorners)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Camera.CalculateFrustumCornersInternal_Injected(intPtr, ref viewport, z, eye, out blittableArrayWrapper);
			}
			finally
			{
				Vector3[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Vector3>(ref array);
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000D480 File Offset: 0x0000B680
		public void CalculateFrustumCorners(Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, Vector3[] outCorners)
		{
			bool flag = outCorners == null;
			if (flag)
			{
				throw new ArgumentNullException("outCorners");
			}
			bool flag2 = outCorners.Length < 4;
			if (flag2)
			{
				throw new ArgumentException("outCorners minimum size is 4", "outCorners");
			}
			this.CalculateFrustumCornersInternal(viewport, z, eye, outCorners);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000D4CC File Offset: 0x0000B6CC
		[NativeName("CalculateProjectionMatrixFromPhysicalProperties")]
		private static void CalculateProjectionMatrixFromPhysicalPropertiesInternal(out Matrix4x4 output, float focalLength, Vector2 sensorSize, Vector2 lensShift, float nearClip, float farClip, float gateAspect, Camera.GateFitMode gateFitMode)
		{
			Camera.CalculateProjectionMatrixFromPhysicalPropertiesInternal_Injected(out output, focalLength, ref sensorSize, ref lensShift, nearClip, farClip, gateAspect, gateFitMode);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		public static void CalculateProjectionMatrixFromPhysicalProperties(out Matrix4x4 output, float focalLength, Vector2 sensorSize, Vector2 lensShift, float nearClip, float farClip, Camera.GateFitParameters gateFitParameters = default(Camera.GateFitParameters))
		{
			Camera.CalculateProjectionMatrixFromPhysicalPropertiesInternal(out output, focalLength, sensorSize, lensShift, nearClip, farClip, gateFitParameters.aspect, gateFitParameters.mode);
		}

		// Token: 0x06000729 RID: 1833
		[NativeName("FocalLengthToFieldOfView_Safe")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float FocalLengthToFieldOfView(float focalLength, float sensorSize);

		// Token: 0x0600072A RID: 1834
		[NativeName("FieldOfViewToFocalLength_Safe")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float FieldOfViewToFocalLength(float fieldOfView, float sensorSize);

		// Token: 0x0600072B RID: 1835
		[NativeName("HorizontalToVerticalFieldOfView_Safe")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float HorizontalToVerticalFieldOfView(float horizontalFieldOfView, float aspectRatio);

		// Token: 0x0600072C RID: 1836
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float VerticalToHorizontalFieldOfView(float verticalFieldOfView, float aspectRatio);

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0000D50C File Offset: 0x0000B70C
		public static Camera main
		{
			[FreeFunction("FindMainCamera")]
			get
			{
				return Unmarshal.UnmarshalUnityObject<Camera>(Camera.get_main_Injected());
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0000D524 File Offset: 0x0000B724
		public static Camera current
		{
			get
			{
				return Camera.currentInternal;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0000D53C File Offset: 0x0000B73C
		private static Camera currentInternal
		{
			[FreeFunction("GetCurrentCameraPPtr")]
			get
			{
				return Unmarshal.UnmarshalUnityObject<Camera>(Camera.get_currentInternal_Injected());
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0000D554 File Offset: 0x0000B754
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x0000D57C File Offset: 0x0000B77C
		public Scene scene
		{
			[FreeFunction("CameraScripting::GetScene", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Scene scene;
				Camera.get_scene_Injected(intPtr, out scene);
				return scene;
			}
			[FreeFunction("CameraScripting::SetScene", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_scene_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0000D5A0 File Offset: 0x0000B7A0
		public bool stereoEnabled
		{
			[NativeMethod("GetStereoEnabledForBuiltInOrSRP")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_stereoEnabled_Injected(intPtr);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		public float stereoSeparation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_stereoSeparation_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_stereoSeparation_Injected(intPtr, value);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0000D60C File Offset: 0x0000B80C
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x0000D630 File Offset: 0x0000B830
		public float stereoConvergence
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_stereoConvergence_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_stereoConvergence_Injected(intPtr, value);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0000D654 File Offset: 0x0000B854
		public bool areVRStereoViewMatricesWithinSingleCullTolerance
		{
			[NativeName("AreVRStereoViewMatricesWithinSingleCullTolerance")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_areVRStereoViewMatricesWithinSingleCullTolerance_Injected(intPtr);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0000D678 File Offset: 0x0000B878
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x0000D690 File Offset: 0x0000B890
		public StereoTargetEyeMask stereoTargetEye
		{
			get
			{
				return this.stereoTargetEyeInternal;
			}
			set
			{
				bool flag = GraphicsSettings.currentRenderPipeline != null;
				if (flag)
				{
					Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.stereoTargetEye only with the built-in renderer.");
				}
				this.stereoTargetEyeInternal = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0000D6C4 File Offset: 0x0000B8C4
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0000D6E8 File Offset: 0x0000B8E8
		[NativeProperty("StereoTargetEye")]
		internal StereoTargetEyeMask stereoTargetEyeInternal
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_stereoTargetEyeInternal_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_stereoTargetEyeInternal_Injected(intPtr, value);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0000D70C File Offset: 0x0000B90C
		public Camera.MonoOrStereoscopicEye stereoActiveEye
		{
			[FreeFunction("CameraScripting::GetStereoActiveEye", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_stereoActiveEye_Injected(intPtr);
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0000D730 File Offset: 0x0000B930
		public Matrix4x4 GetStereoNonJitteredProjectionMatrix(Camera.StereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			Camera.GetStereoNonJitteredProjectionMatrix_Injected(intPtr, eye, out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0000D758 File Offset: 0x0000B958
		[FreeFunction("CameraScripting::GetStereoViewMatrix", HasExplicitThis = true)]
		public Matrix4x4 GetStereoViewMatrix(Camera.StereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			Camera.GetStereoViewMatrix_Injected(intPtr, eye, out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0000D780 File Offset: 0x0000B980
		public void CopyStereoDeviceProjectionMatrixToNonJittered(Camera.StereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.CopyStereoDeviceProjectionMatrixToNonJittered_Injected(intPtr, eye);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
		[FreeFunction("CameraScripting::GetStereoProjectionMatrix", HasExplicitThis = true)]
		public Matrix4x4 GetStereoProjectionMatrix(Camera.StereoscopicEye eye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			Camera.GetStereoProjectionMatrix_Injected(intPtr, eye, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0000D7CC File Offset: 0x0000B9CC
		public void SetStereoProjectionMatrix(Camera.StereoscopicEye eye, Matrix4x4 matrix)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.SetStereoProjectionMatrix_Injected(intPtr, eye, ref matrix);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
		public void ResetStereoProjectionMatrices()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetStereoProjectionMatrices_Injected(intPtr);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0000D818 File Offset: 0x0000BA18
		public void SetStereoViewMatrix(Camera.StereoscopicEye eye, Matrix4x4 matrix)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.SetStereoViewMatrix_Injected(intPtr, eye, ref matrix);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0000D840 File Offset: 0x0000BA40
		public void ResetStereoViewMatrices()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.ResetStereoViewMatrices_Injected(intPtr);
		}

		// Token: 0x06000745 RID: 1861
		[FreeFunction("CameraScripting::GetAllCamerasCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAllCamerasCount();

		// Token: 0x06000746 RID: 1862 RVA: 0x0000D864 File Offset: 0x0000BA64
		[FreeFunction("CameraScripting::GetAllCameras")]
		private static int GetAllCamerasImpl([NotNull] [Out] Camera[] cam)
		{
			if (cam == null)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			return Camera.GetAllCamerasImpl_Injected(cam);
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0000D888 File Offset: 0x0000BA88
		public static int allCamerasCount
		{
			get
			{
				return Camera.GetAllCamerasCount();
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
		public static Camera[] allCameras
		{
			get
			{
				Camera[] array = new Camera[Camera.allCamerasCount];
				Camera.GetAllCamerasImpl(array);
				return array;
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		public static int GetAllCameras(Camera[] cameras)
		{
			bool flag = cameras == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = cameras.Length < Camera.allCamerasCount;
			if (flag2)
			{
				throw new ArgumentException("Passed in array to fill with cameras is to small to hold the number of cameras. Use Camera.allCamerasCount to get the needed size.");
			}
			return Camera.GetAllCamerasImpl(cameras);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0000D908 File Offset: 0x0000BB08
		[FreeFunction("CameraScripting::RenderToCubemap", HasExplicitThis = true)]
		private bool RenderToCubemapImpl(Texture tex, [DefaultValue("63")] int faceMask)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Camera.RenderToCubemapImpl_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(tex), faceMask);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000D934 File Offset: 0x0000BB34
		public bool RenderToCubemap(Cubemap cubemap, int faceMask)
		{
			return this.RenderToCubemapImpl(cubemap, faceMask);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000D950 File Offset: 0x0000BB50
		public bool RenderToCubemap(Cubemap cubemap)
		{
			return this.RenderToCubemapImpl(cubemap, 63);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000D96C File Offset: 0x0000BB6C
		public bool RenderToCubemap(RenderTexture cubemap, int faceMask)
		{
			return this.RenderToCubemapImpl(cubemap, faceMask);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0000D988 File Offset: 0x0000BB88
		public bool RenderToCubemap(RenderTexture cubemap)
		{
			return this.RenderToCubemapImpl(cubemap, 63);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0000D9A4 File Offset: 0x0000BBA4
		[NativeConditional("UNITY_EDITOR")]
		private int GetFilterMode()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Camera.GetFilterMode_Injected(intPtr);
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0000D9C8 File Offset: 0x0000BBC8
		[NativeConditional("UNITY_EDITOR")]
		public Camera.SceneViewFilterMode sceneViewFilterMode
		{
			get
			{
				return (Camera.SceneViewFilterMode)this.GetFilterMode();
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0000DA04 File Offset: 0x0000BC04
		[NativeConditional("UNITY_EDITOR")]
		public bool renderCloudsInSceneView
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_renderCloudsInSceneView_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Camera.set_renderCloudsInSceneView_Injected(intPtr, value);
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000DA28 File Offset: 0x0000BC28
		[NativeName("RenderToCubemap")]
		private bool RenderToCubemapEyeImpl(RenderTexture cubemap, int faceMask, Camera.MonoOrStereoscopicEye stereoEye)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Camera.RenderToCubemapEyeImpl_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(cubemap), faceMask, stereoEye);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0000DA54 File Offset: 0x0000BC54
		public bool RenderToCubemap(RenderTexture cubemap, int faceMask, Camera.MonoOrStereoscopicEye stereoEye)
		{
			return this.RenderToCubemapEyeImpl(cubemap, faceMask, stereoEye);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000DA70 File Offset: 0x0000BC70
		[FreeFunction("CameraScripting::Render", HasExplicitThis = true)]
		public void Render()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.Render_Injected(intPtr);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000DA94 File Offset: 0x0000BC94
		[FreeFunction("CameraScripting::RenderWithShader", HasExplicitThis = true)]
		public unsafe void RenderWithShader(Shader shader, string replacementTag)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<Shader>(shader);
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(replacementTag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = replacementTag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Camera.RenderWithShader_Injected(intPtr, intPtr2, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000DB00 File Offset: 0x0000BD00
		[FreeFunction("CameraScripting::RenderDontRestore", HasExplicitThis = true)]
		public void RenderDontRestore()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.RenderDontRestore_Injected(intPtr);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0000DB24 File Offset: 0x0000BD24
		[Obsolete("SubmitRenderRequests is obsolete, use SubmitRenderRequest with RequestData of supported types such as RenderPipeline.StandardRequest", true)]
		public void SubmitRenderRequests(List<Camera.RenderRequest> renderRequests)
		{
			bool flag = renderRequests == null || renderRequests.Count == 0;
			if (flag)
			{
				throw new ArgumentException("SubmitRenderRequests has been invoked with invalid renderRequests");
			}
			bool flag2 = GraphicsSettings.currentRenderPipeline == null;
			if (flag2)
			{
				Debug.LogWarning("Trying to invoke 'SubmitRenderRequests' when no SRP is set. A scriptable render pipeline is needed for this function call");
			}
			else
			{
				this.SubmitRenderRequestsInternal(renderRequests);
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0000DB78 File Offset: 0x0000BD78
		public void SubmitRenderRequest<RequestData>(RequestData renderRequest)
		{
			bool flag = renderRequest == null;
			if (flag)
			{
				throw new ArgumentException("SubmitRenderRequests is invoked with invalid renderRequests");
			}
			ObjectIdRequest objectIdRequest = renderRequest as ObjectIdRequest;
			bool flag2 = objectIdRequest != null;
			if (flag2)
			{
				bool flag3 = objectIdRequest.destination.depthStencilFormat == GraphicsFormat.None;
				if (flag3)
				{
					Debug.LogWarning("ObjectId Render Request submitted without a depth stencil, which can produce results that are not depth tested correctly");
				}
				bool flag4 = GraphicsSettings.currentRenderPipeline == null || !RenderPipelineManager.currentPipeline.IsRenderRequestSupported<ObjectIdRequest>(this, objectIdRequest);
				if (flag4)
				{
					throw new ArgumentException((GraphicsSettings.currentRenderPipeline == null) ? "The Built-In Render Pipeline does not support ObjectIdRequest outside of the editor." : "The current render pipeline does not support ObjectIdRequest, and the fallback implementation of the Built-In Render Pipeline is not available outside of the editor.");
				}
			}
			bool flag5 = GraphicsSettings.currentRenderPipeline == null;
			if (flag5)
			{
				Debug.LogWarning("Trying to invoke 'SubmitRenderRequest' when no SRP is set. A scriptable render pipeline is needed for this function call");
			}
			else
			{
				this.SubmitRenderRequestsInternal(renderRequest);
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0000DC44 File Offset: 0x0000BE44
		[FreeFunction("CameraScripting::SubmitRenderRequests", HasExplicitThis = true)]
		private void SubmitRenderRequestsInternal(object requests)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.SubmitRenderRequestsInternal_Injected(intPtr, requests);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0000DC68 File Offset: 0x0000BE68
		[NativeConditional("UNITY_EDITOR")]
		[FreeFunction("CameraScripting::SubmitBuiltInObjectIDRenderRequest", HasExplicitThis = true)]
		[return: Unmarshalled]
		private Object[] SubmitBuiltInObjectIDRenderRequest(RenderTexture target, int mipLevel, CubemapFace cubemapFace, int depthSlice)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Camera.SubmitBuiltInObjectIDRenderRequest_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(target), mipLevel, cubemapFace, depthSlice);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0000DC94 File Offset: 0x0000BE94
		[FreeFunction("CameraScripting::SetupCurrent")]
		public static void SetupCurrent(Camera cur)
		{
			Camera.SetupCurrent_Injected(Object.MarshalledUnityObject.Marshal<Camera>(cur));
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		[FreeFunction("CameraScripting::CopyFrom", HasExplicitThis = true)]
		public void CopyFrom(Camera other)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.CopyFrom_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Camera>(other));
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		public int commandBufferCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Camera.get_commandBufferCount_Injected(intPtr);
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		[NativeName("RemoveCommandBuffers")]
		private void RemoveCommandBuffersImpl(CameraEvent evt)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.RemoveCommandBuffersImpl_Injected(intPtr, evt);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0000DD1C File Offset: 0x0000BF1C
		[NativeName("RemoveAllCommandBuffers")]
		private void RemoveAllCommandBuffersImpl()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Camera.RemoveAllCommandBuffersImpl_Injected(intPtr);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0000DD40 File Offset: 0x0000BF40
		public void RemoveCommandBuffers(CameraEvent evt)
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.RemoveCommandBuffers only with the built-in renderer.");
			}
			else
			{
				this.m_NonSerializedVersion += 1U;
				this.RemoveCommandBuffersImpl(evt);
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0000DD84 File Offset: 0x0000BF84
		public void RemoveAllCommandBuffers()
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.RemoveAllCommandBuffers only with the built-in renderer.");
			}
			else
			{
				this.m_NonSerializedVersion += 1U;
				this.RemoveAllCommandBuffersImpl();
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0000DDC4 File Offset: 0x0000BFC4
		[NativeName("AddCommandBuffer")]
		private void AddCommandBufferImpl(CameraEvent evt, [NotNull] CommandBuffer buffer)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = CommandBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			Camera.AddCommandBufferImpl_Injected(intPtr, evt, intPtr2);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0000DE0C File Offset: 0x0000C00C
		[NativeName("AddCommandBufferAsync")]
		private void AddCommandBufferAsyncImpl(CameraEvent evt, [NotNull] CommandBuffer buffer, ComputeQueueType queueType)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = CommandBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			Camera.AddCommandBufferAsyncImpl_Injected(intPtr, evt, intPtr2, queueType);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0000DE54 File Offset: 0x0000C054
		[NativeName("RemoveCommandBuffer")]
		private void RemoveCommandBufferImpl(CameraEvent evt, [NotNull] CommandBuffer buffer)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = CommandBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			Camera.RemoveCommandBufferImpl_Injected(intPtr, evt, intPtr2);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000DE9C File Offset: 0x0000C09C
		public void AddCommandBuffer(CameraEvent evt, CommandBuffer buffer)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			bool flag3 = RenderPipelineManager.currentPipeline != null;
			if (flag3)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.AddCommandBuffer only with the built-in renderer.");
			}
			else
			{
				this.AddCommandBufferImpl(evt, buffer);
				this.m_NonSerializedVersion += 1U;
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000DF1C File Offset: 0x0000C11C
		public void AddCommandBufferAsync(CameraEvent evt, CommandBuffer buffer, ComputeQueueType queueType)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			bool flag3 = RenderPipelineManager.currentPipeline != null;
			if (flag3)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.AddCommandBufferAsync only with the built-in renderer.");
			}
			else
			{
				this.AddCommandBufferAsyncImpl(evt, buffer, queueType);
				this.m_NonSerializedVersion += 1U;
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000DF9C File Offset: 0x0000C19C
		public void RemoveCommandBuffer(CameraEvent evt, CommandBuffer buffer)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			bool flag3 = RenderPipelineManager.currentPipeline != null;
			if (flag3)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.RemoveCommandBuffer only with the built-in renderer.");
			}
			else
			{
				this.RemoveCommandBufferImpl(evt, buffer);
				this.m_NonSerializedVersion += 1U;
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000E01C File Offset: 0x0000C21C
		public CommandBuffer[] GetCommandBuffers(CameraEvent evt)
		{
			bool flag = RenderPipelineManager.currentPipeline != null;
			if (flag)
			{
				Debug.LogWarning("Your project uses a scriptable render pipeline. You can use Camera.GetCommandBuffers only with the built-in renderer.");
			}
			return this.GetCommandBuffersImpl(evt);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000E050 File Offset: 0x0000C250
		[FreeFunction("CameraScripting::GetCommandBuffers", HasExplicitThis = true)]
		internal CommandBuffer[] GetCommandBuffersImpl(CameraEvent evt)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Camera.GetCommandBuffersImpl_Injected(intPtr, evt);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000E074 File Offset: 0x0000C274
		[RequiredByNativeCode]
		private static void FireOnPreCull(Camera cam)
		{
			bool flag = Camera.onPreCull != null;
			if (flag)
			{
				Camera.onPreCull(cam);
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000E09C File Offset: 0x0000C29C
		[RequiredByNativeCode]
		private static void FireOnPreRender(Camera cam)
		{
			bool flag = Camera.onPreRender != null;
			if (flag)
			{
				Camera.onPreRender(cam);
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
		[RequiredByNativeCode]
		private static void FireOnPostRender(Camera cam)
		{
			bool flag = Camera.onPostRender != null;
			if (flag)
			{
				Camera.onPostRender(cam);
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0000E0EA File Offset: 0x0000C2EA
		[RequiredByNativeCode]
		private static void BumpNonSerializedVersion(Camera cam)
		{
			cam.m_NonSerializedVersion += 1U;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00002925 File Offset: 0x00000B25
		internal void OnlyUsedForTesting1()
		{
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00002925 File Offset: 0x00000B25
		internal void OnlyUsedForTesting2()
		{
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0000E0FC File Offset: 0x0000C2FC
		public unsafe bool TryGetCullingParameters(out ScriptableCullingParameters cullingParameters)
		{
			return Camera.GetCullingParameters_Internal(this, false, out cullingParameters, sizeof(ScriptableCullingParameters));
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000E11C File Offset: 0x0000C31C
		public unsafe bool TryGetCullingParameters(bool stereoAware, out ScriptableCullingParameters cullingParameters)
		{
			return Camera.GetCullingParameters_Internal(this, stereoAware, out cullingParameters, sizeof(ScriptableCullingParameters));
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0000E13C File Offset: 0x0000C33C
		[FreeFunction("ScriptableRenderPipeline_Bindings::GetCullingParameters_Internal")]
		[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h")]
		private static bool GetCullingParameters_Internal(Camera camera, bool stereoAware, out ScriptableCullingParameters cullingParameters, int managedCullingParametersSize)
		{
			return Camera.GetCullingParameters_Internal_Injected(Object.MarshalledUnityObject.Marshal<Camera>(camera), stereoAware, out cullingParameters, managedCullingParametersSize);
		}

		// Token: 0x06000774 RID: 1908
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_nearClipPlane_Injected(IntPtr _unity_self);

		// Token: 0x06000775 RID: 1909
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_nearClipPlane_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000776 RID: 1910
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_farClipPlane_Injected(IntPtr _unity_self);

		// Token: 0x06000777 RID: 1911
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_farClipPlane_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000778 RID: 1912
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_fieldOfView_Injected(IntPtr _unity_self);

		// Token: 0x06000779 RID: 1913
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fieldOfView_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600077A RID: 1914
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderingPath get_renderingPath_Injected(IntPtr _unity_self);

		// Token: 0x0600077B RID: 1915
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderingPath_Injected(IntPtr _unity_self, RenderingPath value);

		// Token: 0x0600077C RID: 1916
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderingPath get_actualRenderingPath_Injected(IntPtr _unity_self);

		// Token: 0x0600077D RID: 1917
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Reset_Injected(IntPtr _unity_self);

		// Token: 0x0600077E RID: 1918
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_allowHDR_Injected(IntPtr _unity_self);

		// Token: 0x0600077F RID: 1919
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_allowHDR_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000780 RID: 1920
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_allowMSAA_Injected(IntPtr _unity_self);

		// Token: 0x06000781 RID: 1921
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_allowMSAA_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000782 RID: 1922
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_allowDynamicResolution_Injected(IntPtr _unity_self);

		// Token: 0x06000783 RID: 1923
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_allowDynamicResolution_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000784 RID: 1924
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_forceIntoRenderTexture_Injected(IntPtr _unity_self);

		// Token: 0x06000785 RID: 1925
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceIntoRenderTexture_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000786 RID: 1926
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_orthographicSize_Injected(IntPtr _unity_self);

		// Token: 0x06000787 RID: 1927
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_orthographicSize_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000788 RID: 1928
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_orthographic_Injected(IntPtr _unity_self);

		// Token: 0x06000789 RID: 1929
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_orthographic_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600078A RID: 1930
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern OpaqueSortMode get_opaqueSortMode_Injected(IntPtr _unity_self);

		// Token: 0x0600078B RID: 1931
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_opaqueSortMode_Injected(IntPtr _unity_self, OpaqueSortMode value);

		// Token: 0x0600078C RID: 1932
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TransparencySortMode get_transparencySortMode_Injected(IntPtr _unity_self);

		// Token: 0x0600078D RID: 1933
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_transparencySortMode_Injected(IntPtr _unity_self, TransparencySortMode value);

		// Token: 0x0600078E RID: 1934
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_transparencySortAxis_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600078F RID: 1935
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_transparencySortAxis_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000790 RID: 1936
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetTransparencySortSettings_Injected(IntPtr _unity_self);

		// Token: 0x06000791 RID: 1937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_depth_Injected(IntPtr _unity_self);

		// Token: 0x06000792 RID: 1938
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_depth_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000793 RID: 1939
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_aspect_Injected(IntPtr _unity_self);

		// Token: 0x06000794 RID: 1940
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_aspect_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000795 RID: 1941
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetAspect_Injected(IntPtr _unity_self);

		// Token: 0x06000796 RID: 1942
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_velocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000797 RID: 1943
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_cullingMask_Injected(IntPtr _unity_self);

		// Token: 0x06000798 RID: 1944
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cullingMask_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000799 RID: 1945
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_eventMask_Injected(IntPtr _unity_self);

		// Token: 0x0600079A RID: 1946
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_eventMask_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600079B RID: 1947
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_layerCullSphericalInternal_Injected(IntPtr _unity_self);

		// Token: 0x0600079C RID: 1948
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_layerCullSphericalInternal_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600079D RID: 1949
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CameraType get_cameraType_Injected(IntPtr _unity_self);

		// Token: 0x0600079E RID: 1950
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cameraType_Injected(IntPtr _unity_self, CameraType value);

		// Token: 0x0600079F RID: 1951
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_skyboxMaterial_Injected(IntPtr _unity_self);

		// Token: 0x060007A0 RID: 1952
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong get_overrideSceneCullingMask_Injected(IntPtr _unity_self);

		// Token: 0x060007A1 RID: 1953
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_overrideSceneCullingMask_Injected(IntPtr _unity_self, ulong value);

		// Token: 0x060007A2 RID: 1954
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong get_sceneCullingMask_Injected(IntPtr _unity_self);

		// Token: 0x060007A3 RID: 1955
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useInteractiveLightBakingData_Injected(IntPtr _unity_self);

		// Token: 0x060007A4 RID: 1956
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useInteractiveLightBakingData_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060007A5 RID: 1957
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLayerCullDistances_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060007A6 RID: 1958
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLayerCullDistances_Injected(IntPtr _unity_self, ref ManagedSpanWrapper d);

		// Token: 0x060007A7 RID: 1959
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useOcclusionCulling_Injected(IntPtr _unity_self);

		// Token: 0x060007A8 RID: 1960
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useOcclusionCulling_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060007A9 RID: 1961
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_cullingMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x060007AA RID: 1962
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cullingMatrix_Injected(IntPtr _unity_self, [In] ref Matrix4x4 value);

		// Token: 0x060007AB RID: 1963
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetCullingMatrix_Injected(IntPtr _unity_self);

		// Token: 0x060007AC RID: 1964
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_backgroundColor_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x060007AD RID: 1965
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_backgroundColor_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x060007AE RID: 1966
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CameraClearFlags get_clearFlags_Injected(IntPtr _unity_self);

		// Token: 0x060007AF RID: 1967
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_clearFlags_Injected(IntPtr _unity_self, CameraClearFlags value);

		// Token: 0x060007B0 RID: 1968
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DepthTextureMode get_depthTextureMode_Injected(IntPtr _unity_self);

		// Token: 0x060007B1 RID: 1969
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_depthTextureMode_Injected(IntPtr _unity_self, DepthTextureMode value);

		// Token: 0x060007B2 RID: 1970
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_clearStencilAfterLightingPass_Injected(IntPtr _unity_self);

		// Token: 0x060007B3 RID: 1971
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_clearStencilAfterLightingPass_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060007B4 RID: 1972
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetReplacementShader_Injected(IntPtr _unity_self, IntPtr shader, ref ManagedSpanWrapper replacementTag);

		// Token: 0x060007B5 RID: 1973
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetReplacementShader_Injected(IntPtr _unity_self);

		// Token: 0x060007B6 RID: 1974
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Camera.ProjectionMatrixMode get_projectionMatrixMode_Injected(IntPtr _unity_self);

		// Token: 0x060007B7 RID: 1975
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_usePhysicalProperties_Injected(IntPtr _unity_self);

		// Token: 0x060007B8 RID: 1976
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_usePhysicalProperties_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060007B9 RID: 1977
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_iso_Injected(IntPtr _unity_self);

		// Token: 0x060007BA RID: 1978
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_iso_Injected(IntPtr _unity_self, int value);

		// Token: 0x060007BB RID: 1979
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_shutterSpeed_Injected(IntPtr _unity_self);

		// Token: 0x060007BC RID: 1980
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shutterSpeed_Injected(IntPtr _unity_self, float value);

		// Token: 0x060007BD RID: 1981
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_aperture_Injected(IntPtr _unity_self);

		// Token: 0x060007BE RID: 1982
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_aperture_Injected(IntPtr _unity_self, float value);

		// Token: 0x060007BF RID: 1983
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_focusDistance_Injected(IntPtr _unity_self);

		// Token: 0x060007C0 RID: 1984
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_focusDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x060007C1 RID: 1985
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_focalLength_Injected(IntPtr _unity_self);

		// Token: 0x060007C2 RID: 1986
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_focalLength_Injected(IntPtr _unity_self, float value);

		// Token: 0x060007C3 RID: 1987
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_bladeCount_Injected(IntPtr _unity_self);

		// Token: 0x060007C4 RID: 1988
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bladeCount_Injected(IntPtr _unity_self, int value);

		// Token: 0x060007C5 RID: 1989
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_curvature_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060007C6 RID: 1990
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_curvature_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060007C7 RID: 1991
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_barrelClipping_Injected(IntPtr _unity_self);

		// Token: 0x060007C8 RID: 1992
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_barrelClipping_Injected(IntPtr _unity_self, float value);

		// Token: 0x060007C9 RID: 1993
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_anamorphism_Injected(IntPtr _unity_self);

		// Token: 0x060007CA RID: 1994
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anamorphism_Injected(IntPtr _unity_self, float value);

		// Token: 0x060007CB RID: 1995
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_sensorSize_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060007CC RID: 1996
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sensorSize_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060007CD RID: 1997
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_lensShift_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060007CE RID: 1998
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lensShift_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060007CF RID: 1999
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Camera.GateFitMode get_gateFit_Injected(IntPtr _unity_self);

		// Token: 0x060007D0 RID: 2000
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_gateFit_Injected(IntPtr _unity_self, Camera.GateFitMode value);

		// Token: 0x060007D1 RID: 2001
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetGateFittedFieldOfView_Injected(IntPtr _unity_self);

		// Token: 0x060007D2 RID: 2002
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGateFittedLensShift_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060007D3 RID: 2003
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalSpaceAim_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060007D4 RID: 2004
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rect_Injected(IntPtr _unity_self, out Rect ret);

		// Token: 0x060007D5 RID: 2005
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rect_Injected(IntPtr _unity_self, [In] ref Rect value);

		// Token: 0x060007D6 RID: 2006
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_pixelRect_Injected(IntPtr _unity_self, out Rect ret);

		// Token: 0x060007D7 RID: 2007
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_pixelRect_Injected(IntPtr _unity_self, [In] ref Rect value);

		// Token: 0x060007D8 RID: 2008
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_pixelWidth_Injected(IntPtr _unity_self);

		// Token: 0x060007D9 RID: 2009
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_pixelHeight_Injected(IntPtr _unity_self);

		// Token: 0x060007DA RID: 2010
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_scaledPixelWidth_Injected(IntPtr _unity_self);

		// Token: 0x060007DB RID: 2011
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_scaledPixelHeight_Injected(IntPtr _unity_self);

		// Token: 0x060007DC RID: 2012
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_targetTexture_Injected(IntPtr _unity_self);

		// Token: 0x060007DD RID: 2013
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetTexture_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060007DE RID: 2014
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_activeTexture_Injected(IntPtr _unity_self);

		// Token: 0x060007DF RID: 2015
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_targetDisplay_Injected(IntPtr _unity_self);

		// Token: 0x060007E0 RID: 2016
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetDisplay_Injected(IntPtr _unity_self, int value);

		// Token: 0x060007E1 RID: 2017
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTargetBuffersImpl_Injected(IntPtr _unity_self, [In] ref RenderBuffer color, [In] ref RenderBuffer depth);

		// Token: 0x060007E2 RID: 2018
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTargetBuffersMRTImpl_Injected(IntPtr _unity_self, ref ManagedSpanWrapper color, [In] ref RenderBuffer depth);

		// Token: 0x060007E3 RID: 2019
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetCameraBufferWarnings_Injected(IntPtr _unity_self);

		// Token: 0x060007E4 RID: 2020
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_cameraToWorldMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x060007E5 RID: 2021
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_worldToCameraMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x060007E6 RID: 2022
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_worldToCameraMatrix_Injected(IntPtr _unity_self, [In] ref Matrix4x4 value);

		// Token: 0x060007E7 RID: 2023
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_projectionMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x060007E8 RID: 2024
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_projectionMatrix_Injected(IntPtr _unity_self, [In] ref Matrix4x4 value);

		// Token: 0x060007E9 RID: 2025
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_nonJitteredProjectionMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x060007EA RID: 2026
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_nonJitteredProjectionMatrix_Injected(IntPtr _unity_self, [In] ref Matrix4x4 value);

		// Token: 0x060007EB RID: 2027
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useJitteredProjectionMatrixForTransparentRendering_Injected(IntPtr _unity_self);

		// Token: 0x060007EC RID: 2028
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useJitteredProjectionMatrixForTransparentRendering_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060007ED RID: 2029
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_previousViewProjectionMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x060007EE RID: 2030
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetWorldToCameraMatrix_Injected(IntPtr _unity_self);

		// Token: 0x060007EF RID: 2031
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetProjectionMatrix_Injected(IntPtr _unity_self);

		// Token: 0x060007F0 RID: 2032
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateObliqueMatrix_Injected(IntPtr _unity_self, [In] ref Vector4 clipPlane, out Matrix4x4 ret);

		// Token: 0x060007F1 RID: 2033
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WorldToScreenPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x060007F2 RID: 2034
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WorldToViewportPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x060007F3 RID: 2035
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ViewportToWorldPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x060007F4 RID: 2036
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScreenToWorldPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x060007F5 RID: 2037
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScreenToViewportPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, out Vector3 ret);

		// Token: 0x060007F6 RID: 2038
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ViewportToScreenPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, out Vector3 ret);

		// Token: 0x060007F7 RID: 2039
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetFrustumPlaneSizeAt_Injected(IntPtr _unity_self, float distance, out Vector2 ret);

		// Token: 0x060007F8 RID: 2040
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ViewportPointToRay_Injected(IntPtr _unity_self, [In] ref Vector2 pos, Camera.MonoOrStereoscopicEye eye, out Ray ret);

		// Token: 0x060007F9 RID: 2041
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScreenPointToRay_Injected(IntPtr _unity_self, [In] ref Vector2 pos, Camera.MonoOrStereoscopicEye eye, out Ray ret);

		// Token: 0x060007FA RID: 2042
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateFrustumCornersInternal_Injected(IntPtr _unity_self, [In] ref Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, out BlittableArrayWrapper outCorners);

		// Token: 0x060007FB RID: 2043
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateProjectionMatrixFromPhysicalPropertiesInternal_Injected(out Matrix4x4 output, float focalLength, [In] ref Vector2 sensorSize, [In] ref Vector2 lensShift, float nearClip, float farClip, float gateAspect, Camera.GateFitMode gateFitMode);

		// Token: 0x060007FC RID: 2044
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_main_Injected();

		// Token: 0x060007FD RID: 2045
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_currentInternal_Injected();

		// Token: 0x060007FE RID: 2046
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_scene_Injected(IntPtr _unity_self, out Scene ret);

		// Token: 0x060007FF RID: 2047
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_scene_Injected(IntPtr _unity_self, [In] ref Scene value);

		// Token: 0x06000800 RID: 2048
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_stereoEnabled_Injected(IntPtr _unity_self);

		// Token: 0x06000801 RID: 2049
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_stereoSeparation_Injected(IntPtr _unity_self);

		// Token: 0x06000802 RID: 2050
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stereoSeparation_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000803 RID: 2051
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_stereoConvergence_Injected(IntPtr _unity_self);

		// Token: 0x06000804 RID: 2052
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stereoConvergence_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000805 RID: 2053
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_areVRStereoViewMatricesWithinSingleCullTolerance_Injected(IntPtr _unity_self);

		// Token: 0x06000806 RID: 2054
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern StereoTargetEyeMask get_stereoTargetEyeInternal_Injected(IntPtr _unity_self);

		// Token: 0x06000807 RID: 2055
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stereoTargetEyeInternal_Injected(IntPtr _unity_self, StereoTargetEyeMask value);

		// Token: 0x06000808 RID: 2056
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Camera.MonoOrStereoscopicEye get_stereoActiveEye_Injected(IntPtr _unity_self);

		// Token: 0x06000809 RID: 2057
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStereoNonJitteredProjectionMatrix_Injected(IntPtr _unity_self, Camera.StereoscopicEye eye, out Matrix4x4 ret);

		// Token: 0x0600080A RID: 2058
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStereoViewMatrix_Injected(IntPtr _unity_self, Camera.StereoscopicEye eye, out Matrix4x4 ret);

		// Token: 0x0600080B RID: 2059
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyStereoDeviceProjectionMatrixToNonJittered_Injected(IntPtr _unity_self, Camera.StereoscopicEye eye);

		// Token: 0x0600080C RID: 2060
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStereoProjectionMatrix_Injected(IntPtr _unity_self, Camera.StereoscopicEye eye, out Matrix4x4 ret);

		// Token: 0x0600080D RID: 2061
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStereoProjectionMatrix_Injected(IntPtr _unity_self, Camera.StereoscopicEye eye, [In] ref Matrix4x4 matrix);

		// Token: 0x0600080E RID: 2062
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetStereoProjectionMatrices_Injected(IntPtr _unity_self);

		// Token: 0x0600080F RID: 2063
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStereoViewMatrix_Injected(IntPtr _unity_self, Camera.StereoscopicEye eye, [In] ref Matrix4x4 matrix);

		// Token: 0x06000810 RID: 2064
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetStereoViewMatrices_Injected(IntPtr _unity_self);

		// Token: 0x06000811 RID: 2065
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAllCamerasImpl_Injected([Out] Camera[] cam);

		// Token: 0x06000812 RID: 2066
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RenderToCubemapImpl_Injected(IntPtr _unity_self, IntPtr tex, [DefaultValue("63")] int faceMask);

		// Token: 0x06000813 RID: 2067
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFilterMode_Injected(IntPtr _unity_self);

		// Token: 0x06000814 RID: 2068
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_renderCloudsInSceneView_Injected(IntPtr _unity_self);

		// Token: 0x06000815 RID: 2069
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderCloudsInSceneView_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000816 RID: 2070
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RenderToCubemapEyeImpl_Injected(IntPtr _unity_self, IntPtr cubemap, int faceMask, Camera.MonoOrStereoscopicEye stereoEye);

		// Token: 0x06000817 RID: 2071
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Render_Injected(IntPtr _unity_self);

		// Token: 0x06000818 RID: 2072
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RenderWithShader_Injected(IntPtr _unity_self, IntPtr shader, ref ManagedSpanWrapper replacementTag);

		// Token: 0x06000819 RID: 2073
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RenderDontRestore_Injected(IntPtr _unity_self);

		// Token: 0x0600081A RID: 2074
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SubmitRenderRequestsInternal_Injected(IntPtr _unity_self, object requests);

		// Token: 0x0600081B RID: 2075
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object[] SubmitBuiltInObjectIDRenderRequest_Injected(IntPtr _unity_self, IntPtr target, int mipLevel, CubemapFace cubemapFace, int depthSlice);

		// Token: 0x0600081C RID: 2076
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetupCurrent_Injected(IntPtr cur);

		// Token: 0x0600081D RID: 2077
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyFrom_Injected(IntPtr _unity_self, IntPtr other);

		// Token: 0x0600081E RID: 2078
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_commandBufferCount_Injected(IntPtr _unity_self);

		// Token: 0x0600081F RID: 2079
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveCommandBuffersImpl_Injected(IntPtr _unity_self, CameraEvent evt);

		// Token: 0x06000820 RID: 2080
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveAllCommandBuffersImpl_Injected(IntPtr _unity_self);

		// Token: 0x06000821 RID: 2081
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddCommandBufferImpl_Injected(IntPtr _unity_self, CameraEvent evt, IntPtr buffer);

		// Token: 0x06000822 RID: 2082
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddCommandBufferAsyncImpl_Injected(IntPtr _unity_self, CameraEvent evt, IntPtr buffer, ComputeQueueType queueType);

		// Token: 0x06000823 RID: 2083
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveCommandBufferImpl_Injected(IntPtr _unity_self, CameraEvent evt, IntPtr buffer);

		// Token: 0x06000824 RID: 2084
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CommandBuffer[] GetCommandBuffersImpl_Injected(IntPtr _unity_self, CameraEvent evt);

		// Token: 0x06000825 RID: 2085
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetCullingParameters_Internal_Injected(IntPtr camera, bool stereoAware, out ScriptableCullingParameters cullingParameters, int managedCullingParametersSize);

		// Token: 0x040003C0 RID: 960
		public const float kMinAperture = 0.7f;

		// Token: 0x040003C1 RID: 961
		public const float kMaxAperture = 32f;

		// Token: 0x040003C2 RID: 962
		public const int kMinBladeCount = 3;

		// Token: 0x040003C3 RID: 963
		public const int kMaxBladeCount = 11;

		// Token: 0x040003C4 RID: 964
		internal uint m_NonSerializedVersion;

		// Token: 0x040003C5 RID: 965
		public static Camera.CameraCallback onPreCull;

		// Token: 0x040003C6 RID: 966
		public static Camera.CameraCallback onPreRender;

		// Token: 0x040003C7 RID: 967
		public static Camera.CameraCallback onPostRender;

		// Token: 0x0200011E RID: 286
		internal enum ProjectionMatrixMode
		{
			// Token: 0x040003C9 RID: 969
			Explicit,
			// Token: 0x040003CA RID: 970
			Implicit,
			// Token: 0x040003CB RID: 971
			PhysicalPropertiesBased
		}

		// Token: 0x0200011F RID: 287
		public enum GateFitMode
		{
			// Token: 0x040003CD RID: 973
			Vertical = 1,
			// Token: 0x040003CE RID: 974
			Horizontal,
			// Token: 0x040003CF RID: 975
			Fill,
			// Token: 0x040003D0 RID: 976
			Overscan,
			// Token: 0x040003D1 RID: 977
			None = 0
		}

		// Token: 0x02000120 RID: 288
		public enum FieldOfViewAxis
		{
			// Token: 0x040003D3 RID: 979
			Vertical,
			// Token: 0x040003D4 RID: 980
			Horizontal
		}

		// Token: 0x02000121 RID: 289
		public struct GateFitParameters
		{
			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06000826 RID: 2086 RVA: 0x0000E157 File Offset: 0x0000C357
			// (set) Token: 0x06000827 RID: 2087 RVA: 0x0000E15F File Offset: 0x0000C35F
			public Camera.GateFitMode mode { readonly get; set; }

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06000828 RID: 2088 RVA: 0x0000E168 File Offset: 0x0000C368
			// (set) Token: 0x06000829 RID: 2089 RVA: 0x0000E170 File Offset: 0x0000C370
			public float aspect { readonly get; set; }

			// Token: 0x0600082A RID: 2090 RVA: 0x0000E179 File Offset: 0x0000C379
			public GateFitParameters(Camera.GateFitMode mode, float aspect)
			{
				this.mode = mode;
				this.aspect = aspect;
			}
		}

		// Token: 0x02000122 RID: 290
		public enum StereoscopicEye
		{
			// Token: 0x040003D8 RID: 984
			Left,
			// Token: 0x040003D9 RID: 985
			Right
		}

		// Token: 0x02000123 RID: 291
		public enum MonoOrStereoscopicEye
		{
			// Token: 0x040003DB RID: 987
			Left,
			// Token: 0x040003DC RID: 988
			Right,
			// Token: 0x040003DD RID: 989
			Mono
		}

		// Token: 0x02000124 RID: 292
		public enum SceneViewFilterMode
		{
			// Token: 0x040003DF RID: 991
			Off,
			// Token: 0x040003E0 RID: 992
			ShowFiltered
		}

		// Token: 0x02000125 RID: 293
		[Obsolete("The RenderRequest struct is obsolete, use the function overload with RequestData of supported types such as RenderPipeline.StandardRequest", true)]
		public enum RenderRequestMode
		{
			// Token: 0x040003E2 RID: 994
			None,
			// Token: 0x040003E3 RID: 995
			ObjectId,
			// Token: 0x040003E4 RID: 996
			Depth,
			// Token: 0x040003E5 RID: 997
			VertexNormal,
			// Token: 0x040003E6 RID: 998
			WorldPosition,
			// Token: 0x040003E7 RID: 999
			EntityId,
			// Token: 0x040003E8 RID: 1000
			BaseColor,
			// Token: 0x040003E9 RID: 1001
			SpecularColor,
			// Token: 0x040003EA RID: 1002
			Metallic,
			// Token: 0x040003EB RID: 1003
			Emission,
			// Token: 0x040003EC RID: 1004
			Normal,
			// Token: 0x040003ED RID: 1005
			Smoothness,
			// Token: 0x040003EE RID: 1006
			Occlusion,
			// Token: 0x040003EF RID: 1007
			DiffuseColor
		}

		// Token: 0x02000126 RID: 294
		[Obsolete("The RenderRequest struct is obsolete, use the function overload with RequestData of supported types such as RenderPipeline.StandardRequest", true)]
		public enum RenderRequestOutputSpace
		{
			// Token: 0x040003F1 RID: 1009
			ScreenSpace = -1,
			// Token: 0x040003F2 RID: 1010
			UV0,
			// Token: 0x040003F3 RID: 1011
			UV1,
			// Token: 0x040003F4 RID: 1012
			UV2,
			// Token: 0x040003F5 RID: 1013
			UV3,
			// Token: 0x040003F6 RID: 1014
			UV4,
			// Token: 0x040003F7 RID: 1015
			UV5,
			// Token: 0x040003F8 RID: 1016
			UV6,
			// Token: 0x040003F9 RID: 1017
			UV7,
			// Token: 0x040003FA RID: 1018
			UV8
		}

		// Token: 0x02000127 RID: 295
		[Obsolete("The RenderRequest struct is obsolete, use the function overload with RequestData of supported types such as RenderPipeline.StandardRequest", true)]
		public struct RenderRequest
		{
			// Token: 0x0600082B RID: 2091 RVA: 0x0000E18C File Offset: 0x0000C38C
			public RenderRequest(Camera.RenderRequestMode mode, RenderTexture rt)
			{
				this.m_CameraRenderMode = mode;
				this.m_ResultRT = rt;
				this.m_OutputSpace = Camera.RenderRequestOutputSpace.ScreenSpace;
			}

			// Token: 0x0600082C RID: 2092 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
			public RenderRequest(Camera.RenderRequestMode mode, Camera.RenderRequestOutputSpace space, RenderTexture rt)
			{
				this.m_CameraRenderMode = mode;
				this.m_ResultRT = rt;
				this.m_OutputSpace = space;
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x0600082D RID: 2093 RVA: 0x0000E1BC File Offset: 0x0000C3BC
			public bool isValid
			{
				get
				{
					return this.m_CameraRenderMode != Camera.RenderRequestMode.None && this.m_ResultRT != null;
				}
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x0600082E RID: 2094 RVA: 0x0000E1D5 File Offset: 0x0000C3D5
			public Camera.RenderRequestMode mode
			{
				get
				{
					return this.m_CameraRenderMode;
				}
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x0600082F RID: 2095 RVA: 0x0000E1DD File Offset: 0x0000C3DD
			public RenderTexture result
			{
				get
				{
					return this.m_ResultRT;
				}
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x06000830 RID: 2096 RVA: 0x0000E1E5 File Offset: 0x0000C3E5
			public Camera.RenderRequestOutputSpace outputSpace
			{
				get
				{
					return this.m_OutputSpace;
				}
			}

			// Token: 0x040003FB RID: 1019
			private readonly Camera.RenderRequestMode m_CameraRenderMode;

			// Token: 0x040003FC RID: 1020
			private readonly RenderTexture m_ResultRT;

			// Token: 0x040003FD RID: 1021
			private readonly Camera.RenderRequestOutputSpace m_OutputSpace;
		}

		// Token: 0x02000128 RID: 296
		// (Invoke) Token: 0x06000832 RID: 2098
		public delegate void CameraCallback(Camera cam);
	}
}
