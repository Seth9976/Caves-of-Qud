using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002E1 RID: 737
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class RecursivePatternSyntax : PatternSyntax
	{
		// Token: 0x06004161 RID: 16737 RVA: 0x0010F553 File Offset: 0x0010D753
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal RecursivePatternSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x06004162 RID: 16738 RVA: 0x0010F55E File Offset: 0x0010D75E
		public TypeSyntax Type
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.type);
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x06004163 RID: 16739 RVA: 0x0010F56C File Offset: 0x0010D76C
		public PositionalPatternClauseSyntax PositionalPatternClause
		{
			get
			{
				return base.GetRed<PositionalPatternClauseSyntax>(ref this.positionalPatternClause, 1);
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x06004164 RID: 16740 RVA: 0x0010F57B File Offset: 0x0010D77B
		public PropertyPatternClauseSyntax PropertyPatternClause
		{
			get
			{
				return base.GetRed<PropertyPatternClauseSyntax>(ref this.propertyPatternClause, 2);
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x0010F58A File Offset: 0x0010D78A
		public VariableDesignationSyntax Designation
		{
			get
			{
				return base.GetRed<VariableDesignationSyntax>(ref this.designation, 3);
			}
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x0010F59C File Offset: 0x0010D79C
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 0:
				syntaxNode = base.GetRedAtZero<TypeSyntax>(ref this.type);
				break;
			case 1:
				syntaxNode = base.GetRed<PositionalPatternClauseSyntax>(ref this.positionalPatternClause, 1);
				break;
			case 2:
				syntaxNode = base.GetRed<PropertyPatternClauseSyntax>(ref this.propertyPatternClause, 2);
				break;
			case 3:
				syntaxNode = base.GetRed<VariableDesignationSyntax>(ref this.designation, 3);
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x0010F604 File Offset: 0x0010D804
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 0:
				syntaxNode = this.type;
				break;
			case 1:
				syntaxNode = this.positionalPatternClause;
				break;
			case 2:
				syntaxNode = this.propertyPatternClause;
				break;
			case 3:
				syntaxNode = this.designation;
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x0010F650 File Offset: 0x0010D850
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRecursivePattern(this);
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x0010F659 File Offset: 0x0010D859
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRecursivePattern(this);
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x0010F664 File Offset: 0x0010D864
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public RecursivePatternSyntax Update(TypeSyntax type, PositionalPatternClauseSyntax positionalPatternClause, PropertyPatternClauseSyntax propertyPatternClause, VariableDesignationSyntax designation)
		{
			if (type == this.Type && positionalPatternClause == this.PositionalPatternClause && propertyPatternClause == this.PropertyPatternClause && designation == this.Designation)
			{
				return this;
			}
			RecursivePatternSyntax recursivePatternSyntax = SyntaxFactory.RecursivePattern(type, positionalPatternClause, propertyPatternClause, designation);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return recursivePatternSyntax;
			}
			return recursivePatternSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x0010F6BA File Offset: 0x0010D8BA
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public RecursivePatternSyntax WithType([global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type)
		{
			return this.Update(type, this.PositionalPatternClause, this.PropertyPatternClause, this.Designation);
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x0010F6D5 File Offset: 0x0010D8D5
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public RecursivePatternSyntax WithPositionalPatternClause([global::System.Runtime.CompilerServices.Nullable(2)] PositionalPatternClauseSyntax positionalPatternClause)
		{
			return this.Update(this.Type, positionalPatternClause, this.PropertyPatternClause, this.Designation);
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x0010F6F0 File Offset: 0x0010D8F0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public RecursivePatternSyntax WithPropertyPatternClause([global::System.Runtime.CompilerServices.Nullable(2)] PropertyPatternClauseSyntax propertyPatternClause)
		{
			return this.Update(this.Type, this.PositionalPatternClause, propertyPatternClause, this.Designation);
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x0010F70B File Offset: 0x0010D90B
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public RecursivePatternSyntax WithDesignation([global::System.Runtime.CompilerServices.Nullable(2)] VariableDesignationSyntax designation)
		{
			return this.Update(this.Type, this.PositionalPatternClause, this.PropertyPatternClause, designation);
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x0010F728 File Offset: 0x0010D928
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public RecursivePatternSyntax AddPositionalPatternClauseSubpatterns(params SubpatternSyntax[] items)
		{
			PositionalPatternClauseSyntax positionalPatternClauseSyntax;
			if ((positionalPatternClauseSyntax = this.PositionalPatternClause) == null)
			{
				positionalPatternClauseSyntax = SyntaxFactory.PositionalPatternClause(default(SeparatedSyntaxList<SubpatternSyntax>));
			}
			PositionalPatternClauseSyntax positionalPatternClauseSyntax2 = positionalPatternClauseSyntax;
			return this.WithPositionalPatternClause(positionalPatternClauseSyntax2.WithSubpatterns(positionalPatternClauseSyntax2.Subpatterns.AddRange(items)));
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x0010F76C File Offset: 0x0010D96C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public RecursivePatternSyntax AddPropertyPatternClauseSubpatterns(params SubpatternSyntax[] items)
		{
			PropertyPatternClauseSyntax propertyPatternClauseSyntax;
			if ((propertyPatternClauseSyntax = this.PropertyPatternClause) == null)
			{
				propertyPatternClauseSyntax = SyntaxFactory.PropertyPatternClause(default(SeparatedSyntaxList<SubpatternSyntax>));
			}
			PropertyPatternClauseSyntax propertyPatternClauseSyntax2 = propertyPatternClauseSyntax;
			return this.WithPropertyPatternClause(propertyPatternClauseSyntax2.WithSubpatterns(propertyPatternClauseSyntax2.Subpatterns.AddRange(items)));
		}

		// Token: 0x040014AC RID: 5292
		private TypeSyntax type;

		// Token: 0x040014AD RID: 5293
		private PositionalPatternClauseSyntax positionalPatternClause;

		// Token: 0x040014AE RID: 5294
		private PropertyPatternClauseSyntax propertyPatternClause;

		// Token: 0x040014AF RID: 5295
		private VariableDesignationSyntax designation;
	}
}
