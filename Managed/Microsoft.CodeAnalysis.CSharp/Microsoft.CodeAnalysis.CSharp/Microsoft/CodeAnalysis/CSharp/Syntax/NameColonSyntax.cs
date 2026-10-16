using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002BC RID: 700
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class NameColonSyntax : CSharpSyntaxNode
	{
		// Token: 0x06003FB2 RID: 16306 RVA: 0x0010C606 File Offset: 0x0010A806
		internal NameColonSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06003FB3 RID: 16307 RVA: 0x0010C611 File Offset: 0x0010A811
		public IdentifierNameSyntax Name
		{
			get
			{
				return base.GetRedAtZero<IdentifierNameSyntax>(ref this.name);
			}
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06003FB4 RID: 16308 RVA: 0x0010C61F File Offset: 0x0010A81F
		public SyntaxToken ColonToken
		{
			get
			{
				return new SyntaxToken(this, ((NameColonSyntax)base.Green).colonToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x0010C645 File Offset: 0x0010A845
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<IdentifierNameSyntax>(ref this.name);
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x0010C658 File Offset: 0x0010A858
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.name;
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x0010C665 File Offset: 0x0010A865
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitNameColon(this);
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x0010C66E File Offset: 0x0010A86E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitNameColon(this);
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x0010C678 File Offset: 0x0010A878
		public NameColonSyntax Update(IdentifierNameSyntax name, SyntaxToken colonToken)
		{
			if (name == this.Name && !(colonToken != this.ColonToken))
			{
				return this;
			}
			NameColonSyntax nameColonSyntax = SyntaxFactory.NameColon(name, colonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return nameColonSyntax;
			}
			return nameColonSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x0010C6BD File Offset: 0x0010A8BD
		public NameColonSyntax WithName(IdentifierNameSyntax name)
		{
			return this.Update(name, this.ColonToken);
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x0010C6CC File Offset: 0x0010A8CC
		public NameColonSyntax WithColonToken(SyntaxToken colonToken)
		{
			return this.Update(this.Name, colonToken);
		}

		// Token: 0x0400147B RID: 5243
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private IdentifierNameSyntax name;
	}
}
