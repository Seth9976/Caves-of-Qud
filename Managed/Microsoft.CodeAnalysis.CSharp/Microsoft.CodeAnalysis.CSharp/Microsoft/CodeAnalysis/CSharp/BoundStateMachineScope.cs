using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200018C RID: 396
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundStateMachineScope : BoundStatement
	{
		// Token: 0x06001E83 RID: 7811 RVA: 0x000A0DD4 File Offset: 0x0009EFD4
		public BoundStateMachineScope(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<StateMachineFieldSymbol> fields, BoundStatement statement, bool hasErrors = false)
			: base(BoundKind.StateMachineScope, syntax, hasErrors || statement.HasErrors())
		{
			this.Fields = fields;
			this.Statement = statement;
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001E84 RID: 7812 RVA: 0x000A0DFA File Offset: 0x0009EFFA
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<StateMachineFieldSymbol> Fields
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x000A0E02 File Offset: 0x0009F002
		public BoundStatement Statement { get; }

		// Token: 0x06001E86 RID: 7814 RVA: 0x000A0E0A File Offset: 0x0009F00A
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitStateMachineScope(this);
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x000A0E13 File Offset: 0x0009F013
		public BoundStateMachineScope Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<StateMachineFieldSymbol> fields, BoundStatement statement)
		{
			if (fields != this.Fields || statement != this.Statement)
			{
				BoundStateMachineScope boundStateMachineScope = new BoundStateMachineScope(this.Syntax, fields, statement, base.HasErrors);
				boundStateMachineScope.CopyAttributes(this);
				return boundStateMachineScope;
			}
			return this;
		}
	}
}
