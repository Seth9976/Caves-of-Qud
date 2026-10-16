using System;
using System.Collections;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200061D RID: 1565
	internal class TagStack
	{
		// Token: 0x06004317 RID: 17175 RVA: 0x000B1208 File Offset: 0x000AF408
		public TagStack()
		{
			this.tags = new Stack();
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x000B121B File Offset: 0x000AF41B
		public void Push(string tagid)
		{
			this.tags.Push(tagid);
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x000B1229 File Offset: 0x000AF429
		public string Pop()
		{
			if (this.tags.Count == 0)
			{
				return null;
			}
			return (string)this.tags.Pop();
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x000B124A File Offset: 0x000AF44A
		public bool CompareTo(string tagid)
		{
			return this.tags.Count != 0 && string.Compare(tagid, (string)this.tags.Peek(), true, Helpers.InvariantCulture) == 0;
		}

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x0600431B RID: 17179 RVA: 0x000B127A File Offset: 0x000AF47A
		public int Count
		{
			get
			{
				return this.tags.Count;
			}
		}

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x000B1287 File Offset: 0x000AF487
		public string Current
		{
			get
			{
				return (string)this.tags.Peek();
			}
		}

		// Token: 0x040023F7 RID: 9207
		private Stack tags;
	}
}
