using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000140 RID: 320
	internal struct LocalizableErrorArgument : IFormattable
	{
		// Token: 0x06001923 RID: 6435 RVA: 0x0008762C File Offset: 0x0008582C
		internal LocalizableErrorArgument(MessageID id)
		{
			this._id = id;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00087635 File Offset: 0x00085835
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0008763F File Offset: 0x0008583F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return ErrorFacts.GetMessage(this._id, formatProvider as CultureInfo);
		}

		// Token: 0x04000DCE RID: 3534
		private readonly MessageID _id;
	}
}
