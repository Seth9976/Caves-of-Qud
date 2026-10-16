using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using Unity.Hierarchy;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x02000061 RID: 97
	public abstract class CollectionViewController : IDisposable
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600037D RID: 893 RVA: 0x000105B8 File Offset: 0x0000E7B8
		// (remove) Token: 0x0600037E RID: 894 RVA: 0x000105F0 File Offset: 0x0000E7F0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action itemsSourceChanged;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600037F RID: 895 RVA: 0x00010628 File Offset: 0x0000E828
		// (remove) Token: 0x06000380 RID: 896 RVA: 0x00010660 File Offset: 0x0000E860
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int, int> itemIndexChanged;

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00010695 File Offset: 0x0000E895
		// (set) Token: 0x06000382 RID: 898 RVA: 0x000106A0 File Offset: 0x0000E8A0
		public virtual IList itemsSource
		{
			get
			{
				return this.m_ItemsSource;
			}
			set
			{
				bool flag = this.m_ItemsSource == value;
				if (!flag)
				{
					this.m_ItemsSource = value;
					bool flag2 = this.m_View.GetProperty("__unity-collection-view-internal-binding") == null;
					if (flag2)
					{
						this.m_View.RefreshItems();
					}
					this.RaiseItemsSourceChanged();
				}
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000106F5 File Offset: 0x0000E8F5
		protected void SetItemsSourceWithoutNotify(IList source)
		{
			this.m_ItemsSource = source;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000106FF File Offset: 0x0000E8FF
		private protected void SetHierarchyViewModelWithoutNotify(HierarchyViewModel source)
		{
			this.m_ItemsSource = new ReadOnlyHierarchyViewModelList(source);
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0001070E File Offset: 0x0000E90E
		protected BaseVerticalCollectionView view
		{
			get
			{
				return this.m_View;
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00010716 File Offset: 0x0000E916
		public void SetView(BaseVerticalCollectionView collectionView)
		{
			this.m_View = collectionView;
			this.PrepareView();
			Assert.IsNotNull<BaseVerticalCollectionView>(this.m_View, "View must not be null.");
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00002152 File Offset: 0x00000352
		protected virtual void PrepareView()
		{
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00010738 File Offset: 0x0000E938
		public virtual void Dispose()
		{
			this.itemsSourceChanged = null;
			this.itemIndexChanged = null;
			this.m_View = null;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00010750 File Offset: 0x0000E950
		public virtual int GetItemsCount()
		{
			IList itemsSource = this.m_ItemsSource;
			return (itemsSource != null) ? itemsSource.Count : 0;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00010774 File Offset: 0x0000E974
		internal virtual int GetItemsMinCount()
		{
			return this.GetItemsCount();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001077C File Offset: 0x0000E97C
		public virtual int GetIndexForId(int id)
		{
			return id;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00010790 File Offset: 0x0000E990
		public virtual int GetIdForIndex(int index)
		{
			return index;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000107A4 File Offset: 0x0000E9A4
		public virtual object GetItemForIndex(int index)
		{
			bool flag = this.m_ItemsSource == null;
			object obj;
			if (flag)
			{
				obj = null;
			}
			else
			{
				bool flag2 = index < 0 || index >= this.m_ItemsSource.Count;
				if (flag2)
				{
					obj = null;
				}
				else
				{
					obj = this.m_ItemsSource[index];
				}
			}
			return obj;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000107F4 File Offset: 0x0000E9F4
		public virtual object GetItemForId(int id)
		{
			bool flag = this.m_ItemsSource == null;
			object obj;
			if (flag)
			{
				obj = null;
			}
			else
			{
				int indexForId = this.GetIndexForId(id);
				bool flag2 = indexForId < 0 || indexForId >= this.m_ItemsSource.Count;
				if (flag2)
				{
					obj = null;
				}
				else
				{
					obj = this.m_ItemsSource[indexForId];
				}
			}
			return obj;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001084B File Offset: 0x0000EA4B
		internal virtual void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			reusableItem.Init(this.MakeItem());
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00002152 File Offset: 0x00000352
		internal virtual void SetBindingContext(ReusableCollectionItem reusableItem, int index)
		{
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001085C File Offset: 0x0000EA5C
		internal virtual void InvokeBindItem(ReusableCollectionItem reusableItem, int index)
		{
			this.BindItem(reusableItem.bindableElement, index);
			this.SetBindingContext(reusableItem, index);
			reusableItem.SetSelected(this.m_View.selectedIndices.Contains(index));
			reusableItem.rootElement.pseudoStates &= ~PseudoStates.Hover;
			reusableItem.index = index;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000108B6 File Offset: 0x0000EAB6
		internal virtual void InvokeUnbindItem(ReusableCollectionItem reusableItem, int index)
		{
			this.UnbindItem(reusableItem.bindableElement, index);
			reusableItem.index = -1;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000108CF File Offset: 0x0000EACF
		internal virtual void InvokeDestroyItem(ReusableCollectionItem reusableItem)
		{
			this.DestroyItem(reusableItem.bindableElement);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00002152 File Offset: 0x00000352
		internal virtual void PreRefresh()
		{
		}

		// Token: 0x06000395 RID: 917
		protected abstract VisualElement MakeItem();

		// Token: 0x06000396 RID: 918
		protected abstract void BindItem(VisualElement element, int index);

		// Token: 0x06000397 RID: 919
		protected abstract void UnbindItem(VisualElement element, int index);

		// Token: 0x06000398 RID: 920
		protected abstract void DestroyItem(VisualElement element);

		// Token: 0x06000399 RID: 921 RVA: 0x000108DF File Offset: 0x0000EADF
		protected void RaiseItemsSourceChanged()
		{
			Action action = this.itemsSourceChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000108F4 File Offset: 0x0000EAF4
		protected void RaiseItemIndexChanged(int srcIndex, int dstIndex)
		{
			Action<int, int> action = this.itemIndexChanged;
			if (action != null)
			{
				action(srcIndex, dstIndex);
			}
		}

		// Token: 0x040001EB RID: 491
		private BaseVerticalCollectionView m_View;

		// Token: 0x040001EC RID: 492
		private IList m_ItemsSource;
	}
}
