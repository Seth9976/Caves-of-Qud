using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Internal
{
	// Token: 0x0200017A RID: 378
	[NullableContext(2)]
	[Nullable(0)]
	internal readonly struct StreamConstraints
	{
		// Token: 0x06000B9A RID: 2970 RVA: 0x00022E9F File Offset: 0x0002109F
		public StreamConstraints(object guardOpt, long startPosition, int imageSize)
		{
			this.GuardOpt = guardOpt;
			this.ImageStart = startPosition;
			this.ImageSize = imageSize;
		}

		// Token: 0x04000951 RID: 2385
		public readonly object GuardOpt;

		// Token: 0x04000952 RID: 2386
		public readonly long ImageStart;

		// Token: 0x04000953 RID: 2387
		public readonly int ImageSize;
	}
}
