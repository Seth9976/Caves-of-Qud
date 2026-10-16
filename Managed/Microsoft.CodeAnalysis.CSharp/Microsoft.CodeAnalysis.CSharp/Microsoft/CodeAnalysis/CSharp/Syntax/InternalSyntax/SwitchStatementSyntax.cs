using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000409 RID: 1033
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SwitchStatementSyntax : StatementSyntax
	{
		// Token: 0x0600591A RID: 22810 RVA: 0x0015D984 File Offset: 0x0015BB84
		internal SwitchStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken switchKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken openParenToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken closeParenToken, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode sections, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 8;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(switchKeyword);
			this.switchKeyword = switchKeyword;
			if (openParenToken != null)
			{
				base.AdjustFlagsAndWidth(openParenToken);
				this.openParenToken = openParenToken;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			if (closeParenToken != null)
			{
				base.AdjustFlagsAndWidth(closeParenToken);
				this.closeParenToken = closeParenToken;
			}
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (sections != null)
			{
				base.AdjustFlagsAndWidth(sections);
				this.sections = sections;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x0600591B RID: 22811 RVA: 0x0015DA30 File Offset: 0x0015BC30
		internal SwitchStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken switchKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken openParenToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken closeParenToken, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode sections, SyntaxToken closeBraceToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 8;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(switchKeyword);
			this.switchKeyword = switchKeyword;
			if (openParenToken != null)
			{
				base.AdjustFlagsAndWidth(openParenToken);
				this.openParenToken = openParenToken;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			if (closeParenToken != null)
			{
				base.AdjustFlagsAndWidth(closeParenToken);
				this.closeParenToken = closeParenToken;
			}
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (sections != null)
			{
				base.AdjustFlagsAndWidth(sections);
				this.sections = sections;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x0600591C RID: 22812 RVA: 0x0015DAE0 File Offset: 0x0015BCE0
		internal SwitchStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken switchKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken openParenToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken closeParenToken, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode sections, SyntaxToken closeBraceToken)
			: base(kind)
		{
			base.SlotCount = 8;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(switchKeyword);
			this.switchKeyword = switchKeyword;
			if (openParenToken != null)
			{
				base.AdjustFlagsAndWidth(openParenToken);
				this.openParenToken = openParenToken;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			if (closeParenToken != null)
			{
				base.AdjustFlagsAndWidth(closeParenToken);
				this.closeParenToken = closeParenToken;
			}
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (sections != null)
			{
				base.AdjustFlagsAndWidth(sections);
				this.sections = sections;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x0600591D RID: 22813 RVA: 0x0015DB86 File Offset: 0x0015BD86
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x0600591E RID: 22814 RVA: 0x0015DB93 File Offset: 0x0015BD93
		public SyntaxToken SwitchKeyword
		{
			get
			{
				return this.switchKeyword;
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x0600591F RID: 22815 RVA: 0x0015DB9B File Offset: 0x0015BD9B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken OpenParenToken
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x06005920 RID: 22816 RVA: 0x0015DBA3 File Offset: 0x0015BDA3
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x06005921 RID: 22817 RVA: 0x0015DBAB File Offset: 0x0015BDAB
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken CloseParenToken
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x06005922 RID: 22818 RVA: 0x0015DBB3 File Offset: 0x0015BDB3
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return this.openBraceToken;
			}
		}

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x06005923 RID: 22819 RVA: 0x0015DBBB File Offset: 0x0015BDBB
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<SwitchSectionSyntax> Sections
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SwitchSectionSyntax>(this.sections);
			}
		}

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x06005924 RID: 22820 RVA: 0x0015DBC8 File Offset: 0x0015BDC8
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return this.closeBraceToken;
			}
		}

		// Token: 0x06005925 RID: 22821 RVA: 0x0015DBD0 File Offset: 0x0015BDD0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.attributeLists;
				break;
			case 1:
				greenNode = this.switchKeyword;
				break;
			case 2:
				greenNode = this.openParenToken;
				break;
			case 3:
				greenNode = this.expression;
				break;
			case 4:
				greenNode = this.closeParenToken;
				break;
			case 5:
				greenNode = this.openBraceToken;
				break;
			case 6:
				greenNode = this.sections;
				break;
			case 7:
				greenNode = this.closeBraceToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005926 RID: 22822 RVA: 0x0015DC50 File Offset: 0x0015BE50
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new SwitchStatementSyntax(this, parent, position);
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x0015DC5A File Offset: 0x0015BE5A
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSwitchStatement(this);
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x0015DC63 File Offset: 0x0015BE63
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSwitchStatement(this);
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x0015DC6C File Offset: 0x0015BE6C
		public SwitchStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken switchKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SwitchSectionSyntax> sections, SyntaxToken closeBraceToken)
		{
			if (attributeLists != this.AttributeLists || switchKeyword != this.SwitchKeyword || openParenToken != this.OpenParenToken || expression != this.Expression || closeParenToken != this.CloseParenToken || openBraceToken != this.OpenBraceToken || sections != this.Sections || closeBraceToken != this.CloseBraceToken)
			{
				SwitchStatementSyntax switchStatementSyntax = SyntaxFactory.SwitchStatement(attributeLists, switchKeyword, openParenToken, expression, closeParenToken, openBraceToken, sections, closeBraceToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					switchStatementSyntax = switchStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					switchStatementSyntax = switchStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return switchStatementSyntax;
			}
			return this;
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x0015DD14 File Offset: 0x0015BF14
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new SwitchStatementSyntax(base.Kind, this.attributeLists, this.switchKeyword, this.openParenToken, this.expression, this.closeParenToken, this.openBraceToken, this.sections, this.closeBraceToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x0015DD64 File Offset: 0x0015BF64
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new SwitchStatementSyntax(base.Kind, this.attributeLists, this.switchKeyword, this.openParenToken, this.expression, this.closeParenToken, this.openBraceToken, this.sections, this.closeBraceToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x0015DDB4 File Offset: 0x0015BFB4
		internal SwitchStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 8;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.switchKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken2 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken2);
				this.openParenToken = syntaxToken2;
			}
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken3 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken3);
				this.closeParenToken = syntaxToken3;
			}
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.openBraceToken = syntaxToken4;
			GreenNode greenNode2 = (GreenNode)reader.ReadValue();
			if (greenNode2 != null)
			{
				base.AdjustFlagsAndWidth(greenNode2);
				this.sections = greenNode2;
			}
			SyntaxToken syntaxToken5 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken5);
			this.closeBraceToken = syntaxToken5;
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x0015DEBC File Offset: 0x0015C0BC
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.switchKeyword);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.closeParenToken);
			writer.WriteValue(this.openBraceToken);
			writer.WriteValue(this.sections);
			writer.WriteValue(this.closeBraceToken);
		}

		// Token: 0x0600592E RID: 22830 RVA: 0x0015DF30 File Offset: 0x0015C130
		static SwitchStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(SwitchStatementSyntax), (ObjectReader r) => new SwitchStatementSyntax(r));
		}

		// Token: 0x04001783 RID: 6019
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001784 RID: 6020
		internal readonly SyntaxToken switchKeyword;

		// Token: 0x04001785 RID: 6021
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001786 RID: 6022
		internal readonly ExpressionSyntax expression;

		// Token: 0x04001787 RID: 6023
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken closeParenToken;

		// Token: 0x04001788 RID: 6024
		internal readonly SyntaxToken openBraceToken;

		// Token: 0x04001789 RID: 6025
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode sections;

		// Token: 0x0400178A RID: 6026
		internal readonly SyntaxToken closeBraceToken;
	}
}
