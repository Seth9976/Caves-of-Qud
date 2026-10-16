using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200032A RID: 810
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class TypeCrefSyntax : CrefSyntax
	{
		// Token: 0x06004588 RID: 17800 RVA: 0x00116DB5 File Offset: 0x00114FB5
		internal TypeCrefSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06004589 RID: 17801 RVA: 0x00116DC0 File Offset: 0x00114FC0
		public TypeSyntax Type
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.type);
			}
		}

		// Token: 0x0600458A RID: 17802 RVA: 0x00116DCE File Offset: 0x00114FCE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<TypeSyntax>(ref this.type);
		}

		// Token: 0x0600458B RID: 17803 RVA: 0x00116DE1 File Offset: 0x00114FE1
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x00116DEE File Offset: 0x00114FEE
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypeCref(this);
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x00116DF7 File Offset: 0x00114FF7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypeCref(this);
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x00116E00 File Offset: 0x00115000
		public TypeCrefSyntax Update(TypeSyntax type)
		{
			if (type == this.Type)
			{
				return this;
			}
			TypeCrefSyntax typeCrefSyntax = SyntaxFactory.TypeCref(type);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return typeCrefSyntax;
			}
			return typeCrefSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x00116E36 File Offset: 0x00115036
		public TypeCrefSyntax WithType(TypeSyntax type)
		{
			return this.Update(type);
		}

		// Token: 0x04001510 RID: 5392
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;
	}
}
