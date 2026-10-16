using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000040 RID: 64
	[NativeHeader("Modules/Physics/PhysicsMaterial.h")]
	public class PhysicsMaterial : Object
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x00008D95 File Offset: 0x00006F95
		public PhysicsMaterial()
		{
			PhysicsMaterial.Internal_CreateDynamicsMaterial(this, "DynamicMaterial");
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00008DAB File Offset: 0x00006FAB
		public PhysicsMaterial(string name)
		{
			PhysicsMaterial.Internal_CreateDynamicsMaterial(this, name);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00008DC0 File Offset: 0x00006FC0
		private unsafe static void Internal_CreateDynamicsMaterial([Writable] PhysicsMaterial mat, string name)
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
				PhysicsMaterial.Internal_CreateDynamicsMaterial_Injected(mat, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00008E18 File Offset: 0x00007018
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x00008E3C File Offset: 0x0000703C
		public float bounciness
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PhysicsMaterial.get_bounciness_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PhysicsMaterial.set_bounciness_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00008E60 File Offset: 0x00007060
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00008E84 File Offset: 0x00007084
		public float dynamicFriction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PhysicsMaterial.get_dynamicFriction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PhysicsMaterial.set_dynamicFriction_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00008EA8 File Offset: 0x000070A8
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x00008ECC File Offset: 0x000070CC
		public float staticFriction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PhysicsMaterial.get_staticFriction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PhysicsMaterial.set_staticFriction_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00008EF0 File Offset: 0x000070F0
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x00008F14 File Offset: 0x00007114
		public PhysicsMaterialCombine frictionCombine
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PhysicsMaterial.get_frictionCombine_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PhysicsMaterial.set_frictionCombine_Injected(intPtr, value);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00008F38 File Offset: 0x00007138
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x00008F5C File Offset: 0x0000715C
		public PhysicsMaterialCombine bounceCombine
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PhysicsMaterial.get_bounceCombine_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PhysicsMaterial>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PhysicsMaterial.set_bounceCombine_Injected(intPtr, value);
			}
		}

		// Token: 0x060004A0 RID: 1184
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateDynamicsMaterial_Injected([Writable] PhysicsMaterial mat, ref ManagedSpanWrapper name);

		// Token: 0x060004A1 RID: 1185
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_bounciness_Injected(IntPtr _unity_self);

		// Token: 0x060004A2 RID: 1186
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bounciness_Injected(IntPtr _unity_self, float value);

		// Token: 0x060004A3 RID: 1187
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_dynamicFriction_Injected(IntPtr _unity_self);

		// Token: 0x060004A4 RID: 1188
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_dynamicFriction_Injected(IntPtr _unity_self, float value);

		// Token: 0x060004A5 RID: 1189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_staticFriction_Injected(IntPtr _unity_self);

		// Token: 0x060004A6 RID: 1190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_staticFriction_Injected(IntPtr _unity_self, float value);

		// Token: 0x060004A7 RID: 1191
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PhysicsMaterialCombine get_frictionCombine_Injected(IntPtr _unity_self);

		// Token: 0x060004A8 RID: 1192
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_frictionCombine_Injected(IntPtr _unity_self, PhysicsMaterialCombine value);

		// Token: 0x060004A9 RID: 1193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PhysicsMaterialCombine get_bounceCombine_Injected(IntPtr _unity_self);

		// Token: 0x060004AA RID: 1194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bounceCombine_Injected(IntPtr _unity_self, PhysicsMaterialCombine value);
	}
}
