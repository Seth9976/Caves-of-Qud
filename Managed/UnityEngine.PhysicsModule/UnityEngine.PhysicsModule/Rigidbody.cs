using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000052 RID: 82
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Physics/Rigidbody.h")]
	public class Rigidbody : Component
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		public Vector3 linearVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Rigidbody.get_linearVelocity_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_linearVelocity_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000ACF4 File Offset: 0x00008EF4
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0000AD1C File Offset: 0x00008F1C
		public Vector3 angularVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Rigidbody.get_angularVelocity_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_angularVelocity_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000AD40 File Offset: 0x00008F40
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0000AD64 File Offset: 0x00008F64
		public float linearDamping
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_linearDamping_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_linearDamping_Injected(intPtr, value);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000AD88 File Offset: 0x00008F88
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0000ADAC File Offset: 0x00008FAC
		public float angularDamping
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_angularDamping_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_angularDamping_Injected(intPtr, value);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000ADD0 File Offset: 0x00008FD0
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0000ADF4 File Offset: 0x00008FF4
		public float mass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_mass_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_mass_Injected(intPtr, value);
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000AE18 File Offset: 0x00009018
		public void SetDensity(float density)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.SetDensity_Injected(intPtr, density);
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000AE3C File Offset: 0x0000903C
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x0000AE60 File Offset: 0x00009060
		public bool useGravity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_useGravity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_useGravity_Injected(intPtr, value);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000AE84 File Offset: 0x00009084
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0000AEA8 File Offset: 0x000090A8
		public float maxDepenetrationVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_maxDepenetrationVelocity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_maxDepenetrationVelocity_Injected(intPtr, value);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0000AECC File Offset: 0x000090CC
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x0000AEF0 File Offset: 0x000090F0
		public bool isKinematic
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_isKinematic_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_isKinematic_Injected(intPtr, value);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000AF13 File Offset: 0x00009113
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0000AF2C File Offset: 0x0000912C
		public bool freezeRotation
		{
			get
			{
				return this.constraints.HasFlag(RigidbodyConstraints.FreezeRotation);
			}
			set
			{
				if (value)
				{
					this.constraints |= RigidbodyConstraints.FreezeRotation;
				}
				else
				{
					this.constraints &= RigidbodyConstraints.FreezePosition;
				}
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000AF64 File Offset: 0x00009164
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0000AF88 File Offset: 0x00009188
		public RigidbodyConstraints constraints
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_constraints_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_constraints_Injected(intPtr, value);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000AFAC File Offset: 0x000091AC
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0000AFD0 File Offset: 0x000091D0
		public CollisionDetectionMode collisionDetectionMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_collisionDetectionMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_collisionDetectionMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000AFF4 File Offset: 0x000091F4
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x0000B018 File Offset: 0x00009218
		public bool automaticCenterOfMass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_automaticCenterOfMass_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_automaticCenterOfMass_Injected(intPtr, value);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000B03C File Offset: 0x0000923C
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0000B064 File Offset: 0x00009264
		public Vector3 centerOfMass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Rigidbody.get_centerOfMass_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_centerOfMass_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000B088 File Offset: 0x00009288
		public Vector3 worldCenterOfMass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Rigidbody.get_worldCenterOfMass_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0000B0B0 File Offset: 0x000092B0
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0000B0D4 File Offset: 0x000092D4
		public bool automaticInertiaTensor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_automaticInertiaTensor_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_automaticInertiaTensor_Injected(intPtr, value);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0000B0F8 File Offset: 0x000092F8
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0000B120 File Offset: 0x00009320
		public Quaternion inertiaTensorRotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Rigidbody.get_inertiaTensorRotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_inertiaTensorRotation_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0000B144 File Offset: 0x00009344
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x0000B16C File Offset: 0x0000936C
		public Vector3 inertiaTensor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Rigidbody.get_inertiaTensor_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_inertiaTensor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0000B190 File Offset: 0x00009390
		internal Matrix4x4 worldInertiaTensorMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Rigidbody.get_worldInertiaTensorMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000B1B8 File Offset: 0x000093B8
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x0000B1DC File Offset: 0x000093DC
		public bool detectCollisions
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_detectCollisions_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_detectCollisions_Injected(intPtr, value);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000B200 File Offset: 0x00009400
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0000B228 File Offset: 0x00009428
		public Vector3 position
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Rigidbody.get_position_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_position_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000B24C File Offset: 0x0000944C
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x0000B274 File Offset: 0x00009474
		public Quaternion rotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Rigidbody.get_rotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_rotation_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000B298 File Offset: 0x00009498
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x0000B2BC File Offset: 0x000094BC
		public RigidbodyInterpolation interpolation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_interpolation_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_interpolation_Injected(intPtr, value);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000B2E0 File Offset: 0x000094E0
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0000B304 File Offset: 0x00009504
		public int solverIterations
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_solverIterations_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_solverIterations_Injected(intPtr, value);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000B328 File Offset: 0x00009528
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0000B34C File Offset: 0x0000954C
		public float sleepThreshold
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_sleepThreshold_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_sleepThreshold_Injected(intPtr, value);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000B370 File Offset: 0x00009570
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0000B394 File Offset: 0x00009594
		public float maxAngularVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_maxAngularVelocity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_maxAngularVelocity_Injected(intPtr, value);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000B3B8 File Offset: 0x000095B8
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0000B3DC File Offset: 0x000095DC
		public float maxLinearVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_maxLinearVelocity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_maxLinearVelocity_Injected(intPtr, value);
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000B400 File Offset: 0x00009600
		public void MovePosition(Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.MovePosition_Injected(intPtr, ref position);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000B424 File Offset: 0x00009624
		public void MoveRotation(Quaternion rot)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.MoveRotation_Injected(intPtr, ref rot);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000B448 File Offset: 0x00009648
		public void Move(Vector3 position, Quaternion rotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.Move_Injected(intPtr, ref position, ref rotation);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000B470 File Offset: 0x00009670
		public void Sleep()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.Sleep_Injected(intPtr);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000B494 File Offset: 0x00009694
		public bool IsSleeping()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Rigidbody.IsSleeping_Injected(intPtr);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000B4B8 File Offset: 0x000096B8
		public void WakeUp()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.WakeUp_Injected(intPtr);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000B4DC File Offset: 0x000096DC
		public void ResetCenterOfMass()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.ResetCenterOfMass_Injected(intPtr);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000B500 File Offset: 0x00009700
		public void ResetInertiaTensor()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.ResetInertiaTensor_Injected(intPtr);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0000B524 File Offset: 0x00009724
		public Vector3 GetRelativePointVelocity(Vector3 relativePoint)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Rigidbody.GetRelativePointVelocity_Injected(intPtr, ref relativePoint, out vector);
			return vector;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0000B54C File Offset: 0x0000974C
		public Vector3 GetPointVelocity(Vector3 worldPoint)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Rigidbody.GetPointVelocity_Injected(intPtr, ref worldPoint, out vector);
			return vector;
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000B574 File Offset: 0x00009774
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0000B598 File Offset: 0x00009798
		public int solverVelocityIterations
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_solverVelocityIterations_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_solverVelocityIterations_Injected(intPtr, value);
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000B5BC File Offset: 0x000097BC
		public void PublishTransform()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.PublishTransform_Injected(intPtr);
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000B5E0 File Offset: 0x000097E0
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0000B608 File Offset: 0x00009808
		public LayerMask excludeLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				Rigidbody.get_excludeLayers_Injected(intPtr, out layerMask);
				return layerMask;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_excludeLayers_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000B62C File Offset: 0x0000982C
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0000B654 File Offset: 0x00009854
		public LayerMask includeLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				Rigidbody.get_includeLayers_Injected(intPtr, out layerMask);
				return layerMask;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_includeLayers_Injected(intPtr, ref value);
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000B678 File Offset: 0x00009878
		public Vector3 GetAccumulatedForce([DefaultValue("Time.fixedDeltaTime")] float step)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Rigidbody.GetAccumulatedForce_Injected(intPtr, step, out vector);
			return vector;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000B6A0 File Offset: 0x000098A0
		[ExcludeFromDocs]
		public Vector3 GetAccumulatedForce()
		{
			return this.GetAccumulatedForce(Time.fixedDeltaTime);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000B6C0 File Offset: 0x000098C0
		public Vector3 GetAccumulatedTorque([DefaultValue("Time.fixedDeltaTime")] float step)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Rigidbody.GetAccumulatedTorque_Injected(intPtr, step, out vector);
			return vector;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000B6E8 File Offset: 0x000098E8
		[ExcludeFromDocs]
		public Vector3 GetAccumulatedTorque()
		{
			return this.GetAccumulatedTorque(Time.fixedDeltaTime);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000B708 File Offset: 0x00009908
		public void AddForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.AddForce_Injected(intPtr, ref force, mode);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000B72D File Offset: 0x0000992D
		[ExcludeFromDocs]
		public void AddForce(Vector3 force)
		{
			this.AddForce(force, ForceMode.Force);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000B739 File Offset: 0x00009939
		public void AddForce(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForce(new Vector3(x, y, z), mode);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0000B74D File Offset: 0x0000994D
		[ExcludeFromDocs]
		public void AddForce(float x, float y, float z)
		{
			this.AddForce(new Vector3(x, y, z), ForceMode.Force);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0000B760 File Offset: 0x00009960
		public void AddRelativeForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.AddRelativeForce_Injected(intPtr, ref force, mode);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0000B785 File Offset: 0x00009985
		[ExcludeFromDocs]
		public void AddRelativeForce(Vector3 force)
		{
			this.AddRelativeForce(force, ForceMode.Force);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0000B791 File Offset: 0x00009991
		public void AddRelativeForce(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeForce(new Vector3(x, y, z), mode);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000B7A5 File Offset: 0x000099A5
		[ExcludeFromDocs]
		public void AddRelativeForce(float x, float y, float z)
		{
			this.AddRelativeForce(new Vector3(x, y, z), ForceMode.Force);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000B7B8 File Offset: 0x000099B8
		public void AddTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.AddTorque_Injected(intPtr, ref torque, mode);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000B7DD File Offset: 0x000099DD
		[ExcludeFromDocs]
		public void AddTorque(Vector3 torque)
		{
			this.AddTorque(torque, ForceMode.Force);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000B7E9 File Offset: 0x000099E9
		public void AddTorque(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddTorque(new Vector3(x, y, z), mode);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000B7FD File Offset: 0x000099FD
		[ExcludeFromDocs]
		public void AddTorque(float x, float y, float z)
		{
			this.AddTorque(new Vector3(x, y, z), ForceMode.Force);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000B810 File Offset: 0x00009A10
		public void AddRelativeTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.AddRelativeTorque_Injected(intPtr, ref torque, mode);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000B835 File Offset: 0x00009A35
		[ExcludeFromDocs]
		public void AddRelativeTorque(Vector3 torque)
		{
			this.AddRelativeTorque(torque, ForceMode.Force);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000B841 File Offset: 0x00009A41
		public void AddRelativeTorque(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeTorque(new Vector3(x, y, z), mode);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000B855 File Offset: 0x00009A55
		[ExcludeFromDocs]
		public void AddRelativeTorque(float x, float y, float z)
		{
			this.AddRelativeTorque(x, y, z, ForceMode.Force);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000B864 File Offset: 0x00009A64
		public void AddForceAtPosition(Vector3 force, Vector3 position, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.AddForceAtPosition_Injected(intPtr, ref force, ref position, mode);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000B88B File Offset: 0x00009A8B
		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector3 force, Vector3 position)
		{
			this.AddForceAtPosition(force, position, ForceMode.Force);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000B898 File Offset: 0x00009A98
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, [DefaultValue("0.0f")] float upwardsModifier, [DefaultValue("ForceMode.Force)")] ForceMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.AddExplosionForce_Injected(intPtr, explosionForce, ref explosionPosition, explosionRadius, upwardsModifier, mode);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000B8C2 File Offset: 0x00009AC2
		[ExcludeFromDocs]
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier)
		{
			this.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode.Force);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000B8D2 File Offset: 0x00009AD2
		[ExcludeFromDocs]
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius)
		{
			this.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 0f, ForceMode.Force);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		[NativeName("ClosestPointOnBounds")]
		private void Internal_ClosestPointOnBounds(Vector3 point, ref Vector3 outPos, ref float distance)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.Internal_ClosestPointOnBounds_Injected(intPtr, ref point, ref outPos, ref distance);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000B910 File Offset: 0x00009B10
		public Vector3 ClosestPointOnBounds(Vector3 position)
		{
			float num = 0f;
			Vector3 zero = Vector3.zero;
			this.Internal_ClosestPointOnBounds(position, ref zero, ref num);
			return zero;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000B93C File Offset: 0x00009B3C
		private RaycastHit SweepTest(Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction, ref bool hasHit)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RaycastHit raycastHit;
			Rigidbody.SweepTest_Injected(intPtr, ref direction, maxDistance, queryTriggerInteraction, ref hasHit, out raycastHit);
			return raycastHit;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000B968 File Offset: 0x00009B68
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			bool flag3;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				bool flag2 = false;
				hitInfo = this.SweepTest(vector, maxDistance, queryTriggerInteraction, ref flag2);
				flag3 = flag2;
			}
			else
			{
				hitInfo = default(RaycastHit);
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000B9BC File Offset: 0x00009BBC
		[ExcludeFromDocs]
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			return this.SweepTest(direction, out hitInfo, maxDistance, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		[ExcludeFromDocs]
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo)
		{
			return this.SweepTest(direction, out hitInfo, float.PositiveInfinity, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		[NativeName("SweepTestAll")]
		private RaycastHit[] Internal_SweepTestAll(Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Rigidbody.Internal_SweepTestAll_Injected(intPtr, ref direction, maxDistance, queryTriggerInteraction, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RaycastHit[] array;
				blittableArrayWrapper.Unmarshal<RaycastHit>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000BA40 File Offset: 0x00009C40
		public RaycastHit[] SweepTestAll(Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			RaycastHit[] array;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				array = this.Internal_SweepTestAll(vector, maxDistance, queryTriggerInteraction);
			}
			else
			{
				array = new RaycastHit[0];
			}
			return array;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0000BA84 File Offset: 0x00009C84
		[ExcludeFromDocs]
		public RaycastHit[] SweepTestAll(Vector3 direction, float maxDistance)
		{
			return this.SweepTestAll(direction, maxDistance, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		[ExcludeFromDocs]
		public RaycastHit[] SweepTestAll(Vector3 direction)
		{
			return this.SweepTestAll(direction, float.PositiveInfinity, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000BABF File Offset: 0x00009CBF
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x0000BAC7 File Offset: 0x00009CC7
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use Rigidbody.linearDamping instead. (UnityUpgradable) -> linearDamping")]
		public float drag
		{
			get
			{
				return this.linearDamping;
			}
			set
			{
				this.linearDamping = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0000BAD1 File Offset: 0x00009CD1
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0000BAD9 File Offset: 0x00009CD9
		[Obsolete("Please use Rigidbody.angularDamping instead. (UnityUpgradable) -> angularDamping")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float angularDrag
		{
			get
			{
				return this.angularDamping;
			}
			set
			{
				this.angularDamping = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000BAE3 File Offset: 0x00009CE3
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0000BAEB File Offset: 0x00009CEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use Rigidbody.linearVelocity instead. (UnityUpgradable) -> linearVelocity")]
		public Vector3 velocity
		{
			get
			{
				return this.linearVelocity;
			}
			set
			{
				this.linearVelocity = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0000274B File Offset: 0x0000094B
		[Obsolete("The sleepVelocity is no longer supported. Use sleepThreshold. Note that sleepThreshold is energy but not velocity.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float sleepVelocity
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0000BB10 File Offset: 0x00009D10
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0000274B File Offset: 0x0000094B
		[Obsolete("The sleepAngularVelocity is no longer supported. Use sleepThreshold to specify energy.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float sleepAngularVelocity
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0000BB27 File Offset: 0x00009D27
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Rigidbody.maxAngularVelocity instead.")]
		public void SetMaxAngularVelocity(float a)
		{
			this.maxAngularVelocity = a;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0000BB34 File Offset: 0x00009D34
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x0000274B File Offset: 0x0000094B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Cone friction is no longer supported.", true)]
		public bool useConeFriction
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0000BB48 File Offset: 0x00009D48
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x0000BB60 File Offset: 0x00009D60
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use Rigidbody.solverIterations instead. (UnityUpgradable) -> solverIterations", true)]
		public int solverIterationCount
		{
			get
			{
				return this.solverIterations;
			}
			set
			{
				this.solverIterations = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000BB6C File Offset: 0x00009D6C
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x0000BB84 File Offset: 0x00009D84
		[Obsolete("Please use Rigidbody.solverVelocityIterations instead. (UnityUpgradable) -> solverVelocityIterations", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int solverVelocityIterationCount
		{
			get
			{
				return this.solverVelocityIterations;
			}
			set
			{
				this.solverVelocityIterations = value;
			}
		}

		// Token: 0x0600061B RID: 1563
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_linearVelocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600061C RID: 1564
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearVelocity_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600061D RID: 1565
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_angularVelocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600061E RID: 1566
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularVelocity_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600061F RID: 1567
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_linearDamping_Injected(IntPtr _unity_self);

		// Token: 0x06000620 RID: 1568
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearDamping_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000621 RID: 1569
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_angularDamping_Injected(IntPtr _unity_self);

		// Token: 0x06000622 RID: 1570
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularDamping_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000623 RID: 1571
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_mass_Injected(IntPtr _unity_self);

		// Token: 0x06000624 RID: 1572
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mass_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000625 RID: 1573
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDensity_Injected(IntPtr _unity_self, float density);

		// Token: 0x06000626 RID: 1574
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useGravity_Injected(IntPtr _unity_self);

		// Token: 0x06000627 RID: 1575
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useGravity_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000628 RID: 1576
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxDepenetrationVelocity_Injected(IntPtr _unity_self);

		// Token: 0x06000629 RID: 1577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxDepenetrationVelocity_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600062A RID: 1578
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isKinematic_Injected(IntPtr _unity_self);

		// Token: 0x0600062B RID: 1579
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_isKinematic_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600062C RID: 1580
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RigidbodyConstraints get_constraints_Injected(IntPtr _unity_self);

		// Token: 0x0600062D RID: 1581
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_constraints_Injected(IntPtr _unity_self, RigidbodyConstraints value);

		// Token: 0x0600062E RID: 1582
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CollisionDetectionMode get_collisionDetectionMode_Injected(IntPtr _unity_self);

		// Token: 0x0600062F RID: 1583
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_collisionDetectionMode_Injected(IntPtr _unity_self, CollisionDetectionMode value);

		// Token: 0x06000630 RID: 1584
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_automaticCenterOfMass_Injected(IntPtr _unity_self);

		// Token: 0x06000631 RID: 1585
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_automaticCenterOfMass_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000632 RID: 1586
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_centerOfMass_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000633 RID: 1587
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_centerOfMass_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000634 RID: 1588
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_worldCenterOfMass_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000635 RID: 1589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_automaticInertiaTensor_Injected(IntPtr _unity_self);

		// Token: 0x06000636 RID: 1590
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_automaticInertiaTensor_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000637 RID: 1591
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_inertiaTensorRotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x06000638 RID: 1592
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_inertiaTensorRotation_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x06000639 RID: 1593
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_inertiaTensor_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600063A RID: 1594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_inertiaTensor_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600063B RID: 1595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_worldInertiaTensorMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x0600063C RID: 1596
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_detectCollisions_Injected(IntPtr _unity_self);

		// Token: 0x0600063D RID: 1597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_detectCollisions_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600063E RID: 1598
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_position_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600063F RID: 1599
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_position_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000640 RID: 1600
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x06000641 RID: 1601
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotation_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x06000642 RID: 1602
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RigidbodyInterpolation get_interpolation_Injected(IntPtr _unity_self);

		// Token: 0x06000643 RID: 1603
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_interpolation_Injected(IntPtr _unity_self, RigidbodyInterpolation value);

		// Token: 0x06000644 RID: 1604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_solverIterations_Injected(IntPtr _unity_self);

		// Token: 0x06000645 RID: 1605
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_solverIterations_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000646 RID: 1606
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_sleepThreshold_Injected(IntPtr _unity_self);

		// Token: 0x06000647 RID: 1607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sleepThreshold_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000648 RID: 1608
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxAngularVelocity_Injected(IntPtr _unity_self);

		// Token: 0x06000649 RID: 1609
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxAngularVelocity_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600064A RID: 1610
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxLinearVelocity_Injected(IntPtr _unity_self);

		// Token: 0x0600064B RID: 1611
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxLinearVelocity_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600064C RID: 1612
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MovePosition_Injected(IntPtr _unity_self, [In] ref Vector3 position);

		// Token: 0x0600064D RID: 1613
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveRotation_Injected(IntPtr _unity_self, [In] ref Quaternion rot);

		// Token: 0x0600064E RID: 1614
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Move_Injected(IntPtr _unity_self, [In] ref Vector3 position, [In] ref Quaternion rotation);

		// Token: 0x0600064F RID: 1615
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Sleep_Injected(IntPtr _unity_self);

		// Token: 0x06000650 RID: 1616
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsSleeping_Injected(IntPtr _unity_self);

		// Token: 0x06000651 RID: 1617
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WakeUp_Injected(IntPtr _unity_self);

		// Token: 0x06000652 RID: 1618
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetCenterOfMass_Injected(IntPtr _unity_self);

		// Token: 0x06000653 RID: 1619
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetInertiaTensor_Injected(IntPtr _unity_self);

		// Token: 0x06000654 RID: 1620
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRelativePointVelocity_Injected(IntPtr _unity_self, [In] ref Vector3 relativePoint, out Vector3 ret);

		// Token: 0x06000655 RID: 1621
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPointVelocity_Injected(IntPtr _unity_self, [In] ref Vector3 worldPoint, out Vector3 ret);

		// Token: 0x06000656 RID: 1622
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_solverVelocityIterations_Injected(IntPtr _unity_self);

		// Token: 0x06000657 RID: 1623
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_solverVelocityIterations_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000658 RID: 1624
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PublishTransform_Injected(IntPtr _unity_self);

		// Token: 0x06000659 RID: 1625
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_excludeLayers_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x0600065A RID: 1626
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_excludeLayers_Injected(IntPtr _unity_self, [In] ref LayerMask value);

		// Token: 0x0600065B RID: 1627
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_includeLayers_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x0600065C RID: 1628
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_includeLayers_Injected(IntPtr _unity_self, [In] ref LayerMask value);

		// Token: 0x0600065D RID: 1629
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAccumulatedForce_Injected(IntPtr _unity_self, [DefaultValue("Time.fixedDeltaTime")] float step, out Vector3 ret);

		// Token: 0x0600065E RID: 1630
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAccumulatedTorque_Injected(IntPtr _unity_self, [DefaultValue("Time.fixedDeltaTime")] float step, out Vector3 ret);

		// Token: 0x0600065F RID: 1631
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddForce_Injected(IntPtr _unity_self, [In] ref Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x06000660 RID: 1632
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddRelativeForce_Injected(IntPtr _unity_self, [In] ref Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x06000661 RID: 1633
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddTorque_Injected(IntPtr _unity_self, [In] ref Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x06000662 RID: 1634
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddRelativeTorque_Injected(IntPtr _unity_self, [In] ref Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x06000663 RID: 1635
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddForceAtPosition_Injected(IntPtr _unity_self, [In] ref Vector3 force, [In] ref Vector3 position, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x06000664 RID: 1636
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddExplosionForce_Injected(IntPtr _unity_self, float explosionForce, [In] ref Vector3 explosionPosition, float explosionRadius, [DefaultValue("0.0f")] float upwardsModifier, [DefaultValue("ForceMode.Force)")] ForceMode mode);

		// Token: 0x06000665 RID: 1637
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ClosestPointOnBounds_Injected(IntPtr _unity_self, [In] ref Vector3 point, ref Vector3 outPos, ref float distance);

		// Token: 0x06000666 RID: 1638
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SweepTest_Injected(IntPtr _unity_self, [In] ref Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction, ref bool hasHit, out RaycastHit ret);

		// Token: 0x06000667 RID: 1639
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SweepTestAll_Injected(IntPtr _unity_self, [In] ref Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction, out BlittableArrayWrapper ret);
	}
}
