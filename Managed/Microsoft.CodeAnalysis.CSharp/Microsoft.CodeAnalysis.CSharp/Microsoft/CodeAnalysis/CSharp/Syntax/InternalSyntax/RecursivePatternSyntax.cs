using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003D8 RID: 984
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RecursivePatternSyntax : PatternSyntax
	{
		// Token: 0x06005608 RID: 22024 RVA: 0x0015414C File Offset: 0x0015234C
		internal RecursivePatternSyntax(SyntaxKind kind, TypeSyntax type, PositionalPatternClauseSyntax positionalPatternClause, PropertyPatternClauseSyntax propertyPatternClause, VariableDesignationSyntax designation, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
			if (positionalPatternClause != null)
			{
				base.AdjustFlagsAndWidth(positionalPatternClause);
				this.positionalPatternClause = positionalPatternClause;
			}
			if (propertyPatternClause != null)
			{
				base.AdjustFlagsAndWidth(propertyPatternClause);
				this.propertyPatternClause = propertyPatternClause;
			}
			if (designation != null)
			{
				base.AdjustFlagsAndWidth(designation);
				this.designation = designation;
			}
		}

		// Token: 0x06005609 RID: 22025 RVA: 0x001541B8 File Offset: 0x001523B8
		internal RecursivePatternSyntax(SyntaxKind kind, TypeSyntax type, PositionalPatternClauseSyntax positionalPatternClause, PropertyPatternClauseSyntax propertyPatternClause, VariableDesignationSyntax designation, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
			if (positionalPatternClause != null)
			{
				base.AdjustFlagsAndWidth(positionalPatternClause);
				this.positionalPatternClause = positionalPatternClause;
			}
			if (propertyPatternClause != null)
			{
				base.AdjustFlagsAndWidth(propertyPatternClause);
				this.propertyPatternClause = propertyPatternClause;
			}
			if (designation != null)
			{
				base.AdjustFlagsAndWidth(designation);
				this.designation = designation;
			}
		}

		// Token: 0x0600560A RID: 22026 RVA: 0x00154228 File Offset: 0x00152428
		internal RecursivePatternSyntax(SyntaxKind kind, TypeSyntax type, PositionalPatternClauseSyntax positionalPatternClause, PropertyPatternClauseSyntax propertyPatternClause, VariableDesignationSyntax designation)
			: base(kind)
		{
			base.SlotCount = 4;
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
			if (positionalPatternClause != null)
			{
				base.AdjustFlagsAndWidth(positionalPatternClause);
				this.positionalPatternClause = positionalPatternClause;
			}
			if (propertyPatternClause != null)
			{
				base.AdjustFlagsAndWidth(propertyPatternClause);
				this.propertyPatternClause = propertyPatternClause;
			}
			if (designation != null)
			{
				base.AdjustFlagsAndWidth(designation);
				this.designation = designation;
			}
		}

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x0600560B RID: 22027 RVA: 0x0015428D File Offset: 0x0015248D
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x0600560C RID: 22028 RVA: 0x00154295 File Offset: 0x00152495
		public PositionalPatternClauseSyntax PositionalPatternClause
		{
			get
			{
				return this.positionalPatternClause;
			}
		}

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x0600560D RID: 22029 RVA: 0x0015429D File Offset: 0x0015249D
		public PropertyPatternClauseSyntax PropertyPatternClause
		{
			get
			{
				return this.propertyPatternClause;
			}
		}

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x0600560E RID: 22030 RVA: 0x001542A5 File Offset: 0x001524A5
		public VariableDesignationSyntax Designation
		{
			get
			{
				return this.designation;
			}
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x001542B0 File Offset: 0x001524B0
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.type;
				break;
			case 1:
				greenNode = this.positionalPatternClause;
				break;
			case 2:
				greenNode = this.propertyPatternClause;
				break;
			case 3:
				greenNode = this.designation;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005610 RID: 22032 RVA: 0x001542FC File Offset: 0x001524FC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new RecursivePatternSyntax(this, parent, position);
		}

		// Token: 0x06005611 RID: 22033 RVA: 0x00154306 File Offset: 0x00152506
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRecursivePattern(this);
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x0015430F File Offset: 0x0015250F
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRecursivePattern(this);
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x00154318 File Offset: 0x00152518
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public RecursivePatternSyntax Update(TypeSyntax type, PositionalPatternClauseSyntax positionalPatternClause, PropertyPatternClauseSyntax propertyPatternClause, VariableDesignationSyntax designation)
		{
			if (type != this.Type || positionalPatternClause != this.PositionalPatternClause || propertyPatternClause != this.PropertyPatternClause || designation != this.Designation)
			{
				RecursivePatternSyntax recursivePatternSyntax = SyntaxFactory.RecursivePattern(type, positionalPatternClause, propertyPatternClause, designation);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					recursivePatternSyntax = recursivePatternSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					recursivePatternSyntax = recursivePatternSyntax.WithAnnotationsGreen(annotations);
				}
				return recursivePatternSyntax;
			}
			return this;
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x00154384 File Offset: 0x00152584
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new RecursivePatternSyntax(base.Kind, this.type, this.positionalPatternClause, this.propertyPatternClause, this.designation, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x001543B0 File Offset: 0x001525B0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new RecursivePatternSyntax(base.Kind, this.type, this.positionalPatternClause, this.propertyPatternClause, this.designation, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005616 RID: 22038 RVA: 0x001543DC File Offset: 0x001525DC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal RecursivePatternSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			if (typeSyntax != null)
			{
				base.AdjustFlagsAndWidth(typeSyntax);
				this.type = typeSyntax;
			}
			PositionalPatternClauseSyntax positionalPatternClauseSyntax = (PositionalPatternClauseSyntax)reader.ReadValue();
			if (positionalPatternClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(positionalPatternClauseSyntax);
				this.positionalPatternClause = positionalPatternClauseSyntax;
			}
			PropertyPatternClauseSyntax propertyPatternClauseSyntax = (PropertyPatternClauseSyntax)reader.ReadValue();
			if (propertyPatternClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(propertyPatternClauseSyntax);
				this.propertyPatternClause = propertyPatternClauseSyntax;
			}
			VariableDesignationSyntax variableDesignationSyntax = (VariableDesignationSyntax)reader.ReadValue();
			if (variableDesignationSyntax != null)
			{
				base.AdjustFlagsAndWidth(variableDesignationSyntax);
				this.designation = variableDesignationSyntax;
			}
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x0015446B File Offset: 0x0015266B
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.type);
			writer.WriteValue(this.positionalPatternClause);
			writer.WriteValue(this.propertyPatternClause);
			writer.WriteValue(this.designation);
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x001544A4 File Offset: 0x001526A4
		static RecursivePatternSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(RecursivePatternSyntax), (ObjectReader r) => new RecursivePatternSyntax(r));
		}

		// Token: 0x040016CE RID: 5838
		internal readonly TypeSyntax type;

		// Token: 0x040016CF RID: 5839
		internal readonly PositionalPatternClauseSyntax positionalPatternClause;

		// Token: 0x040016D0 RID: 5840
		internal readonly PropertyPatternClauseSyntax propertyPatternClause;

		// Token: 0x040016D1 RID: 5841
		internal readonly VariableDesignationSyntax designation;
	}
}
