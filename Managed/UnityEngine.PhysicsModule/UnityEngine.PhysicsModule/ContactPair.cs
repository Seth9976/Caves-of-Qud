using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200003A RID: 58
	[UsedByNativeCode]
	public readonly struct ContactPair
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00008999 File Offset: 0x00006B99
		public int colliderInstanceID
		{
			get
			{
				return this.m_ColliderID;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x000089A1 File Offset: 0x00006BA1
		public int otherColliderInstanceID
		{
			get
			{
				return this.m_OtherColliderID;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x000089A9 File Offset: 0x00006BA9
		public Collider collider
		{
			get
			{
				return (this.m_ColliderID == 0) ? null : Physics.GetColliderByInstanceID(this.m_ColliderID);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000089C1 File Offset: 0x00006BC1
		public Collider otherCollider
		{
			get
			{
				return (this.m_OtherColliderID == 0) ? null : Physics.GetColliderByInstanceID(this.m_OtherColliderID);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x000089D9 File Offset: 0x00006BD9
		public int contactCount
		{
			get
			{
				return (int)this.m_NbPoints;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x000089E1 File Offset: 0x00006BE1
		public Vector3 impulseSum
		{
			get
			{
				return this.m_ImpulseSum;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x000089E9 File Offset: 0x00006BE9
		public bool isCollisionEnter
		{
			get
			{
				return (this.m_Events & CollisionPairEventFlags.NotifyTouchFound) > (CollisionPairEventFlags)0;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x000089F6 File Offset: 0x00006BF6
		public bool isCollisionExit
		{
			get
			{
				return (this.m_Events & CollisionPairEventFlags.NotifyTouchLost) > (CollisionPairEventFlags)0;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00008A04 File Offset: 0x00006C04
		public bool isCollisionStay
		{
			get
			{
				return (this.m_Events & CollisionPairEventFlags.NotifyTouchPersists) > (CollisionPairEventFlags)0;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00008A11 File Offset: 0x00006C11
		internal bool hasRemovedCollider
		{
			get
			{
				return (this.m_Flags & CollisionPairFlags.RemovedShape) != (CollisionPairFlags)0 || (this.m_Flags & CollisionPairFlags.RemovedOtherShape) > (CollisionPairFlags)0;
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00008A2C File Offset: 0x00006C2C
		internal int ExtractContacts(List<ContactPoint> managedContainer, bool flipped)
		{
			int num = (int)Math.Min((long)managedContainer.Capacity, (long)((ulong)this.m_NbPoints));
			managedContainer.Clear();
			for (int i = 0; i < num; i++)
			{
				readonly ref ContactPairPoint contactPoint = ref this.GetContactPoint(i);
				ContactPoint contactPoint2 = new ContactPoint
				{
					m_Point = contactPoint.position,
					m_Impulse = contactPoint.impulse,
					m_Separation = contactPoint.separation
				};
				if (flipped)
				{
					contactPoint2.m_Normal = -contactPoint.normal;
					contactPoint2.m_ThisColliderInstanceID = this.m_OtherColliderID;
					contactPoint2.m_OtherColliderInstanceID = this.m_ColliderID;
				}
				else
				{
					contactPoint2.m_Normal = contactPoint.normal;
					contactPoint2.m_ThisColliderInstanceID = this.m_ColliderID;
					contactPoint2.m_OtherColliderInstanceID = this.m_OtherColliderID;
				}
				managedContainer.Add(contactPoint2);
			}
			return num;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00008B1C File Offset: 0x00006D1C
		internal int ExtractContactsArray(ContactPoint[] managedContainer, bool flipped)
		{
			int num = (int)Math.Min((long)managedContainer.Length, (long)((ulong)this.m_NbPoints));
			for (int i = 0; i < num; i++)
			{
				readonly ref ContactPairPoint contactPoint = ref this.GetContactPoint(i);
				ContactPoint contactPoint2 = new ContactPoint
				{
					m_Point = contactPoint.position,
					m_Impulse = contactPoint.impulse,
					m_Separation = contactPoint.separation
				};
				if (flipped)
				{
					contactPoint2.m_Normal = -contactPoint.normal;
					contactPoint2.m_ThisColliderInstanceID = this.m_OtherColliderID;
					contactPoint2.m_OtherColliderInstanceID = this.m_ColliderID;
				}
				else
				{
					contactPoint2.m_Normal = contactPoint.normal;
					contactPoint2.m_ThisColliderInstanceID = this.m_ColliderID;
					contactPoint2.m_OtherColliderInstanceID = this.m_OtherColliderID;
				}
				managedContainer[i] = contactPoint2;
			}
			return num;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00008C00 File Offset: 0x00006E00
		public unsafe void CopyToNativeArray(NativeArray<ContactPairPoint> buffer)
		{
			int num = Mathf.Min(buffer.Length, this.contactCount);
			for (int i = 0; i < num; i++)
			{
				buffer[i] = *this.GetContactPoint(i);
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00008C48 File Offset: 0x00006E48
		public readonly ref ContactPairPoint GetContactPoint(int index)
		{
			return this.GetContactPoint_Internal(index);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00008C64 File Offset: 0x00006E64
		public unsafe uint GetContactPointFaceIndex(int contactIndex)
		{
			uint internalFaceIndex = this.GetContactPoint_Internal(contactIndex)->m_InternalFaceIndex0;
			uint internalFaceIndex2 = this.GetContactPoint_Internal(contactIndex)->m_InternalFaceIndex1;
			bool flag = internalFaceIndex != uint.MaxValue;
			uint num;
			if (flag)
			{
				num = Physics.TranslateTriangleIndexFromID(this.m_ColliderID, internalFaceIndex);
			}
			else
			{
				bool flag2 = internalFaceIndex2 != uint.MaxValue;
				if (flag2)
				{
					num = Physics.TranslateTriangleIndexFromID(this.m_OtherColliderID, internalFaceIndex2);
				}
				else
				{
					num = uint.MaxValue;
				}
			}
			return num;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00008CC8 File Offset: 0x00006EC8
		internal unsafe ContactPairPoint* GetContactPoint_Internal(int index)
		{
			bool flag = (long)index >= (long)((ulong)this.m_NbPoints);
			if (flag)
			{
				throw new IndexOutOfRangeException("Invalid ContactPairPoint index. Index should be greater than 0 and less than ContactPair.ContactCount");
			}
			return this.m_StartPtr.ToInt64() / (long)sizeof(ContactPairPoint) + index * sizeof(ContactPairPoint);
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00008D0D File Offset: 0x00006F0D
		[Obsolete("Please use ContactPair.colliderInstanceID instead. (UnityUpgradable) -> colliderInstanceID", false)]
		public int ColliderInstanceID
		{
			get
			{
				return this.colliderInstanceID;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00008D15 File Offset: 0x00006F15
		[Obsolete("Please use ContactPair.otherColliderInstanceID instead. (UnityUpgradable) -> otherColliderInstanceID", false)]
		public int OtherColliderInstanceID
		{
			get
			{
				return this.otherColliderInstanceID;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00008D1D File Offset: 0x00006F1D
		[Obsolete("Please use ContactPair.collider instead. (UnityUpgradable) -> collider", false)]
		public Collider Collider
		{
			get
			{
				return this.collider;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00008D25 File Offset: 0x00006F25
		[Obsolete("Please use ContactPair.otherCollider instead. (UnityUpgradable) -> otherCollider", false)]
		public Collider OtherCollider
		{
			get
			{
				return this.otherCollider;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00008D2D File Offset: 0x00006F2D
		[Obsolete("Please use ContactPair.contactCount instead. (UnityUpgradable) -> contactCount", false)]
		public int ContactCount
		{
			get
			{
				return this.contactCount;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00008D35 File Offset: 0x00006F35
		[Obsolete("Please use ContactPair.impulseSum instead. (UnityUpgradable) -> impulseSum", false)]
		public Vector3 ImpulseSum
		{
			get
			{
				return this.impulseSum;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00008D3D File Offset: 0x00006F3D
		[Obsolete("Please use ContactPair.isCollisionEnter instead. (UnityUpgradable) -> isCollisionEnter", false)]
		public bool IsCollisionEnter
		{
			get
			{
				return this.isCollisionEnter;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00008D45 File Offset: 0x00006F45
		[Obsolete("Please use ContactPair.isCollisionExit instead. (UnityUpgradable) -> isCollisionExit", false)]
		public bool IsCollisionExit
		{
			get
			{
				return this.isCollisionExit;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00008D4D File Offset: 0x00006F4D
		[Obsolete("Please use ContactPair.isCollisionStay instead. (UnityUpgradable) -> isCollisionStay", false)]
		public bool IsCollisionStay
		{
			get
			{
				return this.isCollisionStay;
			}
		}

		// Token: 0x040000DC RID: 220
		private const uint c_InvalidFaceIndex = 4294967295U;

		// Token: 0x040000DD RID: 221
		internal readonly int m_ColliderID;

		// Token: 0x040000DE RID: 222
		internal readonly int m_OtherColliderID;

		// Token: 0x040000DF RID: 223
		internal readonly IntPtr m_StartPtr;

		// Token: 0x040000E0 RID: 224
		internal readonly uint m_NbPoints;

		// Token: 0x040000E1 RID: 225
		internal readonly CollisionPairFlags m_Flags;

		// Token: 0x040000E2 RID: 226
		internal readonly CollisionPairEventFlags m_Events;

		// Token: 0x040000E3 RID: 227
		internal readonly Vector3 m_ImpulseSum;
	}
}
