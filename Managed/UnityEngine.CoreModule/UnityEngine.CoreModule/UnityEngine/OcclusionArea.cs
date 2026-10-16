using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000194 RID: 404
	[NativeHeader("Runtime/Camera/OcclusionArea.h")]
	public sealed class OcclusionArea : Component
	{
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x00022000 File Offset: 0x00020200
		// (set) Token: 0x060013A6 RID: 5030 RVA: 0x00022028 File Offset: 0x00020228
		public Vector3 center
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OcclusionArea>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				OcclusionArea.get_center_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OcclusionArea>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				OcclusionArea.set_center_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x0002204C File Offset: 0x0002024C
		// (set) Token: 0x060013A8 RID: 5032 RVA: 0x00022074 File Offset: 0x00020274
		public Vector3 size
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OcclusionArea>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				OcclusionArea.get_size_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OcclusionArea>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				OcclusionArea.set_size_Injected(intPtr, ref value);
			}
		}

		// Token: 0x060013AA RID: 5034
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_center_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060013AB RID: 5035
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_center_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060013AC RID: 5036
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_size_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060013AD RID: 5037
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_size_Injected(IntPtr _unity_self, [In] ref Vector3 value);
	}
}
