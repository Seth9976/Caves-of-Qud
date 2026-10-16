using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Provides a generic collection that supports data binding.</summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	// Token: 0x02000388 RID: 904
	[Serializable]
	public class BindingList<T> : Collection<T>, IBindingList, IList, ICollection, IEnumerable, ICancelAddNew, IRaiseItemChangedEvents
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindingList`1" /> class using default values.</summary>
		// Token: 0x06001D93 RID: 7571 RVA: 0x000691F4 File Offset: 0x000673F4
		public BindingList()
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindingList`1" /> class with the specified list.</summary>
		/// <param name="list">An <see cref="T:System.Collections.Generic.IList`1" /> of items to be contained in the <see cref="T:System.ComponentModel.BindingList`1" />.</param>
		// Token: 0x06001D94 RID: 7572 RVA: 0x0006922C File Offset: 0x0006742C
		public BindingList(IList<T> list)
			: base(list)
		{
			this.Initialize();
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x00069268 File Offset: 0x00067468
		private void Initialize()
		{
			this.allowNew = this.ItemTypeHasDefaultConstructor;
			if (typeof(INotifyPropertyChanged).IsAssignableFrom(typeof(T)))
			{
				this.raiseItemChangedEvents = true;
				foreach (T t in base.Items)
				{
					this.HookPropertyChanged(t);
				}
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x000692E4 File Offset: 0x000674E4
		private bool ItemTypeHasDefaultConstructor
		{
			get
			{
				Type typeFromHandle = typeof(T);
				return typeFromHandle.IsPrimitive || typeFromHandle.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, Array.Empty<Type>(), null) != null;
			}
		}

		/// <summary>Occurs before an item is added to the list.</summary>
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06001D97 RID: 7575 RVA: 0x0006931E File Offset: 0x0006751E
		// (remove) Token: 0x06001D98 RID: 7576 RVA: 0x0006934D File Offset: 0x0006754D
		public event AddingNewEventHandler AddingNew
		{
			add
			{
				bool flag = this.AllowNew;
				this._onAddingNew = (AddingNewEventHandler)Delegate.Combine(this._onAddingNew, value);
				if (flag != this.AllowNew)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
			remove
			{
				bool flag = this.AllowNew;
				this._onAddingNew = (AddingNewEventHandler)Delegate.Remove(this._onAddingNew, value);
				if (flag != this.AllowNew)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BindingList`1.AddingNew" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AddingNewEventArgs" /> that contains the event data. </param>
		// Token: 0x06001D99 RID: 7577 RVA: 0x0006937C File Offset: 0x0006757C
		protected virtual void OnAddingNew(AddingNewEventArgs e)
		{
			AddingNewEventHandler onAddingNew = this._onAddingNew;
			if (onAddingNew == null)
			{
				return;
			}
			onAddingNew(this, e);
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00069390 File Offset: 0x00067590
		private object FireAddingNew()
		{
			AddingNewEventArgs e = new AddingNewEventArgs(null);
			this.OnAddingNew(e);
			return e.NewObject;
		}

		/// <summary>Occurs when the list or an item in the list changes.</summary>
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06001D9B RID: 7579 RVA: 0x000693B1 File Offset: 0x000675B1
		// (remove) Token: 0x06001D9C RID: 7580 RVA: 0x000693CA File Offset: 0x000675CA
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				this._onListChanged = (ListChangedEventHandler)Delegate.Combine(this._onListChanged, value);
			}
			remove
			{
				this._onListChanged = (ListChangedEventHandler)Delegate.Remove(this._onListChanged, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06001D9D RID: 7581 RVA: 0x000693E3 File Offset: 0x000675E3
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			ListChangedEventHandler onListChanged = this._onListChanged;
			if (onListChanged == null)
			{
				return;
			}
			onListChanged(this, e);
		}

		/// <summary>Gets or sets a value indicating whether adding or removing items within the list raises <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events.</summary>
		/// <returns>true if adding or removing items raises <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events; otherwise, false. The default is true.</returns>
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x000693F7 File Offset: 0x000675F7
		// (set) Token: 0x06001D9F RID: 7583 RVA: 0x000693FF File Offset: 0x000675FF
		public bool RaiseListChangedEvents
		{
			get
			{
				return this.raiseListChangedEvents;
			}
			set
			{
				this.raiseListChangedEvents = value;
			}
		}

		/// <summary>Raises a <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event of type <see cref="F:System.ComponentModel.ListChangedType.Reset" />.</summary>
		// Token: 0x06001DA0 RID: 7584 RVA: 0x00069408 File Offset: 0x00067608
		public void ResetBindings()
		{
			this.FireListChanged(ListChangedType.Reset, -1);
		}

		/// <summary>Raises a <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event of type <see cref="F:System.ComponentModel.ListChangedType.ItemChanged" /> for the item at the specified position.</summary>
		/// <param name="position">A zero-based index of the item to be reset.</param>
		// Token: 0x06001DA1 RID: 7585 RVA: 0x00069412 File Offset: 0x00067612
		public void ResetItem(int position)
		{
			this.FireListChanged(ListChangedType.ItemChanged, position);
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0006941C File Offset: 0x0006761C
		private void FireListChanged(ListChangedType type, int index)
		{
			if (this.raiseListChangedEvents)
			{
				this.OnListChanged(new ListChangedEventArgs(type, index));
			}
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06001DA3 RID: 7587 RVA: 0x00069434 File Offset: 0x00067634
		protected override void ClearItems()
		{
			this.EndNew(this.addNewPos);
			if (this.raiseItemChangedEvents)
			{
				foreach (T t in base.Items)
				{
					this.UnhookPropertyChanged(t);
				}
			}
			base.ClearItems();
			this.FireListChanged(ListChangedType.Reset, -1);
		}

		/// <summary>Inserts the specified item in the list at the specified index.</summary>
		/// <param name="index">The zero-based index where the item is to be inserted.</param>
		/// <param name="item">The item to insert in the list.</param>
		// Token: 0x06001DA4 RID: 7588 RVA: 0x000694A4 File Offset: 0x000676A4
		protected override void InsertItem(int index, T item)
		{
			this.EndNew(this.addNewPos);
			base.InsertItem(index, item);
			if (this.raiseItemChangedEvents)
			{
				this.HookPropertyChanged(item);
			}
			this.FireListChanged(ListChangedType.ItemAdded, index);
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove. </param>
		/// <exception cref="T:System.NotSupportedException">You are removing a newly added item and <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> is set to false. </exception>
		// Token: 0x06001DA5 RID: 7589 RVA: 0x000694D4 File Offset: 0x000676D4
		protected override void RemoveItem(int index)
		{
			if (!this.allowRemove && (this.addNewPos < 0 || this.addNewPos != index))
			{
				throw new NotSupportedException();
			}
			this.EndNew(this.addNewPos);
			if (this.raiseItemChangedEvents)
			{
				this.UnhookPropertyChanged(base[index]);
			}
			base.RemoveItem(index);
			this.FireListChanged(ListChangedType.ItemDeleted, index);
		}

		/// <summary>Replaces the item at the specified index with the specified item.</summary>
		/// <param name="index">The zero-based index of the item to replace.</param>
		/// <param name="item">The new value for the item at the specified index. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06001DA6 RID: 7590 RVA: 0x00069531 File Offset: 0x00067731
		protected override void SetItem(int index, T item)
		{
			if (this.raiseItemChangedEvents)
			{
				this.UnhookPropertyChanged(base[index]);
			}
			base.SetItem(index, item);
			if (this.raiseItemChangedEvents)
			{
				this.HookPropertyChanged(item);
			}
			this.FireListChanged(ListChangedType.ItemChanged, index);
		}

		/// <summary>Discards a pending new item.</summary>
		/// <param name="itemIndex">The index of the of the new item to be added </param>
		// Token: 0x06001DA7 RID: 7591 RVA: 0x00069567 File Offset: 0x00067767
		public virtual void CancelNew(int itemIndex)
		{
			if (this.addNewPos >= 0 && this.addNewPos == itemIndex)
			{
				this.RemoveItem(this.addNewPos);
				this.addNewPos = -1;
			}
		}

		/// <summary>Commits a pending new item to the collection.</summary>
		/// <param name="itemIndex">The index of the new item to be added.</param>
		// Token: 0x06001DA8 RID: 7592 RVA: 0x0006958E File Offset: 0x0006778E
		public virtual void EndNew(int itemIndex)
		{
			if (this.addNewPos >= 0 && this.addNewPos == itemIndex)
			{
				this.addNewPos = -1;
			}
		}

		/// <summary>Adds a new item to the collection.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.BindingSource.AllowNew" /> property is set to false. -or-A public default constructor could not be found for the current item type.</exception>
		// Token: 0x06001DA9 RID: 7593 RVA: 0x000695A9 File Offset: 0x000677A9
		public T AddNew()
		{
			return (T)((object)((IBindingList)this).AddNew());
		}

		/// <summary>Adds a new item to the list. For more information, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06001DAA RID: 7594 RVA: 0x000695B8 File Offset: 0x000677B8
		object IBindingList.AddNew()
		{
			object obj = this.AddNewCore();
			this.addNewPos = ((obj != null) ? base.IndexOf((T)((object)obj)) : (-1));
			return obj;
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x000695E5 File Offset: 0x000677E5
		private bool AddingNewHandled
		{
			get
			{
				return this._onAddingNew != null && this._onAddingNew.GetInvocationList().Length != 0;
			}
		}

		/// <summary>Adds a new item to the end of the collection.</summary>
		/// <returns>The item that was added to the collection.</returns>
		/// <exception cref="T:System.InvalidCastException">The new item is not the same type as the objects contained in the <see cref="T:System.ComponentModel.BindingList`1" />.</exception>
		// Token: 0x06001DAC RID: 7596 RVA: 0x00069600 File Offset: 0x00067800
		protected virtual object AddNewCore()
		{
			object obj = this.FireAddingNew();
			if (obj == null)
			{
				obj = SecurityUtils.SecureCreateInstance(typeof(T));
			}
			base.Add((T)((object)obj));
			return obj;
		}

		/// <summary>Gets or sets a value indicating whether you can add items to the list using the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method.</summary>
		/// <returns>true if you can add items to the list with the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method; otherwise, false. The default depends on the underlying type contained in the list.</returns>
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x00069634 File Offset: 0x00067834
		// (set) Token: 0x06001DAE RID: 7598 RVA: 0x00069653 File Offset: 0x00067853
		public bool AllowNew
		{
			get
			{
				if (this.userSetAllowNew || this.allowNew)
				{
					return this.allowNew;
				}
				return this.AddingNewHandled;
			}
			set
			{
				bool flag = this.AllowNew;
				this.userSetAllowNew = true;
				this.allowNew = value;
				if (flag != value)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether new items can be added to the list using the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method.</summary>
		/// <returns>true if you can add items to the list with the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method; otherwise, false. The default depends on the underlying type contained in the list.</returns>
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x00069674 File Offset: 0x00067874
		bool IBindingList.AllowNew
		{
			get
			{
				return this.AllowNew;
			}
		}

		/// <summary>Gets or sets a value indicating whether items in the list can be edited.</summary>
		/// <returns>true if list items can be edited; otherwise, false. The default is true.</returns>
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x0006967C File Offset: 0x0006787C
		// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x00069684 File Offset: 0x00067884
		public bool AllowEdit
		{
			get
			{
				return this.allowEdit;
			}
			set
			{
				if (this.allowEdit != value)
				{
					this.allowEdit = value;
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether items in the list can be edited.</summary>
		/// <returns>true if list items can be edited; otherwise, false. The default is true.</returns>
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x0006969E File Offset: 0x0006789E
		bool IBindingList.AllowEdit
		{
			get
			{
				return this.AllowEdit;
			}
		}

		/// <summary>Gets or sets a value indicating whether you can remove items from the collection. </summary>
		/// <returns>true if you can remove items from the list with the <see cref="M:System.ComponentModel.BindingList`1.RemoveItem(System.Int32)" /> method otherwise, false. The default is true.</returns>
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x000696A6 File Offset: 0x000678A6
		// (set) Token: 0x06001DB4 RID: 7604 RVA: 0x000696AE File Offset: 0x000678AE
		public bool AllowRemove
		{
			get
			{
				return this.allowRemove;
			}
			set
			{
				if (this.allowRemove != value)
				{
					this.allowRemove = value;
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether items can be removed from the list.</summary>
		/// <returns>true if you can remove items from the list with the <see cref="M:System.ComponentModel.BindingList`1.RemoveItem(System.Int32)" /> method; otherwise, false. The default is true.</returns>
		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x000696C8 File Offset: 0x000678C8
		bool IBindingList.AllowRemove
		{
			get
			{
				return this.AllowRemove;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</summary>
		/// <returns>true if a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or when an item changes; otherwise, false.</returns>
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x000696D0 File Offset: 0x000678D0
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return this.SupportsChangeNotificationCore;
			}
		}

		/// <summary>Gets a value indicating whether <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events are enabled.</summary>
		/// <returns>true if <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events are supported; otherwise, false. The default is true.</returns>
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x0000390E File Offset: 0x00001B0E
		protected virtual bool SupportsChangeNotificationCore
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</summary>
		/// <returns>true if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method; otherwise, false.</returns>
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x000696D8 File Offset: 0x000678D8
		bool IBindingList.SupportsSearching
		{
			get
			{
				return this.SupportsSearchingCore;
			}
		}

		/// <summary>Gets a value indicating whether the list supports searching.</summary>
		/// <returns>true if the list supports searching; otherwise, false. The default is false.</returns>
		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual bool SupportsSearchingCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</summary>
		/// <returns>true if the list supports sorting; otherwise, false.</returns>
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001DBA RID: 7610 RVA: 0x000696E0 File Offset: 0x000678E0
		bool IBindingList.SupportsSorting
		{
			get
			{
				return this.SupportsSortingCore;
			}
		}

		/// <summary>Gets a value indicating whether the list supports sorting.</summary>
		/// <returns>true if the list supports sorting; otherwise, false. The default is false.</returns>
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual bool SupportsSortingCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</summary>
		/// <returns>true if <see cref="M:System.ComponentModel.IBindingListView.ApplySort(System.ComponentModel.ListSortDescriptionCollection)" /> has been called and <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /> has not been called; otherwise, false.</returns>
		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x000696E8 File Offset: 0x000678E8
		bool IBindingList.IsSorted
		{
			get
			{
				return this.IsSortedCore;
			}
		}

		/// <summary>Gets a value indicating whether the list is sorted. </summary>
		/// <returns>true if the list is sorted; otherwise, false. The default is false.</returns>
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual bool IsSortedCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</returns>
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x000696F0 File Offset: 0x000678F0
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				return this.SortPropertyCore;
			}
		}

		/// <summary>Gets the property descriptor that is used for sorting the list if sorting is implemented in a derived class; otherwise, returns null. </summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> used for sorting the list.</returns>
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x00002F6A File Offset: 0x0000116A
		protected virtual PropertyDescriptor SortPropertyCore
		{
			get
			{
				return null;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</returns>
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x000696F8 File Offset: 0x000678F8
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				return this.SortDirectionCore;
			}
		}

		/// <summary>Gets the direction the list is sorted.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. The default is <see cref="F:System.ComponentModel.ListSortDirection.Ascending" />. </returns>
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual ListSortDirection SortDirectionCore
		{
			get
			{
				return ListSortDirection.Ascending;
			}
		}

		/// <summary>Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor" /> and a <see cref="T:System.ComponentModel.ListSortDirection" />. For a complete description of this member, see <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />. </summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to sort by.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</param>
		// Token: 0x06001DC2 RID: 7618 RVA: 0x00069700 File Offset: 0x00067900
		void IBindingList.ApplySort(PropertyDescriptor prop, ListSortDirection direction)
		{
			this.ApplySortCore(prop, direction);
		}

		/// <summary>Sorts the items if overridden in a derived class; otherwise, throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that specifies the property to sort on.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" />  values.</param>
		/// <exception cref="T:System.NotSupportedException">Method is not overridden in a derived class. </exception>
		// Token: 0x06001DC3 RID: 7619 RVA: 0x000044FA File Offset: 0x000026FA
		protected virtual void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /></summary>
		// Token: 0x06001DC4 RID: 7620 RVA: 0x0006970A File Offset: 0x0006790A
		void IBindingList.RemoveSort()
		{
			this.RemoveSortCore();
		}

		/// <summary>Removes any sort applied with <see cref="M:System.ComponentModel.BindingList`1.ApplySortCore(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" /> if sorting is implemented in a derived class; otherwise, raises <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">Method is not overridden in a derived class. </exception>
		// Token: 0x06001DC5 RID: 7621 RVA: 0x000044FA File Offset: 0x000026FA
		protected virtual void RemoveSortCore()
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" />.</summary>
		/// <returns>The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" /> .</returns>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search on.</param>
		/// <param name="key">The value of the <paramref name="property" /> parameter to search for.</param>
		// Token: 0x06001DC6 RID: 7622 RVA: 0x00069712 File Offset: 0x00067912
		int IBindingList.Find(PropertyDescriptor prop, object key)
		{
			return this.FindCore(prop, key);
		}

		/// <summary>Searches for the index of the item that has the specified property descriptor with the specified value, if searching is implemented in a derived class; otherwise, a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The zero-based index of the item that matches the property descriptor and contains the specified value.</returns>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search for.</param>
		/// <param name="key">The value of <paramref name="property" /> to match.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.ComponentModel.BindingList`1.FindCore(System.ComponentModel.PropertyDescriptor,System.Object)" /> is not overridden in a derived class.</exception>
		// Token: 0x06001DC7 RID: 7623 RVA: 0x000044FA File Offset: 0x000026FA
		protected virtual int FindCore(PropertyDescriptor prop, object key)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add as a search criteria. </param>
		// Token: 0x06001DC8 RID: 7624 RVA: 0x00003917 File Offset: 0x00001B17
		void IBindingList.AddIndex(PropertyDescriptor prop)
		{
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching.</param>
		// Token: 0x06001DC9 RID: 7625 RVA: 0x00003917 File Offset: 0x00001B17
		void IBindingList.RemoveIndex(PropertyDescriptor prop)
		{
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x0006971C File Offset: 0x0006791C
		private void HookPropertyChanged(T item)
		{
			INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				if (this._propertyChangedEventHandler == null)
				{
					this._propertyChangedEventHandler = new PropertyChangedEventHandler(this.Child_PropertyChanged);
				}
				notifyPropertyChanged.PropertyChanged += this._propertyChangedEventHandler;
			}
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00069760 File Offset: 0x00067960
		private void UnhookPropertyChanged(T item)
		{
			INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
			if (notifyPropertyChanged != null && this._propertyChangedEventHandler != null)
			{
				notifyPropertyChanged.PropertyChanged -= this._propertyChangedEventHandler;
			}
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00069790 File Offset: 0x00067990
		private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this.RaiseListChangedEvents)
			{
				if (sender == null || e == null || string.IsNullOrEmpty(e.PropertyName))
				{
					this.ResetBindings();
					return;
				}
				T t;
				try
				{
					t = (T)((object)sender);
				}
				catch (InvalidCastException)
				{
					this.ResetBindings();
					return;
				}
				int num = this._lastChangeIndex;
				if (num >= 0 && num < base.Count)
				{
					T t2 = base[num];
					if (t2.Equals(t))
					{
						goto IL_007B;
					}
				}
				num = base.IndexOf(t);
				this._lastChangeIndex = num;
				IL_007B:
				if (num == -1)
				{
					this.UnhookPropertyChanged(t);
					this.ResetBindings();
					return;
				}
				if (this._itemTypeProperties == null)
				{
					this._itemTypeProperties = TypeDescriptor.GetProperties(typeof(T));
				}
				PropertyDescriptor propertyDescriptor = this._itemTypeProperties.Find(e.PropertyName, true);
				ListChangedEventArgs e2 = new ListChangedEventArgs(ListChangedType.ItemChanged, num, propertyDescriptor);
				this.OnListChanged(e2);
			}
		}

		/// <summary>Gets a value indicating whether item property value changes raise <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events of type <see cref="F:System.ComponentModel.ListChangedType.ItemChanged" />. This member cannot be overridden in a derived class.</summary>
		/// <returns>true if the list type implements <see cref="T:System.ComponentModel.INotifyPropertyChanged" />, otherwise, false. The default is false.</returns>
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001DCD RID: 7629 RVA: 0x0006987C File Offset: 0x00067A7C
		bool IRaiseItemChangedEvents.RaisesItemChangedEvents
		{
			get
			{
				return this.raiseItemChangedEvents;
			}
		}

		// Token: 0x04000F0B RID: 3851
		private int addNewPos = -1;

		// Token: 0x04000F0C RID: 3852
		private bool raiseListChangedEvents = true;

		// Token: 0x04000F0D RID: 3853
		private bool raiseItemChangedEvents;

		// Token: 0x04000F0E RID: 3854
		[NonSerialized]
		private PropertyDescriptorCollection _itemTypeProperties;

		// Token: 0x04000F0F RID: 3855
		[NonSerialized]
		private PropertyChangedEventHandler _propertyChangedEventHandler;

		// Token: 0x04000F10 RID: 3856
		[NonSerialized]
		private AddingNewEventHandler _onAddingNew;

		// Token: 0x04000F11 RID: 3857
		[NonSerialized]
		private ListChangedEventHandler _onListChanged;

		// Token: 0x04000F12 RID: 3858
		[NonSerialized]
		private int _lastChangeIndex = -1;

		// Token: 0x04000F13 RID: 3859
		private bool allowNew = true;

		// Token: 0x04000F14 RID: 3860
		private bool allowEdit = true;

		// Token: 0x04000F15 RID: 3861
		private bool allowRemove = true;

		// Token: 0x04000F16 RID: 3862
		private bool userSetAllowNew;
	}
}
