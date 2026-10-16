using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004E9 RID: 1257
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class FunctionPointerParameterSymbol : ParameterSymbol
	{
		// Token: 0x06006A4D RID: 27213 RVA: 0x0018C338 File Offset: 0x0018A538
		public FunctionPointerParameterSymbol(TypeWithAnnotations typeWithAnnotations, RefKind refKind, int ordinal, FunctionPointerMethodSymbol containingSymbol, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> refCustomModifiers)
		{
			this.TypeWithAnnotations = typeWithAnnotations;
			this.RefKind = refKind;
			this.Ordinal = ordinal;
			this._containingSymbol = containingSymbol;
			this.RefCustomModifiers = refCustomModifiers;
		}

		// Token: 0x17001B44 RID: 6980
		// (get) Token: 0x06006A4E RID: 27214 RVA: 0x0018C365 File Offset: 0x0018A565
		public override TypeWithAnnotations TypeWithAnnotations { get; }

		// Token: 0x17001B45 RID: 6981
		// (get) Token: 0x06006A4F RID: 27215 RVA: 0x0018C36D File Offset: 0x0018A56D
		public override RefKind RefKind { get; }

		// Token: 0x17001B46 RID: 6982
		// (get) Token: 0x06006A50 RID: 27216 RVA: 0x0018C375 File Offset: 0x0018A575
		public override int Ordinal { get; }

		// Token: 0x17001B47 RID: 6983
		// (get) Token: 0x06006A51 RID: 27217 RVA: 0x0018C37D File Offset: 0x0018A57D
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x17001B48 RID: 6984
		// (get) Token: 0x06006A52 RID: 27218 RVA: 0x0018C385 File Offset: 0x0018A585
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06006A53 RID: 27219 RVA: 0x0018C390 File Offset: 0x0018A590
		public override bool Equals(Symbol other, TypeCompareKind compareKind)
		{
			if (this == other)
			{
				return true;
			}
			FunctionPointerParameterSymbol functionPointerParameterSymbol = other as FunctionPointerParameterSymbol;
			return functionPointerParameterSymbol != null && this.Equals(functionPointerParameterSymbol, compareKind);
		}

		// Token: 0x06006A54 RID: 27220 RVA: 0x0018C3B7 File Offset: 0x0018A5B7
		internal bool Equals(FunctionPointerParameterSymbol other, TypeCompareKind compareKind)
		{
			return other.Ordinal == this.Ordinal && this._containingSymbol.Equals(other._containingSymbol, compareKind);
		}

		// Token: 0x06006A55 RID: 27221 RVA: 0x0018C3DC File Offset: 0x0018A5DC
		internal bool MethodEqualityChecks(FunctionPointerParameterSymbol other, TypeCompareKind compareKind)
		{
			return FunctionPointerTypeSymbol.RefKindEquals(compareKind, this.RefKind, other.RefKind) && ((compareKind & TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds) != TypeCompareKind.ConsiderEverything || this.RefCustomModifiers.SequenceEqual<CustomModifier, CustomModifier>(other.RefCustomModifiers, null)) && this.TypeWithAnnotations.Equals(other.TypeWithAnnotations, compareKind);
		}

		// Token: 0x06006A56 RID: 27222 RVA: 0x0018C42D File Offset: 0x0018A62D
		public override int GetHashCode()
		{
			return Hash.Combine(this._containingSymbol.GetHashCode(), this.Ordinal + 1);
		}

		// Token: 0x06006A57 RID: 27223 RVA: 0x0018C448 File Offset: 0x0018A648
		internal int MethodHashCode()
		{
			return Hash.Combine(this.TypeWithAnnotations.GetHashCode(), FunctionPointerTypeSymbol.GetRefKindForHashCode(this.RefKind).GetHashCode());
		}

		// Token: 0x17001B49 RID: 6985
		// (get) Token: 0x06006A58 RID: 27224 RVA: 0x0018C487 File Offset: 0x0018A687
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17001B4A RID: 6986
		// (get) Token: 0x06006A59 RID: 27225 RVA: 0x0018C48E File Offset: 0x0018A68E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17001B4B RID: 6987
		// (get) Token: 0x06006A5A RID: 27226 RVA: 0x0018C495 File Offset: 0x0018A695
		public override bool IsDiscard
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B4C RID: 6988
		// (get) Token: 0x06006A5B RID: 27227 RVA: 0x0018C498 File Offset: 0x0018A698
		public override bool IsParams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B4D RID: 6989
		// (get) Token: 0x06006A5C RID: 27228 RVA: 0x0018C49B File Offset: 0x0018A69B
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B4E RID: 6990
		// (get) Token: 0x06006A5D RID: 27229 RVA: 0x0018C49E File Offset: 0x0018A69E
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001B4F RID: 6991
		// (get) Token: 0x06006A5E RID: 27230 RVA: 0x0018C4A1 File Offset: 0x0018A6A1
		internal override bool IsMetadataOptional
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B50 RID: 6992
		// (get) Token: 0x06006A5F RID: 27231 RVA: 0x0018C4A4 File Offset: 0x0018A6A4
		internal override bool IsMetadataIn
		{
			get
			{
				return this.RefKind == RefKind.In;
			}
		}

		// Token: 0x17001B51 RID: 6993
		// (get) Token: 0x06006A60 RID: 27232 RVA: 0x0018C4AF File Offset: 0x0018A6AF
		internal override bool IsMetadataOut
		{
			get
			{
				return this.RefKind == RefKind.Out;
			}
		}

		// Token: 0x17001B52 RID: 6994
		// (get) Token: 0x06006A61 RID: 27233 RVA: 0x0018C4BA File Offset: 0x0018A6BA
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override ConstantValue ExplicitDefaultConstantValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001B53 RID: 6995
		// (get) Token: 0x06006A62 RID: 27234 RVA: 0x0018C4BD File Offset: 0x0018A6BD
		internal override bool IsIDispatchConstant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B54 RID: 6996
		// (get) Token: 0x06006A63 RID: 27235 RVA: 0x0018C4C0 File Offset: 0x0018A6C0
		internal override bool IsIUnknownConstant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B55 RID: 6997
		// (get) Token: 0x06006A64 RID: 27236 RVA: 0x0018C4C3 File Offset: 0x0018A6C3
		internal override bool IsCallerFilePath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B56 RID: 6998
		// (get) Token: 0x06006A65 RID: 27237 RVA: 0x0018C4C6 File Offset: 0x0018A6C6
		internal override bool IsCallerLineNumber
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B57 RID: 6999
		// (get) Token: 0x06006A66 RID: 27238 RVA: 0x0018C4C9 File Offset: 0x0018A6C9
		internal override bool IsCallerMemberName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B58 RID: 7000
		// (get) Token: 0x06006A67 RID: 27239 RVA: 0x0018C4CC File Offset: 0x0018A6CC
		internal override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17001B59 RID: 7001
		// (get) Token: 0x06006A68 RID: 27240 RVA: 0x0018C4CF File Offset: 0x0018A6CF
		internal override ImmutableHashSet<string> NotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x04001A70 RID: 6768
		private readonly FunctionPointerMethodSymbol _containingSymbol;
	}
}
