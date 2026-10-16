using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>A collection of <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column objects that represent the columns in a <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited. </summary>
	// Token: 0x02000375 RID: 885
	public sealed class DataGridColumnCollection : ICollection, IEnumerable, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that corresponds with this collection. </param>
		/// <param name="columns">A <see cref="T:System.Collections.ArrayList" /> that stores the collection of columns. </param>
		// Token: 0x060021BF RID: 8639 RVA: 0x00054DF8 File Offset: 0x00052FF8
		public DataGridColumnCollection(DataGrid owner, ArrayList columns)
		{
			this.owner = owner;
			this.columns = columns;
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object to the end of the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
		/// <param name="column">The <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object to append to the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
		// Token: 0x060021C0 RID: 8640 RVA: 0x00054E0E File Offset: 0x0005300E
		public void Add(DataGridColumn column)
		{
			this.columns.Add(column);
			column.Set_Owner(this.owner);
			if (this.track)
			{
				((IStateManager)column).TrackViewState();
			}
		}

		/// <summary>Inserts a <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection at the specified index.</summary>
		/// <param name="index">The index location in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> at which to insert the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column. </param>
		/// <param name="column">The <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column to insert in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="column" /> is null.</exception>
		// Token: 0x060021C1 RID: 8641 RVA: 0x00054E37 File Offset: 0x00053037
		public void AddAt(int index, DataGridColumn column)
		{
			this.columns.Insert(index, column);
			column.Set_Owner(this.owner);
			if (this.track)
			{
				((IStateManager)column).TrackViewState();
			}
		}

		/// <summary>Removes all <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column objects from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
		// Token: 0x060021C2 RID: 8642 RVA: 0x00054E60 File Offset: 0x00053060
		public void Clear()
		{
			this.columns.Clear();
		}

		/// <summary>Copies the items from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection to the specified <see cref="T:System.Array" />, starting at the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
		/// <param name="index">The first position in the specified <see cref="T:System.Array" /> to receive the copied contents. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		// Token: 0x060021C3 RID: 8643 RVA: 0x00054E6D File Offset: 0x0005306D
		public void CopyTo(Array array, int index)
		{
			this.columns.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> interface that contains all the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column objects in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> interface that contains all <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column objects in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />.</returns>
		// Token: 0x060021C4 RID: 8644 RVA: 0x00054E7C File Offset: 0x0005307C
		public IEnumerator GetEnumerator()
		{
			return this.columns.GetEnumerator();
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
		/// <returns>The index position of the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. The default value is -1, which indicates that the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived object is not found.</returns>
		/// <param name="column">The <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column to search for in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
		// Token: 0x060021C5 RID: 8645 RVA: 0x00054E89 File Offset: 0x00053089
		public int IndexOf(DataGridColumn column)
		{
			return this.columns.IndexOf(column);
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x00003946 File Offset: 0x00001B46
		[Obsolete("figure out what you need with me")]
		internal void OnColumnsChanged()
		{
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
		/// <param name="column">The <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column to remove from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />. </param>
		// Token: 0x060021C7 RID: 8647 RVA: 0x00054E97 File Offset: 0x00053097
		public void Remove(DataGridColumn column)
		{
			this.columns.Remove(column);
		}

		/// <summary>Removes a <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than <see cref="P:System.Web.UI.WebControls.DataGridColumnCollection.Count" />.</exception>
		// Token: 0x060021C8 RID: 8648 RVA: 0x00054EA5 File Offset: 0x000530A5
		public void RemoveAt(int index)
		{
			this.columns.RemoveAt(index);
		}

		/// <summary>Loads the previously saved state.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that contains the saved view state values for the control.</param>
		// Token: 0x060021C9 RID: 8649 RVA: 0x00054EB4 File Offset: 0x000530B4
		void IStateManager.LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array == null)
			{
				return;
			}
			int num = 0;
			foreach (object obj in this)
			{
				((IStateManager)obj).LoadViewState(array[num++]);
			}
		}

		/// <summary>Returns an object containing state changes.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved view state values for the control.</returns>
		// Token: 0x060021CA RID: 8650 RVA: 0x00054F1C File Offset: 0x0005311C
		object IStateManager.SaveViewState()
		{
			object[] array = new object[this.Count];
			int num = 0;
			foreach (object obj in this)
			{
				IStateManager stateManager = (IStateManager)obj;
				array[num++] = stateManager.SaveViewState();
			}
			object[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Starts tracking state changes.</summary>
		// Token: 0x060021CB RID: 8651 RVA: 0x00054FA8 File Offset: 0x000531A8
		void IStateManager.TrackViewState()
		{
			this.track = true;
			foreach (object obj in this)
			{
				((IStateManager)obj).TrackViewState();
			}
		}

		/// <summary>Gets the number of columns in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
		/// <returns>The number of columns in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" />.</returns>
		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x00055000 File Offset: 0x00053200
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.columns.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is tracking its view-state changes.</summary>
		/// <returns>true if a <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> object is tracking its view-state changes; otherwise, false.</returns>
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x0005500D File Offset: 0x0005320D
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.track;
			}
		}

		/// <summary>Gets a value that indicates whether the columns in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection can be modified.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x00055015 File Offset: 0x00053215
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.columns.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x00055022 File Offset: 0x00053222
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return this.columns.IsSynchronized;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column object from the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection at the specified index.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DataGridColumn" />-derived column in the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> to retrieve. </param>
		// Token: 0x17000AAE RID: 2734
		[Browsable(false)]
		public DataGridColumn this[int index]
		{
			get
			{
				return (DataGridColumn)this.columns[index];
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x00055042 File Offset: 0x00053242
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this.columns.SyncRoot;
			}
		}

		// Token: 0x040018F7 RID: 6391
		private DataGrid owner;

		// Token: 0x040018F8 RID: 6392
		private ArrayList columns;

		// Token: 0x040018F9 RID: 6393
		private bool track;
	}
}
