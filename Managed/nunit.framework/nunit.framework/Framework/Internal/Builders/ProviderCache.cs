using System;
using System.Collections.Generic;

namespace NUnit.Framework.Internal.Builders
{
	// Token: 0x020000C0 RID: 192
	internal class ProviderCache
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x00010F58 File Offset: 0x0000F158
		public static object GetInstanceOf(Type providerType)
		{
			return ProviderCache.GetInstanceOf(providerType, null);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00010F64 File Offset: 0x0000F164
		public static object GetInstanceOf(Type providerType, object[] providerArgs)
		{
			ProviderCache.CacheEntry cacheEntry = new ProviderCache.CacheEntry(providerType, providerArgs);
			object obj = (ProviderCache.instances.ContainsKey(cacheEntry) ? ProviderCache.instances[cacheEntry] : null);
			if (obj == null)
			{
				obj = (ProviderCache.instances[cacheEntry] = Reflect.Construct(providerType, providerArgs));
			}
			return obj;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00010FB0 File Offset: 0x0000F1B0
		public static void Clear()
		{
			foreach (ProviderCache.CacheEntry cacheEntry in ProviderCache.instances.Keys)
			{
				IDisposable disposable = ProviderCache.instances[cacheEntry] as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			ProviderCache.instances.Clear();
		}

		// Token: 0x0400016A RID: 362
		private static Dictionary<ProviderCache.CacheEntry, object> instances = new Dictionary<ProviderCache.CacheEntry, object>();

		// Token: 0x02000192 RID: 402
		private class CacheEntry
		{
			// Token: 0x06000AEB RID: 2795 RVA: 0x0001ABCC File Offset: 0x00018DCC
			public CacheEntry(Type providerType, object[] providerArgs)
			{
				this.providerType = providerType;
			}

			// Token: 0x06000AEC RID: 2796 RVA: 0x0001ABDC File Offset: 0x00018DDC
			public override bool Equals(object obj)
			{
				ProviderCache.CacheEntry cacheEntry = obj as ProviderCache.CacheEntry;
				return cacheEntry != null && this.providerType == cacheEntry.providerType;
			}

			// Token: 0x06000AED RID: 2797 RVA: 0x0001AC06 File Offset: 0x00018E06
			public override int GetHashCode()
			{
				return this.providerType.GetHashCode();
			}

			// Token: 0x04000298 RID: 664
			private Type providerType;
		}
	}
}
