using System;
using System.Collections;
using System.Collections.Generic;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.EventDescriptor" /> objects.</summary>
	// Token: 0x020003AA RID: 938
	public class EventDescriptorCollection : ICollection, IEnumerable, IList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptorCollection" /> class with the given array of <see cref="T:System.ComponentModel.EventDescriptor" /> objects.</summary>
		/// <param name="events">An array of type <see cref="T:System.ComponentModel.EventDescriptor" /> that provides the events for this collection. </param>
		// Token: 0x06001E93 RID: 7827 RVA: 0x0006C2CC File Offset: 0x0006A4CC
		public EventDescriptorCollection(EventDescriptor[] events)
		{
			if (events == null)
			{
				this._events = Array.Empty<EventDescriptor>();
			}
			else
			{
				this._events = events;
				this.Count = events.Length;
			}
			this._eventsOwned = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptorCollection" /> class with the given array of <see cref="T:System.ComponentModel.EventDescriptor" /> objects. The collection is optionally read-only.</summary>
		/// <param name="events">An array of type <see cref="T:System.ComponentModel.EventDescriptor" /> that provides the events for this collection. </param>
		/// <param name="readOnly">true to specify a read-only collection; otherwise, false.</param>
		// Token: 0x06001E94 RID: 7828 RVA: 0x0006C2FB File Offset: 0x0006A4FB
		public EventDescriptorCollection(EventDescriptor[] events, bool readOnly)
			: this(events)
		{
			this._readOnly = readOnly;
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0006C30B File Offset: 0x0006A50B
		private EventDescriptorCollection(EventDescriptor[] events, int eventCount, string[] namedSort, IComparer comparer)
		{
			this._eventsOwned = false;
			if (namedSort != null)
			{
				this._namedSort = (string[])namedSort.Clone();
			}
			this._comparer = comparer;
			this._events = events;
			this.Count = eventCount;
			this._needSort = true;
		}

		/// <summary>Gets the number of event descriptors in the collection.</summary>
		/// <returns>The number of event descriptors in the collection.</returns>
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x0006C34B File Offset: 0x0006A54B
		// (set) Token: 0x06001E97 RID: 7831 RVA: 0x0006C353 File Offset: 0x0006A553
		public int Count { get; private set; }

		/// <summary>Gets or sets the event with the specified index number.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified index number.</returns>
		/// <param name="index">The zero-based index number of the <see cref="T:System.ComponentModel.EventDescriptor" /> to get or set. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is not a valid index for <see cref="P:System.ComponentModel.EventDescriptorCollection.Item(System.Int32)" />. </exception>
		// Token: 0x1700062F RID: 1583
		public virtual EventDescriptor this[int index]
		{
			get
			{
				if (index >= this.Count)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsureEventsOwned();
				return this._events[index];
			}
		}

		/// <summary>Gets or sets the event with the specified name.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified name, or null if the event does not exist.</returns>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.EventDescriptor" /> to get or set. </param>
		// Token: 0x17000630 RID: 1584
		public virtual EventDescriptor this[string name]
		{
			get
			{
				return this.Find(name, false);
			}
		}

		/// <summary>Adds an <see cref="T:System.ComponentModel.EventDescriptor" /> to the end of the collection.</summary>
		/// <returns>The position of the <see cref="T:System.ComponentModel.EventDescriptor" /> within the collection.</returns>
		/// <param name="value">An <see cref="T:System.ComponentModel.EventDescriptor" /> to add to the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001E9A RID: 7834 RVA: 0x0006C388 File Offset: 0x0006A588
		public int Add(EventDescriptor value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.Count + 1);
			EventDescriptor[] events = this._events;
			int count = this.Count;
			this.Count = count + 1;
			events[count] = value;
			return this.Count - 1;
		}

		/// <summary>Removes all objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001E9B RID: 7835 RVA: 0x0006C3D2 File Offset: 0x0006A5D2
		public void Clear()
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			this.Count = 0;
		}

		/// <summary>Returns whether the collection contains the given <see cref="T:System.ComponentModel.EventDescriptor" />.</summary>
		/// <returns>true if the collection contains the <paramref name="value" /> parameter given; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to find within the collection. </param>
		// Token: 0x06001E9C RID: 7836 RVA: 0x0006C3E9 File Offset: 0x0006A5E9
		public bool Contains(EventDescriptor value)
		{
			return this.IndexOf(value) >= 0;
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06001E9D RID: 7837 RVA: 0x0006C3F8 File Offset: 0x0006A5F8
		void ICollection.CopyTo(Array array, int index)
		{
			this.EnsureEventsOwned();
			Array.Copy(this._events, 0, array, index, this.Count);
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x0006C414 File Offset: 0x0006A614
		private void EnsureEventsOwned()
		{
			if (!this._eventsOwned)
			{
				this._eventsOwned = true;
				if (this._events != null)
				{
					EventDescriptor[] array = new EventDescriptor[this.Count];
					Array.Copy(this._events, 0, array, 0, this.Count);
					this._events = array;
				}
			}
			if (this._needSort)
			{
				this._needSort = false;
				this.InternalSort(this._namedSort);
			}
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x0006C47C File Offset: 0x0006A67C
		private void EnsureSize(int sizeNeeded)
		{
			if (sizeNeeded <= this._events.Length)
			{
				return;
			}
			if (this._events.Length == 0)
			{
				this.Count = 0;
				this._events = new EventDescriptor[sizeNeeded];
				return;
			}
			this.EnsureEventsOwned();
			EventDescriptor[] array = new EventDescriptor[Math.Max(sizeNeeded, this._events.Length * 2)];
			Array.Copy(this._events, 0, array, 0, this.Count);
			this._events = array;
		}

		/// <summary>Gets the description of the event with the specified name in the collection.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified name, or null if the event does not exist.</returns>
		/// <param name="name">The name of the event to get from the collection. </param>
		/// <param name="ignoreCase">true if you want to ignore the case of the event; otherwise, false. </param>
		// Token: 0x06001EA0 RID: 7840 RVA: 0x0006C4EC File Offset: 0x0006A6EC
		public virtual EventDescriptor Find(string name, bool ignoreCase)
		{
			EventDescriptor eventDescriptor = null;
			if (ignoreCase)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (string.Equals(this._events[i].Name, name, StringComparison.OrdinalIgnoreCase))
					{
						eventDescriptor = this._events[i];
						break;
					}
				}
			}
			else
			{
				for (int j = 0; j < this.Count; j++)
				{
					if (string.Equals(this._events[j].Name, name, StringComparison.Ordinal))
					{
						eventDescriptor = this._events[j];
						break;
					}
				}
			}
			return eventDescriptor;
		}

		/// <summary>Returns the index of the given <see cref="T:System.ComponentModel.EventDescriptor" />.</summary>
		/// <returns>The index of the given <see cref="T:System.ComponentModel.EventDescriptor" /> within the collection.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to find within the collection. </param>
		// Token: 0x06001EA1 RID: 7841 RVA: 0x0006C565 File Offset: 0x0006A765
		public int IndexOf(EventDescriptor value)
		{
			return Array.IndexOf<EventDescriptor>(this._events, value, 0, this.Count);
		}

		/// <summary>Inserts an <see cref="T:System.ComponentModel.EventDescriptor" /> to the collection at a specified index.</summary>
		/// <param name="index">The index within the collection in which to insert the <paramref name="value" /> parameter. </param>
		/// <param name="value">An <see cref="T:System.ComponentModel.EventDescriptor" /> to insert into the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001EA2 RID: 7842 RVA: 0x0006C57C File Offset: 0x0006A77C
		public void Insert(int index, EventDescriptor value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.Count + 1);
			if (index < this.Count)
			{
				Array.Copy(this._events, index, this._events, index + 1, this.Count - index);
			}
			this._events[index] = value;
			int count = this.Count;
			this.Count = count + 1;
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.EventDescriptor" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to remove from the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001EA3 RID: 7843 RVA: 0x0006C5E4 File Offset: 0x0006A7E4
		public void Remove(EventDescriptor value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			int num = this.IndexOf(value);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.ComponentModel.EventDescriptor" /> at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.ComponentModel.EventDescriptor" /> to remove. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001EA4 RID: 7844 RVA: 0x0006C614 File Offset: 0x0006A814
		public void RemoveAt(int index)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			if (index < this.Count - 1)
			{
				Array.Copy(this._events, index + 1, this._events, index, this.Count - index - 1);
			}
			this._events[this.Count - 1] = null;
			int count = this.Count;
			this.Count = count - 1;
		}

		/// <summary>Gets an enumerator for this <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</summary>
		/// <returns>An enumerator that implements <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06001EA5 RID: 7845 RVA: 0x0006C679 File Offset: 0x0006A879
		public IEnumerator GetEnumerator()
		{
			if (this._events.Length == this.Count)
			{
				return this._events.GetEnumerator();
			}
			return new EventDescriptorCollection.ArraySubsetEnumerator(this._events, this.Count);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, using the default sort for this collection, which is usually alphabetical.</summary>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		// Token: 0x06001EA6 RID: 7846 RVA: 0x0006C6A8 File Offset: 0x0006A8A8
		public virtual EventDescriptorCollection Sort()
		{
			return new EventDescriptorCollection(this._events, this.Count, this._namedSort, this._comparer);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, given a specified sort order.</summary>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in the collection. </param>
		// Token: 0x06001EA7 RID: 7847 RVA: 0x0006C6C7 File Offset: 0x0006A8C7
		public virtual EventDescriptorCollection Sort(string[] names)
		{
			return new EventDescriptorCollection(this._events, this.Count, names, this._comparer);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, given a specified sort order and an <see cref="T:System.Collections.IComparer" />.</summary>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in the collection. </param>
		/// <param name="comparer">An <see cref="T:System.Collections.IComparer" /> to use to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection. </param>
		// Token: 0x06001EA8 RID: 7848 RVA: 0x0006C6E1 File Offset: 0x0006A8E1
		public virtual EventDescriptorCollection Sort(string[] names, IComparer comparer)
		{
			return new EventDescriptorCollection(this._events, this.Count, names, comparer);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		/// <param name="comparer">An <see cref="T:System.Collections.IComparer" /> to use to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection. </param>
		// Token: 0x06001EA9 RID: 7849 RVA: 0x0006C6F6 File Offset: 0x0006A8F6
		public virtual EventDescriptorCollection Sort(IComparer comparer)
		{
			return new EventDescriptorCollection(this._events, this.Count, this._namedSort, comparer);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection. </param>
		// Token: 0x06001EAA RID: 7850 RVA: 0x0006C710 File Offset: 0x0006A910
		protected void InternalSort(string[] names)
		{
			if (this._events.Length == 0)
			{
				return;
			}
			this.InternalSort(this._comparer);
			if (names != null && names.Length != 0)
			{
				List<EventDescriptor> list = new List<EventDescriptor>(this._events);
				int num = 0;
				int num2 = this._events.Length;
				for (int i = 0; i < names.Length; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						EventDescriptor eventDescriptor = list[j];
						if (eventDescriptor != null && eventDescriptor.Name.Equals(names[i]))
						{
							this._events[num++] = eventDescriptor;
							list[j] = null;
							break;
						}
					}
				}
				for (int k = 0; k < num2; k++)
				{
					if (list[k] != null)
					{
						this._events[num++] = list[k];
					}
				}
			}
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="sorter">A comparer to use to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection. </param>
		// Token: 0x06001EAB RID: 7851 RVA: 0x0006C7DB File Offset: 0x0006A9DB
		protected void InternalSort(IComparer sorter)
		{
			if (sorter == null)
			{
				TypeDescriptor.SortDescriptorArray(this);
				return;
			}
			Array.Sort(this._events, sorter);
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized.</summary>
		/// <returns>true if access to the collection is synchronized; otherwise, false.</returns>
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001EAC RID: 7852 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x00002F6A File Offset: 0x0000116A
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001EAE RID: 7854 RVA: 0x0006C7F3 File Offset: 0x0006A9F3
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001EAF RID: 7855 RVA: 0x0006C7FB File Offset: 0x0006A9FB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0. -or-<paramref name="index" /> is equal to or greater than <see cref="P:System.ComponentModel.EventDescriptorCollection.Count" />.</exception>
		// Token: 0x17000634 RID: 1588
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException();
				}
				if (index >= this.Count)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsureEventsOwned();
				this._events[index] = (EventDescriptor)value;
			}
		}

		/// <summary>Adds an item to the collection.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001EB2 RID: 7858 RVA: 0x0006C83F File Offset: 0x0006AA3F
		int IList.Add(object value)
		{
			return this.Add((EventDescriptor)value);
		}

		/// <summary>Determines whether the collection contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x06001EB3 RID: 7859 RVA: 0x0006C84D File Offset: 0x0006AA4D
		bool IList.Contains(object value)
		{
			return this.Contains((EventDescriptor)value);
		}

		/// <summary>Removes all the items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001EB4 RID: 7860 RVA: 0x0006C85B File Offset: 0x0006AA5B
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines the index of a specific item in the collection.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x06001EB5 RID: 7861 RVA: 0x0006C863 File Offset: 0x0006AA63
		int IList.IndexOf(object value)
		{
			return this.IndexOf((EventDescriptor)value);
		}

		/// <summary>Inserts an item to the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001EB6 RID: 7862 RVA: 0x0006C871 File Offset: 0x0006AA71
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (EventDescriptor)value);
		}

		/// <summary>Removes the first occurrence of a specific object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001EB7 RID: 7863 RVA: 0x0006C880 File Offset: 0x0006AA80
		void IList.Remove(object value)
		{
			this.Remove((EventDescriptor)value);
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001EB8 RID: 7864 RVA: 0x0006C88E File Offset: 0x0006AA8E
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x0006C897 File Offset: 0x0006AA97
		bool IList.IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x0006C897 File Offset: 0x0006AA97
		bool IList.IsFixedSize
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x04000F5B RID: 3931
		private EventDescriptor[] _events;

		// Token: 0x04000F5C RID: 3932
		private string[] _namedSort;

		// Token: 0x04000F5D RID: 3933
		private readonly IComparer _comparer;

		// Token: 0x04000F5E RID: 3934
		private bool _eventsOwned;

		// Token: 0x04000F5F RID: 3935
		private bool _needSort;

		// Token: 0x04000F60 RID: 3936
		private readonly bool _readOnly;

		/// <summary>Specifies an empty collection to use, rather than creating a new one with no items. This static field is read-only.</summary>
		// Token: 0x04000F61 RID: 3937
		public static readonly EventDescriptorCollection Empty = new EventDescriptorCollection(null, true);

		// Token: 0x020003AB RID: 939
		private class ArraySubsetEnumerator : IEnumerator
		{
			// Token: 0x06001EBC RID: 7868 RVA: 0x0006C8AD File Offset: 0x0006AAAD
			public ArraySubsetEnumerator(Array array, int count)
			{
				this._array = array;
				this._total = count;
				this._current = -1;
			}

			// Token: 0x06001EBD RID: 7869 RVA: 0x0006C8CA File Offset: 0x0006AACA
			public bool MoveNext()
			{
				if (this._current < this._total - 1)
				{
					this._current++;
					return true;
				}
				return false;
			}

			// Token: 0x06001EBE RID: 7870 RVA: 0x0006C8ED File Offset: 0x0006AAED
			public void Reset()
			{
				this._current = -1;
			}

			// Token: 0x17000637 RID: 1591
			// (get) Token: 0x06001EBF RID: 7871 RVA: 0x0006C8F6 File Offset: 0x0006AAF6
			public object Current
			{
				get
				{
					if (this._current == -1)
					{
						throw new InvalidOperationException();
					}
					return this._array.GetValue(this._current);
				}
			}

			// Token: 0x04000F63 RID: 3939
			private readonly Array _array;

			// Token: 0x04000F64 RID: 3940
			private readonly int _total;

			// Token: 0x04000F65 RID: 3941
			private int _current;
		}
	}
}
