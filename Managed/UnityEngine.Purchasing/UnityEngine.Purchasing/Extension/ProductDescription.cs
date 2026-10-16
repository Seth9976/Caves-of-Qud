using System;

namespace UnityEngine.Purchasing.Extension
{
	// Token: 0x0200004C RID: 76
	public class ProductDescription
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00004161 File Offset: 0x00002361
		public ProductDescription(string id, ProductMetadata metadata, string receipt, string transactionId)
		{
			this.storeSpecificId = id;
			this.metadata = metadata;
			this.receipt = receipt;
			this.transactionId = transactionId;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004186 File Offset: 0x00002386
		public ProductDescription(string id, ProductMetadata metadata, string receipt, string transactionId, ProductType type)
			: this(id, metadata, receipt, transactionId)
		{
			this.type = type;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000419B File Offset: 0x0000239B
		public ProductDescription(string id, ProductMetadata metadata)
			: this(id, metadata, null, null)
		{
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000041A7 File Offset: 0x000023A7
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000041AF File Offset: 0x000023AF
		public string storeSpecificId { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000041B8 File Offset: 0x000023B8
		// (set) Token: 0x06000154 RID: 340 RVA: 0x000041C0 File Offset: 0x000023C0
		public ProductMetadata metadata { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000041C9 File Offset: 0x000023C9
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000041D1 File Offset: 0x000023D1
		public string receipt { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000041DA File Offset: 0x000023DA
		// (set) Token: 0x06000158 RID: 344 RVA: 0x000041E2 File Offset: 0x000023E2
		public string transactionId { get; set; }

		// Token: 0x0400009F RID: 159
		public ProductType type;
	}
}
