using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[NativeClass("Unity::Cloth")]
	[NativeHeader("Modules/Cloth/Cloth.h")]
	[RequireComponent(typeof(Transform), typeof(SkinnedMeshRenderer))]
	public sealed class Cloth : Component
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002098 File Offset: 0x00000298
		public Vector3[] vertices
		{
			[NativeName("GetPositions")]
			get
			{
				Vector3[] array2;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					Cloth.get_vertices_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					Vector3[] array;
					blittableArrayWrapper.Unmarshal<Vector3>(ref array);
					array2 = array;
				}
				return array2;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020DC File Offset: 0x000002DC
		public Vector3[] normals
		{
			[NativeName("GetNormals")]
			get
			{
				Vector3[] array2;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					Cloth.get_normals_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					Vector3[] array;
					blittableArrayWrapper.Unmarshal<Vector3>(ref array);
					array2 = array;
				}
				return array2;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002120 File Offset: 0x00000320
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002164 File Offset: 0x00000364
		public unsafe ClothSkinningCoefficient[] coefficients
		{
			[NativeName("GetCoefficients")]
			get
			{
				ClothSkinningCoefficient[] array2;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					Cloth.get_coefficients_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					ClothSkinningCoefficient[] array;
					blittableArrayWrapper.Unmarshal<ClothSkinningCoefficient>(ref array);
					array2 = array;
				}
				return array2;
			}
			[NativeName("SetCoefficients")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Span<ClothSkinningCoefficient> span = new Span<ClothSkinningCoefficient>(value);
				fixed (ClothSkinningCoefficient* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Cloth.set_coefficients_Injected(intPtr, ref managedSpanWrapper);
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021AC File Offset: 0x000003AC
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000021D0 File Offset: 0x000003D0
		public CapsuleCollider[] capsuleColliders
		{
			[NativeName("GetCapsuleColliders")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_capsuleColliders_Injected(intPtr);
			}
			[NativeName("SetCapsuleColliders")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_capsuleColliders_Injected(intPtr, value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021F4 File Offset: 0x000003F4
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002218 File Offset: 0x00000418
		public ClothSphereColliderPair[] sphereColliders
		{
			[NativeName("GetSphereColliders")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_sphereColliders_Injected(intPtr);
			}
			[NativeName("SetSphereColliders")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_sphereColliders_Injected(intPtr, value);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000223C File Offset: 0x0000043C
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002260 File Offset: 0x00000460
		public float sleepThreshold
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_sleepThreshold_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_sleepThreshold_Injected(intPtr, value);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002284 File Offset: 0x00000484
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000022A8 File Offset: 0x000004A8
		public float bendingStiffness
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_bendingStiffness_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_bendingStiffness_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022CC File Offset: 0x000004CC
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000022F0 File Offset: 0x000004F0
		public float stretchingStiffness
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_stretchingStiffness_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_stretchingStiffness_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002314 File Offset: 0x00000514
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002338 File Offset: 0x00000538
		public float damping
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_damping_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_damping_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000235C File Offset: 0x0000055C
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002384 File Offset: 0x00000584
		public Vector3 externalAcceleration
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Cloth.get_externalAcceleration_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_externalAcceleration_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023A8 File Offset: 0x000005A8
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000023D0 File Offset: 0x000005D0
		public Vector3 randomAcceleration
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Cloth.get_randomAcceleration_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_randomAcceleration_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023F4 File Offset: 0x000005F4
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002418 File Offset: 0x00000618
		public bool useGravity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_useGravity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_useGravity_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000243C File Offset: 0x0000063C
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002460 File Offset: 0x00000660
		public bool enabled
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_enabled_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_enabled_Injected(intPtr, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002484 File Offset: 0x00000684
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000024A8 File Offset: 0x000006A8
		public float friction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_friction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_friction_Injected(intPtr, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000024CC File Offset: 0x000006CC
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000024F0 File Offset: 0x000006F0
		public float collisionMassScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_collisionMassScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_collisionMassScale_Injected(intPtr, value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002514 File Offset: 0x00000714
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002538 File Offset: 0x00000738
		public bool enableContinuousCollision
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_enableContinuousCollision_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_enableContinuousCollision_Injected(intPtr, value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000255C File Offset: 0x0000075C
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002580 File Offset: 0x00000780
		public float useVirtualParticles
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_useVirtualParticles_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_useVirtualParticles_Injected(intPtr, value);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000025A4 File Offset: 0x000007A4
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000025C8 File Offset: 0x000007C8
		public float worldVelocityScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_worldVelocityScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_worldVelocityScale_Injected(intPtr, value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000025EC File Offset: 0x000007EC
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002610 File Offset: 0x00000810
		public float worldAccelerationScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_worldAccelerationScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_worldAccelerationScale_Injected(intPtr, value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002634 File Offset: 0x00000834
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002658 File Offset: 0x00000858
		public float clothSolverFrequency
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_clothSolverFrequency_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_clothSolverFrequency_Injected(intPtr, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000267C File Offset: 0x0000087C
		// (set) Token: 0x0600002E RID: 46 RVA: 0x0000269B File Offset: 0x0000089B
		[Obsolete("Parameter solverFrequency is obsolete and no longer supported. Please use clothSolverFrequency instead.")]
		public bool solverFrequency
		{
			get
			{
				return this.clothSolverFrequency > 0f;
			}
			set
			{
				this.clothSolverFrequency = (value ? 120f : 0f);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026B4 File Offset: 0x000008B4
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000026D8 File Offset: 0x000008D8
		public bool useTethers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_useTethers_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_useTethers_Injected(intPtr, value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000026FC File Offset: 0x000008FC
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002720 File Offset: 0x00000920
		public float stiffnessFrequency
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_stiffnessFrequency_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_stiffnessFrequency_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002744 File Offset: 0x00000944
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002768 File Offset: 0x00000968
		public float selfCollisionDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_selfCollisionDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_selfCollisionDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000278C File Offset: 0x0000098C
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000027B0 File Offset: 0x000009B0
		public float selfCollisionStiffness
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Cloth.get_selfCollisionStiffness_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Cloth.set_selfCollisionStiffness_Injected(intPtr, value);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027D4 File Offset: 0x000009D4
		public void ClearTransformMotion()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Cloth.ClearTransformMotion_Injected(intPtr);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027F8 File Offset: 0x000009F8
		public unsafe void GetSelfAndInterCollisionIndices([NotNull] List<uint> indices)
		{
			if (indices == null)
			{
				ThrowHelper.ThrowArgumentNullException(indices, "indices");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (uint[] array = NoAllocHelpers.ExtractArrayFromList<uint>(indices))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, indices.Count);
					Cloth.GetSelfAndInterCollisionIndices_Injected(intPtr, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<uint>(indices);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002874 File Offset: 0x00000A74
		public unsafe void SetSelfAndInterCollisionIndices([NotNull] List<uint> indices)
		{
			if (indices == null)
			{
				ThrowHelper.ThrowArgumentNullException(indices, "indices");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (uint[] array = NoAllocHelpers.ExtractArrayFromList<uint>(indices))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, indices.Count);
					Cloth.SetSelfAndInterCollisionIndices_Injected(intPtr, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<uint>(indices);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000028F0 File Offset: 0x00000AF0
		public unsafe void GetVirtualParticleIndices([NotNull] List<uint> indicesOutList)
		{
			if (indicesOutList == null)
			{
				ThrowHelper.ThrowArgumentNullException(indicesOutList, "indicesOutList");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (uint[] array = NoAllocHelpers.ExtractArrayFromList<uint>(indicesOutList))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, indicesOutList.Count);
					Cloth.GetVirtualParticleIndices_Injected(intPtr, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<uint>(indicesOutList);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000296C File Offset: 0x00000B6C
		public unsafe void SetVirtualParticleIndices([NotNull] List<uint> indicesIn)
		{
			if (indicesIn == null)
			{
				ThrowHelper.ThrowArgumentNullException(indicesIn, "indicesIn");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (uint[] array = NoAllocHelpers.ExtractArrayFromList<uint>(indicesIn))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, indicesIn.Count);
					Cloth.SetVirtualParticleIndices_Injected(intPtr, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<uint>(indicesIn);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000029E8 File Offset: 0x00000BE8
		public unsafe void GetVirtualParticleWeights([NotNull] List<Vector3> weightsOutList)
		{
			if (weightsOutList == null)
			{
				ThrowHelper.ThrowArgumentNullException(weightsOutList, "weightsOutList");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (Vector3[] array = NoAllocHelpers.ExtractArrayFromList<Vector3>(weightsOutList))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, weightsOutList.Count);
					Cloth.GetVirtualParticleWeights_Injected(intPtr, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Vector3>(weightsOutList);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A64 File Offset: 0x00000C64
		public unsafe void SetVirtualParticleWeights([NotNull] List<Vector3> weights)
		{
			if (weights == null)
			{
				ThrowHelper.ThrowArgumentNullException(weights, "weights");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (Vector3[] array = NoAllocHelpers.ExtractArrayFromList<Vector3>(weights))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, weights.Count);
					Cloth.SetVirtualParticleWeights_Injected(intPtr, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Vector3>(weights);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002AE0 File Offset: 0x00000CE0
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002AE8 File Offset: 0x00000CE8
		[Obsolete("useContinuousCollision is no longer supported, use enableContinuousCollision instead")]
		public float useContinuousCollision { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002AF1 File Offset: 0x00000CF1
		[Obsolete("Deprecated.Cloth.selfCollisions is no longer supported since Unity 5.0.", true)]
		public bool selfCollision { get; }

		// Token: 0x06000041 RID: 65 RVA: 0x00002AFC File Offset: 0x00000CFC
		public void SetEnabledFading(bool enabled, float interpolationTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Cloth>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Cloth.SetEnabledFading_Injected(intPtr, enabled, interpolationTime);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B20 File Offset: 0x00000D20
		[ExcludeFromDocs]
		public void SetEnabledFading(bool enabled)
		{
			this.SetEnabledFading(enabled, 0.5f);
		}

		// Token: 0x06000044 RID: 68
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_vertices_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x06000045 RID: 69
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_normals_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x06000046 RID: 70
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_coefficients_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x06000047 RID: 71
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_coefficients_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x06000048 RID: 72
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CapsuleCollider[] get_capsuleColliders_Injected(IntPtr _unity_self);

		// Token: 0x06000049 RID: 73
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_capsuleColliders_Injected(IntPtr _unity_self, CapsuleCollider[] value);

		// Token: 0x0600004A RID: 74
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ClothSphereColliderPair[] get_sphereColliders_Injected(IntPtr _unity_self);

		// Token: 0x0600004B RID: 75
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sphereColliders_Injected(IntPtr _unity_self, ClothSphereColliderPair[] value);

		// Token: 0x0600004C RID: 76
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_sleepThreshold_Injected(IntPtr _unity_self);

		// Token: 0x0600004D RID: 77
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sleepThreshold_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600004E RID: 78
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_bendingStiffness_Injected(IntPtr _unity_self);

		// Token: 0x0600004F RID: 79
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bendingStiffness_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_stretchingStiffness_Injected(IntPtr _unity_self);

		// Token: 0x06000051 RID: 81
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stretchingStiffness_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_damping_Injected(IntPtr _unity_self);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_damping_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_externalAcceleration_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_externalAcceleration_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000056 RID: 86
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_randomAcceleration_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000057 RID: 87
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_randomAcceleration_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useGravity_Injected(IntPtr _unity_self);

		// Token: 0x06000059 RID: 89
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useGravity_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600005A RID: 90
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enabled_Injected(IntPtr _unity_self);

		// Token: 0x0600005B RID: 91
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enabled_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600005C RID: 92
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_friction_Injected(IntPtr _unity_self);

		// Token: 0x0600005D RID: 93
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_friction_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600005E RID: 94
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_collisionMassScale_Injected(IntPtr _unity_self);

		// Token: 0x0600005F RID: 95
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_collisionMassScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000060 RID: 96
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableContinuousCollision_Injected(IntPtr _unity_self);

		// Token: 0x06000061 RID: 97
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableContinuousCollision_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000062 RID: 98
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_useVirtualParticles_Injected(IntPtr _unity_self);

		// Token: 0x06000063 RID: 99
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useVirtualParticles_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000064 RID: 100
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_worldVelocityScale_Injected(IntPtr _unity_self);

		// Token: 0x06000065 RID: 101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_worldVelocityScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000066 RID: 102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_worldAccelerationScale_Injected(IntPtr _unity_self);

		// Token: 0x06000067 RID: 103
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_worldAccelerationScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000068 RID: 104
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_clothSolverFrequency_Injected(IntPtr _unity_self);

		// Token: 0x06000069 RID: 105
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_clothSolverFrequency_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600006A RID: 106
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useTethers_Injected(IntPtr _unity_self);

		// Token: 0x0600006B RID: 107
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useTethers_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600006C RID: 108
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_stiffnessFrequency_Injected(IntPtr _unity_self);

		// Token: 0x0600006D RID: 109
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stiffnessFrequency_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600006E RID: 110
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_selfCollisionDistance_Injected(IntPtr _unity_self);

		// Token: 0x0600006F RID: 111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_selfCollisionDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_selfCollisionStiffness_Injected(IntPtr _unity_self);

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_selfCollisionStiffness_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000072 RID: 114
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearTransformMotion_Injected(IntPtr _unity_self);

		// Token: 0x06000073 RID: 115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSelfAndInterCollisionIndices_Injected(IntPtr _unity_self, ref BlittableListWrapper indices);

		// Token: 0x06000074 RID: 116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSelfAndInterCollisionIndices_Injected(IntPtr _unity_self, ref BlittableListWrapper indices);

		// Token: 0x06000075 RID: 117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVirtualParticleIndices_Injected(IntPtr _unity_self, ref BlittableListWrapper indicesOutList);

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVirtualParticleIndices_Injected(IntPtr _unity_self, ref BlittableListWrapper indicesIn);

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVirtualParticleWeights_Injected(IntPtr _unity_self, ref BlittableListWrapper weightsOutList);

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVirtualParticleWeights_Injected(IntPtr _unity_self, ref BlittableListWrapper weights);

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetEnabledFading_Injected(IntPtr _unity_self, bool enabled, float interpolationTime);
	}
}
