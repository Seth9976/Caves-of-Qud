using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003AE RID: 942
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DeclarationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060053A6 RID: 21414 RVA: 0x0014D9CD File Offset: 0x0014BBCD
		internal DeclarationExpressionSyntax(SyntaxKind kind, TypeSyntax type, VariableDesignationSyntax designation, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(designation);
			this.designation = designation;
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x0014D9FD File Offset: 0x0014BBFD
		internal DeclarationExpressionSyntax(SyntaxKind kind, TypeSyntax type, VariableDesignationSyntax designation, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(designation);
			this.designation = designation;
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x0014DA31 File Offset: 0x0014BC31
		internal DeclarationExpressionSyntax(SyntaxKind kind, TypeSyntax type, VariableDesignationSyntax designation)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(designation);
			this.designation = designation;
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x060053A9 RID: 21417 RVA: 0x0014DA5D File Offset: 0x0014BC5D
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x060053AA RID: 21418 RVA: 0x0014DA65 File Offset: 0x0014BC65
		public VariableDesignationSyntax Designation
		{
			get
			{
				return this.designation;
			}
		}

		// Token: 0x060053AB RID: 21419 RVA: 0x0014DA70 File Offset: 0x0014BC70
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

		// Token: 0x060053AC RID: 21420 RVA: 0x0014DA9B File Offset: 0x0014BC9B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new DeclarationExpressionSyntax(this, parent, position);
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x0014DAA5 File Offset: 0x0014BCA5
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDeclarationExpression(this);
		}

		// Token: 0x060053AE RID: 21422 RVA: 0x0014DAAE File Offset: 0x0014BCAE
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDeclarationExpression(this);
		}

		// Token: 0x060053AF RID: 21423 RVA: 0x0014DAB8 File Offset: 0x0014BCB8
		public DeclarationExpressionSyntax Update(TypeSyntax type, VariableDesignationSyntax designation)
		{
			if (type != this.Type || designation != this.Designation)
			{
				DeclarationExpressionSyntax declarationExpressionSyntax = SyntaxFactory.DeclarationExpression(type, designation);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					declarationExpressionSyntax = declarationExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					declarationExpressionSyntax = declarationExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return declarationExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x0014DB0E File Offset: 0x0014BD0E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new DeclarationExpressionSyntax(base.Kind, this.type, this.designation, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x0014DB2E File Offset: 0x0014BD2E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new DeclarationExpressionSyntax(base.Kind, this.type, this.designation, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x0014DB50 File Offset: 0x0014BD50
		internal DeclarationExpressionSyntax(ObjectReader reader)
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

		// Token: 0x060053B3 RID: 21427 RVA: 0x0014DB9F File Offset: 0x0014BD9F
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.type);
			writer.WriteValue(this.designation);
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x0014DBC0 File Offset: 0x0014BDC0
		static DeclarationExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(DeclarationExpressionSyntax), (ObjectReader r) => new DeclarationExpressionSyntax(r));
		}

		// Token: 0x0400165A RID: 5722
		internal readonly TypeSyntax type;

		// Token: 0x0400165B RID: 5723
		internal readonly VariableDesignationSyntax designation;
	}
}
