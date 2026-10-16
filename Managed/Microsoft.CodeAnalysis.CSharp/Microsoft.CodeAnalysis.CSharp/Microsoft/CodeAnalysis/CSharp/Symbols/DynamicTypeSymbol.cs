using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004DC RID: 1244
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DynamicTypeSymbol : TypeSymbol
	{
		// Token: 0x0600690F RID: 26895 RVA: 0x0018A68C File Offset: 0x0018888C
		private DynamicTypeSymbol()
		{
		}

		// Token: 0x17001A80 RID: 6784
		// (get) Token: 0x06006910 RID: 26896 RVA: 0x0018A694 File Offset: 0x00188894
		public override string Name
		{
			get
			{
				return "dynamic";
			}
		}

		// Token: 0x17001A81 RID: 6785
		// (get) Token: 0x06006911 RID: 26897 RVA: 0x0018A69B File Offset: 0x0018889B
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A82 RID: 6786
		// (get) Token: 0x06006912 RID: 26898 RVA: 0x0018A69E File Offset: 0x0018889E
		public override bool IsReferenceType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001A83 RID: 6787
		// (get) Token: 0x06006913 RID: 26899 RVA: 0x0018A6A1 File Offset: 0x001888A1
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A84 RID: 6788
		// (get) Token: 0x06006914 RID: 26900 RVA: 0x0018A6A4 File Offset: 0x001888A4
		public override SymbolKind Kind
		{
			get
			{
				return SymbolKind.DynamicType;
			}
		}

		// Token: 0x17001A85 RID: 6789
		// (get) Token: 0x06006915 RID: 26901 RVA: 0x0018A6A7 File Offset: 0x001888A7
		public override TypeKind TypeKind
		{
			get
			{
				return TypeKind.Dynamic;
			}
		}

		// Token: 0x17001A86 RID: 6790
		// (get) Token: 0x06006916 RID: 26902 RVA: 0x0018A6AA File Offset: 0x001888AA
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17001A87 RID: 6791
		// (get) Token: 0x06006917 RID: 26903 RVA: 0x0018A6B1 File Offset: 0x001888B1
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17001A88 RID: 6792
		// (get) Token: 0x06006918 RID: 26904 RVA: 0x0018A6B8 File Offset: 0x001888B8
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06006919 RID: 26905 RVA: 0x0018A6BB File Offset: 0x001888BB
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x17001A89 RID: 6793
		// (get) Token: 0x0600691A RID: 26906 RVA: 0x0018A6C2 File Offset: 0x001888C2
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A8A RID: 6794
		// (get) Token: 0x0600691B RID: 26907 RVA: 0x0018A6C5 File Offset: 0x001888C5
		public override bool IsValueType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600691C RID: 26908 RVA: 0x0018A6C8 File Offset: 0x001888C8
		internal sealed override ManagedKind GetManagedKind([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return ManagedKind.Managed;
		}

		// Token: 0x17001A8B RID: 6795
		// (get) Token: 0x0600691D RID: 26909 RVA: 0x0018A6CB File Offset: 0x001888CB
		public sealed override bool IsRefLikeType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A8C RID: 6796
		// (get) Token: 0x0600691E RID: 26910 RVA: 0x0018A6CE File Offset: 0x001888CE
		public sealed override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A8D RID: 6797
		// (get) Token: 0x0600691F RID: 26911 RVA: 0x0018A6D1 File Offset: 0x001888D1
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06006920 RID: 26912 RVA: 0x0018A6D4 File Offset: 0x001888D4
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Symbol> GetMembers()
		{
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x06006921 RID: 26913 RVA: 0x0018A6DB File Offset: 0x001888DB
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x06006922 RID: 26914 RVA: 0x0018A6E2 File Offset: 0x001888E2
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006923 RID: 26915 RVA: 0x0018A6E9 File Offset: 0x001888E9
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006924 RID: 26916 RVA: 0x0018A6F0 File Offset: 0x001888F0
		internal override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TArgument, [global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitDynamicType(this, argument);
		}

		// Token: 0x06006925 RID: 26917 RVA: 0x0018A6FA File Offset: 0x001888FA
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitDynamicType(this);
		}

		// Token: 0x06006926 RID: 26918 RVA: 0x0018A703 File Offset: 0x00188903
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitDynamicType(this);
		}

		// Token: 0x17001A8E RID: 6798
		// (get) Token: 0x06006927 RID: 26919 RVA: 0x0018A70C File Offset: 0x0018890C
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ContainingSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001A8F RID: 6799
		// (get) Token: 0x06006928 RID: 26920 RVA: 0x0018A70F File Offset: 0x0018890F
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x06006929 RID: 26921 RVA: 0x0018A712 File Offset: 0x00188912
		internal override bool GetUnificationUseSiteDiagnosticRecursive(ref DiagnosticInfo result, Symbol owner, ref HashSet<TypeSymbol> checkedTypes)
		{
			return false;
		}

		// Token: 0x0600692A RID: 26922 RVA: 0x0018A715 File Offset: 0x00188915
		public override int GetHashCode()
		{
			return 1;
		}

		// Token: 0x0600692B RID: 26923 RVA: 0x0018A718 File Offset: 0x00188918
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
		{
			if (t2 == null)
			{
				return false;
			}
			if (this == t2 || t2.TypeKind == TypeKind.Dynamic)
			{
				return true;
			}
			if ((comparison & TypeCompareKind.IgnoreDynamic) != TypeCompareKind.ConsiderEverything)
			{
				NamedTypeSymbol namedTypeSymbol = t2 as NamedTypeSymbol;
				return namedTypeSymbol != null && namedTypeSymbol.SpecialType == SpecialType.System_Object;
			}
			return false;
		}

		// Token: 0x0600692C RID: 26924 RVA: 0x0018A755 File Offset: 0x00188955
		internal override void AddNullableTransforms(ArrayBuilder<byte> transforms)
		{
		}

		// Token: 0x0600692D RID: 26925 RVA: 0x0018A757 File Offset: 0x00188957
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal override bool ApplyNullableTransforms(byte defaultTransformFlag, ImmutableArray<byte> transforms, ref int position, [global::System.Runtime.CompilerServices.Nullable(1)] out TypeSymbol result)
		{
			result = this;
			return true;
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x0018A75E File Offset: 0x0018895E
		internal override TypeSymbol SetNullabilityForReferenceTypes(Func<TypeWithAnnotations, TypeWithAnnotations> transform)
		{
			return this;
		}

		// Token: 0x0600692F RID: 26927 RVA: 0x0018A761 File Offset: 0x00188961
		internal override TypeSymbol MergeEquivalentTypes(TypeSymbol other, VarianceKind variance)
		{
			return this;
		}

		// Token: 0x06006930 RID: 26928 RVA: 0x0018A764 File Offset: 0x00188964
		protected override ISymbol CreateISymbol()
		{
			return new DynamicTypeSymbol(this, base.DefaultNullableAnnotation);
		}

		// Token: 0x06006931 RID: 26929 RVA: 0x0018A772 File Offset: 0x00188972
		protected sealed override ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation)
		{
			return new DynamicTypeSymbol(this, nullableAnnotation);
		}

		// Token: 0x17001A90 RID: 6800
		// (get) Token: 0x06006932 RID: 26930 RVA: 0x0018A77B File Offset: 0x0018897B
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A91 RID: 6801
		// (get) Token: 0x06006933 RID: 26931 RVA: 0x0018A77E File Offset: 0x0018897E
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001A3F RID: 6719
		internal static readonly DynamicTypeSymbol Instance = new DynamicTypeSymbol();
	}
}
