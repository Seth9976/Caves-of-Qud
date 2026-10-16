using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200040A RID: 1034
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal class ArraySubsetEnumerator : IEnumerator
	{
		// Token: 0x0600215D RID: 8541 RVA: 0x0007219A File Offset: 0x0007039A
		public ArraySubsetEnumerator(Array array, int count)
		{
			this.array = array;
			this.total = count;
			this.current = -1;
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x000721B7 File Offset: 0x000703B7
		public bool MoveNext()
		{
			if (this.current < this.total - 1)
			{
				this.current++;
				return true;
			}
			return false;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000721DA File Offset: 0x000703DA
		public void Reset()
		{
			this.current = -1;
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x000721E3 File Offset: 0x000703E3
		public object Current
		{
			get
			{
				if (this.current == -1)
				{
					throw new InvalidOperationException();
				}
				return this.array.GetValue(this.current);
			}
		}

		// Token: 0x04001020 RID: 4128
		private Array array;

		// Token: 0x04001021 RID: 4129
		private int total;

		// Token: 0x04001022 RID: 4130
		private int current;
	}
}
