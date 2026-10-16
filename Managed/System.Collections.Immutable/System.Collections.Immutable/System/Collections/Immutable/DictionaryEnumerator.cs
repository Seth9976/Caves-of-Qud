using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000018 RID: 24
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DictionaryEnumerator<TKey, [Nullable(2)] TValue> : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002D34 File Offset: 0x00000F34
		internal DictionaryEnumerator([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerator<KeyValuePair<TKey, TValue>> inner)
		{
			Requires.NotNull<IEnumerator<KeyValuePair<TKey, TValue>>>(inner, "inner");
			this._inner = inner;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002D50 File Offset: 0x00000F50
		public DictionaryEntry Entry
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				object obj = keyValuePair.Key;
				keyValuePair = this._inner.Current;
				return new DictionaryEntry(obj, keyValuePair.Value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002D94 File Offset: 0x00000F94
		public object Key
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002DBC File Offset: 0x00000FBC
		[Nullable(2)]
		public object Value
		{
			[NullableContext(2)]
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002DE1 File Offset: 0x00000FE1
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002DEE File Offset: 0x00000FEE
		public bool MoveNext()
		{
			return this._inner.MoveNext();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002DFB File Offset: 0x00000FFB
		public void Reset()
		{
			this._inner.Reset();
		}

		// Token: 0x04000010 RID: 16
		private readonly IEnumerator<KeyValuePair<TKey, TValue>> _inner;
	}
}
