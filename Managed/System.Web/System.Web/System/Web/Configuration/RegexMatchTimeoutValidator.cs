using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200056E RID: 1390
	internal sealed class RegexMatchTimeoutValidator : TimeSpanValidator
	{
		// Token: 0x06003B22 RID: 15138 RVA: 0x0009C737 File Offset: 0x0009A937
		public RegexMatchTimeoutValidator()
			: base(RegexMatchTimeoutValidator._minValue, RegexMatchTimeoutValidator._maxValue)
		{
		}

		// Token: 0x04002041 RID: 8257
		private static readonly TimeSpan _minValue = TimeSpan.Zero;

		// Token: 0x04002042 RID: 8258
		private static readonly TimeSpan _maxValue = TimeSpan.FromMilliseconds(2147483646.0);
	}
}
