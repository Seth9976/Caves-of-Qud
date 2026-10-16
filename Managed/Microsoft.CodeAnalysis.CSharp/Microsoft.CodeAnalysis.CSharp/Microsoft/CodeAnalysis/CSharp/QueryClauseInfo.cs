using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200010F RID: 271
	public struct QueryClauseInfo : IEquatable<QueryClauseInfo>
	{
		// Token: 0x06001631 RID: 5681 RVA: 0x000774E6 File Offset: 0x000756E6
		internal QueryClauseInfo(SymbolInfo castInfo, SymbolInfo operationInfo)
		{
			this._castInfo = castInfo;
			this._operationInfo = operationInfo;
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x000774F6 File Offset: 0x000756F6
		public SymbolInfo CastInfo
		{
			get
			{
				return this._castInfo;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x000774FE File Offset: 0x000756FE
		public SymbolInfo OperationInfo
		{
			get
			{
				return this._operationInfo;
			}
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00077506 File Offset: 0x00075706
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is QueryClauseInfo && this.Equals((QueryClauseInfo)obj);
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x00077520 File Offset: 0x00075720
		public bool Equals(QueryClauseInfo other)
		{
			return this._castInfo.Equals(other._castInfo) && this._operationInfo.Equals(other._operationInfo);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0007755C File Offset: 0x0007575C
		public override int GetHashCode()
		{
			return Hash.Combine(this.CastInfo.GetHashCode(), this._operationInfo.GetHashCode());
		}

		// Token: 0x04000641 RID: 1601
		private readonly SymbolInfo _castInfo;

		// Token: 0x04000642 RID: 1602
		private readonly SymbolInfo _operationInfo;
	}
}
