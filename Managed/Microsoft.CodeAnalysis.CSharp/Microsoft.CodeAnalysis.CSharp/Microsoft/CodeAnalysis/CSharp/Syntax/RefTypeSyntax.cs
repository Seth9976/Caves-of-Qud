using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000279 RID: 633
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class RefTypeSyntax : TypeSyntax
	{
		// Token: 0x06003C55 RID: 15445 RVA: 0x00105286 File Offset: 0x00103486
		public RefTypeSyntax Update(SyntaxToken refKeyword, TypeSyntax type)
		{
			return this.Update(refKeyword, this.ReadOnlyKeyword, type);
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x00105296 File Offset: 0x00103496
		internal RefTypeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06003C57 RID: 15447 RVA: 0x001052A1 File Offset: 0x001034A1
		public SyntaxToken RefKeyword
		{
			get
			{
				return new SyntaxToken(this, ((RefTypeSyntax)base.Green).refKeyword, base.Position, 0);
			}
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x001052C0 File Offset: 0x001034C0
		public SyntaxToken ReadOnlyKeyword
		{
			get
			{
				SyntaxToken readOnlyKeyword = ((RefTypeSyntax)base.Green).readOnlyKeyword;
				if (readOnlyKeyword == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, readOnlyKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x00105300 File Offset: 0x00103500
		public TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 2);
			}
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x0010530F File Offset: 0x0010350F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return base.GetRed<TypeSyntax>(ref this.type, 2);
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x00105324 File Offset: 0x00103524
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x00105332 File Offset: 0x00103532
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRefType(this);
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x0010533B File Offset: 0x0010353B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRefType(this);
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x00105344 File Offset: 0x00103544
		public RefTypeSyntax Update(SyntaxToken refKeyword, SyntaxToken readOnlyKeyword, TypeSyntax type)
		{
			if (!(refKeyword != this.RefKeyword) && !(readOnlyKeyword != this.ReadOnlyKeyword) && type == this.Type)
			{
				return this;
			}
			RefTypeSyntax refTypeSyntax = SyntaxFactory.RefType(refKeyword, readOnlyKeyword, type);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return refTypeSyntax;
			}
			return refTypeSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x00105398 File Offset: 0x00103598
		public RefTypeSyntax WithRefKeyword(SyntaxToken refKeyword)
		{
			return this.Update(refKeyword, this.ReadOnlyKeyword, this.Type);
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x001053AD File Offset: 0x001035AD
		public RefTypeSyntax WithReadOnlyKeyword(SyntaxToken readOnlyKeyword)
		{
			return this.Update(this.RefKeyword, readOnlyKeyword, this.Type);
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x001053C2 File Offset: 0x001035C2
		public RefTypeSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.RefKeyword, this.ReadOnlyKeyword, type);
		}

		// Token: 0x0400141B RID: 5147
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;
	}
}
