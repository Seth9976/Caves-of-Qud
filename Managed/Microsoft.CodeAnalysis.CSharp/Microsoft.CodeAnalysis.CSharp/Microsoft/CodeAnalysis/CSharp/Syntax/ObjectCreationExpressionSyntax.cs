using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002C3 RID: 707
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ObjectCreationExpressionSyntax : BaseObjectCreationExpressionSyntax
	{
		// Token: 0x06004008 RID: 16392 RVA: 0x0010CDCE File Offset: 0x0010AFCE
		internal ObjectCreationExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06004009 RID: 16393 RVA: 0x0010CDD9 File Offset: 0x0010AFD9
		public override SyntaxToken NewKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ObjectCreationExpressionSyntax)base.Green).newKeyword, base.Position, 0);
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x0010CDF8 File Offset: 0x0010AFF8
		public TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 1);
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x0600400B RID: 16395 RVA: 0x0010CE07 File Offset: 0x0010B007
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ArgumentListSyntax ArgumentList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ArgumentListSyntax>(ref this.argumentList, 2);
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x0010CE16 File Offset: 0x0010B016
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override InitializerExpressionSyntax Initializer
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<InitializerExpressionSyntax>(ref this.initializer, 3);
			}
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x0010CE28 File Offset: 0x0010B028
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 1:
				syntaxNode = base.GetRed<TypeSyntax>(ref this.type, 1);
				break;
			case 2:
				syntaxNode = base.GetRed<ArgumentListSyntax>(ref this.argumentList, 2);
				break;
			case 3:
				syntaxNode = base.GetRed<InitializerExpressionSyntax>(ref this.initializer, 3);
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x0010CE80 File Offset: 0x0010B080
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 1:
				syntaxNode = this.type;
				break;
			case 2:
				syntaxNode = this.argumentList;
				break;
			case 3:
				syntaxNode = this.initializer;
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x0010CEC1 File Offset: 0x0010B0C1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitObjectCreationExpression(this);
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x0010CECA File Offset: 0x0010B0CA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitObjectCreationExpression(this);
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x0010CED4 File Offset: 0x0010B0D4
		public ObjectCreationExpressionSyntax Update(SyntaxToken newKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			if (!(newKeyword != this.NewKeyword) && type == this.Type && argumentList == this.ArgumentList && initializer == this.Initializer)
			{
				return this;
			}
			ObjectCreationExpressionSyntax objectCreationExpressionSyntax = SyntaxFactory.ObjectCreationExpression(newKeyword, type, argumentList, initializer);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return objectCreationExpressionSyntax;
			}
			return objectCreationExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x0010CF2F File Offset: 0x0010B12F
		internal override BaseObjectCreationExpressionSyntax WithNewKeywordCore(SyntaxToken newKeyword)
		{
			return this.WithNewKeyword(newKeyword);
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x0010CF38 File Offset: 0x0010B138
		public new ObjectCreationExpressionSyntax WithNewKeyword(SyntaxToken newKeyword)
		{
			return this.Update(newKeyword, this.Type, this.ArgumentList, this.Initializer);
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x0010CF53 File Offset: 0x0010B153
		public ObjectCreationExpressionSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.NewKeyword, type, this.ArgumentList, this.Initializer);
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x0010CF6E File Offset: 0x0010B16E
		internal override BaseObjectCreationExpressionSyntax WithArgumentListCore([global::System.Runtime.CompilerServices.Nullable(2)] ArgumentListSyntax argumentList)
		{
			return this.WithArgumentList(argumentList);
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x0010CF77 File Offset: 0x0010B177
		public new ObjectCreationExpressionSyntax WithArgumentList([global::System.Runtime.CompilerServices.Nullable(2)] ArgumentListSyntax argumentList)
		{
			return this.Update(this.NewKeyword, this.Type, argumentList, this.Initializer);
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x0010CF92 File Offset: 0x0010B192
		internal override BaseObjectCreationExpressionSyntax WithInitializerCore([global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			return this.WithInitializer(initializer);
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x0010CF9B File Offset: 0x0010B19B
		public new ObjectCreationExpressionSyntax WithInitializer([global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			return this.Update(this.NewKeyword, this.Type, this.ArgumentList, initializer);
		}

		// Token: 0x06004019 RID: 16409 RVA: 0x0010CFB6 File Offset: 0x0010B1B6
		internal override BaseObjectCreationExpressionSyntax AddArgumentListArgumentsCore(params ArgumentSyntax[] items)
		{
			return this.AddArgumentListArguments(items);
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x0010CFC0 File Offset: 0x0010B1C0
		public new ObjectCreationExpressionSyntax AddArgumentListArguments(params ArgumentSyntax[] items)
		{
			ArgumentListSyntax argumentListSyntax;
			if ((argumentListSyntax = this.ArgumentList) == null)
			{
				argumentListSyntax = SyntaxFactory.ArgumentList(default(SeparatedSyntaxList<ArgumentSyntax>));
			}
			ArgumentListSyntax argumentListSyntax2 = argumentListSyntax;
			return this.WithArgumentList(argumentListSyntax2.WithArguments(argumentListSyntax2.Arguments.AddRange(items)));
		}

		// Token: 0x04001484 RID: 5252
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x04001485 RID: 5253
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArgumentListSyntax argumentList;

		// Token: 0x04001486 RID: 5254
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private InitializerExpressionSyntax initializer;
	}
}
