using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000016 RID: 22
	[NativeClass("Physics::ArticulationBody")]
	[NativeHeader("Modules/Physics/ArticulationBody.h")]
	[RequireComponent(typeof(Transform))]
	public class ArticulationBody : Behaviour
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002BA0 File Offset: 0x00000DA0
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public ArticulationJointType jointType
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_jointType_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_jointType_Injected(intPtr, value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002BE8 File Offset: 0x00000DE8
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002C10 File Offset: 0x00000E10
		public Vector3 anchorPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ArticulationBody.get_anchorPosition_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_anchorPosition_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002C34 File Offset: 0x00000E34
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002C5C File Offset: 0x00000E5C
		public Vector3 parentAnchorPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ArticulationBody.get_parentAnchorPosition_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_parentAnchorPosition_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002C80 File Offset: 0x00000E80
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public Quaternion anchorRotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				ArticulationBody.get_anchorRotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_anchorRotation_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002CCC File Offset: 0x00000ECC
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public Quaternion parentAnchorRotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				ArticulationBody.get_parentAnchorRotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_parentAnchorRotation_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002D18 File Offset: 0x00000F18
		public bool isRoot
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_isRoot_Injected(intPtr);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002D3C File Offset: 0x00000F3C
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002D60 File Offset: 0x00000F60
		public bool matchAnchors
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_matchAnchors_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_matchAnchors_Injected(intPtr, value);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002D84 File Offset: 0x00000F84
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public ArticulationDofLock linearLockX
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_linearLockX_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_linearLockX_Injected(intPtr, value);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002DCC File Offset: 0x00000FCC
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public ArticulationDofLock linearLockY
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_linearLockY_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_linearLockY_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002E14 File Offset: 0x00001014
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002E38 File Offset: 0x00001038
		public ArticulationDofLock linearLockZ
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_linearLockZ_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_linearLockZ_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002E5C File Offset: 0x0000105C
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002E80 File Offset: 0x00001080
		public ArticulationDofLock swingYLock
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_swingYLock_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_swingYLock_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002EA4 File Offset: 0x000010A4
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002EC8 File Offset: 0x000010C8
		public ArticulationDofLock swingZLock
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_swingZLock_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_swingZLock_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002EEC File Offset: 0x000010EC
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002F10 File Offset: 0x00001110
		public ArticulationDofLock twistLock
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_twistLock_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_twistLock_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002F34 File Offset: 0x00001134
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002F5C File Offset: 0x0000115C
		public ArticulationDrive xDrive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationDrive articulationDrive;
				ArticulationBody.get_xDrive_Injected(intPtr, out articulationDrive);
				return articulationDrive;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_xDrive_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002F80 File Offset: 0x00001180
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002FA8 File Offset: 0x000011A8
		public ArticulationDrive yDrive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationDrive articulationDrive;
				ArticulationBody.get_yDrive_Injected(intPtr, out articulationDrive);
				return articulationDrive;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_yDrive_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002FCC File Offset: 0x000011CC
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002FF4 File Offset: 0x000011F4
		public ArticulationDrive zDrive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationDrive articulationDrive;
				ArticulationBody.get_zDrive_Injected(intPtr, out articulationDrive);
				return articulationDrive;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_zDrive_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003018 File Offset: 0x00001218
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000303C File Offset: 0x0000123C
		public bool immovable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_immovable_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_immovable_Injected(intPtr, value);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003060 File Offset: 0x00001260
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003084 File Offset: 0x00001284
		public bool useGravity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_useGravity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_useGravity_Injected(intPtr, value);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000030A8 File Offset: 0x000012A8
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000030CC File Offset: 0x000012CC
		public float linearDamping
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_linearDamping_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_linearDamping_Injected(intPtr, value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000030F0 File Offset: 0x000012F0
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003114 File Offset: 0x00001314
		public float angularDamping
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_angularDamping_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_angularDamping_Injected(intPtr, value);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003138 File Offset: 0x00001338
		// (set) Token: 0x06000082 RID: 130 RVA: 0x0000315C File Offset: 0x0000135C
		public float jointFriction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_jointFriction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_jointFriction_Injected(intPtr, value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003180 File Offset: 0x00001380
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000031A8 File Offset: 0x000013A8
		public LayerMask excludeLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				ArticulationBody.get_excludeLayers_Injected(intPtr, out layerMask);
				return layerMask;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_excludeLayers_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000031CC File Offset: 0x000013CC
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000031F4 File Offset: 0x000013F4
		public LayerMask includeLayers
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LayerMask layerMask;
				ArticulationBody.get_includeLayers_Injected(intPtr, out layerMask);
				return layerMask;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_includeLayers_Injected(intPtr, ref value);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003218 File Offset: 0x00001418
		public Vector3 GetAccumulatedForce([DefaultValue("Time.fixedDeltaTime")] float step)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			ArticulationBody.GetAccumulatedForce_Injected(intPtr, step, out vector);
			return vector;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003240 File Offset: 0x00001440
		[ExcludeFromDocs]
		public Vector3 GetAccumulatedForce()
		{
			return this.GetAccumulatedForce(Time.fixedDeltaTime);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003260 File Offset: 0x00001460
		public Vector3 GetAccumulatedTorque([DefaultValue("Time.fixedDeltaTime")] float step)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			ArticulationBody.GetAccumulatedTorque_Injected(intPtr, step, out vector);
			return vector;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003288 File Offset: 0x00001488
		[ExcludeFromDocs]
		public Vector3 GetAccumulatedTorque()
		{
			return this.GetAccumulatedTorque(Time.fixedDeltaTime);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000032A8 File Offset: 0x000014A8
		public void AddForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.AddForce_Injected(intPtr, ref force, mode);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000032CD File Offset: 0x000014CD
		[ExcludeFromDocs]
		public void AddForce(Vector3 force)
		{
			this.AddForce(force, ForceMode.Force);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000032DC File Offset: 0x000014DC
		public void AddRelativeForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.AddRelativeForce_Injected(intPtr, ref force, mode);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003301 File Offset: 0x00001501
		[ExcludeFromDocs]
		public void AddRelativeForce(Vector3 force)
		{
			this.AddRelativeForce(force, ForceMode.Force);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003310 File Offset: 0x00001510
		public void AddTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.AddTorque_Injected(intPtr, ref torque, mode);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003335 File Offset: 0x00001535
		[ExcludeFromDocs]
		public void AddTorque(Vector3 torque)
		{
			this.AddTorque(torque, ForceMode.Force);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003344 File Offset: 0x00001544
		public void AddRelativeTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.AddRelativeTorque_Injected(intPtr, ref torque, mode);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003369 File Offset: 0x00001569
		[ExcludeFromDocs]
		public void AddRelativeTorque(Vector3 torque)
		{
			this.AddRelativeTorque(torque, ForceMode.Force);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003378 File Offset: 0x00001578
		public void AddForceAtPosition(Vector3 force, Vector3 position, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.AddForceAtPosition_Injected(intPtr, ref force, ref position, mode);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000339F File Offset: 0x0000159F
		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector3 force, Vector3 position)
		{
			this.AddForceAtPosition(force, position, ForceMode.Force);
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000033AC File Offset: 0x000015AC
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000033D4 File Offset: 0x000015D4
		public Vector3 linearVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ArticulationBody.get_linearVelocity_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_linearVelocity_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000033F8 File Offset: 0x000015F8
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003420 File Offset: 0x00001620
		public Vector3 angularVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ArticulationBody.get_angularVelocity_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_angularVelocity_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003444 File Offset: 0x00001644
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003468 File Offset: 0x00001668
		public float mass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_mass_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_mass_Injected(intPtr, value);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000348C File Offset: 0x0000168C
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000034B0 File Offset: 0x000016B0
		public bool automaticCenterOfMass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_automaticCenterOfMass_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_automaticCenterOfMass_Injected(intPtr, value);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000034D4 File Offset: 0x000016D4
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000034FC File Offset: 0x000016FC
		public Vector3 centerOfMass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ArticulationBody.get_centerOfMass_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_centerOfMass_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003520 File Offset: 0x00001720
		public Vector3 worldCenterOfMass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ArticulationBody.get_worldCenterOfMass_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003548 File Offset: 0x00001748
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x0000356C File Offset: 0x0000176C
		public bool automaticInertiaTensor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_automaticInertiaTensor_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_automaticInertiaTensor_Injected(intPtr, value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003590 File Offset: 0x00001790
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000035B8 File Offset: 0x000017B8
		public Vector3 inertiaTensor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ArticulationBody.get_inertiaTensor_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_inertiaTensor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000035DC File Offset: 0x000017DC
		internal Matrix4x4 worldInertiaTensorMatrix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				ArticulationBody.get_worldInertiaTensorMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003604 File Offset: 0x00001804
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000362C File Offset: 0x0000182C
		public Quaternion inertiaTensorRotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				ArticulationBody.get_inertiaTensorRotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_inertiaTensorRotation_Injected(intPtr, ref value);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003650 File Offset: 0x00001850
		public void ResetCenterOfMass()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.ResetCenterOfMass_Injected(intPtr);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003674 File Offset: 0x00001874
		public void ResetInertiaTensor()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.ResetInertiaTensor_Injected(intPtr);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003698 File Offset: 0x00001898
		public void Sleep()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.Sleep_Injected(intPtr);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000036BC File Offset: 0x000018BC
		public bool IsSleeping()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ArticulationBody.IsSleeping_Injected(intPtr);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000036E0 File Offset: 0x000018E0
		public void WakeUp()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.WakeUp_Injected(intPtr);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003704 File Offset: 0x00001904
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003728 File Offset: 0x00001928
		public float sleepThreshold
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_sleepThreshold_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_sleepThreshold_Injected(intPtr, value);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000374C File Offset: 0x0000194C
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00003770 File Offset: 0x00001970
		public int solverIterations
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_solverIterations_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_solverIterations_Injected(intPtr, value);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003794 File Offset: 0x00001994
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000037B8 File Offset: 0x000019B8
		public int solverVelocityIterations
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_solverVelocityIterations_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_solverVelocityIterations_Injected(intPtr, value);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000037DC File Offset: 0x000019DC
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003800 File Offset: 0x00001A00
		public float maxAngularVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_maxAngularVelocity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_maxAngularVelocity_Injected(intPtr, value);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003824 File Offset: 0x00001A24
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003848 File Offset: 0x00001A48
		public float maxLinearVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_maxLinearVelocity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_maxLinearVelocity_Injected(intPtr, value);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000386C File Offset: 0x00001A6C
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003890 File Offset: 0x00001A90
		public float maxJointVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_maxJointVelocity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_maxJointVelocity_Injected(intPtr, value);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000038B4 File Offset: 0x00001AB4
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000038D8 File Offset: 0x00001AD8
		public float maxDepenetrationVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_maxDepenetrationVelocity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_maxDepenetrationVelocity_Injected(intPtr, value);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000038FC File Offset: 0x00001AFC
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003924 File Offset: 0x00001B24
		public ArticulationReducedSpace jointPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationReducedSpace articulationReducedSpace;
				ArticulationBody.get_jointPosition_Injected(intPtr, out articulationReducedSpace);
				return articulationReducedSpace;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_jointPosition_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003948 File Offset: 0x00001B48
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00003970 File Offset: 0x00001B70
		public ArticulationReducedSpace jointVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationReducedSpace articulationReducedSpace;
				ArticulationBody.get_jointVelocity_Injected(intPtr, out articulationReducedSpace);
				return articulationReducedSpace;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_jointVelocity_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003994 File Offset: 0x00001B94
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000039BC File Offset: 0x00001BBC
		public ArticulationReducedSpace jointAcceleration
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationReducedSpace articulationReducedSpace;
				ArticulationBody.get_jointAcceleration_Injected(intPtr, out articulationReducedSpace);
				return articulationReducedSpace;
			}
			[Obsolete("Setting joint accelerations is not supported in forward kinematics. To have inverse dynamics take acceleration into account, use GetJointForcesForAcceleration instead", true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_jointAcceleration_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000039E0 File Offset: 0x00001BE0
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003A08 File Offset: 0x00001C08
		public ArticulationReducedSpace jointForce
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationReducedSpace articulationReducedSpace;
				ArticulationBody.get_jointForce_Injected(intPtr, out articulationReducedSpace);
				return articulationReducedSpace;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_jointForce_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003A2C File Offset: 0x00001C2C
		public ArticulationReducedSpace driveForce
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationReducedSpace articulationReducedSpace;
				ArticulationBody.get_driveForce_Injected(intPtr, out articulationReducedSpace);
				return articulationReducedSpace;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003A54 File Offset: 0x00001C54
		public int dofCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_dofCount_Injected(intPtr);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003A78 File Offset: 0x00001C78
		public int index
		{
			[NativeMethod("GetBodyIndex")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_index_Injected(intPtr);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003A9C File Offset: 0x00001C9C
		public void TeleportRoot(Vector3 position, Quaternion rotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.TeleportRoot_Injected(intPtr, ref position, ref rotation);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003AC4 File Offset: 0x00001CC4
		public Vector3 GetClosestPoint(Vector3 point)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			ArticulationBody.GetClosestPoint_Injected(intPtr, ref point, out vector);
			return vector;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003AEC File Offset: 0x00001CEC
		public Vector3 GetRelativePointVelocity(Vector3 relativePoint)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			ArticulationBody.GetRelativePointVelocity_Injected(intPtr, ref relativePoint, out vector);
			return vector;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003B14 File Offset: 0x00001D14
		public Vector3 GetPointVelocity(Vector3 worldPoint)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			ArticulationBody.GetPointVelocity_Injected(intPtr, ref worldPoint, out vector);
			return vector;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003B3C File Offset: 0x00001D3C
		[NativeMethod("GetDenseJacobian")]
		private int GetDenseJacobian_Internal(ref ArticulationJacobian jacobian)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ArticulationBody.GetDenseJacobian_Internal_Injected(intPtr, ref jacobian);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003B60 File Offset: 0x00001D60
		public int GetDenseJacobian(ref ArticulationJacobian jacobian)
		{
			bool flag = jacobian.elements == null;
			if (flag)
			{
				jacobian.elements = new List<float>();
			}
			return this.GetDenseJacobian_Internal(ref jacobian);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003B94 File Offset: 0x00001D94
		public unsafe int GetJointPositions(List<float> positions)
		{
			int jointPositions_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (positions != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(positions))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, positions.Count);
					}
				}
				jointPositions_Injected = ArticulationBody.GetJointPositions_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(positions);
			}
			return jointPositions_Injected;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003C0C File Offset: 0x00001E0C
		public unsafe void SetJointPositions(List<float> positions)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (positions != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(positions))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, positions.Count);
					}
				}
				ArticulationBody.SetJointPositions_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(positions);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003C80 File Offset: 0x00001E80
		public unsafe int GetJointVelocities(List<float> velocities)
		{
			int jointVelocities_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (velocities != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(velocities))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, velocities.Count);
					}
				}
				jointVelocities_Injected = ArticulationBody.GetJointVelocities_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(velocities);
			}
			return jointVelocities_Injected;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public unsafe void SetJointVelocities(List<float> velocities)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (velocities != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(velocities))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, velocities.Count);
					}
				}
				ArticulationBody.SetJointVelocities_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(velocities);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003D6C File Offset: 0x00001F6C
		public unsafe int GetJointAccelerations(List<float> accelerations)
		{
			int jointAccelerations_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (accelerations != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(accelerations))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, accelerations.Count);
					}
				}
				jointAccelerations_Injected = ArticulationBody.GetJointAccelerations_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(accelerations);
			}
			return jointAccelerations_Injected;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public unsafe int GetJointForces(List<float> forces)
		{
			int jointForces_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (forces != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(forces))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, forces.Count);
					}
				}
				jointForces_Injected = ArticulationBody.GetJointForces_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(forces);
			}
			return jointForces_Injected;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003E5C File Offset: 0x0000205C
		public unsafe void SetJointForces(List<float> forces)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (forces != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(forces))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, forces.Count);
					}
				}
				ArticulationBody.SetJointForces_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(forces);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003ED0 File Offset: 0x000020D0
		public ArticulationReducedSpace GetJointForcesForAcceleration(ArticulationReducedSpace acceleration)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationReducedSpace articulationReducedSpace;
			ArticulationBody.GetJointForcesForAcceleration_Injected(intPtr, ref acceleration, out articulationReducedSpace);
			return articulationReducedSpace;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003EF8 File Offset: 0x000020F8
		public unsafe int GetDriveForces(List<float> forces)
		{
			int driveForces_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (forces != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(forces))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, forces.Count);
					}
				}
				driveForces_Injected = ArticulationBody.GetDriveForces_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(forces);
			}
			return driveForces_Injected;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003F70 File Offset: 0x00002170
		public unsafe int GetJointGravityForces(List<float> forces)
		{
			int jointGravityForces_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (forces != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(forces))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, forces.Count);
					}
				}
				jointGravityForces_Injected = ArticulationBody.GetJointGravityForces_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(forces);
			}
			return jointGravityForces_Injected;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003FE8 File Offset: 0x000021E8
		public unsafe int GetJointCoriolisCentrifugalForces(List<float> forces)
		{
			int jointCoriolisCentrifugalForces_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (forces != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(forces))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, forces.Count);
					}
				}
				jointCoriolisCentrifugalForces_Injected = ArticulationBody.GetJointCoriolisCentrifugalForces_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(forces);
			}
			return jointCoriolisCentrifugalForces_Injected;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004060 File Offset: 0x00002260
		public unsafe int GetJointExternalForces(List<float> forces, float step)
		{
			int jointExternalForces_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (forces != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(forces))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, forces.Count);
					}
				}
				jointExternalForces_Injected = ArticulationBody.GetJointExternalForces_Injected(intPtr, ref blittableListWrapper, step);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(forces);
			}
			return jointExternalForces_Injected;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000040D8 File Offset: 0x000022D8
		public unsafe int GetDriveTargets(List<float> targets)
		{
			int driveTargets_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (targets != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(targets))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, targets.Count);
					}
				}
				driveTargets_Injected = ArticulationBody.GetDriveTargets_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(targets);
			}
			return driveTargets_Injected;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004150 File Offset: 0x00002350
		public unsafe void SetDriveTargets(List<float> targets)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (targets != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(targets))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, targets.Count);
					}
				}
				ArticulationBody.SetDriveTargets_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(targets);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000041C4 File Offset: 0x000023C4
		public unsafe int GetDriveTargetVelocities(List<float> targetVelocities)
		{
			int driveTargetVelocities_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (targetVelocities != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(targetVelocities))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, targetVelocities.Count);
					}
				}
				driveTargetVelocities_Injected = ArticulationBody.GetDriveTargetVelocities_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(targetVelocities);
			}
			return driveTargetVelocities_Injected;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000423C File Offset: 0x0000243C
		public unsafe void SetDriveTargetVelocities(List<float> targetVelocities)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (targetVelocities != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(targetVelocities))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, targetVelocities.Count);
					}
				}
				ArticulationBody.SetDriveTargetVelocities_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(targetVelocities);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000042B0 File Offset: 0x000024B0
		public unsafe int GetDofStartIndices(List<int> dofStartIndices)
		{
			int dofStartIndices_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (dofStartIndices != null)
				{
					fixed (int[] array = NoAllocHelpers.ExtractArrayFromList<int>(dofStartIndices))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, dofStartIndices.Count);
					}
				}
				dofStartIndices_Injected = ArticulationBody.GetDofStartIndices_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<int>(dofStartIndices);
			}
			return dofStartIndices_Injected;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004328 File Offset: 0x00002528
		public void SetDriveTarget(ArticulationDriveAxis axis, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.SetDriveTarget_Injected(intPtr, axis, value);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000434C File Offset: 0x0000254C
		public void SetDriveTargetVelocity(ArticulationDriveAxis axis, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.SetDriveTargetVelocity_Injected(intPtr, axis, value);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004370 File Offset: 0x00002570
		public void SetDriveLimits(ArticulationDriveAxis axis, float lower, float upper)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.SetDriveLimits_Injected(intPtr, axis, lower, upper);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004398 File Offset: 0x00002598
		public void SetDriveStiffness(ArticulationDriveAxis axis, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.SetDriveStiffness_Injected(intPtr, axis, value);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000043BC File Offset: 0x000025BC
		public void SetDriveDamping(ArticulationDriveAxis axis, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.SetDriveDamping_Injected(intPtr, axis, value);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000043E0 File Offset: 0x000025E0
		public void SetDriveForceLimit(ArticulationDriveAxis axis, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.SetDriveForceLimit_Injected(intPtr, axis, value);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004404 File Offset: 0x00002604
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00004428 File Offset: 0x00002628
		public CollisionDetectionMode collisionDetectionMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ArticulationBody.get_collisionDetectionMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ArticulationBody.set_collisionDetectionMode_Injected(intPtr, value);
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000444C File Offset: 0x0000264C
		public void PublishTransform()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ArticulationBody.PublishTransform_Injected(intPtr);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004470 File Offset: 0x00002670
		public void SnapAnchorToClosestContact()
		{
			bool flag = !base.transform.parent;
			if (!flag)
			{
				ArticulationBody articulationBody = base.transform.parent.GetComponentInParent<ArticulationBody>();
				while (articulationBody && !articulationBody.enabled)
				{
					articulationBody = articulationBody.transform.parent.GetComponentInParent<ArticulationBody>();
				}
				bool flag2 = !articulationBody;
				if (!flag2)
				{
					Vector3 worldCenterOfMass = articulationBody.worldCenterOfMass;
					Vector3 closestPoint = this.GetClosestPoint(worldCenterOfMass);
					this.anchorPosition = base.transform.InverseTransformPoint(closestPoint);
					this.anchorRotation = Quaternion.FromToRotation(Vector3.right, base.transform.InverseTransformDirection(worldCenterOfMass - closestPoint).normalized);
				}
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004539 File Offset: 0x00002739
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004541 File Offset: 0x00002741
		[Obsolete("Please use ArticulationBody.linearVelocity instead. (UnityUpgradable) -> linearVelocity")]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000454B File Offset: 0x0000274B
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00004553 File Offset: 0x00002753
		[Obsolete("computeParentAnchor has been renamed to matchAnchors (UnityUpgradable) -> matchAnchors")]
		public bool computeParentAnchor
		{
			get
			{
				return this.matchAnchors;
			}
			set
			{
				this.matchAnchors = value;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004560 File Offset: 0x00002760
		[Obsolete("Setting joint accelerations is not supported in forward kinematics. To have inverse dynamics take acceleration into account, use GetJointForcesForAcceleration instead", true)]
		public unsafe void SetJointAccelerations(List<float> accelerations)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ArticulationBody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (accelerations != null)
				{
					fixed (float[] array = NoAllocHelpers.ExtractArrayFromList<float>(accelerations))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, accelerations.Count);
					}
				}
				ArticulationBody.SetJointAccelerations_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<float>(accelerations);
			}
		}

		// Token: 0x060000EC RID: 236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ArticulationJointType get_jointType_Injected(IntPtr _unity_self);

		// Token: 0x060000ED RID: 237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_jointType_Injected(IntPtr _unity_self, ArticulationJointType value);

		// Token: 0x060000EE RID: 238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_anchorPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060000EF RID: 239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anchorPosition_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060000F0 RID: 240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_parentAnchorPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060000F1 RID: 241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_parentAnchorPosition_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060000F2 RID: 242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_anchorRotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x060000F3 RID: 243
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_anchorRotation_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x060000F4 RID: 244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_parentAnchorRotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x060000F5 RID: 245
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_parentAnchorRotation_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x060000F6 RID: 246
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isRoot_Injected(IntPtr _unity_self);

		// Token: 0x060000F7 RID: 247
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_matchAnchors_Injected(IntPtr _unity_self);

		// Token: 0x060000F8 RID: 248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_matchAnchors_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000F9 RID: 249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ArticulationDofLock get_linearLockX_Injected(IntPtr _unity_self);

		// Token: 0x060000FA RID: 250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearLockX_Injected(IntPtr _unity_self, ArticulationDofLock value);

		// Token: 0x060000FB RID: 251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ArticulationDofLock get_linearLockY_Injected(IntPtr _unity_self);

		// Token: 0x060000FC RID: 252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearLockY_Injected(IntPtr _unity_self, ArticulationDofLock value);

		// Token: 0x060000FD RID: 253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ArticulationDofLock get_linearLockZ_Injected(IntPtr _unity_self);

		// Token: 0x060000FE RID: 254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearLockZ_Injected(IntPtr _unity_self, ArticulationDofLock value);

		// Token: 0x060000FF RID: 255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ArticulationDofLock get_swingYLock_Injected(IntPtr _unity_self);

		// Token: 0x06000100 RID: 256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_swingYLock_Injected(IntPtr _unity_self, ArticulationDofLock value);

		// Token: 0x06000101 RID: 257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ArticulationDofLock get_swingZLock_Injected(IntPtr _unity_self);

		// Token: 0x06000102 RID: 258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_swingZLock_Injected(IntPtr _unity_self, ArticulationDofLock value);

		// Token: 0x06000103 RID: 259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ArticulationDofLock get_twistLock_Injected(IntPtr _unity_self);

		// Token: 0x06000104 RID: 260
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_twistLock_Injected(IntPtr _unity_self, ArticulationDofLock value);

		// Token: 0x06000105 RID: 261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_xDrive_Injected(IntPtr _unity_self, out ArticulationDrive ret);

		// Token: 0x06000106 RID: 262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_xDrive_Injected(IntPtr _unity_self, [In] ref ArticulationDrive value);

		// Token: 0x06000107 RID: 263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_yDrive_Injected(IntPtr _unity_self, out ArticulationDrive ret);

		// Token: 0x06000108 RID: 264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_yDrive_Injected(IntPtr _unity_self, [In] ref ArticulationDrive value);

		// Token: 0x06000109 RID: 265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_zDrive_Injected(IntPtr _unity_self, out ArticulationDrive ret);

		// Token: 0x0600010A RID: 266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_zDrive_Injected(IntPtr _unity_self, [In] ref ArticulationDrive value);

		// Token: 0x0600010B RID: 267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_immovable_Injected(IntPtr _unity_self);

		// Token: 0x0600010C RID: 268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_immovable_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600010D RID: 269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useGravity_Injected(IntPtr _unity_self);

		// Token: 0x0600010E RID: 270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useGravity_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600010F RID: 271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_linearDamping_Injected(IntPtr _unity_self);

		// Token: 0x06000110 RID: 272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearDamping_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000111 RID: 273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_angularDamping_Injected(IntPtr _unity_self);

		// Token: 0x06000112 RID: 274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularDamping_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000113 RID: 275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_jointFriction_Injected(IntPtr _unity_self);

		// Token: 0x06000114 RID: 276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_jointFriction_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000115 RID: 277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_excludeLayers_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x06000116 RID: 278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_excludeLayers_Injected(IntPtr _unity_self, [In] ref LayerMask value);

		// Token: 0x06000117 RID: 279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_includeLayers_Injected(IntPtr _unity_self, out LayerMask ret);

		// Token: 0x06000118 RID: 280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_includeLayers_Injected(IntPtr _unity_self, [In] ref LayerMask value);

		// Token: 0x06000119 RID: 281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAccumulatedForce_Injected(IntPtr _unity_self, [DefaultValue("Time.fixedDeltaTime")] float step, out Vector3 ret);

		// Token: 0x0600011A RID: 282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAccumulatedTorque_Injected(IntPtr _unity_self, [DefaultValue("Time.fixedDeltaTime")] float step, out Vector3 ret);

		// Token: 0x0600011B RID: 283
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddForce_Injected(IntPtr _unity_self, [In] ref Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x0600011C RID: 284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddRelativeForce_Injected(IntPtr _unity_self, [In] ref Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x0600011D RID: 285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddTorque_Injected(IntPtr _unity_self, [In] ref Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x0600011E RID: 286
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddRelativeTorque_Injected(IntPtr _unity_self, [In] ref Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x0600011F RID: 287
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddForceAtPosition_Injected(IntPtr _unity_self, [In] ref Vector3 force, [In] ref Vector3 position, [DefaultValue("ForceMode.Force")] ForceMode mode);

		// Token: 0x06000120 RID: 288
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_linearVelocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000121 RID: 289
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearVelocity_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000122 RID: 290
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_angularVelocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000123 RID: 291
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularVelocity_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000124 RID: 292
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_mass_Injected(IntPtr _unity_self);

		// Token: 0x06000125 RID: 293
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mass_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000126 RID: 294
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_automaticCenterOfMass_Injected(IntPtr _unity_self);

		// Token: 0x06000127 RID: 295
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_automaticCenterOfMass_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000128 RID: 296
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_centerOfMass_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000129 RID: 297
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_centerOfMass_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600012A RID: 298
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_worldCenterOfMass_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600012B RID: 299
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_automaticInertiaTensor_Injected(IntPtr _unity_self);

		// Token: 0x0600012C RID: 300
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_automaticInertiaTensor_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600012D RID: 301
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_inertiaTensor_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600012E RID: 302
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_inertiaTensor_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600012F RID: 303
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_worldInertiaTensorMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x06000130 RID: 304
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_inertiaTensorRotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x06000131 RID: 305
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_inertiaTensorRotation_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x06000132 RID: 306
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetCenterOfMass_Injected(IntPtr _unity_self);

		// Token: 0x06000133 RID: 307
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetInertiaTensor_Injected(IntPtr _unity_self);

		// Token: 0x06000134 RID: 308
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Sleep_Injected(IntPtr _unity_self);

		// Token: 0x06000135 RID: 309
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsSleeping_Injected(IntPtr _unity_self);

		// Token: 0x06000136 RID: 310
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WakeUp_Injected(IntPtr _unity_self);

		// Token: 0x06000137 RID: 311
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_sleepThreshold_Injected(IntPtr _unity_self);

		// Token: 0x06000138 RID: 312
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sleepThreshold_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000139 RID: 313
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_solverIterations_Injected(IntPtr _unity_self);

		// Token: 0x0600013A RID: 314
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_solverIterations_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600013B RID: 315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_solverVelocityIterations_Injected(IntPtr _unity_self);

		// Token: 0x0600013C RID: 316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_solverVelocityIterations_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600013D RID: 317
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxAngularVelocity_Injected(IntPtr _unity_self);

		// Token: 0x0600013E RID: 318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxAngularVelocity_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600013F RID: 319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxLinearVelocity_Injected(IntPtr _unity_self);

		// Token: 0x06000140 RID: 320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxLinearVelocity_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000141 RID: 321
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxJointVelocity_Injected(IntPtr _unity_self);

		// Token: 0x06000142 RID: 322
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxJointVelocity_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000143 RID: 323
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxDepenetrationVelocity_Injected(IntPtr _unity_self);

		// Token: 0x06000144 RID: 324
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxDepenetrationVelocity_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000145 RID: 325
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_jointPosition_Injected(IntPtr _unity_self, out ArticulationReducedSpace ret);

		// Token: 0x06000146 RID: 326
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_jointPosition_Injected(IntPtr _unity_self, [In] ref ArticulationReducedSpace value);

		// Token: 0x06000147 RID: 327
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_jointVelocity_Injected(IntPtr _unity_self, out ArticulationReducedSpace ret);

		// Token: 0x06000148 RID: 328
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_jointVelocity_Injected(IntPtr _unity_self, [In] ref ArticulationReducedSpace value);

		// Token: 0x06000149 RID: 329
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_jointAcceleration_Injected(IntPtr _unity_self, out ArticulationReducedSpace ret);

		// Token: 0x0600014A RID: 330
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_jointAcceleration_Injected(IntPtr _unity_self, [In] ref ArticulationReducedSpace value);

		// Token: 0x0600014B RID: 331
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_jointForce_Injected(IntPtr _unity_self, out ArticulationReducedSpace ret);

		// Token: 0x0600014C RID: 332
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_jointForce_Injected(IntPtr _unity_self, [In] ref ArticulationReducedSpace value);

		// Token: 0x0600014D RID: 333
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_driveForce_Injected(IntPtr _unity_self, out ArticulationReducedSpace ret);

		// Token: 0x0600014E RID: 334
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_dofCount_Injected(IntPtr _unity_self);

		// Token: 0x0600014F RID: 335
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_index_Injected(IntPtr _unity_self);

		// Token: 0x06000150 RID: 336
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TeleportRoot_Injected(IntPtr _unity_self, [In] ref Vector3 position, [In] ref Quaternion rotation);

		// Token: 0x06000151 RID: 337
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetClosestPoint_Injected(IntPtr _unity_self, [In] ref Vector3 point, out Vector3 ret);

		// Token: 0x06000152 RID: 338
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRelativePointVelocity_Injected(IntPtr _unity_self, [In] ref Vector3 relativePoint, out Vector3 ret);

		// Token: 0x06000153 RID: 339
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPointVelocity_Injected(IntPtr _unity_self, [In] ref Vector3 worldPoint, out Vector3 ret);

		// Token: 0x06000154 RID: 340
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDenseJacobian_Internal_Injected(IntPtr _unity_self, ref ArticulationJacobian jacobian);

		// Token: 0x06000155 RID: 341
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetJointPositions_Injected(IntPtr _unity_self, ref BlittableListWrapper positions);

		// Token: 0x06000156 RID: 342
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetJointPositions_Injected(IntPtr _unity_self, ref BlittableListWrapper positions);

		// Token: 0x06000157 RID: 343
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetJointVelocities_Injected(IntPtr _unity_self, ref BlittableListWrapper velocities);

		// Token: 0x06000158 RID: 344
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetJointVelocities_Injected(IntPtr _unity_self, ref BlittableListWrapper velocities);

		// Token: 0x06000159 RID: 345
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetJointAccelerations_Injected(IntPtr _unity_self, ref BlittableListWrapper accelerations);

		// Token: 0x0600015A RID: 346
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetJointForces_Injected(IntPtr _unity_self, ref BlittableListWrapper forces);

		// Token: 0x0600015B RID: 347
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetJointForces_Injected(IntPtr _unity_self, ref BlittableListWrapper forces);

		// Token: 0x0600015C RID: 348
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetJointForcesForAcceleration_Injected(IntPtr _unity_self, [In] ref ArticulationReducedSpace acceleration, out ArticulationReducedSpace ret);

		// Token: 0x0600015D RID: 349
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDriveForces_Injected(IntPtr _unity_self, ref BlittableListWrapper forces);

		// Token: 0x0600015E RID: 350
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetJointGravityForces_Injected(IntPtr _unity_self, ref BlittableListWrapper forces);

		// Token: 0x0600015F RID: 351
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetJointCoriolisCentrifugalForces_Injected(IntPtr _unity_self, ref BlittableListWrapper forces);

		// Token: 0x06000160 RID: 352
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetJointExternalForces_Injected(IntPtr _unity_self, ref BlittableListWrapper forces, float step);

		// Token: 0x06000161 RID: 353
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDriveTargets_Injected(IntPtr _unity_self, ref BlittableListWrapper targets);

		// Token: 0x06000162 RID: 354
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDriveTargets_Injected(IntPtr _unity_self, ref BlittableListWrapper targets);

		// Token: 0x06000163 RID: 355
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDriveTargetVelocities_Injected(IntPtr _unity_self, ref BlittableListWrapper targetVelocities);

		// Token: 0x06000164 RID: 356
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDriveTargetVelocities_Injected(IntPtr _unity_self, ref BlittableListWrapper targetVelocities);

		// Token: 0x06000165 RID: 357
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDofStartIndices_Injected(IntPtr _unity_self, ref BlittableListWrapper dofStartIndices);

		// Token: 0x06000166 RID: 358
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDriveTarget_Injected(IntPtr _unity_self, ArticulationDriveAxis axis, float value);

		// Token: 0x06000167 RID: 359
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDriveTargetVelocity_Injected(IntPtr _unity_self, ArticulationDriveAxis axis, float value);

		// Token: 0x06000168 RID: 360
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDriveLimits_Injected(IntPtr _unity_self, ArticulationDriveAxis axis, float lower, float upper);

		// Token: 0x06000169 RID: 361
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDriveStiffness_Injected(IntPtr _unity_self, ArticulationDriveAxis axis, float value);

		// Token: 0x0600016A RID: 362
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDriveDamping_Injected(IntPtr _unity_self, ArticulationDriveAxis axis, float value);

		// Token: 0x0600016B RID: 363
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDriveForceLimit_Injected(IntPtr _unity_self, ArticulationDriveAxis axis, float value);

		// Token: 0x0600016C RID: 364
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CollisionDetectionMode get_collisionDetectionMode_Injected(IntPtr _unity_self);

		// Token: 0x0600016D RID: 365
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_collisionDetectionMode_Injected(IntPtr _unity_self, CollisionDetectionMode value);

		// Token: 0x0600016E RID: 366
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PublishTransform_Injected(IntPtr _unity_self);

		// Token: 0x0600016F RID: 367
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetJointAccelerations_Injected(IntPtr _unity_self, ref BlittableListWrapper accelerations);
	}
}
