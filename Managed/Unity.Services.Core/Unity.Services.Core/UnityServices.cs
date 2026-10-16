using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.Core
{
	// Token: 0x0200000F RID: 15
	public static class UnityServices
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000034 RID: 52 RVA: 0x0000244B File Offset: 0x0000064B
		// (remove) Token: 0x06000035 RID: 53 RVA: 0x0000245F File Offset: 0x0000065F
		public static event Action Initialized
		{
			add
			{
				if (UnityServices.Instance != null)
				{
					UnityServices.Instance.Initialized += value;
				}
			}
			remove
			{
				if (UnityServices.Instance != null)
				{
					UnityServices.Instance.Initialized -= value;
				}
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000036 RID: 54 RVA: 0x00002473 File Offset: 0x00000673
		// (remove) Token: 0x06000037 RID: 55 RVA: 0x00002487 File Offset: 0x00000687
		public static event Action<Exception> InitializeFailed
		{
			add
			{
				if (UnityServices.Instance != null)
				{
					UnityServices.Instance.InitializeFailed += value;
				}
			}
			remove
			{
				if (UnityServices.Instance != null)
				{
					UnityServices.Instance.InitializeFailed -= value;
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000249B File Offset: 0x0000069B
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000024A2 File Offset: 0x000006A2
		public static IUnityServices Instance { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000024AA File Offset: 0x000006AA
		public static IReadOnlyDictionary<string, IUnityServices> Services
		{
			get
			{
				return UnityServices.s_Services;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000024B1 File Offset: 0x000006B1
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000024B8 File Offset: 0x000006B8
		internal static TaskCompletionSource<object> InstantiationCompletion { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000024C0 File Offset: 0x000006C0
		private static Dictionary<string, IUnityServices> s_Services { get; } = new Dictionary<string, IUnityServices>();

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000024C8 File Offset: 0x000006C8
		public static ServicesInitializationState State
		{
			get
			{
				if (!UnityThreadUtils.IsRunningOnUnityThread)
				{
					throw new ServicesInitializationException("You are attempting to access UnityServices.State from a non-Unity Thread. UnityServices.State can only be accessed from Unity Thread");
				}
				if (UnityServices.Instance != null)
				{
					return UnityServices.Instance.State;
				}
				TaskCompletionSource<object> instantiationCompletion = UnityServices.InstantiationCompletion;
				if (instantiationCompletion != null && instantiationCompletion.Task.Status == TaskStatus.WaitingForActivation)
				{
					return ServicesInitializationState.Initializing;
				}
				return ServicesInitializationState.Uninitialized;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002517 File Offset: 0x00000717
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002523 File Offset: 0x00000723
		public static string ExternalUserId
		{
			get
			{
				return UnityServices.ExternalUserIdProperty.UserId;
			}
			set
			{
				UnityServices.ExternalUserIdProperty.UserId = value;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002530 File Offset: 0x00000730
		public static Task InitializeAsync()
		{
			return UnityServices.InitializeAsync(new InitializationOptions());
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000253C File Offset: 0x0000073C
		[PreserveDependency("Register()", "Unity.Services.Core.Registration.CorePackageInitializer", "Unity.Services.Core.Registration")]
		[PreserveDependency("CreateStaticInstance()", "Unity.Services.Core.Internal.UnityServicesInitializer", "Unity.Services.Core.Internal")]
		[PreserveDependency("EnableServicesInitializationAsync()", "Unity.Services.Core.Internal.UnityServicesInitializer", "Unity.Services.Core.Internal")]
		[PreserveDependency("CaptureUnityThreadInfo()", "Unity.Services.Core.UnityThreadUtils", "Unity.Services.Core")]
		public static async Task InitializeAsync(InitializationOptions options)
		{
			if (!UnityThreadUtils.IsRunningOnUnityThread)
			{
				throw new ServicesInitializationException("You are attempting to initialize Unity Services from a non-Unity Thread. Unity Services can only be initialized from Unity Thread");
			}
			if (!Application.isPlaying)
			{
				throw new ServicesInitializationException("You are attempting to initialize Unity Services in Edit Mode. Unity Services can only be initialized in Play Mode");
			}
			if (UnityServices.Instance == null)
			{
				if (UnityServices.InstantiationCompletion == null)
				{
					UnityServices.InstantiationCompletion = new TaskCompletionSource<object>();
				}
				await UnityServices.InstantiationCompletion.Task;
			}
			await UnityServices.Instance.InitializeAsync(options);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002580 File Offset: 0x00000780
		public static IUnityServices CreateServices()
		{
			return UnityServices.CreateServices(Guid.NewGuid().ToString());
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000025A8 File Offset: 0x000007A8
		public static IUnityServices CreateServices(string servicesId)
		{
			if (string.IsNullOrEmpty(servicesId))
			{
				throw new ArgumentException("The services identifier cannot be null or empty");
			}
			if (UnityServices.s_Services.ContainsKey(servicesId))
			{
				throw new ServicesCreationException("The services identifier '" + servicesId + "' is already registered.");
			}
			IUnityServices unityServices = UnityServicesBuilder.Create(servicesId);
			UnityServices.s_Services[servicesId] = unityServices;
			return unityServices;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000025FF File Offset: 0x000007FF
		internal static void ClearServices()
		{
			UnityServices.s_Services.Clear();
		}

		// Token: 0x0400001E RID: 30
		internal static ExternalUserIdProperty ExternalUserIdProperty = new ExternalUserIdProperty();
	}
}
