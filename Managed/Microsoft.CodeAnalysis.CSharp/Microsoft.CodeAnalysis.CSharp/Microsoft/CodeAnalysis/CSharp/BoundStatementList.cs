using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000CA RID: 202
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class BoundStatementList : BoundStatement
	{
		// Token: 0x060010FE RID: 4350 RVA: 0x0005D931 File Offset: 0x0005BB31
		public static BoundStatementList Synthesized(SyntaxNode syntax, params BoundStatement[] statements)
		{
			return BoundStatementList.Synthesized(syntax, false, statements.AsImmutableOrNull<BoundStatement>());
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0005D940 File Offset: 0x0005BB40
		public static BoundStatementList Synthesized(SyntaxNode syntax, bool hasErrors, params BoundStatement[] statements)
		{
			return BoundStatementList.Synthesized(syntax, hasErrors, statements.AsImmutableOrNull<BoundStatement>());
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0005D94F File Offset: 0x0005BB4F
		public static BoundStatementList Synthesized(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements)
		{
			return BoundStatementList.Synthesized(syntax, false, statements);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0005D959 File Offset: 0x0005BB59
		public static BoundStatementList Synthesized(SyntaxNode syntax, bool hasErrors, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements)
		{
			return new BoundStatementList(syntax, statements, hasErrors)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x0005D96A File Offset: 0x0005BB6A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				if (base.Kind != BoundKind.StatementList && base.Kind != BoundKind.Scope)
				{
					return ImmutableArray<BoundNode>.Empty;
				}
				return StaticCast<BoundNode>.From<BoundStatement>(this.Statements);
			}
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0005D991 File Offset: 0x0005BB91
		protected BoundStatementList(BoundKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements, bool hasErrors = false)
			: base(kind, syntax, hasErrors)
		{
			this.Statements = statements;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0005D9A4 File Offset: 0x0005BBA4
		public BoundStatementList(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements, bool hasErrors = false)
			: base(BoundKind.StatementList, syntax, hasErrors || statements.HasErrors<BoundStatement>())
		{
			this.Statements = statements;
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x0005D9C2 File Offset: 0x0005BBC2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundStatement> Statements
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0005D9CA File Offset: 0x0005BBCA
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitStatementList(this);
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0005D9D3 File Offset: 0x0005BBD3
		public BoundStatementList Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements)
		{
			if (statements != this.Statements)
			{
				BoundStatementList boundStatementList = new BoundStatementList(this.Syntax, statements, base.HasErrors);
				boundStatementList.CopyAttributes(this);
				return boundStatementList;
			}
			return this;
		}
	}
}
