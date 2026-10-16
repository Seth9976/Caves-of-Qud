using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000415 RID: 1045
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class FinallyClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x060059DB RID: 23003 RVA: 0x0015FEF5 File Offset: 0x0015E0F5
		internal FinallyClauseSyntax(SyntaxKind kind, SyntaxToken finallyKeyword, BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(finallyKeyword);
			this.finallyKeyword = finallyKeyword;
			base.AdjustFlagsAndWidth(block);
			this.block = block;
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x0015FF25 File Offset: 0x0015E125
		internal FinallyClauseSyntax(SyntaxKind kind, SyntaxToken finallyKeyword, BlockSyntax block, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(finallyKeyword);
			this.finallyKeyword = finallyKeyword;
			base.AdjustFlagsAndWidth(block);
			this.block = block;
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x0015FF59 File Offset: 0x0015E159
		internal FinallyClauseSyntax(SyntaxKind kind, SyntaxToken finallyKeyword, BlockSyntax block)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(finallyKeyword);
			this.finallyKeyword = finallyKeyword;
			base.AdjustFlagsAndWidth(block);
			this.block = block;
		}

		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x060059DE RID: 23006 RVA: 0x0015FF85 File Offset: 0x0015E185
		public SyntaxToken FinallyKeyword
		{
			get
			{
				return this.finallyKeyword;
			}
		}

		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x060059DF RID: 23007 RVA: 0x0015FF8D File Offset: 0x0015E18D
		public BlockSyntax Block
		{
			get
			{
				return this.block;
			}
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x0015FF98 File Offset: 0x0015E198
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
					greenNode = this.block;
				}
			}
			else
			{
				greenNode = this.finallyKeyword;
			}
			return greenNode;
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x0015FFC3 File Offset: 0x0015E1C3
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new FinallyClauseSyntax(this, parent, position);
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x0015FFCD File Offset: 0x0015E1CD
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFinallyClause(this);
		}

		// Token: 0x060059E3 RID: 23011 RVA: 0x0015FFD6 File Offset: 0x0015E1D6
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFinallyClause(this);
		}

		// Token: 0x060059E4 RID: 23012 RVA: 0x0015FFE0 File Offset: 0x0015E1E0
		public FinallyClauseSyntax Update(SyntaxToken finallyKeyword, BlockSyntax block)
		{
			if (finallyKeyword != this.FinallyKeyword || block != this.Block)
			{
				FinallyClauseSyntax finallyClauseSyntax = SyntaxFactory.FinallyClause(finallyKeyword, block);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					finallyClauseSyntax = finallyClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					finallyClauseSyntax = finallyClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return finallyClauseSyntax;
			}
			return this;
		}

		// Token: 0x060059E5 RID: 23013 RVA: 0x00160036 File Offset: 0x0015E236
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new FinallyClauseSyntax(base.Kind, this.finallyKeyword, this.block, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060059E6 RID: 23014 RVA: 0x00160056 File Offset: 0x0015E256
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new FinallyClauseSyntax(base.Kind, this.finallyKeyword, this.block, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060059E7 RID: 23015 RVA: 0x00160078 File Offset: 0x0015E278
		internal FinallyClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.finallyKeyword = syntaxToken;
			BlockSyntax blockSyntax = (BlockSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(blockSyntax);
			this.block = blockSyntax;
		}

		// Token: 0x060059E8 RID: 23016 RVA: 0x001600C7 File Offset: 0x0015E2C7
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.finallyKeyword);
			writer.WriteValue(this.block);
		}

		// Token: 0x060059E9 RID: 23017 RVA: 0x001600E8 File Offset: 0x0015E2E8
		static FinallyClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(FinallyClauseSyntax), (ObjectReader r) => new FinallyClauseSyntax(r));
		}

		// Token: 0x040017B0 RID: 6064
		internal readonly SyntaxToken finallyKeyword;

		// Token: 0x040017B1 RID: 6065
		internal readonly BlockSyntax block;
	}
}
