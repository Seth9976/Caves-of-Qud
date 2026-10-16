using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000157 RID: 343
	[PreventReadOnlyInstanceModification]
	[NativeHeader("Runtime/Graphics/LightingSettings.h")]
	public sealed class LightingSettings : Object
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x00002925 File Offset: 0x00000B25
		[RequiredByNativeCode]
		internal void LightingSettingsDontStripMe()
		{
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00013E67 File Offset: 0x00012067
		public LightingSettings()
		{
			LightingSettings.Internal_Create(this);
		}

		// Token: 0x06000AB2 RID: 2738
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] LightingSettings self);

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00013E78 File Offset: 0x00012078
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x00013E9C File Offset: 0x0001209C
		[NativeName("EnableBakedLightmaps")]
		public bool bakedGI
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightingSettings>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightingSettings.get_bakedGI_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightingSettings>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightingSettings.set_bakedGI_Injected(intPtr, value);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00013EC0 File Offset: 0x000120C0
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x00013EE4 File Offset: 0x000120E4
		[NativeName("EnableRealtimeLightmaps")]
		public bool realtimeGI
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightingSettings>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightingSettings.get_realtimeGI_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightingSettings>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightingSettings.set_realtimeGI_Injected(intPtr, value);
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x00013F08 File Offset: 0x00012108
		// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x00013F2C File Offset: 0x0001212C
		[NativeName("RealtimeEnvironmentLighting")]
		public bool realtimeEnvironmentLighting
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightingSettings>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightingSettings.get_realtimeEnvironmentLighting_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightingSettings>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LightingSettings.set_realtimeEnvironmentLighting_Injected(intPtr, value);
			}
		}

		// Token: 0x06000AB9 RID: 2745
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_bakedGI_Injected(IntPtr _unity_self);

		// Token: 0x06000ABA RID: 2746
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bakedGI_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000ABB RID: 2747
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_realtimeGI_Injected(IntPtr _unity_self);

		// Token: 0x06000ABC RID: 2748
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_realtimeGI_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000ABD RID: 2749
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_realtimeEnvironmentLighting_Injected(IntPtr _unity_self);

		// Token: 0x06000ABE RID: 2750
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_realtimeEnvironmentLighting_Injected(IntPtr _unity_self, bool value);
	}
}
