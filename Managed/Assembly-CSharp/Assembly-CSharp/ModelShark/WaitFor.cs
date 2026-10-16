using System;
using System.Collections;

namespace ModelShark
{
	// Token: 0x0200019B RID: 411
	public static class WaitFor
	{
		// Token: 0x06000F89 RID: 3977 RVA: 0x0006298C File Offset: 0x00060B8C
		public static IEnumerator Frames(int frameCount)
		{
			while (frameCount > 0)
			{
				int num = frameCount;
				frameCount = num - 1;
				yield return null;
			}
			yield break;
		}
	}
}
