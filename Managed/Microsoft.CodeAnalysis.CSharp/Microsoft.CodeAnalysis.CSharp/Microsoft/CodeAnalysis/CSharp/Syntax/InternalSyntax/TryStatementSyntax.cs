using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000411 RID: 1041
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TryStatementSyntax : StatementSyntax
	{
		// Token: 0x06005996 RID: 22934 RVA: 0x0015F0F8 File Offset: 0x0015D2F8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal TryStatementSyntax(SyntaxKind kind, GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken tryKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] BlockSyntax block, GreenNode catches, FinallyClauseSyntax @finally, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(tryKeyword);
			this.tryKeyword = tryKeyword;
			base.AdjustFlagsAndWidth(block);
			this.block = block;
			if (catches != null)
			{
				base.AdjustFlagsAndWidth(catches);
				this.catches = catches;
			}
			if (@finally != null)
			{
				base.AdjustFlagsAndWidth(@finally);
				this.@finally = @finally;
			}
		}

		// Token: 0x06005997 RID: 22935 RVA: 0x0015F170 File Offset: 0x0015D370
		internal TryStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken tryKeyword, BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode catches, [global::System.Runtime.CompilerServices.Nullable(2)] FinallyClauseSyntax @finally, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(tryKeyword);
			this.tryKeyword = tryKeyword;
			base.AdjustFlagsAndWidth(block);
			this.block = block;
			if (catches != null)
			{
				base.AdjustFlagsAndWidth(catches);
				this.catches = catches;
			}
			if (@finally != null)
			{
				base.AdjustFlagsAndWidth(@finally);
				this.@finally = @finally;
			}
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x0015F1EC File Offset: 0x0015D3EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal TryStatementSyntax(SyntaxKind kind, GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken tryKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] BlockSyntax block, GreenNode catches, FinallyClauseSyntax @finally)
			: base(kind)
		{
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(tryKeyword);
			this.tryKeyword = tryKeyword;
			base.AdjustFlagsAndWidth(block);
			this.block = block;
			if (catches != null)
			{
				base.AdjustFlagsAndWidth(catches);
				this.catches = catches;
			}
			if (@finally != null)
			{
				base.AdjustFlagsAndWidth(@finally);
				this.@finally = @finally;
			}
		}

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x06005999 RID: 22937 RVA: 0x0015F25E File Offset: 0x0015D45E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x0600599A RID: 22938 RVA: 0x0015F26B File Offset: 0x0015D46B
		public SyntaxToken TryKeyword
		{
			get
			{
				return this.tryKeyword;
			}
		}

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x0600599B RID: 22939 RVA: 0x0015F273 File Offset: 0x0015D473
		public BlockSyntax Block
		{
			get
			{
				return this.block;
			}
		}

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x0600599C RID: 22940 RVA: 0x0015F27B File Offset: 0x0015D47B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<CatchClauseSyntax> Catches
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<CatchClauseSyntax>(this.catches);
			}
		}

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x0600599D RID: 22941 RVA: 0x0015F288 File Offset: 0x0015D488
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public FinallyClauseSyntax Finally
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.@finally;
			}
		}

		// Token: 0x0600599E RID: 22942 RVA: 0x0015F290 File Offset: 0x0015D490
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
				greenNode = this.tryKeyword;
				break;
			case 2:
				greenNode = this.block;
				break;
			case 3:
				greenNode = this.catches;
				break;
			case 4:
				greenNode = this.@finally;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600599F RID: 22943 RVA: 0x0015F2E9 File Offset: 0x0015D4E9
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new TryStatementSyntax(this, parent, position);
		}

		// Token: 0x060059A0 RID: 22944 RVA: 0x0015F2F3 File Offset: 0x0015D4F3
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTryStatement(this);
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x0015F2FC File Offset: 0x0015D4FC
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTryStatement(this);
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x0015F308 File Offset: 0x0015D508
		public TryStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken tryKeyword, BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<CatchClauseSyntax> catches, FinallyClauseSyntax @finally)
		{
			if (attributeLists != this.AttributeLists || tryKeyword != this.TryKeyword || block != this.Block || catches != this.Catches || @finally != this.Finally)
			{
				TryStatementSyntax tryStatementSyntax = SyntaxFactory.TryStatement(attributeLists, tryKeyword, block, catches, @finally);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					tryStatementSyntax = tryStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					tryStatementSyntax = tryStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return tryStatementSyntax;
			}
			return this;
		}

		// Token: 0x060059A3 RID: 22947 RVA: 0x0015F38A File Offset: 0x0015D58A
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new TryStatementSyntax(base.Kind, this.attributeLists, this.tryKeyword, this.block, this.catches, this.@finally, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060059A4 RID: 22948 RVA: 0x0015F3BC File Offset: 0x0015D5BC
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new TryStatementSyntax(base.Kind, this.attributeLists, this.tryKeyword, this.block, this.catches, this.@finally, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060059A5 RID: 22949 RVA: 0x0015F3F0 File Offset: 0x0015D5F0
		internal TryStatementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 5;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.tryKeyword = syntaxToken;
			BlockSyntax blockSyntax = (BlockSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(blockSyntax);
			this.block = blockSyntax;
			GreenNode greenNode2 = (GreenNode)reader.ReadValue();
			if (greenNode2 != null)
			{
				base.AdjustFlagsAndWidth(greenNode2);
				this.catches = greenNode2;
			}
			FinallyClauseSyntax finallyClauseSyntax = (FinallyClauseSyntax)reader.ReadValue();
			if (finallyClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(finallyClauseSyntax);
				this.@finally = finallyClauseSyntax;
			}
		}

		// Token: 0x060059A6 RID: 22950 RVA: 0x0015F49C File Offset: 0x0015D69C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.tryKeyword);
			writer.WriteValue(this.block);
			writer.WriteValue(this.catches);
			writer.WriteValue(this.@finally);
		}

		// Token: 0x060059A7 RID: 22951 RVA: 0x0015F4EC File Offset: 0x0015D6EC
		static TryStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(TryStatementSyntax), (ObjectReader r) => new TryStatementSyntax(r));
		}

		// Token: 0x0400179F RID: 6047
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x040017A0 RID: 6048
		internal readonly SyntaxToken tryKeyword;

		// Token: 0x040017A1 RID: 6049
		internal readonly BlockSyntax block;

		// Token: 0x040017A2 RID: 6050
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode catches;

		// Token: 0x040017A3 RID: 6051
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly FinallyClauseSyntax @finally;
	}
}
