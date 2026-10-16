using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x02000734 RID: 1844
	internal class MultiColumnHeaderColumnIcon : Image
	{
		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x000D0C72 File Offset: 0x000CEE72
		// (set) Token: 0x0600359C RID: 13724 RVA: 0x000D0C7A File Offset: 0x000CEE7A
		public bool isImageInline { get; set; }

		// Token: 0x0600359D RID: 13725 RVA: 0x000D0C83 File Offset: 0x000CEE83
		public MultiColumnHeaderColumnIcon()
		{
			base.AddToClassList(MultiColumnHeaderColumnIcon.ussClassName);
			base.RegisterCallback<CustomStyleResolvedEvent>(delegate(CustomStyleResolvedEvent evt)
			{
				this.UpdateClassList();
			}, TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000D0CB0 File Offset: 0x000CEEB0
		public void UpdateClassList()
		{
			base.parent.RemoveFromClassList(MultiColumnHeaderColumn.hasIconUssClassName);
			bool flag = base.image != null || base.sprite != null || base.vectorImage != null;
			if (flag)
			{
				base.parent.AddToClassList(MultiColumnHeaderColumn.hasIconUssClassName);
			}
		}

		// Token: 0x04001B50 RID: 6992
		public new static readonly string ussClassName = MultiColumnHeaderColumn.ussClassName + "__icon";
	}
}
