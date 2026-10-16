using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000153 RID: 339
	internal class EmptyStructTypeCache
	{
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x00090C60 File Offset: 0x0008EE60
		private SmallDictionary<NamedTypeSymbol, bool> Cache
		{
			get
			{
				SmallDictionary<NamedTypeSymbol, bool> smallDictionary;
				if ((smallDictionary = this._cache) == null)
				{
					smallDictionary = (this._cache = new SmallDictionary<NamedTypeSymbol, bool>(SymbolEqualityComparer.ConsiderEverything));
				}
				return smallDictionary;
			}
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00090C8A File Offset: 0x0008EE8A
		public static EmptyStructTypeCache CreateForDev12Compatibility(CSharpCompilation compilation)
		{
			return new EmptyStructTypeCache(compilation, true);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x00090C93 File Offset: 0x0008EE93
		public static EmptyStructTypeCache CreatePrecise()
		{
			return new EmptyStructTypeCache(null, false);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00090C9C File Offset: 0x0008EE9C
		public static EmptyStructTypeCache CreateNeverEmpty()
		{
			return new EmptyStructTypeCache.NeverEmptyStructTypeCache();
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00090CA3 File Offset: 0x0008EEA3
		private EmptyStructTypeCache(CSharpCompilation compilation, bool dev12CompilerCompatibility)
		{
			this._dev12CompilerCompatibility = dev12CompilerCompatibility;
			this._sourceAssembly = ((compilation != null) ? compilation.SourceAssembly : null);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00090CC4 File Offset: 0x0008EEC4
		public virtual bool IsEmptyStructType(TypeSymbol type)
		{
			return this.IsEmptyStructType(type, ConsList<NamedTypeSymbol>.Empty);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00090CD4 File Offset: 0x0008EED4
		private bool IsEmptyStructType(TypeSymbol type, ConsList<NamedTypeSymbol> typesWithMembersOfThisType)
		{
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			if (namedTypeSymbol == null || !EmptyStructTypeCache.IsTrackableStructType(namedTypeSymbol))
			{
				return false;
			}
			bool flag;
			if (this.Cache.TryGetValue(namedTypeSymbol, out flag))
			{
				return flag;
			}
			flag = this.CheckStruct(typesWithMembersOfThisType, namedTypeSymbol);
			this.Cache[namedTypeSymbol] = flag;
			return flag;
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x00090D1E File Offset: 0x0008EF1E
		private bool CheckStruct(ConsList<NamedTypeSymbol> typesWithMembersOfThisType, NamedTypeSymbol nts)
		{
			if (!typesWithMembersOfThisType.ContainsReference(nts))
			{
				typesWithMembersOfThisType = new ConsList<NamedTypeSymbol>(nts, typesWithMembersOfThisType);
				return this.CheckStructInstanceFields(typesWithMembersOfThisType, nts);
			}
			return true;
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x00090D3C File Offset: 0x0008EF3C
		public static bool IsTrackableStructType(TypeSymbol type)
		{
			if (type == null)
			{
				return false;
			}
			NamedTypeSymbol namedTypeSymbol = type.OriginalDefinition as NamedTypeSymbol;
			return namedTypeSymbol != null && (namedTypeSymbol.IsStructType() && namedTypeSymbol.SpecialType == SpecialType.None) && !namedTypeSymbol.KnownCircularStruct;
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x00090D7C File Offset: 0x0008EF7C
		private bool CheckStructInstanceFields(ConsList<NamedTypeSymbol> typesWithMembersOfThisType, NamedTypeSymbol type)
		{
			foreach (Symbol symbol in type.OriginalDefinition.GetMembersUnordered())
			{
				if (!symbol.IsStatic)
				{
					FieldSymbol actualField = this.GetActualField(symbol, type);
					if (actualField != null)
					{
						TypeSymbol type2 = actualField.Type;
						if (!this.IsEmptyStructType(type2, typesWithMembersOfThisType))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x00090DDC File Offset: 0x0008EFDC
		public IEnumerable<FieldSymbol> GetStructInstanceFields(TypeSymbol type)
		{
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return SpecializedCollections.EmptyEnumerable<FieldSymbol>();
			}
			return this.GetStructFields(namedTypeSymbol, false);
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x00090E01 File Offset: 0x0008F001
		public IEnumerable<FieldSymbol> GetStructFields(NamedTypeSymbol type, bool includeStatic)
		{
			foreach (Symbol symbol in type.OriginalDefinition.GetMembersUnordered())
			{
				if (includeStatic || !symbol.IsStatic)
				{
					FieldSymbol actualField = this.GetActualField(symbol, type);
					if (actualField != null)
					{
						yield return actualField;
					}
				}
			}
			ImmutableArray<Symbol>.Enumerator enumerator = default(ImmutableArray<Symbol>.Enumerator);
			yield break;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x00090E20 File Offset: 0x0008F020
		private FieldSymbol GetActualField(Symbol member, NamedTypeSymbol type)
		{
			SymbolKind kind = member.Kind;
			if (kind != SymbolKind.Event)
			{
				if (kind != SymbolKind.Field)
				{
					return null;
				}
				FieldSymbol fieldSymbol = (FieldSymbol)member;
				if (fieldSymbol.IsVirtualTupleField)
				{
					return null;
				}
				if (!fieldSymbol.IsFixedSizeBuffer && !this.ShouldIgnoreStructField(fieldSymbol, fieldSymbol.Type))
				{
					return fieldSymbol.AsMember(type);
				}
				return null;
			}
			else
			{
				EventSymbol eventSymbol = (EventSymbol)member;
				if (eventSymbol.HasAssociatedField && !this.ShouldIgnoreStructField(eventSymbol, eventSymbol.Type))
				{
					return eventSymbol.AssociatedField.AsMember(type);
				}
				return null;
			}
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x00090E9C File Offset: 0x0008F09C
		private bool ShouldIgnoreStructField(Symbol member, TypeSymbol memberType)
		{
			return this._dev12CompilerCompatibility && (member.ContainingAssembly != this._sourceAssembly || member.ContainingModule.Ordinal != 0) && EmptyStructTypeCache.IsIgnorableType(memberType) && !EmptyStructTypeCache.IsAccessibleInAssembly(member, this._sourceAssembly);
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00090EDC File Offset: 0x0008F0DC
		private static bool IsIgnorableType(TypeSymbol type)
		{
			TypeKind typeKind;
			for (;;)
			{
				typeKind = type.TypeKind;
				if (typeKind != TypeKind.Array)
				{
					break;
				}
				type = ((ArrayTypeSymbol)type).BaseTypeNoUseSiteDiagnostics;
			}
			return typeKind != TypeKind.Enum && typeKind - TypeKind.Struct > 1;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x00090F14 File Offset: 0x0008F114
		private static bool IsAccessibleInAssembly(Symbol symbol, SourceAssemblySymbol assembly)
		{
			while (symbol != null && symbol.Kind != SymbolKind.Namespace)
			{
				switch (symbol.DeclaredAccessibility)
				{
				case Accessibility.Private:
					return false;
				case Accessibility.ProtectedAndInternal:
				case Accessibility.Internal:
					if (!assembly.HasInternalAccessTo(symbol.ContainingAssembly))
					{
						return false;
					}
					break;
				}
				symbol = symbol.ContainingSymbol;
			}
			return true;
		}

		// Token: 0x04000E3D RID: 3645
		private SmallDictionary<NamedTypeSymbol, bool> _cache;

		// Token: 0x04000E3E RID: 3646
		private readonly bool _dev12CompilerCompatibility;

		// Token: 0x04000E3F RID: 3647
		private readonly SourceAssemblySymbol _sourceAssembly;

		// Token: 0x020006ED RID: 1773
		private sealed class NeverEmptyStructTypeCache : EmptyStructTypeCache
		{
			// Token: 0x060084DA RID: 34010 RVA: 0x001E10C1 File Offset: 0x001DF2C1
			public NeverEmptyStructTypeCache()
				: base(null, false)
			{
			}

			// Token: 0x060084DB RID: 34011 RVA: 0x001E10CB File Offset: 0x001DF2CB
			public override bool IsEmptyStructType(TypeSymbol type)
			{
				return false;
			}
		}
	}
}
