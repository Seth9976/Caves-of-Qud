using System;

namespace UnityEngine.Purchasing.Extension
{
	// Token: 0x02000044 RID: 68
	public abstract class AbstractPurchasingModule : IPurchasingModule
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00004117 File Offset: 0x00002317
		public void Configure(IPurchasingBinder binder)
		{
			this.m_Binder = binder;
			this.Configure();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004126 File Offset: 0x00002326
		protected void RegisterStore(string name, IStore store)
		{
			this.m_Binder.RegisterStore(name, store);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004135 File Offset: 0x00002335
		protected void BindExtension<T>(T instance) where T : IStoreExtension
		{
			this.m_Binder.RegisterExtension<T>(instance);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004143 File Offset: 0x00002343
		protected void BindConfiguration<T>(T instance) where T : IStoreConfiguration
		{
			this.m_Binder.RegisterConfiguration<T>(instance);
		}

		// Token: 0x06000133 RID: 307
		public abstract void Configure();

		// Token: 0x0400009D RID: 157
		protected IPurchasingBinder m_Binder;
	}
}
