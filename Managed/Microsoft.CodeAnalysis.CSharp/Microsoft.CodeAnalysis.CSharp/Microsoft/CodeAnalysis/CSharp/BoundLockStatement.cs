using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001A1 RID: 417
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundLockStatement : BoundStatement
	{
		// Token: 0x06001EF6 RID: 7926 RVA: 0x000A1AB4 File Offset: 0x0009FCB4
		public BoundLockStatement(SyntaxNode syntax, BoundExpression argument, BoundStatement body, bool hasErrors = false)
			: base(BoundKind.LockStatement, syntax, hasErrors || argument.HasErrors() || body.HasErrors())
		{
			this.Argument = argument;
			this.Body = body;
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x000A1AE2 File Offset: 0x0009FCE2
		public BoundExpression Argument { get; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x000A1AEA File Offset: 0x0009FCEA
		public BoundStatement Body { get; }

		// Token: 0x06001EF9 RID: 7929 RVA: 0x000A1AF2 File Offset: 0x0009FCF2
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitLockStatement(this);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x000A1AFB File Offset: 0x0009FCFB
		public BoundLockStatement Update(BoundExpression argument, BoundStatement body)
		{
			if (argument != this.Argument || body != this.Body)
			{
				BoundLockStatement boundLockStatement = new BoundLockStatement(this.Syntax, argument, body, base.HasErrors);
				boundLockStatement.CopyAttributes(this);
				return boundLockStatement;
			}
			return this;
		}
	}
}
