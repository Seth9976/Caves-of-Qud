using System;
using System.Collections.Generic;

namespace XRL.UI.Framework
{
	// Token: 0x0200150C RID: 5388
	public class PooledFrameworkDataElement<T> : FrameworkDataElement where T : FrameworkDataElement, new()
	{
		// Token: 0x0600D221 RID: 53793 RVA: 0x004E07C4 File Offset: 0x004DE9C4
		public static T next()
		{
			if (PooledFrameworkDataElement<T>.pool.Count > 0)
			{
				return PooledFrameworkDataElement<T>.pool.Dequeue();
			}
			return new T();
		}

		// Token: 0x0600D222 RID: 53794 RVA: 0x004E07E3 File Offset: 0x004DE9E3
		public virtual void free()
		{
			PooledFrameworkDataElement<T>.pool.Enqueue(this as T);
		}

		// Token: 0x04005347 RID: 21319
		private static Queue<T> pool = new Queue<T>();
	}
}
