using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.LowLevelPhysics;

namespace UnityEngine
{
	// Token: 0x0200001D RID: 29
	[NativeHeader("Modules/Physics/Collider.h")]
	public class Collider : Component
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00004F5C File Offset: 0x0000315C
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00004F80 File Offset: 0x00003180
		public bool enabled
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider.get_enabled_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider.set_enabled_Injected(intPtr, value);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00004FA4 File Offset: 0x000031A4
		public Rigidbody attachedRigidbody
		{
			[NativeMethod("GetRigidbody")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Rigidbody>(Collider.get_attachedRigidbody_Injected(intPtr));
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00004FCC File Offset: 0x000031CC
		public ArticulationBody attachedArticulationBody
		{
			[NativeMethod("GetArticulationBody")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<ArticulationBody>(Collider.get_attachedArticulationBody_Injected(intPtr));
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00004FF4 File Offset: 0x000031F4
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00005018 File Offset: 0x00003218
		public bool isTrigger
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider.get_isTrigger_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider.set_isTrigger_Injected(intPtr, value);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000503C File Offset: 0x0000323C
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00005060 File Offset: 0x00003260
		public float contactOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider.get_contactOffset_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider.set_contactOffset_Injected(intPtr, value);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00005084 File Offset: 0x00003284
		public Vector3 ClosestPoint(Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Collider.ClosestPoint_Injected(intPtr, ref position, out vector);
			return vector;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000050AC File Offset: 0x000032AC
		public Bounds bounds
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				Collider.get_bounds_Injected(intPtr, out bounds);
				return bounds;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000050D4 File Offset: 0x000032D4
		// (set) Token: 0x060001FD RID: 509 RVA: 0x000050F8 File Offset: 0x000032F8
		public bool hasModifiableContacts
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider.get_hasModifiableContacts_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider.set_hasModifiableContacts_Injected(intPtr, value);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000511C File Offset: 0x0000331C
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00005140 File Offset: 0x00003340
		public bool providesContacts
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider.get_providesContacts_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider.set_providesContacts_Injected(intPtr, value);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00005164 File Offset: 0x00003364
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00005188 File Offset: 0x00003388
		public int layerOverridePriority
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider.get_layerOverridePriority_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider.set_layerOverridePriority_Injected(intPtr, value);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000051AC File Offset: 0x000033AC
		// (set) Token: 0x06000203 RID: 515 RVA: 0x000051D4 File Offset: 0x000033D4
		public LayerMask excludeLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				Collider.get_excludeLayers_Injected(intPtr, out layerMask);
				return layerMask;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider.set_excludeLayers_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000051F8 File Offset: 0x000033F8
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00005220 File Offset: 0x00003420
		public LayerMask includeLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				Collider.get_includeLayers_Injected(intPtr, out layerMask);
				return layerMask;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider.set_includeLayers_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00005244 File Offset: 0x00003444
		public GeometryHolder GeometryHolder
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GeometryHolder geometryHolder;
				Collider.get_GeometryHolder_Injected(intPtr, out geometryHolder);
				return geometryHolder;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000526C File Offset: 0x0000346C
		public T GetGeometry<T>() where T : struct, IGeometry
		{
			return this.GeometryHolder.As<T>();
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000528C File Offset: 0x0000348C
		// (set) Token: 0x06000209 RID: 521 RVA: 0x000052B4 File Offset: 0x000034B4
		[NativeMethod("Material")]
		public PhysicsMaterial sharedMaterial
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<PhysicsMaterial>(Collider.get_sharedMaterial_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider.set_sharedMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<PhysicsMaterial>(value));
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000052DC File Offset: 0x000034DC
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00005304 File Offset: 0x00003504
		public PhysicsMaterial material
		{
			[NativeMethod("GetClonedMaterial")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<PhysicsMaterial>(Collider.get_material_Injected(intPtr));
			}
			[NativeMethod("SetMaterial")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider.set_material_Injected(intPtr, Object.MarshalledUnityObject.Marshal<PhysicsMaterial>(value));
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000532C File Offset: 0x0000352C
		private RaycastHit Raycast(Ray ray, float maxDistance, ref bool hasHit)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RaycastHit raycastHit;
			Collider.Raycast_Injected(intPtr, ref ray, maxDistance, ref hasHit, out raycastHit);
			return raycastHit;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00005358 File Offset: 0x00003558
		public bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
		{
			bool flag = false;
			hitInfo = this.Raycast(ray, maxDistance, ref flag);
			return flag;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00005380 File Offset: 0x00003580
		[NativeName("ClosestPointOnBounds")]
		private void Internal_ClosestPointOnBounds(Vector3 point, ref Vector3 outPos, ref float distance)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Collider.Internal_ClosestPointOnBounds_Injected(intPtr, ref point, ref outPos, ref distance);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000053A8 File Offset: 0x000035A8
		public Vector3 ClosestPointOnBounds(Vector3 position)
		{
			float num = 0f;
			Vector3 zero = Vector3.zero;
			this.Internal_ClosestPointOnBounds(position, ref zero, ref num);
			return zero;
		}

		// Token: 0x06000211 RID: 529
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enabled_Injected(IntPtr _unity_self);

		// Token: 0x06000212 RID: 530
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enabled_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000213 RID: 531
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_attachedRigidbody_Injected(IntPtr _unity_self);

		// Token: 0x06000214 RID: 532
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_attachedArticulationBody_Injected(IntPtr _unity_self);

		// Token: 0x06000215 RID: 533
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isTrigger_Injected(IntPtr _unity_self);

		// Token: 0x06000216 RID: 534
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_isTrigger_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000217 RID: 535
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_contactOffset_Injected(IntPtr _unity_self);

		// Token: 0x06000218 RID: 536
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_contactOffset_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000219 RID: 537
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClosestPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, out Vector3 ret);

		// Token: 0x0600021A RID: 538
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x0600021B RID: 539
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasModifiableContacts_Injected(IntPtr _unity_self);

		// Token: 0x0600021C RID: 540
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_hasModifiableContacts_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600021D RID: 541
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_providesContacts_Injected(IntPtr _unity_self);

		// Token: 0x0600021E RID: 542
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_providesContacts_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600021F RID: 543
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_layerOverridePriority_Injected(IntPtr _unity_self);

		// Token: 0x06000220 RID: 544
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_layerOverridePriority_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000221 RID: 545
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_excludeLayers_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x06000222 RID: 546
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_excludeLayers_Injected(IntPtr _unity_self, [In] ref LayerMask value);

		// Token: 0x06000223 RID: 547
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_includeLayers_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x06000224 RID: 548
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_includeLayers_Injected(IntPtr _unity_self, [In] ref LayerMask value);

		// Token: 0x06000225 RID: 549
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_GeometryHolder_Injected(IntPtr _unity_self, out GeometryHolder ret);

		// Token: 0x06000226 RID: 550
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_sharedMaterial_Injected(IntPtr _unity_self);

		// Token: 0x06000227 RID: 551
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sharedMaterial_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000228 RID: 552
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_material_Injected(IntPtr _unity_self);

		// Token: 0x06000229 RID: 553
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_material_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600022A RID: 554
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Raycast_Injected(IntPtr _unity_self, [In] ref Ray ray, float maxDistance, ref bool hasHit, out RaycastHit ret);

		// Token: 0x0600022B RID: 555
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ClosestPointOnBounds_Injected(IntPtr _unity_self, [In] ref Vector3 point, ref Vector3 outPos, ref float distance);
	}
}
