using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	[NativeHeader("Modules/Physics/BoxCollider.h")]
	[RequireComponent(typeof(Transform))]
	public class BoxCollider : Collider
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000045E0 File Offset: 0x000027E0
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00004608 File Offset: 0x00002808
		public Vector3 center
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BoxCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				BoxCollider.get_center_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BoxCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BoxCollider.set_center_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000462C File Offset: 0x0000282C
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00004654 File Offset: 0x00002854
		public Vector3 size
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BoxCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				BoxCollider.get_size_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BoxCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BoxCollider.set_size_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00004678 File Offset: 0x00002878
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000469A File Offset: 0x0000289A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use BoxCollider.size instead. (UnityUpgradable) -> size")]
		public Vector3 extents
		{
			get
			{
				return this.size * 0.5f;
			}
			set
			{
				this.size = value * 2f;
			}
		}

		// Token: 0x06000177 RID: 375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_center_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000178 RID: 376
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_center_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000179 RID: 377
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_size_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600017A RID: 378
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_size_Injected(IntPtr _unity_self, [In] ref Vector3 value);
	}
}
