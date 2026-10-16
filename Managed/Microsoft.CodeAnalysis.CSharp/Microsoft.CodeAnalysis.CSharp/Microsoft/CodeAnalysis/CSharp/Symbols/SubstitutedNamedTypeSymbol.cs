using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000564 RID: 1380
	internal abstract class SubstitutedNamedTypeSymbol : WrappedNamedTypeSymbol
	{
		// Token: 0x06007481 RID: 29825 RVA: 0x001B2F95 File Offset: 0x001B1195
		protected SubstitutedNamedTypeSymbol(Symbol newContainer, TypeMap map, NamedTypeSymbol originalDefinition, NamedTypeSymbol constructedFrom = null, bool unbound = false, NamedTypeSymbol.TupleExtraData tupleData = null)
			: base(originalDefinition, tupleData)
		{
			this._newContainer = newContainer;
			this._inputMap = map;
			this._unbound = unbound;
			if (constructedFrom != null)
			{
				this._lazyTypeParameters = constructedFrom.TypeParameters;
				this._lazyMap = map;
			}
		}

		// Token: 0x17002010 RID: 8208
		// (get) Token: 0x06007482 RID: 29826 RVA: 0x001B2FCE File Offset: 0x001B11CE
		public sealed override bool IsUnboundGenericType
		{
			get
			{
				return this._unbound;
			}
		}

		// Token: 0x17002011 RID: 8209
		// (get) Token: 0x06007483 RID: 29827 RVA: 0x001B2FD6 File Offset: 0x001B11D6
		private TypeMap Map
		{
			get
			{
				this.EnsureMapAndTypeParameters();
				return this._lazyMap;
			}
		}

		// Token: 0x17002012 RID: 8210
		// (get) Token: 0x06007484 RID: 29828 RVA: 0x001B2FE4 File Offset: 0x001B11E4
		public sealed override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				this.EnsureMapAndTypeParameters();
				return this._lazyTypeParameters;
			}
		}

		// Token: 0x06007485 RID: 29829 RVA: 0x001B2FF4 File Offset: 0x001B11F4
		private void EnsureMapAndTypeParameters()
		{
			if (!this._lazyTypeParameters.IsDefault)
			{
				return;
			}
			ImmutableArray<TypeParameterSymbol> immutableArray;
			TypeMap typeMap = this._inputMap.WithAlphaRename(this.OriginalDefinition, this, out immutableArray);
			TypeMap typeMap2 = Interlocked.CompareExchange<TypeMap>(ref this._lazyMap, typeMap, null);
			if (typeMap2 != null)
			{
				immutableArray = typeMap2.SubstituteTypeParameters(this.OriginalDefinition.TypeParameters);
			}
			ImmutableInterlocked.InterlockedCompareExchange<TypeParameterSymbol>(ref this._lazyTypeParameters, immutableArray, default(ImmutableArray<TypeParameterSymbol>));
		}

		// Token: 0x17002013 RID: 8211
		// (get) Token: 0x06007486 RID: 29830 RVA: 0x001B305D File Offset: 0x001B125D
		public sealed override Symbol ContainingSymbol
		{
			get
			{
				return this._newContainer;
			}
		}

		// Token: 0x17002014 RID: 8212
		// (get) Token: 0x06007487 RID: 29831 RVA: 0x001B3065 File Offset: 0x001B1265
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._newContainer as NamedTypeSymbol;
			}
		}

		// Token: 0x17002015 RID: 8213
		// (get) Token: 0x06007488 RID: 29832 RVA: 0x001B3072 File Offset: 0x001B1272
		public sealed override SymbolKind Kind
		{
			get
			{
				return this.OriginalDefinition.Kind;
			}
		}

		// Token: 0x17002016 RID: 8214
		// (get) Token: 0x06007489 RID: 29833 RVA: 0x001B307F File Offset: 0x001B127F
		public sealed override NamedTypeSymbol OriginalDefinition
		{
			get
			{
				return this._underlyingType;
			}
		}

		// Token: 0x0600748A RID: 29834 RVA: 0x001B3087 File Offset: 0x001B1287
		internal sealed override NamedTypeSymbol GetDeclaredBaseType(ConsList<TypeSymbol> basesBeingResolved)
		{
			if (!this._unbound)
			{
				return this.Map.SubstituteNamedType(this.OriginalDefinition.GetDeclaredBaseType(basesBeingResolved));
			}
			return null;
		}

		// Token: 0x0600748B RID: 29835 RVA: 0x001B30AA File Offset: 0x001B12AA
		internal sealed override ImmutableArray<NamedTypeSymbol> GetDeclaredInterfaces(ConsList<TypeSymbol> basesBeingResolved)
		{
			if (!this._unbound)
			{
				return this.Map.SubstituteNamedTypes(this.OriginalDefinition.GetDeclaredInterfaces(basesBeingResolved));
			}
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x17002017 RID: 8215
		// (get) Token: 0x0600748C RID: 29836 RVA: 0x001B30D1 File Offset: 0x001B12D1
		internal sealed override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				if (!this._unbound)
				{
					return this.Map.SubstituteNamedType(this.OriginalDefinition.BaseTypeNoUseSiteDiagnostics);
				}
				return null;
			}
		}

		// Token: 0x0600748D RID: 29837 RVA: 0x001B30F3 File Offset: 0x001B12F3
		internal sealed override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved)
		{
			if (!this._unbound)
			{
				return this.Map.SubstituteNamedTypes(this.OriginalDefinition.InterfacesNoUseSiteDiagnostics(basesBeingResolved));
			}
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x0600748E RID: 29838 RVA: 0x001B311A File Offset: 0x001B131A
		internal override ImmutableArray<NamedTypeSymbol> GetInterfacesToEmit()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600748F RID: 29839
		internal abstract override bool GetUnificationUseSiteDiagnosticRecursive(ref DiagnosticInfo result, Symbol owner, ref HashSet<TypeSymbol> checkedTypes);

		// Token: 0x17002018 RID: 8216
		// (get) Token: 0x06007490 RID: 29840 RVA: 0x001B3124 File Offset: 0x001B1324
		public sealed override IEnumerable<string> MemberNames
		{
			get
			{
				if (this._unbound)
				{
					return new List<string>(Enumerable.Distinct<string>(from s in this.GetTypeMembersUnordered()
						select s.Name));
				}
				if (this.IsTupleType)
				{
					return Enumerable.Distinct<string>(from s in this.GetMembers()
						select s.Name);
				}
				return this.OriginalDefinition.MemberNames;
			}
		}

		// Token: 0x06007491 RID: 29841 RVA: 0x001B31B1 File Offset: 0x001B13B1
		public sealed override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.OriginalDefinition.GetAttributes();
		}

		// Token: 0x06007492 RID: 29842 RVA: 0x001B31BE File Offset: 0x001B13BE
		internal sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembersUnordered()
		{
			return this.OriginalDefinition.GetTypeMembersUnordered().SelectAsArray((NamedTypeSymbol t, SubstitutedNamedTypeSymbol self) => t.AsMember(self), this);
		}

		// Token: 0x06007493 RID: 29843 RVA: 0x001B31F0 File Offset: 0x001B13F0
		public sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return this.OriginalDefinition.GetTypeMembers().SelectAsArray((NamedTypeSymbol t, SubstitutedNamedTypeSymbol self) => t.AsMember(self), this);
		}

		// Token: 0x06007494 RID: 29844 RVA: 0x001B3222 File Offset: 0x001B1422
		public sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return this.OriginalDefinition.GetTypeMembers(name).SelectAsArray((NamedTypeSymbol t, SubstitutedNamedTypeSymbol self) => t.AsMember(self), this);
		}

		// Token: 0x06007495 RID: 29845 RVA: 0x001B3255 File Offset: 0x001B1455
		public sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return this.OriginalDefinition.GetTypeMembers(name, arity).SelectAsArray((NamedTypeSymbol t, SubstitutedNamedTypeSymbol self) => t.AsMember(self), this);
		}

		// Token: 0x06007496 RID: 29846 RVA: 0x001B328C File Offset: 0x001B148C
		public sealed override ImmutableArray<Symbol> GetMembers()
		{
			ArrayBuilder<Symbol> arrayBuilder = ArrayBuilder<Symbol>.GetInstance();
			if (this._unbound)
			{
				foreach (Symbol symbol in this.OriginalDefinition.GetMembers())
				{
					if (symbol.Kind == SymbolKind.NamedType)
					{
						arrayBuilder.Add(((NamedTypeSymbol)symbol).AsMember(this));
					}
				}
			}
			else
			{
				foreach (Symbol symbol2 in this.OriginalDefinition.GetMembers())
				{
					arrayBuilder.Add(symbol2.SymbolAsMember(this));
				}
			}
			if (this.IsTupleType)
			{
				arrayBuilder = base.AddOrWrapTupleMembers(arrayBuilder.ToImmutableAndFree());
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x06007497 RID: 29847 RVA: 0x001B3340 File Offset: 0x001B1540
		internal sealed override ImmutableArray<Symbol> GetMembersUnordered()
		{
			ArrayBuilder<Symbol> arrayBuilder = ArrayBuilder<Symbol>.GetInstance();
			if (this._unbound)
			{
				foreach (Symbol symbol in this.OriginalDefinition.GetMembersUnordered())
				{
					if (symbol.Kind == SymbolKind.NamedType)
					{
						arrayBuilder.Add(((NamedTypeSymbol)symbol).AsMember(this));
					}
				}
			}
			else
			{
				foreach (Symbol symbol2 in this.OriginalDefinition.GetMembersUnordered())
				{
					arrayBuilder.Add(symbol2.SymbolAsMember(this));
				}
			}
			if (this.IsTupleType)
			{
				arrayBuilder = base.AddOrWrapTupleMembers(arrayBuilder.ToImmutableAndFree());
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x06007498 RID: 29848 RVA: 0x001B33F4 File Offset: 0x001B15F4
		public sealed override ImmutableArray<Symbol> GetMembers(string name)
		{
			if (this._unbound)
			{
				return StaticCast<Symbol>.From<NamedTypeSymbol>(this.GetTypeMembers(name));
			}
			ConcurrentCache<string, ImmutableArray<Symbol>> lazyMembersByNameCache = this._lazyMembersByNameCache;
			ImmutableArray<Symbol> immutableArray;
			if (lazyMembersByNameCache != null && lazyMembersByNameCache.TryGetValue(name, out immutableArray))
			{
				return immutableArray;
			}
			return this.GetMembersWorker(name);
		}

		// Token: 0x06007499 RID: 29849 RVA: 0x001B3434 File Offset: 0x001B1634
		private ImmutableArray<Symbol> GetMembersWorker(string name)
		{
			SubstitutedNamedTypeSymbol.<>c__DisplayClass41_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.name = name;
			if (this.IsTupleType)
			{
				ImmutableArray<Symbol> immutableArray = this.GetMembers().WhereAsArray((Symbol m, string name) => m.Name == name, CS$<>8__locals1.name);
				this.<GetMembersWorker>g__cacheResult|41_0(immutableArray, ref CS$<>8__locals1);
				return immutableArray;
			}
			ImmutableArray<Symbol> members = this.OriginalDefinition.GetMembers(CS$<>8__locals1.name);
			if (members.IsDefaultOrEmpty)
			{
				return members;
			}
			ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance(members.Length);
			foreach (Symbol symbol in members)
			{
				instance.Add(symbol.SymbolAsMember(this));
			}
			ImmutableArray<Symbol> immutableArray2 = instance.ToImmutableAndFree();
			this.<GetMembersWorker>g__cacheResult|41_0(immutableArray2, ref CS$<>8__locals1);
			return immutableArray2;
		}

		// Token: 0x0600749A RID: 29850 RVA: 0x001B34FE File Offset: 0x001B16FE
		internal override IEnumerable<FieldSymbol> GetFieldsToEmit()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600749B RID: 29851 RVA: 0x001B3505 File Offset: 0x001B1705
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers()
		{
			if (!this._unbound)
			{
				return this.OriginalDefinition.GetEarlyAttributeDecodingMembers().SelectAsArray(SubstitutedNamedTypeSymbol.s_symbolAsMemberFunc, this);
			}
			return this.GetMembers();
		}

		// Token: 0x0600749C RID: 29852 RVA: 0x001B352C File Offset: 0x001B172C
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers(string name)
		{
			if (this._unbound)
			{
				return this.GetMembers(name);
			}
			ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance();
			foreach (Symbol symbol in this.OriginalDefinition.GetEarlyAttributeDecodingMembers(name))
			{
				instance.Add(symbol.SymbolAsMember(this));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x17002019 RID: 8217
		// (get) Token: 0x0600749D RID: 29853 RVA: 0x001B358A File Offset: 0x001B178A
		public sealed override NamedTypeSymbol EnumUnderlyingType
		{
			get
			{
				return this.OriginalDefinition.EnumUnderlyingType;
			}
		}

		// Token: 0x0600749E RID: 29854 RVA: 0x001B3597 File Offset: 0x001B1797
		public override int GetHashCode()
		{
			if (this._hashCode == 0)
			{
				this._hashCode = this.ComputeHashCode();
			}
			return this._hashCode;
		}

		// Token: 0x1700201A RID: 8218
		// (get) Token: 0x0600749F RID: 29855 RVA: 0x001B35B3 File Offset: 0x001B17B3
		internal sealed override TypeMap TypeSubstitution
		{
			get
			{
				return this.Map;
			}
		}

		// Token: 0x1700201B RID: 8219
		// (get) Token: 0x060074A0 RID: 29856 RVA: 0x001B35BB File Offset: 0x001B17BB
		internal sealed override bool IsComImport
		{
			get
			{
				return this.OriginalDefinition.IsComImport;
			}
		}

		// Token: 0x1700201C RID: 8220
		// (get) Token: 0x060074A1 RID: 29857 RVA: 0x001B35C8 File Offset: 0x001B17C8
		internal sealed override NamedTypeSymbol ComImportCoClass
		{
			get
			{
				return this.OriginalDefinition.ComImportCoClass;
			}
		}

		// Token: 0x060074A2 RID: 29858 RVA: 0x001B35D5 File Offset: 0x001B17D5
		internal override IEnumerable<MethodSymbol> GetMethodsToEmit()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060074A3 RID: 29859 RVA: 0x001B35DC File Offset: 0x001B17DC
		internal override IEnumerable<EventSymbol> GetEventsToEmit()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060074A4 RID: 29860 RVA: 0x001B35E3 File Offset: 0x001B17E3
		internal override IEnumerable<PropertySymbol> GetPropertiesToEmit()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060074A5 RID: 29861 RVA: 0x001B35EA File Offset: 0x001B17EA
		internal override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060074A6 RID: 29862 RVA: 0x001B35F1 File Offset: 0x001B17F1
		internal sealed override NamedTypeSymbol AsNativeInteger()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700201D RID: 8221
		// (get) Token: 0x060074A7 RID: 29863 RVA: 0x001B35F8 File Offset: 0x001B17F8
		internal sealed override NamedTypeSymbol NativeIntegerUnderlyingType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700201E RID: 8222
		// (get) Token: 0x060074A8 RID: 29864 RVA: 0x001B35FB File Offset: 0x001B17FB
		internal sealed override bool IsRecord
		{
			get
			{
				return this._underlyingType.IsRecord;
			}
		}

		// Token: 0x1700201F RID: 8223
		// (get) Token: 0x060074A9 RID: 29865 RVA: 0x001B3608 File Offset: 0x001B1808
		internal sealed override bool IsRecordStruct
		{
			get
			{
				return this._underlyingType.IsRecordStruct;
			}
		}

		// Token: 0x060074AA RID: 29866 RVA: 0x001B3615 File Offset: 0x001B1815
		internal sealed override bool HasPossibleWellKnownCloneMethod()
		{
			return this._underlyingType.HasPossibleWellKnownCloneMethod();
		}

		// Token: 0x060074AC RID: 29868 RVA: 0x001B3638 File Offset: 0x001B1838
		[CompilerGenerated]
		private void <GetMembersWorker>g__cacheResult|41_0(ImmutableArray<Symbol> result, ref SubstitutedNamedTypeSymbol.<>c__DisplayClass41_0 A_2)
		{
			ConcurrentCache<string, ImmutableArray<Symbol>> concurrentCache;
			if ((concurrentCache = this._lazyMembersByNameCache) == null)
			{
				concurrentCache = (this._lazyMembersByNameCache = new ConcurrentCache<string, ImmutableArray<Symbol>>(8));
			}
			concurrentCache.TryAdd(A_2.name, result);
		}

		// Token: 0x04001C50 RID: 7248
		private static readonly Func<Symbol, NamedTypeSymbol, Symbol> s_symbolAsMemberFunc = new Func<Symbol, NamedTypeSymbol, Symbol>(SymbolExtensions.SymbolAsMember);

		// Token: 0x04001C51 RID: 7249
		private readonly bool _unbound;

		// Token: 0x04001C52 RID: 7250
		private readonly TypeMap _inputMap;

		// Token: 0x04001C53 RID: 7251
		private readonly Symbol _newContainer;

		// Token: 0x04001C54 RID: 7252
		private TypeMap _lazyMap;

		// Token: 0x04001C55 RID: 7253
		private ImmutableArray<TypeParameterSymbol> _lazyTypeParameters;

		// Token: 0x04001C56 RID: 7254
		private int _hashCode;

		// Token: 0x04001C57 RID: 7255
		private ConcurrentCache<string, ImmutableArray<Symbol>> _lazyMembersByNameCache;
	}
}
