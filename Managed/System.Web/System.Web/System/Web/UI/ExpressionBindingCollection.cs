using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.ExpressionBinding" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020001C8 RID: 456
	public sealed class ExpressionBindingCollection : ICollection, IEnumerable
	{
		/// <summary>Occurs when the collection of <see cref="T:System.Web.UI.ExpressionBinding" /> objects is changed.</summary>
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600128C RID: 4748 RVA: 0x000307CD File Offset: 0x0002E9CD
		// (remove) Token: 0x0600128D RID: 4749 RVA: 0x000307E0 File Offset: 0x0002E9E0
		public event EventHandler Changed
		{
			add
			{
				this.events.AddHandler(ExpressionBindingCollection.changedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ExpressionBindingCollection.changedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> class.</summary>
		// Token: 0x0600128E RID: 4750 RVA: 0x000307F3 File Offset: 0x0002E9F3
		public ExpressionBindingCollection()
		{
			this.list = new Hashtable();
			this.removed = new ArrayList();
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the <see cref="T:System.Web.UI.ExpressionBindingCollection" />.</returns>
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x0003081C File Offset: 0x0002EA1C
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the collection can be modified.</summary>
		/// <returns>false in all cases. </returns>
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00030829 File Offset: 0x0002EA29
		public bool IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases. </returns>
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x00030836 File Offset: 0x0002EA36
		public bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets an <see cref="T:System.Web.UI.ExpressionBinding" /> object from the collection with the specified <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> value.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ExpressionBinding" /> in the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> with the specified <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" />.</returns>
		/// <param name="propertyName">The <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> of the <see cref="T:System.Web.UI.ExpressionBinding" /> to retrieve.</param>
		// Token: 0x17000609 RID: 1545
		public ExpressionBinding this[string propertyName]
		{
			get
			{
				return this.list[propertyName] as ExpressionBinding;
			}
		}

		/// <summary>Gets a collection of strings representing the names of bindings that have been removed.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the names of bindings that have been removed.</returns>
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x00030856 File Offset: 0x0002EA56
		public ICollection RemovedBindings
		{
			get
			{
				return this.removed;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Web.UI.ExpressionBindingCollection" />.</returns>
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x0003085E File Offset: 0x0002EA5E
		public object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.ExpressionBinding" /> object to the end of the collection.</summary>
		/// <param name="binding">The <see cref="T:System.Web.UI.ExpressionBinding" /> to append to the <see cref="T:System.Web.UI.ExpressionBindingCollection" />.</param>
		// Token: 0x06001295 RID: 4757 RVA: 0x0003086B File Offset: 0x0002EA6B
		public void Add(ExpressionBinding binding)
		{
			this.list.Add(binding.PropertyName, binding);
			this.OnChanged(new EventArgs());
		}

		/// <summary>Removes all the <see cref="T:System.Web.UI.ExpressionBinding" /> objects from the collection.</summary>
		// Token: 0x06001296 RID: 4758 RVA: 0x0003088A File Offset: 0x0002EA8A
		public void Clear()
		{
			this.list.Clear();
			this.removed.Clear();
			this.OnChanged(new EventArgs());
		}

		/// <summary>Determines whether the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> collection contains a specific <see cref="T:System.Web.UI.ExpressionBinding" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.ExpressionBinding" /> is found in the <see cref="T:System.Web.UI.ExpressionBindingCollection" />; otherwise, false.</returns>
		/// <param name="propName">The <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> of the <see cref="T:System.Web.UI.ExpressionBinding" /> to locate in the collection.</param>
		// Token: 0x06001297 RID: 4759 RVA: 0x000308AD File Offset: 0x0002EAAD
		public bool Contains(string propName)
		{
			return this.list.Contains(propName);
		}

		/// <summary>Copies all the <see cref="T:System.Web.UI.ExpressionBinding" /> objects from the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> collection to a one-dimensional array, starting at the specified index in the target array.</summary>
		/// <param name="array">The zero-based array that receives the <see cref="T:System.Web.UI.ExpressionBinding" /> objects copied from the collection.</param>
		/// <param name="index">The position in the target array at which the array starts receiving the copied items.</param>
		// Token: 0x06001298 RID: 4760 RVA: 0x000308BB File Offset: 0x0002EABB
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Copies all the <see cref="T:System.Web.UI.ExpressionBinding" /> objects from the <see cref="T:System.Web.UI.ExpressionBindingCollection" /> collection to a one-dimensional array of <see cref="T:System.Web.UI.ExpressionBinding" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="array">The zero-based array of <see cref="T:System.Web.UI.ExpressionBinding" /> objects that receives the <see cref="T:System.Web.UI.ExpressionBinding" /> objects copied from the collection.</param>
		/// <param name="index">The position in the target array at which the array starts receiving the copied items.</param>
		// Token: 0x06001299 RID: 4761 RVA: 0x000308CC File Offset: 0x0002EACC
		public void CopyTo(ExpressionBinding[] array, int index)
		{
			if (index < 0)
			{
				throw new ArgumentNullException("Index cannot be negative");
			}
			if (index >= array.Length)
			{
				throw new ArgumentException("Index cannot be greater than or equal to length of array passed");
			}
			if (this.list.Count > array.Length - index + 1)
			{
				throw new ArgumentException("Number of elements in source is greater than available space from index to end of destination");
			}
			foreach (object obj in this.list.Keys)
			{
				string text = (string)obj;
				array[index++] = (ExpressionBinding)this.list[text];
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" />-implemented object that can be used to iterate through the <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all the <see cref="T:System.Web.UI.ExpressionBinding" /> objects in the <see cref="T:System.Web.UI.ExpressionBindingCollection" />.</returns>
		// Token: 0x0600129A RID: 4762 RVA: 0x0003097C File Offset: 0x0002EB7C
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.ExpressionBinding" /> object from the collection.</summary>
		/// <param name="binding">The <see cref="T:System.Web.UI.ExpressionBinding" /> to remove from the collection.</param>
		// Token: 0x0600129B RID: 4763 RVA: 0x00030989 File Offset: 0x0002EB89
		public void Remove(ExpressionBinding binding)
		{
			this.Remove(binding.PropertyName, true);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.ExpressionBinding" /> object from the collection.</summary>
		/// <param name="propertyName">The <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> of the <see cref="T:System.Web.UI.ExpressionBinding" /> to remove from the collection.</param>
		// Token: 0x0600129C RID: 4764 RVA: 0x00030998 File Offset: 0x0002EB98
		public void Remove(string propertyName)
		{
			this.Remove(propertyName, true);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.ExpressionBinding" /> object from the collection.</summary>
		/// <param name="propertyName">The <see cref="P:System.Web.UI.ExpressionBinding.PropertyName" /> of the <see cref="T:System.Web.UI.ExpressionBinding" /> to remove from the collection.</param>
		/// <param name="addToRemovedList">true to add the <see cref="T:System.Web.UI.ExpressionBinding" /> to the <see cref="P:System.Web.UI.ExpressionBindingCollection.RemovedBindings" /> collection; otherwise, false.</param>
		// Token: 0x0600129D RID: 4765 RVA: 0x000309A2 File Offset: 0x0002EBA2
		public void Remove(string propertyName, bool addToRemovedList)
		{
			if (addToRemovedList)
			{
				this.removed.Add(string.Empty);
			}
			else
			{
				this.removed.Add(propertyName);
			}
			this.list.Remove(propertyName);
			this.OnChanged(new EventArgs());
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x000309E0 File Offset: 0x0002EBE0
		private void OnChanged(EventArgs e)
		{
			EventHandler eventHandler = this.events[ExpressionBindingCollection.changedEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0400142D RID: 5165
		private static readonly object changedEvent = new object();

		// Token: 0x0400142E RID: 5166
		private Hashtable list;

		// Token: 0x0400142F RID: 5167
		private ArrayList removed;

		// Token: 0x04001430 RID: 5168
		private EventHandlerList events = new EventHandlerList();
	}
}
