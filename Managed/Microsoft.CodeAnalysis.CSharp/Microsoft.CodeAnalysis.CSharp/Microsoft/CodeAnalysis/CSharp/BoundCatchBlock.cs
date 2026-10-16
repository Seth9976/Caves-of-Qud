using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001A2 RID: 418
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundCatchBlock : BoundNode
	{
		// Token: 0x06001EFB RID: 7931 RVA: 0x000A1B2C File Offset: 0x0009FD2C
		public BoundCatchBlock([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression exceptionSourceOpt, TypeSymbol exceptionTypeOpt, BoundStatementList exceptionFilterPrologueOpt, BoundExpression exceptionFilterOpt, [global::System.Runtime.CompilerServices.Nullable(1)] BoundBlock body, bool isSynthesizedAsyncCatchAll, bool hasErrors = false)
			: base(BoundKind.CatchBlock, syntax, hasErrors || exceptionSourceOpt.HasErrors() || exceptionFilterPrologueOpt.HasErrors() || exceptionFilterOpt.HasErrors() || body.HasErrors())
		{
			this.Locals = locals;
			this.ExceptionSourceOpt = exceptionSourceOpt;
			this.ExceptionTypeOpt = exceptionTypeOpt;
			this.ExceptionFilterPrologueOpt = exceptionFilterPrologueOpt;
			this.ExceptionFilterOpt = exceptionFilterOpt;
			this.Body = body;
			this.IsSynthesizedAsyncCatchAll = isSynthesizedAsyncCatchAll;
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x000A1BA0 File Offset: 0x0009FDA0
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> Locals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x000A1BA8 File Offset: 0x0009FDA8
		public BoundExpression ExceptionSourceOpt { get; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x000A1BB0 File Offset: 0x0009FDB0
		public TypeSymbol ExceptionTypeOpt { get; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x000A1BB8 File Offset: 0x0009FDB8
		public BoundStatementList ExceptionFilterPrologueOpt { get; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x000A1BC0 File Offset: 0x0009FDC0
		public BoundExpression ExceptionFilterOpt { get; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x000A1BC8 File Offset: 0x0009FDC8
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundBlock Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x000A1BD0 File Offset: 0x0009FDD0
		public bool IsSynthesizedAsyncCatchAll { get; }

		// Token: 0x06001F03 RID: 7939 RVA: 0x000A1BD8 File Offset: 0x0009FDD8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitCatchBlock(this);
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x000A1BE4 File Offset: 0x0009FDE4
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundCatchBlock Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression exceptionSourceOpt, TypeSymbol exceptionTypeOpt, BoundStatementList exceptionFilterPrologueOpt, BoundExpression exceptionFilterOpt, [global::System.Runtime.CompilerServices.Nullable(1)] BoundBlock body, bool isSynthesizedAsyncCatchAll)
		{
			if (locals != this.Locals || exceptionSourceOpt != this.ExceptionSourceOpt || !TypeSymbol.Equals(exceptionTypeOpt, this.ExceptionTypeOpt, TypeCompareKind.ConsiderEverything) || exceptionFilterPrologueOpt != this.ExceptionFilterPrologueOpt || exceptionFilterOpt != this.ExceptionFilterOpt || body != this.Body || isSynthesizedAsyncCatchAll != this.IsSynthesizedAsyncCatchAll)
			{
				BoundCatchBlock boundCatchBlock = new BoundCatchBlock(this.Syntax, locals, exceptionSourceOpt, exceptionTypeOpt, exceptionFilterPrologueOpt, exceptionFilterOpt, body, isSynthesizedAsyncCatchAll, base.HasErrors);
				boundCatchBlock.CopyAttributes(this);
				return boundCatchBlock;
			}
			return this;
		}
	}
}
