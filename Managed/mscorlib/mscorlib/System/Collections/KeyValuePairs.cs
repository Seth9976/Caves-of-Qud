using System;
using System.Diagnostics;

namespace System.Collections
{
	// Token: 0x02000A26 RID: 2598
	[DebuggerDisplay("{_value}", Name = "[{_key}]")]
	internal class KeyValuePairs
	{
		// Token: 0x06005C17 RID: 23575 RVA: 0x00135B35 File Offset: 0x00133D35
		public KeyValuePairs(object key, object value)
		{
			this._value = value;
			this._key = key;
		}

		// Token: 0x04003888 RID: 14472
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly object _key;

		// Token: 0x04003889 RID: 14473
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly object _value;
	}
}
