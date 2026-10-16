using System;

namespace XRL.World
{
	// Token: 0x0200094B RID: 2379
	public static class QudGameObjectExtensions
	{
		// Token: 0x0600613C RID: 24892 RVA: 0x001DA331 File Offset: 0x001D8531
		public static bool IsValid(this GameObject obj)
		{
			return obj != null && obj.Physics != null && !obj.IsInGraveyard();
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x001DA349 File Offset: 0x001D8549
		public static bool IsInvalid(this GameObject obj)
		{
			return obj == null || !obj.IsValid();
		}
	}
}
