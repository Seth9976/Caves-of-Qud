using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002EC RID: 748
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class SourceTextContainer
	{
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002014 RID: 8212
		public abstract SourceText CurrentText { get; }

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06002015 RID: 8213
		// (remove) Token: 0x06002016 RID: 8214
		public abstract event EventHandler<TextChangeEventArgs> TextChanged;
	}
}
