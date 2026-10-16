using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x02000130 RID: 304
	internal class SimpleRecyclingCache
	{
		// Token: 0x06000E30 RID: 3632 RVA: 0x0002619C File Offset: 0x0002439C
		internal SimpleRecyclingCache()
		{
			this.CreateHashtable();
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x000261AA File Offset: 0x000243AA
		private void CreateHashtable()
		{
			SimpleRecyclingCache._hashtable = new Hashtable(100, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x170004E4 RID: 1252
		internal object this[object key]
		{
			get
			{
				return SimpleRecyclingCache._hashtable[key];
			}
			set
			{
				lock (this)
				{
					if (SimpleRecyclingCache._hashtable.Count >= 100)
					{
						SimpleRecyclingCache._hashtable.Clear();
					}
					SimpleRecyclingCache._hashtable[key] = value;
				}
			}
		}

		// Token: 0x040011D8 RID: 4568
		private const int MAX_SIZE = 100;

		// Token: 0x040011D9 RID: 4569
		private static Hashtable _hashtable;
	}
}
