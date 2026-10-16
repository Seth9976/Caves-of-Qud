using System;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200007D RID: 125
	internal sealed class WithMethodTypeParametersBinder : WithTypeParametersBinder
	{
		// Token: 0x06000AFC RID: 2812 RVA: 0x000516EC File Offset: 0x0004F8EC
		internal WithMethodTypeParametersBinder(MethodSymbol methodSymbol, Binder next)
			: base(next)
		{
			this._methodSymbol = methodSymbol;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x000516FC File Offset: 0x0004F8FC
		protected override bool InExecutableBinder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x000516FF File Offset: 0x0004F8FF
		internal override Symbol ContainingMemberOrLambda
		{
			get
			{
				return this._methodSymbol;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00051708 File Offset: 0x0004F908
		protected override MultiDictionary<string, TypeParameterSymbol> TypeParameterMap
		{
			get
			{
				if (this._lazyTypeParameterMap == null)
				{
					MultiDictionary<string, TypeParameterSymbol> multiDictionary = new MultiDictionary<string, TypeParameterSymbol>();
					foreach (TypeParameterSymbol typeParameterSymbol in this._methodSymbol.TypeParameters)
					{
						multiDictionary.Add(typeParameterSymbol.Name, typeParameterSymbol);
					}
					Interlocked.CompareExchange<MultiDictionary<string, TypeParameterSymbol>>(ref this._lazyTypeParameterMap, multiDictionary, null);
				}
				return this._lazyTypeParameterMap;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0005176C File Offset: 0x0004F96C
		protected override LookupOptions LookupMask
		{
			get
			{
				return LookupOptions.NamespaceAliasesOnly | LookupOptions.MustNotBeMethodTypeParameter;
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00051774 File Offset: 0x0004F974
		protected override void AddLookupSymbolsInfoInSingleBinder(LookupSymbolsInfo result, LookupOptions options, Binder originalBinder)
		{
			if (base.CanConsiderTypeParameters(options))
			{
				foreach (TypeParameterSymbol typeParameterSymbol in this._methodSymbol.TypeParameters)
				{
					if (originalBinder.CanAddLookupSymbolInfo(typeParameterSymbol, options, result, null, null))
					{
						result.AddSymbol(typeParameterSymbol, typeParameterSymbol.Name, 0);
					}
				}
			}
		}

		// Token: 0x0400047F RID: 1151
		private readonly MethodSymbol _methodSymbol;

		// Token: 0x04000480 RID: 1152
		private MultiDictionary<string, TypeParameterSymbol> _lazyTypeParameterMap;
	}
}
