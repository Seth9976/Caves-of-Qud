using System;
using System.Collections.Generic;
using Uniject;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;
using UnityEngine.Purchasing.Telemetry;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000022 RID: 34
	internal class ProductDetailsResponseConsolidator : IProductDetailsResponseConsolidator
	{
		// Token: 0x06000097 RID: 151 RVA: 0x0000398C File Offset: 0x00001B8C
		internal ProductDetailsResponseConsolidator(IUtil util, ITelemetryDiagnostics telemetryDiagnostics, Action<IProductDetailsQueryResponse> onProductDetailsResponseConsolidated)
		{
			this.m_Util = util;
			this.m_OnProductDetailsResponseConsolidated = onProductDetailsResponseConsolidated;
			this.m_TelemetryDiagnostics = telemetryDiagnostics;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000039B4 File Offset: 0x00001BB4
		public void Consolidate(IGoogleBillingResult billingResult, IEnumerable<AndroidJavaObject> productDetails)
		{
			try
			{
				this.m_NumberReceivedCallbacks++;
				this.m_Responses.AddResponse(billingResult, productDetails);
				if (this.m_NumberReceivedCallbacks >= 2)
				{
					this.m_OnProductDetailsResponseConsolidated(this.m_Responses);
				}
			}
			catch (Exception ex)
			{
				this.m_TelemetryDiagnostics.SendDiagnostic("sku_details_response_consolidator_error", ex);
			}
		}

		// Token: 0x04000048 RID: 72
		private const int k_RequiredNumberOfCallbacks = 2;

		// Token: 0x04000049 RID: 73
		private int m_NumberReceivedCallbacks;

		// Token: 0x0400004A RID: 74
		private readonly Action<IProductDetailsQueryResponse> m_OnProductDetailsResponseConsolidated;

		// Token: 0x0400004B RID: 75
		private readonly IProductDetailsQueryResponse m_Responses = new ProductDetailsQueryResponse();

		// Token: 0x0400004C RID: 76
		private readonly IUtil m_Util;

		// Token: 0x0400004D RID: 77
		private readonly ITelemetryDiagnostics m_TelemetryDiagnostics;
	}
}
