using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200044A RID: 1098
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BracketedParameterListSyntax : BaseParameterListSyntax
	{
		// Token: 0x06005D30 RID: 23856 RVA: 0x0016B7E4 File Offset: 0x001699E4
		internal BracketedParameterListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken closeBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x06005D31 RID: 23857 RVA: 0x0016B834 File Offset: 0x00169A34
		internal BracketedParameterListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken closeBracketToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x0016B886 File Offset: 0x00169A86
		internal BracketedParameterListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken closeBracketToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x06005D33 RID: 23859 RVA: 0x0016B8C5 File Offset: 0x00169AC5
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return this.openBracketToken;
			}
		}

		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x06005D34 RID: 23860 RVA: 0x0016B8CD File Offset: 0x00169ACD
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SeparatedSyntaxList<ParameterSyntax> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<ParameterSyntax>(new SyntaxList<CSharpSyntaxNode>(this.parameters));
			}
		}

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x06005D35 RID: 23861 RVA: 0x0016B8E4 File Offset: 0x00169AE4
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return this.closeBracketToken;
			}
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x0016B8EC File Offset: 0x00169AEC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.openBracketToken;
				break;
			case 1:
				greenNode = this.parameters;
				break;
			case 2:
				greenNode = this.closeBracketToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x0016B92B File Offset: 0x00169B2B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new BracketedParameterListSyntax(this, parent, position);
		}

		// Token: 0x06005D38 RID: 23864 RVA: 0x0016B935 File Offset: 0x00169B35
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBracketedParameterList(this);
		}

		// Token: 0x06005D39 RID: 23865 RVA: 0x0016B93E File Offset: 0x00169B3E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBracketedParameterList(this);
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x0016B948 File Offset: 0x00169B48
		public BracketedParameterListSyntax Update(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ParameterSyntax> parameters, SyntaxToken closeBracketToken)
		{
			if (openBracketToken == this.OpenBracketToken)
			{
				SeparatedSyntaxList<ParameterSyntax> separatedSyntaxList = this.Parameters;
				if (!((in parameters) != (in separatedSyntaxList)) && closeBracketToken == this.CloseBracketToken)
				{
					return this;
				}
			}
			BracketedParameterListSyntax bracketedParameterListSyntax = SyntaxFactory.BracketedParameterList(openBracketToken, parameters, closeBracketToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				bracketedParameterListSyntax = bracketedParameterListSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				bracketedParameterListSyntax = bracketedParameterListSyntax.WithAnnotationsGreen(annotations);
			}
			return bracketedParameterListSyntax;
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x0016B9B1 File Offset: 0x00169BB1
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new BracketedParameterListSyntax(base.Kind, this.openBracketToken, this.parameters, this.closeBracketToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005D3C RID: 23868 RVA: 0x0016B9D7 File Offset: 0x00169BD7
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new BracketedParameterListSyntax(base.Kind, this.openBracketToken, this.parameters, this.closeBracketToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005D3D RID: 23869 RVA: 0x0016BA00 File Offset: 0x00169C00
		internal BracketedParameterListSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openBracketToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.parameters = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeBracketToken = syntaxToken2;
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x0016BA6C File Offset: 0x00169C6C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openBracketToken);
			writer.WriteValue(this.parameters);
			writer.WriteValue(this.closeBracketToken);
		}

		// Token: 0x06005D3F RID: 23871 RVA: 0x0016BA99 File Offset: 0x00169C99
		static BracketedParameterListSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(BracketedParameterListSyntax), (ObjectReader r) => new BracketedParameterListSyntax(r));
		}

		// Token: 0x04001898 RID: 6296
		internal readonly SyntaxToken openBracketToken;

		// Token: 0x04001899 RID: 6297
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode parameters;

		// Token: 0x0400189A RID: 6298
		internal readonly SyntaxToken closeBracketToken;
	}
}
