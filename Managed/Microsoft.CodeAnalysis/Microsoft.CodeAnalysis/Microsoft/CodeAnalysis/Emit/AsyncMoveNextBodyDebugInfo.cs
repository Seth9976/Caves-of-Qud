using System;
using System.Collections.Immutable;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000478 RID: 1144
	internal sealed class AsyncMoveNextBodyDebugInfo : StateMachineMoveNextBodyDebugInfo
	{
		// Token: 0x06002C8C RID: 11404 RVA: 0x0007DC7F File Offset: 0x0007BE7F
		public AsyncMoveNextBodyDebugInfo(IMethodDefinition kickoffMethod, int catchHandlerOffset, ImmutableArray<int> yieldOffsets, ImmutableArray<int> resumeOffsets)
			: base(kickoffMethod)
		{
			this.CatchHandlerOffset = catchHandlerOffset;
			this.YieldOffsets = yieldOffsets;
			this.ResumeOffsets = resumeOffsets;
		}

		// Token: 0x040011B8 RID: 4536
		public readonly int CatchHandlerOffset;

		// Token: 0x040011B9 RID: 4537
		public readonly ImmutableArray<int> YieldOffsets;

		// Token: 0x040011BA RID: 4538
		public readonly ImmutableArray<int> ResumeOffsets;
	}
}
