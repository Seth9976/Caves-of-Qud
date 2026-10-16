using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000046 RID: 70
	public class EnumeratedIterator : IEnumerator
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x0000D765 File Offset: 0x0000B965
		public virtual bool MoveNext()
		{
			bool flag = this.hasMoreElements();
			if (flag)
			{
				this.tempAuxObj = this.nextElement();
			}
			return flag;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D77C File Offset: 0x0000B97C
		public virtual void Reset()
		{
			this.tempAuxObj = null;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000D785 File Offset: 0x0000B985
		public virtual object Current
		{
			get
			{
				return this.tempAuxObj;
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D78D File Offset: 0x0000B98D
		public EnumeratedIterator(IEnumerator iterator)
		{
			this.i = iterator;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000D79C File Offset: 0x0000B99C
		public bool hasMoreElements()
		{
			return this.i.MoveNext();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000D7A9 File Offset: 0x0000B9A9
		public object nextElement()
		{
			return this.i.Current;
		}

		// Token: 0x040001AB RID: 427
		private object tempAuxObj;

		// Token: 0x040001AC RID: 428
		private IEnumerator i;
	}
}
