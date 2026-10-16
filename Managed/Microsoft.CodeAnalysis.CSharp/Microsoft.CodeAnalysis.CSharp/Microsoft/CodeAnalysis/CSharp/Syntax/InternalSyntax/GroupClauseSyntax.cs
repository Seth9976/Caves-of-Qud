using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003CD RID: 973
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class GroupClauseSyntax : SelectOrGroupClauseSyntax
	{
		// Token: 0x0600556C RID: 21868 RVA: 0x00152A8C File Offset: 0x00150C8C
		internal GroupClauseSyntax(SyntaxKind kind, SyntaxToken groupKeyword, ExpressionSyntax groupExpression, SyntaxToken byKeyword, ExpressionSyntax byExpression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(groupKeyword);
			this.groupKeyword = groupKeyword;
			base.AdjustFlagsAndWidth(groupExpression);
			this.groupExpression = groupExpression;
			base.AdjustFlagsAndWidth(byKeyword);
			this.byKeyword = byKeyword;
			base.AdjustFlagsAndWidth(byExpression);
			this.byExpression = byExpression;
		}

		// Token: 0x0600556D RID: 21869 RVA: 0x00152AE8 File Offset: 0x00150CE8
		internal GroupClauseSyntax(SyntaxKind kind, SyntaxToken groupKeyword, ExpressionSyntax groupExpression, SyntaxToken byKeyword, ExpressionSyntax byExpression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(groupKeyword);
			this.groupKeyword = groupKeyword;
			base.AdjustFlagsAndWidth(groupExpression);
			this.groupExpression = groupExpression;
			base.AdjustFlagsAndWidth(byKeyword);
			this.byKeyword = byKeyword;
			base.AdjustFlagsAndWidth(byExpression);
			this.byExpression = byExpression;
		}

		// Token: 0x0600556E RID: 21870 RVA: 0x00152B48 File Offset: 0x00150D48
		internal GroupClauseSyntax(SyntaxKind kind, SyntaxToken groupKeyword, ExpressionSyntax groupExpression, SyntaxToken byKeyword, ExpressionSyntax byExpression)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(groupKeyword);
			this.groupKeyword = groupKeyword;
			base.AdjustFlagsAndWidth(groupExpression);
			this.groupExpression = groupExpression;
			base.AdjustFlagsAndWidth(byKeyword);
			this.byKeyword = byKeyword;
			base.AdjustFlagsAndWidth(byExpression);
			this.byExpression = byExpression;
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x0600556F RID: 21871 RVA: 0x00152B9F File Offset: 0x00150D9F
		public SyntaxToken GroupKeyword
		{
			get
			{
				return this.groupKeyword;
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x06005570 RID: 21872 RVA: 0x00152BA7 File Offset: 0x00150DA7
		public ExpressionSyntax GroupExpression
		{
			get
			{
				return this.groupExpression;
			}
		}

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x06005571 RID: 21873 RVA: 0x00152BAF File Offset: 0x00150DAF
		public SyntaxToken ByKeyword
		{
			get
			{
				return this.byKeyword;
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06005572 RID: 21874 RVA: 0x00152BB7 File Offset: 0x00150DB7
		public ExpressionSyntax ByExpression
		{
			get
			{
				return this.byExpression;
			}
		}

		// Token: 0x06005573 RID: 21875 RVA: 0x00152BC0 File Offset: 0x00150DC0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.groupKeyword;
				break;
			case 1:
				greenNode = this.groupExpression;
				break;
			case 2:
				greenNode = this.byKeyword;
				break;
			case 3:
				greenNode = this.byExpression;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005574 RID: 21876 RVA: 0x00152C0C File Offset: 0x00150E0C
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new GroupClauseSyntax(this, parent, position);
		}

		// Token: 0x06005575 RID: 21877 RVA: 0x00152C16 File Offset: 0x00150E16
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitGroupClause(this);
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x00152C1F File Offset: 0x00150E1F
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitGroupClause(this);
		}

		// Token: 0x06005577 RID: 21879 RVA: 0x00152C28 File Offset: 0x00150E28
		public GroupClauseSyntax Update(SyntaxToken groupKeyword, ExpressionSyntax groupExpression, SyntaxToken byKeyword, ExpressionSyntax byExpression)
		{
			if (groupKeyword != this.GroupKeyword || groupExpression != this.GroupExpression || byKeyword != this.ByKeyword || byExpression != this.ByExpression)
			{
				GroupClauseSyntax groupClauseSyntax = SyntaxFactory.GroupClause(groupKeyword, groupExpression, byKeyword, byExpression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					groupClauseSyntax = groupClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					groupClauseSyntax = groupClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return groupClauseSyntax;
			}
			return this;
		}

		// Token: 0x06005578 RID: 21880 RVA: 0x00152C94 File Offset: 0x00150E94
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new GroupClauseSyntax(base.Kind, this.groupKeyword, this.groupExpression, this.byKeyword, this.byExpression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005579 RID: 21881 RVA: 0x00152CC0 File Offset: 0x00150EC0
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new GroupClauseSyntax(base.Kind, this.groupKeyword, this.groupExpression, this.byKeyword, this.byExpression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600557A RID: 21882 RVA: 0x00152CEC File Offset: 0x00150EEC
		internal GroupClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.groupKeyword = syntaxToken;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.groupExpression = expressionSyntax;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.byKeyword = syntaxToken2;
			ExpressionSyntax expressionSyntax2 = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax2);
			this.byExpression = expressionSyntax2;
		}

		// Token: 0x0600557B RID: 21883 RVA: 0x00152D6F File Offset: 0x00150F6F
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.groupKeyword);
			writer.WriteValue(this.groupExpression);
			writer.WriteValue(this.byKeyword);
			writer.WriteValue(this.byExpression);
		}

		// Token: 0x0600557C RID: 21884 RVA: 0x00152DA8 File Offset: 0x00150FA8
		static GroupClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(GroupClauseSyntax), (ObjectReader r) => new GroupClauseSyntax(r));
		}

		// Token: 0x040016B7 RID: 5815
		internal readonly SyntaxToken groupKeyword;

		// Token: 0x040016B8 RID: 5816
		internal readonly ExpressionSyntax groupExpression;

		// Token: 0x040016B9 RID: 5817
		internal readonly SyntaxToken byKeyword;

		// Token: 0x040016BA RID: 5818
		internal readonly ExpressionSyntax byExpression;
	}
}
