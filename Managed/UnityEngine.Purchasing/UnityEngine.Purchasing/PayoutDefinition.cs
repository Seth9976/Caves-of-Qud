using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200001D RID: 29
	[Serializable]
	public class PayoutDefinition
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000025F6 File Offset: 0x000007F6
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000025FE File Offset: 0x000007FE
		public PayoutType type
		{
			get
			{
				return this.m_Type;
			}
			private set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002607 File Offset: 0x00000807
		public string typeString
		{
			get
			{
				return this.m_Type.ToString();
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000261A File Offset: 0x0000081A
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002622 File Offset: 0x00000822
		public string subtype
		{
			get
			{
				return this.m_Subtype;
			}
			private set
			{
				if (value.Length > 64)
				{
					throw new ArgumentException(string.Format("subtype cannot be longer than {0}", 64));
				}
				this.m_Subtype = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000264C File Offset: 0x0000084C
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002654 File Offset: 0x00000854
		public double quantity
		{
			get
			{
				return this.m_Quantity;
			}
			private set
			{
				this.m_Quantity = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000265D File Offset: 0x0000085D
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002665 File Offset: 0x00000865
		public string data
		{
			get
			{
				return this.m_Data;
			}
			private set
			{
				if (value.Length > 1024)
				{
					throw new ArgumentException(string.Format("data cannot be longer than {0}", 1024));
				}
				this.m_Data = value;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002695 File Offset: 0x00000895
		public PayoutDefinition()
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000026B3 File Offset: 0x000008B3
		public PayoutDefinition(string typeString, string subtype, double quantity)
			: this(typeString, subtype, quantity, string.Empty)
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000026C4 File Offset: 0x000008C4
		public PayoutDefinition(string typeString, string subtype, double quantity, string data)
		{
			PayoutType payoutType = PayoutType.Other;
			if (Enum.IsDefined(typeof(PayoutType), typeString))
			{
				payoutType = (PayoutType)Enum.Parse(typeof(PayoutType), typeString);
			}
			this.type = payoutType;
			this.subtype = subtype;
			this.quantity = quantity;
			this.data = data;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002734 File Offset: 0x00000934
		public PayoutDefinition(string subtype, double quantity)
			: this(PayoutType.Other, subtype, quantity, string.Empty)
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002744 File Offset: 0x00000944
		public PayoutDefinition(string subtype, double quantity, string data)
			: this(PayoutType.Other, subtype, quantity, data)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002750 File Offset: 0x00000950
		public PayoutDefinition(PayoutType type, string subtype, double quantity)
			: this(type, subtype, quantity, string.Empty)
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002760 File Offset: 0x00000960
		public PayoutDefinition(PayoutType type, string subtype, double quantity, string data)
		{
			this.type = type;
			this.subtype = subtype;
			this.quantity = quantity;
			this.data = data;
		}

		// Token: 0x0400001A RID: 26
		[SerializeField]
		private PayoutType m_Type;

		// Token: 0x0400001B RID: 27
		[SerializeField]
		private string m_Subtype = string.Empty;

		// Token: 0x0400001C RID: 28
		[SerializeField]
		private double m_Quantity;

		// Token: 0x0400001D RID: 29
		[SerializeField]
		private string m_Data = string.Empty;

		// Token: 0x0400001E RID: 30
		public const int MaxSubtypeLength = 64;

		// Token: 0x0400001F RID: 31
		public const int MaxDataLength = 1024;
	}
}
