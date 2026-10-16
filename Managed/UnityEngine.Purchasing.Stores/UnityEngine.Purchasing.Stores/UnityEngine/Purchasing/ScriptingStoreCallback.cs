using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Uniject;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000043 RID: 67
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class ScriptingStoreCallback : IStoreCallback
	{
		// Token: 0x0600015F RID: 351 RVA: 0x0000554F File Offset: 0x0000374F
		public ScriptingStoreCallback(IStoreCallback forwardTo, IUtil util)
		{
			this.m_ForwardTo = forwardTo;
			this.m_Util = util;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005565 File Offset: 0x00003765
		public ProductCollection products
		{
			get
			{
				return this.m_ForwardTo.products;
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005574 File Offset: 0x00003774
		[Obsolete]
		public void OnSetupFailed(InitializationFailureReason reason)
		{
			this.m_Util.RunOnMainThread(delegate
			{
				this.m_ForwardTo.OnSetupFailed(reason, null);
			});
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000055AC File Offset: 0x000037AC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public void OnSetupFailed(InitializationFailureReason reason, string message)
		{
			this.m_Util.RunOnMainThread(delegate
			{
				this.m_ForwardTo.OnSetupFailed(reason, message);
			});
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000055EC File Offset: 0x000037EC
		public void OnProductsRetrieved(List<ProductDescription> products)
		{
			this.m_Util.RunOnMainThread(delegate
			{
				this.m_ForwardTo.OnProductsRetrieved(products);
			});
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005624 File Offset: 0x00003824
		public void OnPurchaseSucceeded(string id, [global::System.Runtime.CompilerServices.Nullable(2)] string receipt, string transactionID)
		{
			this.m_Util.RunOnMainThread(delegate
			{
				this.m_ForwardTo.OnPurchaseSucceeded(id, receipt, transactionID);
			});
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000566C File Offset: 0x0000386C
		public void OnAllPurchasesRetrieved(List<Product> purchasedProducts)
		{
			this.m_Util.RunOnMainThread(delegate
			{
				this.m_ForwardTo.OnAllPurchasesRetrieved(purchasedProducts);
			});
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000056A4 File Offset: 0x000038A4
		public void OnPurchaseFailed(PurchaseFailureDescription desc)
		{
			this.m_Util.RunOnMainThread(delegate
			{
				this.m_ForwardTo.OnPurchaseFailed(desc);
			});
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000056DC File Offset: 0x000038DC
		// (set) Token: 0x06000168 RID: 360 RVA: 0x000056E4 File Offset: 0x000038E4
		public bool useTransactionLog { get; set; }

		// Token: 0x04000096 RID: 150
		private readonly IStoreCallback m_ForwardTo;

		// Token: 0x04000097 RID: 151
		private readonly IUtil m_Util;
	}
}
