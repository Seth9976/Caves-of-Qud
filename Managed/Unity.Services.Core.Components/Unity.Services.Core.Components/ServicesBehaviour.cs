using System;
using Unity.Services.Core.Internal;
using UnityEngine;

namespace Unity.Services.Core.Components
{
	// Token: 0x02000003 RID: 3
	public abstract class ServicesBehaviour : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public IUnityServices Services { get; internal set; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		internal virtual void Start()
		{
			this.SetRegistry();
			if (this.Services != null)
			{
				if (this.Services.State == ServicesInitializationState.Initialized)
				{
					this.OnServicesInitialized();
					return;
				}
				this.Services.Initialized -= this.OnServicesInitialized;
				this.Services.Initialized += this.OnServicesInitialized;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002130 File Offset: 0x00000330
		internal virtual void OnDestroy()
		{
			if (this.Services != null)
			{
				this.Services.Initialized -= this.OnServicesInitialized;
			}
			this.Cleanup();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002158 File Offset: 0x00000358
		private void SetRegistry()
		{
			this.Services = (this.UseCustomServices ? (UnityServices.Services.ContainsKey(this.ServicesIdentifier) ? UnityServices.Services[this.ServicesIdentifier] : UnityServices.CreateServices(this.ServicesIdentifier)) : UnityServices.Instance);
			this.OnServicesReady();
		}

		// Token: 0x06000008 RID: 8
		protected abstract void OnServicesReady();

		// Token: 0x06000009 RID: 9
		protected abstract void OnServicesInitialized();

		// Token: 0x0600000A RID: 10
		protected abstract void Cleanup();

		// Token: 0x04000002 RID: 2
		[SerializeField]
		[Tooltip("Use this to setup a custom services registry. All services in a registry are unique.")]
		[Visibility("ShowAdvancedFeatures", true)]
		public bool UseCustomServices;

		// Token: 0x04000003 RID: 3
		[SerializeField]
		[Tooltip("Unique local identifier for the custom set of services. Used as the key in the registries dictionary.")]
		[Visibility("ShowAdvancedFeatures", true)]
		public string ServicesIdentifier;

		// Token: 0x04000004 RID: 4
		[SerializeField]
		[HideInInspector]
		internal bool ShowAdvancedFeatures;
	}
}
