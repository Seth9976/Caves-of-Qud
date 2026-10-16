using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200030A RID: 778
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class NameEqualsSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600433B RID: 17211 RVA: 0x001123DD File Offset: 0x001105DD
		internal NameEqualsSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x0600433C RID: 17212 RVA: 0x001123E8 File Offset: 0x001105E8
		public IdentifierNameSyntax Name
		{
			get
			{
				return base.GetRedAtZero<IdentifierNameSyntax>(ref this.name);
			}
		}

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x001123F6 File Offset: 0x001105F6
		public SyntaxToken EqualsToken
		{
			get
			{
				return new SyntaxToken(this, ((NameEqualsSyntax)base.Green).equalsToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x0011241C File Offset: 0x0011061C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<IdentifierNameSyntax>(ref this.name);
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x0011242F File Offset: 0x0011062F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.name;
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x0011243C File Offset: 0x0011063C
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitNameEquals(this);
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x00112445 File Offset: 0x00110645
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitNameEquals(this);
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x00112450 File Offset: 0x00110650
		public NameEqualsSyntax Update(IdentifierNameSyntax name, SyntaxToken equalsToken)
		{
			if (name == this.Name && !(equalsToken != this.EqualsToken))
			{
				return this;
			}
			NameEqualsSyntax nameEqualsSyntax = SyntaxFactory.NameEquals(name, equalsToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return nameEqualsSyntax;
			}
			return nameEqualsSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x00112495 File Offset: 0x00110695
		public NameEqualsSyntax WithName(IdentifierNameSyntax name)
		{
			return this.Update(name, this.EqualsToken);
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x001124A4 File Offset: 0x001106A4
		public NameEqualsSyntax WithEqualsToken(SyntaxToken equalsToken)
		{
			return this.Update(this.Name, equalsToken);
		}

		// Token: 0x040014DC RID: 5340
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private IdentifierNameSyntax name;
	}
}
