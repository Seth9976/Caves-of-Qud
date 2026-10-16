using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Timeline
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	internal struct ObjectId : IEquatable<ObjectId>, IComparable<ObjectId>
	{
		// Token: 0x06000270 RID: 624 RVA: 0x00008D23 File Offset: 0x00006F23
		internal ObjectId(EntityId data)
		{
			this.m_IntData = 0;
			this.m_Data = data;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00008D34 File Offset: 0x00006F34
		public static implicit operator ObjectId(EntityId entityId)
		{
			return new ObjectId
			{
				m_Data = entityId
			};
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008D52 File Offset: 0x00006F52
		public static implicit operator EntityId(ObjectId objectId)
		{
			return objectId.m_Data;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008D5A File Offset: 0x00006F5A
		public static implicit operator int(ObjectId objectId)
		{
			return objectId.m_IntData;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00008D64 File Offset: 0x00006F64
		public static implicit operator ObjectId(int instanceId)
		{
			return new ObjectId
			{
				m_Data = EntityId.None,
				m_IntData = instanceId
			};
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00008D90 File Offset: 0x00006F90
		public override bool Equals(object obj)
		{
			if (obj is ObjectId)
			{
				ObjectId objectId = (ObjectId)obj;
				return this.Equals(objectId);
			}
			return false;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00008DB5 File Offset: 0x00006FB5
		public bool Equals(ObjectId other)
		{
			return this.m_Data == other.m_Data;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00008DCF File Offset: 0x00006FCF
		public int CompareTo(ObjectId other)
		{
			return this.m_Data.CompareTo(other.m_Data);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00008DE2 File Offset: 0x00006FE2
		public static bool operator ==(ObjectId left, ObjectId right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00008DEC File Offset: 0x00006FEC
		public static bool operator !=(ObjectId left, ObjectId right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00008DF9 File Offset: 0x00006FF9
		public static bool operator <(ObjectId left, ObjectId right)
		{
			return left.m_Data < right.m_Data;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00008E13 File Offset: 0x00007013
		public static bool operator >(ObjectId left, ObjectId right)
		{
			return left.m_Data > right.m_Data;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00008E2D File Offset: 0x0000702D
		public static bool operator <=(ObjectId left, ObjectId right)
		{
			return left.m_Data <= right.m_Data;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00008E4A File Offset: 0x0000704A
		public static bool operator >=(ObjectId left, ObjectId right)
		{
			return left.m_Data >= right.m_Data;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00008E67 File Offset: 0x00007067
		public override int GetHashCode()
		{
			return this.m_Data.GetHashCode();
		}

		// Token: 0x040000D5 RID: 213
		[SerializeField]
		[FieldOffset(0)]
		private EntityId m_Data;

		// Token: 0x040000D6 RID: 214
		[FieldOffset(0)]
		private int m_IntData;

		// Token: 0x040000D7 RID: 215
		public static readonly ObjectId InvalidId = new ObjectId(EntityId.None);
	}
}
