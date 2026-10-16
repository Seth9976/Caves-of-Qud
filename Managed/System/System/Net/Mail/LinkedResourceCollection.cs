using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Stores linked resources to be sent as part of an e-mail message.</summary>
	// Token: 0x02000802 RID: 2050
	public sealed class LinkedResourceCollection : Collection<LinkedResource>, IDisposable
	{
		// Token: 0x0600417A RID: 16762 RVA: 0x000E227B File Offset: 0x000E047B
		internal LinkedResourceCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.LinkedResourceCollection" />.</summary>
		// Token: 0x0600417B RID: 16763 RVA: 0x000E2283 File Offset: 0x000E0483
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x00003917 File Offset: 0x00001B17
		private void Dispose(bool disposing)
		{
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x000E2292 File Offset: 0x000E0492
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x000E229A File Offset: 0x000E049A
		protected override void InsertItem(int index, LinkedResource item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x000E22A4 File Offset: 0x000E04A4
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x000E22AD File Offset: 0x000E04AD
		protected override void SetItem(int index, LinkedResource item)
		{
			base.SetItem(index, item);
		}
	}
}
