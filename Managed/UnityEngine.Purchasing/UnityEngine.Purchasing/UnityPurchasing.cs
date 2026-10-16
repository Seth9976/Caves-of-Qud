using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200002E RID: 46
	public abstract class UnityPurchasing
	{
		// Token: 0x060000FA RID: 250 RVA: 0x000038CC File Offset: 0x00001ACC
		[Obsolete("Use Initialize(IDetailedStoreListener, ConfigurationBuilder)", false)]
		public static void Initialize(IStoreListener listener, ConfigurationBuilder builder)
		{
			ILogger unityLogger = Debug.unityLogger;
			UnityServicesInitializationChecker unityServicesInitializationChecker = new UnityServicesInitializationChecker(unityLogger);
			LegacyAnalyticsWrapper legacyAnalyticsWrapper = new LegacyAnalyticsWrapper(UnityPurchasing.GenerateLegacyUnityAnalytics(), new EmptyAnalyticsAdapter());
			UnityPurchasing.Initialize(listener, builder, unityLogger, Application.persistentDataPath, UnityPurchasing.GenerateUnityAnalytics(unityLogger), legacyAnalyticsWrapper, builder.factory.GetCatalogProvider(), unityServicesInitializationChecker);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003918 File Offset: 0x00001B18
		public static void Initialize(IDetailedStoreListener listener, ConfigurationBuilder builder)
		{
			ILogger unityLogger = Debug.unityLogger;
			UnityServicesInitializationChecker unityServicesInitializationChecker = new UnityServicesInitializationChecker(unityLogger);
			LegacyAnalyticsWrapper legacyAnalyticsWrapper = new LegacyAnalyticsWrapper(UnityPurchasing.GenerateLegacyUnityAnalytics(), new EmptyAnalyticsAdapter());
			UnityPurchasing.Initialize(listener, builder, unityLogger, Application.persistentDataPath, UnityPurchasing.GenerateUnityAnalytics(unityLogger), legacyAnalyticsWrapper, builder.factory.GetCatalogProvider(), unityServicesInitializationChecker);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003962 File Offset: 0x00001B62
		private static IAnalyticsAdapter GenerateUnityAnalytics(ILogger logger)
		{
			return new EmptyAnalyticsAdapter();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003969 File Offset: 0x00001B69
		private static IAnalyticsAdapter GenerateLegacyUnityAnalytics()
		{
			return new LegacyAnalyticsAdapter(new LegacyUnityAnalytics());
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003975 File Offset: 0x00001B75
		public static void ClearTransactionLog()
		{
			new TransactionLog(Debug.unityLogger, Application.persistentDataPath).Clear();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000398C File Offset: 0x00001B8C
		internal static void Initialize(IStoreListener listener, ConfigurationBuilder builder, ILogger logger, string persistentDatapath, IAnalyticsAdapter ugsAnalytics, IAnalyticsAdapter legacyAnalytics, ICatalogProvider catalog, IUnityServicesInitializationChecker unityServicesInitializationChecker)
		{
			unityServicesInitializationChecker.CheckAndLogWarning();
			TransactionLog transactionLog = new TransactionLog(logger, persistentDatapath);
			PurchasingManager manager = new PurchasingManager(transactionLog, logger, builder.factory.service, builder.factory.storeName, unityServicesInitializationChecker, builder.logUnavailableProducts);
			AnalyticsClient analyticsClient = new AnalyticsClient(ugsAnalytics, legacyAnalytics);
			StoreListenerProxy proxy = new StoreListenerProxy(listener, analyticsClient, builder.factory);
			UnityPurchasing.FetchAndMergeProducts(builder.useCatalogProvider, builder.products, catalog, delegate(HashSet<ProductDefinition> response)
			{
				manager.Initialize(proxy, response);
			});
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003A18 File Offset: 0x00001C18
		internal static void FetchAndMergeProducts(bool useCatalog, HashSet<ProductDefinition> localProductSet, ICatalogProvider catalog, Action<HashSet<ProductDefinition>> callback)
		{
			if (useCatalog && catalog != null)
			{
				catalog.FetchProducts(delegate(HashSet<ProductDefinition> cloudProducts)
				{
					HashSet<ProductDefinition> hashSet = new HashSet<ProductDefinition>(localProductSet);
					foreach (ProductDefinition productDefinition in cloudProducts)
					{
						hashSet.Remove(productDefinition);
						hashSet.Add(productDefinition);
					}
					callback(hashSet);
				});
				return;
			}
			callback(localProductSet);
		}
	}
}
