using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x020000EB RID: 235
	[NullableContext(2)]
	[Nullable(0)]
	internal struct UsedNamespaceOrType : IEquatable<UsedNamespaceOrType>
	{
		// Token: 0x06000760 RID: 1888 RVA: 0x0001821B File Offset: 0x0001641B
		private UsedNamespaceOrType(string alias = null, IAssemblyReference targetAssembly = null, INamespace targetNamespace = null, ITypeReference targetType = null, string targetXmlNamespace = null)
		{
			this.AliasOpt = alias;
			this.TargetAssemblyOpt = targetAssembly;
			this.TargetNamespaceOpt = targetNamespace;
			this.TargetTypeOpt = targetType;
			this.TargetXmlNamespaceOpt = targetXmlNamespace;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00018242 File Offset: 0x00016442
		[NullableContext(1)]
		internal static UsedNamespaceOrType CreateType(ITypeReference type, [Nullable(2)] string aliasOpt = null)
		{
			return new UsedNamespaceOrType(aliasOpt, null, null, type, null);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001824E File Offset: 0x0001644E
		internal static UsedNamespaceOrType CreateNamespace([Nullable(1)] INamespace @namespace, IAssemblyReference assemblyOpt = null, string aliasOpt = null)
		{
			return new UsedNamespaceOrType(aliasOpt, assemblyOpt, @namespace, null, null);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001825A File Offset: 0x0001645A
		[NullableContext(1)]
		internal static UsedNamespaceOrType CreateExternAlias(string alias)
		{
			return new UsedNamespaceOrType(alias, null, null, null, null);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00018266 File Offset: 0x00016466
		[NullableContext(1)]
		internal static UsedNamespaceOrType CreateXmlNamespace(string prefix, string xmlNamespace)
		{
			return new UsedNamespaceOrType(prefix, null, null, null, xmlNamespace);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00018274 File Offset: 0x00016474
		public override bool Equals(object obj)
		{
			if (obj is UsedNamespaceOrType)
			{
				UsedNamespaceOrType usedNamespaceOrType = (UsedNamespaceOrType)obj;
				return this.Equals(usedNamespaceOrType);
			}
			return false;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001829C File Offset: 0x0001649C
		public bool Equals(UsedNamespaceOrType other)
		{
			return this.AliasOpt == other.AliasOpt && object.Equals(this.TargetAssemblyOpt, other.TargetAssemblyOpt) && UsedNamespaceOrType.Equals(this.TargetNamespaceOpt, other.TargetNamespaceOpt) && UsedNamespaceOrType.Equals(this.TargetTypeOpt, other.TargetTypeOpt) && this.TargetXmlNamespaceOpt == other.TargetXmlNamespaceOpt;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00018308 File Offset: 0x00016508
		public override int GetHashCode()
		{
			return Hash.Combine<string>(this.AliasOpt, Hash.Combine<object>(this.TargetAssemblyOpt, Hash.Combine(UsedNamespaceOrType.GetHashCode(this.TargetNamespaceOpt), Hash.Combine(UsedNamespaceOrType.GetHashCode(this.TargetTypeOpt), Hash.Combine<string>(this.TargetXmlNamespaceOpt, 0)))));
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00018358 File Offset: 0x00016558
		private static bool Equals(ITypeReference x, ITypeReference y)
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
			if (internalSymbol != null && internalSymbol2 != null)
			{
				return internalSymbol.Equals(internalSymbol2);
			}
			return internalSymbol == null && internalSymbol2 == null && x.Equals(y);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000183A0 File Offset: 0x000165A0
		private static int GetHashCode(ITypeReference obj)
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
			return obj.GetHashCode();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000183D0 File Offset: 0x000165D0
		private static bool Equals(INamespace x, INamespace y)
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
			if (internalSymbol != null && internalSymbol2 != null)
			{
				return internalSymbol.Equals(internalSymbol2);
			}
			return internalSymbol == null && internalSymbol2 == null && x.Equals(y);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00018418 File Offset: 0x00016618
		private static int GetHashCode(INamespace obj)
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
			return obj.GetHashCode();
		}

		// Token: 0x04000245 RID: 581
		public readonly string AliasOpt;

		// Token: 0x04000246 RID: 582
		public readonly IAssemblyReference TargetAssemblyOpt;

		// Token: 0x04000247 RID: 583
		public readonly INamespace TargetNamespaceOpt;

		// Token: 0x04000248 RID: 584
		public readonly ITypeReference TargetTypeOpt;

		// Token: 0x04000249 RID: 585
		public readonly string TargetXmlNamespaceOpt;
	}
}
