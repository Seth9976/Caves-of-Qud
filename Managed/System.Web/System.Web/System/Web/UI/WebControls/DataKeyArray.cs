using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200037B RID: 891
	public sealed class DataKeyArray : ICollection, IEnumerable, IStateManager
	{
		// Token: 0x06002204 RID: 8708 RVA: 0x00055A36 File Offset: 0x00053C36
		internal DataKeyArray(IList keys)
		{
			this.keys = keys;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> class.</summary>
		/// <param name="keys">An <see cref="T:System.Collections.ArrayList" /> of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects with which to populate the collection.</param>
		// Token: 0x06002205 RID: 8709 RVA: 0x00055A36 File Offset: 0x00053C36
		public DataKeyArray(ArrayList keys)
		{
			this.keys = keys;
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x00055A45 File Offset: 0x00053C45
		public int Count
		{
			get
			{
				return this.keys.Count;
			}
		}

		/// <summary>Gets a value indicating whether the items in the collection can be modified.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DataKey" /> object from the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.DataKey" /> at the specified index in the collection.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.DataKey" /> to retrieve from the collection.</param>
		// Token: 0x17000ACB RID: 2763
		public DataKey this[int index]
		{
			get
			{
				return (DataKey)this.keys[index];
			}
		}

		/// <summary>Gets the object used to synchronize access to the collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies all the items from this collection to the specified array of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects, starting at the specified index in the array.</summary>
		/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects that receives the copied items from the collection.</param>
		/// <param name="index">The first index in the specified array to receive the copied contents.</param>
		// Token: 0x0600220B RID: 8715 RVA: 0x00055A68 File Offset: 0x00053C68
		public void CopyTo(DataKey[] array, int index)
		{
			foreach (object obj in this)
			{
				DataKey dataKey = (DataKey)obj;
				array[index++] = dataKey;
			}
		}

		/// <summary>Copies all the items from this collection to the specified <see cref="T:System.Array" />, starting at the specified index in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the collection.</param>
		/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the copied contents.</param>
		// Token: 0x0600220C RID: 8716 RVA: 0x00055AC0 File Offset: 0x00053CC0
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object obj in this)
			{
				array.SetValue(obj, index++);
			}
		}

		/// <summary>Returns an enumerator that contains all <see cref="T:System.Web.UI.WebControls.DataKey" /> objects in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.Web.UI.WebControls.DataKey" /> objects in the collection.</returns>
		// Token: 0x0600220D RID: 8717 RVA: 0x00055B18 File Offset: 0x00053D18
		public IEnumerator GetEnumerator()
		{
			return this.keys.GetEnumerator();
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> object.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" />.</param>
		// Token: 0x0600220E RID: 8718 RVA: 0x00055B28 File Offset: 0x00053D28
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			object[] array = (object[])savedState;
			int num = 0;
			while (num < array.Length && num < this.keys.Count)
			{
				((IStateManager)this.keys[num]).LoadViewState(array[num]);
				num++;
			}
		}

		/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.DataKeyArray" />.</returns>
		// Token: 0x0600220F RID: 8719 RVA: 0x00055B78 File Offset: 0x00053D78
		object IStateManager.SaveViewState()
		{
			if (this.keys.Count == 0)
			{
				return null;
			}
			object[] array = new object[this.keys.Count];
			for (int i = 0; i < this.keys.Count; i++)
			{
				array[i] = ((IStateManager)this.keys[i]).SaveViewState();
			}
			return array;
		}

		/// <summary>Marks the starting point at which to begin tracking and saving view-state changes to the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> object.</summary>
		// Token: 0x06002210 RID: 8720 RVA: 0x00055BD8 File Offset: 0x00053DD8
		void IStateManager.TrackViewState()
		{
			this.trackViewState = true;
			foreach (object obj in this.keys)
			{
				((IStateManager)obj).TrackViewState();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> object is tracking its view-state changes.</summary>
		/// <returns>true to indicate that the <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> is tracking its view-state changes; otherwise, false.</returns>
		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x00055C38 File Offset: 0x00053E38
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.trackViewState;
			}
		}

		// Token: 0x0400190A RID: 6410
		private IList keys;

		// Token: 0x0400190B RID: 6411
		private bool trackViewState;
	}
}
