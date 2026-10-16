using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002E8 RID: 744
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class TypePatternSyntax : PatternSyntax
	{
		// Token: 0x060041B3 RID: 16819 RVA: 0x0010FE1F File Offset: 0x0010E01F
		internal TypePatternSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x060041B4 RID: 16820 RVA: 0x0010FE2A File Offset: 0x0010E02A
		public TypeSyntax Type
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.type);
			}
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x0010FE38 File Offset: 0x0010E038
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<TypeSyntax>(ref this.type);
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x0010FE4B File Offset: 0x0010E04B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x0010FE58 File Offset: 0x0010E058
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypePattern(this);
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x0010FE61 File Offset: 0x0010E061
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypePattern(this);
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x0010FE6C File Offset: 0x0010E06C
		public TypePatternSyntax Update(TypeSyntax type)
		{
			if (type == this.Type)
			{
				return this;
			}
			TypePatternSyntax typePatternSyntax = SyntaxFactory.TypePattern(type);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return typePatternSyntax;
			}
			return typePatternSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x0010FEA2 File Offset: 0x0010E0A2
		public TypePatternSyntax WithType(TypeSyntax type)
		{
			return this.Update(type);
		}

		// Token: 0x040014B7 RID: 5303
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;
	}
}
