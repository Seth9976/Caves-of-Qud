using System;
using System.Collections.Generic;
using Uniject;
using UnityEngine.Purchasing.Default;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;
using UnityEngine.Purchasing.Telemetry;
using UnityEngine.Purchasing.Utils;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200007A RID: 122
	public class StandardPurchasingModule : AbstractPurchasingModule, IAndroidStoreSelection, IStoreConfiguration
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000916E File Offset: 0x0000736E
		public string Version
		{
			get
			{
				return this.k_Version;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00009176 File Offset: 0x00007376
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000917E File Offset: 0x0000737E
		internal IUtil util { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00009187 File Offset: 0x00007387
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000918F File Offset: 0x0000738F
		internal ILogger logger { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00009198 File Offset: 0x00007398
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x000091A0 File Offset: 0x000073A0
		internal StandardPurchasingModule.StoreInstance storeInstance { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x000091A9 File Offset: 0x000073A9
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x000091B1 File Offset: 0x000073B1
		internal ITelemetryMetricsInstanceWrapper telemetryMetricsInstanceWrapper { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x000091BA File Offset: 0x000073BA
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x000091C2 File Offset: 0x000073C2
		internal ITelemetryDiagnosticsInstanceWrapper telemetryDiagnosticsInstanceWrapper { get; set; }

		// Token: 0x060002D7 RID: 727 RVA: 0x000091CC File Offset: 0x000073CC
		internal StandardPurchasingModule(IUtil util, ILogger logger, INativeStoreProvider nativeStoreProvider, RuntimePlatform platform, AppStore android, ITelemetryDiagnosticsInstanceWrapper telemetryDiagnosticsInstanceWrapper, ITelemetryMetricsInstanceWrapper telemetryMetricsInstanceWrapper)
		{
			this.util = util;
			this.logger = logger;
			this.m_NativeStoreProvider = nativeStoreProvider;
			this.m_RuntimePlatform = platform;
			this.useFakeStoreUIMode = FakeStoreUIMode.Default;
			this.useFakeStoreAlways = false;
			this.appStore = android;
			this.telemetryDiagnosticsInstanceWrapper = telemetryDiagnosticsInstanceWrapper;
			this.telemetryMetricsInstanceWrapper = telemetryMetricsInstanceWrapper;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000922D File Offset: 0x0000742D
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00009235 File Offset: 0x00007435
		public AppStore appStore { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000923E File Offset: 0x0000743E
		// (set) Token: 0x060002DB RID: 731 RVA: 0x00009246 File Offset: 0x00007446
		public FakeStoreUIMode useFakeStoreUIMode { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000924F File Offset: 0x0000744F
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00009257 File Offset: 0x00007457
		public bool useFakeStoreAlways { get; set; }

		// Token: 0x060002DE RID: 734 RVA: 0x00009260 File Offset: 0x00007460
		public static StandardPurchasingModule Instance()
		{
			return StandardPurchasingModule.Instance(AppStore.NotSpecified);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00009268 File Offset: 0x00007468
		public static StandardPurchasingModule Instance(AppStore androidStore)
		{
			if (StandardPurchasingModule.ModuleInstance == null)
			{
				ILogger unityLogger = Debug.unityLogger;
				GameObject gameObject = new GameObject("IAPUtil");
				Object.DontDestroyOnLoad(gameObject);
				gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
				UnityUtil unityUtil = gameObject.AddComponent<UnityUtil>();
				TextAsset textAsset = Resources.Load("BillingMode") as TextAsset;
				StoreConfiguration storeConfiguration = null;
				if (null != textAsset)
				{
					storeConfiguration = StoreConfiguration.Deserialize(textAsset.text);
				}
				if (androidStore == AppStore.NotSpecified)
				{
					androidStore = AppStore.GooglePlay;
					if (storeConfiguration != null)
					{
						AppStore androidStore2 = storeConfiguration.androidStore;
						if (androidStore2 != AppStore.NotSpecified)
						{
							androidStore = androidStore2;
						}
					}
				}
				StandardPurchasingModule.ModuleInstance = new StandardPurchasingModule(unityUtil, unityLogger, new NativeStoreProvider(), Application.platform, androidStore, new TelemetryDiagnosticsInstanceWrapper(unityLogger, unityUtil), new TelemetryMetricsInstanceWrapper(unityLogger, unityUtil));
			}
			return StandardPurchasingModule.ModuleInstance;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000930C File Offset: 0x0000750C
		public override void Configure()
		{
			base.BindConfiguration<IGooglePlayConfiguration>(new FakeGooglePlayStoreConfiguration());
			base.BindExtension<IGooglePlayStoreExtensions>(new FakeGooglePlayStoreExtensions());
			base.BindConfiguration<IAppleConfiguration>(new FakeAppleConfiguration());
			base.BindExtension<IAppleExtensions>(new FakeAppleExtensions());
			base.BindConfiguration<IAmazonConfiguration>(new FakeAmazonExtensions());
			base.BindExtension<IAmazonExtensions>(new FakeAmazonExtensions());
			base.BindConfiguration<IMicrosoftConfiguration>(new StandardPurchasingModule.MicrosoftConfiguration(this));
			base.BindExtension<IMicrosoftExtensions>(new FakeMicrosoftExtensions());
			base.BindConfiguration<IAndroidStoreSelection>(this);
			base.BindExtension<IUDPExtensions>(new FakeUDPExtension());
			base.BindExtension<ITransactionHistoryExtensions>(new FakeTransactionHistoryExtensions());
			if (this.storeInstance == null)
			{
				this.storeInstance = this.InstantiateStore();
			}
			base.RegisterStore(this.storeInstance.storeName, this.storeInstance.instance);
			IStoreInternal storeInternal = this.storeInstance.instance as IStoreInternal;
			if (storeInternal != null)
			{
				storeInternal.SetModule(this);
			}
			if (this.util != null && this.util.IsClassOrSubclass(typeof(JSONStore), this.storeInstance.instance.GetType()))
			{
				JSONStore jsonstore = (JSONStore)this.storeInstance.instance;
				base.BindExtension<ITransactionHistoryExtensions>(jsonstore);
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00009424 File Offset: 0x00007624
		private StandardPurchasingModule.StoreInstance InstantiateStore()
		{
			if (this.useFakeStoreAlways)
			{
				return new StandardPurchasingModule.StoreInstance("fake", this.InstantiateFakeStore());
			}
			RuntimePlatform runtimePlatform = this.m_RuntimePlatform;
			if (runtimePlatform <= RuntimePlatform.IPhonePlayer)
			{
				if (runtimePlatform == RuntimePlatform.OSXPlayer)
				{
					this.appStore = AppStore.MacAppStore;
					return new StandardPurchasingModule.StoreInstance("MacAppStore", this.InstantiateApple());
				}
				if (runtimePlatform != RuntimePlatform.IPhonePlayer)
				{
					goto IL_00D0;
				}
			}
			else if (runtimePlatform != RuntimePlatform.Android)
			{
				if (runtimePlatform - RuntimePlatform.MetroPlayerX86 <= 2)
				{
					this.appStore = AppStore.WinRT;
					return new StandardPurchasingModule.StoreInstance("WinRT", this.instantiateWindowsStore());
				}
				if (runtimePlatform != RuntimePlatform.tvOS)
				{
					goto IL_00D0;
				}
			}
			else
			{
				if (this.appStore == AppStore.UDP)
				{
					return new StandardPurchasingModule.StoreInstance(StandardPurchasingModule.AndroidStoreNameMap[this.appStore], this.InstantiateUDP());
				}
				return new StandardPurchasingModule.StoreInstance(StandardPurchasingModule.AndroidStoreNameMap[this.appStore], this.InstantiateAndroid());
			}
			this.appStore = AppStore.AppleAppStore;
			return new StandardPurchasingModule.StoreInstance("AppleAppStore", this.InstantiateApple());
			IL_00D0:
			this.appStore = AppStore.fake;
			return new StandardPurchasingModule.StoreInstance("fake", this.InstantiateFakeStore());
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009518 File Offset: 0x00007718
		private IStore InstantiateAndroid()
		{
			if (this.appStore == AppStore.GooglePlay)
			{
				return this.InstantiateGoogleStore();
			}
			MetricizedJsonStore metricizedJsonStore = new MetricizedJsonStore(new TelemetryMetricsService(this.telemetryMetricsInstanceWrapper));
			return this.InstantiateAndroidHelper(metricizedJsonStore);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00009550 File Offset: 0x00007750
		private IStore InstantiateGoogleStore()
		{
			IGooglePurchaseCallback googlePurchaseCallback = new GooglePlayPurchaseCallback(this.util);
			IGoogleProductCallback googleProductCallback = new GooglePlayProductCallback();
			GooglePurchaseStateEnumProvider googlePurchaseStateEnumProvider = new GooglePurchaseStateEnumProvider();
			IGooglePlayStoreService googlePlayStoreService = this.BuildAndInitGooglePlayStoreServiceAar(googlePurchaseCallback, googleProductCallback, googlePurchaseStateEnumProvider);
			IGooglePlayStorePurchaseService googlePlayStorePurchaseService = new GooglePlayStorePurchaseService(googlePlayStoreService);
			IGooglePlayStoreFinishTransactionService googlePlayStoreFinishTransactionService = new GooglePlayStoreFinishTransactionService(googlePlayStoreService);
			IGoogleFetchPurchases googleFetchPurchases = new GoogleFetchPurchases(googlePlayStoreService, this.util);
			GooglePlayConfiguration googlePlayConfiguration = StandardPurchasingModule.BuildGooglePlayStoreConfiguration(googlePlayStoreService, googlePurchaseCallback, googleProductCallback);
			TelemetryDiagnostics telemetryDiagnostics = new TelemetryDiagnostics(this.telemetryDiagnosticsInstanceWrapper);
			TelemetryMetricsService telemetryMetricsService = new TelemetryMetricsService(this.telemetryMetricsInstanceWrapper);
			MetricizedGooglePlayStoreExtensions metricizedGooglePlayStoreExtensions = new MetricizedGooglePlayStoreExtensions(googlePlayStoreService, googlePurchaseStateEnumProvider, this.logger, telemetryDiagnostics, telemetryMetricsService);
			GooglePlayStore googlePlayStore = new GooglePlayStore(new GooglePlayStoreRetrieveProductsService(googlePlayStoreService, googleFetchPurchases, googlePlayConfiguration, metricizedGooglePlayStoreExtensions), googlePlayStorePurchaseService, googleFetchPurchases, googlePlayStoreFinishTransactionService, googlePurchaseCallback, googlePlayConfiguration, metricizedGooglePlayStoreExtensions, this.util);
			this.util.AddPauseListener(new Action<bool>(googlePlayStore.OnPause));
			this.BindGoogleConfiguration(googlePlayConfiguration);
			this.BindGoogleExtension(metricizedGooglePlayStoreExtensions);
			return googlePlayStore;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000961E File Offset: 0x0000781E
		private void BindGoogleExtension(GooglePlayStoreExtensions googlePlayStoreExtensions)
		{
			base.BindExtension<IGooglePlayStoreExtensions>(googlePlayStoreExtensions);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00009628 File Offset: 0x00007828
		private static GooglePlayConfiguration BuildGooglePlayStoreConfiguration(IGooglePlayStoreService googlePlayStoreService, IGooglePurchaseCallback googlePurchaseCallback, IGoogleProductCallback googleProductCallback)
		{
			GooglePlayConfiguration googlePlayConfiguration = new GooglePlayConfiguration(googlePlayStoreService);
			googlePurchaseCallback.SetStoreConfiguration(googlePlayConfiguration);
			googleProductCallback.SetStoreConfiguration(googlePlayConfiguration);
			return googlePlayConfiguration;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000964B File Offset: 0x0000784B
		private void BindGoogleConfiguration(GooglePlayConfiguration googlePlayConfiguration)
		{
			base.BindConfiguration<IGooglePlayConfiguration>(googlePlayConfiguration);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00009654 File Offset: 0x00007854
		private IGooglePlayStoreService BuildAndInitGooglePlayStoreServiceAar(IGooglePurchaseCallback googlePurchaseCallback, IGoogleProductCallback googleProductCallback, IGooglePurchaseStateEnumProvider googlePurchaseStateEnumProvider)
		{
			GoogleCachedQueryProductDetailsService googleCachedQueryProductDetailsService = new GoogleCachedQueryProductDetailsService();
			GoogleLastKnownProductService googleLastKnownProductService = new GoogleLastKnownProductService();
			GooglePurchaseBuilder googlePurchaseBuilder = new GooglePurchaseBuilder(googleCachedQueryProductDetailsService, this.logger);
			GooglePurchaseUpdatedListener googlePurchaseUpdatedListener = new GooglePurchaseUpdatedListener(googleLastKnownProductService, googlePurchaseCallback, googlePurchaseBuilder, googleCachedQueryProductDetailsService, googlePurchaseStateEnumProvider, null);
			TelemetryDiagnostics telemetryDiagnostics = new TelemetryDiagnostics(this.telemetryDiagnosticsInstanceWrapper);
			GoogleBillingClient googleBillingClient = new GoogleBillingClient(googlePurchaseUpdatedListener, this.util, telemetryDiagnostics);
			ProductDetailsConverter productDetailsConverter = new ProductDetailsConverter();
			ExponentialRetryPolicy exponentialRetryPolicy = new ExponentialRetryPolicy(1000, 30000, 2);
			GoogleConnectionRetryPolicy googleConnectionRetryPolicy = new GoogleConnectionRetryPolicy(2000, 30000, 2);
			QueryProductDetailsService queryProductDetailsService = new QueryProductDetailsService(googleBillingClient, googleCachedQueryProductDetailsService, productDetailsConverter, exponentialRetryPolicy, googleProductCallback, this.util, telemetryDiagnostics);
			GooglePurchaseService googlePurchaseService = new GooglePurchaseService(googleBillingClient, googlePurchaseCallback, queryProductDetailsService);
			GoogleQueryPurchasesService googleQueryPurchasesService = new GoogleQueryPurchasesService(googleBillingClient, googlePurchaseBuilder);
			GoogleFinishTransactionService googleFinishTransactionService = new GoogleFinishTransactionService(googleBillingClient, googleQueryPurchasesService);
			BillingClientStateListener billingClientStateListener = new BillingClientStateListener();
			TelemetryMetricsService telemetryMetricsService = new TelemetryMetricsService(this.telemetryMetricsInstanceWrapper);
			googlePurchaseUpdatedListener.SetGoogleQueryPurchaseService(googleQueryPurchasesService);
			MetricizedGooglePlayStoreService metricizedGooglePlayStoreService = new MetricizedGooglePlayStoreService(googleBillingClient, queryProductDetailsService, googlePurchaseService, googleFinishTransactionService, googleQueryPurchasesService, billingClientStateListener, googleLastKnownProductService, telemetryDiagnostics, telemetryMetricsService, this.logger, googleConnectionRetryPolicy, this.util);
			metricizedGooglePlayStoreService.InitConnectionWithGooglePlay();
			return metricizedGooglePlayStoreService;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00009744 File Offset: 0x00007944
		private IStore InstantiateUDP()
		{
			UDPImpl udpimpl = new UDPImpl();
			base.BindExtension<IUDPExtensions>(udpimpl);
			INativeUDPStore nativeUDPStore = (INativeUDPStore)this.GetAndroidNativeStore(udpimpl);
			udpimpl.SetNativeStore(nativeUDPStore);
			return udpimpl;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00009773 File Offset: 0x00007973
		private IStore InstantiateAndroidHelper(JSONStore store)
		{
			store.SetNativeStore(this.GetAndroidNativeStore(store));
			return store;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009783 File Offset: 0x00007983
		private INativeStore GetAndroidNativeStore(JSONStore store)
		{
			return this.m_NativeStoreProvider.GetAndroidStore(store, this.appStore, this.m_Binder, this.util);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000097A4 File Offset: 0x000079A4
		private IStore InstantiateApple()
		{
			TelemetryDiagnostics telemetryDiagnostics = new TelemetryDiagnostics(this.telemetryDiagnosticsInstanceWrapper);
			TelemetryMetricsService telemetryMetricsService = new TelemetryMetricsService(this.telemetryMetricsInstanceWrapper);
			MetricizedAppleStoreImpl metricizedAppleStoreImpl = new MetricizedAppleStoreImpl(this.util, telemetryDiagnostics, telemetryMetricsService);
			INativeAppleStore storekit = this.m_NativeStoreProvider.GetStorekit(metricizedAppleStoreImpl);
			metricizedAppleStoreImpl.SetNativeStore(storekit);
			base.BindExtension<IAppleExtensions>(metricizedAppleStoreImpl);
			return metricizedAppleStoreImpl;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000097F4 File Offset: 0x000079F4
		private void UseMockWindowsStore(bool value)
		{
			if (this.windowsStore != null)
			{
				IWindowsIAP windowsIAP = Factory.Create(value);
				this.windowsStore.SetWindowsIAP(windowsIAP);
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000981C File Offset: 0x00007A1C
		private IStore instantiateWindowsStore()
		{
			IWindowsIAP windowsIAP = Factory.Create(false);
			this.windowsStore = new WinRTStore(windowsIAP, this.util, this.logger);
			this.util.AddPauseListener(new Action<bool>(this.windowsStore.restoreTransactions));
			return this.windowsStore;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000986C File Offset: 0x00007A6C
		private IStore InstantiateFakeStore()
		{
			FakeStore fakeStore = null;
			if (this.useFakeStoreUIMode != FakeStoreUIMode.Default)
			{
				fakeStore = new UIFakeStore
				{
					UIMode = this.useFakeStoreUIMode
				};
			}
			if (fakeStore == null)
			{
				fakeStore = new FakeStore();
			}
			return fakeStore;
		}

		// Token: 0x04000176 RID: 374
		[Obsolete("Not accurate. Use Version instead.", false)]
		public const string k_PackageVersion = "3.0.1";

		// Token: 0x04000177 RID: 375
		internal readonly string k_Version = "4.14.2";

		// Token: 0x04000178 RID: 376
		private readonly INativeStoreProvider m_NativeStoreProvider;

		// Token: 0x04000179 RID: 377
		private readonly RuntimePlatform m_RuntimePlatform;

		// Token: 0x0400017A RID: 378
		private static StandardPurchasingModule ModuleInstance;

		// Token: 0x04000180 RID: 384
		private static readonly Dictionary<AppStore, string> AndroidStoreNameMap = new Dictionary<AppStore, string>
		{
			{
				AppStore.AmazonAppStore,
				"AmazonApps"
			},
			{
				AppStore.GooglePlay,
				"GooglePlay"
			},
			{
				AppStore.UDP,
				UDP.Name
			},
			{
				AppStore.NotSpecified,
				"GooglePlay"
			}
		};

		// Token: 0x04000182 RID: 386
		private readonly bool usingMockMicrosoft;

		// Token: 0x04000185 RID: 389
		private WinRTStore windowsStore;

		// Token: 0x02000106 RID: 262
		internal class StoreInstance
		{
			// Token: 0x17000068 RID: 104
			// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000DC7F File Offset: 0x0000BE7F
			internal string storeName { get; }

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000DC87 File Offset: 0x0000BE87
			internal IStore instance { get; }

			// Token: 0x060004B3 RID: 1203 RVA: 0x0000DC8F File Offset: 0x0000BE8F
			internal StoreInstance(string name, IStore instance)
			{
				this.storeName = name;
				this.instance = instance;
			}
		}

		// Token: 0x02000107 RID: 263
		private class MicrosoftConfiguration : IMicrosoftConfiguration, IStoreConfiguration
		{
			// Token: 0x060004B4 RID: 1204 RVA: 0x0000DCA5 File Offset: 0x0000BEA5
			public MicrosoftConfiguration(StandardPurchasingModule module)
			{
				this.module = module;
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000DCB4 File Offset: 0x0000BEB4
			// (set) Token: 0x060004B6 RID: 1206 RVA: 0x0000DCBC File Offset: 0x0000BEBC
			public bool useMockBillingSystem
			{
				get
				{
					return this.useMock;
				}
				set
				{
					this.module.UseMockWindowsStore(value);
					this.useMock = value;
				}
			}

			// Token: 0x04000301 RID: 769
			private bool useMock;

			// Token: 0x04000302 RID: 770
			private readonly StandardPurchasingModule module;
		}
	}
}
