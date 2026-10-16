using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000101 RID: 257
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public struct AwaitExpressionInfo : IEquatable<AwaitExpressionInfo>
	{
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x00069374 File Offset: 0x00067574
		public readonly IMethodSymbol GetAwaiterMethod { get; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0006937C File Offset: 0x0006757C
		public readonly IPropertySymbol IsCompletedProperty { get; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x00069384 File Offset: 0x00067584
		public readonly IMethodSymbol GetResultMethod { get; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0006938C File Offset: 0x0006758C
		public readonly bool IsDynamic { get; }

		// Token: 0x06001370 RID: 4976 RVA: 0x00069394 File Offset: 0x00067594
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal AwaitExpressionInfo(IMethodSymbol getAwaiter, IPropertySymbol isCompleted, IMethodSymbol getResult, bool isDynamic)
		{
			this.GetAwaiterMethod = getAwaiter;
			this.IsCompletedProperty = isCompleted;
			this.GetResultMethod = getResult;
			this.IsDynamic = isDynamic;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x000693B4 File Offset: 0x000675B4
		public override bool Equals(object obj)
		{
			if (obj is AwaitExpressionInfo)
			{
				AwaitExpressionInfo awaitExpressionInfo = (AwaitExpressionInfo)obj;
				return this.Equals(awaitExpressionInfo);
			}
			return false;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x000693DC File Offset: 0x000675DC
		public bool Equals(AwaitExpressionInfo other)
		{
			return object.Equals(this.GetAwaiterMethod, other.GetAwaiterMethod) && object.Equals(this.IsCompletedProperty, other.IsCompletedProperty) && object.Equals(this.GetResultMethod, other.GetResultMethod) && this.IsDynamic == other.IsDynamic;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00069438 File Offset: 0x00067638
		public override int GetHashCode()
		{
			return Hash.Combine<IMethodSymbol>(this.GetAwaiterMethod, Hash.Combine<IPropertySymbol>(this.IsCompletedProperty, Hash.Combine<IMethodSymbol>(this.GetResultMethod, this.IsDynamic.GetHashCode())));
		}
	}
}
