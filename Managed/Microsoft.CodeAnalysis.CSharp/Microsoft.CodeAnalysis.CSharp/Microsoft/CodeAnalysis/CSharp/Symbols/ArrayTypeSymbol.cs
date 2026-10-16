using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Emit.NoPia;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004AA RID: 1194
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class ArrayTypeSymbol : TypeSymbol, IArrayTypeReference, ITypeReference, IReference
	{
		// Token: 0x060062CD RID: 25293 RVA: 0x0017B86C File Offset: 0x00179A6C
		ITypeReference IArrayTypeReference.GetElementType(EmitContext context)
		{
			PEModuleBuilder<CSharpCompilation, SourceModuleSymbol, AssemblySymbol, TypeSymbol, NamedTypeSymbol, MethodSymbol, SyntaxNode, EmbeddedTypesManager, ModuleCompilationState> pemoduleBuilder = (PEModuleBuilder)context.Module;
			TypeWithAnnotations elementTypeWithAnnotations = this.AdaptedArrayTypeSymbol.ElementTypeWithAnnotations;
			ITypeReference typeReference = pemoduleBuilder.Translate(elementTypeWithAnnotations.Type, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
			if (elementTypeWithAnnotations.CustomModifiers.Length == 0)
			{
				return typeReference;
			}
			return new ModifiedTypeReference(typeReference, ImmutableArray<ICustomModifier>.CastUp<CustomModifier>(elementTypeWithAnnotations.CustomModifiers));
		}

		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x060062CE RID: 25294 RVA: 0x0017B8D3 File Offset: 0x00179AD3
		bool IArrayTypeReference.IsSZArray
		{
			get
			{
				return this.AdaptedArrayTypeSymbol.IsSZArray;
			}
		}

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x060062CF RID: 25295 RVA: 0x0017B8E0 File Offset: 0x00179AE0
		[global::System.Runtime.CompilerServices.Nullable(0)]
		ImmutableArray<int> IArrayTypeReference.LowerBounds
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return this.AdaptedArrayTypeSymbol.LowerBounds;
			}
		}

		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x060062D0 RID: 25296 RVA: 0x0017B8ED File Offset: 0x00179AED
		int IArrayTypeReference.Rank
		{
			get
			{
				return this.AdaptedArrayTypeSymbol.Rank;
			}
		}

		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x060062D1 RID: 25297 RVA: 0x0017B8FA File Offset: 0x00179AFA
		[global::System.Runtime.CompilerServices.Nullable(0)]
		ImmutableArray<int> IArrayTypeReference.Sizes
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return this.AdaptedArrayTypeSymbol.Sizes;
			}
		}

		// Token: 0x060062D2 RID: 25298 RVA: 0x0017B907 File Offset: 0x00179B07
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x060062D3 RID: 25299 RVA: 0x0017B910 File Offset: 0x00179B10
		bool ITypeReference.IsEnum
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x060062D4 RID: 25300 RVA: 0x0017B913 File Offset: 0x00179B13
		bool ITypeReference.IsValueType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x060062D5 RID: 25301 RVA: 0x0017B918 File Offset: 0x00179B18
		TypeDefinitionHandle ITypeReference.TypeDef
		{
			get
			{
				return default(TypeDefinitionHandle);
			}
		}

		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x060062D6 RID: 25302 RVA: 0x0017B92E File Offset: 0x00179B2E
		Microsoft.Cci.PrimitiveTypeCode ITypeReference.TypeCode
		{
			get
			{
				return Microsoft.Cci.PrimitiveTypeCode.NotPrimitive;
			}
		}

		// Token: 0x060062D7 RID: 25303 RVA: 0x0017B932 File Offset: 0x00179B32
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		ITypeDefinition ITypeReference.GetResolvedType(EmitContext context)
		{
			return null;
		}

		// Token: 0x17001757 RID: 5975
		// (get) Token: 0x060062D8 RID: 25304 RVA: 0x0017B935 File Offset: 0x00179B35
		[global::System.Runtime.CompilerServices.Nullable(2)]
		IGenericMethodParameterReference ITypeReference.AsGenericMethodParameterReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x060062D9 RID: 25305 RVA: 0x0017B938 File Offset: 0x00179B38
		[global::System.Runtime.CompilerServices.Nullable(2)]
		IGenericTypeInstanceReference ITypeReference.AsGenericTypeInstanceReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x060062DA RID: 25306 RVA: 0x0017B93B File Offset: 0x00179B3B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		IGenericTypeParameterReference ITypeReference.AsGenericTypeParameterReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x060062DB RID: 25307 RVA: 0x0017B93E File Offset: 0x00179B3E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		INamespaceTypeDefinition ITypeReference.AsNamespaceTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x060062DC RID: 25308 RVA: 0x0017B941 File Offset: 0x00179B41
		[global::System.Runtime.CompilerServices.Nullable(2)]
		INamespaceTypeReference ITypeReference.AsNamespaceTypeReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x060062DD RID: 25309 RVA: 0x0017B944 File Offset: 0x00179B44
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		INestedTypeDefinition ITypeReference.AsNestedTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x060062DE RID: 25310 RVA: 0x0017B947 File Offset: 0x00179B47
		[global::System.Runtime.CompilerServices.Nullable(2)]
		INestedTypeReference ITypeReference.AsNestedTypeReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x060062DF RID: 25311 RVA: 0x0017B94A File Offset: 0x00179B4A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		ISpecializedNestedTypeReference ITypeReference.AsSpecializedNestedTypeReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x060062E0 RID: 25312 RVA: 0x0017B94D File Offset: 0x00179B4D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		ITypeDefinition ITypeReference.AsTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x060062E1 RID: 25313 RVA: 0x0017B950 File Offset: 0x00179B50
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x060062E2 RID: 25314 RVA: 0x0017B953 File Offset: 0x00179B53
		internal ArrayTypeSymbol AdaptedArrayTypeSymbol
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060062E3 RID: 25315 RVA: 0x0017B956 File Offset: 0x00179B56
		internal new ArrayTypeSymbol GetCciAdapter()
		{
			return this;
		}

		// Token: 0x060062E4 RID: 25316 RVA: 0x0017B959 File Offset: 0x00179B59
		private ArrayTypeSymbol(TypeWithAnnotations elementTypeWithAnnotations, NamedTypeSymbol array)
		{
			this._elementTypeWithAnnotations = elementTypeWithAnnotations;
			this._baseType = array;
		}

		// Token: 0x060062E5 RID: 25317 RVA: 0x0017B970 File Offset: 0x00179B70
		internal static ArrayTypeSymbol CreateCSharpArray(AssemblySymbol declaringAssembly, TypeWithAnnotations elementTypeWithAnnotations, int rank = 1)
		{
			if (rank == 1)
			{
				return ArrayTypeSymbol.CreateSZArray(declaringAssembly, elementTypeWithAnnotations);
			}
			return ArrayTypeSymbol.CreateMDArray(declaringAssembly, elementTypeWithAnnotations, rank, default(ImmutableArray<int>), default(ImmutableArray<int>));
		}

		// Token: 0x060062E6 RID: 25318 RVA: 0x0017B9A3 File Offset: 0x00179BA3
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		internal static ArrayTypeSymbol CreateMDArray(TypeWithAnnotations elementTypeWithAnnotations, int rank, ImmutableArray<int> sizes, ImmutableArray<int> lowerBounds, [global::System.Runtime.CompilerServices.Nullable(1)] NamedTypeSymbol array)
		{
			if (sizes.IsDefaultOrEmpty && lowerBounds.IsDefault)
			{
				return new ArrayTypeSymbol.MDArrayNoSizesOrBounds(elementTypeWithAnnotations, rank, array);
			}
			return new ArrayTypeSymbol.MDArrayWithSizesAndBounds(elementTypeWithAnnotations, rank, sizes, lowerBounds, array);
		}

		// Token: 0x060062E7 RID: 25319 RVA: 0x0017B9CC File Offset: 0x00179BCC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		internal static ArrayTypeSymbol CreateMDArray([global::System.Runtime.CompilerServices.Nullable(1)] AssemblySymbol declaringAssembly, TypeWithAnnotations elementType, int rank, ImmutableArray<int> sizes, ImmutableArray<int> lowerBounds)
		{
			return ArrayTypeSymbol.CreateMDArray(elementType, rank, sizes, lowerBounds, declaringAssembly.GetSpecialType(SpecialType.System_Array));
		}

		// Token: 0x060062E8 RID: 25320 RVA: 0x0017B9E0 File Offset: 0x00179BE0
		internal static ArrayTypeSymbol CreateSZArray(TypeWithAnnotations elementTypeWithAnnotations, NamedTypeSymbol array)
		{
			return new ArrayTypeSymbol.SZArray(elementTypeWithAnnotations, array, ArrayTypeSymbol.GetSZArrayInterfaces(elementTypeWithAnnotations, array.ContainingAssembly));
		}

		// Token: 0x060062E9 RID: 25321 RVA: 0x0017B9F5 File Offset: 0x00179BF5
		internal static ArrayTypeSymbol CreateSZArray(TypeWithAnnotations elementTypeWithAnnotations, NamedTypeSymbol array, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<NamedTypeSymbol> constructedInterfaces)
		{
			return new ArrayTypeSymbol.SZArray(elementTypeWithAnnotations, array, constructedInterfaces);
		}

		// Token: 0x060062EA RID: 25322 RVA: 0x0017B9FF File Offset: 0x00179BFF
		internal static ArrayTypeSymbol CreateSZArray(AssemblySymbol declaringAssembly, TypeWithAnnotations elementType)
		{
			return ArrayTypeSymbol.CreateSZArray(elementType, declaringAssembly.GetSpecialType(SpecialType.System_Array), ArrayTypeSymbol.GetSZArrayInterfaces(elementType, declaringAssembly));
		}

		// Token: 0x060062EB RID: 25323 RVA: 0x0017BA18 File Offset: 0x00179C18
		internal ArrayTypeSymbol WithElementType(TypeWithAnnotations elementTypeWithAnnotations)
		{
			if (!this.ElementTypeWithAnnotations.IsSameAs(elementTypeWithAnnotations))
			{
				return this.WithElementTypeCore(elementTypeWithAnnotations);
			}
			return this;
		}

		// Token: 0x060062EC RID: 25324
		protected abstract ArrayTypeSymbol WithElementTypeCore(TypeWithAnnotations elementTypeWithAnnotations);

		// Token: 0x060062ED RID: 25325 RVA: 0x0017BA40 File Offset: 0x00179C40
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<NamedTypeSymbol> GetSZArrayInterfaces(TypeWithAnnotations elementTypeWithAnnotations, AssemblySymbol declaringAssembly)
		{
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			NamedTypeSymbol specialType = declaringAssembly.GetSpecialType(SpecialType.System_Collections_Generic_IList_T);
			if (!specialType.IsErrorType())
			{
				instance.Add(new ConstructedNamedTypeSymbol(specialType, ImmutableArray.Create<TypeWithAnnotations>(elementTypeWithAnnotations), false, null));
			}
			NamedTypeSymbol specialType2 = declaringAssembly.GetSpecialType(SpecialType.System_Collections_Generic_IReadOnlyList_T);
			if (!specialType2.IsErrorType())
			{
				instance.Add(new ConstructedNamedTypeSymbol(specialType2, ImmutableArray.Create<TypeWithAnnotations>(elementTypeWithAnnotations), false, null));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x060062EE RID: 25326
		public abstract int Rank { get; }

		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x060062EF RID: 25327
		public abstract bool IsSZArray { get; }

		// Token: 0x060062F0 RID: 25328 RVA: 0x0017BAA3 File Offset: 0x00179CA3
		internal bool HasSameShapeAs(ArrayTypeSymbol other)
		{
			return this.Rank == other.Rank && this.IsSZArray == other.IsSZArray;
		}

		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x060062F1 RID: 25329 RVA: 0x0017BAC3 File Offset: 0x00179CC3
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public virtual ImmutableArray<int> Sizes
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return ImmutableArray<int>.Empty;
			}
		}

		// Token: 0x17001761 RID: 5985
		// (get) Token: 0x060062F2 RID: 25330 RVA: 0x0017BACC File Offset: 0x00179CCC
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public virtual ImmutableArray<int> LowerBounds
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return default(ImmutableArray<int>);
			}
		}

		// Token: 0x060062F3 RID: 25331 RVA: 0x0017BAE4 File Offset: 0x00179CE4
		internal bool HasSameSizesAndLowerBoundsAs(ArrayTypeSymbol other)
		{
			if (!this.Sizes.SequenceEqual<int, int>(other.Sizes, null))
			{
				return false;
			}
			ImmutableArray<int> lowerBounds = this.LowerBounds;
			if (lowerBounds.IsDefault)
			{
				return other.LowerBounds.IsDefault;
			}
			ImmutableArray<int> lowerBounds2 = other.LowerBounds;
			return !lowerBounds2.IsDefault && lowerBounds.SequenceEqual<int, int>(lowerBounds2, null);
		}

		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x060062F4 RID: 25332
		internal abstract bool HasDefaultSizesAndLowerBounds { get; }

		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x060062F5 RID: 25333 RVA: 0x0017BB40 File Offset: 0x00179D40
		public TypeWithAnnotations ElementTypeWithAnnotations
		{
			get
			{
				return this._elementTypeWithAnnotations;
			}
		}

		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x060062F6 RID: 25334 RVA: 0x0017BB48 File Offset: 0x00179D48
		public TypeSymbol ElementType
		{
			get
			{
				return this._elementTypeWithAnnotations.Type;
			}
		}

		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x060062F7 RID: 25335 RVA: 0x0017BB55 File Offset: 0x00179D55
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				return this._baseType;
			}
		}

		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x060062F8 RID: 25336 RVA: 0x0017BB5D File Offset: 0x00179D5D
		public override bool IsReferenceType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x060062F9 RID: 25337 RVA: 0x0017BB60 File Offset: 0x00179D60
		public override bool IsValueType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060062FA RID: 25338 RVA: 0x0017BB63 File Offset: 0x00179D63
		internal sealed override ManagedKind GetManagedKind([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return ManagedKind.Managed;
		}

		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x060062FB RID: 25339 RVA: 0x0017BB66 File Offset: 0x00179D66
		public sealed override bool IsRefLikeType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x060062FC RID: 25340 RVA: 0x0017BB69 File Offset: 0x00179D69
		public sealed override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700176A RID: 5994
		// (get) Token: 0x060062FD RID: 25341 RVA: 0x0017BB6C File Offset: 0x00179D6C
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x060062FE RID: 25342 RVA: 0x0017BB6F File Offset: 0x00179D6F
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Symbol> GetMembers()
		{
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x060062FF RID: 25343 RVA: 0x0017BB76 File Offset: 0x00179D76
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x06006300 RID: 25344 RVA: 0x0017BB7D File Offset: 0x00179D7D
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006301 RID: 25345 RVA: 0x0017BB84 File Offset: 0x00179D84
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006302 RID: 25346 RVA: 0x0017BB8B File Offset: 0x00179D8B
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x06006303 RID: 25347 RVA: 0x0017BB92 File Offset: 0x00179D92
		public override SymbolKind Kind
		{
			get
			{
				return SymbolKind.ArrayType;
			}
		}

		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x06006304 RID: 25348 RVA: 0x0017BB95 File Offset: 0x00179D95
		public override TypeKind TypeKind
		{
			get
			{
				return TypeKind.Array;
			}
		}

		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x06006305 RID: 25349 RVA: 0x0017BB98 File Offset: 0x00179D98
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ContainingSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x06006306 RID: 25350 RVA: 0x0017BB9B File Offset: 0x00179D9B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x06006307 RID: 25351 RVA: 0x0017BBA2 File Offset: 0x00179DA2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x06006308 RID: 25352 RVA: 0x0017BBA9 File Offset: 0x00179DA9
		internal override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TArgument, [global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitArrayType(this, argument);
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x0017BBB3 File Offset: 0x00179DB3
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitArrayType(this);
		}

		// Token: 0x0600630A RID: 25354 RVA: 0x0017BBBC File Offset: 0x00179DBC
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitArrayType(this);
		}

		// Token: 0x0600630B RID: 25355 RVA: 0x0017BBC5 File Offset: 0x00179DC5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
		{
			return this.Equals(t2 as ArrayTypeSymbol, comparison);
		}

		// Token: 0x0600630C RID: 25356 RVA: 0x0017BBD4 File Offset: 0x00179DD4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private bool Equals(ArrayTypeSymbol other, TypeCompareKind comparison)
		{
			return this == other || (other != null && other.HasSameShapeAs(this) && other.ElementTypeWithAnnotations.Equals(this.ElementTypeWithAnnotations, comparison) && ((comparison & TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds) != TypeCompareKind.ConsiderEverything || this.HasSameSizesAndLowerBoundsAs(other)));
		}

		// Token: 0x0600630D RID: 25357 RVA: 0x0017BC20 File Offset: 0x00179E20
		public override int GetHashCode()
		{
			int num = 0;
			TypeSymbol typeSymbol = this;
			while (typeSymbol.TypeKind == TypeKind.Array)
			{
				ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)typeSymbol;
				num = Hash.Combine(arrayTypeSymbol.Rank, num);
				typeSymbol = arrayTypeSymbol.ElementType;
			}
			return Hash.Combine<TypeSymbol>(typeSymbol, num);
		}

		// Token: 0x0600630E RID: 25358 RVA: 0x0017BC5C File Offset: 0x00179E5C
		internal override void AddNullableTransforms(ArrayBuilder<byte> transforms)
		{
			this.ElementTypeWithAnnotations.AddNullableTransforms(transforms);
		}

		// Token: 0x0600630F RID: 25359 RVA: 0x0017BC78 File Offset: 0x00179E78
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal override bool ApplyNullableTransforms(byte defaultTransformFlag, ImmutableArray<byte> transforms, ref int position, [global::System.Runtime.CompilerServices.Nullable(1)] out TypeSymbol result)
		{
			TypeWithAnnotations typeWithAnnotations;
			if (!this.ElementTypeWithAnnotations.ApplyNullableTransforms(defaultTransformFlag, transforms, ref position, out typeWithAnnotations))
			{
				result = this;
				return false;
			}
			result = this.WithElementType(typeWithAnnotations);
			return true;
		}

		// Token: 0x06006310 RID: 25360 RVA: 0x0017BCAB File Offset: 0x00179EAB
		internal override TypeSymbol SetNullabilityForReferenceTypes(Func<TypeWithAnnotations, TypeWithAnnotations> transform)
		{
			return this.WithElementType(transform.Invoke(this.ElementTypeWithAnnotations));
		}

		// Token: 0x06006311 RID: 25361 RVA: 0x0017BCC0 File Offset: 0x00179EC0
		internal override TypeSymbol MergeEquivalentTypes(TypeSymbol other, VarianceKind variance)
		{
			TypeWithAnnotations typeWithAnnotations = this.ElementTypeWithAnnotations.MergeEquivalentTypes(((ArrayTypeSymbol)other).ElementTypeWithAnnotations, variance);
			return this.WithElementType(typeWithAnnotations);
		}

		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x06006312 RID: 25362 RVA: 0x0017BCEF File Offset: 0x00179EEF
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x06006313 RID: 25363 RVA: 0x0017BCF2 File Offset: 0x00179EF2
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x06006314 RID: 25364 RVA: 0x0017BCF5 File Offset: 0x00179EF5
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x06006315 RID: 25365 RVA: 0x0017BCF8 File Offset: 0x00179EF8
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006316 RID: 25366 RVA: 0x0017BCFC File Offset: 0x00179EFC
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			UseSiteInfo<AssemblySymbol> useSiteInfo = default(UseSiteInfo<AssemblySymbol>);
			base.DeriveUseSiteInfoFromType(ref useSiteInfo, this.ElementTypeWithAnnotations, Symbol.AllowedRequiredModifierType.None);
			return useSiteInfo;
		}

		// Token: 0x06006317 RID: 25367 RVA: 0x0017BD22 File Offset: 0x00179F22
		internal override bool GetUnificationUseSiteDiagnosticRecursive(ref DiagnosticInfo result, Symbol owner, ref HashSet<TypeSymbol> checkedTypes)
		{
			return this._elementTypeWithAnnotations.GetUnificationUseSiteDiagnosticRecursive(ref result, owner, ref checkedTypes) || (this._baseType != null && this._baseType.GetUnificationUseSiteDiagnosticRecursive(ref result, owner, ref checkedTypes)) || Symbol.GetUnificationUseSiteDiagnosticRecursive<NamedTypeSymbol>(ref result, this.InterfacesNoUseSiteDiagnostics(null), owner, ref checkedTypes);
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x0017BD5D File Offset: 0x00179F5D
		protected sealed override ISymbol CreateISymbol()
		{
			return new ArrayTypeSymbol(this, base.DefaultNullableAnnotation);
		}

		// Token: 0x06006319 RID: 25369 RVA: 0x0017BD6B File Offset: 0x00179F6B
		protected sealed override ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation)
		{
			return new ArrayTypeSymbol(this, nullableAnnotation);
		}

		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x0600631A RID: 25370 RVA: 0x0017BD74 File Offset: 0x00179F74
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x0600631B RID: 25371 RVA: 0x0017BD77 File Offset: 0x00179F77
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400198A RID: 6538
		private readonly TypeWithAnnotations _elementTypeWithAnnotations;

		// Token: 0x0400198B RID: 6539
		private readonly NamedTypeSymbol _baseType;

		// Token: 0x020008CB RID: 2251
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class SZArray : ArrayTypeSymbol
		{
			// Token: 0x06008E14 RID: 36372 RVA: 0x001F5BD1 File Offset: 0x001F3DD1
			internal SZArray(TypeWithAnnotations elementTypeWithAnnotations, NamedTypeSymbol array, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<NamedTypeSymbol> constructedInterfaces)
				: base(elementTypeWithAnnotations, array)
			{
				this._interfaces = constructedInterfaces;
			}

			// Token: 0x06008E15 RID: 36373 RVA: 0x001F5BE4 File Offset: 0x001F3DE4
			protected override ArrayTypeSymbol WithElementTypeCore(TypeWithAnnotations newElementType)
			{
				ImmutableArray<NamedTypeSymbol> immutableArray = this._interfaces.SelectAsArray((NamedTypeSymbol i, TypeSymbol t) => i.OriginalDefinition.Construct(new TypeSymbol[] { t }), newElementType.Type);
				return new ArrayTypeSymbol.SZArray(newElementType, this.BaseTypeNoUseSiteDiagnostics, immutableArray);
			}

			// Token: 0x17002833 RID: 10291
			// (get) Token: 0x06008E16 RID: 36374 RVA: 0x001F5C30 File Offset: 0x001F3E30
			public override int Rank
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x17002834 RID: 10292
			// (get) Token: 0x06008E17 RID: 36375 RVA: 0x001F5C33 File Offset: 0x001F3E33
			public override bool IsSZArray
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008E18 RID: 36376 RVA: 0x001F5C36 File Offset: 0x001F3E36
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved = null)
			{
				return this._interfaces;
			}

			// Token: 0x17002835 RID: 10293
			// (get) Token: 0x06008E19 RID: 36377 RVA: 0x001F5C3E File Offset: 0x001F3E3E
			internal override bool HasDefaultSizesAndLowerBounds
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04002655 RID: 9813
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			private readonly ImmutableArray<NamedTypeSymbol> _interfaces;
		}

		// Token: 0x020008CC RID: 2252
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private abstract class MDArray : ArrayTypeSymbol
		{
			// Token: 0x06008E1A RID: 36378 RVA: 0x001F5C41 File Offset: 0x001F3E41
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			internal MDArray(TypeWithAnnotations elementTypeWithAnnotations, int rank, NamedTypeSymbol array)
				: base(elementTypeWithAnnotations, array)
			{
				this._rank = rank;
			}

			// Token: 0x17002836 RID: 10294
			// (get) Token: 0x06008E1B RID: 36379 RVA: 0x001F5C52 File Offset: 0x001F3E52
			public sealed override int Rank
			{
				get
				{
					return this._rank;
				}
			}

			// Token: 0x17002837 RID: 10295
			// (get) Token: 0x06008E1C RID: 36380 RVA: 0x001F5C5A File Offset: 0x001F3E5A
			public sealed override bool IsSZArray
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008E1D RID: 36381 RVA: 0x001F5C5D File Offset: 0x001F3E5D
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			internal sealed override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved = null)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}

			// Token: 0x04002656 RID: 9814
			private readonly int _rank;
		}

		// Token: 0x020008CD RID: 2253
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class MDArrayNoSizesOrBounds : ArrayTypeSymbol.MDArray
		{
			// Token: 0x06008E1E RID: 36382 RVA: 0x001F5C64 File Offset: 0x001F3E64
			internal MDArrayNoSizesOrBounds(TypeWithAnnotations elementTypeWithAnnotations, int rank, NamedTypeSymbol array)
				: base(elementTypeWithAnnotations, rank, array)
			{
			}

			// Token: 0x06008E1F RID: 36383 RVA: 0x001F5C6F File Offset: 0x001F3E6F
			protected override ArrayTypeSymbol WithElementTypeCore(TypeWithAnnotations elementTypeWithAnnotations)
			{
				return new ArrayTypeSymbol.MDArrayNoSizesOrBounds(elementTypeWithAnnotations, this.Rank, this.BaseTypeNoUseSiteDiagnostics);
			}

			// Token: 0x17002838 RID: 10296
			// (get) Token: 0x06008E20 RID: 36384 RVA: 0x001F5C83 File Offset: 0x001F3E83
			internal override bool HasDefaultSizesAndLowerBounds
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x020008CE RID: 2254
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private sealed class MDArrayWithSizesAndBounds : ArrayTypeSymbol.MDArray
		{
			// Token: 0x06008E21 RID: 36385 RVA: 0x001F5C86 File Offset: 0x001F3E86
			internal MDArrayWithSizesAndBounds(TypeWithAnnotations elementTypeWithAnnotations, int rank, ImmutableArray<int> sizes, ImmutableArray<int> lowerBounds, [global::System.Runtime.CompilerServices.Nullable(1)] NamedTypeSymbol array)
				: base(elementTypeWithAnnotations, rank, array)
			{
				this._sizes = sizes.NullToEmpty<int>();
				this._lowerBounds = lowerBounds;
			}

			// Token: 0x06008E22 RID: 36386 RVA: 0x001F5CA6 File Offset: 0x001F3EA6
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			protected override ArrayTypeSymbol WithElementTypeCore(TypeWithAnnotations elementTypeWithAnnotations)
			{
				return new ArrayTypeSymbol.MDArrayWithSizesAndBounds(elementTypeWithAnnotations, this.Rank, this._sizes, this._lowerBounds, this.BaseTypeNoUseSiteDiagnostics);
			}

			// Token: 0x17002839 RID: 10297
			// (get) Token: 0x06008E23 RID: 36387 RVA: 0x001F5CC6 File Offset: 0x001F3EC6
			public override ImmutableArray<int> Sizes
			{
				get
				{
					return this._sizes;
				}
			}

			// Token: 0x1700283A RID: 10298
			// (get) Token: 0x06008E24 RID: 36388 RVA: 0x001F5CCE File Offset: 0x001F3ECE
			public override ImmutableArray<int> LowerBounds
			{
				get
				{
					return this._lowerBounds;
				}
			}

			// Token: 0x1700283B RID: 10299
			// (get) Token: 0x06008E25 RID: 36389 RVA: 0x001F5CD6 File Offset: 0x001F3ED6
			internal override bool HasDefaultSizesAndLowerBounds
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04002657 RID: 9815
			private readonly ImmutableArray<int> _sizes;

			// Token: 0x04002658 RID: 9816
			private readonly ImmutableArray<int> _lowerBounds;
		}
	}
}
