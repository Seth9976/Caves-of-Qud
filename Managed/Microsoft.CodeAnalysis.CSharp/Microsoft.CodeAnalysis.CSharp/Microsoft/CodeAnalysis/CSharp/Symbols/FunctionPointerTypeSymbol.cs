using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004AF RID: 1199
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class FunctionPointerTypeSymbol : TypeSymbol, IFunctionPointerTypeReference, ITypeReference, IReference
	{
		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x060063D5 RID: 25557 RVA: 0x0017D91C File Offset: 0x0017BB1C
		ISignature IFunctionPointerTypeReference.Signature
		{
			get
			{
				if (this._lazySignature == null)
				{
					Interlocked.CompareExchange<FunctionPointerTypeSymbol.FunctionPointerMethodSignature>(ref this._lazySignature, new FunctionPointerTypeSymbol.FunctionPointerMethodSignature(this.AdaptedFunctionPointerTypeSymbol.Signature), null);
				}
				return this._lazySignature;
			}
		}

		// Token: 0x060063D6 RID: 25558 RVA: 0x0017D949 File Offset: 0x0017BB49
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x060063D7 RID: 25559 RVA: 0x0017D952 File Offset: 0x0017BB52
		bool ITypeReference.IsEnum
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x060063D8 RID: 25560 RVA: 0x0017D955 File Offset: 0x0017BB55
		Microsoft.Cci.PrimitiveTypeCode ITypeReference.TypeCode
		{
			get
			{
				return Microsoft.Cci.PrimitiveTypeCode.FunctionPointer;
			}
		}

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x060063D9 RID: 25561 RVA: 0x0017D95C File Offset: 0x0017BB5C
		TypeDefinitionHandle ITypeReference.TypeDef
		{
			get
			{
				return default(TypeDefinitionHandle);
			}
		}

		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x060063DA RID: 25562 RVA: 0x0017D972 File Offset: 0x0017BB72
		[global::System.Runtime.CompilerServices.Nullable(2)]
		IGenericMethodParameterReference ITypeReference.AsGenericMethodParameterReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x060063DB RID: 25563 RVA: 0x0017D975 File Offset: 0x0017BB75
		[global::System.Runtime.CompilerServices.Nullable(2)]
		IGenericTypeInstanceReference ITypeReference.AsGenericTypeInstanceReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x060063DC RID: 25564 RVA: 0x0017D978 File Offset: 0x0017BB78
		[global::System.Runtime.CompilerServices.Nullable(2)]
		IGenericTypeParameterReference ITypeReference.AsGenericTypeParameterReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x060063DD RID: 25565 RVA: 0x0017D97B File Offset: 0x0017BB7B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		INamespaceTypeReference ITypeReference.AsNamespaceTypeReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x060063DE RID: 25566 RVA: 0x0017D97E File Offset: 0x0017BB7E
		[global::System.Runtime.CompilerServices.Nullable(2)]
		INestedTypeReference ITypeReference.AsNestedTypeReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x060063DF RID: 25567 RVA: 0x0017D981 File Offset: 0x0017BB81
		[global::System.Runtime.CompilerServices.Nullable(2)]
		ISpecializedNestedTypeReference ITypeReference.AsSpecializedNestedTypeReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x060063E0 RID: 25568 RVA: 0x0017D984 File Offset: 0x0017BB84
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		INamespaceTypeDefinition ITypeReference.AsNamespaceTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x060063E1 RID: 25569 RVA: 0x0017D987 File Offset: 0x0017BB87
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		INestedTypeDefinition ITypeReference.AsNestedTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x060063E2 RID: 25570 RVA: 0x0017D98A File Offset: 0x0017BB8A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		ITypeDefinition ITypeReference.AsTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x060063E3 RID: 25571 RVA: 0x0017D98D File Offset: 0x0017BB8D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		ITypeDefinition ITypeReference.GetResolvedType(EmitContext context)
		{
			return null;
		}

		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x060063E4 RID: 25572 RVA: 0x0017D990 File Offset: 0x0017BB90
		bool ITypeReference.IsValueType
		{
			get
			{
				return this.AdaptedFunctionPointerTypeSymbol.IsValueType;
			}
		}

		// Token: 0x060063E5 RID: 25573 RVA: 0x0017D99D File Offset: 0x0017BB9D
		IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x0017D9A4 File Offset: 0x0017BBA4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x060063E7 RID: 25575 RVA: 0x0017D9A7 File Offset: 0x0017BBA7
		internal FunctionPointerTypeSymbol AdaptedFunctionPointerTypeSymbol
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060063E8 RID: 25576 RVA: 0x0017D9AA File Offset: 0x0017BBAA
		internal new FunctionPointerTypeSymbol GetCciAdapter()
		{
			return this;
		}

		// Token: 0x060063E9 RID: 25577 RVA: 0x0017D9AD File Offset: 0x0017BBAD
		public static FunctionPointerTypeSymbol CreateFromSource(FunctionPointerTypeSyntax syntax, Binder typeBinder, BindingDiagnosticBag diagnostics, ConsList<TypeSymbol> basesBeingResolved, bool suppressUseSiteDiagnostics)
		{
			return new FunctionPointerTypeSymbol(FunctionPointerMethodSymbol.CreateFromSource(syntax, typeBinder, diagnostics, basesBeingResolved, suppressUseSiteDiagnostics));
		}

		// Token: 0x060063EA RID: 25578 RVA: 0x0017D9BF File Offset: 0x0017BBBF
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static FunctionPointerTypeSymbol CreateFromPartsForTests(CallingConvention callingConvention, TypeWithAnnotations returnType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> refCustomModifiers, RefKind returnRefKind, ImmutableArray<TypeWithAnnotations> parameterTypes, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<ImmutableArray<CustomModifier>> parameterRefCustomModifiers, ImmutableArray<RefKind> parameterRefKinds, [global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilation compilation)
		{
			return new FunctionPointerTypeSymbol(FunctionPointerMethodSymbol.CreateFromPartsForTest(callingConvention, returnType, refCustomModifiers, returnRefKind, parameterTypes, parameterRefCustomModifiers, parameterRefKinds, compilation));
		}

		// Token: 0x060063EB RID: 25579 RVA: 0x0017D9D7 File Offset: 0x0017BBD7
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static FunctionPointerTypeSymbol CreateFromParts(CallingConvention callingConvention, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> callingConventionModifiers, TypeWithAnnotations returnType, RefKind returnRefKind, ImmutableArray<TypeWithAnnotations> parameterTypes, ImmutableArray<RefKind> parameterRefKinds, [global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilation compilation)
		{
			return new FunctionPointerTypeSymbol(FunctionPointerMethodSymbol.CreateFromParts(callingConvention, callingConventionModifiers, returnType, returnRefKind, parameterTypes, parameterRefKinds, compilation));
		}

		// Token: 0x060063EC RID: 25580 RVA: 0x0017D9ED File Offset: 0x0017BBED
		public static FunctionPointerTypeSymbol CreateFromMetadata(CallingConvention callingConvention, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<ParamInfo<TypeSymbol>> retAndParamTypes)
		{
			return new FunctionPointerTypeSymbol(FunctionPointerMethodSymbol.CreateFromMetadata(callingConvention, retAndParamTypes));
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x0017D9FB File Offset: 0x0017BBFB
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public FunctionPointerTypeSymbol SubstituteTypeSymbol(TypeWithAnnotations substitutedReturnType, ImmutableArray<TypeWithAnnotations> substitutedParameterTypes, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> refCustomModifiers, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<ImmutableArray<CustomModifier>> paramRefCustomModifiers)
		{
			return new FunctionPointerTypeSymbol(this.Signature.SubstituteParameterSymbols(substitutedReturnType, substitutedParameterTypes, refCustomModifiers, paramRefCustomModifiers));
		}

		// Token: 0x060063EE RID: 25582 RVA: 0x0017DA12 File Offset: 0x0017BC12
		private FunctionPointerTypeSymbol(FunctionPointerMethodSymbol signature)
		{
			this.Signature = signature;
		}

		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x060063EF RID: 25583 RVA: 0x0017DA21 File Offset: 0x0017BC21
		public FunctionPointerMethodSymbol Signature { get; }

		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x060063F0 RID: 25584 RVA: 0x0017DA29 File Offset: 0x0017BC29
		public override bool IsReferenceType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x060063F1 RID: 25585 RVA: 0x0017DA2C File Offset: 0x0017BC2C
		public override bool IsValueType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x060063F2 RID: 25586 RVA: 0x0017DA2F File Offset: 0x0017BC2F
		public override TypeKind TypeKind
		{
			get
			{
				return TypeKind.FunctionPointer;
			}
		}

		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x060063F3 RID: 25587 RVA: 0x0017DA33 File Offset: 0x0017BC33
		public override bool IsRefLikeType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x060063F4 RID: 25588 RVA: 0x0017DA36 File Offset: 0x0017BC36
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x060063F5 RID: 25589 RVA: 0x0017DA39 File Offset: 0x0017BC39
		public override SymbolKind Kind
		{
			get
			{
				return SymbolKind.FunctionPointerType;
			}
		}

		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x060063F6 RID: 25590 RVA: 0x0017DA3D File Offset: 0x0017BC3D
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ContainingSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x060063F7 RID: 25591 RVA: 0x0017DA40 File Offset: 0x0017BC40
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x060063F8 RID: 25592 RVA: 0x0017DA47 File Offset: 0x0017BC47
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x060063F9 RID: 25593 RVA: 0x0017DA4E File Offset: 0x0017BC4E
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x060063FA RID: 25594 RVA: 0x0017DA51 File Offset: 0x0017BC51
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x060063FB RID: 25595 RVA: 0x0017DA54 File Offset: 0x0017BC54
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x060063FC RID: 25596 RVA: 0x0017DA57 File Offset: 0x0017BC57
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x060063FD RID: 25597 RVA: 0x0017DA5A File Offset: 0x0017BC5A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x060063FE RID: 25598 RVA: 0x0017DA5D File Offset: 0x0017BC5D
		internal override ManagedKind GetManagedKind([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return ManagedKind.Unmanaged;
		}

		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x060063FF RID: 25599 RVA: 0x0017DA60 File Offset: 0x0017BC60
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06006400 RID: 25600 RVA: 0x0017DA63 File Offset: 0x0017BC63
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitFunctionPointerType(this);
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x0017DA6C File Offset: 0x0017BC6C
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitFunctionPointerType(this);
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x0017DA75 File Offset: 0x0017BC75
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Symbol> GetMembers()
		{
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x06006403 RID: 25603 RVA: 0x0017DA7C File Offset: 0x0017BC7C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x0017DA83 File Offset: 0x0017BC83
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006405 RID: 25605 RVA: 0x0017DA8A File Offset: 0x0017BC8A
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x0017DA91 File Offset: 0x0017BC91
		internal override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TArgument, [global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument a)
		{
			return visitor.VisitFunctionPointerType(this, a);
		}

		// Token: 0x06006407 RID: 25607 RVA: 0x0017DA9B File Offset: 0x0017BC9B
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved = null)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006408 RID: 25608 RVA: 0x0017DAA4 File Offset: 0x0017BCA4
		internal override bool Equals(TypeSymbol t2, TypeCompareKind compareKind)
		{
			if (this == t2)
			{
				return true;
			}
			FunctionPointerTypeSymbol functionPointerTypeSymbol = t2 as FunctionPointerTypeSymbol;
			return functionPointerTypeSymbol != null && this.Signature.Equals(functionPointerTypeSymbol.Signature, compareKind);
		}

		// Token: 0x06006409 RID: 25609 RVA: 0x0017DAD5 File Offset: 0x0017BCD5
		public override int GetHashCode()
		{
			return Hash.Combine(1, this.Signature.GetHashCode());
		}

		// Token: 0x0600640A RID: 25610 RVA: 0x0017DAE8 File Offset: 0x0017BCE8
		protected override ISymbol CreateISymbol()
		{
			return new FunctionPointerTypeSymbol(this, base.DefaultNullableAnnotation);
		}

		// Token: 0x0600640B RID: 25611 RVA: 0x0017DAF6 File Offset: 0x0017BCF6
		protected override ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation)
		{
			return new FunctionPointerTypeSymbol(this, nullableAnnotation);
		}

		// Token: 0x0600640C RID: 25612 RVA: 0x0017DAFF File Offset: 0x0017BCFF
		internal override void AddNullableTransforms(ArrayBuilder<byte> transforms)
		{
			this.Signature.AddNullableTransforms(transforms);
		}

		// Token: 0x0600640D RID: 25613 RVA: 0x0017DB10 File Offset: 0x0017BD10
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal override bool ApplyNullableTransforms(byte defaultTransformFlag, ImmutableArray<byte> transforms, ref int position, [global::System.Runtime.CompilerServices.Nullable(1)] out TypeSymbol result)
		{
			FunctionPointerMethodSymbol functionPointerMethodSymbol = this.Signature.ApplyNullableTransforms(defaultTransformFlag, transforms, ref position);
			bool flag = this.Signature != functionPointerMethodSymbol;
			result = (flag ? new FunctionPointerTypeSymbol(functionPointerMethodSymbol) : this);
			return flag;
		}

		// Token: 0x0600640E RID: 25614 RVA: 0x0017DB4C File Offset: 0x0017BD4C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			UseSiteInfo<AssemblySymbol> useSiteInfo = this.Signature.GetUseSiteInfo();
			DiagnosticInfo diagnosticInfo = useSiteInfo.DiagnosticInfo;
			if (diagnosticInfo != null && diagnosticInfo.Code == 570 && useSiteInfo.DiagnosticInfo.Arguments.AsSingleton<object>() == this.Signature)
			{
				return new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_BogusType, new object[] { this }));
			}
			return useSiteInfo;
		}

		// Token: 0x0600640F RID: 25615 RVA: 0x0017DBB3 File Offset: 0x0017BDB3
		internal override bool GetUnificationUseSiteDiagnosticRecursive([global::System.Runtime.CompilerServices.Nullable(2)] ref DiagnosticInfo result, Symbol owner, ref HashSet<TypeSymbol> checkedTypes)
		{
			return this.Signature.GetUnificationUseSiteDiagnosticRecursive(ref result, owner, ref checkedTypes);
		}

		// Token: 0x06006410 RID: 25616 RVA: 0x0017DBC4 File Offset: 0x0017BDC4
		internal override TypeSymbol MergeEquivalentTypes(TypeSymbol other, VarianceKind variance)
		{
			FunctionPointerMethodSymbol functionPointerMethodSymbol = this.Signature.MergeEquivalentTypes(((FunctionPointerTypeSymbol)other).Signature, variance);
			if (functionPointerMethodSymbol != this.Signature)
			{
				return new FunctionPointerTypeSymbol(functionPointerMethodSymbol);
			}
			return this;
		}

		// Token: 0x06006411 RID: 25617 RVA: 0x0017DBFC File Offset: 0x0017BDFC
		internal override TypeSymbol SetNullabilityForReferenceTypes(Func<TypeWithAnnotations, TypeWithAnnotations> transform)
		{
			FunctionPointerMethodSymbol functionPointerMethodSymbol = this.Signature.SetNullabilityForReferenceTypes(transform);
			if (this.Signature != functionPointerMethodSymbol)
			{
				return new FunctionPointerTypeSymbol(functionPointerMethodSymbol);
			}
			return this;
		}

		// Token: 0x06006412 RID: 25618 RVA: 0x0017DC27 File Offset: 0x0017BE27
		internal static bool RefKindEquals(TypeCompareKind compareKind, RefKind refKind1, RefKind refKind2)
		{
			if ((compareKind & TypeCompareKind.FunctionPointerRefMatchesOutInRefReadonly) == TypeCompareKind.ConsiderEverything)
			{
				return refKind1 == refKind2;
			}
			return refKind1 == RefKind.None == (refKind2 == RefKind.None);
		}

		// Token: 0x06006413 RID: 25619 RVA: 0x0017DC3E File Offset: 0x0017BE3E
		internal static RefKind GetRefKindForHashCode(RefKind refKind)
		{
			if (refKind != RefKind.None)
			{
				return RefKind.Ref;
			}
			return RefKind.None;
		}

		// Token: 0x06006414 RID: 25620 RVA: 0x0017DC48 File Offset: 0x0017BE48
		internal static bool IsCallingConventionModifier(NamedTypeSymbol modifierType)
		{
			AssemblySymbol containingAssembly = modifierType.ContainingAssembly;
			AssemblySymbol containingAssembly2 = modifierType.ContainingAssembly;
			return containingAssembly == ((containingAssembly2 != null) ? containingAssembly2.CorLibrary : null) && modifierType.Arity == 0 && modifierType.Name != "CallConv" && modifierType.Name.StartsWith("CallConv", 4) && modifierType.IsCompilerServicesTopLevelType();
		}

		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x06006415 RID: 25621 RVA: 0x0017DCA4 File Offset: 0x0017BEA4
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x06006416 RID: 25622 RVA: 0x0017DCA7 File Offset: 0x0017BEA7
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400198E RID: 6542
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private FunctionPointerTypeSymbol.FunctionPointerMethodSignature _lazySignature;

		// Token: 0x020008D3 RID: 2259
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class FunctionPointerMethodSignature : ISignature
		{
			// Token: 0x17002840 RID: 10304
			// (get) Token: 0x06008E39 RID: 36409 RVA: 0x001F5ECB File Offset: 0x001F40CB
			internal ISignature Underlying
			{
				get
				{
					return this._underlying.GetCciAdapter();
				}
			}

			// Token: 0x06008E3A RID: 36410 RVA: 0x001F5ED8 File Offset: 0x001F40D8
			internal FunctionPointerMethodSignature(FunctionPointerMethodSymbol underlying)
			{
				this._underlying = underlying;
			}

			// Token: 0x17002841 RID: 10305
			// (get) Token: 0x06008E3B RID: 36411 RVA: 0x001F5EE7 File Offset: 0x001F40E7
			public CallingConvention CallingConvention
			{
				get
				{
					return this.Underlying.CallingConvention;
				}
			}

			// Token: 0x17002842 RID: 10306
			// (get) Token: 0x06008E3C RID: 36412 RVA: 0x001F5EF4 File Offset: 0x001F40F4
			public ushort ParameterCount
			{
				get
				{
					return this.Underlying.ParameterCount;
				}
			}

			// Token: 0x17002843 RID: 10307
			// (get) Token: 0x06008E3D RID: 36413 RVA: 0x001F5F01 File Offset: 0x001F4101
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<ICustomModifier> ReturnValueCustomModifiers
			{
				[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.Underlying.ReturnValueCustomModifiers;
				}
			}

			// Token: 0x17002844 RID: 10308
			// (get) Token: 0x06008E3E RID: 36414 RVA: 0x001F5F0E File Offset: 0x001F410E
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<ICustomModifier> RefCustomModifiers
			{
				[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.Underlying.RefCustomModifiers;
				}
			}

			// Token: 0x17002845 RID: 10309
			// (get) Token: 0x06008E3F RID: 36415 RVA: 0x001F5F1B File Offset: 0x001F411B
			public bool ReturnValueIsByRef
			{
				get
				{
					return this.Underlying.ReturnValueIsByRef;
				}
			}

			// Token: 0x06008E40 RID: 36416 RVA: 0x001F5F28 File Offset: 0x001F4128
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<IParameterTypeInformation> GetParameters(EmitContext context)
			{
				return this.Underlying.GetParameters(context);
			}

			// Token: 0x06008E41 RID: 36417 RVA: 0x001F5F36 File Offset: 0x001F4136
			public ITypeReference GetType(EmitContext context)
			{
				return this.Underlying.GetType(context);
			}

			// Token: 0x06008E42 RID: 36418 RVA: 0x001F5F44 File Offset: 0x001F4144
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public override bool Equals(object obj)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008E43 RID: 36419 RVA: 0x001F5F4B File Offset: 0x001F414B
			public override int GetHashCode()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008E44 RID: 36420 RVA: 0x001F5F52 File Offset: 0x001F4152
			public override string ToString()
			{
				return this._underlying.ToDisplayString(SymbolDisplayFormat.ILVisualizationFormat);
			}

			// Token: 0x04002661 RID: 9825
			private readonly FunctionPointerMethodSymbol _underlying;
		}
	}
}
