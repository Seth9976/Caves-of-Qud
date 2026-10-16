using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.Symbols
{
	// Token: 0x020002C3 RID: 707
	[NullableContext(1)]
	internal interface ISymbolInternal
	{
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001D9C RID: 7580
		SymbolKind Kind { get; }

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001D9D RID: 7581
		string Name { get; }

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001D9E RID: 7582
		string MetadataName { get; }

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001D9F RID: 7583
		[Nullable(0)]
		Compilation DeclaringCompilation
		{
			[NullableContext(0)]
			get;
		}

		// Token: 0x06001DA0 RID: 7584
		[NullableContext(2)]
		bool Equals(ISymbolInternal other, TypeCompareKind compareKind);

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001DA1 RID: 7585
		ISymbolInternal ContainingSymbol { get; }

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001DA2 RID: 7586
		IAssemblySymbolInternal ContainingAssembly { get; }

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001DA3 RID: 7587
		IModuleSymbolInternal ContainingModule { get; }

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001DA4 RID: 7588
		INamedTypeSymbolInternal ContainingType { get; }

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001DA5 RID: 7589
		INamespaceSymbolInternal ContainingNamespace { get; }

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001DA6 RID: 7590
		bool IsDefinition { get; }

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001DA7 RID: 7591
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<Location> Locations
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001DA8 RID: 7592
		bool IsImplicitlyDeclared { get; }

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001DA9 RID: 7593
		Accessibility DeclaredAccessibility { get; }

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001DAA RID: 7594
		bool IsStatic { get; }

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001DAB RID: 7595
		bool IsVirtual { get; }

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001DAC RID: 7596
		bool IsOverride { get; }

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001DAD RID: 7597
		bool IsAbstract { get; }

		// Token: 0x06001DAE RID: 7598
		ISymbol GetISymbol();

		// Token: 0x06001DAF RID: 7599
		IReference GetCciAdapter();
	}
}
