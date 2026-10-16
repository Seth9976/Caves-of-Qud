using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000AB RID: 171
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundBaseReference : BoundExpression
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00055A64 File Offset: 0x00053C64
		public override bool SuppressVirtualCalls
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00055A67 File Offset: 0x00053C67
		public BoundBaseReference(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors)
			: base(BoundKind.BaseReference, syntax, type, hasErrors)
		{
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00055A74 File Offset: 0x00053C74
		public BoundBaseReference(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
			: base(BoundKind.BaseReference, syntax, type)
		{
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00055A80 File Offset: 0x00053C80
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00055A88 File Offset: 0x00053C88
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitBaseReference(this);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00055A91 File Offset: 0x00053C91
		public BoundBaseReference Update([global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundBaseReference boundBaseReference = new BoundBaseReference(this.Syntax, type, base.HasErrors);
				boundBaseReference.CopyAttributes(this);
				return boundBaseReference;
			}
			return this;
		}
	}
}
