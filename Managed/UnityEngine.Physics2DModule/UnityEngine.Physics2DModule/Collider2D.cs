using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	[RequiredByNativeCode(Optional = true)]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Physics2D/Public/Collider2D.h")]
	public class Collider2D : Behaviour
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000A030 File Offset: 0x00008230
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000A054 File Offset: 0x00008254
		public float density
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider2D.get_density_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_density_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000A078 File Offset: 0x00008278
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000A09C File Offset: 0x0000829C
		public bool isTrigger
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider2D.get_isTrigger_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_isTrigger_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000A0C0 File Offset: 0x000082C0
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0000A0E4 File Offset: 0x000082E4
		public bool usedByEffector
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider2D.get_usedByEffector_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_usedByEffector_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000A108 File Offset: 0x00008308
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x0000A12C File Offset: 0x0000832C
		public Collider2D.CompositeOperation compositeOperation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider2D.get_compositeOperation_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_compositeOperation_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000A150 File Offset: 0x00008350
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0000A174 File Offset: 0x00008374
		public int compositeOrder
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider2D.get_compositeOrder_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_compositeOrder_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000A198 File Offset: 0x00008398
		public CompositeCollider2D composite
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<CompositeCollider2D>(Collider2D.get_composite_Injected(intPtr));
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000A1C0 File Offset: 0x000083C0
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000A1E8 File Offset: 0x000083E8
		public Vector2 offset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Collider2D.get_offset_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_offset_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000A20C File Offset: 0x0000840C
		public Rigidbody2D attachedRigidbody
		{
			[NativeMethod("GetAttachedRigidbody_Binding")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Rigidbody2D>(Collider2D.get_attachedRigidbody_Injected(intPtr));
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000A234 File Offset: 0x00008434
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Collider2D.get_localToWorldMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000A25C File Offset: 0x0000845C
		public int shapeCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider2D.get_shapeCount_Injected(intPtr);
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000A280 File Offset: 0x00008480
		[ExcludeFromDocs]
		public Mesh CreateMesh(bool useBodyPosition, bool useBodyRotation)
		{
			return this.CreateMesh(useBodyPosition, useBodyRotation, true);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000A29C File Offset: 0x0000849C
		[NativeMethod("CreateMesh_Binding")]
		public Mesh CreateMesh(bool useBodyPosition, bool useBodyRotation, [DefaultValue("true")] bool useDelaunay = true)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Mesh>(Collider2D.CreateMesh_Injected(intPtr, useBodyPosition, useBodyRotation, useDelaunay));
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000A2C8 File Offset: 0x000084C8
		[NativeMethod("GetShapeHash_Binding")]
		public uint GetShapeHash()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Collider2D.GetShapeHash_Injected(intPtr);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000A2EC File Offset: 0x000084EC
		public int GetShapes(PhysicsShapeGroup2D physicsShapeGroup)
		{
			return this.GetShapes_Internal(ref physicsShapeGroup.m_GroupState, 0, this.shapeCount);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000A314 File Offset: 0x00008514
		public int GetShapes(PhysicsShapeGroup2D physicsShapeGroup, int shapeIndex, [DefaultValue("1")] int shapeCount = 1)
		{
			int shapeCount2 = this.shapeCount;
			bool flag = shapeIndex < 0 || shapeIndex >= shapeCount2 || shapeCount < 1 || shapeIndex + shapeCount > shapeCount2;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot get shape range from {0} to {1} as Collider2D only has {2} shape(s).", shapeIndex, shapeIndex + shapeCount - 1, shapeCount2));
			}
			return this.GetShapes_Internal(ref physicsShapeGroup.m_GroupState, shapeIndex, shapeCount);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000A37C File Offset: 0x0000857C
		[NativeMethod("GetShapes_Binding")]
		private int GetShapes_Internal(ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState, int shapeIndex, int shapeCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Collider2D.GetShapes_Internal_Injected(intPtr, ref physicsShapeGroupState, shapeIndex, shapeCount);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000A3A4 File Offset: 0x000085A4
		[NativeMethod("GetShapeBounds_Binding")]
		public unsafe Bounds GetShapeBounds(List<Bounds> bounds, bool useRadii, bool useWorldSpace)
		{
			Bounds bounds3;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (bounds != null)
				{
					fixed (Bounds[] array = NoAllocHelpers.ExtractArrayFromList<Bounds>(bounds))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, bounds.Count);
					}
				}
				Bounds bounds2;
				Collider2D.GetShapeBounds_Injected(intPtr, ref blittableListWrapper, useRadii, useWorldSpace, out bounds2);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Bounds>(bounds);
				Bounds bounds2;
				bounds3 = bounds2;
			}
			return bounds3;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000A420 File Offset: 0x00008620
		public Bounds bounds
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				Collider2D.get_bounds_Injected(intPtr, out bounds);
				return bounds;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000A448 File Offset: 0x00008648
		public ColliderErrorState2D errorState
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider2D.get_errorState_Injected(intPtr);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000A46C File Offset: 0x0000866C
		public bool compositeCapable
		{
			[NativeMethod("GetCompositeCapable_Binding")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider2D.get_compositeCapable_Injected(intPtr);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000A490 File Offset: 0x00008690
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0000A4B8 File Offset: 0x000086B8
		public PhysicsMaterial2D sharedMaterial
		{
			[NativeMethod("GetMaterial")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<PhysicsMaterial2D>(Collider2D.get_sharedMaterial_Injected(intPtr));
			}
			[NativeMethod("SetMaterial")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_sharedMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<PhysicsMaterial2D>(value));
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000A4E0 File Offset: 0x000086E0
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x0000A504 File Offset: 0x00008704
		public int layerOverridePriority
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider2D.get_layerOverridePriority_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_layerOverridePriority_Injected(intPtr, value);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000A528 File Offset: 0x00008728
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x0000A550 File Offset: 0x00008750
		public LayerMask excludeLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				Collider2D.get_excludeLayers_Injected(intPtr, out layerMask);
				return layerMask;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_excludeLayers_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000A574 File Offset: 0x00008774
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x0000A59C File Offset: 0x0000879C
		public LayerMask includeLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				Collider2D.get_includeLayers_Injected(intPtr, out layerMask);
				return layerMask;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_includeLayers_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000A5C0 File Offset: 0x000087C0
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0000A5E8 File Offset: 0x000087E8
		public LayerMask forceSendLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				Collider2D.get_forceSendLayers_Injected(intPtr, out layerMask);
				return layerMask;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_forceSendLayers_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000A60C File Offset: 0x0000880C
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x0000A634 File Offset: 0x00008834
		public LayerMask forceReceiveLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				Collider2D.get_forceReceiveLayers_Injected(intPtr, out layerMask);
				return layerMask;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_forceReceiveLayers_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000A658 File Offset: 0x00008858
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0000A680 File Offset: 0x00008880
		public LayerMask contactCaptureLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				Collider2D.get_contactCaptureLayers_Injected(intPtr, out layerMask);
				return layerMask;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_contactCaptureLayers_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000A6A4 File Offset: 0x000088A4
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x0000A6CC File Offset: 0x000088CC
		public LayerMask callbackLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				Collider2D.get_callbackLayers_Injected(intPtr, out layerMask);
				return layerMask;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider2D.set_callbackLayers_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000A6F0 File Offset: 0x000088F0
		public float friction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider2D.get_friction_Injected(intPtr);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000A714 File Offset: 0x00008914
		public float bounciness
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider2D.get_bounciness_Injected(intPtr);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000A738 File Offset: 0x00008938
		public PhysicsMaterialCombine2D frictionCombine
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider2D.get_frictionCombine_Injected(intPtr);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000A75C File Offset: 0x0000895C
		public PhysicsMaterialCombine2D bounceCombine
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider2D.get_bounceCombine_Injected(intPtr);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000A780 File Offset: 0x00008980
		internal LayerMask contactMask
		{
			[NativeMethod("GetContactMask_Binding")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				Collider2D.get_contactMask_Injected(intPtr, out layerMask);
				return layerMask;
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000A7A8 File Offset: 0x000089A8
		public bool IsTouching([NotNull] Collider2D collider)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			return Collider2D.IsTouching_Injected(intPtr, intPtr2);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000A7F0 File Offset: 0x000089F0
		public bool IsTouching(Collider2D collider, ContactFilter2D contactFilter)
		{
			return this.IsTouching_OtherColliderWithFilter(collider, contactFilter);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000A80C File Offset: 0x00008A0C
		[NativeMethod("IsTouching")]
		private bool IsTouching_OtherColliderWithFilter([NotNull] Collider2D collider, ContactFilter2D contactFilter)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			return Collider2D.IsTouching_OtherColliderWithFilter_Injected(intPtr, intPtr2, ref contactFilter);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000A854 File Offset: 0x00008A54
		public bool IsTouching(ContactFilter2D contactFilter)
		{
			return this.IsTouching_AnyColliderWithFilter(contactFilter);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000A870 File Offset: 0x00008A70
		[NativeMethod("IsTouching")]
		private bool IsTouching_AnyColliderWithFilter(ContactFilter2D contactFilter)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Collider2D.IsTouching_AnyColliderWithFilter_Injected(intPtr, ref contactFilter);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000A894 File Offset: 0x00008A94
		[ExcludeFromDocs]
		public bool IsTouchingLayers()
		{
			return this.IsTouchingLayers(-1);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		public bool IsTouchingLayers([DefaultValue("Physics2D.AllLayers")] int layerMask)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Collider2D.IsTouchingLayers_Injected(intPtr, layerMask);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000A8D4 File Offset: 0x00008AD4
		public bool OverlapPoint(Vector2 point)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Collider2D.OverlapPoint_Injected(intPtr, ref point);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000A8F8 File Offset: 0x00008AF8
		public int Overlap(ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapCollider(this, contactFilter, results);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000A914 File Offset: 0x00008B14
		public int Overlap(List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapCollider(this, results);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000A930 File Offset: 0x00008B30
		public int Overlap(ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapCollider(this, contactFilter, results);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000A94C File Offset: 0x00008B4C
		public int Overlap(Vector2 position, float angle, List<Collider2D> results)
		{
			bool flag = this.attachedRigidbody;
			if (flag)
			{
				return PhysicsScene2D.OverlapCollider(position, angle, this, results);
			}
			throw new InvalidOperationException("Cannot perform a Collider Overlap at a specific position and angle if the Collider is not attached to a Rigidbody2D.");
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000A984 File Offset: 0x00008B84
		public int Overlap(Vector2 position, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			bool flag = this.attachedRigidbody;
			if (flag)
			{
				return PhysicsScene2D.OverlapCollider(position, angle, this, contactFilter, results);
			}
			throw new InvalidOperationException("Cannot perform a Collider Overlap at a specific position and angle if the Collider is not attached to a Rigidbody2D.");
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000A9BC File Offset: 0x00008BBC
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, RaycastHit2D[] results)
		{
			ContactFilter2D contactFilter2D = default(ContactFilter2D);
			contactFilter2D.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter2D.SetLayerMask(this.contactMask);
			return this.CastArray_Internal(direction, float.PositiveInfinity, contactFilter2D, true, false, results);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000AA00 File Offset: 0x00008C00
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, RaycastHit2D[] results, float distance)
		{
			ContactFilter2D contactFilter2D = default(ContactFilter2D);
			contactFilter2D.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter2D.SetLayerMask(this.contactMask);
			return this.CastArray_Internal(direction, distance, contactFilter2D, true, false, results);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000AA40 File Offset: 0x00008C40
		public int Cast(Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("true")] bool ignoreSiblingColliders)
		{
			ContactFilter2D contactFilter2D = default(ContactFilter2D);
			contactFilter2D.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter2D.SetLayerMask(this.contactMask);
			return this.CastArray_Internal(direction, distance, contactFilter2D, ignoreSiblingColliders, false, results);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000AA84 File Offset: 0x00008C84
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return this.CastArray_Internal(direction, float.PositiveInfinity, contactFilter, true, false, results);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000AAA8 File Offset: 0x00008CA8
		[ExcludeFromDocs]
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance)
		{
			return this.CastArray_Internal(direction, distance, contactFilter, true, false, results);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("true")] bool ignoreSiblingColliders)
		{
			return this.CastArray_Internal(direction, distance, contactFilter, ignoreSiblingColliders, false, results);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public int Cast(Vector2 direction, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity, [DefaultValue("true")] bool ignoreSiblingColliders = true)
		{
			return this.CastList_Internal(direction, distance, ignoreSiblingColliders, false, results);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000AB08 File Offset: 0x00008D08
		public int Cast(Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity, [DefaultValue("true")] bool ignoreSiblingColliders = true)
		{
			return this.CastListFiltered_Internal(direction, distance, contactFilter, ignoreSiblingColliders, false, results);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000AB28 File Offset: 0x00008D28
		public int Cast(Vector2 position, float angle, Vector2 direction, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity, [DefaultValue("true")] bool ignoreSiblingColliders = true)
		{
			bool flag = this.attachedRigidbody;
			if (flag)
			{
				return this.CastFrom_Internal(position, angle, direction, distance, ignoreSiblingColliders, false, results);
			}
			throw new InvalidOperationException("Cannot perform a Collider Cast from a specific position and angle if the Collider is not attached to a Rigidbody2D.");
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000AB68 File Offset: 0x00008D68
		public int Cast(Vector2 position, float angle, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity, [DefaultValue("true")] bool ignoreSiblingColliders = true)
		{
			bool flag = this.attachedRigidbody;
			if (flag)
			{
				return this.CastFromFiltered_Internal(position, angle, direction, distance, contactFilter, ignoreSiblingColliders, false, results);
			}
			throw new InvalidOperationException("Cannot perform a Collider Cast from a specific position and angle if the Collider is not attached to a Rigidbody2D.");
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000ABA8 File Offset: 0x00008DA8
		[NativeMethod("CastArray_Binding")]
		private unsafe int CastArray_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, bool ignoreSiblingColliders, bool checkIgnoreColliders, [NotNull] RaycastHit2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<RaycastHit2D> span = new Span<RaycastHit2D>(results);
			int num;
			fixed (RaycastHit2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = Collider2D.CastArray_Internal_Injected(intPtr, ref direction, distance, ref contactFilter, ignoreSiblingColliders, checkIgnoreColliders, ref managedSpanWrapper);
			}
			return num;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000AC0C File Offset: 0x00008E0C
		[NativeMethod("CastList_Binding")]
		private unsafe int CastList_Internal(Vector2 direction, float distance, bool ignoreSiblingColliders, bool checkIgnoreColliders, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = Collider2D.CastList_Internal_Injected(intPtr, ref direction, distance, ignoreSiblingColliders, checkIgnoreColliders, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000AC98 File Offset: 0x00008E98
		[NativeMethod("CastListFiltered_Binding")]
		private unsafe int CastListFiltered_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, bool ignoreSiblingColliders, bool checkIgnoreColliders, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = Collider2D.CastListFiltered_Internal_Injected(intPtr, ref direction, distance, ref contactFilter, ignoreSiblingColliders, checkIgnoreColliders, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000AD24 File Offset: 0x00008F24
		[NativeMethod("CastFrom_Binding")]
		private unsafe int CastFrom_Internal(Vector2 position, float angle, Vector2 direction, float distance, bool ignoreSiblingColliders, bool checkIgnoreColliders, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = Collider2D.CastFrom_Internal_Injected(intPtr, ref position, angle, ref direction, distance, ignoreSiblingColliders, checkIgnoreColliders, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		[NativeMethod("CastFromFiltered_Binding")]
		private unsafe int CastFromFiltered_Internal(Vector2 position, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, bool ignoreSiblingColliders, bool checkIgnoreColliders, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = Collider2D.CastFromFiltered_Internal_Injected(intPtr, ref position, angle, ref direction, distance, ref contactFilter, ignoreSiblingColliders, checkIgnoreColliders, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000AE44 File Offset: 0x00009044
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-1, float.NegativeInfinity, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, float.PositiveInfinity, contactFilter2D, results);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000AE78 File Offset: 0x00009078
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-1, float.NegativeInfinity, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, distance, contactFilter2D, results);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000AEA8 File Offset: 0x000090A8
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, distance, contactFilter2D, results);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000AED8 File Offset: 0x000090D8
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return this.RaycastArray_Internal(direction, distance, contactFilter2D, results);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000AF04 File Offset: 0x00009104
		public int Raycast(Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("Physics2D.AllLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return this.RaycastArray_Internal(direction, distance, contactFilter2D, results);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000AF2C File Offset: 0x0000912C
		[ExcludeFromDocs]
		public int Raycast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return this.RaycastArray_Internal(direction, float.PositiveInfinity, contactFilter, results);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000AF4C File Offset: 0x0000914C
		public int Raycast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return this.RaycastArray_Internal(direction, distance, contactFilter, results);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000AF6C File Offset: 0x0000916C
		[NativeMethod("RaycastArray_Binding")]
		private unsafe int RaycastArray_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] RaycastHit2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<RaycastHit2D> span = new Span<RaycastHit2D>(results);
			int num;
			fixed (RaycastHit2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = Collider2D.RaycastArray_Internal_Injected(intPtr, ref direction, distance, ref contactFilter, ref managedSpanWrapper);
			}
			return num;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000AFCC File Offset: 0x000091CC
		public int Raycast(Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return this.RaycastList_Internal(direction, distance, contactFilter, results);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000AFEC File Offset: 0x000091EC
		[NativeMethod("RaycastList_Binding")]
		private unsafe int RaycastList_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = Collider2D.RaycastList_Internal_Injected(intPtr, ref direction, distance, ref contactFilter, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000B074 File Offset: 0x00009274
		public ColliderDistance2D Distance(Collider2D collider)
		{
			return Physics2D.Distance(this, collider);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000B090 File Offset: 0x00009290
		public ColliderDistance2D Distance(Vector2 thisPosition, float thisAngle, Collider2D collider, Vector2 position, float angle)
		{
			return Physics2D.Distance(this, thisPosition, thisAngle, collider, position, angle);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000B0B0 File Offset: 0x000092B0
		public Vector2 ClosestPoint(Vector2 position)
		{
			return Physics2D.ClosestPoint(position, this);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000B0CC File Offset: 0x000092CC
		public int GetContacts(ContactPoint2D[] contacts)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000B0F4 File Offset: 0x000092F4
		public int GetContacts(List<ContactPoint2D> contacts)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000B11C File Offset: 0x0000931C
		public int GetContacts(ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			return Physics2D.GetContacts(this, contactFilter, contacts);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000B138 File Offset: 0x00009338
		public int GetContacts(ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetContacts(this, contactFilter, contacts);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000B154 File Offset: 0x00009354
		public int GetContacts(Collider2D[] colliders)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000B17C File Offset: 0x0000937C
		public int GetContacts(List<Collider2D> colliders)
		{
			return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000B1A4 File Offset: 0x000093A4
		public int GetContacts(ContactFilter2D contactFilter, Collider2D[] colliders)
		{
			return Physics2D.GetContacts(this, contactFilter, colliders);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000B1C0 File Offset: 0x000093C0
		public int GetContacts(ContactFilter2D contactFilter, List<Collider2D> colliders)
		{
			return Physics2D.GetContacts(this, contactFilter, colliders);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000B1DC File Offset: 0x000093DC
		[Obsolete("OverlapCollider has been renamed to Overlap (UnityUpgradable) -> Overlap(*)", false)]
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int OverlapCollider(ContactFilter2D contactFilter, Collider2D[] results)
		{
			return this.Overlap(contactFilter, results);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000B1F8 File Offset: 0x000093F8
		[Obsolete("OverlapCollider has been renamed to Overlap (UnityUpgradable) -> Overlap(*)", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public int OverlapCollider(ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return this.Overlap(contactFilter, results);
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000B214 File Offset: 0x00009414
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0000B22F File Offset: 0x0000942F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("usedByComposite has been deprecated. Use Collider2D.compositeOperation instead", false)]
		[ExcludeFromDocs]
		public bool usedByComposite
		{
			get
			{
				return this.compositeOperation > Collider2D.CompositeOperation.None;
			}
			set
			{
				this.compositeOperation = (value ? Collider2D.CompositeOperation.Merge : Collider2D.CompositeOperation.None);
			}
		}

		// Token: 0x06000445 RID: 1093
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_density_Injected(IntPtr _unity_self);

		// Token: 0x06000446 RID: 1094
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_density_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000447 RID: 1095
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isTrigger_Injected(IntPtr _unity_self);

		// Token: 0x06000448 RID: 1096
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_isTrigger_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000449 RID: 1097
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_usedByEffector_Injected(IntPtr _unity_self);

		// Token: 0x0600044A RID: 1098
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_usedByEffector_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600044B RID: 1099
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D.CompositeOperation get_compositeOperation_Injected(IntPtr _unity_self);

		// Token: 0x0600044C RID: 1100
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_compositeOperation_Injected(IntPtr _unity_self, Collider2D.CompositeOperation value);

		// Token: 0x0600044D RID: 1101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_compositeOrder_Injected(IntPtr _unity_self);

		// Token: 0x0600044E RID: 1102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_compositeOrder_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600044F RID: 1103
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_composite_Injected(IntPtr _unity_self);

		// Token: 0x06000450 RID: 1104
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_offset_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000451 RID: 1105
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_offset_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000452 RID: 1106
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_attachedRigidbody_Injected(IntPtr _unity_self);

		// Token: 0x06000453 RID: 1107
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localToWorldMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x06000454 RID: 1108
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_shapeCount_Injected(IntPtr _unity_self);

		// Token: 0x06000455 RID: 1109
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateMesh_Injected(IntPtr _unity_self, bool useBodyPosition, bool useBodyRotation, [DefaultValue("true")] bool useDelaunay);

		// Token: 0x06000456 RID: 1110
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetShapeHash_Injected(IntPtr _unity_self);

		// Token: 0x06000457 RID: 1111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetShapes_Internal_Injected(IntPtr _unity_self, ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState, int shapeIndex, int shapeCount);

		// Token: 0x06000458 RID: 1112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetShapeBounds_Injected(IntPtr _unity_self, ref BlittableListWrapper bounds, bool useRadii, bool useWorldSpace, out Bounds ret);

		// Token: 0x06000459 RID: 1113
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x0600045A RID: 1114
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ColliderErrorState2D get_errorState_Injected(IntPtr _unity_self);

		// Token: 0x0600045B RID: 1115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_compositeCapable_Injected(IntPtr _unity_self);

		// Token: 0x0600045C RID: 1116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_sharedMaterial_Injected(IntPtr _unity_self);

		// Token: 0x0600045D RID: 1117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sharedMaterial_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600045E RID: 1118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_layerOverridePriority_Injected(IntPtr _unity_self);

		// Token: 0x0600045F RID: 1119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_layerOverridePriority_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000460 RID: 1120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_excludeLayers_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x06000461 RID: 1121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_excludeLayers_Injected(IntPtr _unity_self, [In] ref LayerMask value);

		// Token: 0x06000462 RID: 1122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_includeLayers_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x06000463 RID: 1123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_includeLayers_Injected(IntPtr _unity_self, [In] ref LayerMask value);

		// Token: 0x06000464 RID: 1124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_forceSendLayers_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x06000465 RID: 1125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceSendLayers_Injected(IntPtr _unity_self, [In] ref LayerMask value);

		// Token: 0x06000466 RID: 1126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_forceReceiveLayers_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x06000467 RID: 1127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_forceReceiveLayers_Injected(IntPtr _unity_self, [In] ref LayerMask value);

		// Token: 0x06000468 RID: 1128
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_contactCaptureLayers_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x06000469 RID: 1129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_contactCaptureLayers_Injected(IntPtr _unity_self, [In] ref LayerMask value);

		// Token: 0x0600046A RID: 1130
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_callbackLayers_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x0600046B RID: 1131
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_callbackLayers_Injected(IntPtr _unity_self, [In] ref LayerMask value);

		// Token: 0x0600046C RID: 1132
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_friction_Injected(IntPtr _unity_self);

		// Token: 0x0600046D RID: 1133
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_bounciness_Injected(IntPtr _unity_self);

		// Token: 0x0600046E RID: 1134
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PhysicsMaterialCombine2D get_frictionCombine_Injected(IntPtr _unity_self);

		// Token: 0x0600046F RID: 1135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PhysicsMaterialCombine2D get_bounceCombine_Injected(IntPtr _unity_self);

		// Token: 0x06000470 RID: 1136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_contactMask_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x06000471 RID: 1137
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTouching_Injected(IntPtr _unity_self, IntPtr collider);

		// Token: 0x06000472 RID: 1138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTouching_OtherColliderWithFilter_Injected(IntPtr _unity_self, IntPtr collider, [In] ref ContactFilter2D contactFilter);

		// Token: 0x06000473 RID: 1139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTouching_AnyColliderWithFilter_Injected(IntPtr _unity_self, [In] ref ContactFilter2D contactFilter);

		// Token: 0x06000474 RID: 1140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTouchingLayers_Injected(IntPtr _unity_self, [DefaultValue("Physics2D.AllLayers")] int layerMask);

		// Token: 0x06000475 RID: 1141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool OverlapPoint_Injected(IntPtr _unity_self, [In] ref Vector2 point);

		// Token: 0x06000476 RID: 1142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CastArray_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, bool ignoreSiblingColliders, bool checkIgnoreColliders, ref ManagedSpanWrapper results);

		// Token: 0x06000477 RID: 1143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CastList_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 direction, float distance, bool ignoreSiblingColliders, bool checkIgnoreColliders, ref BlittableListWrapper results);

		// Token: 0x06000478 RID: 1144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CastListFiltered_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, bool ignoreSiblingColliders, bool checkIgnoreColliders, ref BlittableListWrapper results);

		// Token: 0x06000479 RID: 1145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CastFrom_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 position, float angle, [In] ref Vector2 direction, float distance, bool ignoreSiblingColliders, bool checkIgnoreColliders, ref BlittableListWrapper results);

		// Token: 0x0600047A RID: 1146
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CastFromFiltered_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 position, float angle, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, bool ignoreSiblingColliders, bool checkIgnoreColliders, ref BlittableListWrapper results);

		// Token: 0x0600047B RID: 1147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RaycastArray_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, ref ManagedSpanWrapper results);

		// Token: 0x0600047C RID: 1148
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RaycastList_Internal_Injected(IntPtr _unity_self, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, ref BlittableListWrapper results);

		// Token: 0x02000026 RID: 38
		public enum CompositeOperation
		{
			// Token: 0x040000BB RID: 187
			None,
			// Token: 0x040000BC RID: 188
			Merge,
			// Token: 0x040000BD RID: 189
			Intersect,
			// Token: 0x040000BE RID: 190
			Difference,
			// Token: 0x040000BF RID: 191
			Flip
		}
	}
}
