using System;

namespace Battlehub.UIControls
{
	// Token: 0x020003C5 RID: 965
	public class SelectionChangedArgs : EventArgs
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x000B5831 File Offset: 0x000B3A31
		// (set) Token: 0x060023DE RID: 9182 RVA: 0x000B5839 File Offset: 0x000B3A39
		public object[] OldItems { get; private set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x000B5842 File Offset: 0x000B3A42
		// (set) Token: 0x060023E0 RID: 9184 RVA: 0x000B584A File Offset: 0x000B3A4A
		public object[] NewItems { get; private set; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060023E1 RID: 9185 RVA: 0x000B5853 File Offset: 0x000B3A53
		public object OldItem
		{
			get
			{
				if (this.OldItems == null)
				{
					return null;
				}
				if (this.OldItems.Length == 0)
				{
					return null;
				}
				return this.OldItems[0];
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x000B5872 File Offset: 0x000B3A72
		public object NewItem
		{
			get
			{
				if (this.NewItems == null)
				{
					return null;
				}
				if (this.NewItems.Length == 0)
				{
					return null;
				}
				return this.NewItems[0];
			}
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000B5891 File Offset: 0x000B3A91
		public SelectionChangedArgs(object[] oldItems, object[] newItems)
		{
			this.OldItems = oldItems;
			this.NewItems = newItems;
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000B58A7 File Offset: 0x000B3AA7
		public SelectionChangedArgs(object oldItem, object newItem)
		{
			this.OldItems = new object[] { oldItem };
			this.NewItems = new object[] { newItem };
		}
	}
}
