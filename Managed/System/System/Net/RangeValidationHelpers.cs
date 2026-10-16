using System;

namespace System.Net
{
	// Token: 0x0200056C RID: 1388
	internal static class RangeValidationHelpers
	{
		// Token: 0x06002CF4 RID: 11508 RVA: 0x00099E34 File Offset: 0x00098034
		public static bool ValidateRange(int actual, int fromAllowed, int toAllowed)
		{
			return actual >= fromAllowed && actual <= toAllowed;
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x00099E44 File Offset: 0x00098044
		public static void ValidateSegment(ArraySegment<byte> segment)
		{
			if (segment.Array == null)
			{
				throw new ArgumentNullException("segment");
			}
			if (segment.Offset < 0 || segment.Count < 0 || segment.Count > segment.Array.Length - segment.Offset)
			{
				throw new ArgumentOutOfRangeException("segment");
			}
		}
	}
}
