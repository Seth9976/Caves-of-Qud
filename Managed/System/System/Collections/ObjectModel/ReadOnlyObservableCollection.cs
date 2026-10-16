using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Collections.ObjectModel
{
	/// <summary>Represents a read-only <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />.</summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	// Token: 0x0200049B RID: 1179
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(global::System.Collections.Generic.CollectionDebugView<>))]
	[Serializable]
	public class ReadOnlyObservableCollection<T> : ReadOnlyCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyObservableCollection`1" /> class that serves as a wrapper around the specified <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />.</summary>
		/// <param name="list">The <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> with which to create this instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyObservableCollection`1" /> class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null.</exception>
		// Token: 0x0600258D RID: 9613 RVA: 0x00083798 File Offset: 0x00081998
		public ReadOnlyObservableCollection(ObservableCollection<T> list)
			: base(list)
		{
			((INotifyCollectionChanged)base.Items).CollectionChanged += this.HandleCollectionChanged;
			((INotifyPropertyChanged)base.Items).PropertyChanged += this.HandlePropertyChanged;
		}

		/// <summary>Occurs when the collection changes.</summary>
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x0600258E RID: 9614 RVA: 0x000837E4 File Offset: 0x000819E4
		// (remove) Token: 0x0600258F RID: 9615 RVA: 0x000837ED File Offset: 0x000819ED
		event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
		{
			add
			{
				this.CollectionChanged += value;
			}
			remove
			{
				this.CollectionChanged -= value;
			}
		}

		/// <summary>Occurs when an item is added or removed.</summary>
		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06002590 RID: 9616 RVA: 0x000837F8 File Offset: 0x000819F8
		// (remove) Token: 0x06002591 RID: 9617 RVA: 0x00083830 File Offset: 0x00081A30
		[field: NonSerialized]
		protected virtual event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ReadOnlyObservableCollection`1.CollectionChanged" /> event using the provided arguments.</summary>
		/// <param name="args">Arguments of the event being raised.</param>
		// Token: 0x06002592 RID: 9618 RVA: 0x00083865 File Offset: 0x00081A65
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, args);
			}
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06002593 RID: 9619 RVA: 0x0008387C File Offset: 0x00081A7C
		// (remove) Token: 0x06002594 RID: 9620 RVA: 0x00083885 File Offset: 0x00081A85
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

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06002595 RID: 9621 RVA: 0x00083890 File Offset: 0x00081A90
		// (remove) Token: 0x06002596 RID: 9622 RVA: 0x000838C8 File Offset: 0x00081AC8
		[field: NonSerialized]
		protected virtual event PropertyChangedEventHandler PropertyChanged;

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ReadOnlyObservableCollection`1.PropertyChanged" /> event using the provided arguments.</summary>
		/// <param name="args">Arguments of the event being raised.</param>
		// Token: 0x06002597 RID: 9623 RVA: 0x000838FD File Offset: 0x00081AFD
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, args);
			}
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x00083914 File Offset: 0x00081B14
		private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			this.OnCollectionChanged(e);
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x0008391D File Offset: 0x00081B1D
		private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.OnPropertyChanged(e);
		}
	}
}
