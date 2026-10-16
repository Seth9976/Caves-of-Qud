using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001D3 RID: 467
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundStringInsert : BoundExpression
	{
		// Token: 0x06001FFC RID: 8188 RVA: 0x000A364C File Offset: 0x000A184C
		public BoundStringInsert([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression value, BoundExpression alignment, BoundLiteral format, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.StringInsert, syntax, type, hasErrors || value.HasErrors() || alignment.HasErrors() || format.HasErrors())
		{
			this.Value = value;
			this.Alignment = alignment;
			this.Format = format;
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x000A369B File Offset: 0x000A189B
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundExpression Value
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x000A36A3 File Offset: 0x000A18A3
		public BoundExpression Alignment { get; }

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x000A36AB File Offset: 0x000A18AB
		public BoundLiteral Format { get; }

		// Token: 0x06002000 RID: 8192 RVA: 0x000A36B3 File Offset: 0x000A18B3
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitStringInsert(this);
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000A36BC File Offset: 0x000A18BC
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundStringInsert Update([global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression value, BoundExpression alignment, BoundLiteral format, TypeSymbol type)
		{
			if (value != this.Value || alignment != this.Alignment || format != this.Format || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundStringInsert boundStringInsert = new BoundStringInsert(this.Syntax, value, alignment, format, type, base.HasErrors);
				boundStringInsert.CopyAttributes(this);
				return boundStringInsert;
			}
			return this;
		}
	}
}
