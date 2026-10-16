using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004E0 RID: 1248
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class ErrorTypeSymbol : NamedTypeSymbol
	{
		// Token: 0x17001AD5 RID: 6869
		// (get) Token: 0x06006984 RID: 27012
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal abstract DiagnosticInfo ErrorInfo
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17001AD6 RID: 6870
		// (get) Token: 0x06006985 RID: 27013 RVA: 0x0018A9F4 File Offset: 0x00188BF4
		internal virtual LookupResultKind ResultKind
		{
			get
			{
				return LookupResultKind.Empty;
			}
		}

		// Token: 0x06006986 RID: 27014 RVA: 0x0018A9F8 File Offset: 0x00188BF8
		internal TypeWithAnnotations Substitute(AbstractTypeMap typeMap)
		{
			return TypeWithAnnotations.Create(typeMap.SubstituteNamedType(this), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
		}

		// Token: 0x17001AD7 RID: 6871
		// (get) Token: 0x06006987 RID: 27015 RVA: 0x0018AA1B File Offset: 0x00188C1B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public virtual ImmutableArray<Symbol> CandidateSymbols
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<Symbol>.Empty;
			}
		}

		// Token: 0x17001AD8 RID: 6872
		// (get) Token: 0x06006988 RID: 27016 RVA: 0x0018AA24 File Offset: 0x00188C24
		public CandidateReason CandidateReason
		{
			get
			{
				if (!this.CandidateSymbols.IsEmpty)
				{
					return this.ResultKind.ToCandidateReason();
				}
				return CandidateReason.None;
			}
		}

		// Token: 0x06006989 RID: 27017 RVA: 0x0018AA4E File Offset: 0x00188C4E
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			return new UseSiteInfo<AssemblySymbol>(this.ErrorInfo);
		}

		// Token: 0x17001AD9 RID: 6873
		// (get) Token: 0x0600698A RID: 27018 RVA: 0x0018AA5B File Offset: 0x00188C5B
		public override bool IsReferenceType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001ADA RID: 6874
		// (get) Token: 0x0600698B RID: 27019 RVA: 0x0018AA5E File Offset: 0x00188C5E
		public sealed override bool IsValueType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001ADB RID: 6875
		// (get) Token: 0x0600698C RID: 27020 RVA: 0x0018AA61 File Offset: 0x00188C61
		public sealed override bool IsRefLikeType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001ADC RID: 6876
		// (get) Token: 0x0600698D RID: 27021 RVA: 0x0018AA64 File Offset: 0x00188C64
		public sealed override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001ADD RID: 6877
		// (get) Token: 0x0600698E RID: 27022 RVA: 0x0018AA67 File Offset: 0x00188C67
		public override IEnumerable<string> MemberNames
		{
			get
			{
				return SpecializedCollections.EmptyEnumerable<string>();
			}
		}

		// Token: 0x0600698F RID: 27023 RVA: 0x0018AA6E File Offset: 0x00188C6E
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Symbol> GetMembers()
		{
			if (this.IsTupleType)
			{
				return base.AddOrWrapTupleMembers(ImmutableArray<Symbol>.Empty).ToImmutableAndFree();
			}
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x06006990 RID: 27024 RVA: 0x0018AA8E File Offset: 0x00188C8E
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			return this.GetMembers().WhereAsArray((Symbol m, string name) => m.Name == name, name);
		}

		// Token: 0x06006991 RID: 27025 RVA: 0x0018AABB File Offset: 0x00188CBB
		internal sealed override IEnumerable<FieldSymbol> GetFieldsToEmit()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006992 RID: 27026 RVA: 0x0018AAC2 File Offset: 0x00188CC2
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers()
		{
			return this.GetMembersUnordered();
		}

		// Token: 0x06006993 RID: 27027 RVA: 0x0018AACA File Offset: 0x00188CCA
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers(string name)
		{
			return this.GetMembers(name);
		}

		// Token: 0x06006994 RID: 27028 RVA: 0x0018AAD3 File Offset: 0x00188CD3
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006995 RID: 27029 RVA: 0x0018AADA File Offset: 0x00188CDA
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006996 RID: 27030 RVA: 0x0018AAE1 File Offset: 0x00188CE1
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x17001ADE RID: 6878
		// (get) Token: 0x06006997 RID: 27031 RVA: 0x0018AAE8 File Offset: 0x00188CE8
		public sealed override SymbolKind Kind
		{
			get
			{
				return SymbolKind.ErrorType;
			}
		}

		// Token: 0x17001ADF RID: 6879
		// (get) Token: 0x06006998 RID: 27032 RVA: 0x0018AAEB File Offset: 0x00188CEB
		public sealed override TypeKind TypeKind
		{
			get
			{
				return TypeKind.Error;
			}
		}

		// Token: 0x17001AE0 RID: 6880
		// (get) Token: 0x06006999 RID: 27033 RVA: 0x0018AAEE File Offset: 0x00188CEE
		internal sealed override bool IsInterface
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AE1 RID: 6881
		// (get) Token: 0x0600699A RID: 27034 RVA: 0x0018AAF1 File Offset: 0x00188CF1
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ContainingSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001AE2 RID: 6882
		// (get) Token: 0x0600699B RID: 27035 RVA: 0x0018AAF4 File Offset: 0x00188CF4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17001AE3 RID: 6883
		// (get) Token: 0x0600699C RID: 27036 RVA: 0x0018AAFB File Offset: 0x00188CFB
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17001AE4 RID: 6884
		// (get) Token: 0x0600699D RID: 27037 RVA: 0x0018AB02 File Offset: 0x00188D02
		public override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001AE5 RID: 6885
		// (get) Token: 0x0600699E RID: 27038 RVA: 0x0018AB05 File Offset: 0x00188D05
		public override string Name
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17001AE6 RID: 6886
		// (get) Token: 0x0600699F RID: 27039 RVA: 0x0018AB0C File Offset: 0x00188D0C
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return base.GetTypeParametersAsTypeArguments();
			}
		}

		// Token: 0x17001AE7 RID: 6887
		// (get) Token: 0x060069A0 RID: 27040 RVA: 0x0018AB14 File Offset: 0x00188D14
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				if (this._lazyTypeParameters.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<TypeParameterSymbol>(ref this._lazyTypeParameters, this.GetTypeParameters(), default(ImmutableArray<TypeParameterSymbol>));
				}
				return this._lazyTypeParameters;
			}
		}

		// Token: 0x060069A1 RID: 27041 RVA: 0x0018AB50 File Offset: 0x00188D50
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<TypeParameterSymbol> GetTypeParameters()
		{
			int arity = this.Arity;
			if (arity == 0)
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
			TypeParameterSymbol[] array = new TypeParameterSymbol[arity];
			for (int i = 0; i < arity; i++)
			{
				array[i] = new ErrorTypeSymbol.ErrorTypeParameterSymbol(this, string.Empty, i);
			}
			return array.AsImmutableOrNull<TypeParameterSymbol>();
		}

		// Token: 0x17001AE8 RID: 6888
		// (get) Token: 0x060069A2 RID: 27042 RVA: 0x0018AB95 File Offset: 0x00188D95
		public override NamedTypeSymbol ConstructedFrom
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060069A3 RID: 27043 RVA: 0x0018AB98 File Offset: 0x00188D98
		internal override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TArgument, [global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitErrorType(this, argument);
		}

		// Token: 0x060069A4 RID: 27044 RVA: 0x0018ABA2 File Offset: 0x00188DA2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal ErrorTypeSymbol(NamedTypeSymbol.TupleExtraData tupleData = null)
			: base(tupleData)
		{
		}

		// Token: 0x17001AE9 RID: 6889
		// (get) Token: 0x060069A5 RID: 27045 RVA: 0x0018ABAB File Offset: 0x00188DAB
		public sealed override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x17001AEA RID: 6890
		// (get) Token: 0x060069A6 RID: 27046 RVA: 0x0018ABAE File Offset: 0x00188DAE
		public sealed override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AEB RID: 6891
		// (get) Token: 0x060069A7 RID: 27047 RVA: 0x0018ABB1 File Offset: 0x00188DB1
		public sealed override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AEC RID: 6892
		// (get) Token: 0x060069A8 RID: 27048 RVA: 0x0018ABB4 File Offset: 0x00188DB4
		public sealed override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AED RID: 6893
		// (get) Token: 0x060069A9 RID: 27049 RVA: 0x0018ABB7 File Offset: 0x00188DB7
		internal sealed override bool HasSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AEE RID: 6894
		// (get) Token: 0x060069AA RID: 27050 RVA: 0x0018ABBA File Offset: 0x00188DBA
		public sealed override bool MightContainExtensionMethods
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AEF RID: 6895
		// (get) Token: 0x060069AB RID: 27051 RVA: 0x0018ABBD File Offset: 0x00188DBD
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001AF0 RID: 6896
		// (get) Token: 0x060069AC RID: 27052 RVA: 0x0018ABC0 File Offset: 0x00188DC0
		internal override bool HasCodeAnalysisEmbeddedAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060069AD RID: 27053 RVA: 0x0018ABC3 File Offset: 0x00188DC3
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x060069AE RID: 27054 RVA: 0x0018ABCA File Offset: 0x00188DCA
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<NamedTypeSymbol> GetInterfacesToEmit()
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x060069AF RID: 27055 RVA: 0x0018ABD1 File Offset: 0x00188DD1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal override NamedTypeSymbol GetDeclaredBaseType(ConsList<TypeSymbol> basesBeingResolved)
		{
			return null;
		}

		// Token: 0x060069B0 RID: 27056 RVA: 0x0018ABD4 File Offset: 0x00188DD4
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<NamedTypeSymbol> GetDeclaredInterfaces(ConsList<TypeSymbol> basesBeingResolved)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x060069B1 RID: 27057 RVA: 0x0018ABDB File Offset: 0x00188DDB
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		protected override NamedTypeSymbol ConstructCore(ImmutableArray<TypeWithAnnotations> typeArguments, bool unbound)
		{
			return new ConstructedErrorTypeSymbol(this, typeArguments, null);
		}

		// Token: 0x060069B2 RID: 27058 RVA: 0x0018ABE5 File Offset: 0x00188DE5
		internal override NamedTypeSymbol AsMember(NamedTypeSymbol newOwner)
		{
			if (!newOwner.IsDefinition)
			{
				return new SubstitutedNestedErrorTypeSymbol(newOwner, this);
			}
			return this;
		}

		// Token: 0x17001AF1 RID: 6897
		// (get) Token: 0x060069B3 RID: 27059 RVA: 0x0018ABF8 File Offset: 0x00188DF8
		internal sealed override bool ShouldAddWinRTMembers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AF2 RID: 6898
		// (get) Token: 0x060069B4 RID: 27060 RVA: 0x0018ABFB File Offset: 0x00188DFB
		internal sealed override bool IsWindowsRuntimeImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AF3 RID: 6899
		// (get) Token: 0x060069B5 RID: 27061 RVA: 0x0018AC00 File Offset: 0x00188E00
		internal sealed override TypeLayout Layout
		{
			get
			{
				return default(TypeLayout);
			}
		}

		// Token: 0x17001AF4 RID: 6900
		// (get) Token: 0x060069B6 RID: 27062 RVA: 0x0018AC16 File Offset: 0x00188E16
		internal override CharSet MarshallingCharSet
		{
			get
			{
				return base.DefaultMarshallingCharSet;
			}
		}

		// Token: 0x17001AF5 RID: 6901
		// (get) Token: 0x060069B7 RID: 27063 RVA: 0x0018AC1E File Offset: 0x00188E1E
		public sealed override bool IsSerializable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AF6 RID: 6902
		// (get) Token: 0x060069B8 RID: 27064 RVA: 0x0018AC21 File Offset: 0x00188E21
		internal sealed override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AF7 RID: 6903
		// (get) Token: 0x060069B9 RID: 27065 RVA: 0x0018AC24 File Offset: 0x00188E24
		internal sealed override bool IsComImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AF8 RID: 6904
		// (get) Token: 0x060069BA RID: 27066 RVA: 0x0018AC27 File Offset: 0x00188E27
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x060069BB RID: 27067 RVA: 0x0018AC2A File Offset: 0x00188E2A
		internal sealed override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060069BC RID: 27068 RVA: 0x0018AC31 File Offset: 0x00188E31
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal sealed override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x060069BD RID: 27069 RVA: 0x0018AC38 File Offset: 0x00188E38
		internal override AttributeUsageInfo GetAttributeUsageInfo()
		{
			return AttributeUsageInfo.Null;
		}

		// Token: 0x17001AF9 RID: 6905
		// (get) Token: 0x060069BE RID: 27070 RVA: 0x0018AC3F File Offset: 0x00188E3F
		internal virtual bool Unreported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AFA RID: 6906
		// (get) Token: 0x060069BF RID: 27071 RVA: 0x0018AC42 File Offset: 0x00188E42
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x060069C0 RID: 27072 RVA: 0x0018AC49 File Offset: 0x00188E49
		internal override NamedTypeSymbol AsNativeInteger()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001AFB RID: 6907
		// (get) Token: 0x060069C1 RID: 27073 RVA: 0x0018AC50 File Offset: 0x00188E50
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override NamedTypeSymbol NativeIntegerUnderlyingType
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x060069C2 RID: 27074 RVA: 0x0018AC53 File Offset: 0x00188E53
		protected sealed override ISymbol CreateISymbol()
		{
			return new ErrorTypeSymbol(this, base.DefaultNullableAnnotation);
		}

		// Token: 0x060069C3 RID: 27075 RVA: 0x0018AC61 File Offset: 0x00188E61
		protected sealed override ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation)
		{
			return new ErrorTypeSymbol(this, nullableAnnotation);
		}

		// Token: 0x17001AFC RID: 6908
		// (get) Token: 0x060069C4 RID: 27076 RVA: 0x0018AC6A File Offset: 0x00188E6A
		internal sealed override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AFD RID: 6909
		// (get) Token: 0x060069C5 RID: 27077 RVA: 0x0018AC6D File Offset: 0x00188E6D
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060069C6 RID: 27078 RVA: 0x0018AC70 File Offset: 0x00188E70
		internal sealed override bool HasPossibleWellKnownCloneMethod()
		{
			return false;
		}

		// Token: 0x04001A49 RID: 6729
		internal static readonly ErrorTypeSymbol UnknownResultType = new UnsupportedMetadataTypeSymbol(null);

		// Token: 0x04001A4A RID: 6730
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<TypeParameterSymbol> _lazyTypeParameters;

		// Token: 0x020008F5 RID: 2293
		[global::System.Runtime.CompilerServices.Nullable(0)]
		protected sealed class ErrorTypeParameterSymbol : TypeParameterSymbol
		{
			// Token: 0x06008F24 RID: 36644 RVA: 0x001F898C File Offset: 0x001F6B8C
			public ErrorTypeParameterSymbol(ErrorTypeSymbol container, string name, int ordinal)
			{
				this._container = container;
				this._name = name;
				this._ordinal = ordinal;
			}

			// Token: 0x1700286E RID: 10350
			// (get) Token: 0x06008F25 RID: 36645 RVA: 0x001F89A9 File Offset: 0x001F6BA9
			public override string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x1700286F RID: 10351
			// (get) Token: 0x06008F26 RID: 36646 RVA: 0x001F89B1 File Offset: 0x001F6BB1
			public override TypeParameterKind TypeParameterKind
			{
				get
				{
					return TypeParameterKind.Type;
				}
			}

			// Token: 0x17002870 RID: 10352
			// (get) Token: 0x06008F27 RID: 36647 RVA: 0x001F89B4 File Offset: 0x001F6BB4
			public override Symbol ContainingSymbol
			{
				get
				{
					return this._container;
				}
			}

			// Token: 0x17002871 RID: 10353
			// (get) Token: 0x06008F28 RID: 36648 RVA: 0x001F89BC File Offset: 0x001F6BBC
			public override bool HasConstructorConstraint
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002872 RID: 10354
			// (get) Token: 0x06008F29 RID: 36649 RVA: 0x001F89BF File Offset: 0x001F6BBF
			public override bool HasReferenceTypeConstraint
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002873 RID: 10355
			// (get) Token: 0x06008F2A RID: 36650 RVA: 0x001F89C2 File Offset: 0x001F6BC2
			public override bool IsReferenceTypeFromConstraintTypes
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002874 RID: 10356
			// (get) Token: 0x06008F2B RID: 36651 RVA: 0x001F89C5 File Offset: 0x001F6BC5
			internal override bool? ReferenceTypeConstraintIsNullable
			{
				get
				{
					return new bool?(false);
				}
			}

			// Token: 0x17002875 RID: 10357
			// (get) Token: 0x06008F2C RID: 36652 RVA: 0x001F89CD File Offset: 0x001F6BCD
			public override bool HasNotNullConstraint
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002876 RID: 10358
			// (get) Token: 0x06008F2D RID: 36653 RVA: 0x001F89D0 File Offset: 0x001F6BD0
			internal override bool? IsNotNullable
			{
				get
				{
					return default(bool?);
				}
			}

			// Token: 0x17002877 RID: 10359
			// (get) Token: 0x06008F2E RID: 36654 RVA: 0x001F89E6 File Offset: 0x001F6BE6
			public override bool HasValueTypeConstraint
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002878 RID: 10360
			// (get) Token: 0x06008F2F RID: 36655 RVA: 0x001F89E9 File Offset: 0x001F6BE9
			public override bool IsValueTypeFromConstraintTypes
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002879 RID: 10361
			// (get) Token: 0x06008F30 RID: 36656 RVA: 0x001F89EC File Offset: 0x001F6BEC
			public override bool HasUnmanagedTypeConstraint
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700287A RID: 10362
			// (get) Token: 0x06008F31 RID: 36657 RVA: 0x001F89EF File Offset: 0x001F6BEF
			public override int Ordinal
			{
				get
				{
					return this._ordinal;
				}
			}

			// Token: 0x1700287B RID: 10363
			// (get) Token: 0x06008F32 RID: 36658 RVA: 0x001F89F7 File Offset: 0x001F6BF7
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public override ImmutableArray<Location> Locations
			{
				[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
				get
				{
					return ImmutableArray<Location>.Empty;
				}
			}

			// Token: 0x1700287C RID: 10364
			// (get) Token: 0x06008F33 RID: 36659 RVA: 0x001F89FE File Offset: 0x001F6BFE
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
			{
				[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
				get
				{
					return ImmutableArray<SyntaxReference>.Empty;
				}
			}

			// Token: 0x1700287D RID: 10365
			// (get) Token: 0x06008F34 RID: 36660 RVA: 0x001F8A05 File Offset: 0x001F6C05
			public override VarianceKind Variance
			{
				get
				{
					return VarianceKind.None;
				}
			}

			// Token: 0x1700287E RID: 10366
			// (get) Token: 0x06008F35 RID: 36661 RVA: 0x001F8A08 File Offset: 0x001F6C08
			public override bool IsImplicitlyDeclared
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008F36 RID: 36662 RVA: 0x001F8A0B File Offset: 0x001F6C0B
			internal override void EnsureAllConstraintsAreResolved()
			{
			}

			// Token: 0x06008F37 RID: 36663 RVA: 0x001F8A0D File Offset: 0x001F6C0D
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			internal override ImmutableArray<TypeWithAnnotations> GetConstraintTypes([global::System.Runtime.CompilerServices.Nullable(1)] ConsList<TypeParameterSymbol> inProgress)
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}

			// Token: 0x06008F38 RID: 36664 RVA: 0x001F8A14 File Offset: 0x001F6C14
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			internal override ImmutableArray<NamedTypeSymbol> GetInterfaces(ConsList<TypeParameterSymbol> inProgress)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}

			// Token: 0x06008F39 RID: 36665 RVA: 0x001F8A1B File Offset: 0x001F6C1B
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			internal override NamedTypeSymbol GetEffectiveBaseClass(ConsList<TypeParameterSymbol> inProgress)
			{
				return null;
			}

			// Token: 0x06008F3A RID: 36666 RVA: 0x001F8A1E File Offset: 0x001F6C1E
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			internal override TypeSymbol GetDeducedBaseType(ConsList<TypeParameterSymbol> inProgress)
			{
				return null;
			}

			// Token: 0x06008F3B RID: 36667 RVA: 0x001F8A21 File Offset: 0x001F6C21
			public override int GetHashCode()
			{
				return Hash.Combine(this._container.GetHashCode(), this._ordinal);
			}

			// Token: 0x06008F3C RID: 36668 RVA: 0x001F8A3C File Offset: 0x001F6C3C
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
			{
				if (this == t2)
				{
					return true;
				}
				ErrorTypeSymbol.ErrorTypeParameterSymbol errorTypeParameterSymbol = t2 as ErrorTypeSymbol.ErrorTypeParameterSymbol;
				return errorTypeParameterSymbol != null && errorTypeParameterSymbol._ordinal == this._ordinal && errorTypeParameterSymbol.ContainingType.Equals(this.ContainingType, comparison);
			}

			// Token: 0x04002719 RID: 10009
			private readonly ErrorTypeSymbol _container;

			// Token: 0x0400271A RID: 10010
			private readonly string _name;

			// Token: 0x0400271B RID: 10011
			private readonly int _ordinal;
		}
	}
}
