using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200028A RID: 650
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class UsingDirectiveSyntax : CSharpSyntaxNode
	{
		// Token: 0x06003D71 RID: 15729 RVA: 0x001086BE File Offset: 0x001068BE
		public UsingDirectiveSyntax Update(SyntaxToken usingKeyword, SyntaxToken staticKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax alias, NameSyntax name, SyntaxToken semicolonToken)
		{
			return this.Update(this.GlobalKeyword, usingKeyword, staticKeyword, alias, name, semicolonToken);
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x001086D3 File Offset: 0x001068D3
		internal UsingDirectiveSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06003D73 RID: 15731 RVA: 0x001086E0 File Offset: 0x001068E0
		public SyntaxToken GlobalKeyword
		{
			get
			{
				SyntaxToken globalKeyword = ((UsingDirectiveSyntax)base.Green).globalKeyword;
				if (globalKeyword == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, globalKeyword, base.Position, 0);
			}
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06003D74 RID: 15732 RVA: 0x00108719 File Offset: 0x00106919
		public SyntaxToken UsingKeyword
		{
			get
			{
				return new SyntaxToken(this, ((UsingDirectiveSyntax)base.Green).usingKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06003D75 RID: 15733 RVA: 0x00108740 File Offset: 0x00106940
		public SyntaxToken StaticKeyword
		{
			get
			{
				SyntaxToken staticKeyword = ((UsingDirectiveSyntax)base.Green).staticKeyword;
				if (staticKeyword == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, staticKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06003D76 RID: 15734 RVA: 0x00108780 File Offset: 0x00106980
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public NameEqualsSyntax Alias
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<NameEqualsSyntax>(ref this.alias, 3);
			}
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06003D77 RID: 15735 RVA: 0x0010878F File Offset: 0x0010698F
		public NameSyntax Name
		{
			get
			{
				return base.GetRed<NameSyntax>(ref this.name, 4);
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06003D78 RID: 15736 RVA: 0x0010879E File Offset: 0x0010699E
		public SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((UsingDirectiveSyntax)base.Green).semicolonToken, this.GetChildPosition(5), base.GetChildIndex(5));
			}
		}

		// Token: 0x06003D79 RID: 15737 RVA: 0x001087C4 File Offset: 0x001069C4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 3)
			{
				if (index != 4)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<NameSyntax>(ref this.name, 4);
				}
			}
			else
			{
				syntaxNode = base.GetRed<NameEqualsSyntax>(ref this.alias, 3);
			}
			return syntaxNode;
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x00108800 File Offset: 0x00106A00
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 3)
			{
				if (index != 4)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.name;
				}
			}
			else
			{
				syntaxNode = this.alias;
			}
			return syntaxNode;
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x0010882C File Offset: 0x00106A2C
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitUsingDirective(this);
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x00108835 File Offset: 0x00106A35
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitUsingDirective(this);
		}

		// Token: 0x06003D7D RID: 15741 RVA: 0x00108840 File Offset: 0x00106A40
		public UsingDirectiveSyntax Update(SyntaxToken globalKeyword, SyntaxToken usingKeyword, SyntaxToken staticKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax alias, NameSyntax name, SyntaxToken semicolonToken)
		{
			if (!(globalKeyword != this.GlobalKeyword) && !(usingKeyword != this.UsingKeyword) && !(staticKeyword != this.StaticKeyword) && alias == this.Alias && name == this.Name && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			UsingDirectiveSyntax usingDirectiveSyntax = SyntaxFactory.UsingDirective(globalKeyword, usingKeyword, staticKeyword, alias, name, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return usingDirectiveSyntax;
			}
			return usingDirectiveSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x001088C2 File Offset: 0x00106AC2
		public UsingDirectiveSyntax WithGlobalKeyword(SyntaxToken globalKeyword)
		{
			return this.Update(globalKeyword, this.UsingKeyword, this.StaticKeyword, this.Alias, this.Name, this.SemicolonToken);
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x001088E9 File Offset: 0x00106AE9
		public UsingDirectiveSyntax WithUsingKeyword(SyntaxToken usingKeyword)
		{
			return this.Update(this.GlobalKeyword, usingKeyword, this.StaticKeyword, this.Alias, this.Name, this.SemicolonToken);
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x00108910 File Offset: 0x00106B10
		public UsingDirectiveSyntax WithStaticKeyword(SyntaxToken staticKeyword)
		{
			return this.Update(this.GlobalKeyword, this.UsingKeyword, staticKeyword, this.Alias, this.Name, this.SemicolonToken);
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x00108937 File Offset: 0x00106B37
		public UsingDirectiveSyntax WithAlias([global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax alias)
		{
			return this.Update(this.GlobalKeyword, this.UsingKeyword, this.StaticKeyword, alias, this.Name, this.SemicolonToken);
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x0010895E File Offset: 0x00106B5E
		public UsingDirectiveSyntax WithName(NameSyntax name)
		{
			return this.Update(this.GlobalKeyword, this.UsingKeyword, this.StaticKeyword, this.Alias, name, this.SemicolonToken);
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x00108985 File Offset: 0x00106B85
		public UsingDirectiveSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.GlobalKeyword, this.UsingKeyword, this.StaticKeyword, this.Alias, this.Name, semicolonToken);
		}

		// Token: 0x0400143C RID: 5180
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private NameEqualsSyntax alias;

		// Token: 0x0400143D RID: 5181
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private NameSyntax name;
	}
}
