using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000243 RID: 579
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ArgumentSyntax : CSharpSyntaxNode
	{
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x000F9ADE File Offset: 0x000F7CDE
		[EditorBrowsable(1)]
		public SyntaxToken RefOrOutKeyword
		{
			get
			{
				return this.RefKindKeyword;
			}
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x000F9AE6 File Offset: 0x000F7CE6
		[EditorBrowsable(1)]
		public ArgumentSyntax WithRefOrOutKeyword(SyntaxToken refOrOutKeyword)
		{
			return this.Update(this.NameColon, refOrOutKeyword, this.Expression);
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000F9AFB File Offset: 0x000F7CFB
		internal ArgumentSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x060037AD RID: 14253 RVA: 0x000F9B06 File Offset: 0x000F7D06
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public NameColonSyntax NameColon
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRedAtZero<NameColonSyntax>(ref this.nameColon);
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x000F9B14 File Offset: 0x000F7D14
		public SyntaxToken RefKindKeyword
		{
			get
			{
				SyntaxToken refKindKeyword = ((ArgumentSyntax)base.Green).refKindKeyword;
				if (refKindKeyword == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, refKindKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x060037AF RID: 14255 RVA: 0x000F9B54 File Offset: 0x000F7D54
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 2);
			}
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000F9B64 File Offset: 0x000F7D64
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<ExpressionSyntax>(ref this.expression, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<NameColonSyntax>(ref this.nameColon);
			}
			return syntaxNode;
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000F9B9C File Offset: 0x000F7D9C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.expression;
				}
			}
			else
			{
				syntaxNode = this.nameColon;
			}
			return syntaxNode;
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000F9BC7 File Offset: 0x000F7DC7
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitArgument(this);
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000F9BD0 File Offset: 0x000F7DD0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitArgument(this);
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000F9BDC File Offset: 0x000F7DDC
		public ArgumentSyntax Update([global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon, SyntaxToken refKindKeyword, ExpressionSyntax expression)
		{
			if (nameColon == this.NameColon && !(refKindKeyword != this.RefKindKeyword) && expression == this.Expression)
			{
				return this;
			}
			ArgumentSyntax argumentSyntax = SyntaxFactory.Argument(nameColon, refKindKeyword, expression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return argumentSyntax;
			}
			return argumentSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000F9C2B File Offset: 0x000F7E2B
		public ArgumentSyntax WithNameColon([global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon)
		{
			return this.Update(nameColon, this.RefKindKeyword, this.Expression);
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000F9C40 File Offset: 0x000F7E40
		public ArgumentSyntax WithRefKindKeyword(SyntaxToken refKindKeyword)
		{
			return this.Update(this.NameColon, refKindKeyword, this.Expression);
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000F9C55 File Offset: 0x000F7E55
		public ArgumentSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.NameColon, this.RefKindKeyword, expression);
		}

		// Token: 0x04001394 RID: 5012
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private NameColonSyntax nameColon;

		// Token: 0x04001395 RID: 5013
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
