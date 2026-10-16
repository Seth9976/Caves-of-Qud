using System;
using System.Reflection.Metadata;

namespace Microsoft.Cci
{
	// Token: 0x02000087 RID: 135
	internal sealed class ExceptionHandlerRegionFilter : ExceptionHandlerRegion
	{
		// Token: 0x0600036C RID: 876 RVA: 0x0000CFE0 File Offset: 0x0000B1E0
		public ExceptionHandlerRegionFilter(int tryStartOffset, int tryEndOffset, int handlerStartOffset, int handlerEndOffset, int filterDecisionStartOffset)
			: base(tryStartOffset, tryEndOffset, handlerStartOffset, handlerEndOffset)
		{
			this._filterDecisionStartOffset = filterDecisionStartOffset;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000CFF5 File Offset: 0x0000B1F5
		public override ExceptionRegionKind HandlerKind
		{
			get
			{
				return ExceptionRegionKind.Filter;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
		public override int FilterDecisionStartOffset
		{
			get
			{
				return this._filterDecisionStartOffset;
			}
		}

		// Token: 0x04000177 RID: 375
		private readonly int _filterDecisionStartOffset;
	}
}
