using System;
using System.Collections.Generic;

namespace Trivial.CodeSecurity.LoopDetection
{
	// Token: 0x02000011 RID: 17
	public class LoopDetectionHashGenerator
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003C1C File Offset: 0x00001E1C
		public int GetNextHash()
		{
			int num = -1;
			while (num == -1 || this.usedHashes.Contains(num))
			{
				num = this.rand.Next();
			}
			return num;
		}

		// Token: 0x0400003A RID: 58
		private Random rand = new Random();

		// Token: 0x0400003B RID: 59
		private HashSet<int> usedHashes = new HashSet<int>();
	}
}
