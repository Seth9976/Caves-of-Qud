using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200040C RID: 1036
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CasePatternSwitchLabelSyntax : SwitchLabelSyntax
	{
		// Token: 0x06005943 RID: 22851 RVA: 0x0015E1AC File Offset: 0x0015C3AC
		internal CasePatternSwitchLabelSyntax(SyntaxKind kind, SyntaxToken keyword, PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] WhenClauseSyntax whenClause, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
			if (whenClause != null)
			{
				base.AdjustFlagsAndWidth(whenClause);
				this.whenClause = whenClause;
			}
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x06005944 RID: 22852 RVA: 0x0015E20C File Offset: 0x0015C40C
		internal CasePatternSwitchLabelSyntax(SyntaxKind kind, SyntaxToken keyword, PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] WhenClauseSyntax whenClause, SyntaxToken colonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
			if (whenClause != null)
			{
				base.AdjustFlagsAndWidth(whenClause);
				this.whenClause = whenClause;
			}
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x06005945 RID: 22853 RVA: 0x0015E270 File Offset: 0x0015C470
		internal CasePatternSwitchLabelSyntax(SyntaxKind kind, SyntaxToken keyword, PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] WhenClauseSyntax whenClause, SyntaxToken colonToken)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
			if (whenClause != null)
			{
				base.AdjustFlagsAndWidth(whenClause);
				this.whenClause = whenClause;
			}
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x06005946 RID: 22854 RVA: 0x0015E2CB File Offset: 0x0015C4CB
		public override SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x06005947 RID: 22855 RVA: 0x0015E2D3 File Offset: 0x0015C4D3
		public PatternSyntax Pattern
		{
			get
			{
				return this.pattern;
			}
		}

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x06005948 RID: 22856 RVA: 0x0015E2DB File Offset: 0x0015C4DB
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public WhenClauseSyntax WhenClause
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.whenClause;
			}
		}

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x06005949 RID: 22857 RVA: 0x0015E2E3 File Offset: 0x0015C4E3
		public override SyntaxToken ColonToken
		{
			get
			{
				return this.colonToken;
			}
		}

		// Token: 0x0600594A RID: 22858 RVA: 0x0015E2EC File Offset: 0x0015C4EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.keyword;
				break;
			case 1:
				greenNode = this.pattern;
				break;
			case 2:
				greenNode = this.whenClause;
				break;
			case 3:
				greenNode = this.colonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600594B RID: 22859 RVA: 0x0015E338 File Offset: 0x0015C538
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new CasePatternSwitchLabelSyntax(this, parent, position);
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x0015E342 File Offset: 0x0015C542
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCasePatternSwitchLabel(this);
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x0015E34B File Offset: 0x0015C54B
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCasePatternSwitchLabel(this);
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x0015E354 File Offset: 0x0015C554
		public CasePatternSwitchLabelSyntax Update(SyntaxToken keyword, PatternSyntax pattern, WhenClauseSyntax whenClause, SyntaxToken colonToken)
		{
			if (keyword != this.Keyword || pattern != this.Pattern || whenClause != this.WhenClause || colonToken != this.ColonToken)
			{
				CasePatternSwitchLabelSyntax casePatternSwitchLabelSyntax = SyntaxFactory.CasePatternSwitchLabel(keyword, pattern, whenClause, colonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					casePatternSwitchLabelSyntax = casePatternSwitchLabelSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					casePatternSwitchLabelSyntax = casePatternSwitchLabelSyntax.WithAnnotationsGreen(annotations);
				}
				return casePatternSwitchLabelSyntax;
			}
			return this;
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x0015E3C0 File Offset: 0x0015C5C0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new CasePatternSwitchLabelSyntax(base.Kind, this.keyword, this.pattern, this.whenClause, this.colonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x0015E3EC File Offset: 0x0015C5EC
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new CasePatternSwitchLabelSyntax(base.Kind, this.keyword, this.pattern, this.whenClause, this.colonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x0015E418 File Offset: 0x0015C618
		internal CasePatternSwitchLabelSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.keyword = syntaxToken;
			PatternSyntax patternSyntax = (PatternSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(patternSyntax);
			this.pattern = patternSyntax;
			WhenClauseSyntax whenClauseSyntax = (WhenClauseSyntax)reader.ReadValue();
			if (whenClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(whenClauseSyntax);
				this.whenClause = whenClauseSyntax;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.colonToken = syntaxToken2;
		}

		// Token: 0x06005952 RID: 22866 RVA: 0x0015E49E File Offset: 0x0015C69E
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.pattern);
			writer.WriteValue(this.whenClause);
			writer.WriteValue(this.colonToken);
		}

		// Token: 0x06005953 RID: 22867 RVA: 0x0015E4D7 File Offset: 0x0015C6D7
		static CasePatternSwitchLabelSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(CasePatternSwitchLabelSyntax), (ObjectReader r) => new CasePatternSwitchLabelSyntax(r));
		}

		// Token: 0x0400178D RID: 6029
		internal readonly SyntaxToken keyword;

		// Token: 0x0400178E RID: 6030
		internal readonly PatternSyntax pattern;

		// Token: 0x0400178F RID: 6031
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly WhenClauseSyntax whenClause;

		// Token: 0x04001790 RID: 6032
		internal readonly SyntaxToken colonToken;
	}
}
