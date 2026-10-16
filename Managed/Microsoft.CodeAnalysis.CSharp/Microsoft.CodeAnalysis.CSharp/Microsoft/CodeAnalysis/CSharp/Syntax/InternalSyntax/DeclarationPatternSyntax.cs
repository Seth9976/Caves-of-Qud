using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003D6 RID: 982
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DeclarationPatternSyntax : PatternSyntax
	{
		// Token: 0x060055EA RID: 21994 RVA: 0x00153D23 File Offset: 0x00151F23
		internal DeclarationPatternSyntax(SyntaxKind kind, TypeSyntax type, VariableDesignationSyntax designation, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(designation);
			this.designation = designation;
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x00153D53 File Offset: 0x00151F53
		internal DeclarationPatternSyntax(SyntaxKind kind, TypeSyntax type, VariableDesignationSyntax designation, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(designation);
			this.designation = designation;
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x00153D87 File Offset: 0x00151F87
		internal DeclarationPatternSyntax(SyntaxKind kind, TypeSyntax type, VariableDesignationSyntax designation)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(designation);
			this.designation = designation;
		}

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x060055ED RID: 21997 RVA: 0x00153DB3 File Offset: 0x00151FB3
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x060055EE RID: 21998 RVA: 0x00153DBB File Offset: 0x00151FBB
		public VariableDesignationSyntax Designation
		{
			get
			{
				return this.designation;
			}
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x00153DC4 File Offset: 0x00151FC4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			if (index != 0)
			{
				if (index != 1)
				{
					greenNode = null;
				}
				else
				{
					greenNode = this.designation;
				}
			}
			else
			{
				greenNode = this.type;
			}
			return greenNode;
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x00153DEF File Offset: 0x00151FEF
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new DeclarationPatternSyntax(this, parent, position);
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x00153DF9 File Offset: 0x00151FF9
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDeclarationPattern(this);
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x00153E02 File Offset: 0x00152002
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDeclarationPattern(this);
		}

		// Token: 0x060055F3 RID: 22003 RVA: 0x00153E0C File Offset: 0x0015200C
		public DeclarationPatternSyntax Update(TypeSyntax type, VariableDesignationSyntax designation)
		{
			if (type != this.Type || designation != this.Designation)
			{
				DeclarationPatternSyntax declarationPatternSyntax = SyntaxFactory.DeclarationPattern(type, designation);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					declarationPatternSyntax = declarationPatternSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					declarationPatternSyntax = declarationPatternSyntax.WithAnnotationsGreen(annotations);
				}
				return declarationPatternSyntax;
			}
			return this;
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x00153E62 File Offset: 0x00152062
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new DeclarationPatternSyntax(base.Kind, this.type, this.designation, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x00153E82 File Offset: 0x00152082
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new DeclarationPatternSyntax(base.Kind, this.type, this.designation, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060055F6 RID: 22006 RVA: 0x00153EA4 File Offset: 0x001520A4
		internal DeclarationPatternSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
			VariableDesignationSyntax variableDesignationSyntax = (VariableDesignationSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(variableDesignationSyntax);
			this.designation = variableDesignationSyntax;
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x00153EF3 File Offset: 0x001520F3
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.type);
			writer.WriteValue(this.designation);
		}

		// Token: 0x060055F8 RID: 22008 RVA: 0x00153F14 File Offset: 0x00152114
		static DeclarationPatternSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(DeclarationPatternSyntax), (ObjectReader r) => new DeclarationPatternSyntax(r));
		}

		// Token: 0x040016CA RID: 5834
		internal readonly TypeSyntax type;

		// Token: 0x040016CB RID: 5835
		internal readonly VariableDesignationSyntax designation;
	}
}
