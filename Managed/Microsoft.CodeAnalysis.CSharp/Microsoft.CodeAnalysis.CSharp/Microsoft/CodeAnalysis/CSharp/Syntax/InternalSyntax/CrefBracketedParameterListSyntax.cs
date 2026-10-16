using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200045B RID: 1115
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CrefBracketedParameterListSyntax : BaseCrefParameterListSyntax
	{
		// Token: 0x06005E0C RID: 24076 RVA: 0x0016DA60 File Offset: 0x0016BC60
		internal CrefBracketedParameterListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken closeBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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

		// Token: 0x06005E0D RID: 24077 RVA: 0x0016DAB0 File Offset: 0x0016BCB0
		internal CrefBracketedParameterListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken closeBracketToken, SyntaxFactoryContext context)
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

		// Token: 0x06005E0E RID: 24078 RVA: 0x0016DB02 File Offset: 0x0016BD02
		internal CrefBracketedParameterListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken closeBracketToken)
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

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x06005E0F RID: 24079 RVA: 0x0016DB41 File Offset: 0x0016BD41
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return this.openBracketToken;
			}
		}

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x06005E10 RID: 24080 RVA: 0x0016DB49 File Offset: 0x0016BD49
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SeparatedSyntaxList<CrefParameterSyntax> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<CrefParameterSyntax>(new SyntaxList<CSharpSyntaxNode>(this.parameters));
			}
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x06005E11 RID: 24081 RVA: 0x0016DB60 File Offset: 0x0016BD60
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return this.closeBracketToken;
			}
		}

		// Token: 0x06005E12 RID: 24082 RVA: 0x0016DB68 File Offset: 0x0016BD68
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

		// Token: 0x06005E13 RID: 24083 RVA: 0x0016DBA7 File Offset: 0x0016BDA7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new CrefBracketedParameterListSyntax(this, parent, position);
		}

		// Token: 0x06005E14 RID: 24084 RVA: 0x0016DBB1 File Offset: 0x0016BDB1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCrefBracketedParameterList(this);
		}

		// Token: 0x06005E15 RID: 24085 RVA: 0x0016DBBA File Offset: 0x0016BDBA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCrefBracketedParameterList(this);
		}

		// Token: 0x06005E16 RID: 24086 RVA: 0x0016DBC4 File Offset: 0x0016BDC4
		public CrefBracketedParameterListSyntax Update(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<CrefParameterSyntax> parameters, SyntaxToken closeBracketToken)
		{
			if (openBracketToken == this.OpenBracketToken)
			{
				SeparatedSyntaxList<CrefParameterSyntax> separatedSyntaxList = this.Parameters;
				if (!((in parameters) != (in separatedSyntaxList)) && closeBracketToken == this.CloseBracketToken)
				{
					return this;
				}
			}
			CrefBracketedParameterListSyntax crefBracketedParameterListSyntax = SyntaxFactory.CrefBracketedParameterList(openBracketToken, parameters, closeBracketToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				crefBracketedParameterListSyntax = crefBracketedParameterListSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				crefBracketedParameterListSyntax = crefBracketedParameterListSyntax.WithAnnotationsGreen(annotations);
			}
			return crefBracketedParameterListSyntax;
		}

		// Token: 0x06005E17 RID: 24087 RVA: 0x0016DC2D File Offset: 0x0016BE2D
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new CrefBracketedParameterListSyntax(base.Kind, this.openBracketToken, this.parameters, this.closeBracketToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005E18 RID: 24088 RVA: 0x0016DC53 File Offset: 0x0016BE53
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new CrefBracketedParameterListSyntax(base.Kind, this.openBracketToken, this.parameters, this.closeBracketToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005E19 RID: 24089 RVA: 0x0016DC7C File Offset: 0x0016BE7C
		internal CrefBracketedParameterListSyntax(ObjectReader reader)
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

		// Token: 0x06005E1A RID: 24090 RVA: 0x0016DCE8 File Offset: 0x0016BEE8
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openBracketToken);
			writer.WriteValue(this.parameters);
			writer.WriteValue(this.closeBracketToken);
		}

		// Token: 0x06005E1B RID: 24091 RVA: 0x0016DD15 File Offset: 0x0016BF15
		static CrefBracketedParameterListSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(CrefBracketedParameterListSyntax), (ObjectReader r) => new CrefBracketedParameterListSyntax(r));
		}

		// Token: 0x040018BB RID: 6331
		internal readonly SyntaxToken openBracketToken;

		// Token: 0x040018BC RID: 6332
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode parameters;

		// Token: 0x040018BD RID: 6333
		internal readonly SyntaxToken closeBracketToken;
	}
}
