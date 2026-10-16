using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000657 RID: 1623
	[Serializable]
	internal class PathList
	{
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x000B2395 File Offset: 0x000B0595
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x000B23A4 File Offset: 0x000B05A4
		public int GetCookiesCount()
		{
			int num = 0;
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.m_list.Values)
				{
					CookieCollection cookieCollection = (CookieCollection)obj;
					num += cookieCollection.Count;
				}
			}
			return num;
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x000B2434 File Offset: 0x000B0634
		public ICollection Values
		{
			get
			{
				return this.m_list.Values;
			}
		}

		// Token: 0x17000A4F RID: 2639
		public object this[string s]
		{
			get
			{
				return this.m_list[s];
			}
			set
			{
				object syncRoot = this.SyncRoot;
				lock (syncRoot)
				{
					this.m_list[s] = value;
				}
			}
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000B2498 File Offset: 0x000B0698
		public IEnumerator GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000B24A5 File Offset: 0x000B06A5
		public object SyncRoot
		{
			get
			{
				return this.m_list.SyncRoot;
			}
		}

		// Token: 0x04001E1B RID: 7707
		private SortedList m_list = SortedList.Synchronized(new SortedList(PathList.PathListComparer.StaticInstance));

		// Token: 0x02000658 RID: 1624
		[Serializable]
		private class PathListComparer : IComparer
		{
			// Token: 0x0600331D RID: 13085 RVA: 0x000B24B4 File Offset: 0x000B06B4
			int IComparer.Compare(object ol, object or)
			{
				string text = CookieParser.CheckQuoted((string)ol);
				string text2 = CookieParser.CheckQuoted((string)or);
				int length = text.Length;
				int length2 = text2.Length;
				int num = Math.Min(length, length2);
				for (int i = 0; i < num; i++)
				{
					if (text[i] != text2[i])
					{
						return (int)(text[i] - text2[i]);
					}
				}
				return length2 - length;
			}

			// Token: 0x04001E1C RID: 7708
			internal static readonly PathList.PathListComparer StaticInstance = new PathList.PathListComparer();
		}
	}
}
