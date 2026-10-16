using System;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200003B RID: 59
	internal sealed class LookupSymbolsInfo : AbstractLookupSymbolsInfo<Symbol>
	{
		// Token: 0x060006B4 RID: 1716 RVA: 0x0003B690 File Offset: 0x00039890
		private LookupSymbolsInfo()
			: base(StringComparer.Ordinal)
		{
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0003B69D File Offset: 0x0003989D
		public void Free()
		{
			base.Clear();
			LookupSymbolsInfo.s_pool.Free(this);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0003B6B0 File Offset: 0x000398B0
		public static LookupSymbolsInfo GetInstance()
		{
			return LookupSymbolsInfo.s_pool.Allocate();
		}

		// Token: 0x040000BE RID: 190
		private const int poolSize = 64;

		// Token: 0x040000BF RID: 191
		private static readonly ObjectPool<LookupSymbolsInfo> s_pool = new ObjectPool<LookupSymbolsInfo>(() => new LookupSymbolsInfo(), 64);
	}
}
