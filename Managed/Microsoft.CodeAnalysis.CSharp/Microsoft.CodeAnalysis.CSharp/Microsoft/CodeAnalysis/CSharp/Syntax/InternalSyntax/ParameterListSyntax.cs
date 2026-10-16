using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000449 RID: 1097
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ParameterListSyntax : BaseParameterListSyntax
	{
		// Token: 0x06005D20 RID: 23840 RVA: 0x0016B50C File Offset: 0x0016970C
		internal ParameterListSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005D21 RID: 23841 RVA: 0x0016B55C File Offset: 0x0016975C
		internal ParameterListSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken closeParenToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x0016B5AE File Offset: 0x001697AE
		internal ParameterListSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken closeParenToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x06005D23 RID: 23843 RVA: 0x0016B5ED File Offset: 0x001697ED
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x06005D24 RID: 23844 RVA: 0x0016B5F5 File Offset: 0x001697F5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SeparatedSyntaxList<ParameterSyntax> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<ParameterSyntax>(new SyntaxList<CSharpSyntaxNode>(this.parameters));
			}
		}

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x06005D25 RID: 23845 RVA: 0x0016B60C File Offset: 0x0016980C
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x06005D26 RID: 23846 RVA: 0x0016B614 File Offset: 0x00169814
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
				greenNode = this.parameters;
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

		// Token: 0x06005D27 RID: 23847 RVA: 0x0016B653 File Offset: 0x00169853
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ParameterListSyntax(this, parent, position);
		}

		// Token: 0x06005D28 RID: 23848 RVA: 0x0016B65D File Offset: 0x0016985D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitParameterList(this);
		}

		// Token: 0x06005D29 RID: 23849 RVA: 0x0016B666 File Offset: 0x00169866
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitParameterList(this);
		}

		// Token: 0x06005D2A RID: 23850 RVA: 0x0016B670 File Offset: 0x00169870
		public ParameterListSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ParameterSyntax> parameters, SyntaxToken closeParenToken)
		{
			if (openParenToken == this.OpenParenToken)
			{
				SeparatedSyntaxList<ParameterSyntax> separatedSyntaxList = this.Parameters;
				if (!((in parameters) != (in separatedSyntaxList)) && closeParenToken == this.CloseParenToken)
				{
					return this;
				}
			}
			ParameterListSyntax parameterListSyntax = SyntaxFactory.ParameterList(openParenToken, parameters, closeParenToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				parameterListSyntax = parameterListSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				parameterListSyntax = parameterListSyntax.WithAnnotationsGreen(annotations);
			}
			return parameterListSyntax;
		}

		// Token: 0x06005D2B RID: 23851 RVA: 0x0016B6D9 File Offset: 0x001698D9
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ParameterListSyntax(base.Kind, this.openParenToken, this.parameters, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005D2C RID: 23852 RVA: 0x0016B6FF File Offset: 0x001698FF
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ParameterListSyntax(base.Kind, this.openParenToken, this.parameters, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x0016B728 File Offset: 0x00169928
		internal ParameterListSyntax(ObjectReader reader)
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
				this.parameters = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeParenToken = syntaxToken2;
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x0016B794 File Offset: 0x00169994
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.parameters);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x0016B7C1 File Offset: 0x001699C1
		static ParameterListSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ParameterListSyntax), (ObjectReader r) => new ParameterListSyntax(r));
		}

		// Token: 0x04001895 RID: 6293
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001896 RID: 6294
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode parameters;

		// Token: 0x04001897 RID: 6295
		internal readonly SyntaxToken closeParenToken;
	}
}
