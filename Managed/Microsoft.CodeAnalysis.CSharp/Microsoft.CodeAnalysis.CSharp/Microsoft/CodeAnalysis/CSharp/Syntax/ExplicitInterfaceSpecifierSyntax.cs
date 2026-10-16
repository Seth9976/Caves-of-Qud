using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200031D RID: 797
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ExplicitInterfaceSpecifierSyntax : CSharpSyntaxNode
	{
		// Token: 0x060044B6 RID: 17590 RVA: 0x00115536 File Offset: 0x00113736
		internal ExplicitInterfaceSpecifierSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x060044B7 RID: 17591 RVA: 0x00115541 File Offset: 0x00113741
		public NameSyntax Name
		{
			get
			{
				return base.GetRedAtZero<NameSyntax>(ref this.name);
			}
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x060044B8 RID: 17592 RVA: 0x0011554F File Offset: 0x0011374F
		public SyntaxToken DotToken
		{
			get
			{
				return new SyntaxToken(this, ((ExplicitInterfaceSpecifierSyntax)base.Green).dotToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x00115575 File Offset: 0x00113775
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<NameSyntax>(ref this.name);
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x00115588 File Offset: 0x00113788
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.name;
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x00115595 File Offset: 0x00113795
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitExplicitInterfaceSpecifier(this);
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x0011559E File Offset: 0x0011379E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitExplicitInterfaceSpecifier(this);
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x001155A8 File Offset: 0x001137A8
		public ExplicitInterfaceSpecifierSyntax Update(NameSyntax name, SyntaxToken dotToken)
		{
			if (name == this.Name && !(dotToken != this.DotToken))
			{
				return this;
			}
			ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax = SyntaxFactory.ExplicitInterfaceSpecifier(name, dotToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return explicitInterfaceSpecifierSyntax;
			}
			return explicitInterfaceSpecifierSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x001155ED File Offset: 0x001137ED
		public ExplicitInterfaceSpecifierSyntax WithName(NameSyntax name)
		{
			return this.Update(name, this.DotToken);
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x001155FC File Offset: 0x001137FC
		public ExplicitInterfaceSpecifierSyntax WithDotToken(SyntaxToken dotToken)
		{
			return this.Update(this.Name, dotToken);
		}

		// Token: 0x040014FC RID: 5372
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private NameSyntax name;
	}
}
