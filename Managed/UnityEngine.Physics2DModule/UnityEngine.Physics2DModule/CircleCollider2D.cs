using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000028 RID: 40
	[NativeHeader("Modules/Physics2D/Public/CircleCollider2D.h")]
	public sealed class CircleCollider2D : Collider2D
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x0000B904 File Offset: 0x00009B04
		public float radius
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CircleCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CircleCollider2D.get_radius_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CircleCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CircleCollider2D.set_radius_Injected(intPtr, value);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000B928 File Offset: 0x00009B28
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x00005DAF File Offset: 0x00003FAF
		[Obsolete("CircleCollider2D.center has been deprecated. Use CircleCollider2D.offset instead (UnityUpgradable) -> offset", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x0600049F RID: 1183
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_radius_Injected(IntPtr _unity_self);

		// Token: 0x060004A0 RID: 1184
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_radius_Injected(IntPtr _unity_self, float value);
	}
}
