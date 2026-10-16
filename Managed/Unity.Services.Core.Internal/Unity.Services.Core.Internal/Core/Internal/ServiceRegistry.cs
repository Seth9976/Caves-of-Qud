using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000051 RID: 81
	internal class ServiceRegistry : IServiceRegistry
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00003A03 File Offset: 0x00001C03
		[NotNull]
		internal Dictionary<int, object> ServiceTypeHashToInstance { get; }

		// Token: 0x0600015C RID: 348 RVA: 0x00003A0B File Offset: 0x00001C0B
		public ServiceRegistry()
		{
			this.ServiceTypeHashToInstance = new Dictionary<int, object>();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003A1E File Offset: 0x00001C1E
		public ServiceRegistry([NotNull] Dictionary<int, object> serviceTypeHashToInstance)
		{
			this.ServiceTypeHashToInstance = serviceTypeHashToInstance;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003A30 File Offset: 0x00001C30
		public void RegisterService<T>(T service)
		{
			Type typeFromHandle = typeof(T);
			if (service.GetType() == typeFromHandle)
			{
				throw new ArgumentException("Interface type of service not specified.");
			}
			int hashCode = typeFromHandle.GetHashCode();
			this.ServiceTypeHashToInstance[hashCode] = service;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00003A84 File Offset: 0x00001C84
		public T GetService<T>()
		{
			Type typeFromHandle = typeof(T);
			object obj;
			if (!this.ServiceTypeHashToInstance.TryGetValue(typeFromHandle.GetHashCode(), out obj))
			{
				return default(T);
			}
			return (T)((object)obj);
		}
	}
}
