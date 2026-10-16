using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000056 RID: 86
	internal class UnityServicesInternal : IUnityServices
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600017D RID: 381 RVA: 0x00003CC0 File Offset: 0x00001EC0
		// (remove) Token: 0x0600017E RID: 382 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public event Action Initialized;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600017F RID: 383 RVA: 0x00003D30 File Offset: 0x00001F30
		// (remove) Token: 0x06000180 RID: 384 RVA: 0x00003D68 File Offset: 0x00001F68
		public event Action<Exception> InitializeFailed;

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00003D9D File Offset: 0x00001F9D
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00003DA5 File Offset: 0x00001FA5
		public ServicesInitializationState State { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00003DAE File Offset: 0x00001FAE
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00003DBB File Offset: 0x00001FBB
		public InitializationOptions Options
		{
			get
			{
				return this.Registry.Options;
			}
			internal set
			{
				this.Registry.Options = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00003DC9 File Offset: 0x00001FC9
		[NotNull]
		internal CoreRegistry Registry { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00003DD1 File Offset: 0x00001FD1
		[NotNull]
		internal CoreMetrics Metrics { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00003DD9 File Offset: 0x00001FD9
		[NotNull]
		internal CoreDiagnostics Diagnostics { get; }

		// Token: 0x06000188 RID: 392 RVA: 0x00003DE1 File Offset: 0x00001FE1
		public UnityServicesInternal([NotNull] CoreRegistry registry, [NotNull] CoreMetrics coreMetrics, [NotNull] CoreDiagnostics coreDiagnostics)
		{
			this.Registry = registry;
			this.Metrics = coreMetrics;
			this.Diagnostics = coreDiagnostics;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00003E00 File Offset: 0x00002000
		public async Task InitializeAsync(InitializationOptions options)
		{
			try
			{
				if (options == null)
				{
					options = new InitializationOptions();
				}
				if (!this.HasRequestedInitialization() || this.<InitializeAsync>g__HasInitializationFailed|27_0())
				{
					this.Registry.Options = options;
					this.m_Initialization = new TaskCompletionSource<object>();
				}
				if (!this.CanInitialize || this.State != ServicesInitializationState.Uninitialized)
				{
					await this.m_Initialization.Task;
				}
				else
				{
					await this.InitializeServicesAsync();
				}
				this.TriggerInitializeSuccess();
			}
			catch (Exception ex)
			{
				this.TriggerInitializeFailed(ex);
				throw;
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00003E4B File Offset: 0x0000204B
		public string GetIdentifier()
		{
			return this.Registry.InstanceId;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00003E58 File Offset: 0x00002058
		private void TriggerInitializeSuccess()
		{
			try
			{
				Action initialized = this.Initialized;
				if (initialized != null)
				{
					initialized();
				}
			}
			catch (Exception ex)
			{
				CoreLogger.LogError(string.Format("{0} {1}", "Exception in services initialization success event handler: ", ex));
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00003EA0 File Offset: 0x000020A0
		private void TriggerInitializeFailed(Exception initException)
		{
			try
			{
				Action<Exception> initializeFailed = this.InitializeFailed;
				if (initializeFailed != null)
				{
					initializeFailed(initException);
				}
			}
			catch (Exception ex)
			{
				CoreLogger.LogError(string.Format("{0} {1}", "Exception in services initialization failure event handler: ", ex));
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00003EEC File Offset: 0x000020EC
		public T GetService<T>()
		{
			return this.Registry.GetService<T>();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00003EF9 File Offset: 0x000020F9
		private bool HasRequestedInitialization()
		{
			return this.m_Initialization != null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00003F08 File Offset: 0x00002108
		private async Task InitializeServicesAsync()
		{
			UnityServicesInternal.<>c__DisplayClass33_0 CS$<>8__locals1 = new UnityServicesInternal.<>c__DisplayClass33_0();
			CS$<>8__locals1.<>4__this = this;
			this.State = ServicesInitializationState.Initializing;
			CS$<>8__locals1.initStopwatch = new Stopwatch();
			CS$<>8__locals1.initStopwatch.Start();
			CS$<>8__locals1.dependencyTree = this.Registry.PackageRegistry.Tree;
			if (CS$<>8__locals1.dependencyTree == null)
			{
				NullReferenceException ex = new NullReferenceException("Services require a valid dependency tree to be initialized.");
				CS$<>8__locals1.<InitializeServicesAsync>g__FailServicesInitialization|2(ex);
				throw ex;
			}
			CS$<>8__locals1.sortedPackageTypeHashes = new List<int>(CS$<>8__locals1.dependencyTree.PackageTypeHashToInstance.Count);
			try
			{
				CS$<>8__locals1.<InitializeServicesAsync>g__SortPackages|0();
				await CS$<>8__locals1.<InitializeServicesAsync>g__InitializePackagesAsync|1();
			}
			catch (Exception ex2)
			{
				CS$<>8__locals1.<InitializeServicesAsync>g__FailServicesInitialization|2(ex2);
				throw;
			}
			CS$<>8__locals1.<InitializeServicesAsync>g__SucceedServicesInitialization|3();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00003F4C File Offset: 0x0000214C
		internal void SendInitializationMetrics(List<PackageInitializationInfo> packageInitInfos)
		{
			foreach (PackageInitializationInfo packageInitializationInfo in packageInitInfos)
			{
				this.Metrics.SendInitTimeMetricForPackage(packageInitializationInfo.PackageType, packageInitializationInfo.InitializationTimeInSeconds);
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00003FAC File Offset: 0x000021AC
		internal void EnableInitialization()
		{
			this.CanInitialize = true;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00003FB8 File Offset: 0x000021B8
		internal async Task EnableInitializationAsync()
		{
			this.CanInitialize = true;
			CorePackageRegistry.Instance.Lock();
			if (this.HasRequestedInitialization())
			{
				await this.InitializeServicesAsync();
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00003FFB File Offset: 0x000021FB
		[CompilerGenerated]
		private bool <InitializeAsync>g__HasInitializationFailed|27_0()
		{
			return this.m_Initialization.Task.IsCompleted && this.m_Initialization.Task.Status != TaskStatus.RanToCompletion;
		}

		// Token: 0x0400006F RID: 111
		internal const string InitSuccessEventInvocationError = "Exception in services initialization success event handler: ";

		// Token: 0x04000070 RID: 112
		internal const string InitFailureEventInvocationError = "Exception in services initialization failure event handler: ";

		// Token: 0x04000074 RID: 116
		internal bool CanInitialize;

		// Token: 0x04000075 RID: 117
		private TaskCompletionSource<object> m_Initialization;
	}
}
