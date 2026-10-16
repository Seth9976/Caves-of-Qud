using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200025F RID: 607
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SymbolEqualityComparer : IEqualityComparer<ISymbol>
	{
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x00045C76 File Offset: 0x00043E76
		internal TypeCompareKind CompareKind { get; }

		// Token: 0x06001730 RID: 5936 RVA: 0x00045C7E File Offset: 0x00043E7E
		internal SymbolEqualityComparer(TypeCompareKind compareKind)
		{
			this.CompareKind = compareKind;
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x00045C8D File Offset: 0x00043E8D
		[NullableContext(2)]
		public bool Equals(ISymbol x, ISymbol y)
		{
			if (x == null)
			{
				return y == null;
			}
			return x.Equals(y, this);
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00045C9F File Offset: 0x00043E9F
		[NullableContext(2)]
		public int GetHashCode(ISymbol obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x04000954 RID: 2388
		public static readonly SymbolEqualityComparer Default = new SymbolEqualityComparer(TypeCompareKind.AllNullableIgnoreOptions);

		// Token: 0x04000955 RID: 2389
		public static readonly SymbolEqualityComparer IncludeNullability = new SymbolEqualityComparer(TypeCompareKind.ConsiderEverything);

		// Token: 0x04000956 RID: 2390
		internal static readonly SymbolEqualityComparer ConsiderEverything = new SymbolEqualityComparer(TypeCompareKind.ConsiderEverything);

		// Token: 0x04000957 RID: 2391
		internal static readonly SymbolEqualityComparer IgnoreAll = new SymbolEqualityComparer(TypeCompareKind.AllIgnoreOptions);

		// Token: 0x04000958 RID: 2392
		internal static readonly SymbolEqualityComparer CLRSignature = new SymbolEqualityComparer(TypeCompareKind.CLRSignatureCompareOptions);
	}
}
