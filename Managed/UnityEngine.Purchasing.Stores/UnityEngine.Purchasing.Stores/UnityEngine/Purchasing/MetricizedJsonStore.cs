using System;
using System.Collections.ObjectModel;
using UnityEngine.Purchasing.Telemetry;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000065 RID: 101
	internal class MetricizedJsonStore : JSONStore
	{
		// Token: 0x06000253 RID: 595 RVA: 0x00007909 File Offset: 0x00005B09
		public MetricizedJsonStore(ITelemetryMetricsService telemetryMetricsService)
		{
			this.m_TelemetryMetricsService = telemetryMetricsService;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007918 File Offset: 0x00005B18
		public override void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__0(products);
			}, TelemetryMetricDefinitions.retrieveProductsName);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007958 File Offset: 0x00005B58
		public override void Purchase(ProductDefinition product, string developerPayload)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__1(product, developerPayload);
			}, TelemetryMetricDefinitions.initPurchaseName);
		}

		// Token: 0x04000106 RID: 262
		private readonly ITelemetryMetricsService m_TelemetryMetricsService;
	}
}
