using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000043 RID: 67
	[NativeHeader("Modules/Physics2D/Public/PhysicsMaterial2D.h")]
	public sealed class PhysicsMaterial2D : Object
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x0000E478 File Offset: 0x0000C678
		public PhysicsMaterial2D()
		{
			PhysicsMaterial2D.Create_Internal(this, null);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0000E48A File Offset: 0x0000C68A
		public PhysicsMaterial2D(string name)
		{
			PhysicsMaterial2D.Create_Internal(this, name);
		}

		// Token: 0x060006D1 RID: 1745
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetCombinedValues(float valueA, float valueB, PhysicsMaterialCombine2D materialCombineA, PhysicsMaterialCombine2D materialCombineB);

		// Token: 0x060006D2 RID: 1746 RVA: 0x0000E49C File Offset: 0x0000C69C
		[NativeMethod("Create_Binding")]
		private unsafe static void Create_Internal([Writable] PhysicsMaterial2D scriptMaterial, string name)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				PhysicsMaterial2D.Create_Internal_Injected(scriptMaterial, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x0000E518 File Offset: 0x0000C718
		public float bounciness
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PhysicsMaterial2D.get_bounciness_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PhysicsMaterial2D.set_bounciness_Injected(intPtr, value);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0000E53C File Offset: 0x0000C73C
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x0000E560 File Offset: 0x0000C760
		public float friction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PhysicsMaterial2D.get_friction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PhysicsMaterial2D.set_friction_Injected(intPtr, value);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0000E584 File Offset: 0x0000C784
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		public PhysicsMaterialCombine2D frictionCombine
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PhysicsMaterial2D.get_frictionCombine_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PhysicsMaterial2D.set_frictionCombine_Injected(intPtr, value);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x0000E5F0 File Offset: 0x0000C7F0
		public PhysicsMaterialCombine2D bounceCombine
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PhysicsMaterial2D.get_bounceCombine_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PhysicsMaterial2D.set_bounceCombine_Injected(intPtr, value);
			}
		}

		// Token: 0x060006DB RID: 1755
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Create_Internal_Injected([Writable] PhysicsMaterial2D scriptMaterial, ref ManagedSpanWrapper name);

		// Token: 0x060006DC RID: 1756
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_bounciness_Injected(IntPtr _unity_self);

		// Token: 0x060006DD RID: 1757
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bounciness_Injected(IntPtr _unity_self, float value);

		// Token: 0x060006DE RID: 1758
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_friction_Injected(IntPtr _unity_self);

		// Token: 0x060006DF RID: 1759
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_friction_Injected(IntPtr _unity_self, float value);

		// Token: 0x060006E0 RID: 1760
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PhysicsMaterialCombine2D get_frictionCombine_Injected(IntPtr _unity_self);

		// Token: 0x060006E1 RID: 1761
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_frictionCombine_Injected(IntPtr _unity_self, PhysicsMaterialCombine2D value);

		// Token: 0x060006E2 RID: 1762
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PhysicsMaterialCombine2D get_bounceCombine_Injected(IntPtr _unity_self);

		// Token: 0x060006E3 RID: 1763
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bounceCombine_Injected(IntPtr _unity_self, PhysicsMaterialCombine2D value);
	}
}
