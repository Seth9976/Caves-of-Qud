using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200016F RID: 367
	internal abstract class RequiredLanguageVersion : IFormattable
	{
		// Token: 0x06000F31 RID: 3889
		[NullableContext(1)]
		public abstract override string ToString();

		// Token: 0x06000F32 RID: 3890 RVA: 0x0002CDB8 File Offset: 0x0002AFB8
		[NullableContext(2)]
		[return: Nullable(1)]
		string IFormattable.ToString(string format, IFormatProvider formatProvider)
		{
			return this.ToString();
		}
	}
}
