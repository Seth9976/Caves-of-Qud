using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003F2 RID: 1010
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ParenthesizedVariableDesignationSyntax : VariableDesignationSyntax
	{
		// Token: 0x0600577F RID: 22399 RVA: 0x00157F60 File Offset: 0x00156160
		internal ParenthesizedVariableDesignationSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode variables, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (variables != null)
			{
				base.AdjustFlagsAndWidth(variables);
				this.variables = variables;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x00157FB0 File Offset: 0x001561B0
		internal ParenthesizedVariableDesignationSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode variables, SyntaxToken closeParenToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (variables != null)
			{
				base.AdjustFlagsAndWidth(variables);
				this.variables = variables;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x00158002 File Offset: 0x00156202
		internal ParenthesizedVariableDesignationSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode variables, SyntaxToken closeParenToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (variables != null)
			{
				base.AdjustFlagsAndWidth(variables);
				this.variables = variables;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x06005782 RID: 22402 RVA: 0x00158041 File Offset: 0x00156241
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x06005783 RID: 22403 RVA: 0x00158049 File Offset: 0x00156249
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<VariableDesignationSyntax> Variables
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<VariableDesignationSyntax>(new SyntaxList<CSharpSyntaxNode>(this.variables));
			}
		}

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x06005784 RID: 22404 RVA: 0x00158060 File Offset: 0x00156260
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x00158068 File Offset: 0x00156268
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.openParenToken;
				break;
			case 1:
				greenNode = this.variables;
				break;
			case 2:
				greenNode = this.closeParenToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005786 RID: 22406 RVA: 0x001580A7 File Offset: 0x001562A7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ParenthesizedVariableDesignationSyntax(this, parent, position);
		}

		// Token: 0x06005787 RID: 22407 RVA: 0x001580B1 File Offset: 0x001562B1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitParenthesizedVariableDesignation(this);
		}

		// Token: 0x06005788 RID: 22408 RVA: 0x001580BA File Offset: 0x001562BA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitParenthesizedVariableDesignation(this);
		}

		// Token: 0x06005789 RID: 22409 RVA: 0x001580C4 File Offset: 0x001562C4
		public ParenthesizedVariableDesignationSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<VariableDesignationSyntax> variables, SyntaxToken closeParenToken)
		{
			if (openParenToken == this.OpenParenToken)
			{
				SeparatedSyntaxList<VariableDesignationSyntax> separatedSyntaxList = this.Variables;
				if (!((in variables) != (in separatedSyntaxList)) && closeParenToken == this.CloseParenToken)
				{
					return this;
				}
			}
			ParenthesizedVariableDesignationSyntax parenthesizedVariableDesignationSyntax = SyntaxFactory.ParenthesizedVariableDesignation(openParenToken, variables, closeParenToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				parenthesizedVariableDesignationSyntax = parenthesizedVariableDesignationSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				parenthesizedVariableDesignationSyntax = parenthesizedVariableDesignationSyntax.WithAnnotationsGreen(annotations);
			}
			return parenthesizedVariableDesignationSyntax;
		}

		// Token: 0x0600578A RID: 22410 RVA: 0x0015812D File Offset: 0x0015632D
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ParenthesizedVariableDesignationSyntax(base.Kind, this.openParenToken, this.variables, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600578B RID: 22411 RVA: 0x00158153 File Offset: 0x00156353
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ParenthesizedVariableDesignationSyntax(base.Kind, this.openParenToken, this.variables, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600578C RID: 22412 RVA: 0x0015817C File Offset: 0x0015637C
		internal ParenthesizedVariableDesignationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openParenToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.variables = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeParenToken = syntaxToken2;
		}

		// Token: 0x0600578D RID: 22413 RVA: 0x001581E8 File Offset: 0x001563E8
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.variables);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x0600578E RID: 22414 RVA: 0x00158215 File Offset: 0x00156415
		static ParenthesizedVariableDesignationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ParenthesizedVariableDesignationSyntax), (ObjectReader r) => new ParenthesizedVariableDesignationSyntax(r));
		}

		// Token: 0x04001710 RID: 5904
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001711 RID: 5905
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode variables;

		// Token: 0x04001712 RID: 5906
		internal readonly SyntaxToken closeParenToken;
	}
}
