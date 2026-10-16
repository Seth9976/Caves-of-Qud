using System;
using System.Collections;
using System.Collections.Generic;

namespace LitJson
{
	// Token: 0x0200031C RID: 796
	internal class OrderedDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x000910F2 File Offset: 0x0008F2F2
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x00091100 File Offset: 0x0008F300
		public DictionaryEntry Entry
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0009112C File Offset: 0x0008F32C
		public object Key
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x0009114C File Offset: 0x0008F34C
		public object Value
		{
			get
			{
				KeyValuePair<string, JsonData> keyValuePair = this.list_enumerator.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x0009116C File Offset: 0x0008F36C
		public OrderedDictionaryEnumerator(IEnumerator<KeyValuePair<string, JsonData>> enumerator)
		{
			this.list_enumerator = enumerator;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x0009117B File Offset: 0x0008F37B
		public bool MoveNext()
		{
			return this.list_enumerator.MoveNext();
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x00091188 File Offset: 0x0008F388
		public void Reset()
		{
			this.list_enumerator.Reset();
		}

		// Token: 0x04000F82 RID: 3970
		private IEnumerator<KeyValuePair<string, JsonData>> list_enumerator;
	}
}
