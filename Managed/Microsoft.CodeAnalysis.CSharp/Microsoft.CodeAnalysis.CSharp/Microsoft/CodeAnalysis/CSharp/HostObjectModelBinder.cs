using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000029 RID: 41
	internal sealed class HostObjectModelBinder : Binder
	{
		// Token: 0x060005E7 RID: 1511 RVA: 0x000386A6 File Offset: 0x000368A6
		public HostObjectModelBinder(Binder next)
			: base(next, null)
		{
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000386B0 File Offset: 0x000368B0
		private TypeSymbol GetHostObjectType()
		{
			return base.Compilation.GetHostObjectTypeSymbol();
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000386C0 File Offset: 0x000368C0
		internal override void LookupSymbolsInSingleBinder(LookupResult result, string name, int arity, ConsList<TypeSymbol> basesBeingResolved, LookupOptions options, Binder originalBinder, bool diagnose, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			TypeSymbol hostObjectType = this.GetHostObjectType();
			if (hostObjectType.Kind == SymbolKind.ErrorType)
			{
				result.SetFrom(new CSDiagnosticInfo(ErrorCode.ERR_NameNotInContextPossibleMissingReference, new object[]
				{
					name,
					((MissingMetadataTypeSymbol)hostObjectType).ContainingAssembly.Identity
				}, ImmutableArray<Symbol>.Empty, ImmutableArray<Location>.Empty));
				return;
			}
			base.LookupMembersInternal(result, hostObjectType, name, arity, basesBeingResolved, options, originalBinder, diagnose, ref useSiteInfo);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0003872C File Offset: 0x0003692C
		protected override void AddLookupSymbolsInfoInSingleBinder(LookupSymbolsInfo result, LookupOptions options, Binder originalBinder)
		{
			TypeSymbol hostObjectType = this.GetHostObjectType();
			if (hostObjectType.Kind != SymbolKind.ErrorType)
			{
				base.AddMemberLookupSymbolsInfo(result, hostObjectType, options, originalBinder);
			}
		}
	}
}
