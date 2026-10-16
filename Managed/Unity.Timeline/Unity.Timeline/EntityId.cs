using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	internal struct EntityId : IComparable<EntityId>
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00008CBB File Offset: 0x00006EBB
		public static implicit operator int(EntityId entityId)
		{
			return entityId.m_Value;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008CC4 File Offset: 0x00006EC4
		public static implicit operator EntityId(int instanceId)
		{
			return new EntityId
			{
				m_Value = instanceId
			};
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008CE2 File Offset: 0x00006EE2
		internal static EntityId AllocateNextLowestEntityId()
		{
			return 1;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008CEA File Offset: 0x00006EEA
		public int CompareTo(EntityId other)
		{
			return this.m_Value.CompareTo(other.m_Value);
		}

		// Token: 0x040000D3 RID: 211
		public int m_Value;

		// Token: 0x040000D4 RID: 212
		public static readonly EntityId None = new EntityId
		{
			m_Value = -1
		};
	}
}
