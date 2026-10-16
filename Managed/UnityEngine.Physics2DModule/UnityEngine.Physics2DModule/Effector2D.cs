using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003B RID: 59
	[NativeHeader("Modules/Physics2D/Effector2D.h")]
	public class Effector2D : Behaviour
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0000D800 File Offset: 0x0000BA00
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0000D824 File Offset: 0x0000BA24
		public bool useColliderMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Effector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Effector2D.get_useColliderMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Effector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Effector2D.set_useColliderMask_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0000D848 File Offset: 0x0000BA48
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x0000D86C File Offset: 0x0000BA6C
		public int colliderMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Effector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Effector2D.get_colliderMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Effector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Effector2D.set_colliderMask_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0000D890 File Offset: 0x0000BA90
		internal bool requiresCollider
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Effector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Effector2D.get_requiresCollider_Injected(intPtr);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0000D8B4 File Offset: 0x0000BAB4
		internal bool designedForTrigger
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Effector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Effector2D.get_designedForTrigger_Injected(intPtr);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		internal bool designedForNonTrigger
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Effector2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Effector2D.get_designedForNonTrigger_Injected(intPtr);
			}
		}

		// Token: 0x0600061F RID: 1567
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useColliderMask_Injected(IntPtr _unity_self);

		// Token: 0x06000620 RID: 1568
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useColliderMask_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000621 RID: 1569
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_colliderMask_Injected(IntPtr _unity_self);

		// Token: 0x06000622 RID: 1570
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_colliderMask_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000623 RID: 1571
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_requiresCollider_Injected(IntPtr _unity_self);

		// Token: 0x06000624 RID: 1572
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_designedForTrigger_Injected(IntPtr _unity_self);

		// Token: 0x06000625 RID: 1573
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_designedForNonTrigger_Injected(IntPtr _unity_self);
	}
}
