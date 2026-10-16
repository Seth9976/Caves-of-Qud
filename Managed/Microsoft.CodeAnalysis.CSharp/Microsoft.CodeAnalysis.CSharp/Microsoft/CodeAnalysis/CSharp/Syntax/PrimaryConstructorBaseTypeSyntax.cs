using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000315 RID: 789
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class PrimaryConstructorBaseTypeSyntax : BaseTypeSyntax
	{
		// Token: 0x0600444C RID: 17484 RVA: 0x00114A10 File Offset: 0x00112C10
		internal PrimaryConstructorBaseTypeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x0600444D RID: 17485 RVA: 0x00114A1B File Offset: 0x00112C1B
		public override TypeSyntax Type
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.type);
			}
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x0600444E RID: 17486 RVA: 0x00114A29 File Offset: 0x00112C29
		public ArgumentListSyntax ArgumentList
		{
			get
			{
				return base.GetRed<ArgumentListSyntax>(ref this.argumentList, 1);
			}
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x00114A38 File Offset: 0x00112C38
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
					syntaxNode = base.GetRed<ArgumentListSyntax>(ref this.argumentList, 1);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<TypeSyntax>(ref this.type);
			}
			return syntaxNode;
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x00114A70 File Offset: 0x00112C70
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
					syntaxNode = this.argumentList;
				}
			}
			else
			{
				syntaxNode = this.type;
			}
			return syntaxNode;
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x00114A9B File Offset: 0x00112C9B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPrimaryConstructorBaseType(this);
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x00114AA4 File Offset: 0x00112CA4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPrimaryConstructorBaseType(this);
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x00114AB0 File Offset: 0x00112CB0
		public PrimaryConstructorBaseTypeSyntax Update(TypeSyntax type, ArgumentListSyntax argumentList)
		{
			if (type == this.Type && argumentList == this.ArgumentList)
			{
				return this;
			}
			PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeSyntax = SyntaxFactory.PrimaryConstructorBaseType(type, argumentList);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return primaryConstructorBaseTypeSyntax;
			}
			return primaryConstructorBaseTypeSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x00114AF0 File Offset: 0x00112CF0
		internal override BaseTypeSyntax WithTypeCore(TypeSyntax type)
		{
			return this.WithType(type);
		}

		// Token: 0x06004455 RID: 17493 RVA: 0x00114AF9 File Offset: 0x00112CF9
		public new PrimaryConstructorBaseTypeSyntax WithType(TypeSyntax type)
		{
			return this.Update(type, this.ArgumentList);
		}

		// Token: 0x06004456 RID: 17494 RVA: 0x00114B08 File Offset: 0x00112D08
		public PrimaryConstructorBaseTypeSyntax WithArgumentList(ArgumentListSyntax argumentList)
		{
			return this.Update(this.Type, argumentList);
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x00114B18 File Offset: 0x00112D18
		public PrimaryConstructorBaseTypeSyntax AddArgumentListArguments(params ArgumentSyntax[] items)
		{
			return this.WithArgumentList(this.ArgumentList.WithArguments(this.ArgumentList.Arguments.AddRange(items)));
		}

		// Token: 0x040014F3 RID: 5363
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x040014F4 RID: 5364
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArgumentListSyntax argumentList;
	}
}
