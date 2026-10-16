using System;
using System.Collections.Generic;

namespace AiUnity.Common.Tags
{
	// Token: 0x020002BE RID: 702
	public interface ITagAccess
	{
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600187C RID: 6268
		IEnumerable<string> TagPaths { get; }
	}
}
