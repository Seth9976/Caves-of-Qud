using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000291 RID: 657
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ArrayTypeSyntax : TypeSyntax
	{
		// Token: 0x06003DDE RID: 15838 RVA: 0x00109608 File Offset: 0x00107808
		internal ArrayTypeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x00109613 File Offset: 0x00107813
		public TypeSyntax ElementType
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.elementType);
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06003DE0 RID: 15840 RVA: 0x00109621 File Offset: 0x00107821
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<ArrayRankSpecifierSyntax> RankSpecifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<ArrayRankSpecifierSyntax>(base.GetRed(ref this.rankSpecifiers, 1));
			}
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x00109638 File Offset: 0x00107838
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed(ref this.rankSpecifiers, 1);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<TypeSyntax>(ref this.elementType);
			}
			return syntaxNode;
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x00109670 File Offset: 0x00107870
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.rankSpecifiers;
				}
			}
			else
			{
				syntaxNode = this.elementType;
			}
			return syntaxNode;
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x0010969B File Offset: 0x0010789B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitArrayType(this);
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x001096A4 File Offset: 0x001078A4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitArrayType(this);
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x001096B0 File Offset: 0x001078B0
		public ArrayTypeSyntax Update(TypeSyntax elementType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ArrayRankSpecifierSyntax> rankSpecifiers)
		{
			if (elementType == this.ElementType && !(rankSpecifiers != this.RankSpecifiers))
			{
				return this;
			}
			ArrayTypeSyntax arrayTypeSyntax = SyntaxFactory.ArrayType(elementType, rankSpecifiers);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return arrayTypeSyntax;
			}
			return arrayTypeSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x001096F5 File Offset: 0x001078F5
		public ArrayTypeSyntax WithElementType(TypeSyntax elementType)
		{
			return this.Update(elementType, this.RankSpecifiers);
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x00109704 File Offset: 0x00107904
		public ArrayTypeSyntax WithRankSpecifiers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ArrayRankSpecifierSyntax> rankSpecifiers)
		{
			return this.Update(this.ElementType, rankSpecifiers);
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x00109714 File Offset: 0x00107914
		public ArrayTypeSyntax AddRankSpecifiers(params ArrayRankSpecifierSyntax[] items)
		{
			return this.WithRankSpecifiers(this.RankSpecifiers.AddRange(items));
		}

		// Token: 0x0400144D RID: 5197
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax elementType;

		// Token: 0x0400144E RID: 5198
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode rankSpecifiers;
	}
}
