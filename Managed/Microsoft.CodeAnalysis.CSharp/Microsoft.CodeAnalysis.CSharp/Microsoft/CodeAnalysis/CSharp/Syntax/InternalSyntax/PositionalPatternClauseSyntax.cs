using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003D9 RID: 985
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PositionalPatternClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005619 RID: 22041 RVA: 0x001544C8 File Offset: 0x001526C8
		internal PositionalPatternClauseSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode subpatterns, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (subpatterns != null)
			{
				base.AdjustFlagsAndWidth(subpatterns);
				this.subpatterns = subpatterns;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x0600561A RID: 22042 RVA: 0x00154518 File Offset: 0x00152718
		internal PositionalPatternClauseSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode subpatterns, SyntaxToken closeParenToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (subpatterns != null)
			{
				base.AdjustFlagsAndWidth(subpatterns);
				this.subpatterns = subpatterns;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x0600561B RID: 22043 RVA: 0x0015456A File Offset: 0x0015276A
		internal PositionalPatternClauseSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode subpatterns, SyntaxToken closeParenToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (subpatterns != null)
			{
				base.AdjustFlagsAndWidth(subpatterns);
				this.subpatterns = subpatterns;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x0600561C RID: 22044 RVA: 0x001545A9 File Offset: 0x001527A9
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x0600561D RID: 22045 RVA: 0x001545B1 File Offset: 0x001527B1
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<SubpatternSyntax> Subpatterns
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<SubpatternSyntax>(new SyntaxList<CSharpSyntaxNode>(this.subpatterns));
			}
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x0600561E RID: 22046 RVA: 0x001545C8 File Offset: 0x001527C8
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x0600561F RID: 22047 RVA: 0x001545D0 File Offset: 0x001527D0
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
				greenNode = this.subpatterns;
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

		// Token: 0x06005620 RID: 22048 RVA: 0x0015460F File Offset: 0x0015280F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new PositionalPatternClauseSyntax(this, parent, position);
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x00154619 File Offset: 0x00152819
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPositionalPatternClause(this);
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x00154622 File Offset: 0x00152822
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPositionalPatternClause(this);
		}

		// Token: 0x06005623 RID: 22051 RVA: 0x0015462C File Offset: 0x0015282C
		public PositionalPatternClauseSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SubpatternSyntax> subpatterns, SyntaxToken closeParenToken)
		{
			if (openParenToken == this.OpenParenToken)
			{
				SeparatedSyntaxList<SubpatternSyntax> separatedSyntaxList = this.Subpatterns;
				if (!((in subpatterns) != (in separatedSyntaxList)) && closeParenToken == this.CloseParenToken)
				{
					return this;
				}
			}
			PositionalPatternClauseSyntax positionalPatternClauseSyntax = SyntaxFactory.PositionalPatternClause(openParenToken, subpatterns, closeParenToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				positionalPatternClauseSyntax = positionalPatternClauseSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				positionalPatternClauseSyntax = positionalPatternClauseSyntax.WithAnnotationsGreen(annotations);
			}
			return positionalPatternClauseSyntax;
		}

		// Token: 0x06005624 RID: 22052 RVA: 0x00154695 File Offset: 0x00152895
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new PositionalPatternClauseSyntax(base.Kind, this.openParenToken, this.subpatterns, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x001546BB File Offset: 0x001528BB
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new PositionalPatternClauseSyntax(base.Kind, this.openParenToken, this.subpatterns, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x001546E4 File Offset: 0x001528E4
		internal PositionalPatternClauseSyntax(ObjectReader reader)
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
				this.subpatterns = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeParenToken = syntaxToken2;
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x00154750 File Offset: 0x00152950
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.subpatterns);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x0015477D File Offset: 0x0015297D
		static PositionalPatternClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(PositionalPatternClauseSyntax), (ObjectReader r) => new PositionalPatternClauseSyntax(r));
		}

		// Token: 0x040016D2 RID: 5842
		internal readonly SyntaxToken openParenToken;

		// Token: 0x040016D3 RID: 5843
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode subpatterns;

		// Token: 0x040016D4 RID: 5844
		internal readonly SyntaxToken closeParenToken;
	}
}
