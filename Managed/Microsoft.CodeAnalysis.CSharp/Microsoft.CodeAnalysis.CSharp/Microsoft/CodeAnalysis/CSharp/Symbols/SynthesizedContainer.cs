using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.InteropServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000582 RID: 1410
	internal abstract class SynthesizedContainer : NamedTypeSymbol
	{
		// Token: 0x060075C0 RID: 30144 RVA: 0x001B6AF8 File Offset: 0x001B4CF8
		protected SynthesizedContainer(string name, int parameterCount, bool returnsVoid)
			: base(null)
		{
			this.Name = name;
			this.TypeMap = TypeMap.Empty;
			this._typeParameters = this.CreateTypeParameters(parameterCount, returnsVoid);
			this._constructedFromTypeParameters = default(ImmutableArray<TypeParameterSymbol>);
		}

		// Token: 0x060075C1 RID: 30145 RVA: 0x001B6B30 File Offset: 0x001B4D30
		protected SynthesizedContainer(string name, MethodSymbol containingMethod)
			: base(null)
		{
			this.Name = name;
			if (containingMethod == null)
			{
				this.TypeMap = TypeMap.Empty;
				this._typeParameters = ImmutableArray<TypeParameterSymbol>.Empty;
				return;
			}
			this.TypeMap = TypeMap.Empty.WithConcatAlphaRename(containingMethod, this, out this._typeParameters, out this._constructedFromTypeParameters, null);
		}

		// Token: 0x060075C2 RID: 30146 RVA: 0x001B6B8A File Offset: 0x001B4D8A
		protected SynthesizedContainer(string name, ImmutableArray<TypeParameterSymbol> typeParameters, TypeMap typeMap)
			: base(null)
		{
			this.Name = name;
			this._typeParameters = typeParameters;
			this.TypeMap = typeMap;
		}

		// Token: 0x060075C3 RID: 30147 RVA: 0x001B6BA8 File Offset: 0x001B4DA8
		private ImmutableArray<TypeParameterSymbol> CreateTypeParameters(int parameterCount, bool returnsVoid)
		{
			ArrayBuilder<TypeParameterSymbol> instance = ArrayBuilder<TypeParameterSymbol>.GetInstance(parameterCount + (returnsVoid ? 0 : 1));
			for (int i = 0; i < parameterCount; i++)
			{
				instance.Add(new AnonymousTypeManager.AnonymousTypeParameterSymbol(this, i, "T" + (i + 1).ToString()));
			}
			if (!returnsVoid)
			{
				instance.Add(new AnonymousTypeManager.AnonymousTypeParameterSymbol(this, parameterCount, "TResult"));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x17002077 RID: 8311
		// (get) Token: 0x060075C4 RID: 30148 RVA: 0x001B6C0D File Offset: 0x001B4E0D
		internal TypeMap TypeMap { get; }

		// Token: 0x17002078 RID: 8312
		// (get) Token: 0x060075C5 RID: 30149 RVA: 0x001B6C15 File Offset: 0x001B4E15
		internal virtual MethodSymbol Constructor
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002079 RID: 8313
		// (get) Token: 0x060075C6 RID: 30150 RVA: 0x001B6C18 File Offset: 0x001B4E18
		internal sealed override bool IsInterface
		{
			get
			{
				return this.TypeKind == TypeKind.Interface;
			}
		}

		// Token: 0x060075C7 RID: 30151 RVA: 0x001B6C24 File Offset: 0x001B4E24
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			if (this.ContainingSymbol.Kind == SymbolKind.NamedType && (this.ContainingSymbol.IsImplicitlyDeclared || this.ContainingSymbol is SimpleProgramNamedTypeSymbol))
			{
				return;
			}
			CSharpCompilation declaringCompilation = this.ContainingSymbol.DeclaringCompilation;
			Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_CompilerGeneratedAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
		}

		// Token: 0x060075C8 RID: 30152 RVA: 0x001B6C90 File Offset: 0x001B4E90
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700207A RID: 8314
		// (get) Token: 0x060075C9 RID: 30153 RVA: 0x001B6C97 File Offset: 0x001B4E97
		internal ImmutableArray<TypeParameterSymbol> ConstructedFromTypeParameters
		{
			get
			{
				return this._constructedFromTypeParameters;
			}
		}

		// Token: 0x1700207B RID: 8315
		// (get) Token: 0x060075CA RID: 30154 RVA: 0x001B6C9F File Offset: 0x001B4E9F
		public sealed override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return this._typeParameters;
			}
		}

		// Token: 0x1700207C RID: 8316
		// (get) Token: 0x060075CB RID: 30155 RVA: 0x001B6CA7 File Offset: 0x001B4EA7
		public sealed override string Name { get; }

		// Token: 0x1700207D RID: 8317
		// (get) Token: 0x060075CC RID: 30156 RVA: 0x001B6CAF File Offset: 0x001B4EAF
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x1700207E RID: 8318
		// (get) Token: 0x060075CD RID: 30157 RVA: 0x001B6CB6 File Offset: 0x001B4EB6
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x1700207F RID: 8319
		// (get) Token: 0x060075CE RID: 30158 RVA: 0x001B6CBD File Offset: 0x001B4EBD
		public override IEnumerable<string> MemberNames
		{
			get
			{
				return SpecializedCollections.EmptyEnumerable<string>();
			}
		}

		// Token: 0x17002080 RID: 8320
		// (get) Token: 0x060075CF RID: 30159 RVA: 0x001B6CC4 File Offset: 0x001B4EC4
		public override NamedTypeSymbol ConstructedFrom
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002081 RID: 8321
		// (get) Token: 0x060075D0 RID: 30160 RVA: 0x001B6CC7 File Offset: 0x001B4EC7
		public override bool IsSealed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002082 RID: 8322
		// (get) Token: 0x060075D1 RID: 30161 RVA: 0x001B6CCA File Offset: 0x001B4ECA
		public override bool IsAbstract
		{
			get
			{
				return this.Constructor == null && this.TypeKind != TypeKind.Struct;
			}
		}

		// Token: 0x17002083 RID: 8323
		// (get) Token: 0x060075D2 RID: 30162 RVA: 0x001B6CE3 File Offset: 0x001B4EE3
		internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
		{
			get
			{
				return base.GetTypeParametersAsTypeArguments();
			}
		}

		// Token: 0x17002084 RID: 8324
		// (get) Token: 0x060075D3 RID: 30163 RVA: 0x001B6CEB File Offset: 0x001B4EEB
		internal override bool HasCodeAnalysisEmbeddedAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060075D4 RID: 30164 RVA: 0x001B6CF0 File Offset: 0x001B4EF0
		public override ImmutableArray<Symbol> GetMembers()
		{
			Symbol constructor = this.Constructor;
			if (constructor != null)
			{
				return ImmutableArray.Create<Symbol>(constructor);
			}
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x060075D5 RID: 30165 RVA: 0x001B6D14 File Offset: 0x001B4F14
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			MethodSymbol constructor = this.Constructor;
			if (constructor == null || !(name == constructor.Name))
			{
				return ImmutableArray<Symbol>.Empty;
			}
			return ImmutableArray.Create<Symbol>(constructor);
		}

		// Token: 0x060075D6 RID: 30166 RVA: 0x001B6D45 File Offset: 0x001B4F45
		internal override IEnumerable<FieldSymbol> GetFieldsToEmit()
		{
			foreach (Symbol symbol in this.GetMembers())
			{
				if (symbol.Kind == SymbolKind.Field)
				{
					yield return (FieldSymbol)symbol;
				}
			}
			ImmutableArray<Symbol>.Enumerator enumerator = default(ImmutableArray<Symbol>.Enumerator);
			yield break;
		}

		// Token: 0x060075D7 RID: 30167 RVA: 0x001B6D55 File Offset: 0x001B4F55
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers()
		{
			return this.GetMembersUnordered();
		}

		// Token: 0x060075D8 RID: 30168 RVA: 0x001B6D5D File Offset: 0x001B4F5D
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers(string name)
		{
			return this.GetMembers(name);
		}

		// Token: 0x060075D9 RID: 30169 RVA: 0x001B6D66 File Offset: 0x001B4F66
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x060075DA RID: 30170 RVA: 0x001B6D6D File Offset: 0x001B4F6D
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x060075DB RID: 30171 RVA: 0x001B6D74 File Offset: 0x001B4F74
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x17002085 RID: 8325
		// (get) Token: 0x060075DC RID: 30172 RVA: 0x001B6D7B File Offset: 0x001B4F7B
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Private;
			}
		}

		// Token: 0x17002086 RID: 8326
		// (get) Token: 0x060075DD RID: 30173 RVA: 0x001B6D7E File Offset: 0x001B4F7E
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002087 RID: 8327
		// (get) Token: 0x060075DE RID: 30174 RVA: 0x001B6D81 File Offset: 0x001B4F81
		public sealed override bool IsRefLikeType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002088 RID: 8328
		// (get) Token: 0x060075DF RID: 30175 RVA: 0x001B6D84 File Offset: 0x001B4F84
		public sealed override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060075E0 RID: 30176 RVA: 0x001B6D87 File Offset: 0x001B4F87
		internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x060075E1 RID: 30177 RVA: 0x001B6D8E File Offset: 0x001B4F8E
		internal override ImmutableArray<NamedTypeSymbol> GetInterfacesToEmit()
		{
			return base.CalculateInterfacesToEmit();
		}

		// Token: 0x17002089 RID: 8329
		// (get) Token: 0x060075E2 RID: 30178 RVA: 0x001B6D96 File Offset: 0x001B4F96
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				return this.ContainingAssembly.GetSpecialType((this.TypeKind == TypeKind.Struct) ? SpecialType.System_ValueType : SpecialType.System_Object);
			}
		}

		// Token: 0x060075E3 RID: 30179 RVA: 0x001B6DB1 File Offset: 0x001B4FB1
		internal override NamedTypeSymbol GetDeclaredBaseType(ConsList<TypeSymbol> basesBeingResolved)
		{
			return this.BaseTypeNoUseSiteDiagnostics;
		}

		// Token: 0x060075E4 RID: 30180 RVA: 0x001B6DB9 File Offset: 0x001B4FB9
		internal override ImmutableArray<NamedTypeSymbol> GetDeclaredInterfaces(ConsList<TypeSymbol> basesBeingResolved)
		{
			return this.InterfacesNoUseSiteDiagnostics(basesBeingResolved);
		}

		// Token: 0x1700208A RID: 8330
		// (get) Token: 0x060075E5 RID: 30181 RVA: 0x001B6DC2 File Offset: 0x001B4FC2
		public override bool MightContainExtensionMethods
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700208B RID: 8331
		// (get) Token: 0x060075E6 RID: 30182 RVA: 0x001B6DC8 File Offset: 0x001B4FC8
		public override int Arity
		{
			get
			{
				return this.TypeParameters.Length;
			}
		}

		// Token: 0x1700208C RID: 8332
		// (get) Token: 0x060075E7 RID: 30183 RVA: 0x001B6DE3 File Offset: 0x001B4FE3
		internal override bool MangleName
		{
			get
			{
				return this.Arity > 0;
			}
		}

		// Token: 0x1700208D RID: 8333
		// (get) Token: 0x060075E8 RID: 30184 RVA: 0x001B6DEE File Offset: 0x001B4FEE
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700208E RID: 8334
		// (get) Token: 0x060075E9 RID: 30185 RVA: 0x001B6DF1 File Offset: 0x001B4FF1
		internal override bool ShouldAddWinRTMembers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700208F RID: 8335
		// (get) Token: 0x060075EA RID: 30186 RVA: 0x001B6DF4 File Offset: 0x001B4FF4
		internal override bool IsWindowsRuntimeImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002090 RID: 8336
		// (get) Token: 0x060075EB RID: 30187 RVA: 0x001B6DF7 File Offset: 0x001B4FF7
		internal override bool IsComImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002091 RID: 8337
		// (get) Token: 0x060075EC RID: 30188 RVA: 0x001B6DFA File Offset: 0x001B4FFA
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060075ED RID: 30189 RVA: 0x001B6DFD File Offset: 0x001B4FFD
		internal sealed override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x17002092 RID: 8338
		// (get) Token: 0x060075EE RID: 30190 RVA: 0x001B6E04 File Offset: 0x001B5004
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002093 RID: 8339
		// (get) Token: 0x060075EF RID: 30191 RVA: 0x001B6E07 File Offset: 0x001B5007
		internal override CharSet MarshallingCharSet
		{
			get
			{
				return base.DefaultMarshallingCharSet;
			}
		}

		// Token: 0x17002094 RID: 8340
		// (get) Token: 0x060075F0 RID: 30192 RVA: 0x001B6E0F File Offset: 0x001B500F
		public override bool IsSerializable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060075F1 RID: 30193 RVA: 0x001B6E12 File Offset: 0x001B5012
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060075F2 RID: 30194 RVA: 0x001B6E1C File Offset: 0x001B501C
		internal override AttributeUsageInfo GetAttributeUsageInfo()
		{
			return default(AttributeUsageInfo);
		}

		// Token: 0x17002095 RID: 8341
		// (get) Token: 0x060075F3 RID: 30195 RVA: 0x001B6E34 File Offset: 0x001B5034
		internal override TypeLayout Layout
		{
			get
			{
				return default(TypeLayout);
			}
		}

		// Token: 0x17002096 RID: 8342
		// (get) Token: 0x060075F4 RID: 30196 RVA: 0x001B6E4A File Offset: 0x001B504A
		internal override bool HasSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060075F5 RID: 30197 RVA: 0x001B6E4D File Offset: 0x001B504D
		internal sealed override NamedTypeSymbol AsNativeInteger()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17002097 RID: 8343
		// (get) Token: 0x060075F6 RID: 30198 RVA: 0x001B6E54 File Offset: 0x001B5054
		internal sealed override NamedTypeSymbol NativeIntegerUnderlyingType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001CAA RID: 7338
		private readonly ImmutableArray<TypeParameterSymbol> _typeParameters;

		// Token: 0x04001CAB RID: 7339
		private readonly ImmutableArray<TypeParameterSymbol> _constructedFromTypeParameters;
	}
}
