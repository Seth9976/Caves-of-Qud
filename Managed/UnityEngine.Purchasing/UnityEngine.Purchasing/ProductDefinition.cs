using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000021 RID: 33
	public class ProductDefinition
	{
		// Token: 0x06000096 RID: 150 RVA: 0x0000298A File Offset: 0x00000B8A
		private ProductDefinition()
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000299D File Offset: 0x00000B9D
		public ProductDefinition(string id, string storeSpecificId, ProductType type)
			: this(id, storeSpecificId, type, true)
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000029A9 File Offset: 0x00000BA9
		public ProductDefinition(string id, string storeSpecificId, ProductType type, bool enabled)
			: this(id, storeSpecificId, type, enabled, null)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000029B7 File Offset: 0x00000BB7
		public ProductDefinition(string id, string storeSpecificId, ProductType type, bool enabled, PayoutDefinition payout)
			: this(id, storeSpecificId, type, enabled, new List<PayoutDefinition> { payout })
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000029D1 File Offset: 0x00000BD1
		public ProductDefinition(string id, string storeSpecificId, ProductType type, bool enabled, IEnumerable<PayoutDefinition> payouts)
		{
			this.id = id;
			this.storeSpecificId = storeSpecificId;
			this.type = type;
			this.enabled = enabled;
			this.SetPayouts(payouts);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002A09 File Offset: 0x00000C09
		public ProductDefinition(string id, ProductType type)
			: this(id, id, type)
		{
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002A14 File Offset: 0x00000C14
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00002A1C File Offset: 0x00000C1C
		public string id { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002A25 File Offset: 0x00000C25
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00002A2D File Offset: 0x00000C2D
		public string storeSpecificId { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002A36 File Offset: 0x00000C36
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00002A3E File Offset: 0x00000C3E
		public ProductType type { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002A47 File Offset: 0x00000C47
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00002A4F File Offset: 0x00000C4F
		public bool enabled { get; private set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x00002A58 File Offset: 0x00000C58
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			ProductDefinition productDefinition = obj as ProductDefinition;
			return productDefinition != null && this.id == productDefinition.id;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002A87 File Offset: 0x00000C87
		public override int GetHashCode()
		{
			return this.id.GetHashCode();
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002A94 File Offset: 0x00000C94
		public IEnumerable<PayoutDefinition> payouts
		{
			get
			{
				return this.m_Payouts;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002A9C File Offset: 0x00000C9C
		public PayoutDefinition payout
		{
			get
			{
				if (this.m_Payouts.Count <= 0)
				{
					return null;
				}
				return this.m_Payouts[0];
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002ABA File Offset: 0x00000CBA
		internal void SetPayouts(IEnumerable<PayoutDefinition> newPayouts)
		{
			if (newPayouts == null)
			{
				return;
			}
			this.m_Payouts.Clear();
			this.m_Payouts.AddRange(newPayouts);
		}

		// Token: 0x04000034 RID: 52
		private readonly List<PayoutDefinition> m_Payouts = new List<PayoutDefinition>();
	}
}
