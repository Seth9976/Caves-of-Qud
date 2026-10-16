using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000D0 RID: 208
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundTryStatement : BoundStatement
	{
		// Token: 0x0600112A RID: 4394 RVA: 0x0005DD0C File Offset: 0x0005BF0C
		public BoundTryStatement(SyntaxNode syntax, BoundBlock tryBlock, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundCatchBlock> catchBlocks, [global::System.Runtime.CompilerServices.Nullable(2)] BoundBlock finallyBlockOpt, [global::System.Runtime.CompilerServices.Nullable(2)] LabelSymbol finallyLabelOpt = null)
			: this(syntax, tryBlock, catchBlocks, finallyBlockOpt, finallyLabelOpt, false, false)
		{
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0005DD20 File Offset: 0x0005BF20
		public BoundTryStatement(SyntaxNode syntax, BoundBlock tryBlock, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundCatchBlock> catchBlocks, [global::System.Runtime.CompilerServices.Nullable(2)] BoundBlock finallyBlockOpt, [global::System.Runtime.CompilerServices.Nullable(2)] LabelSymbol finallyLabelOpt, bool preferFaultHandler, bool hasErrors = false)
			: base(BoundKind.TryStatement, syntax, hasErrors || tryBlock.HasErrors() || catchBlocks.HasErrors<BoundCatchBlock>() || finallyBlockOpt.HasErrors())
		{
			this.TryBlock = tryBlock;
			this.CatchBlocks = catchBlocks;
			this.FinallyBlockOpt = finallyBlockOpt;
			this.FinallyLabelOpt = finallyLabelOpt;
			this.PreferFaultHandler = preferFaultHandler;
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x0005DD7A File Offset: 0x0005BF7A
		public BoundBlock TryBlock { get; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x0005DD82 File Offset: 0x0005BF82
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundCatchBlock> CatchBlocks
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0005DD8A File Offset: 0x0005BF8A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundBlock FinallyBlockOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x0005DD92 File Offset: 0x0005BF92
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public LabelSymbol FinallyLabelOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0005DD9A File Offset: 0x0005BF9A
		public bool PreferFaultHandler { get; }

		// Token: 0x06001131 RID: 4401 RVA: 0x0005DDA2 File Offset: 0x0005BFA2
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitTryStatement(this);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0005DDAC File Offset: 0x0005BFAC
		public BoundTryStatement Update(BoundBlock tryBlock, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundCatchBlock> catchBlocks, [global::System.Runtime.CompilerServices.Nullable(2)] BoundBlock finallyBlockOpt, [global::System.Runtime.CompilerServices.Nullable(2)] LabelSymbol finallyLabelOpt, bool preferFaultHandler)
		{
			if (tryBlock != this.TryBlock || catchBlocks != this.CatchBlocks || finallyBlockOpt != this.FinallyBlockOpt || !SymbolEqualityComparer.ConsiderEverything.Equals(finallyLabelOpt, this.FinallyLabelOpt) || preferFaultHandler != this.PreferFaultHandler)
			{
				BoundTryStatement boundTryStatement = new BoundTryStatement(this.Syntax, tryBlock, catchBlocks, finallyBlockOpt, finallyLabelOpt, preferFaultHandler, base.HasErrors);
				boundTryStatement.CopyAttributes(this);
				return boundTryStatement;
			}
			return this;
		}
	}
}
