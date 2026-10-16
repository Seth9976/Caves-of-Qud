using System;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200050B RID: 1291
	internal sealed class OverriddenOrHiddenMembersResult
	{
		// Token: 0x17001C16 RID: 7190
		// (get) Token: 0x06006CA5 RID: 27813 RVA: 0x00191994 File Offset: 0x0018FB94
		public ImmutableArray<Symbol> OverriddenMembers
		{
			get
			{
				return this._overriddenMembers;
			}
		}

		// Token: 0x17001C17 RID: 7191
		// (get) Token: 0x06006CA6 RID: 27814 RVA: 0x0019199C File Offset: 0x0018FB9C
		public ImmutableArray<Symbol> HiddenMembers
		{
			get
			{
				return this._hiddenMembers;
			}
		}

		// Token: 0x06006CA7 RID: 27815 RVA: 0x001919A4 File Offset: 0x0018FBA4
		private OverriddenOrHiddenMembersResult(ImmutableArray<Symbol> overriddenMembers, ImmutableArray<Symbol> hiddenMembers)
		{
			this._overriddenMembers = overriddenMembers;
			this._hiddenMembers = hiddenMembers;
		}

		// Token: 0x06006CA8 RID: 27816 RVA: 0x001919BA File Offset: 0x0018FBBA
		public static OverriddenOrHiddenMembersResult Create(ImmutableArray<Symbol> overriddenMembers, ImmutableArray<Symbol> hiddenMembers)
		{
			if (overriddenMembers.IsEmpty && hiddenMembers.IsEmpty)
			{
				return OverriddenOrHiddenMembersResult.Empty;
			}
			return new OverriddenOrHiddenMembersResult(overriddenMembers, hiddenMembers);
		}

		// Token: 0x06006CA9 RID: 27817 RVA: 0x001919DC File Offset: 0x0018FBDC
		internal static Symbol GetOverriddenMember(Symbol substitutedOverridingMember, Symbol overriddenByDefinitionMember)
		{
			if (overriddenByDefinitionMember != null)
			{
				NamedTypeSymbol containingType = overriddenByDefinitionMember.ContainingType;
				NamedTypeSymbol originalDefinition = containingType.OriginalDefinition;
				NamedTypeSymbol namedTypeSymbol = substitutedOverridingMember.ContainingType.BaseTypeNoUseSiteDiagnostics;
				while (namedTypeSymbol != null)
				{
					if (TypeSymbol.Equals(namedTypeSymbol.OriginalDefinition, originalDefinition, TypeCompareKind.ConsiderEverything))
					{
						if (TypeSymbol.Equals(namedTypeSymbol, containingType, TypeCompareKind.ConsiderEverything))
						{
							return overriddenByDefinitionMember;
						}
						return overriddenByDefinitionMember.OriginalDefinition.SymbolAsMember(namedTypeSymbol);
					}
					else
					{
						namedTypeSymbol = namedTypeSymbol.BaseTypeNoUseSiteDiagnostics;
					}
				}
				throw ExceptionUtilities.Unreachable;
			}
			return null;
		}

		// Token: 0x06006CAA RID: 27818 RVA: 0x00191A44 File Offset: 0x0018FC44
		internal Symbol GetOverriddenMember()
		{
			foreach (Symbol symbol in this._overriddenMembers)
			{
				if (symbol.IsAbstract || symbol.IsVirtual || symbol.IsOverride)
				{
					return symbol;
				}
			}
			return null;
		}

		// Token: 0x04001AE4 RID: 6884
		public static readonly OverriddenOrHiddenMembersResult Empty = new OverriddenOrHiddenMembersResult(ImmutableArray<Symbol>.Empty, ImmutableArray<Symbol>.Empty);

		// Token: 0x04001AE5 RID: 6885
		private readonly ImmutableArray<Symbol> _overriddenMembers;

		// Token: 0x04001AE6 RID: 6886
		private readonly ImmutableArray<Symbol> _hiddenMembers;
	}
}
