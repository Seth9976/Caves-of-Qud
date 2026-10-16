using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200018B RID: 395
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundScope : BoundStatementList
	{
		// Token: 0x06001E7F RID: 7807 RVA: 0x000A0D69 File Offset: 0x0009EF69
		public BoundScope(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements, bool hasErrors = false)
			: base(BoundKind.Scope, syntax, statements, hasErrors || statements.HasErrors<BoundStatement>())
		{
			this.Locals = locals;
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x000A0D89 File Offset: 0x0009EF89
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> Locals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x000A0D91 File Offset: 0x0009EF91
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitScope(this);
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x000A0D9A File Offset: 0x0009EF9A
		public BoundScope Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements)
		{
			if (locals != this.Locals || statements != base.Statements)
			{
				BoundScope boundScope = new BoundScope(this.Syntax, locals, statements, base.HasErrors);
				boundScope.CopyAttributes(this);
				return boundScope;
			}
			return this;
		}
	}
}
