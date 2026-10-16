using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Stores attachments to be sent as part of an e-mail message.</summary>
	// Token: 0x020007FF RID: 2047
	public sealed class AttachmentCollection : Collection<Attachment>, IDisposable
	{
		// Token: 0x06004169 RID: 16745 RVA: 0x000E212E File Offset: 0x000E032E
		internal AttachmentCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.AttachmentCollection" />. </summary>
		// Token: 0x0600416A RID: 16746 RVA: 0x000E2138 File Offset: 0x000E0338
		public void Dispose()
		{
			for (int i = 0; i < base.Count; i++)
			{
				base[i].Dispose();
			}
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x000E2162 File Offset: 0x000E0362
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x000E216A File Offset: 0x000E036A
		protected override void InsertItem(int index, Attachment item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x000E2174 File Offset: 0x000E0374
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x000E217D File Offset: 0x000E037D
		protected override void SetItem(int index, Attachment item)
		{
			base.SetItem(index, item);
		}
	}
}
