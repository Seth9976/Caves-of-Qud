using System;
using System.Collections;
using System.Collections.Generic;
using Internal.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.Oid" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020002B9 RID: 697
	public sealed class OidCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.OidCollection" /> class.</summary>
		// Token: 0x060015D7 RID: 5591 RVA: 0x00057746 File Offset: 0x00055946
		public OidCollection()
		{
			this._list = new List<Oid>();
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.Oid" /> object to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The index of the added <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <param name="oid">The <see cref="T:System.Security.Cryptography.Oid" /> object to add to the collection.</param>
		// Token: 0x060015D8 RID: 5592 RVA: 0x00057759 File Offset: 0x00055959
		public int Add(Oid oid)
		{
			int count = this._list.Count;
			this._list.Add(oid);
			return count;
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.Oid" /> object from the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <param name="index">The location of the <see cref="T:System.Security.Cryptography.Oid" /> object in the collection.</param>
		// Token: 0x17000415 RID: 1045
		public Oid this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		/// <summary>Gets the first <see cref="T:System.Security.Cryptography.Oid" /> object that contains a value of the <see cref="P:System.Security.Cryptography.Oid.Value" /> property or a value of the <see cref="P:System.Security.Cryptography.Oid.FriendlyName" /> property that matches the specified string value from the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <param name="oid">A string that represents a <see cref="P:System.Security.Cryptography.Oid.Value" /> property or a <see cref="P:System.Security.Cryptography.Oid.FriendlyName" /> property.</param>
		// Token: 0x17000416 RID: 1046
		public Oid this[string oid]
		{
			get
			{
				string text = OidLookup.ToOid(oid, OidGroup.All, false);
				if (text == null)
				{
					text = oid;
				}
				foreach (Oid oid2 in this._list)
				{
					if (oid2.Value == text)
					{
						return oid2;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.Oid" /> objects in a collection. </summary>
		/// <returns>The number of <see cref="T:System.Security.Cryptography.Oid" /> objects in a collection.</returns>
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x000577F0 File Offset: 0x000559F0
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidEnumerator" /> object.</returns>
		// Token: 0x060015DC RID: 5596 RVA: 0x000577FD File Offset: 0x000559FD
		public OidEnumerator GetEnumerator()
		{
			return new OidEnumerator(this);
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the collection.</returns>
		// Token: 0x060015DD RID: 5597 RVA: 0x00057805 File Offset: 0x00055A05
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.OidCollection" /> object into an array.</summary>
		/// <param name="array">The array to copy the <see cref="T:System.Security.Cryptography.OidCollection" /> object to.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> cannot be a multidimensional array.-or-The length of <paramref name="array" /> is an invalid offset length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="index" /> is out range.</exception>
		// Token: 0x060015DE RID: 5598 RVA: 0x00057810 File Offset: 0x00055A10
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (index + this.Count > array.Length)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.OidCollection" /> object into an array.</summary>
		/// <param name="array">The array to copy the <see cref="T:System.Security.Cryptography.OidCollection" /> object into.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		// Token: 0x060015DF RID: 5599 RVA: 0x0005789B File Offset: 0x00055A9B
		public void CopyTo(Oid[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this._list.CopyTo(array, index);
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object is thread safe.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</returns>
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x000075E1 File Offset: 0x000057E1
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000C57 RID: 3159
		private readonly List<Oid> _list;
	}
}
