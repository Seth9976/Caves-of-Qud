using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200040F RID: 1039
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SwitchExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06005973 RID: 22899 RVA: 0x0015E9B0 File Offset: 0x0015CBB0
		internal SwitchExpressionSyntax(SyntaxKind kind, ExpressionSyntax governingExpression, SyntaxToken switchKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arms, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(governingExpression);
			this.governingExpression = governingExpression;
			base.AdjustFlagsAndWidth(switchKeyword);
			this.switchKeyword = switchKeyword;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (arms != null)
			{
				base.AdjustFlagsAndWidth(arms);
				this.arms = arms;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x06005974 RID: 22900 RVA: 0x0015EA20 File Offset: 0x0015CC20
		internal SwitchExpressionSyntax(SyntaxKind kind, ExpressionSyntax governingExpression, SyntaxToken switchKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arms, SyntaxToken closeBraceToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(governingExpression);
			this.governingExpression = governingExpression;
			base.AdjustFlagsAndWidth(switchKeyword);
			this.switchKeyword = switchKeyword;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (arms != null)
			{
				base.AdjustFlagsAndWidth(arms);
				this.arms = arms;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x06005975 RID: 22901 RVA: 0x0015EA94 File Offset: 0x0015CC94
		internal SwitchExpressionSyntax(SyntaxKind kind, ExpressionSyntax governingExpression, SyntaxToken switchKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arms, SyntaxToken closeBraceToken)
			: base(kind)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(governingExpression);
			this.governingExpression = governingExpression;
			base.AdjustFlagsAndWidth(switchKeyword);
			this.switchKeyword = switchKeyword;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (arms != null)
			{
				base.AdjustFlagsAndWidth(arms);
				this.arms = arms;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x06005976 RID: 22902 RVA: 0x0015EAFF File Offset: 0x0015CCFF
		public ExpressionSyntax GoverningExpression
		{
			get
			{
				return this.governingExpression;
			}
		}

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x06005977 RID: 22903 RVA: 0x0015EB07 File Offset: 0x0015CD07
		public SyntaxToken SwitchKeyword
		{
			get
			{
				return this.switchKeyword;
			}
		}

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x06005978 RID: 22904 RVA: 0x0015EB0F File Offset: 0x0015CD0F
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return this.openBraceToken;
			}
		}

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x06005979 RID: 22905 RVA: 0x0015EB17 File Offset: 0x0015CD17
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<SwitchExpressionArmSyntax> Arms
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<SwitchExpressionArmSyntax>(new SyntaxList<CSharpSyntaxNode>(this.arms));
			}
		}

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x0600597A RID: 22906 RVA: 0x0015EB2E File Offset: 0x0015CD2E
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return this.closeBraceToken;
			}
		}

		// Token: 0x0600597B RID: 22907 RVA: 0x0015EB38 File Offset: 0x0015CD38
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.governingExpression;
				break;
			case 1:
				greenNode = this.switchKeyword;
				break;
			case 2:
				greenNode = this.openBraceToken;
				break;
			case 3:
				greenNode = this.arms;
				break;
			case 4:
				greenNode = this.closeBraceToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600597C RID: 22908 RVA: 0x0015EB91 File Offset: 0x0015CD91
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new SwitchExpressionSyntax(this, parent, position);
		}

		// Token: 0x0600597D RID: 22909 RVA: 0x0015EB9B File Offset: 0x0015CD9B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSwitchExpression(this);
		}

		// Token: 0x0600597E RID: 22910 RVA: 0x0015EBA4 File Offset: 0x0015CDA4
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSwitchExpression(this);
		}

		// Token: 0x0600597F RID: 22911 RVA: 0x0015EBB0 File Offset: 0x0015CDB0
		public SwitchExpressionSyntax Update(ExpressionSyntax governingExpression, SyntaxToken switchKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SwitchExpressionArmSyntax> arms, SyntaxToken closeBraceToken)
		{
			if (governingExpression == this.GoverningExpression && switchKeyword == this.SwitchKeyword && openBraceToken == this.OpenBraceToken)
			{
				SeparatedSyntaxList<SwitchExpressionArmSyntax> separatedSyntaxList = this.Arms;
				if (!((in arms) != (in separatedSyntaxList)) && closeBraceToken == this.CloseBraceToken)
				{
					return this;
				}
			}
			SwitchExpressionSyntax switchExpressionSyntax = SyntaxFactory.SwitchExpression(governingExpression, switchKeyword, openBraceToken, arms, closeBraceToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				switchExpressionSyntax = switchExpressionSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				switchExpressionSyntax = switchExpressionSyntax.WithAnnotationsGreen(annotations);
			}
			return switchExpressionSyntax;
		}

		// Token: 0x06005980 RID: 22912 RVA: 0x0015EC30 File Offset: 0x0015CE30
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new SwitchExpressionSyntax(base.Kind, this.governingExpression, this.switchKeyword, this.openBraceToken, this.arms, this.closeBraceToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005981 RID: 22913 RVA: 0x0015EC62 File Offset: 0x0015CE62
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new SwitchExpressionSyntax(base.Kind, this.governingExpression, this.switchKeyword, this.openBraceToken, this.arms, this.closeBraceToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005982 RID: 22914 RVA: 0x0015EC94 File Offset: 0x0015CE94
		internal SwitchExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 5;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.governingExpression = expressionSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.switchKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.openBraceToken = syntaxToken2;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.arms = greenNode;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.closeBraceToken = syntaxToken3;
		}

		// Token: 0x06005983 RID: 22915 RVA: 0x0015ED38 File Offset: 0x0015CF38
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.governingExpression);
			writer.WriteValue(this.switchKeyword);
			writer.WriteValue(this.openBraceToken);
			writer.WriteValue(this.arms);
			writer.WriteValue(this.closeBraceToken);
		}

		// Token: 0x06005984 RID: 22916 RVA: 0x0015ED88 File Offset: 0x0015CF88
		static SwitchExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(SwitchExpressionSyntax), (ObjectReader r) => new SwitchExpressionSyntax(r));
		}

		// Token: 0x04001796 RID: 6038
		internal readonly ExpressionSyntax governingExpression;

		// Token: 0x04001797 RID: 6039
		internal readonly SyntaxToken switchKeyword;

		// Token: 0x04001798 RID: 6040
		internal readonly SyntaxToken openBraceToken;

		// Token: 0x04001799 RID: 6041
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode arms;

		// Token: 0x0400179A RID: 6042
		internal readonly SyntaxToken closeBraceToken;
	}
}
