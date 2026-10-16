using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000055 RID: 85
	internal static class UnityServicesInitializer
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00003BE0 File Offset: 0x00001DE0
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void CreateStaticInstance()
		{
			if (UnityServicesInitializer.s_CreatedServices)
			{
				UnityServicesInitializer.s_CreatedServices = false;
				return;
			}
			UnityServices.ClearServices();
			UnityServicesBuilder.InstanceCreationDelegate = new UnityServicesBuilder.CreationDelegate(UnityServicesInitializer.CreateInstance);
			CorePackageRegistry corePackageRegistry = new CorePackageRegistry();
			CoreRegistry coreRegistry = new CoreRegistry(corePackageRegistry.Registry, ServicesType.Default, null);
			CorePackageRegistry.Instance = corePackageRegistry;
			CoreRegistry.Instance = coreRegistry;
			CoreMetrics coreMetrics = new CoreMetrics();
			CoreDiagnostics coreDiagnostics = new CoreDiagnostics();
			UnityServices.Instance = new UnityServicesInternal(coreRegistry, coreMetrics, coreDiagnostics);
			TaskCompletionSource<object> instantiationCompletion = UnityServices.InstantiationCompletion;
			if (instantiationCompletion != null)
			{
				instantiationCompletion.TrySetResult(null);
			}
			CoreMetrics.Instance = coreMetrics;
			CoreDiagnostics.Instance = coreDiagnostics;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00003C68 File Offset: 0x00001E68
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static async void EnableServicesInitializationAsync()
		{
			await ((UnityServicesInternal)UnityServices.Instance).EnableInitializationAsync();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00003C97 File Offset: 0x00001E97
		internal static IUnityServices CreateInstance(string servicesId)
		{
			UnityServicesInternal unityServicesInternal = new UnityServicesInternal(new CoreRegistry(CorePackageRegistry.Instance.Registry, ServicesType.Instance, servicesId), CoreMetrics.Instance, CoreDiagnostics.Instance);
			unityServicesInternal.EnableInitialization();
			return unityServicesInternal;
		}

		// Token: 0x0400006E RID: 110
		private static bool s_CreatedServices;
	}
}
