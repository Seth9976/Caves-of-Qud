using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200001F RID: 31
	public class Product
	{
		// Token: 0x0600007C RID: 124 RVA: 0x0000279B File Offset: 0x0000099B
		internal Product(ProductDefinition definition, ProductMetadata metadata, string receipt)
		{
			this.definition = definition;
			this.metadata = metadata;
			this.receipt = receipt;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000027B8 File Offset: 0x000009B8
		internal Product(ProductDefinition definition, ProductMetadata metadata)
			: this(definition, metadata, null)
		{
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000027C3 File Offset: 0x000009C3
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000027CB File Offset: 0x000009CB
		public ProductDefinition definition { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000027D4 File Offset: 0x000009D4
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000027DC File Offset: 0x000009DC
		public ProductMetadata metadata { get; internal set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000027E5 File Offset: 0x000009E5
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000027ED File Offset: 0x000009ED
		public bool availableToPurchase { get; internal set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000027F6 File Offset: 0x000009F6
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000027FE File Offset: 0x000009FE
		public string transactionID { get; internal set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002807 File Offset: 0x00000A07
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000280F File Offset: 0x00000A0F
		public string appleOriginalTransactionID { get; internal set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002818 File Offset: 0x00000A18
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002820 File Offset: 0x00000A20
		public bool appleProductIsRestored { get; internal set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002829 File Offset: 0x00000A29
		public bool hasReceipt
		{
			get
			{
				return !string.IsNullOrEmpty(this.receipt);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002839 File Offset: 0x00000A39
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002841 File Offset: 0x00000A41
		public string receipt { get; internal set; }

		// Token: 0x0600008D RID: 141 RVA: 0x0000284C File Offset: 0x00000A4C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Product product = obj as Product;
			return product != null && this.definition.Equals(product.definition);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000287B File Offset: 0x00000A7B
		public override int GetHashCode()
		{
			return this.definition.GetHashCode();
		}
	}
}
