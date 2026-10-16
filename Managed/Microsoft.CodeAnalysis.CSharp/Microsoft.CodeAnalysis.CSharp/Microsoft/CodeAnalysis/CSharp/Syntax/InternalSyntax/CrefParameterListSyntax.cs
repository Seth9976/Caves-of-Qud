using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200045A RID: 1114
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CrefParameterListSyntax : BaseCrefParameterListSyntax
	{
		// Token: 0x06005DFC RID: 24060 RVA: 0x0016D788 File Offset: 0x0016B988
		internal CrefParameterListSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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

		// Token: 0x06005DFD RID: 24061 RVA: 0x0016D7D8 File Offset: 0x0016B9D8
		internal CrefParameterListSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken closeParenToken, SyntaxFactoryContext context)
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

		// Token: 0x06005DFE RID: 24062 RVA: 0x0016D82A File Offset: 0x0016BA2A
		internal CrefParameterListSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken closeParenToken)
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

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x06005DFF RID: 24063 RVA: 0x0016D869 File Offset: 0x0016BA69
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x06005E00 RID: 24064 RVA: 0x0016D871 File Offset: 0x0016BA71
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SeparatedSyntaxList<CrefParameterSyntax> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<CrefParameterSyntax>(new SyntaxList<CSharpSyntaxNode>(this.parameters));
			}
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06005E01 RID: 24065 RVA: 0x0016D888 File Offset: 0x0016BA88
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x06005E02 RID: 24066 RVA: 0x0016D890 File Offset: 0x0016BA90
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

		// Token: 0x06005E03 RID: 24067 RVA: 0x0016D8CF File Offset: 0x0016BACF
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new CrefParameterListSyntax(this, parent, position);
		}

		// Token: 0x06005E04 RID: 24068 RVA: 0x0016D8D9 File Offset: 0x0016BAD9
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCrefParameterList(this);
		}

		// Token: 0x06005E05 RID: 24069 RVA: 0x0016D8E2 File Offset: 0x0016BAE2
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCrefParameterList(this);
		}

		// Token: 0x06005E06 RID: 24070 RVA: 0x0016D8EC File Offset: 0x0016BAEC
		public CrefParameterListSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<CrefParameterSyntax> parameters, SyntaxToken closeParenToken)
		{
			if (openParenToken == this.OpenParenToken)
			{
				SeparatedSyntaxList<CrefParameterSyntax> separatedSyntaxList = this.Parameters;
				if (!((in parameters) != (in separatedSyntaxList)) && closeParenToken == this.CloseParenToken)
				{
					return this;
				}
			}
			CrefParameterListSyntax crefParameterListSyntax = SyntaxFactory.CrefParameterList(openParenToken, parameters, closeParenToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				crefParameterListSyntax = crefParameterListSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				crefParameterListSyntax = crefParameterListSyntax.WithAnnotationsGreen(annotations);
			}
			return crefParameterListSyntax;
		}

		// Token: 0x06005E07 RID: 24071 RVA: 0x0016D955 File Offset: 0x0016BB55
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new CrefParameterListSyntax(base.Kind, this.openParenToken, this.parameters, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005E08 RID: 24072 RVA: 0x0016D97B File Offset: 0x0016BB7B
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new CrefParameterListSyntax(base.Kind, this.openParenToken, this.parameters, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005E09 RID: 24073 RVA: 0x0016D9A4 File Offset: 0x0016BBA4
		internal CrefParameterListSyntax(ObjectReader reader)
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

		// Token: 0x06005E0A RID: 24074 RVA: 0x0016DA10 File Offset: 0x0016BC10
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.parameters);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x06005E0B RID: 24075 RVA: 0x0016DA3D File Offset: 0x0016BC3D
		static CrefParameterListSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(CrefParameterListSyntax), (ObjectReader r) => new CrefParameterListSyntax(r));
		}

		// Token: 0x040018B8 RID: 6328
		internal readonly SyntaxToken openParenToken;

		// Token: 0x040018B9 RID: 6329
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode parameters;

		// Token: 0x040018BA RID: 6330
		internal readonly SyntaxToken closeParenToken;
	}
}
