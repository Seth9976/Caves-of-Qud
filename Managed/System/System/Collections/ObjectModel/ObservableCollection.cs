using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace System.Collections.ObjectModel
{
	/// <summary>Represents a dynamic data collection that provides notifications when items get added, removed, or when the whole list is refreshed.</summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	// Token: 0x02000498 RID: 1176
	[DebuggerTypeProxy(typeof(global::System.Collections.Generic.CollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class ObservableCollection<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class.</summary>
		// Token: 0x0600256D RID: 9581 RVA: 0x00083396 File Offset: 0x00081596
		public ObservableCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class that contains elements copied from the specified collection.</summary>
		/// <param name="collection">The collection from which the elements are copied.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collection" /> parameter cannot be null.</exception>
		// Token: 0x0600256E RID: 9582 RVA: 0x0008339E File Offset: 0x0008159E
		public ObservableCollection(IEnumerable<T> collection)
			: base(ObservableCollection<T>.CreateCopy(collection, "collection"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class that contains elements copied from the specified list.</summary>
		/// <param name="list">The list from which the elements are copied.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="list" /> parameter cannot be null.</exception>
		// Token: 0x0600256F RID: 9583 RVA: 0x000833B1 File Offset: 0x000815B1
		public ObservableCollection(List<T> list)
			: base(ObservableCollection<T>.CreateCopy(list, "list"))
		{
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000833C4 File Offset: 0x000815C4
		private static List<T> CreateCopy(IEnumerable<T> collection, string paramName)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(paramName);
			}
			return new List<T>(collection);
		}

		/// <summary>Moves the item at the specified index to a new location in the collection.</summary>
		/// <param name="oldIndex">The zero-based index specifying the location of the item to be moved.</param>
		/// <param name="newIndex">The zero-based index specifying the new location of the item.</param>
		// Token: 0x06002571 RID: 9585 RVA: 0x000833D6 File Offset: 0x000815D6
		public void Move(int oldIndex, int newIndex)
		{
			this.MoveItem(oldIndex, newIndex);
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06002572 RID: 9586 RVA: 0x000833E0 File Offset: 0x000815E0
		// (remove) Token: 0x06002573 RID: 9587 RVA: 0x000833E9 File Offset: 0x000815E9
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				this.PropertyChanged += value;
			}
			remove
			{
				this.PropertyChanged -= value;
			}
		}

		/// <summary>Occurs when an item is added, removed, changed, moved, or the entire list is refreshed.</summary>
		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06002574 RID: 9588 RVA: 0x000833F4 File Offset: 0x000815F4
		// (remove) Token: 0x06002575 RID: 9589 RVA: 0x0008342C File Offset: 0x0008162C
		[field: NonSerialized]
		public virtual event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>Removes all items from the collection.</summary>
		// Token: 0x06002576 RID: 9590 RVA: 0x00083461 File Offset: 0x00081661
		protected override void ClearItems()
		{
			this.CheckReentrancy();
			base.ClearItems();
			this.OnCountPropertyChanged();
			this.OnIndexerPropertyChanged();
			this.OnCollectionReset();
		}

		/// <summary>Removes the item at the specified index of the collection.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06002577 RID: 9591 RVA: 0x00083484 File Offset: 0x00081684
		protected override void RemoveItem(int index)
		{
			this.CheckReentrancy();
			T t = base[index];
			base.RemoveItem(index);
			this.OnCountPropertyChanged();
			this.OnIndexerPropertyChanged();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, t, index);
		}

		/// <summary>Inserts an item into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert.</param>
		// Token: 0x06002578 RID: 9592 RVA: 0x000834C0 File Offset: 0x000816C0
		protected override void InsertItem(int index, T item)
		{
			this.CheckReentrancy();
			base.InsertItem(index, item);
			this.OnCountPropertyChanged();
			this.OnIndexerPropertyChanged();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
		}

		/// <summary>Replaces the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to replace.</param>
		/// <param name="item">The new value for the element at the specified index.</param>
		// Token: 0x06002579 RID: 9593 RVA: 0x000834EC File Offset: 0x000816EC
		protected override void SetItem(int index, T item)
		{
			this.CheckReentrancy();
			T t = base[index];
			base.SetItem(index, item);
			this.OnIndexerPropertyChanged();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Replace, t, item, index);
		}

		/// <summary>Moves the item at the specified index to a new location in the collection.</summary>
		/// <param name="oldIndex">The zero-based index specifying the location of the item to be moved.</param>
		/// <param name="newIndex">The zero-based index specifying the new location of the item.</param>
		// Token: 0x0600257A RID: 9594 RVA: 0x0008352C File Offset: 0x0008172C
		protected virtual void MoveItem(int oldIndex, int newIndex)
		{
			this.CheckReentrancy();
			T t = base[oldIndex];
			base.RemoveItem(oldIndex);
			base.InsertItem(newIndex, t);
			this.OnIndexerPropertyChanged();
			this.OnCollectionChanged(NotifyCollectionChangedAction.Move, t, newIndex, oldIndex);
		}

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.PropertyChanged" /> event with the provided arguments.</summary>
		/// <param name="e">Arguments of the event being raised.</param>
		// Token: 0x0600257B RID: 9595 RVA: 0x0008356B File Offset: 0x0008176B
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, e);
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x0600257C RID: 9596 RVA: 0x00083580 File Offset: 0x00081780
		// (remove) Token: 0x0600257D RID: 9597 RVA: 0x000835B8 File Offset: 0x000817B8
		[field: NonSerialized]
		protected virtual event PropertyChangedEventHandler PropertyChanged;

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event with the provided arguments.</summary>
		/// <param name="e">Arguments of the event being raised.</param>
		// Token: 0x0600257E RID: 9598 RVA: 0x000835F0 File Offset: 0x000817F0
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
			if (collectionChanged != null)
			{
				this._blockReentrancyCount++;
				try
				{
					collectionChanged(this, e);
				}
				finally
				{
					this._blockReentrancyCount--;
				}
			}
		}

		/// <summary>Disallows reentrant attempts to change this collection.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that can be used to dispose of the object.</returns>
		// Token: 0x0600257F RID: 9599 RVA: 0x00083640 File Offset: 0x00081840
		protected IDisposable BlockReentrancy()
		{
			this._blockReentrancyCount++;
			return this.EnsureMonitorInitialized();
		}

		/// <summary>Checks for reentrant attempts to change this collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">If there was a call to <see cref="M:System.Collections.ObjectModel.ObservableCollection`1.BlockReentrancy" /> of which the <see cref="T:System.IDisposable" /> return value has not yet been disposed of. Typically, this means when there are additional attempts to change this collection during a <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event. However, it depends on when derived classes choose to call <see cref="M:System.Collections.ObjectModel.ObservableCollection`1.BlockReentrancy" />.</exception>
		// Token: 0x06002580 RID: 9600 RVA: 0x00083656 File Offset: 0x00081856
		protected void CheckReentrancy()
		{
			if (this._blockReentrancyCount > 0)
			{
				NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
				if (collectionChanged != null && collectionChanged.GetInvocationList().Length > 1)
				{
					throw new InvalidOperationException("Cannot change ObservableCollection during a CollectionChanged event.");
				}
			}
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x00083685 File Offset: 0x00081885
		private void OnCountPropertyChanged()
		{
			this.OnPropertyChanged(EventArgsCache.CountPropertyChanged);
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x00083692 File Offset: 0x00081892
		private void OnIndexerPropertyChanged()
		{
			this.OnPropertyChanged(EventArgsCache.IndexerPropertyChanged);
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x0008369F File Offset: 0x0008189F
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000836AF File Offset: 0x000818AF
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x000836C1 File Offset: 0x000818C1
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000836D3 File Offset: 0x000818D3
		private void OnCollectionReset()
		{
			this.OnCollectionChanged(EventArgsCache.ResetCollectionChanged);
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000836E0 File Offset: 0x000818E0
		private ObservableCollection<T>.SimpleMonitor EnsureMonitorInitialized()
		{
			ObservableCollection<T>.SimpleMonitor simpleMonitor;
			if ((simpleMonitor = this._monitor) == null)
			{
				simpleMonitor = (this._monitor = new ObservableCollection<T>.SimpleMonitor(this));
			}
			return simpleMonitor;
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x00083706 File Offset: 0x00081906
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.EnsureMonitorInitialized();
			this._monitor._busyCount = this._blockReentrancyCount;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x00083720 File Offset: 0x00081920
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this._monitor != null)
			{
				this._blockReentrancyCount = this._monitor._busyCount;
				this._monitor._collection = this;
			}
		}

		// Token: 0x040014C9 RID: 5321
		private ObservableCollection<T>.SimpleMonitor _monitor;

		// Token: 0x040014CA RID: 5322
		[NonSerialized]
		private int _blockReentrancyCount;

		// Token: 0x02000499 RID: 1177
		[Serializable]
		private sealed class SimpleMonitor : IDisposable
		{
			// Token: 0x0600258A RID: 9610 RVA: 0x00083747 File Offset: 0x00081947
			public SimpleMonitor(ObservableCollection<T> collection)
			{
				this._collection = collection;
			}

			// Token: 0x0600258B RID: 9611 RVA: 0x00083756 File Offset: 0x00081956
			public void Dispose()
			{
				this._collection._blockReentrancyCount--;
			}

			// Token: 0x040014CD RID: 5325
			internal int _busyCount;

			// Token: 0x040014CE RID: 5326
			[NonSerialized]
			internal ObservableCollection<T> _collection;
		}
	}
}
