using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200040A RID: 1034
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SwitchSectionSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600592F RID: 22831 RVA: 0x0015DF51 File Offset: 0x0015C151
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SwitchSectionSyntax(SyntaxKind kind, GreenNode labels, GreenNode statements, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			if (labels != null)
			{
				base.AdjustFlagsAndWidth(labels);
				this.labels = labels;
			}
			if (statements != null)
			{
				base.AdjustFlagsAndWidth(statements);
				this.statements = statements;
			}
		}

		// Token: 0x06005930 RID: 22832 RVA: 0x0015DF87 File Offset: 0x0015C187
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SwitchSectionSyntax(SyntaxKind kind, GreenNode labels, GreenNode statements, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			if (labels != null)
			{
				base.AdjustFlagsAndWidth(labels);
				this.labels = labels;
			}
			if (statements != null)
			{
				base.AdjustFlagsAndWidth(statements);
				this.statements = statements;
			}
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x0015DFC1 File Offset: 0x0015C1C1
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SwitchSectionSyntax(SyntaxKind kind, GreenNode labels, GreenNode statements)
			: base(kind)
		{
			base.SlotCount = 2;
			if (labels != null)
			{
				base.AdjustFlagsAndWidth(labels);
				this.labels = labels;
			}
			if (statements != null)
			{
				base.AdjustFlagsAndWidth(statements);
				this.statements = statements;
			}
		}

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x06005932 RID: 22834 RVA: 0x0015DFF3 File Offset: 0x0015C1F3
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<SwitchLabelSyntax> Labels
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SwitchLabelSyntax>(this.labels);
			}
		}

		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x06005933 RID: 22835 RVA: 0x0015E000 File Offset: 0x0015C200
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<StatementSyntax> Statements
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<StatementSyntax>(this.statements);
			}
		}

		// Token: 0x06005934 RID: 22836 RVA: 0x0015E010 File Offset: 0x0015C210
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			if (index != 0)
			{
				if (index != 1)
				{
					greenNode = null;
				}
				else
				{
					greenNode = this.statements;
				}
			}
			else
			{
				greenNode = this.labels;
			}
			return greenNode;
		}

		// Token: 0x06005935 RID: 22837 RVA: 0x0015E03B File Offset: 0x0015C23B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new SwitchSectionSyntax(this, parent, position);
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x0015E045 File Offset: 0x0015C245
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSwitchSection(this);
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x0015E04E File Offset: 0x0015C24E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSwitchSection(this);
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x0015E058 File Offset: 0x0015C258
		public SwitchSectionSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SwitchLabelSyntax> labels, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<StatementSyntax> statements)
		{
			if (labels != this.Labels || statements != this.Statements)
			{
				SwitchSectionSyntax switchSectionSyntax = SyntaxFactory.SwitchSection(labels, statements);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					switchSectionSyntax = switchSectionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					switchSectionSyntax = switchSectionSyntax.WithAnnotationsGreen(annotations);
				}
				return switchSectionSyntax;
			}
			return this;
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x0015E0B8 File Offset: 0x0015C2B8
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new SwitchSectionSyntax(base.Kind, this.labels, this.statements, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600593A RID: 22842 RVA: 0x0015E0D8 File Offset: 0x0015C2D8
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new SwitchSectionSyntax(base.Kind, this.labels, this.statements, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600593B RID: 22843 RVA: 0x0015E0F8 File Offset: 0x0015C2F8
		internal SwitchSectionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.labels = greenNode;
			}
			GreenNode greenNode2 = (GreenNode)reader.ReadValue();
			if (greenNode2 != null)
			{
				base.AdjustFlagsAndWidth(greenNode2);
				this.statements = greenNode2;
			}
		}

		// Token: 0x0600593C RID: 22844 RVA: 0x0015E14D File Offset: 0x0015C34D
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.labels);
			writer.WriteValue(this.statements);
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x0015E16E File Offset: 0x0015C36E
		static SwitchSectionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(SwitchSectionSyntax), (ObjectReader r) => new SwitchSectionSyntax(r));
		}

		// Token: 0x0400178B RID: 6027
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode labels;

		// Token: 0x0400178C RID: 6028
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode statements;
	}
}
