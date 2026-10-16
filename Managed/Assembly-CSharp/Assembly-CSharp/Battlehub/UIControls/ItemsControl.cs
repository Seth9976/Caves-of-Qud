using System;

namespace Battlehub.UIControls
{
	// Token: 0x020003CC RID: 972
	public class ItemsControl<TDataBindingArgs> : ItemsControl where TDataBindingArgs : ItemDataBindingArgs, new()
	{
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06002407 RID: 9223 RVA: 0x000B5A3C File Offset: 0x000B3C3C
		// (remove) Token: 0x06002408 RID: 9224 RVA: 0x000B5A74 File Offset: 0x000B3C74
		public event EventHandler<TDataBindingArgs> ItemDataBinding;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06002409 RID: 9225 RVA: 0x000B5AAC File Offset: 0x000B3CAC
		// (remove) Token: 0x0600240A RID: 9226 RVA: 0x000B5AE4 File Offset: 0x000B3CE4
		public event EventHandler<TDataBindingArgs> ItemBeginEdit;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600240B RID: 9227 RVA: 0x000B5B1C File Offset: 0x000B3D1C
		// (remove) Token: 0x0600240C RID: 9228 RVA: 0x000B5B54 File Offset: 0x000B3D54
		public event EventHandler<TDataBindingArgs> ItemEndEdit;

		// Token: 0x0600240D RID: 9229 RVA: 0x000B5B8C File Offset: 0x000B3D8C
		protected override void OnItemBeginEdit(object sender, EventArgs e)
		{
			if (!base.CanHandleEvent(sender))
			{
				return;
			}
			ItemContainer itemContainer = (ItemContainer)sender;
			if (this.ItemBeginEdit != null)
			{
				TDataBindingArgs tdataBindingArgs = new TDataBindingArgs();
				tdataBindingArgs.Item = itemContainer.Item;
				tdataBindingArgs.ItemPresenter = ((itemContainer.ItemPresenter == null) ? base.gameObject : itemContainer.ItemPresenter);
				tdataBindingArgs.EditorPresenter = ((itemContainer.EditorPresenter == null) ? base.gameObject : itemContainer.EditorPresenter);
				this.ItemBeginEdit(this, tdataBindingArgs);
			}
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000B5C24 File Offset: 0x000B3E24
		protected override void OnItemEndEdit(object sender, EventArgs e)
		{
			if (!base.CanHandleEvent(sender))
			{
				return;
			}
			ItemContainer itemContainer = (ItemContainer)sender;
			if (this.ItemBeginEdit != null)
			{
				TDataBindingArgs tdataBindingArgs = new TDataBindingArgs();
				tdataBindingArgs.Item = itemContainer.Item;
				tdataBindingArgs.ItemPresenter = ((itemContainer.ItemPresenter == null) ? base.gameObject : itemContainer.ItemPresenter);
				tdataBindingArgs.EditorPresenter = ((itemContainer.EditorPresenter == null) ? base.gameObject : itemContainer.EditorPresenter);
				this.ItemEndEdit(this, tdataBindingArgs);
			}
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000B5CBC File Offset: 0x000B3EBC
		public override void DataBindItem(object item, ItemContainer itemContainer)
		{
			TDataBindingArgs tdataBindingArgs = new TDataBindingArgs();
			tdataBindingArgs.Item = item;
			tdataBindingArgs.ItemPresenter = ((itemContainer.ItemPresenter == null) ? base.gameObject : itemContainer.ItemPresenter);
			tdataBindingArgs.EditorPresenter = ((itemContainer.EditorPresenter == null) ? base.gameObject : itemContainer.EditorPresenter);
			this.RaiseItemDataBinding(tdataBindingArgs);
			itemContainer.CanEdit = tdataBindingArgs.CanEdit;
			itemContainer.CanDrag = tdataBindingArgs.CanDrag;
			itemContainer.CanDrop = tdataBindingArgs.CanDrop;
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000B5D63 File Offset: 0x000B3F63
		protected void RaiseItemDataBinding(TDataBindingArgs args)
		{
			if (this.ItemDataBinding != null)
			{
				this.ItemDataBinding(this, args);
			}
		}
	}
}
