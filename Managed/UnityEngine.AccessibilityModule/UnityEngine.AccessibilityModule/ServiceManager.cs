using System;
using System.Collections.Generic;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000017 RID: 23
	internal class ServiceManager
	{
		// Token: 0x06000136 RID: 310 RVA: 0x00004F72 File Offset: 0x00003172
		public ServiceManager()
		{
			this.m_Services = new Dictionary<Type, IService>();
			AccessibilityManager.screenReaderStatusChanged += this.ScreenReaderStatusChanged;
			this.UpdateServices(AssistiveSupport.isScreenReaderEnabled);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004FA8 File Offset: 0x000031A8
		public T GetService<T>() where T : IService
		{
			Type typeFromHandle = typeof(T);
			IService service;
			this.m_Services.TryGetValue(typeFromHandle, out service);
			return (T)((object)service);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004FDC File Offset: 0x000031DC
		private void StartService<T>() where T : IService
		{
			T t = this.GetService<T>();
			bool flag = t == null;
			if (flag)
			{
				Type typeFromHandle = typeof(T);
				t = (T)((object)Activator.CreateInstance(typeFromHandle));
				t.Start();
				this.m_Services.Add(typeFromHandle, t);
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005038 File Offset: 0x00003238
		private void StopService<T>() where T : IService
		{
			T service = this.GetService<T>();
			bool flag = service != null;
			if (flag)
			{
				service.Stop();
				this.m_Services.Remove(typeof(T));
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005080 File Offset: 0x00003280
		private void UpdateServices(bool isScreenReaderEnabled)
		{
			if (isScreenReaderEnabled)
			{
				bool flag = !this.m_Services.ContainsKey(typeof(AccessibilityHierarchyService));
				if (flag)
				{
					AccessibilityHierarchyService accessibilityHierarchyService = new AccessibilityHierarchyService();
					accessibilityHierarchyService.Start();
					this.m_Services.Add(typeof(AccessibilityHierarchyService), accessibilityHierarchyService);
				}
			}
			else
			{
				this.StopService<AccessibilityHierarchyService>();
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000050E2 File Offset: 0x000032E2
		protected void ScreenReaderStatusChanged(bool isScreenReaderEnabled)
		{
			this.UpdateServices(isScreenReaderEnabled);
		}

		// Token: 0x0400006C RID: 108
		private readonly IDictionary<Type, IService> m_Services;
	}
}
