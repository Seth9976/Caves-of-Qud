using System;
using System.Reflection.Metadata;

namespace Microsoft.Cci
{
	// Token: 0x02000084 RID: 132
	internal sealed class ExceptionHandlerRegionFinally : ExceptionHandlerRegion
	{
		// Token: 0x06000365 RID: 869 RVA: 0x0000CFA0 File Offset: 0x0000B1A0
		public ExceptionHandlerRegionFinally(int tryStartOffset, int tryEndOffset, int handlerStartOffset, int handlerEndOffset)
			: base(tryStartOffset, tryEndOffset, handlerStartOffset, handlerEndOffset)
		{
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000CFAD File Offset: 0x0000B1AD
		public override ExceptionRegionKind HandlerKind
		{
			get
			{
				return ExceptionRegionKind.Finally;
			}
		}
	}
}
