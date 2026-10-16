using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000018 RID: 24
	[NativeHeader("Modules/VFX/Public/ScriptBindings/VFXManagerBindings.h")]
	[NativeHeader("Modules/VFX/Public/VFXManager.h")]
	[RequiredByNativeCode]
	[StaticAccessor("GetVFXManager()", StaticAccessorType.Dot)]
	public static class VFXManager
	{
		// Token: 0x06000087 RID: 135
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern VisualEffect[] GetComponents();

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002C98 File Offset: 0x00000E98
		internal static ScriptableObject runtimeResources
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<ScriptableObject>(VFXManager.get_runtimeResources_Injected());
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000089 RID: 137
		// (set) Token: 0x0600008A RID: 138
		public static extern float fixedTimeStep
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600008B RID: 139
		// (set) Token: 0x0600008C RID: 140
		public static extern float maxDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600008D RID: 141
		// (set) Token: 0x0600008E RID: 142
		internal static extern uint maxCapacity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600008F RID: 143
		// (set) Token: 0x06000090 RID: 144
		internal static extern float maxScrubTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002CB0 File Offset: 0x00000EB0
		internal static string renderPipeSettingsPath
		{
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					VFXManager.get_renderPipeSettingsPath_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000092 RID: 146
		// (set) Token: 0x06000093 RID: 147
		internal static extern uint batchEmptyLifetime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000094 RID: 148
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CleanupEmptyBatches(bool force = false);

		// Token: 0x06000095 RID: 149 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public static void FlushEmptyBatches()
		{
			VFXManager.CleanupEmptyBatches(true);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002CEC File Offset: 0x00000EEC
		public static VFXBatchedEffectInfo GetBatchedEffectInfo([NotNull] VisualEffectAsset vfx)
		{
			if (vfx == null)
			{
				ThrowHelper.ThrowArgumentNullException(vfx, "vfx");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffectAsset>(vfx);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(vfx, "vfx");
			}
			VFXBatchedEffectInfo vfxbatchedEffectInfo;
			VFXManager.GetBatchedEffectInfo_Injected(intPtr, out vfxbatchedEffectInfo);
			return vfxbatchedEffectInfo;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002D28 File Offset: 0x00000F28
		[FreeFunction(Name = "VFXManagerBindings::GetBatchedEffectInfos", HasExplicitThis = false)]
		public static void GetBatchedEffectInfos([NotNull] List<VFXBatchedEffectInfo> infos)
		{
			if (infos == null)
			{
				ThrowHelper.ThrowArgumentNullException(infos, "infos");
			}
			VFXManager.GetBatchedEffectInfos_Injected(infos);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002D4C File Offset: 0x00000F4C
		internal static VFXBatchInfo GetBatchInfo(VisualEffectAsset vfx, uint batchIndex)
		{
			VFXBatchInfo vfxbatchInfo;
			VFXManager.GetBatchInfo_Injected(Object.MarshalledUnityObject.Marshal<VisualEffectAsset>(vfx), batchIndex, out vfxbatchInfo);
			return vfxbatchInfo;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002D68 File Offset: 0x00000F68
		[Obsolete("Use explicit PrepareCamera and ProcessCameraCommand instead")]
		public static void ProcessCamera(Camera cam)
		{
			VFXManager.PrepareCamera(cam, VFXManager.kDefaultCameraXRSettings);
			VFXManager.Internal_ProcessCameraCommand(cam, null, VFXManager.kDefaultCameraXRSettings, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002D8E File Offset: 0x00000F8E
		public static void PrepareCamera(Camera cam)
		{
			VFXManager.PrepareCamera(cam, VFXManager.kDefaultCameraXRSettings);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public static void PrepareCamera([NotNull] Camera cam, VFXCameraXRSettings camXRSettings)
		{
			if (cam == null)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(cam);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			VFXManager.PrepareCamera_Injected(intPtr, ref camXRSettings);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002DD8 File Offset: 0x00000FD8
		[Obsolete("Use ProcessCameraCommand with CullingResults to allow culling of VFX per camera")]
		public static void ProcessCameraCommand(Camera cam, CommandBuffer cmd)
		{
			VFXManager.Internal_ProcessCameraCommand(cam, cmd, VFXManager.kDefaultCameraXRSettings, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002DF2 File Offset: 0x00000FF2
		[Obsolete("Use ProcessCameraCommand with CullingResults to allow culling of VFX per camera")]
		public static void ProcessCameraCommand(Camera cam, CommandBuffer cmd, VFXCameraXRSettings camXRSettings)
		{
			VFXManager.Internal_ProcessCameraCommand(cam, cmd, camXRSettings, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002E08 File Offset: 0x00001008
		public static void ProcessCameraCommand(Camera cam, CommandBuffer cmd, VFXCameraXRSettings camXRSettings, CullingResults results)
		{
			VFXManager.Internal_ProcessCameraCommand(cam, cmd, camXRSettings, results.ptr, IntPtr.Zero);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002E1F File Offset: 0x0000101F
		public static void ProcessCameraCommand(Camera cam, CommandBuffer cmd, VFXCameraXRSettings camXRSettings, CullingResults results, CullingResults customPassResults)
		{
			VFXManager.Internal_ProcessCameraCommand(cam, cmd, camXRSettings, results.ptr, customPassResults.ptr);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002E38 File Offset: 0x00001038
		private static void Internal_ProcessCameraCommand([NotNull] Camera cam, CommandBuffer cmd, VFXCameraXRSettings camXRSettings, IntPtr cullResults, IntPtr customPassCullResults)
		{
			if (cam == null)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(cam);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			VFXManager.Internal_ProcessCameraCommand_Injected(intPtr, (cmd == null) ? ((IntPtr)0) : CommandBuffer.BindingsMarshaller.ConvertToNative(cmd), ref camXRSettings, cullResults, customPassCullResults);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002E84 File Offset: 0x00001084
		public static VFXCameraBufferTypes IsCameraBufferNeeded([NotNull] Camera cam)
		{
			if (cam == null)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(cam);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			return VFXManager.IsCameraBufferNeeded_Injected(intPtr);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002EBC File Offset: 0x000010BC
		public static void SetCameraBuffer([NotNull] Camera cam, VFXCameraBufferTypes type, Texture buffer, int x, int y, int width, int height)
		{
			if (cam == null)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Camera>(cam);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(cam, "cam");
			}
			VFXManager.SetCameraBuffer_Injected(intPtr, type, Object.MarshalledUnityObject.Marshal<Texture>(buffer), x, y, width, height);
		}

		// Token: 0x060000A3 RID: 163
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetRayTracingEnabled(bool enabled);

		// Token: 0x060000A4 RID: 164
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RequestRtasAabbConstruction();

		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_runtimeResources_Injected();

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_renderPipeSettingsPath_Injected(out ManagedSpanWrapper ret);

		// Token: 0x060000A8 RID: 168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBatchedEffectInfo_Injected(IntPtr vfx, out VFXBatchedEffectInfo ret);

		// Token: 0x060000A9 RID: 169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBatchedEffectInfos_Injected(List<VFXBatchedEffectInfo> infos);

		// Token: 0x060000AA RID: 170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBatchInfo_Injected(IntPtr vfx, uint batchIndex, out VFXBatchInfo ret);

		// Token: 0x060000AB RID: 171
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PrepareCamera_Injected(IntPtr cam, [In] ref VFXCameraXRSettings camXRSettings);

		// Token: 0x060000AC RID: 172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ProcessCameraCommand_Injected(IntPtr cam, IntPtr cmd, [In] ref VFXCameraXRSettings camXRSettings, IntPtr cullResults, IntPtr customPassCullResults);

		// Token: 0x060000AD RID: 173
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VFXCameraBufferTypes IsCameraBufferNeeded_Injected(IntPtr cam);

		// Token: 0x060000AE RID: 174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCameraBuffer_Injected(IntPtr cam, VFXCameraBufferTypes type, IntPtr buffer, int x, int y, int width, int height);

		// Token: 0x0400011E RID: 286
		private static readonly VFXCameraXRSettings kDefaultCameraXRSettings = new VFXCameraXRSettings
		{
			viewTotal = 1U,
			viewCount = 1U,
			viewOffset = 0U
		};
	}
}
