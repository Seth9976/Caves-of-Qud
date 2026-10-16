using System;

namespace Mono.Cecil.Rocks
{
	// Token: 0x02000423 RID: 1059
	internal static class ParameterReferenceRocks
	{
		// Token: 0x06001652 RID: 5714 RVA: 0x00048313 File Offset: 0x00046513
		public static int GetSequence(this ParameterReference self)
		{
			return self.Index + 1;
		}
	}
}
