using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000029 RID: 41
	[NativeHeader("Modules/Physics2D/Public/CapsuleCollider2D.h")]
	public sealed class CapsuleCollider2D : Collider2D
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000B940 File Offset: 0x00009B40
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x0000B968 File Offset: 0x00009B68
		public Vector2 size
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CapsuleCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				CapsuleCollider2D.get_size_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CapsuleCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CapsuleCollider2D.set_size_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000B98C File Offset: 0x00009B8C
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		public CapsuleDirection2D direction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CapsuleCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CapsuleCollider2D.get_direction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CapsuleCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CapsuleCollider2D.set_direction_Injected(intPtr, value);
			}
		}

		// Token: 0x060004A6 RID: 1190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_size_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060004A7 RID: 1191
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_size_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060004A8 RID: 1192
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CapsuleDirection2D get_direction_Injected(IntPtr _unity_self);

		// Token: 0x060004A9 RID: 1193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_direction_Injected(IntPtr _unity_self, CapsuleDirection2D value);
	}
}
