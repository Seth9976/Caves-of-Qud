using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000193 RID: 403
	[NativeHeader("Runtime/Camera/OcclusionPortal.h")]
	public sealed class OcclusionPortal : Component
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x00021FB8 File Offset: 0x000201B8
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x00021FDC File Offset: 0x000201DC
		[NativeProperty("IsOpen")]
		public bool open
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OcclusionPortal>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return OcclusionPortal.get_open_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OcclusionPortal>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				OcclusionPortal.set_open_Injected(intPtr, value);
			}
		}

		// Token: 0x060013A3 RID: 5027
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_open_Injected(IntPtr _unity_self);

		// Token: 0x060013A4 RID: 5028
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_open_Injected(IntPtr _unity_self, bool value);
	}
}
