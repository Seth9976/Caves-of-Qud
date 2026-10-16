using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000010 RID: 16
	public class IDs : IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000022C5 File Offset: 0x000004C5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_Dic.GetEnumerator();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000022D8 File Offset: 0x000004D8
		public void Add(string id, params string[] stores)
		{
			foreach (string text in stores)
			{
				this.m_Dic[text] = id;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002308 File Offset: 0x00000508
		public void Add(string id, params object[] stores)
		{
			foreach (object obj in stores)
			{
				this.m_Dic[obj.ToString()] = id;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000233B File Offset: 0x0000053B
		internal string SpecificIDForStore(string store, string defaultValue)
		{
			if (this.m_Dic.ContainsKey(store))
			{
				return this.m_Dic[store];
			}
			return defaultValue;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002359 File Offset: 0x00000559
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return this.m_Dic.GetEnumerator();
		}

		// Token: 0x0400000B RID: 11
		private readonly Dictionary<string, string> m_Dic = new Dictionary<string, string>();
	}
}
