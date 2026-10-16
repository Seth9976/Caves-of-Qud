using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000176 RID: 374
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundAwaitableInfo : BoundNode
	{
		// Token: 0x06001E0B RID: 7691 RVA: 0x000A0328 File Offset: 0x0009E528
		public BoundAwaitableInfo([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, BoundAwaitableValuePlaceholder awaitableInstancePlaceholder, bool isDynamic, BoundExpression getAwaiter, PropertySymbol isCompleted, MethodSymbol getResult, bool hasErrors = false)
			: base(BoundKind.AwaitableInfo, syntax, hasErrors || awaitableInstancePlaceholder.HasErrors() || getAwaiter.HasErrors())
		{
			this.AwaitableInstancePlaceholder = awaitableInstancePlaceholder;
			this.IsDynamic = isDynamic;
			this.GetAwaiter = getAwaiter;
			this.IsCompleted = isCompleted;
			this.GetResult = getResult;
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x000A037A File Offset: 0x0009E57A
		public BoundAwaitableValuePlaceholder AwaitableInstancePlaceholder { get; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x000A0382 File Offset: 0x0009E582
		public bool IsDynamic { get; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x000A038A File Offset: 0x0009E58A
		public BoundExpression GetAwaiter { get; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x000A0392 File Offset: 0x0009E592
		public PropertySymbol IsCompleted { get; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x000A039A File Offset: 0x0009E59A
		public MethodSymbol GetResult { get; }

		// Token: 0x06001E11 RID: 7697 RVA: 0x000A03A2 File Offset: 0x0009E5A2
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitAwaitableInfo(this);
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x000A03AC File Offset: 0x0009E5AC
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundAwaitableInfo Update(BoundAwaitableValuePlaceholder awaitableInstancePlaceholder, bool isDynamic, BoundExpression getAwaiter, PropertySymbol isCompleted, MethodSymbol getResult)
		{
			if (awaitableInstancePlaceholder != this.AwaitableInstancePlaceholder || isDynamic != this.IsDynamic || getAwaiter != this.GetAwaiter || !SymbolEqualityComparer.ConsiderEverything.Equals(isCompleted, this.IsCompleted) || !SymbolEqualityComparer.ConsiderEverything.Equals(getResult, this.GetResult))
			{
				BoundAwaitableInfo boundAwaitableInfo = new BoundAwaitableInfo(this.Syntax, awaitableInstancePlaceholder, isDynamic, getAwaiter, isCompleted, getResult, base.HasErrors);
				boundAwaitableInfo.CopyAttributes(this);
				return boundAwaitableInfo;
			}
			return this;
		}
	}
}
