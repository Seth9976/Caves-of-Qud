using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001A0 RID: 416
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundUsingStatement : BoundStatement
	{
		// Token: 0x06001EEC RID: 7916 RVA: 0x000A1978 File Offset: 0x0009FB78
		public BoundUsingStatement([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundMultipleLocalDeclarations declarationsOpt, BoundExpression expressionOpt, Conversion iDisposableConversion, [global::System.Runtime.CompilerServices.Nullable(1)] BoundStatement body, BoundAwaitableInfo awaitOpt, MethodArgumentInfo patternDisposeInfoOpt, bool hasErrors = false)
			: base(BoundKind.UsingStatement, syntax, hasErrors || declarationsOpt.HasErrors() || expressionOpt.HasErrors() || body.HasErrors() || awaitOpt.HasErrors())
		{
			this.Locals = locals;
			this.DeclarationsOpt = declarationsOpt;
			this.ExpressionOpt = expressionOpt;
			this.IDisposableConversion = iDisposableConversion;
			this.Body = body;
			this.AwaitOpt = awaitOpt;
			this.PatternDisposeInfoOpt = patternDisposeInfoOpt;
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x000A19EC File Offset: 0x0009FBEC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> Locals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x000A19F4 File Offset: 0x0009FBF4
		public BoundMultipleLocalDeclarations DeclarationsOpt { get; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x000A19FC File Offset: 0x0009FBFC
		public BoundExpression ExpressionOpt { get; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x000A1A04 File Offset: 0x0009FC04
		public Conversion IDisposableConversion { get; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x000A1A0C File Offset: 0x0009FC0C
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundStatement Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x000A1A14 File Offset: 0x0009FC14
		public BoundAwaitableInfo AwaitOpt { get; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x000A1A1C File Offset: 0x0009FC1C
		public MethodArgumentInfo PatternDisposeInfoOpt { get; }

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000A1A24 File Offset: 0x0009FC24
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitUsingStatement(this);
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x000A1A30 File Offset: 0x0009FC30
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundUsingStatement Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundMultipleLocalDeclarations declarationsOpt, BoundExpression expressionOpt, Conversion iDisposableConversion, [global::System.Runtime.CompilerServices.Nullable(1)] BoundStatement body, BoundAwaitableInfo awaitOpt, MethodArgumentInfo patternDisposeInfoOpt)
		{
			if (locals != this.Locals || declarationsOpt != this.DeclarationsOpt || expressionOpt != this.ExpressionOpt || iDisposableConversion != this.IDisposableConversion || body != this.Body || awaitOpt != this.AwaitOpt || patternDisposeInfoOpt != this.PatternDisposeInfoOpt)
			{
				BoundUsingStatement boundUsingStatement = new BoundUsingStatement(this.Syntax, locals, declarationsOpt, expressionOpt, iDisposableConversion, body, awaitOpt, patternDisposeInfoOpt, base.HasErrors);
				boundUsingStatement.CopyAttributes(this);
				return boundUsingStatement;
			}
			return this;
		}
	}
}
