using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003D7 RID: 983
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class VarPatternSyntax : PatternSyntax
	{
		// Token: 0x060055F9 RID: 22009 RVA: 0x00153F35 File Offset: 0x00152135
		internal VarPatternSyntax(SyntaxKind kind, SyntaxToken varKeyword, VariableDesignationSyntax designation, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(varKeyword);
			this.varKeyword = varKeyword;
			base.AdjustFlagsAndWidth(designation);
			this.designation = designation;
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x00153F65 File Offset: 0x00152165
		internal VarPatternSyntax(SyntaxKind kind, SyntaxToken varKeyword, VariableDesignationSyntax designation, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(varKeyword);
			this.varKeyword = varKeyword;
			base.AdjustFlagsAndWidth(designation);
			this.designation = designation;
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x00153F99 File Offset: 0x00152199
		internal VarPatternSyntax(SyntaxKind kind, SyntaxToken varKeyword, VariableDesignationSyntax designation)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(varKeyword);
			this.varKeyword = varKeyword;
			base.AdjustFlagsAndWidth(designation);
			this.designation = designation;
		}

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x060055FC RID: 22012 RVA: 0x00153FC5 File Offset: 0x001521C5
		public SyntaxToken VarKeyword
		{
			get
			{
				return this.varKeyword;
			}
		}

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x060055FD RID: 22013 RVA: 0x00153FCD File Offset: 0x001521CD
		public VariableDesignationSyntax Designation
		{
			get
			{
				return this.designation;
			}
		}

		// Token: 0x060055FE RID: 22014 RVA: 0x00153FD8 File Offset: 0x001521D8
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
				greenNode = this.varKeyword;
			}
			return greenNode;
		}

		// Token: 0x060055FF RID: 22015 RVA: 0x00154003 File Offset: 0x00152203
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new VarPatternSyntax(this, parent, position);
		}

		// Token: 0x06005600 RID: 22016 RVA: 0x0015400D File Offset: 0x0015220D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitVarPattern(this);
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x00154016 File Offset: 0x00152216
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitVarPattern(this);
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x00154020 File Offset: 0x00152220
		public VarPatternSyntax Update(SyntaxToken varKeyword, VariableDesignationSyntax designation)
		{
			if (varKeyword != this.VarKeyword || designation != this.Designation)
			{
				VarPatternSyntax varPatternSyntax = SyntaxFactory.VarPattern(varKeyword, designation);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					varPatternSyntax = varPatternSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					varPatternSyntax = varPatternSyntax.WithAnnotationsGreen(annotations);
				}
				return varPatternSyntax;
			}
			return this;
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x00154076 File Offset: 0x00152276
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new VarPatternSyntax(base.Kind, this.varKeyword, this.designation, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005604 RID: 22020 RVA: 0x00154096 File Offset: 0x00152296
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new VarPatternSyntax(base.Kind, this.varKeyword, this.designation, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x001540B8 File Offset: 0x001522B8
		internal VarPatternSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.varKeyword = syntaxToken;
			VariableDesignationSyntax variableDesignationSyntax = (VariableDesignationSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(variableDesignationSyntax);
			this.designation = variableDesignationSyntax;
		}

		// Token: 0x06005606 RID: 22022 RVA: 0x00154107 File Offset: 0x00152307
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.varKeyword);
			writer.WriteValue(this.designation);
		}

		// Token: 0x06005607 RID: 22023 RVA: 0x00154128 File Offset: 0x00152328
		static VarPatternSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(VarPatternSyntax), (ObjectReader r) => new VarPatternSyntax(r));
		}

		// Token: 0x040016CC RID: 5836
		internal readonly SyntaxToken varKeyword;

		// Token: 0x040016CD RID: 5837
		internal readonly VariableDesignationSyntax designation;
	}
}
