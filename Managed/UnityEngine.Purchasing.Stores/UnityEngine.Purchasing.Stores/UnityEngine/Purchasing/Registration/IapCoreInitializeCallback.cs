using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core.Analytics.Internal;
using Unity.Services.Core.Environments.Internal;
using Unity.Services.Core.Internal;
using Unity.Services.Core.Telemetry.Internal;
using UnityEngine.Purchasing.Telemetry;

namespace UnityEngine.Purchasing.Registration
{
	// Token: 0x02000096 RID: 150
	internal class IapCoreInitializeCallback : IInitializablePackage
	{
		// Token: 0x0600036D RID: 877 RVA: 0x0000B37C File Offset: 0x0000957C
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Register()
		{
			CoreRegistry.Instance.RegisterPackage<IapCoreInitializeCallback>(new IapCoreInitializeCallback()).DependsOn<IMetricsFactory>().DependsOn<IDiagnosticsFactory>()
				.OptionallyDependsOn<IAnalyticsStandardEventComponent>();
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000B3B4 File Offset: 0x000095B4
		public Task Initialize(CoreRegistry registry)
		{
			ITelemetryMetricsInstanceWrapper metricsInstanceWrapper = StandardPurchasingModule.Instance().telemetryMetricsInstanceWrapper;
			ITelemetryDiagnosticsInstanceWrapper diagnosticsInstanceWrapper = StandardPurchasingModule.Instance().telemetryDiagnosticsInstanceWrapper;
			((ITelemetryMetricsService)new TelemetryMetricsService(metricsInstanceWrapper)).ExecuteTimedAction(delegate
			{
				IapCoreInitializeCallback.CacheInitializedEnvironment(registry);
				IapCoreInitializeCallback.InitializeTelemetryComponents(metricsInstanceWrapper, diagnosticsInstanceWrapper);
			}, TelemetryMetricDefinitions.packageInitTimeName);
			return Task.CompletedTask;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000B414 File Offset: 0x00009614
		private static void CacheInitializedEnvironment(CoreRegistry registry)
		{
			string currentEnvironment = IapCoreInitializeCallback.GetCurrentEnvironment(registry);
			CoreServicesEnvironmentSubject.Instance().UpdateCurrentEnvironment(currentEnvironment);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000B434 File Offset: 0x00009634
		private static string GetCurrentEnvironment(CoreRegistry registry)
		{
			string text;
			try
			{
				text = registry.GetServiceComponent<IEnvironments>().Current;
			}
			catch (KeyNotFoundException)
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000B468 File Offset: 0x00009668
		private static void InitializeTelemetryComponents(ITelemetryMetricsInstanceWrapper metricsInstanceWrapper, ITelemetryDiagnosticsInstanceWrapper diagnosticsInstanceWrapper)
		{
			IDiagnosticsFactory serviceComponent = CoreRegistry.Instance.GetServiceComponent<IDiagnosticsFactory>();
			diagnosticsInstanceWrapper.SetDiagnosticsInstance(serviceComponent.Create("com.unity.purchasing"));
			IMetricsFactory serviceComponent2 = CoreRegistry.Instance.GetServiceComponent<IMetricsFactory>();
			metricsInstanceWrapper.SetMetricsInstance(serviceComponent2.Create("com.unity.purchasing"));
		}

		// Token: 0x040001E3 RID: 483
		private const string k_PurchasingPackageName = "com.unity.purchasing";
	}
}
