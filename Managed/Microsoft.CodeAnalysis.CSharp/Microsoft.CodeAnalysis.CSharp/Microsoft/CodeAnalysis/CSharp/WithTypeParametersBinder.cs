using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000080 RID: 128
	internal abstract class WithTypeParametersBinder : Binder
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x000518C0 File Offset: 0x0004FAC0
		internal WithTypeParametersBinder(Binder next)
			: base(next, null)
		{
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000B08 RID: 2824
		protected abstract MultiDictionary<string, TypeParameterSymbol> TypeParameterMap { get; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x000518CA File Offset: 0x0004FACA
		protected virtual LookupOptions LookupMask
		{
			get
			{
				return LookupOptions.NamespaceAliasesOnly | LookupOptions.MustBeInvocableIfMember;
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000518CE File Offset: 0x0004FACE
		protected bool CanConsiderTypeParameters(LookupOptions options)
		{
			return (options & (this.LookupMask | LookupOptions.MustBeInstance | LookupOptions.LabelsOnly)) == LookupOptions.Default;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000518E4 File Offset: 0x0004FAE4
		internal override void LookupSymbolsInSingleBinder(LookupResult result, string name, int arity, ConsList<TypeSymbol> basesBeingResolved, LookupOptions options, Binder originalBinder, bool diagnose, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if ((options & this.LookupMask) != LookupOptions.Default)
			{
				return;
			}
			foreach (TypeParameterSymbol typeParameterSymbol in this.TypeParameterMap[name])
			{
				result.MergeEqual(originalBinder.CheckViability(typeParameterSymbol, arity, options, null, diagnose, ref useSiteInfo, null));
			}
		}
	}
}
