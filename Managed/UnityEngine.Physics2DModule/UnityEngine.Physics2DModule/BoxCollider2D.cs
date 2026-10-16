using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002B RID: 43
	[NativeHeader("Modules/Physics2D/Public/BoxCollider2D.h")]
	public sealed class BoxCollider2D : Collider2D
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000BD3C File Offset: 0x00009F3C
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0000BD64 File Offset: 0x00009F64
		public Vector2 size
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BoxCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				BoxCollider2D.get_size_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BoxCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BoxCollider2D.set_size_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000BD88 File Offset: 0x00009F88
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000BDAC File Offset: 0x00009FAC
		public float edgeRadius
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BoxCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BoxCollider2D.get_edgeRadius_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BoxCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BoxCollider2D.set_edgeRadius_Injected(intPtr, value);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000BDD0 File Offset: 0x00009FD0
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0000BDF4 File Offset: 0x00009FF4
		public bool autoTiling
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BoxCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return BoxCollider2D.get_autoTiling_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BoxCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BoxCollider2D.set_autoTiling_Injected(intPtr, value);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000BE18 File Offset: 0x0000A018
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00005DAF File Offset: 0x00003FAF
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("BoxCollider2D.center has been deprecated. Use BoxCollider2D.offset instead (UnityUpgradable) -> offset", true)]
		public Vector2 center
		{
			get
			{
				return Vector2.zero;
			}
			set
			{
			}
		}

		// Token: 0x060004D6 RID: 1238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_size_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060004D7 RID: 1239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_size_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060004D8 RID: 1240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_edgeRadius_Injected(IntPtr _unity_self);

		// Token: 0x060004D9 RID: 1241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_edgeRadius_Injected(IntPtr _unity_self, float value);

		// Token: 0x060004DA RID: 1242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoTiling_Injected(IntPtr _unity_self);

		// Token: 0x060004DB RID: 1243
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoTiling_Injected(IntPtr _unity_self, bool value);
	}
}
