using System;

namespace Mono
{
	// Token: 0x020001A7 RID: 423
	internal class ArgumentNullOrEmptyException : ArgumentException
	{
		// Token: 0x060006E9 RID: 1769 RVA: 0x00017C14 File Offset: 0x00015E14
		public ArgumentNullOrEmptyException(string paramName)
			: base("Argument null or empty", paramName)
		{
		}
	}
}
