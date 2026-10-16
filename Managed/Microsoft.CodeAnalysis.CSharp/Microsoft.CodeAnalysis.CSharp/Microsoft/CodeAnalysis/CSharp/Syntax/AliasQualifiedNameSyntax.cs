using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000240 RID: 576
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class AliasQualifiedNameSyntax : NameSyntax
	{
		// Token: 0x06003767 RID: 14183 RVA: 0x000F94AA File Offset: 0x000F76AA
		internal override SimpleNameSyntax GetUnqualifiedName()
		{
			return this.Name;
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000F94B2 File Offset: 0x000F76B2
		internal override string ErrorDisplayName()
		{
			return this.Alias.ErrorDisplayName() + "::" + this.Name.ErrorDisplayName();
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000F94D4 File Offset: 0x000F76D4
		internal AliasQualifiedNameSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x0600376A RID: 14186 RVA: 0x000F94DF File Offset: 0x000F76DF
		public IdentifierNameSyntax Alias
		{
			get
			{
				return base.GetRedAtZero<IdentifierNameSyntax>(ref this.alias);
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x0600376B RID: 14187 RVA: 0x000F94ED File Offset: 0x000F76ED
		public SyntaxToken ColonColonToken
		{
			get
			{
				return new SyntaxToken(this, ((AliasQualifiedNameSyntax)base.Green).colonColonToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x0600376C RID: 14188 RVA: 0x000F9513 File Offset: 0x000F7713
		public SimpleNameSyntax Name
		{
			get
			{
				return base.GetRed<SimpleNameSyntax>(ref this.name, 2);
			}
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x000F9524 File Offset: 0x000F7724
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SimpleNameSyntax simpleNameSyntax;
			if (index != 0)
			{
				if (index != 2)
				{
					simpleNameSyntax = null;
				}
				else
				{
					simpleNameSyntax = base.GetRed<SimpleNameSyntax>(ref this.name, 2);
				}
			}
			else
			{
				simpleNameSyntax = base.GetRedAtZero<IdentifierNameSyntax>(ref this.alias);
			}
			return simpleNameSyntax;
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x000F955C File Offset: 0x000F775C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SimpleNameSyntax simpleNameSyntax;
			if (index != 0)
			{
				if (index != 2)
				{
					simpleNameSyntax = null;
				}
				else
				{
					simpleNameSyntax = this.name;
				}
			}
			else
			{
				simpleNameSyntax = this.alias;
			}
			return simpleNameSyntax;
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000F9587 File Offset: 0x000F7787
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAliasQualifiedName(this);
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000F9590 File Offset: 0x000F7790
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAliasQualifiedName(this);
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x000F959C File Offset: 0x000F779C
		public AliasQualifiedNameSyntax Update(IdentifierNameSyntax alias, SyntaxToken colonColonToken, SimpleNameSyntax name)
		{
			if (alias == this.Alias && !(colonColonToken != this.ColonColonToken) && name == this.Name)
			{
				return this;
			}
			AliasQualifiedNameSyntax aliasQualifiedNameSyntax = SyntaxFactory.AliasQualifiedName(alias, colonColonToken, name);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return aliasQualifiedNameSyntax;
			}
			return aliasQualifiedNameSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x000F95EB File Offset: 0x000F77EB
		public AliasQualifiedNameSyntax WithAlias(IdentifierNameSyntax alias)
		{
			return this.Update(alias, this.ColonColonToken, this.Name);
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x000F9600 File Offset: 0x000F7800
		public AliasQualifiedNameSyntax WithColonColonToken(SyntaxToken colonColonToken)
		{
			return this.Update(this.Alias, colonColonToken, this.Name);
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x000F9615 File Offset: 0x000F7815
		public AliasQualifiedNameSyntax WithName(SimpleNameSyntax name)
		{
			return this.Update(this.Alias, this.ColonColonToken, name);
		}

		// Token: 0x0400138F RID: 5007
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private IdentifierNameSyntax alias;

		// Token: 0x04001390 RID: 5008
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SimpleNameSyntax name;
	}
}
