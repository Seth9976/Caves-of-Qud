using System;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Telemetry;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000037 RID: 55
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class MetricizedGooglePlayStoreExtensions : GooglePlayStoreExtensions
	{
		// Token: 0x06000113 RID: 275 RVA: 0x000043EE File Offset: 0x000025EE
		internal MetricizedGooglePlayStoreExtensions(IGooglePlayStoreService googlePlayStoreService, IGooglePurchaseStateEnumProvider googlePurchaseStateEnumProvider, ILogger logger, ITelemetryDiagnostics telemetryDiagnostics, ITelemetryMetricsService telemetryMetricsService)
			: base(googlePlayStoreService, googlePurchaseStateEnumProvider, logger, telemetryDiagnostics)
		{
			this.m_TelemetryMetricsService = telemetryMetricsService;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004404 File Offset: 0x00002604
		public override void UpgradeDowngradeSubscription(string oldSku, string newSku, GooglePlayReplacementMode desiredReplacementMode)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__0(oldSku, newSku, desiredReplacementMode);
			}, TelemetryMetricDefinitions.upgradeDowngradeSubscriptionName);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004450 File Offset: 0x00002650
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[Obsolete("RestoreTransactions(Action<bool> callback) is deprecated, please use RestoreTransactions(Action<bool, string> callback) instead.")]
		public override void RestoreTransactions(Action<bool> callback)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__1(callback);
			}, TelemetryMetricDefinitions.restoreTransactionName);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004490 File Offset: 0x00002690
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override void RestoreTransactions(Action<bool, string> callback)
		{
			this.m_TelemetryMetricsService.ExecuteTimedAction(delegate
			{
				this.<>n__2(callback);
			}, TelemetryMetricDefinitions.restoreTransactionName);
		}

		// Token: 0x0400007E RID: 126
		private readonly ITelemetryMetricsService m_TelemetryMetricsService;
	}
}
