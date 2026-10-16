using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000040 RID: 64
	public class ArrayEnumeration : IEnumerator
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000C4A9 File Offset: 0x0000A6A9
		public virtual bool MoveNext()
		{
			bool flag = this.hasMoreElements();
			if (flag)
			{
				this.tempAuxObj = this.nextElement();
			}
			return flag;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000C4C0 File Offset: 0x0000A6C0
		public virtual void Reset()
		{
			this.tempAuxObj = null;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000C4C9 File Offset: 0x0000A6C9
		public virtual object Current
		{
			get
			{
				return this.tempAuxObj;
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000C4D1 File Offset: 0x0000A6D1
		public ArrayEnumeration(object[] eArray)
		{
			this.eArray = eArray;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
		public bool hasMoreElements()
		{
			return this.eArray != null && this.index < this.eArray.Length;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000C4FC File Offset: 0x0000A6FC
		public object nextElement()
		{
			if (this.eArray == null || this.index >= this.eArray.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			object[] array = this.eArray;
			int num = this.index;
			this.index = num + 1;
			return array[num];
		}

		// Token: 0x0400018D RID: 397
		private object tempAuxObj;

		// Token: 0x0400018E RID: 398
		private object[] eArray;

		// Token: 0x0400018F RID: 399
		private int index;
	}
}
