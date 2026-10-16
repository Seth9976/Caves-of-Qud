using System;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000078 RID: 120
	internal sealed class WithClassTypeParametersBinder : WithTypeParametersBinder
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x00050F06 File Offset: 0x0004F106
		internal WithClassTypeParametersBinder(NamedTypeSymbol container, Binder next)
			: base(next)
		{
			this._namedType = container;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00050F16 File Offset: 0x0004F116
		internal override bool IsAccessibleHelper(Symbol symbol, TypeSymbol accessThroughType, out bool failedThroughTypeCheck, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConsList<TypeSymbol> basesBeingResolved)
		{
			return base.IsSymbolAccessibleConditional(symbol, this._namedType, accessThroughType, out failedThroughTypeCheck, ref useSiteInfo, basesBeingResolved);
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00050F2C File Offset: 0x0004F12C
		protected override MultiDictionary<string, TypeParameterSymbol> TypeParameterMap
		{
			get
			{
				if (this._lazyTypeParameterMap == null)
				{
					MultiDictionary<string, TypeParameterSymbol> multiDictionary = new MultiDictionary<string, TypeParameterSymbol>();
					foreach (TypeParameterSymbol typeParameterSymbol in this._namedType.TypeParameters)
					{
						multiDictionary.Add(typeParameterSymbol.Name, typeParameterSymbol);
					}
					Interlocked.CompareExchange<MultiDictionary<string, TypeParameterSymbol>>(ref this._lazyTypeParameterMap, multiDictionary, null);
				}
				return this._lazyTypeParameterMap;
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00050F90 File Offset: 0x0004F190
		protected override void AddLookupSymbolsInfoInSingleBinder(LookupSymbolsInfo result, LookupOptions options, Binder originalBinder)
		{
			if (base.CanConsiderTypeParameters(options))
			{
				foreach (TypeParameterSymbol typeParameterSymbol in this._namedType.TypeParameters)
				{
					if (originalBinder.CanAddLookupSymbolInfo(typeParameterSymbol, options, result, null, null))
					{
						result.AddSymbol(typeParameterSymbol, typeParameterSymbol.Name, 0);
					}
				}
			}
		}

		// Token: 0x04000477 RID: 1143
		private readonly NamedTypeSymbol _namedType;

		// Token: 0x04000478 RID: 1144
		private MultiDictionary<string, TypeParameterSymbol> _lazyTypeParameterMap;
	}
}
