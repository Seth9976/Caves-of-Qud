using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002F2 RID: 754
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class VariableDeclaratorSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600420F RID: 16911 RVA: 0x001106BA File Offset: 0x0010E8BA
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal VariableDeclaratorSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06004210 RID: 16912 RVA: 0x001106C5 File Offset: 0x0010E8C5
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((VariableDeclaratorSyntax)base.Green).identifier, base.Position, 0);
			}
		}

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06004211 RID: 16913 RVA: 0x001106E4 File Offset: 0x0010E8E4
		public BracketedArgumentListSyntax ArgumentList
		{
			get
			{
				return base.GetRed<BracketedArgumentListSyntax>(ref this.argumentList, 1);
			}
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06004212 RID: 16914 RVA: 0x001106F3 File Offset: 0x0010E8F3
		public EqualsValueClauseSyntax Initializer
		{
			get
			{
				return base.GetRed<EqualsValueClauseSyntax>(ref this.initializer, 2);
			}
		}

		// Token: 0x06004213 RID: 16915 RVA: 0x00110704 File Offset: 0x0010E904
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 1)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<EqualsValueClauseSyntax>(ref this.initializer, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRed<BracketedArgumentListSyntax>(ref this.argumentList, 1);
			}
			return syntaxNode;
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x00110740 File Offset: 0x0010E940
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 1)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.initializer;
				}
			}
			else
			{
				syntaxNode = this.argumentList;
			}
			return syntaxNode;
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x0011076C File Offset: 0x0010E96C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitVariableDeclarator(this);
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x00110775 File Offset: 0x0010E975
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitVariableDeclarator(this);
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x00110780 File Offset: 0x0010E980
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public VariableDeclaratorSyntax Update(SyntaxToken identifier, BracketedArgumentListSyntax argumentList, EqualsValueClauseSyntax initializer)
		{
			if (!(identifier != this.Identifier) && argumentList == this.ArgumentList && initializer == this.Initializer)
			{
				return this;
			}
			VariableDeclaratorSyntax variableDeclaratorSyntax = SyntaxFactory.VariableDeclarator(identifier, argumentList, initializer);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return variableDeclaratorSyntax;
			}
			return variableDeclaratorSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x001107CF File Offset: 0x0010E9CF
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public VariableDeclaratorSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(identifier, this.ArgumentList, this.Initializer);
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x001107E4 File Offset: 0x0010E9E4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public VariableDeclaratorSyntax WithArgumentList([global::System.Runtime.CompilerServices.Nullable(2)] BracketedArgumentListSyntax argumentList)
		{
			return this.Update(this.Identifier, argumentList, this.Initializer);
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x001107F9 File Offset: 0x0010E9F9
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public VariableDeclaratorSyntax WithInitializer([global::System.Runtime.CompilerServices.Nullable(2)] EqualsValueClauseSyntax initializer)
		{
			return this.Update(this.Identifier, this.ArgumentList, initializer);
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x00110810 File Offset: 0x0010EA10
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public VariableDeclaratorSyntax AddArgumentListArguments(params ArgumentSyntax[] items)
		{
			BracketedArgumentListSyntax bracketedArgumentListSyntax;
			if ((bracketedArgumentListSyntax = this.ArgumentList) == null)
			{
				bracketedArgumentListSyntax = SyntaxFactory.BracketedArgumentList(default(SeparatedSyntaxList<ArgumentSyntax>));
			}
			BracketedArgumentListSyntax bracketedArgumentListSyntax2 = bracketedArgumentListSyntax;
			return this.WithArgumentList(bracketedArgumentListSyntax2.WithArguments(bracketedArgumentListSyntax2.Arguments.AddRange(items)));
		}

		// Token: 0x040014C1 RID: 5313
		private BracketedArgumentListSyntax argumentList;

		// Token: 0x040014C2 RID: 5314
		private EqualsValueClauseSyntax initializer;
	}
}
