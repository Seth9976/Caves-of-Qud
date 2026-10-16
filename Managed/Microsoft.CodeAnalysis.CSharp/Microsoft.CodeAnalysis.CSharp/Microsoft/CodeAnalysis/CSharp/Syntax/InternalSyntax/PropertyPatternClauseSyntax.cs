using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003DA RID: 986
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PropertyPatternClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005629 RID: 22057 RVA: 0x001547A0 File Offset: 0x001529A0
		internal PropertyPatternClauseSyntax(SyntaxKind kind, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode subpatterns, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (subpatterns != null)
			{
				base.AdjustFlagsAndWidth(subpatterns);
				this.subpatterns = subpatterns;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x0600562A RID: 22058 RVA: 0x001547F0 File Offset: 0x001529F0
		internal PropertyPatternClauseSyntax(SyntaxKind kind, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode subpatterns, SyntaxToken closeBraceToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (subpatterns != null)
			{
				base.AdjustFlagsAndWidth(subpatterns);
				this.subpatterns = subpatterns;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x0600562B RID: 22059 RVA: 0x00154842 File Offset: 0x00152A42
		internal PropertyPatternClauseSyntax(SyntaxKind kind, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode subpatterns, SyntaxToken closeBraceToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (subpatterns != null)
			{
				base.AdjustFlagsAndWidth(subpatterns);
				this.subpatterns = subpatterns;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x0600562C RID: 22060 RVA: 0x00154881 File Offset: 0x00152A81
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return this.openBraceToken;
			}
		}

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x0600562D RID: 22061 RVA: 0x00154889 File Offset: 0x00152A89
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<SubpatternSyntax> Subpatterns
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<SubpatternSyntax>(new SyntaxList<CSharpSyntaxNode>(this.subpatterns));
			}
		}

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x0600562E RID: 22062 RVA: 0x001548A0 File Offset: 0x00152AA0
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return this.closeBraceToken;
			}
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x001548A8 File Offset: 0x00152AA8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.openBraceToken;
				break;
			case 1:
				greenNode = this.subpatterns;
				break;
			case 2:
				greenNode = this.closeBraceToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005630 RID: 22064 RVA: 0x001548E7 File Offset: 0x00152AE7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new PropertyPatternClauseSyntax(this, parent, position);
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x001548F1 File Offset: 0x00152AF1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPropertyPatternClause(this);
		}

		// Token: 0x06005632 RID: 22066 RVA: 0x001548FA File Offset: 0x00152AFA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPropertyPatternClause(this);
		}

		// Token: 0x06005633 RID: 22067 RVA: 0x00154904 File Offset: 0x00152B04
		public PropertyPatternClauseSyntax Update(SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SubpatternSyntax> subpatterns, SyntaxToken closeBraceToken)
		{
			if (openBraceToken == this.OpenBraceToken)
			{
				SeparatedSyntaxList<SubpatternSyntax> separatedSyntaxList = this.Subpatterns;
				if (!((in subpatterns) != (in separatedSyntaxList)) && closeBraceToken == this.CloseBraceToken)
				{
					return this;
				}
			}
			PropertyPatternClauseSyntax propertyPatternClauseSyntax = SyntaxFactory.PropertyPatternClause(openBraceToken, subpatterns, closeBraceToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				propertyPatternClauseSyntax = propertyPatternClauseSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				propertyPatternClauseSyntax = propertyPatternClauseSyntax.WithAnnotationsGreen(annotations);
			}
			return propertyPatternClauseSyntax;
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x0015496D File Offset: 0x00152B6D
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new PropertyPatternClauseSyntax(base.Kind, this.openBraceToken, this.subpatterns, this.closeBraceToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x00154993 File Offset: 0x00152B93
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new PropertyPatternClauseSyntax(base.Kind, this.openBraceToken, this.subpatterns, this.closeBraceToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x001549BC File Offset: 0x00152BBC
		internal PropertyPatternClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openBraceToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.subpatterns = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeBraceToken = syntaxToken2;
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x00154A28 File Offset: 0x00152C28
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openBraceToken);
			writer.WriteValue(this.subpatterns);
			writer.WriteValue(this.closeBraceToken);
		}

		// Token: 0x06005638 RID: 22072 RVA: 0x00154A55 File Offset: 0x00152C55
		static PropertyPatternClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(PropertyPatternClauseSyntax), (ObjectReader r) => new PropertyPatternClauseSyntax(r));
		}

		// Token: 0x040016D5 RID: 5845
		internal readonly SyntaxToken openBraceToken;

		// Token: 0x040016D6 RID: 5846
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode subpatterns;

		// Token: 0x040016D7 RID: 5847
		internal readonly SyntaxToken closeBraceToken;
	}
}
