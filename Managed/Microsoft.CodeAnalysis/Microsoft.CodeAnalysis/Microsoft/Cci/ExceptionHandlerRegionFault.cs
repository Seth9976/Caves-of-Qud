using System;
using System.Reflection.Metadata;

namespace Microsoft.Cci
{
	// Token: 0x02000085 RID: 133
	internal sealed class ExceptionHandlerRegionFault : ExceptionHandlerRegion
	{
		// Token: 0x06000367 RID: 871 RVA: 0x0000CFB0 File Offset: 0x0000B1B0
		public ExceptionHandlerRegionFault(int tryStartOffset, int tryEndOffset, int handlerStartOffset, int handlerEndOffset)
			: base(tryStartOffset, tryEndOffset, handlerStartOffset, handlerEndOffset)
		{
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000CFBD File Offset: 0x0000B1BD
		public override ExceptionRegionKind HandlerKind
		{
			get
			{
				return ExceptionRegionKind.Fault;
			}
		}
	}
}
