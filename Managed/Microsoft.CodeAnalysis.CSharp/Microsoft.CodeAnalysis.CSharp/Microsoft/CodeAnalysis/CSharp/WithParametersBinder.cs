using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200007F RID: 127
	internal sealed class WithParametersBinder : Binder
	{
		// Token: 0x06000B04 RID: 2820 RVA: 0x000517FD File Offset: 0x0004F9FD
		internal WithParametersBinder(ImmutableArray<ParameterSymbol> parameters, Binder next)
			: base(next, null)
		{
			this._parameters = parameters;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00051810 File Offset: 0x0004FA10
		protected override void AddLookupSymbolsInfoInSingleBinder(LookupSymbolsInfo result, LookupOptions options, Binder originalBinder)
		{
			if (options.CanConsiderLocals())
			{
				foreach (ParameterSymbol parameterSymbol in this._parameters)
				{
					if (originalBinder.CanAddLookupSymbolInfo(parameterSymbol, options, result, null, null))
					{
						result.AddSymbol(parameterSymbol, parameterSymbol.Name, 0);
					}
				}
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00051864 File Offset: 0x0004FA64
		internal override void LookupSymbolsInSingleBinder(LookupResult result, string name, int arity, ConsList<TypeSymbol> basesBeingResolved, LookupOptions options, Binder originalBinder, bool diagnose, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if ((options & (LookupOptions.NamespaceAliasesOnly | LookupOptions.MustBeInvocableIfMember)) != LookupOptions.Default)
			{
				return;
			}
			foreach (ParameterSymbol parameterSymbol in this._parameters)
			{
				if (parameterSymbol.Name == name)
				{
					result.MergeEqual(originalBinder.CheckViability(parameterSymbol, arity, options, null, diagnose, ref useSiteInfo, null));
				}
			}
		}

		// Token: 0x04000483 RID: 1155
		private readonly ImmutableArray<ParameterSymbol> _parameters;
	}
}
