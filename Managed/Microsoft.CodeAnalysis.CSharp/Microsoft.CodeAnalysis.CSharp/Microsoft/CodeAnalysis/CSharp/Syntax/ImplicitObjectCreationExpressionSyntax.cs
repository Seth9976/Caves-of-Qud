using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002C2 RID: 706
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ImplicitObjectCreationExpressionSyntax : BaseObjectCreationExpressionSyntax
	{
		// Token: 0x06003FF7 RID: 16375 RVA: 0x0010CC11 File Offset: 0x0010AE11
		internal ImplicitObjectCreationExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x0010CC1C File Offset: 0x0010AE1C
		public override SyntaxToken NewKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ImplicitObjectCreationExpressionSyntax)base.Green).newKeyword, base.Position, 0);
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x06003FF9 RID: 16377 RVA: 0x0010CC3B File Offset: 0x0010AE3B
		public override ArgumentListSyntax ArgumentList
		{
			get
			{
				return base.GetRed<ArgumentListSyntax>(ref this.argumentList, 1);
			}
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06003FFA RID: 16378 RVA: 0x0010CC4A File Offset: 0x0010AE4A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override InitializerExpressionSyntax Initializer
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<InitializerExpressionSyntax>(ref this.initializer, 2);
			}
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x0010CC5C File Offset: 0x0010AE5C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
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
					syntaxNode = base.GetRed<InitializerExpressionSyntax>(ref this.initializer, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRed<ArgumentListSyntax>(ref this.argumentList, 1);
			}
			return syntaxNode;
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x0010CC98 File Offset: 0x0010AE98
		[global::System.Runtime.CompilerServices.NullableContext(2)]
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

		// Token: 0x06003FFD RID: 16381 RVA: 0x0010CCC4 File Offset: 0x0010AEC4
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitImplicitObjectCreationExpression(this);
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x0010CCCD File Offset: 0x0010AECD
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitImplicitObjectCreationExpression(this);
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x0010CCD8 File Offset: 0x0010AED8
		public ImplicitObjectCreationExpressionSyntax Update(SyntaxToken newKeyword, ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			if (!(newKeyword != this.NewKeyword) && argumentList == this.ArgumentList && initializer == this.Initializer)
			{
				return this;
			}
			ImplicitObjectCreationExpressionSyntax implicitObjectCreationExpressionSyntax = SyntaxFactory.ImplicitObjectCreationExpression(newKeyword, argumentList, initializer);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return implicitObjectCreationExpressionSyntax;
			}
			return implicitObjectCreationExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x0010CD27 File Offset: 0x0010AF27
		internal override BaseObjectCreationExpressionSyntax WithNewKeywordCore(SyntaxToken newKeyword)
		{
			return this.WithNewKeyword(newKeyword);
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x0010CD30 File Offset: 0x0010AF30
		public new ImplicitObjectCreationExpressionSyntax WithNewKeyword(SyntaxToken newKeyword)
		{
			return this.Update(newKeyword, this.ArgumentList, this.Initializer);
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x0010CD45 File Offset: 0x0010AF45
		internal override BaseObjectCreationExpressionSyntax WithArgumentListCore([global::System.Runtime.CompilerServices.Nullable(2)] ArgumentListSyntax argumentList)
		{
			if (argumentList == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return this.WithArgumentList(argumentList);
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x0010CD5D File Offset: 0x0010AF5D
		public new ImplicitObjectCreationExpressionSyntax WithArgumentList(ArgumentListSyntax argumentList)
		{
			return this.Update(this.NewKeyword, argumentList, this.Initializer);
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x0010CD72 File Offset: 0x0010AF72
		internal override BaseObjectCreationExpressionSyntax WithInitializerCore([global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			return this.WithInitializer(initializer);
		}

		// Token: 0x06004005 RID: 16389 RVA: 0x0010CD7B File Offset: 0x0010AF7B
		public new ImplicitObjectCreationExpressionSyntax WithInitializer([global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			return this.Update(this.NewKeyword, this.ArgumentList, initializer);
		}

		// Token: 0x06004006 RID: 16390 RVA: 0x0010CD90 File Offset: 0x0010AF90
		internal override BaseObjectCreationExpressionSyntax AddArgumentListArgumentsCore(params ArgumentSyntax[] items)
		{
			return this.AddArgumentListArguments(items);
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x0010CD9C File Offset: 0x0010AF9C
		public new ImplicitObjectCreationExpressionSyntax AddArgumentListArguments(params ArgumentSyntax[] items)
		{
			return this.WithArgumentList(this.ArgumentList.WithArguments(this.ArgumentList.Arguments.AddRange(items)));
		}

		// Token: 0x04001482 RID: 5250
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArgumentListSyntax argumentList;

		// Token: 0x04001483 RID: 5251
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private InitializerExpressionSyntax initializer;
	}
}
