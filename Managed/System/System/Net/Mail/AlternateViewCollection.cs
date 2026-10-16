using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Represents a collection of <see cref="T:System.Net.Mail.AlternateView" /> objects.</summary>
	// Token: 0x020007FB RID: 2043
	public sealed class AlternateViewCollection : Collection<AlternateView>, IDisposable
	{
		// Token: 0x06004142 RID: 16706 RVA: 0x000DFB24 File Offset: 0x000DDD24
		internal AlternateViewCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.AlternateViewCollection" />.</summary>
		// Token: 0x06004143 RID: 16707 RVA: 0x00003917 File Offset: 0x00001B17
		public void Dispose()
		{
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x000DFB2C File Offset: 0x000DDD2C
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x000DFB34 File Offset: 0x000DDD34
		protected override void InsertItem(int index, AlternateView item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x000DFB3E File Offset: 0x000DDD3E
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x000DFB47 File Offset: 0x000DDD47
		protected override void SetItem(int index, AlternateView item)
		{
			base.SetItem(index, item);
		}
	}
}
