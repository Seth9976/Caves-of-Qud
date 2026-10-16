using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Uniject;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;
using UnityEngine.Purchasing.Stores.Util;
using UnityEngine.Purchasing.Telemetry;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200001F RID: 31
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class MetricizedGooglePlayStoreService : GooglePlayStoreService
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003640 File Offset: 0x00001840
		internal MetricizedGooglePlayStoreService(IGoogleBillingClient billingClient, IQueryProductDetailsService queryProductDetailsService, IGooglePurchaseService purchaseService, IGoogleFinishTransactionService finishTransactionService, IGoogleQueryPurchasesService queryPurchasesService, IBillingClientStateListener billingClientStateListener, IGoogleLastKnownProductService lastKnownProductService, ITelemetryDiagnostics telemetryDiagnostics, ITelemetryMetricsService telemetryMetricsService, ILogger logger, IRetryPolicy retryPolicy, IUtil util)
			: base(billingClient, queryProductDetailsService, purchaseService, finishTransactionService, queryPurchasesService, billingClientStateListener, lastKnownProductService, telemetryDiagnostics, logger, retryPolicy, util)
		{
			this.m_TelemetryDiagnostics = telemetryDiagnostics;
			this.m_TelemetryMetricsService = telemetryMetricsService;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003678 File Offset: 0x00001878
		protected override void DequeueQueryProducts(GoogleBillingResponseCode googleBillingResponseCode)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__0(googleBillingResponseCode);
			}, TelemetryMetricDefinitions.dequeueQueryProductsTimeName);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000036B5 File Offset: 0x000018B5
		protected override void DequeueFetchPurchases()
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(new Action(base.DequeueFetchPurchases), TelemetryMetricDefinitions.dequeueQueryPurchasesTimeName);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000036D4 File Offset: 0x000018D4
		public override void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products, Action<List<ProductDescription>, IGoogleBillingResult> onProductsReceived, Action<GoogleRetrieveProductsFailureReason, GoogleBillingResponseCode> onRetrieveProductsFailed)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__1(products, onProductsReceived, onRetrieveProductsFailed);
			}, TelemetryMetricDefinitions.retrieveProductsName);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003720 File Offset: 0x00001920
		public override void Purchase(ProductDefinition product, [global::System.Runtime.CompilerServices.Nullable(2)] Product oldProduct, GooglePlayReplacementMode? desiredReplacementMode)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__2(product, oldProduct, desiredReplacementMode);
			}, TelemetryMetricDefinitions.initPurchaseName);
		}

		// Token: 0x04000040 RID: 64
		private readonly ITelemetryMetricsService m_TelemetryMetricsService;

		// Token: 0x04000041 RID: 65
		private readonly ITelemetryDiagnostics m_TelemetryDiagnostics;
	}
}
