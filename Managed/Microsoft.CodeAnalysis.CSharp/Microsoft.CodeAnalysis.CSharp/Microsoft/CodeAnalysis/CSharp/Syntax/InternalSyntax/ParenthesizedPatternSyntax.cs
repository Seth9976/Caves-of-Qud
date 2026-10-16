using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003DD RID: 989
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ParenthesizedPatternSyntax : PatternSyntax
	{
		// Token: 0x06005656 RID: 22102 RVA: 0x00154E17 File Offset: 0x00153017
		internal ParenthesizedPatternSyntax(SyntaxKind kind, SyntaxToken openParenToken, PatternSyntax pattern, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005657 RID: 22103 RVA: 0x00154E58 File Offset: 0x00153058
		internal ParenthesizedPatternSyntax(SyntaxKind kind, SyntaxToken openParenToken, PatternSyntax pattern, SyntaxToken closeParenToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005658 RID: 22104 RVA: 0x00154EA7 File Offset: 0x001530A7
		internal ParenthesizedPatternSyntax(SyntaxKind kind, SyntaxToken openParenToken, PatternSyntax pattern, SyntaxToken closeParenToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06005659 RID: 22105 RVA: 0x00154EE3 File Offset: 0x001530E3
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x0600565A RID: 22106 RVA: 0x00154EEB File Offset: 0x001530EB
		public PatternSyntax Pattern
		{
			get
			{
				return this.pattern;
			}
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x0600565B RID: 22107 RVA: 0x00154EF3 File Offset: 0x001530F3
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x00154EFC File Offset: 0x001530FC
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
				greenNode = this.pattern;
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

		// Token: 0x0600565D RID: 22109 RVA: 0x00154F3B File Offset: 0x0015313B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ParenthesizedPatternSyntax(this, parent, position);
		}

		// Token: 0x0600565E RID: 22110 RVA: 0x00154F45 File Offset: 0x00153145
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitParenthesizedPattern(this);
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x00154F4E File Offset: 0x0015314E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitParenthesizedPattern(this);
		}

		// Token: 0x06005660 RID: 22112 RVA: 0x00154F58 File Offset: 0x00153158
		public ParenthesizedPatternSyntax Update(SyntaxToken openParenToken, PatternSyntax pattern, SyntaxToken closeParenToken)
		{
			if (openParenToken != this.OpenParenToken || pattern != this.Pattern || closeParenToken != this.CloseParenToken)
			{
				ParenthesizedPatternSyntax parenthesizedPatternSyntax = SyntaxFactory.ParenthesizedPattern(openParenToken, pattern, closeParenToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					parenthesizedPatternSyntax = parenthesizedPatternSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					parenthesizedPatternSyntax = parenthesizedPatternSyntax.WithAnnotationsGreen(annotations);
				}
				return parenthesizedPatternSyntax;
			}
			return this;
		}

		// Token: 0x06005661 RID: 22113 RVA: 0x00154FB8 File Offset: 0x001531B8
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ParenthesizedPatternSyntax(base.Kind, this.openParenToken, this.pattern, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x00154FDE File Offset: 0x001531DE
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ParenthesizedPatternSyntax(base.Kind, this.openParenToken, this.pattern, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x00155004 File Offset: 0x00153204
		internal ParenthesizedPatternSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openParenToken = syntaxToken;
			PatternSyntax patternSyntax = (PatternSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(patternSyntax);
			this.pattern = patternSyntax;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeParenToken = syntaxToken2;
		}

		// Token: 0x06005664 RID: 22116 RVA: 0x0015506D File Offset: 0x0015326D
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.pattern);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x0015509A File Offset: 0x0015329A
		static ParenthesizedPatternSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ParenthesizedPatternSyntax), (ObjectReader r) => new ParenthesizedPatternSyntax(r));
		}

		// Token: 0x040016DB RID: 5851
		internal readonly SyntaxToken openParenToken;

		// Token: 0x040016DC RID: 5852
		internal readonly PatternSyntax pattern;

		// Token: 0x040016DD RID: 5853
		internal readonly SyntaxToken closeParenToken;
	}
}
