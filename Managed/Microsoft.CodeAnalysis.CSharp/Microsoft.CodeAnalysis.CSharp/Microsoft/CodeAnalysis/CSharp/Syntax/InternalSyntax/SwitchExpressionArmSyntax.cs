using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000410 RID: 1040
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SwitchExpressionArmSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005985 RID: 22917 RVA: 0x0015EDAC File Offset: 0x0015CFAC
		internal SwitchExpressionArmSyntax(SyntaxKind kind, PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] WhenClauseSyntax whenClause, SyntaxToken equalsGreaterThanToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
			if (whenClause != null)
			{
				base.AdjustFlagsAndWidth(whenClause);
				this.whenClause = whenClause;
			}
			base.AdjustFlagsAndWidth(equalsGreaterThanToken);
			this.equalsGreaterThanToken = equalsGreaterThanToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x06005986 RID: 22918 RVA: 0x0015EE0C File Offset: 0x0015D00C
		internal SwitchExpressionArmSyntax(SyntaxKind kind, PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] WhenClauseSyntax whenClause, SyntaxToken equalsGreaterThanToken, ExpressionSyntax expression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
			if (whenClause != null)
			{
				base.AdjustFlagsAndWidth(whenClause);
				this.whenClause = whenClause;
			}
			base.AdjustFlagsAndWidth(equalsGreaterThanToken);
			this.equalsGreaterThanToken = equalsGreaterThanToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x06005987 RID: 22919 RVA: 0x0015EE70 File Offset: 0x0015D070
		internal SwitchExpressionArmSyntax(SyntaxKind kind, PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] WhenClauseSyntax whenClause, SyntaxToken equalsGreaterThanToken, ExpressionSyntax expression)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(pattern);
			this.pattern = pattern;
			if (whenClause != null)
			{
				base.AdjustFlagsAndWidth(whenClause);
				this.whenClause = whenClause;
			}
			base.AdjustFlagsAndWidth(equalsGreaterThanToken);
			this.equalsGreaterThanToken = equalsGreaterThanToken;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x06005988 RID: 22920 RVA: 0x0015EECA File Offset: 0x0015D0CA
		public PatternSyntax Pattern
		{
			get
			{
				return this.pattern;
			}
		}

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x06005989 RID: 22921 RVA: 0x0015EED2 File Offset: 0x0015D0D2
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public WhenClauseSyntax WhenClause
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.whenClause;
			}
		}

		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x0600598A RID: 22922 RVA: 0x0015EEDA File Offset: 0x0015D0DA
		public SyntaxToken EqualsGreaterThanToken
		{
			get
			{
				return this.equalsGreaterThanToken;
			}
		}

		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x0600598B RID: 22923 RVA: 0x0015EEE2 File Offset: 0x0015D0E2
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x0600598C RID: 22924 RVA: 0x0015EEEC File Offset: 0x0015D0EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.pattern;
				break;
			case 1:
				greenNode = this.whenClause;
				break;
			case 2:
				greenNode = this.equalsGreaterThanToken;
				break;
			case 3:
				greenNode = this.expression;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600598D RID: 22925 RVA: 0x0015EF38 File Offset: 0x0015D138
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new SwitchExpressionArmSyntax(this, parent, position);
		}

		// Token: 0x0600598E RID: 22926 RVA: 0x0015EF42 File Offset: 0x0015D142
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSwitchExpressionArm(this);
		}

		// Token: 0x0600598F RID: 22927 RVA: 0x0015EF4B File Offset: 0x0015D14B
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSwitchExpressionArm(this);
		}

		// Token: 0x06005990 RID: 22928 RVA: 0x0015EF54 File Offset: 0x0015D154
		public SwitchExpressionArmSyntax Update(PatternSyntax pattern, WhenClauseSyntax whenClause, SyntaxToken equalsGreaterThanToken, ExpressionSyntax expression)
		{
			if (pattern != this.Pattern || whenClause != this.WhenClause || equalsGreaterThanToken != this.EqualsGreaterThanToken || expression != this.Expression)
			{
				SwitchExpressionArmSyntax switchExpressionArmSyntax = SyntaxFactory.SwitchExpressionArm(pattern, whenClause, equalsGreaterThanToken, expression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					switchExpressionArmSyntax = switchExpressionArmSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					switchExpressionArmSyntax = switchExpressionArmSyntax.WithAnnotationsGreen(annotations);
				}
				return switchExpressionArmSyntax;
			}
			return this;
		}

		// Token: 0x06005991 RID: 22929 RVA: 0x0015EFC0 File Offset: 0x0015D1C0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new SwitchExpressionArmSyntax(base.Kind, this.pattern, this.whenClause, this.equalsGreaterThanToken, this.expression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005992 RID: 22930 RVA: 0x0015EFEC File Offset: 0x0015D1EC
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new SwitchExpressionArmSyntax(base.Kind, this.pattern, this.whenClause, this.equalsGreaterThanToken, this.expression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005993 RID: 22931 RVA: 0x0015F018 File Offset: 0x0015D218
		internal SwitchExpressionArmSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			PatternSyntax patternSyntax = (PatternSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(patternSyntax);
			this.pattern = patternSyntax;
			WhenClauseSyntax whenClauseSyntax = (WhenClauseSyntax)reader.ReadValue();
			if (whenClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(whenClauseSyntax);
				this.whenClause = whenClauseSyntax;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.equalsGreaterThanToken = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
		}

		// Token: 0x06005994 RID: 22932 RVA: 0x0015F09E File Offset: 0x0015D29E
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.pattern);
			writer.WriteValue(this.whenClause);
			writer.WriteValue(this.equalsGreaterThanToken);
			writer.WriteValue(this.expression);
		}

		// Token: 0x06005995 RID: 22933 RVA: 0x0015F0D7 File Offset: 0x0015D2D7
		static SwitchExpressionArmSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(SwitchExpressionArmSyntax), (ObjectReader r) => new SwitchExpressionArmSyntax(r));
		}

		// Token: 0x0400179B RID: 6043
		internal readonly PatternSyntax pattern;

		// Token: 0x0400179C RID: 6044
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly WhenClauseSyntax whenClause;

		// Token: 0x0400179D RID: 6045
		internal readonly SyntaxToken equalsGreaterThanToken;

		// Token: 0x0400179E RID: 6046
		internal readonly ExpressionSyntax expression;
	}
}
