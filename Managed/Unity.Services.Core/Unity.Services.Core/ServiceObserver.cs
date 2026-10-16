using System;
using UnityEngine;

namespace Unity.Services.Core
{
	// Token: 0x0200000E RID: 14
	public class ServiceObserver<T> : IDisposable
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600002B RID: 43 RVA: 0x000022D4 File Offset: 0x000004D4
		// (remove) Token: 0x0600002C RID: 44 RVA: 0x0000230C File Offset: 0x0000050C
		public event Action<T> Initialized;

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002341 File Offset: 0x00000541
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002349 File Offset: 0x00000549
		public T Service { get; private set; }

		// Token: 0x0600002F RID: 47 RVA: 0x00002352 File Offset: 0x00000552
		public ServiceObserver()
			: this(UnityServices.Instance)
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.m_Registry = UnityServices.Instance;
			this.Init();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002378 File Offset: 0x00000578
		public ServiceObserver(IUnityServices registry)
		{
			if (registry == null)
			{
				throw new ArgumentNullException("registry");
			}
			this.m_Registry = registry;
			this.Init();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000239C File Offset: 0x0000059C
		private void Init()
		{
			if (this.m_Registry.State == ServicesInitializationState.Initialized)
			{
				this.AssignService();
				return;
			}
			this.m_Registry.Initialized -= this.AssignService;
			this.m_Registry.Initialized += this.AssignService;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000023EC File Offset: 0x000005EC
		private void AssignService()
		{
			this.Service = this.m_Registry.GetService<T>();
			if (this.Service != null)
			{
				Action<T> initialized = this.Initialized;
				if (initialized == null)
				{
					return;
				}
				initialized(this.Service);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002422 File Offset: 0x00000622
		public void Dispose()
		{
			if (this.m_Registry == null)
			{
				return;
			}
			this.m_Registry.Initialized -= this.AssignService;
			this.m_Registry = null;
		}

		// Token: 0x0400001B RID: 27
		private IUnityServices m_Registry;
	}
}
