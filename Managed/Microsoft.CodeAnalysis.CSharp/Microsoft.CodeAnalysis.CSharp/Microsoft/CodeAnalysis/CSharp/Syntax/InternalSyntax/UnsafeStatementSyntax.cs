using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000405 RID: 1029
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class UnsafeStatementSyntax : StatementSyntax
	{
		// Token: 0x060058D4 RID: 22740 RVA: 0x0015CAF0 File Offset: 0x0015ACF0
		internal UnsafeStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken unsafeKeyword, BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(unsafeKeyword);
			this.unsafeKeyword = unsafeKeyword;
			base.AdjustFlagsAndWidth(block);
			this.block = block;
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x0015CB40 File Offset: 0x0015AD40
		internal UnsafeStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken unsafeKeyword, BlockSyntax block, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(unsafeKeyword);
			this.unsafeKeyword = unsafeKeyword;
			base.AdjustFlagsAndWidth(block);
			this.block = block;
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x0015CB92 File Offset: 0x0015AD92
		internal UnsafeStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken unsafeKeyword, BlockSyntax block)
			: base(kind)
		{
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(unsafeKeyword);
			this.unsafeKeyword = unsafeKeyword;
			base.AdjustFlagsAndWidth(block);
			this.block = block;
		}

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x060058D7 RID: 22743 RVA: 0x0015CBD1 File Offset: 0x0015ADD1
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x060058D8 RID: 22744 RVA: 0x0015CBDE File Offset: 0x0015ADDE
		public SyntaxToken UnsafeKeyword
		{
			get
			{
				return this.unsafeKeyword;
			}
		}

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x060058D9 RID: 22745 RVA: 0x0015CBE6 File Offset: 0x0015ADE6
		public BlockSyntax Block
		{
			get
			{
				return this.block;
			}
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x0015CBF0 File Offset: 0x0015ADF0
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
				greenNode = this.unsafeKeyword;
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

		// Token: 0x060058DB RID: 22747 RVA: 0x0015CC2F File Offset: 0x0015AE2F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new UnsafeStatementSyntax(this, parent, position);
		}

		// Token: 0x060058DC RID: 22748 RVA: 0x0015CC39 File Offset: 0x0015AE39
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitUnsafeStatement(this);
		}

		// Token: 0x060058DD RID: 22749 RVA: 0x0015CC42 File Offset: 0x0015AE42
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitUnsafeStatement(this);
		}

		// Token: 0x060058DE RID: 22750 RVA: 0x0015CC4C File Offset: 0x0015AE4C
		public UnsafeStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken unsafeKeyword, BlockSyntax block)
		{
			if (attributeLists != this.AttributeLists || unsafeKeyword != this.UnsafeKeyword || block != this.Block)
			{
				UnsafeStatementSyntax unsafeStatementSyntax = SyntaxFactory.UnsafeStatement(attributeLists, unsafeKeyword, block);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					unsafeStatementSyntax = unsafeStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					unsafeStatementSyntax = unsafeStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return unsafeStatementSyntax;
			}
			return this;
		}

		// Token: 0x060058DF RID: 22751 RVA: 0x0015CCB1 File Offset: 0x0015AEB1
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new UnsafeStatementSyntax(base.Kind, this.attributeLists, this.unsafeKeyword, this.block, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060058E0 RID: 22752 RVA: 0x0015CCD7 File Offset: 0x0015AED7
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new UnsafeStatementSyntax(base.Kind, this.attributeLists, this.unsafeKeyword, this.block, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060058E1 RID: 22753 RVA: 0x0015CD00 File Offset: 0x0015AF00
		internal UnsafeStatementSyntax(ObjectReader reader)
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
			this.unsafeKeyword = syntaxToken;
			BlockSyntax blockSyntax = (BlockSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(blockSyntax);
			this.block = blockSyntax;
		}

		// Token: 0x060058E2 RID: 22754 RVA: 0x0015CD6C File Offset: 0x0015AF6C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.unsafeKeyword);
			writer.WriteValue(this.block);
		}

		// Token: 0x060058E3 RID: 22755 RVA: 0x0015CD99 File Offset: 0x0015AF99
		static UnsafeStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(UnsafeStatementSyntax), (ObjectReader r) => new UnsafeStatementSyntax(r));
		}

		// Token: 0x04001771 RID: 6001
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001772 RID: 6002
		internal readonly SyntaxToken unsafeKeyword;

		// Token: 0x04001773 RID: 6003
		internal readonly BlockSyntax block;
	}
}
