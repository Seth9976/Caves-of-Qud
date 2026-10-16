using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000375 RID: 885
	internal class SyntaxToken : CSharpSyntaxNode
	{
		// Token: 0x06005054 RID: 20564 RVA: 0x001458AC File Offset: 0x00143AAC
		internal SyntaxToken(SyntaxKind kind)
			: base(kind)
		{
			base.FullWidth = this.Text.Length;
			this.flags |= GreenNode.NodeFlags.IsNotMissing;
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x001458D5 File Offset: 0x00143AD5
		internal SyntaxToken(SyntaxKind kind, DiagnosticInfo[] diagnostics)
			: base(kind, diagnostics)
		{
			base.FullWidth = this.Text.Length;
			this.flags |= GreenNode.NodeFlags.IsNotMissing;
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x001458FF File Offset: 0x00143AFF
		internal SyntaxToken(SyntaxKind kind, DiagnosticInfo[] diagnostics, SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.FullWidth = this.Text.Length;
			this.flags |= GreenNode.NodeFlags.IsNotMissing;
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x0014592A File Offset: 0x00143B2A
		internal SyntaxToken(SyntaxKind kind, int fullWidth)
			: base(kind, fullWidth)
		{
			this.flags |= GreenNode.NodeFlags.IsNotMissing;
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x00145943 File Offset: 0x00143B43
		internal SyntaxToken(SyntaxKind kind, int fullWidth, DiagnosticInfo[] diagnostics)
			: base(kind, diagnostics, fullWidth)
		{
			this.flags |= GreenNode.NodeFlags.IsNotMissing;
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x0014595D File Offset: 0x00143B5D
		internal SyntaxToken(SyntaxKind kind, int fullWidth, DiagnosticInfo[] diagnostics, SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations, fullWidth)
		{
			this.flags |= GreenNode.NodeFlags.IsNotMissing;
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x0014597C File Offset: 0x00143B7C
		internal SyntaxToken(ObjectReader reader)
			: base(reader)
		{
			string text = this.Text;
			if (text != null)
			{
				base.FullWidth = text.Length;
			}
			this.flags |= GreenNode.NodeFlags.IsNotMissing;
		}

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x0600505B RID: 20571 RVA: 0x001459B5 File Offset: 0x00143BB5
		internal override bool ShouldReuseInSerialization
		{
			get
			{
				return base.ShouldReuseInSerialization && base.FullWidth < 42;
			}
		}

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x0600505C RID: 20572 RVA: 0x001459CB File Offset: 0x00143BCB
		public override bool IsToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x001459CE File Offset: 0x00143BCE
		internal override GreenNode GetSlot(int index)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x001459D8 File Offset: 0x00143BD8
		internal static SyntaxToken Create(SyntaxKind kind)
		{
			if (kind <= SyntaxKind.EndOfFileToken)
			{
				return SyntaxToken.s_tokensWithNoTrivia[(int)kind].Value;
			}
			if (!SyntaxFacts.IsAnyToken(kind))
			{
				throw new ArgumentException(string.Format(CSharpResources.ThisMethodCanOnlyBeUsedToCreateTokens, kind), "kind");
			}
			return SyntaxToken.CreateMissing(kind, null, null);
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x00145A2C File Offset: 0x00143C2C
		internal static SyntaxToken Create(SyntaxKind kind, GreenNode leading, GreenNode trailing)
		{
			if (kind > SyntaxKind.EndOfFileToken)
			{
				if (!SyntaxFacts.IsAnyToken(kind))
				{
					throw new ArgumentException(string.Format(CSharpResources.ThisMethodCanOnlyBeUsedToCreateTokens, kind), "kind");
				}
				return SyntaxToken.CreateMissing(kind, leading, trailing);
			}
			else
			{
				if (leading == null)
				{
					if (trailing == null)
					{
						return SyntaxToken.s_tokensWithNoTrivia[(int)kind].Value;
					}
					if (trailing == SyntaxFactory.Space)
					{
						return SyntaxToken.s_tokensWithSingleTrailingSpace[(int)kind].Value;
					}
					if (trailing == SyntaxFactory.CarriageReturnLineFeed)
					{
						return SyntaxToken.s_tokensWithSingleTrailingCRLF[(int)kind].Value;
					}
				}
				if (leading == SyntaxFactory.ElasticZeroSpace && trailing == SyntaxFactory.ElasticZeroSpace)
				{
					return SyntaxToken.s_tokensWithElasticTrivia[(int)kind].Value;
				}
				return new SyntaxToken.SyntaxTokenWithTrivia(kind, leading, trailing);
			}
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x00145ADF File Offset: 0x00143CDF
		internal static SyntaxToken CreateMissing(SyntaxKind kind, GreenNode leading, GreenNode trailing)
		{
			return new SyntaxToken.MissingTokenWithTrivia(kind, leading, trailing);
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x00145AEC File Offset: 0x00143CEC
		static SyntaxToken()
		{
			ObjectBinder.RegisterTypeReader(typeof(SyntaxToken), (ObjectReader r) => new SyntaxToken(r));
			for (SyntaxKind syntaxKind = SyntaxKind.TildeToken; syntaxKind <= SyntaxKind.EndOfFileToken; syntaxKind += 1)
			{
				SyntaxToken.s_tokensWithNoTrivia[(int)syntaxKind].Value = new SyntaxToken(syntaxKind);
				SyntaxToken.s_tokensWithElasticTrivia[(int)syntaxKind].Value = new SyntaxToken.SyntaxTokenWithTrivia(syntaxKind, SyntaxFactory.ElasticZeroSpace, SyntaxFactory.ElasticZeroSpace);
				SyntaxToken.s_tokensWithSingleTrailingSpace[(int)syntaxKind].Value = new SyntaxToken.SyntaxTokenWithTrivia(syntaxKind, null, SyntaxFactory.Space);
				SyntaxToken.s_tokensWithSingleTrailingCRLF[(int)syntaxKind].Value = new SyntaxToken.SyntaxTokenWithTrivia(syntaxKind, null, SyntaxFactory.CarriageReturnLineFeed);
			}
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x00145BD7 File Offset: 0x00143DD7
		internal static IEnumerable<SyntaxToken> GetWellKnownTokens()
		{
			foreach (ArrayElement<SyntaxToken> arrayElement in SyntaxToken.s_tokensWithNoTrivia)
			{
				if (arrayElement.Value != null)
				{
					yield return arrayElement.Value;
				}
			}
			ArrayElement<SyntaxToken>[] array = null;
			foreach (ArrayElement<SyntaxToken> arrayElement2 in SyntaxToken.s_tokensWithElasticTrivia)
			{
				if (arrayElement2.Value != null)
				{
					yield return arrayElement2.Value;
				}
			}
			array = null;
			foreach (ArrayElement<SyntaxToken> arrayElement3 in SyntaxToken.s_tokensWithSingleTrailingSpace)
			{
				if (arrayElement3.Value != null)
				{
					yield return arrayElement3.Value;
				}
			}
			array = null;
			foreach (ArrayElement<SyntaxToken> arrayElement4 in SyntaxToken.s_tokensWithSingleTrailingCRLF)
			{
				if (arrayElement4.Value != null)
				{
					yield return arrayElement4.Value;
				}
			}
			array = null;
			yield break;
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x00145BE0 File Offset: 0x00143DE0
		internal static SyntaxToken Identifier(string text)
		{
			return new SyntaxToken.SyntaxIdentifier(text);
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x00145BE8 File Offset: 0x00143DE8
		internal static SyntaxToken Identifier(GreenNode leading, string text, GreenNode trailing)
		{
			if (leading != null)
			{
				return new SyntaxToken.SyntaxIdentifierWithTrivia(SyntaxKind.IdentifierToken, text, text, leading, trailing);
			}
			if (trailing == null)
			{
				return SyntaxToken.Identifier(text);
			}
			return new SyntaxToken.SyntaxIdentifierWithTrailingTrivia(text, trailing);
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x00145C0D File Offset: 0x00143E0D
		internal static SyntaxToken Identifier(SyntaxKind contextualKind, GreenNode leading, string text, string valueText, GreenNode trailing)
		{
			if (contextualKind == SyntaxKind.IdentifierToken && valueText == text)
			{
				return SyntaxToken.Identifier(leading, text, trailing);
			}
			return new SyntaxToken.SyntaxIdentifierWithTrivia(contextualKind, text, valueText, leading, trailing);
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x00145C35 File Offset: 0x00143E35
		internal static SyntaxToken WithValue<T>(SyntaxKind kind, string text, T value)
		{
			return new SyntaxToken.SyntaxTokenWithValue<T>(kind, text, value);
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x00145C3F File Offset: 0x00143E3F
		internal static SyntaxToken WithValue<T>(SyntaxKind kind, GreenNode leading, string text, T value, GreenNode trailing)
		{
			return new SyntaxToken.SyntaxTokenWithValueAndTrivia<T>(kind, text, value, leading, trailing);
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x00145C4C File Offset: 0x00143E4C
		internal static SyntaxToken StringLiteral(string text)
		{
			return new SyntaxToken.SyntaxTokenWithValue<string>(SyntaxKind.StringLiteralToken, text, text);
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x00145C5A File Offset: 0x00143E5A
		internal static SyntaxToken StringLiteral(CSharpSyntaxNode leading, string text, CSharpSyntaxNode trailing)
		{
			return new SyntaxToken.SyntaxTokenWithValueAndTrivia<string>(SyntaxKind.StringLiteralToken, text, text, leading, trailing);
		}

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x0600506A RID: 20586 RVA: 0x00145C6A File Offset: 0x00143E6A
		public virtual SyntaxKind ContextualKind
		{
			get
			{
				return base.Kind;
			}
		}

		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x0600506B RID: 20587 RVA: 0x00145C72 File Offset: 0x00143E72
		public override int RawContextualKind
		{
			get
			{
				return (int)this.ContextualKind;
			}
		}

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x0600506C RID: 20588 RVA: 0x00145C7A File Offset: 0x00143E7A
		public virtual string Text
		{
			get
			{
				return SyntaxFacts.GetText(base.Kind);
			}
		}

		// Token: 0x0600506D RID: 20589 RVA: 0x00145C87 File Offset: 0x00143E87
		public override string ToString()
		{
			return this.Text;
		}

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x0600506E RID: 20590 RVA: 0x00145C90 File Offset: 0x00143E90
		public virtual object Value
		{
			get
			{
				switch (base.Kind)
				{
				case SyntaxKind.NullKeyword:
					return null;
				case SyntaxKind.TrueKeyword:
					return Boxes.BoxedTrue;
				case SyntaxKind.FalseKeyword:
					return Boxes.BoxedFalse;
				default:
					return this.Text;
				}
			}
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x00145CD2 File Offset: 0x00143ED2
		public override object GetValue()
		{
			return this.Value;
		}

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06005070 RID: 20592 RVA: 0x00145CDA File Offset: 0x00143EDA
		public virtual string ValueText
		{
			get
			{
				return this.Text;
			}
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x00145CE2 File Offset: 0x00143EE2
		public override string GetValueText()
		{
			return this.ValueText;
		}

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06005072 RID: 20594 RVA: 0x00145CEA File Offset: 0x00143EEA
		public override int Width
		{
			get
			{
				return this.Text.Length;
			}
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x00145CF8 File Offset: 0x00143EF8
		public override int GetLeadingTriviaWidth()
		{
			GreenNode leadingTrivia = this.GetLeadingTrivia();
			if (leadingTrivia == null)
			{
				return 0;
			}
			return leadingTrivia.FullWidth;
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x00145D18 File Offset: 0x00143F18
		public override int GetTrailingTriviaWidth()
		{
			GreenNode trailingTrivia = this.GetTrailingTrivia();
			if (trailingTrivia == null)
			{
				return 0;
			}
			return trailingTrivia.FullWidth;
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06005075 RID: 20597 RVA: 0x00145D37 File Offset: 0x00143F37
		internal SyntaxList<CSharpSyntaxNode> LeadingTrivia
		{
			get
			{
				return new SyntaxList<CSharpSyntaxNode>(this.GetLeadingTrivia());
			}
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06005076 RID: 20598 RVA: 0x00145D44 File Offset: 0x00143F44
		internal SyntaxList<CSharpSyntaxNode> TrailingTrivia
		{
			get
			{
				return new SyntaxList<CSharpSyntaxNode>(this.GetTrailingTrivia());
			}
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x00145D51 File Offset: 0x00143F51
		public sealed override GreenNode WithLeadingTrivia(GreenNode trivia)
		{
			return this.TokenWithLeadingTrivia(trivia);
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x00145D5A File Offset: 0x00143F5A
		public virtual SyntaxToken TokenWithLeadingTrivia(GreenNode trivia)
		{
			return new SyntaxToken.SyntaxTokenWithTrivia(base.Kind, trivia, null, base.GetDiagnostics(), base.GetAnnotations());
		}

		// Token: 0x06005079 RID: 20601 RVA: 0x00145D75 File Offset: 0x00143F75
		public sealed override GreenNode WithTrailingTrivia(GreenNode trivia)
		{
			return this.TokenWithTrailingTrivia(trivia);
		}

		// Token: 0x0600507A RID: 20602 RVA: 0x00145D7E File Offset: 0x00143F7E
		public virtual SyntaxToken TokenWithTrailingTrivia(GreenNode trivia)
		{
			return new SyntaxToken.SyntaxTokenWithTrivia(base.Kind, null, trivia, base.GetDiagnostics(), base.GetAnnotations());
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x00145D99 File Offset: 0x00143F99
		internal override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
		{
			return new SyntaxToken(base.Kind, base.FullWidth, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600507C RID: 20604 RVA: 0x00145DB3 File Offset: 0x00143FB3
		internal override GreenNode SetAnnotations(SyntaxAnnotation[] annotations)
		{
			return new SyntaxToken(base.Kind, base.FullWidth, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600507D RID: 20605 RVA: 0x00145DCD File Offset: 0x00143FCD
		internal override DirectiveStack ApplyDirectives(DirectiveStack stack)
		{
			if (base.ContainsDirectives)
			{
				stack = SyntaxToken.ApplyDirectivesToTrivia(this.GetLeadingTrivia(), stack);
				stack = SyntaxToken.ApplyDirectivesToTrivia(this.GetTrailingTrivia(), stack);
			}
			return stack;
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x00145DF4 File Offset: 0x00143FF4
		private static DirectiveStack ApplyDirectivesToTrivia(GreenNode triviaList, DirectiveStack stack)
		{
			if (triviaList != null && triviaList.ContainsDirectives)
			{
				return CSharpSyntaxNode.ApplyDirectivesToListOrNode(triviaList, stack);
			}
			return stack;
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x00145E0A File Offset: 0x0014400A
		public override TResult Accept<TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitToken(this);
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x00145E13 File Offset: 0x00144013
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitToken(this);
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x00145E1C File Offset: 0x0014401C
		protected override void WriteTokenTo(TextWriter writer, bool leading, bool trailing)
		{
			if (leading)
			{
				GreenNode leadingTrivia = this.GetLeadingTrivia();
				if (leadingTrivia != null)
				{
					leadingTrivia.WriteTo(writer, true, true);
				}
			}
			writer.Write(this.Text);
			if (trailing)
			{
				GreenNode trailingTrivia = this.GetTrailingTrivia();
				if (trailingTrivia != null)
				{
					trailingTrivia.WriteTo(writer, true, true);
				}
			}
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x00145E64 File Offset: 0x00144064
		public override bool IsEquivalentTo(GreenNode other)
		{
			if (!base.IsEquivalentTo(other))
			{
				return false;
			}
			SyntaxToken syntaxToken = (SyntaxToken)other;
			if (this.Text != syntaxToken.Text)
			{
				return false;
			}
			GreenNode leadingTrivia = this.GetLeadingTrivia();
			GreenNode leadingTrivia2 = syntaxToken.GetLeadingTrivia();
			if (leadingTrivia != leadingTrivia2)
			{
				if (leadingTrivia == null || leadingTrivia2 == null)
				{
					return false;
				}
				if (!leadingTrivia.IsEquivalentTo(leadingTrivia2))
				{
					return false;
				}
			}
			GreenNode trailingTrivia = this.GetTrailingTrivia();
			GreenNode trailingTrivia2 = syntaxToken.GetTrailingTrivia();
			if (trailingTrivia != trailingTrivia2)
			{
				if (trailingTrivia == null || trailingTrivia2 == null)
				{
					return false;
				}
				if (!trailingTrivia.IsEquivalentTo(trailingTrivia2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005083 RID: 20611 RVA: 0x00145EE7 File Offset: 0x001440E7
		internal override SyntaxNode CreateRed(SyntaxNode parent, int position)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x040015D4 RID: 5588
		internal const SyntaxKind FirstTokenWithWellKnownText = SyntaxKind.TildeToken;

		// Token: 0x040015D5 RID: 5589
		internal const SyntaxKind LastTokenWithWellKnownText = SyntaxKind.EndOfFileToken;

		// Token: 0x040015D6 RID: 5590
		private static readonly ArrayElement<SyntaxToken>[] s_tokensWithNoTrivia = new ArrayElement<SyntaxToken>[8497];

		// Token: 0x040015D7 RID: 5591
		private static readonly ArrayElement<SyntaxToken>[] s_tokensWithElasticTrivia = new ArrayElement<SyntaxToken>[8497];

		// Token: 0x040015D8 RID: 5592
		private static readonly ArrayElement<SyntaxToken>[] s_tokensWithSingleTrailingSpace = new ArrayElement<SyntaxToken>[8497];

		// Token: 0x040015D9 RID: 5593
		private static readonly ArrayElement<SyntaxToken>[] s_tokensWithSingleTrailingCRLF = new ArrayElement<SyntaxToken>[8497];

		// Token: 0x020007B4 RID: 1972
		internal class MissingTokenWithTrivia : SyntaxToken.SyntaxTokenWithTrivia
		{
			// Token: 0x06008922 RID: 35106 RVA: 0x001EFF47 File Offset: 0x001EE147
			internal MissingTokenWithTrivia(SyntaxKind kind, GreenNode leading, GreenNode trailing)
				: base(kind, leading, trailing)
			{
				this.flags &= ~GreenNode.NodeFlags.IsNotMissing;
			}

			// Token: 0x06008923 RID: 35107 RVA: 0x001EFF64 File Offset: 0x001EE164
			internal MissingTokenWithTrivia(SyntaxKind kind, GreenNode leading, GreenNode trailing, DiagnosticInfo[] diagnostics, SyntaxAnnotation[] annotations)
				: base(kind, leading, trailing, diagnostics, annotations)
			{
				this.flags &= ~GreenNode.NodeFlags.IsNotMissing;
			}

			// Token: 0x06008924 RID: 35108 RVA: 0x001EFF85 File Offset: 0x001EE185
			internal MissingTokenWithTrivia(ObjectReader reader)
				: base(reader)
			{
				this.flags &= ~GreenNode.NodeFlags.IsNotMissing;
			}

			// Token: 0x06008925 RID: 35109 RVA: 0x001EFFA0 File Offset: 0x001EE1A0
			static MissingTokenWithTrivia()
			{
				ObjectBinder.RegisterTypeReader(typeof(SyntaxToken.MissingTokenWithTrivia), (ObjectReader r) => new SyntaxToken.MissingTokenWithTrivia(r));
			}

			// Token: 0x1700273F RID: 10047
			// (get) Token: 0x06008926 RID: 35110 RVA: 0x001EFFC1 File Offset: 0x001EE1C1
			public override string Text
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x17002740 RID: 10048
			// (get) Token: 0x06008927 RID: 35111 RVA: 0x001EFFC8 File Offset: 0x001EE1C8
			public override object Value
			{
				get
				{
					if (base.Kind == SyntaxKind.IdentifierToken)
					{
						return string.Empty;
					}
					return null;
				}
			}

			// Token: 0x06008928 RID: 35112 RVA: 0x001EFFDE File Offset: 0x001EE1DE
			public override SyntaxToken TokenWithLeadingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.MissingTokenWithTrivia(base.Kind, trivia, this.TrailingField, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x06008929 RID: 35113 RVA: 0x001EFFFE File Offset: 0x001EE1FE
			public override SyntaxToken TokenWithTrailingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.MissingTokenWithTrivia(base.Kind, this.LeadingField, trivia, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x0600892A RID: 35114 RVA: 0x001F001E File Offset: 0x001EE21E
			internal override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
			{
				return new SyntaxToken.MissingTokenWithTrivia(base.Kind, this.LeadingField, this.TrailingField, diagnostics, base.GetAnnotations());
			}

			// Token: 0x0600892B RID: 35115 RVA: 0x001F003E File Offset: 0x001EE23E
			internal override GreenNode SetAnnotations(SyntaxAnnotation[] annotations)
			{
				return new SyntaxToken.MissingTokenWithTrivia(base.Kind, this.LeadingField, this.TrailingField, base.GetDiagnostics(), annotations);
			}
		}

		// Token: 0x020007B5 RID: 1973
		internal class SyntaxIdentifier : SyntaxToken
		{
			// Token: 0x0600892C RID: 35116 RVA: 0x001F005E File Offset: 0x001EE25E
			static SyntaxIdentifier()
			{
				ObjectBinder.RegisterTypeReader(typeof(SyntaxToken.SyntaxIdentifier), (ObjectReader r) => new SyntaxToken.SyntaxIdentifier(r));
			}

			// Token: 0x0600892D RID: 35117 RVA: 0x001F007F File Offset: 0x001EE27F
			internal SyntaxIdentifier(string text)
				: base(SyntaxKind.IdentifierToken, text.Length)
			{
				this.TextField = text;
			}

			// Token: 0x0600892E RID: 35118 RVA: 0x001F0099 File Offset: 0x001EE299
			internal SyntaxIdentifier(string text, DiagnosticInfo[] diagnostics, SyntaxAnnotation[] annotations)
				: base(SyntaxKind.IdentifierToken, text.Length, diagnostics, annotations)
			{
				this.TextField = text;
			}

			// Token: 0x0600892F RID: 35119 RVA: 0x001F00B5 File Offset: 0x001EE2B5
			internal SyntaxIdentifier(ObjectReader reader)
				: base(reader)
			{
				this.TextField = reader.ReadString();
				base.FullWidth = this.TextField.Length;
			}

			// Token: 0x06008930 RID: 35120 RVA: 0x001F00DB File Offset: 0x001EE2DB
			internal override void WriteTo(ObjectWriter writer)
			{
				base.WriteTo(writer);
				writer.WriteString(this.TextField);
			}

			// Token: 0x17002741 RID: 10049
			// (get) Token: 0x06008931 RID: 35121 RVA: 0x001F00F0 File Offset: 0x001EE2F0
			public override string Text
			{
				get
				{
					return this.TextField;
				}
			}

			// Token: 0x17002742 RID: 10050
			// (get) Token: 0x06008932 RID: 35122 RVA: 0x001F00F8 File Offset: 0x001EE2F8
			public override object Value
			{
				get
				{
					return this.TextField;
				}
			}

			// Token: 0x17002743 RID: 10051
			// (get) Token: 0x06008933 RID: 35123 RVA: 0x001F0100 File Offset: 0x001EE300
			public override string ValueText
			{
				get
				{
					return this.TextField;
				}
			}

			// Token: 0x06008934 RID: 35124 RVA: 0x001F0108 File Offset: 0x001EE308
			public override SyntaxToken TokenWithLeadingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.SyntaxIdentifierWithTrivia(base.Kind, this.TextField, this.TextField, trivia, null, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x06008935 RID: 35125 RVA: 0x001F012F File Offset: 0x001EE32F
			public override SyntaxToken TokenWithTrailingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.SyntaxIdentifierWithTrivia(base.Kind, this.TextField, this.TextField, null, trivia, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x06008936 RID: 35126 RVA: 0x001F0156 File Offset: 0x001EE356
			internal override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
			{
				return new SyntaxToken.SyntaxIdentifier(this.Text, diagnostics, base.GetAnnotations());
			}

			// Token: 0x06008937 RID: 35127 RVA: 0x001F016A File Offset: 0x001EE36A
			internal override GreenNode SetAnnotations(SyntaxAnnotation[] annotations)
			{
				return new SyntaxToken.SyntaxIdentifier(this.Text, base.GetDiagnostics(), annotations);
			}

			// Token: 0x040024DC RID: 9436
			protected readonly string TextField;
		}

		// Token: 0x020007B6 RID: 1974
		internal class SyntaxIdentifierExtended : SyntaxToken.SyntaxIdentifier
		{
			// Token: 0x06008938 RID: 35128 RVA: 0x001F017E File Offset: 0x001EE37E
			internal SyntaxIdentifierExtended(SyntaxKind contextualKind, string text, string valueText)
				: base(text)
			{
				this.contextualKind = contextualKind;
				this.valueText = valueText;
			}

			// Token: 0x06008939 RID: 35129 RVA: 0x001F0195 File Offset: 0x001EE395
			internal SyntaxIdentifierExtended(SyntaxKind contextualKind, string text, string valueText, DiagnosticInfo[] diagnostics, SyntaxAnnotation[] annotations)
				: base(text, diagnostics, annotations)
			{
				this.contextualKind = contextualKind;
				this.valueText = valueText;
			}

			// Token: 0x0600893A RID: 35130 RVA: 0x001F01B0 File Offset: 0x001EE3B0
			internal SyntaxIdentifierExtended(ObjectReader reader)
				: base(reader)
			{
				this.contextualKind = (SyntaxKind)reader.ReadInt16();
				this.valueText = reader.ReadString();
			}

			// Token: 0x0600893B RID: 35131 RVA: 0x001F01D2 File Offset: 0x001EE3D2
			static SyntaxIdentifierExtended()
			{
				ObjectBinder.RegisterTypeReader(typeof(SyntaxToken.SyntaxIdentifierExtended), (ObjectReader r) => new SyntaxToken.SyntaxIdentifierExtended(r));
			}

			// Token: 0x0600893C RID: 35132 RVA: 0x001F01F3 File Offset: 0x001EE3F3
			internal override void WriteTo(ObjectWriter writer)
			{
				base.WriteTo(writer);
				writer.WriteInt16((short)this.contextualKind);
				writer.WriteString(this.valueText);
			}

			// Token: 0x17002744 RID: 10052
			// (get) Token: 0x0600893D RID: 35133 RVA: 0x001F0215 File Offset: 0x001EE415
			public override SyntaxKind ContextualKind
			{
				get
				{
					return this.contextualKind;
				}
			}

			// Token: 0x17002745 RID: 10053
			// (get) Token: 0x0600893E RID: 35134 RVA: 0x001F021D File Offset: 0x001EE41D
			public override string ValueText
			{
				get
				{
					return this.valueText;
				}
			}

			// Token: 0x17002746 RID: 10054
			// (get) Token: 0x0600893F RID: 35135 RVA: 0x001F0225 File Offset: 0x001EE425
			public override object Value
			{
				get
				{
					return this.valueText;
				}
			}

			// Token: 0x06008940 RID: 35136 RVA: 0x001F022D File Offset: 0x001EE42D
			public override SyntaxToken TokenWithLeadingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.SyntaxIdentifierWithTrivia(this.contextualKind, this.TextField, this.valueText, trivia, null, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x06008941 RID: 35137 RVA: 0x001F0254 File Offset: 0x001EE454
			public override SyntaxToken TokenWithTrailingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.SyntaxIdentifierWithTrivia(this.contextualKind, this.TextField, this.valueText, null, trivia, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x06008942 RID: 35138 RVA: 0x001F027B File Offset: 0x001EE47B
			internal override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
			{
				return new SyntaxToken.SyntaxIdentifierExtended(this.contextualKind, this.TextField, this.valueText, diagnostics, base.GetAnnotations());
			}

			// Token: 0x06008943 RID: 35139 RVA: 0x001F029B File Offset: 0x001EE49B
			internal override GreenNode SetAnnotations(SyntaxAnnotation[] annotations)
			{
				return new SyntaxToken.SyntaxIdentifierExtended(this.contextualKind, this.TextField, this.valueText, base.GetDiagnostics(), annotations);
			}

			// Token: 0x040024DD RID: 9437
			protected readonly SyntaxKind contextualKind;

			// Token: 0x040024DE RID: 9438
			protected readonly string valueText;
		}

		// Token: 0x020007B7 RID: 1975
		internal class SyntaxIdentifierWithTrailingTrivia : SyntaxToken.SyntaxIdentifier
		{
			// Token: 0x06008944 RID: 35140 RVA: 0x001F02BB File Offset: 0x001EE4BB
			internal SyntaxIdentifierWithTrailingTrivia(string text, GreenNode trailing)
				: base(text)
			{
				if (trailing != null)
				{
					base.AdjustFlagsAndWidth(trailing);
					this._trailing = trailing;
				}
			}

			// Token: 0x06008945 RID: 35141 RVA: 0x001F02D5 File Offset: 0x001EE4D5
			internal SyntaxIdentifierWithTrailingTrivia(string text, GreenNode trailing, DiagnosticInfo[] diagnostics, SyntaxAnnotation[] annotations)
				: base(text, diagnostics, annotations)
			{
				if (trailing != null)
				{
					base.AdjustFlagsAndWidth(trailing);
					this._trailing = trailing;
				}
			}

			// Token: 0x06008946 RID: 35142 RVA: 0x001F02F4 File Offset: 0x001EE4F4
			internal SyntaxIdentifierWithTrailingTrivia(ObjectReader reader)
				: base(reader)
			{
				GreenNode greenNode = (GreenNode)reader.ReadValue();
				if (greenNode != null)
				{
					base.AdjustFlagsAndWidth(greenNode);
					this._trailing = greenNode;
				}
			}

			// Token: 0x06008947 RID: 35143 RVA: 0x001F0325 File Offset: 0x001EE525
			static SyntaxIdentifierWithTrailingTrivia()
			{
				ObjectBinder.RegisterTypeReader(typeof(SyntaxToken.SyntaxIdentifierWithTrailingTrivia), (ObjectReader r) => new SyntaxToken.SyntaxIdentifierWithTrailingTrivia(r));
			}

			// Token: 0x06008948 RID: 35144 RVA: 0x001F0346 File Offset: 0x001EE546
			internal override void WriteTo(ObjectWriter writer)
			{
				base.WriteTo(writer);
				writer.WriteValue(this._trailing);
			}

			// Token: 0x06008949 RID: 35145 RVA: 0x001F035B File Offset: 0x001EE55B
			public override GreenNode GetTrailingTrivia()
			{
				return this._trailing;
			}

			// Token: 0x0600894A RID: 35146 RVA: 0x001F0363 File Offset: 0x001EE563
			public override SyntaxToken TokenWithLeadingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.SyntaxIdentifierWithTrivia(base.Kind, this.TextField, this.TextField, trivia, this._trailing, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x0600894B RID: 35147 RVA: 0x001F038F File Offset: 0x001EE58F
			public override SyntaxToken TokenWithTrailingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.SyntaxIdentifierWithTrailingTrivia(this.TextField, trivia, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x0600894C RID: 35148 RVA: 0x001F03A9 File Offset: 0x001EE5A9
			internal override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
			{
				return new SyntaxToken.SyntaxIdentifierWithTrailingTrivia(this.TextField, this._trailing, diagnostics, base.GetAnnotations());
			}

			// Token: 0x0600894D RID: 35149 RVA: 0x001F03C3 File Offset: 0x001EE5C3
			internal override GreenNode SetAnnotations(SyntaxAnnotation[] annotations)
			{
				return new SyntaxToken.SyntaxIdentifierWithTrailingTrivia(this.TextField, this._trailing, base.GetDiagnostics(), annotations);
			}

			// Token: 0x040024DF RID: 9439
			private readonly GreenNode _trailing;
		}

		// Token: 0x020007B8 RID: 1976
		internal class SyntaxIdentifierWithTrivia : SyntaxToken.SyntaxIdentifierExtended
		{
			// Token: 0x0600894E RID: 35150 RVA: 0x001F03DD File Offset: 0x001EE5DD
			internal SyntaxIdentifierWithTrivia(SyntaxKind contextualKind, string text, string valueText, GreenNode leading, GreenNode trailing)
				: base(contextualKind, text, valueText)
			{
				if (leading != null)
				{
					base.AdjustFlagsAndWidth(leading);
					this._leading = leading;
				}
				if (trailing != null)
				{
					base.AdjustFlagsAndWidth(trailing);
					this._trailing = trailing;
				}
			}

			// Token: 0x0600894F RID: 35151 RVA: 0x001F0410 File Offset: 0x001EE610
			internal SyntaxIdentifierWithTrivia(SyntaxKind contextualKind, string text, string valueText, GreenNode leading, GreenNode trailing, DiagnosticInfo[] diagnostics, SyntaxAnnotation[] annotations)
				: base(contextualKind, text, valueText, diagnostics, annotations)
			{
				if (leading != null)
				{
					base.AdjustFlagsAndWidth(leading);
					this._leading = leading;
				}
				if (trailing != null)
				{
					base.AdjustFlagsAndWidth(trailing);
					this._trailing = trailing;
				}
			}

			// Token: 0x06008950 RID: 35152 RVA: 0x001F0448 File Offset: 0x001EE648
			internal SyntaxIdentifierWithTrivia(ObjectReader reader)
				: base(reader)
			{
				GreenNode greenNode = (GreenNode)reader.ReadValue();
				if (greenNode != null)
				{
					base.AdjustFlagsAndWidth(greenNode);
					this._leading = greenNode;
				}
				GreenNode greenNode2 = (GreenNode)reader.ReadValue();
				if (greenNode2 != null)
				{
					this._trailing = greenNode2;
					base.AdjustFlagsAndWidth(greenNode2);
				}
			}

			// Token: 0x06008951 RID: 35153 RVA: 0x001F0496 File Offset: 0x001EE696
			static SyntaxIdentifierWithTrivia()
			{
				ObjectBinder.RegisterTypeReader(typeof(SyntaxToken.SyntaxIdentifierWithTrivia), (ObjectReader r) => new SyntaxToken.SyntaxIdentifierWithTrivia(r));
			}

			// Token: 0x06008952 RID: 35154 RVA: 0x001F04B7 File Offset: 0x001EE6B7
			internal override void WriteTo(ObjectWriter writer)
			{
				base.WriteTo(writer);
				writer.WriteValue(this._leading);
				writer.WriteValue(this._trailing);
			}

			// Token: 0x06008953 RID: 35155 RVA: 0x001F04D8 File Offset: 0x001EE6D8
			public override GreenNode GetLeadingTrivia()
			{
				return this._leading;
			}

			// Token: 0x06008954 RID: 35156 RVA: 0x001F04E0 File Offset: 0x001EE6E0
			public override GreenNode GetTrailingTrivia()
			{
				return this._trailing;
			}

			// Token: 0x06008955 RID: 35157 RVA: 0x001F04E8 File Offset: 0x001EE6E8
			public override SyntaxToken TokenWithLeadingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.SyntaxIdentifierWithTrivia(this.contextualKind, this.TextField, this.valueText, trivia, this._trailing, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x06008956 RID: 35158 RVA: 0x001F0514 File Offset: 0x001EE714
			public override SyntaxToken TokenWithTrailingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.SyntaxIdentifierWithTrivia(this.contextualKind, this.TextField, this.valueText, this._leading, trivia, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x06008957 RID: 35159 RVA: 0x001F0540 File Offset: 0x001EE740
			internal override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
			{
				return new SyntaxToken.SyntaxIdentifierWithTrivia(this.contextualKind, this.TextField, this.valueText, this._leading, this._trailing, diagnostics, base.GetAnnotations());
			}

			// Token: 0x06008958 RID: 35160 RVA: 0x001F056C File Offset: 0x001EE76C
			internal override GreenNode SetAnnotations(SyntaxAnnotation[] annotations)
			{
				return new SyntaxToken.SyntaxIdentifierWithTrivia(this.contextualKind, this.TextField, this.valueText, this._leading, this._trailing, base.GetDiagnostics(), annotations);
			}

			// Token: 0x040024E0 RID: 9440
			private readonly GreenNode _leading;

			// Token: 0x040024E1 RID: 9441
			private readonly GreenNode _trailing;
		}

		// Token: 0x020007B9 RID: 1977
		internal class SyntaxTokenWithValue<T> : SyntaxToken
		{
			// Token: 0x06008959 RID: 35161 RVA: 0x001F0598 File Offset: 0x001EE798
			internal SyntaxTokenWithValue(SyntaxKind kind, string text, T value)
				: base(kind, text.Length)
			{
				this.TextField = text;
				this.ValueField = value;
			}

			// Token: 0x0600895A RID: 35162 RVA: 0x001F05B5 File Offset: 0x001EE7B5
			internal SyntaxTokenWithValue(SyntaxKind kind, string text, T value, DiagnosticInfo[] diagnostics, SyntaxAnnotation[] annotations)
				: base(kind, text.Length, diagnostics, annotations)
			{
				this.TextField = text;
				this.ValueField = value;
			}

			// Token: 0x0600895B RID: 35163 RVA: 0x001F05D6 File Offset: 0x001EE7D6
			internal SyntaxTokenWithValue(ObjectReader reader)
				: base(reader)
			{
				this.TextField = reader.ReadString();
				base.FullWidth = this.TextField.Length;
				this.ValueField = (T)((object)reader.ReadValue());
			}

			// Token: 0x0600895C RID: 35164 RVA: 0x001F060D File Offset: 0x001EE80D
			static SyntaxTokenWithValue()
			{
				ObjectBinder.RegisterTypeReader(typeof(SyntaxToken.SyntaxTokenWithValue<T>), (ObjectReader r) => new SyntaxToken.SyntaxTokenWithValue<T>(r));
			}

			// Token: 0x0600895D RID: 35165 RVA: 0x001F062E File Offset: 0x001EE82E
			internal override void WriteTo(ObjectWriter writer)
			{
				base.WriteTo(writer);
				writer.WriteString(this.TextField);
				writer.WriteValue(this.ValueField);
			}

			// Token: 0x17002747 RID: 10055
			// (get) Token: 0x0600895E RID: 35166 RVA: 0x001F0654 File Offset: 0x001EE854
			public override string Text
			{
				get
				{
					return this.TextField;
				}
			}

			// Token: 0x17002748 RID: 10056
			// (get) Token: 0x0600895F RID: 35167 RVA: 0x001F065C File Offset: 0x001EE85C
			public override object Value
			{
				get
				{
					return this.ValueField;
				}
			}

			// Token: 0x17002749 RID: 10057
			// (get) Token: 0x06008960 RID: 35168 RVA: 0x001F0669 File Offset: 0x001EE869
			public override string ValueText
			{
				get
				{
					return Convert.ToString(this.ValueField, CultureInfo.InvariantCulture);
				}
			}

			// Token: 0x06008961 RID: 35169 RVA: 0x001F0680 File Offset: 0x001EE880
			public override SyntaxToken TokenWithLeadingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.SyntaxTokenWithValueAndTrivia<T>(base.Kind, this.TextField, this.ValueField, trivia, null, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x06008962 RID: 35170 RVA: 0x001F06A7 File Offset: 0x001EE8A7
			public override SyntaxToken TokenWithTrailingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.SyntaxTokenWithValueAndTrivia<T>(base.Kind, this.TextField, this.ValueField, null, trivia, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x06008963 RID: 35171 RVA: 0x001F06CE File Offset: 0x001EE8CE
			internal override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
			{
				return new SyntaxToken.SyntaxTokenWithValue<T>(base.Kind, this.TextField, this.ValueField, diagnostics, base.GetAnnotations());
			}

			// Token: 0x06008964 RID: 35172 RVA: 0x001F06EE File Offset: 0x001EE8EE
			internal override GreenNode SetAnnotations(SyntaxAnnotation[] annotations)
			{
				return new SyntaxToken.SyntaxTokenWithValue<T>(base.Kind, this.TextField, this.ValueField, base.GetDiagnostics(), annotations);
			}

			// Token: 0x040024E2 RID: 9442
			protected readonly string TextField;

			// Token: 0x040024E3 RID: 9443
			protected readonly T ValueField;
		}

		// Token: 0x020007BA RID: 1978
		internal class SyntaxTokenWithValueAndTrivia<T> : SyntaxToken.SyntaxTokenWithValue<T>
		{
			// Token: 0x06008965 RID: 35173 RVA: 0x001F070E File Offset: 0x001EE90E
			static SyntaxTokenWithValueAndTrivia()
			{
				ObjectBinder.RegisterTypeReader(typeof(SyntaxToken.SyntaxTokenWithValueAndTrivia<T>), (ObjectReader r) => new SyntaxToken.SyntaxTokenWithValueAndTrivia<T>(r));
			}

			// Token: 0x06008966 RID: 35174 RVA: 0x001F072F File Offset: 0x001EE92F
			internal SyntaxTokenWithValueAndTrivia(SyntaxKind kind, string text, T value, GreenNode leading, GreenNode trailing)
				: base(kind, text, value)
			{
				if (leading != null)
				{
					base.AdjustFlagsAndWidth(leading);
					this._leading = leading;
				}
				if (trailing != null)
				{
					base.AdjustFlagsAndWidth(trailing);
					this._trailing = trailing;
				}
			}

			// Token: 0x06008967 RID: 35175 RVA: 0x001F0762 File Offset: 0x001EE962
			internal SyntaxTokenWithValueAndTrivia(SyntaxKind kind, string text, T value, GreenNode leading, GreenNode trailing, DiagnosticInfo[] diagnostics, SyntaxAnnotation[] annotations)
				: base(kind, text, value, diagnostics, annotations)
			{
				if (leading != null)
				{
					base.AdjustFlagsAndWidth(leading);
					this._leading = leading;
				}
				if (trailing != null)
				{
					base.AdjustFlagsAndWidth(trailing);
					this._trailing = trailing;
				}
			}

			// Token: 0x06008968 RID: 35176 RVA: 0x001F079C File Offset: 0x001EE99C
			internal SyntaxTokenWithValueAndTrivia(ObjectReader reader)
				: base(reader)
			{
				GreenNode greenNode = (GreenNode)reader.ReadValue();
				if (greenNode != null)
				{
					base.AdjustFlagsAndWidth(greenNode);
					this._leading = greenNode;
				}
				GreenNode greenNode2 = (GreenNode)reader.ReadValue();
				if (greenNode2 != null)
				{
					base.AdjustFlagsAndWidth(greenNode2);
					this._trailing = greenNode2;
				}
			}

			// Token: 0x06008969 RID: 35177 RVA: 0x001F07EA File Offset: 0x001EE9EA
			internal override void WriteTo(ObjectWriter writer)
			{
				base.WriteTo(writer);
				writer.WriteValue(this._leading);
				writer.WriteValue(this._trailing);
			}

			// Token: 0x0600896A RID: 35178 RVA: 0x001F080B File Offset: 0x001EEA0B
			public override GreenNode GetLeadingTrivia()
			{
				return this._leading;
			}

			// Token: 0x0600896B RID: 35179 RVA: 0x001F0813 File Offset: 0x001EEA13
			public override GreenNode GetTrailingTrivia()
			{
				return this._trailing;
			}

			// Token: 0x0600896C RID: 35180 RVA: 0x001F081B File Offset: 0x001EEA1B
			public override SyntaxToken TokenWithLeadingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.SyntaxTokenWithValueAndTrivia<T>(base.Kind, this.TextField, this.ValueField, trivia, this._trailing, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x0600896D RID: 35181 RVA: 0x001F0847 File Offset: 0x001EEA47
			public override SyntaxToken TokenWithTrailingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.SyntaxTokenWithValueAndTrivia<T>(base.Kind, this.TextField, this.ValueField, this._leading, trivia, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x0600896E RID: 35182 RVA: 0x001F0873 File Offset: 0x001EEA73
			internal override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
			{
				return new SyntaxToken.SyntaxTokenWithValueAndTrivia<T>(base.Kind, this.TextField, this.ValueField, this._leading, this._trailing, diagnostics, base.GetAnnotations());
			}

			// Token: 0x0600896F RID: 35183 RVA: 0x001F089F File Offset: 0x001EEA9F
			internal override GreenNode SetAnnotations(SyntaxAnnotation[] annotations)
			{
				return new SyntaxToken.SyntaxTokenWithValueAndTrivia<T>(base.Kind, this.TextField, this.ValueField, this._leading, this._trailing, base.GetDiagnostics(), annotations);
			}

			// Token: 0x040024E4 RID: 9444
			private readonly GreenNode _leading;

			// Token: 0x040024E5 RID: 9445
			private readonly GreenNode _trailing;
		}

		// Token: 0x020007BB RID: 1979
		internal class SyntaxTokenWithTrivia : SyntaxToken
		{
			// Token: 0x06008970 RID: 35184 RVA: 0x001F08CB File Offset: 0x001EEACB
			static SyntaxTokenWithTrivia()
			{
				ObjectBinder.RegisterTypeReader(typeof(SyntaxToken.SyntaxTokenWithTrivia), (ObjectReader r) => new SyntaxToken.SyntaxTokenWithTrivia(r));
			}

			// Token: 0x06008971 RID: 35185 RVA: 0x001F08EC File Offset: 0x001EEAEC
			internal SyntaxTokenWithTrivia(SyntaxKind kind, GreenNode leading, GreenNode trailing)
				: base(kind)
			{
				if (leading != null)
				{
					base.AdjustFlagsAndWidth(leading);
					this.LeadingField = leading;
				}
				if (trailing != null)
				{
					base.AdjustFlagsAndWidth(trailing);
					this.TrailingField = trailing;
				}
			}

			// Token: 0x06008972 RID: 35186 RVA: 0x001F0917 File Offset: 0x001EEB17
			internal SyntaxTokenWithTrivia(SyntaxKind kind, GreenNode leading, GreenNode trailing, DiagnosticInfo[] diagnostics, SyntaxAnnotation[] annotations)
				: base(kind, diagnostics, annotations)
			{
				if (leading != null)
				{
					base.AdjustFlagsAndWidth(leading);
					this.LeadingField = leading;
				}
				if (trailing != null)
				{
					base.AdjustFlagsAndWidth(trailing);
					this.TrailingField = trailing;
				}
			}

			// Token: 0x06008973 RID: 35187 RVA: 0x001F0948 File Offset: 0x001EEB48
			internal SyntaxTokenWithTrivia(ObjectReader reader)
				: base(reader)
			{
				GreenNode greenNode = (GreenNode)reader.ReadValue();
				if (greenNode != null)
				{
					base.AdjustFlagsAndWidth(greenNode);
					this.LeadingField = greenNode;
				}
				GreenNode greenNode2 = (GreenNode)reader.ReadValue();
				if (greenNode2 != null)
				{
					base.AdjustFlagsAndWidth(greenNode2);
					this.TrailingField = greenNode2;
				}
			}

			// Token: 0x06008974 RID: 35188 RVA: 0x001F0996 File Offset: 0x001EEB96
			internal override void WriteTo(ObjectWriter writer)
			{
				base.WriteTo(writer);
				writer.WriteValue(this.LeadingField);
				writer.WriteValue(this.TrailingField);
			}

			// Token: 0x06008975 RID: 35189 RVA: 0x001F09B7 File Offset: 0x001EEBB7
			public override GreenNode GetLeadingTrivia()
			{
				return this.LeadingField;
			}

			// Token: 0x06008976 RID: 35190 RVA: 0x001F09BF File Offset: 0x001EEBBF
			public override GreenNode GetTrailingTrivia()
			{
				return this.TrailingField;
			}

			// Token: 0x06008977 RID: 35191 RVA: 0x001F09C7 File Offset: 0x001EEBC7
			public override SyntaxToken TokenWithLeadingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.SyntaxTokenWithTrivia(base.Kind, trivia, this.TrailingField, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x06008978 RID: 35192 RVA: 0x001F09E7 File Offset: 0x001EEBE7
			public override SyntaxToken TokenWithTrailingTrivia(GreenNode trivia)
			{
				return new SyntaxToken.SyntaxTokenWithTrivia(base.Kind, this.LeadingField, trivia, base.GetDiagnostics(), base.GetAnnotations());
			}

			// Token: 0x06008979 RID: 35193 RVA: 0x001F0A07 File Offset: 0x001EEC07
			internal override GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
			{
				return new SyntaxToken.SyntaxTokenWithTrivia(base.Kind, this.LeadingField, this.TrailingField, diagnostics, base.GetAnnotations());
			}

			// Token: 0x0600897A RID: 35194 RVA: 0x001F0A27 File Offset: 0x001EEC27
			internal override GreenNode SetAnnotations(SyntaxAnnotation[] annotations)
			{
				return new SyntaxToken.SyntaxTokenWithTrivia(base.Kind, this.LeadingField, this.TrailingField, base.GetDiagnostics(), annotations);
			}

			// Token: 0x040024E6 RID: 9446
			protected readonly GreenNode LeadingField;

			// Token: 0x040024E7 RID: 9447
			protected readonly GreenNode TrailingField;
		}
	}
}
