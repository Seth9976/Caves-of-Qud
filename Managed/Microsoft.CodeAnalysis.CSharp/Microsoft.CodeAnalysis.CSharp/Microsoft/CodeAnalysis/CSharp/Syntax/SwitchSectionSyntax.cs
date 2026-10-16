using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002FA RID: 762
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class SwitchSectionSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004266 RID: 16998 RVA: 0x00110CEB File Offset: 0x0010EEEB
		internal SwitchSectionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06004267 RID: 16999 RVA: 0x00110CF6 File Offset: 0x0010EEF6
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<SwitchLabelSyntax> Labels
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SwitchLabelSyntax>(base.GetRed(ref this.labels, 0));
			}
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06004268 RID: 17000 RVA: 0x00110D0A File Offset: 0x0010EF0A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<StatementSyntax> Statements
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<StatementSyntax>(base.GetRed(ref this.statements, 1));
			}
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x00110D20 File Offset: 0x0010EF20
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed(ref this.statements, 1);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.labels);
			}
			return syntaxNode;
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x00110D58 File Offset: 0x0010EF58
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.statements;
				}
			}
			else
			{
				syntaxNode = this.labels;
			}
			return syntaxNode;
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x00110D83 File Offset: 0x0010EF83
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSwitchSection(this);
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x00110D8C File Offset: 0x0010EF8C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSwitchSection(this);
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x00110D98 File Offset: 0x0010EF98
		public SwitchSectionSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SwitchLabelSyntax> labels, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<StatementSyntax> statements)
		{
			if (!(labels != this.Labels) && !(statements != this.Statements))
			{
				return this;
			}
			SwitchSectionSyntax switchSectionSyntax = SyntaxFactory.SwitchSection(labels, statements);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return switchSectionSyntax;
			}
			return switchSectionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x00110DE2 File Offset: 0x0010EFE2
		public SwitchSectionSyntax WithLabels([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SwitchLabelSyntax> labels)
		{
			return this.Update(labels, this.Statements);
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x00110DF1 File Offset: 0x0010EFF1
		public SwitchSectionSyntax WithStatements([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<StatementSyntax> statements)
		{
			return this.Update(this.Labels, statements);
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x00110E00 File Offset: 0x0010F000
		public SwitchSectionSyntax AddLabels(params SwitchLabelSyntax[] items)
		{
			return this.WithLabels(this.Labels.AddRange(items));
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x00110E24 File Offset: 0x0010F024
		public SwitchSectionSyntax AddStatements(params StatementSyntax[] items)
		{
			return this.WithStatements(this.Statements.AddRange(items));
		}

		// Token: 0x040014C6 RID: 5318
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode labels;

		// Token: 0x040014C7 RID: 5319
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode statements;
	}
}
