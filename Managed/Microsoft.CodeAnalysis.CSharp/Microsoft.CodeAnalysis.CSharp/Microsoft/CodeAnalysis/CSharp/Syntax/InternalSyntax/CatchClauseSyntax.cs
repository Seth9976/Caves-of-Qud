using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000412 RID: 1042
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CatchClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x060059A8 RID: 22952 RVA: 0x0015F510 File Offset: 0x0015D710
		internal CatchClauseSyntax(SyntaxKind kind, SyntaxToken catchKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] CatchDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] CatchFilterClauseSyntax filter, BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(catchKeyword);
			this.catchKeyword = catchKeyword;
			if (declaration != null)
			{
				base.AdjustFlagsAndWidth(declaration);
				this.declaration = declaration;
			}
			if (filter != null)
			{
				base.AdjustFlagsAndWidth(filter);
				this.filter = filter;
			}
			base.AdjustFlagsAndWidth(block);
			this.block = block;
		}

		// Token: 0x060059A9 RID: 22953 RVA: 0x0015F574 File Offset: 0x0015D774
		internal CatchClauseSyntax(SyntaxKind kind, SyntaxToken catchKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] CatchDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] CatchFilterClauseSyntax filter, BlockSyntax block, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(catchKeyword);
			this.catchKeyword = catchKeyword;
			if (declaration != null)
			{
				base.AdjustFlagsAndWidth(declaration);
				this.declaration = declaration;
			}
			if (filter != null)
			{
				base.AdjustFlagsAndWidth(filter);
				this.filter = filter;
			}
			base.AdjustFlagsAndWidth(block);
			this.block = block;
		}

		// Token: 0x060059AA RID: 22954 RVA: 0x0015F5DC File Offset: 0x0015D7DC
		internal CatchClauseSyntax(SyntaxKind kind, SyntaxToken catchKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] CatchDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] CatchFilterClauseSyntax filter, BlockSyntax block)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(catchKeyword);
			this.catchKeyword = catchKeyword;
			if (declaration != null)
			{
				base.AdjustFlagsAndWidth(declaration);
				this.declaration = declaration;
			}
			if (filter != null)
			{
				base.AdjustFlagsAndWidth(filter);
				this.filter = filter;
			}
			base.AdjustFlagsAndWidth(block);
			this.block = block;
		}

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x060059AB RID: 22955 RVA: 0x0015F63A File Offset: 0x0015D83A
		public SyntaxToken CatchKeyword
		{
			get
			{
				return this.catchKeyword;
			}
		}

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x060059AC RID: 22956 RVA: 0x0015F642 File Offset: 0x0015D842
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public CatchDeclarationSyntax Declaration
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.declaration;
			}
		}

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x060059AD RID: 22957 RVA: 0x0015F64A File Offset: 0x0015D84A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public CatchFilterClauseSyntax Filter
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.filter;
			}
		}

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x060059AE RID: 22958 RVA: 0x0015F652 File Offset: 0x0015D852
		public BlockSyntax Block
		{
			get
			{
				return this.block;
			}
		}

		// Token: 0x060059AF RID: 22959 RVA: 0x0015F65C File Offset: 0x0015D85C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.catchKeyword;
				break;
			case 1:
				greenNode = this.declaration;
				break;
			case 2:
				greenNode = this.filter;
				break;
			case 3:
				greenNode = this.block;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x0015F6A8 File Offset: 0x0015D8A8
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new CatchClauseSyntax(this, parent, position);
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x0015F6B2 File Offset: 0x0015D8B2
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCatchClause(this);
		}

		// Token: 0x060059B2 RID: 22962 RVA: 0x0015F6BB File Offset: 0x0015D8BB
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCatchClause(this);
		}

		// Token: 0x060059B3 RID: 22963 RVA: 0x0015F6C4 File Offset: 0x0015D8C4
		public CatchClauseSyntax Update(SyntaxToken catchKeyword, CatchDeclarationSyntax declaration, CatchFilterClauseSyntax filter, BlockSyntax block)
		{
			if (catchKeyword != this.CatchKeyword || declaration != this.Declaration || filter != this.Filter || block != this.Block)
			{
				CatchClauseSyntax catchClauseSyntax = SyntaxFactory.CatchClause(catchKeyword, declaration, filter, block);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					catchClauseSyntax = catchClauseSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					catchClauseSyntax = catchClauseSyntax.WithAnnotationsGreen(annotations);
				}
				return catchClauseSyntax;
			}
			return this;
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x0015F730 File Offset: 0x0015D930
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new CatchClauseSyntax(base.Kind, this.catchKeyword, this.declaration, this.filter, this.block, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x0015F75C File Offset: 0x0015D95C
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new CatchClauseSyntax(base.Kind, this.catchKeyword, this.declaration, this.filter, this.block, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060059B6 RID: 22966 RVA: 0x0015F788 File Offset: 0x0015D988
		internal CatchClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.catchKeyword = syntaxToken;
			CatchDeclarationSyntax catchDeclarationSyntax = (CatchDeclarationSyntax)reader.ReadValue();
			if (catchDeclarationSyntax != null)
			{
				base.AdjustFlagsAndWidth(catchDeclarationSyntax);
				this.declaration = catchDeclarationSyntax;
			}
			CatchFilterClauseSyntax catchFilterClauseSyntax = (CatchFilterClauseSyntax)reader.ReadValue();
			if (catchFilterClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(catchFilterClauseSyntax);
				this.filter = catchFilterClauseSyntax;
			}
			BlockSyntax blockSyntax = (BlockSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(blockSyntax);
			this.block = blockSyntax;
		}

		// Token: 0x060059B7 RID: 22967 RVA: 0x0015F811 File Offset: 0x0015DA11
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.catchKeyword);
			writer.WriteValue(this.declaration);
			writer.WriteValue(this.filter);
			writer.WriteValue(this.block);
		}

		// Token: 0x060059B8 RID: 22968 RVA: 0x0015F84A File Offset: 0x0015DA4A
		static CatchClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(CatchClauseSyntax), (ObjectReader r) => new CatchClauseSyntax(r));
		}

		// Token: 0x040017A4 RID: 6052
		internal readonly SyntaxToken catchKeyword;

		// Token: 0x040017A5 RID: 6053
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly CatchDeclarationSyntax declaration;

		// Token: 0x040017A6 RID: 6054
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly CatchFilterClauseSyntax filter;

		// Token: 0x040017A7 RID: 6055
		internal readonly BlockSyntax block;
	}
}
