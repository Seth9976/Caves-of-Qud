using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.Cci
{
	// Token: 0x020000C4 RID: 196
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class SymbolEquivalentEqualityComparer : IEqualityComparer<IReference>, IEqualityComparer<INamespace>
	{
		// Token: 0x060006EB RID: 1771 RVA: 0x00017AB1 File Offset: 0x00015CB1
		private SymbolEquivalentEqualityComparer()
		{
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00017ABC File Offset: 0x00015CBC
		public bool Equals(IReference x, IReference y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			ISymbolInternal internalSymbol = x.GetInternalSymbol();
			ISymbolInternal internalSymbol2 = y.GetInternalSymbol();
			return internalSymbol != null && internalSymbol2 != null && internalSymbol.Equals(internalSymbol2);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00017AF4 File Offset: 0x00015CF4
		public int GetHashCode(IReference obj)
		{
			ISymbolInternal symbolInternal = ((obj != null) ? obj.GetInternalSymbol() : null);
			if (symbolInternal != null)
			{
				return symbolInternal.GetHashCode();
			}
			if (obj == null)
			{
				return 0;
			}
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00017B24 File Offset: 0x00015D24
		public bool Equals(INamespace x, INamespace y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			INamespaceSymbolInternal internalSymbol = x.GetInternalSymbol();
			INamespaceSymbolInternal internalSymbol2 = y.GetInternalSymbol();
			return internalSymbol != null && internalSymbol2 != null && internalSymbol.Equals(internalSymbol2);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00017B5C File Offset: 0x00015D5C
		public int GetHashCode(INamespace obj)
		{
			INamespaceSymbolInternal namespaceSymbolInternal = ((obj != null) ? obj.GetInternalSymbol() : null);
			if (namespaceSymbolInternal != null)
			{
				return namespaceSymbolInternal.GetHashCode();
			}
			if (obj == null)
			{
				return 0;
			}
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x04000202 RID: 514
		[Nullable(1)]
		public static readonly SymbolEquivalentEqualityComparer Instance = new SymbolEquivalentEqualityComparer();
	}
}
