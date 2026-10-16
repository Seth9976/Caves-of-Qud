using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000028 RID: 40
	internal class PurchasingFactory : IPurchasingBinder, IExtensionProvider
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00002C20 File Offset: 0x00000E20
		public PurchasingFactory(IPurchasingModule first, params IPurchasingModule[] remainingModules)
		{
			first.Configure(this);
			for (int i = 0; i < remainingModules.Length; i++)
			{
				remainingModules[i].Configure(this);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002C69 File Offset: 0x00000E69
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00002C71 File Offset: 0x00000E71
		public string storeName { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002C7A File Offset: 0x00000E7A
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00002C95 File Offset: 0x00000E95
		public IStore service
		{
			get
			{
				if (this.m_Store != null)
				{
					return this.m_Store;
				}
				throw new InvalidOperationException("No impl available!");
			}
			set
			{
				this.m_Store = value;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00002C9E File Offset: 0x00000E9E
		public void RegisterStore(string name, IStore s)
		{
			if (this.m_Store == null && s != null)
			{
				this.storeName = name;
				this.service = s;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00002CB9 File Offset: 0x00000EB9
		public void RegisterExtension<T>(T instance) where T : IStoreExtension
		{
			this.m_ExtensionMap[typeof(T)] = instance;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00002CD6 File Offset: 0x00000ED6
		public void RegisterConfiguration<T>(T instance) where T : IStoreConfiguration
		{
			this.m_ConfigMap[typeof(T)] = instance;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public T GetConfig<T>() where T : IStoreConfiguration
		{
			if (this.service is T)
			{
				return (T)((object)this.service);
			}
			Type typeFromHandle = typeof(T);
			if (this.m_ConfigMap.ContainsKey(typeFromHandle))
			{
				return (T)((object)this.m_ConfigMap[typeFromHandle]);
			}
			string text = "No binding for config type ";
			Type type = typeFromHandle;
			throw new ArgumentException(text + ((type != null) ? type.ToString() : null));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00002D64 File Offset: 0x00000F64
		public T GetExtension<T>() where T : IStoreExtension
		{
			if (this.service is T)
			{
				return (T)((object)this.service);
			}
			Type typeFromHandle = typeof(T);
			if (this.m_ExtensionMap.ContainsKey(typeFromHandle))
			{
				return (T)((object)this.m_ExtensionMap[typeFromHandle]);
			}
			string text = "No binding for type ";
			Type type = typeFromHandle;
			throw new ArgumentException(text + ((type != null) ? type.ToString() : null));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00002DD1 File Offset: 0x00000FD1
		public void SetCatalogProvider(ICatalogProvider provider)
		{
			this.m_CatalogProvider = provider;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002DDA File Offset: 0x00000FDA
		public void SetCatalogProviderFunction(Action<Action<HashSet<ProductDefinition>>> func)
		{
			this.m_CatalogProvider = new SimpleCatalogProvider(func);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002DE8 File Offset: 0x00000FE8
		internal ICatalogProvider GetCatalogProvider()
		{
			return this.m_CatalogProvider;
		}

		// Token: 0x0400004E RID: 78
		private readonly Dictionary<Type, IStoreConfiguration> m_ConfigMap = new Dictionary<Type, IStoreConfiguration>();

		// Token: 0x0400004F RID: 79
		private readonly Dictionary<Type, IStoreExtension> m_ExtensionMap = new Dictionary<Type, IStoreExtension>();

		// Token: 0x04000050 RID: 80
		private IStore m_Store;

		// Token: 0x04000051 RID: 81
		private ICatalogProvider m_CatalogProvider;
	}
}
