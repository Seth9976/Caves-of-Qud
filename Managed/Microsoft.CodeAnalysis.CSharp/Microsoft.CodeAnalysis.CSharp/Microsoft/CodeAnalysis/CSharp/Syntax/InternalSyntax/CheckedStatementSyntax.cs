using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000404 RID: 1028
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CheckedStatementSyntax : StatementSyntax
	{
		// Token: 0x060058C4 RID: 22724 RVA: 0x0015C820 File Offset: 0x0015AA20
		internal CheckedStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken keyword, BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(block);
			this.block = block;
		}

		// Token: 0x060058C5 RID: 22725 RVA: 0x0015C870 File Offset: 0x0015AA70
		internal CheckedStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken keyword, BlockSyntax block, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(block);
			this.block = block;
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x0015C8C2 File Offset: 0x0015AAC2
		internal CheckedStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken keyword, BlockSyntax block)
			: base(kind)
		{
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(block);
			this.block = block;
		}

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x060058C7 RID: 22727 RVA: 0x0015C901 File Offset: 0x0015AB01
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x060058C8 RID: 22728 RVA: 0x0015C90E File Offset: 0x0015AB0E
		public SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x060058C9 RID: 22729 RVA: 0x0015C916 File Offset: 0x0015AB16
		public BlockSyntax Block
		{
			get
			{
				return this.block;
			}
		}

		// Token: 0x060058CA RID: 22730 RVA: 0x0015C920 File Offset: 0x0015AB20
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
				greenNode = this.keyword;
				break;
			case 2:
				greenNode = this.block;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060058CB RID: 22731 RVA: 0x0015C95F File Offset: 0x0015AB5F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new CheckedStatementSyntax(this, parent, position);
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x0015C969 File Offset: 0x0015AB69
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCheckedStatement(this);
		}

		// Token: 0x060058CD RID: 22733 RVA: 0x0015C972 File Offset: 0x0015AB72
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCheckedStatement(this);
		}

		// Token: 0x060058CE RID: 22734 RVA: 0x0015C97C File Offset: 0x0015AB7C
		public CheckedStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken keyword, BlockSyntax block)
		{
			if (attributeLists != this.AttributeLists || keyword != this.Keyword || block != this.Block)
			{
				CheckedStatementSyntax checkedStatementSyntax = SyntaxFactory.CheckedStatement(base.Kind, attributeLists, keyword, block);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					checkedStatementSyntax = checkedStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					checkedStatementSyntax = checkedStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return checkedStatementSyntax;
			}
			return this;
		}

		// Token: 0x060058CF RID: 22735 RVA: 0x0015C9E7 File Offset: 0x0015ABE7
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new CheckedStatementSyntax(base.Kind, this.attributeLists, this.keyword, this.block, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060058D0 RID: 22736 RVA: 0x0015CA0D File Offset: 0x0015AC0D
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new CheckedStatementSyntax(base.Kind, this.attributeLists, this.keyword, this.block, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x0015CA34 File Offset: 0x0015AC34
		internal CheckedStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.keyword = syntaxToken;
			BlockSyntax blockSyntax = (BlockSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(blockSyntax);
			this.block = blockSyntax;
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x0015CAA0 File Offset: 0x0015ACA0
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.block);
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x0015CACD File Offset: 0x0015ACCD
		static CheckedStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(CheckedStatementSyntax), (ObjectReader r) => new CheckedStatementSyntax(r));
		}

		// Token: 0x0400176E RID: 5998
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x0400176F RID: 5999
		internal readonly SyntaxToken keyword;

		// Token: 0x04001770 RID: 6000
		internal readonly BlockSyntax block;
	}
}
