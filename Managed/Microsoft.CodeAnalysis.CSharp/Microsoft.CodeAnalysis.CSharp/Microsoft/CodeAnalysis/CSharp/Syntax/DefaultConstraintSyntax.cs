using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200031A RID: 794
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class DefaultConstraintSyntax : TypeParameterConstraintSyntax
	{
		// Token: 0x0600447C RID: 17532 RVA: 0x00114F13 File Offset: 0x00113113
		internal DefaultConstraintSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x0600447D RID: 17533 RVA: 0x00114F1E File Offset: 0x0011311E
		public SyntaxToken DefaultKeyword
		{
			get
			{
				return new SyntaxToken(this, ((DefaultConstraintSyntax)base.Green).defaultKeyword, base.Position, 0);
			}
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x00114F3D File Offset: 0x0011313D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x00114F40 File Offset: 0x00113140
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x00114F43 File Offset: 0x00113143
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDefaultConstraint(this);
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x00114F4C File Offset: 0x0011314C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDefaultConstraint(this);
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x00114F58 File Offset: 0x00113158
		public DefaultConstraintSyntax Update(SyntaxToken defaultKeyword)
		{
			if (!(defaultKeyword != this.DefaultKeyword))
			{
				return this;
			}
			DefaultConstraintSyntax defaultConstraintSyntax = SyntaxFactory.DefaultConstraint(defaultKeyword);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return defaultConstraintSyntax;
			}
			return defaultConstraintSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x00114F93 File Offset: 0x00113193
		public DefaultConstraintSyntax WithDefaultKeyword(SyntaxToken defaultKeyword)
		{
			return this.Update(defaultKeyword);
		}
	}
}
