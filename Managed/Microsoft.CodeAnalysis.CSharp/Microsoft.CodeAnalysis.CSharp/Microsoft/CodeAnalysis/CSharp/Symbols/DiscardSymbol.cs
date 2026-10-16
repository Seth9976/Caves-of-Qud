using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004DA RID: 1242
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DiscardSymbol : Symbol
	{
		// Token: 0x060068F7 RID: 26871 RVA: 0x0018A59D File Offset: 0x0018879D
		public DiscardSymbol(TypeWithAnnotations typeWithAnnotations)
		{
			this.TypeWithAnnotations = typeWithAnnotations;
		}

		// Token: 0x17001A72 RID: 6770
		// (get) Token: 0x060068F8 RID: 26872 RVA: 0x0018A5AC File Offset: 0x001887AC
		public TypeWithAnnotations TypeWithAnnotations { get; }

		// Token: 0x17001A73 RID: 6771
		// (get) Token: 0x060068F9 RID: 26873 RVA: 0x0018A5B4 File Offset: 0x001887B4
		public override Symbol ContainingSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001A74 RID: 6772
		// (get) Token: 0x060068FA RID: 26874 RVA: 0x0018A5B7 File Offset: 0x001887B7
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x17001A75 RID: 6773
		// (get) Token: 0x060068FB RID: 26875 RVA: 0x0018A5BA File Offset: 0x001887BA
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17001A76 RID: 6774
		// (get) Token: 0x060068FC RID: 26876 RVA: 0x0018A5C1 File Offset: 0x001887C1
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A77 RID: 6775
		// (get) Token: 0x060068FD RID: 26877 RVA: 0x0018A5C4 File Offset: 0x001887C4
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A78 RID: 6776
		// (get) Token: 0x060068FE RID: 26878 RVA: 0x0018A5C7 File Offset: 0x001887C7
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001A79 RID: 6777
		// (get) Token: 0x060068FF RID: 26879 RVA: 0x0018A5CA File Offset: 0x001887CA
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A7A RID: 6778
		// (get) Token: 0x06006900 RID: 26880 RVA: 0x0018A5CD File Offset: 0x001887CD
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A7B RID: 6779
		// (get) Token: 0x06006901 RID: 26881 RVA: 0x0018A5D0 File Offset: 0x001887D0
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A7C RID: 6780
		// (get) Token: 0x06006902 RID: 26882 RVA: 0x0018A5D3 File Offset: 0x001887D3
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A7D RID: 6781
		// (get) Token: 0x06006903 RID: 26883 RVA: 0x0018A5D6 File Offset: 0x001887D6
		public override SymbolKind Kind
		{
			get
			{
				return SymbolKind.Discard;
			}
		}

		// Token: 0x17001A7E RID: 6782
		// (get) Token: 0x06006904 RID: 26884 RVA: 0x0018A5DA File Offset: 0x001887DA
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17001A7F RID: 6783
		// (get) Token: 0x06006905 RID: 26885 RVA: 0x0018A5E1 File Offset: 0x001887E1
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006906 RID: 26886 RVA: 0x0018A5E4 File Offset: 0x001887E4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TArgument, [global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument a)
		{
			return visitor.VisitDiscard(this, a);
		}

		// Token: 0x06006907 RID: 26887 RVA: 0x0018A5EE File Offset: 0x001887EE
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitDiscard(this);
		}

		// Token: 0x06006908 RID: 26888 RVA: 0x0018A5F7 File Offset: 0x001887F7
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitDiscard(this);
		}

		// Token: 0x06006909 RID: 26889 RVA: 0x0018A600 File Offset: 0x00188800
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			DiscardSymbol discardSymbol = obj as DiscardSymbol;
			return discardSymbol != null && this.TypeWithAnnotations.Equals(discardSymbol.TypeWithAnnotations, compareKind);
		}

		// Token: 0x0600690A RID: 26890 RVA: 0x0018A630 File Offset: 0x00188830
		public override int GetHashCode()
		{
			return this.TypeWithAnnotations.GetHashCode();
		}

		// Token: 0x0600690B RID: 26891 RVA: 0x0018A651 File Offset: 0x00188851
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected override ISymbol CreateISymbol()
		{
			return new DiscardSymbol(this);
		}
	}
}
