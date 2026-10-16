using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Uniject;
using UnityEngine.Purchasing.Telemetry;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000061 RID: 97
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class MetricizedAppleStoreImpl : AppleStoreImpl
	{
		// Token: 0x0600022C RID: 556 RVA: 0x00007306 File Offset: 0x00005506
		public MetricizedAppleStoreImpl(IUtil util, ITelemetryDiagnostics telemetryDiagnostics, ITelemetryMetricsService telemetryMetricsService)
			: base(util, telemetryDiagnostics)
		{
			this.m_TelemetryMetricsService = telemetryMetricsService;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007318 File Offset: 0x00005518
		public override void FetchStorePromotionOrder(Action<List<Product>> successCallback, Action errorCallback)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__0(successCallback, errorCallback);
			}, TelemetryMetricDefinitions.fetchStorePromotionOrderName);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000735C File Offset: 0x0000555C
		public override void FetchStorePromotionVisibility(Product product, Action<string, AppleStorePromotionVisibility> successCallback, Action errorCallback)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__1(product, successCallback, errorCallback);
			}, TelemetryMetricDefinitions.fetchStorePromotionVisibilityName);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000073A8 File Offset: 0x000055A8
		public override void SetStorePromotionOrder(List<Product> products)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__2(products);
			}, TelemetryMetricDefinitions.setStorePromotionOrderName);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000073E8 File Offset: 0x000055E8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[Obsolete("RestoreTransactions(Action<bool> callback) is deprecated, please use RestoreTransactions(Action<bool, string> callback) instead.")]
		public override void RestoreTransactions(Action<bool> callback)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__3(callback);
			}, TelemetryMetricDefinitions.restoreTransactionName);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007428 File Offset: 0x00005628
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override void RestoreTransactions(Action<bool, string> callback)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__4(callback);
			}, TelemetryMetricDefinitions.restoreTransactionName);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007468 File Offset: 0x00005668
		public override void RefreshAppReceipt(Action<string> successCallback, Action<string> errorCallback)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__5(successCallback, errorCallback);
			}, TelemetryMetricDefinitions.refreshAppReceiptName);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000074AC File Offset: 0x000056AC
		public override void ContinuePromotionalPurchases()
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(new Action(base.ContinuePromotionalPurchases), TelemetryMetricDefinitions.continuePromotionalPurchasesName);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000074CA File Offset: 0x000056CA
		public override void PresentCodeRedemptionSheet()
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(new Action(base.PresentCodeRedemptionSheet), TelemetryMetricDefinitions.presentCodeRedemptionSheetName);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000074E8 File Offset: 0x000056E8
		public override void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__6(products);
			}, TelemetryMetricDefinitions.retrieveProductsName);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007528 File Offset: 0x00005728
		public override void Purchase(ProductDefinition product, string developerPayload)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__7(product, developerPayload);
			}, TelemetryMetricDefinitions.initPurchaseName);
		}

		// Token: 0x040000FA RID: 250
		private readonly ITelemetryMetricsService m_TelemetryMetricsService;
	}
}
