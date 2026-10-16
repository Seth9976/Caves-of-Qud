using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000319 RID: 793
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class TypeConstraintSyntax : TypeParameterConstraintSyntax
	{
		// Token: 0x06004474 RID: 17524 RVA: 0x00114E88 File Offset: 0x00113088
		internal TypeConstraintSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06004475 RID: 17525 RVA: 0x00114E93 File Offset: 0x00113093
		public TypeSyntax Type
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.type);
			}
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x00114EA1 File Offset: 0x001130A1
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<TypeSyntax>(ref this.type);
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x00114EB4 File Offset: 0x001130B4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x00114EC1 File Offset: 0x001130C1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypeConstraint(this);
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x00114ECA File Offset: 0x001130CA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypeConstraint(this);
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x00114ED4 File Offset: 0x001130D4
		public TypeConstraintSyntax Update(TypeSyntax type)
		{
			if (type == this.Type)
			{
				return this;
			}
			TypeConstraintSyntax typeConstraintSyntax = SyntaxFactory.TypeConstraint(type);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return typeConstraintSyntax;
			}
			return typeConstraintSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x00114F0A File Offset: 0x0011310A
		public TypeConstraintSyntax WithType(TypeSyntax type)
		{
			return this.Update(type);
		}

		// Token: 0x040014F7 RID: 5367
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;
	}
}
