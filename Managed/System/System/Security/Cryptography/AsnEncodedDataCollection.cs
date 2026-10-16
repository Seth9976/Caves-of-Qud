using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Security.Cryptography
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.AsnEncodedData" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020002B6 RID: 694
	public sealed class AsnEncodedDataCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> class.</summary>
		// Token: 0x060015BA RID: 5562 RVA: 0x00057413 File Offset: 0x00055613
		public AsnEncodedDataCollection()
		{
			this._list = new List<AsnEncodedData>();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> class and adds an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to the collection.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to add to the collection.</param>
		// Token: 0x060015BB RID: 5563 RVA: 0x00057426 File Offset: 0x00055626
		public AsnEncodedDataCollection(AsnEncodedData asnEncodedData)
			: this()
		{
			this._list.Add(asnEncodedData);
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>The index of the added <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</returns>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">Neither of the OIDs are null and the OIDs do not match.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">One of the OIDs is null and the OIDs do not match.</exception>
		// Token: 0x060015BC RID: 5564 RVA: 0x0005743A File Offset: 0x0005563A
		public int Add(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			int count = this._list.Count;
			this._list.Add(asnEncodedData);
			return count;
		}

		/// <summary>Removes an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is null.</exception>
		// Token: 0x060015BD RID: 5565 RVA: 0x00057461 File Offset: 0x00055661
		public void Remove(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			this._list.Remove(asnEncodedData);
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</returns>
		/// <param name="index">The location in the collection.</param>
		// Token: 0x1700040D RID: 1037
		public AsnEncodedData this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.AsnEncodedData" /> objects in a collection.</summary>
		/// <returns>The number of <see cref="T:System.Security.Cryptography.AsnEncodedData" /> objects.</returns>
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x0005748C File Offset: 0x0005568C
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.AsnEncodedDataEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsnEncodedDataEnumerator" /> object.</returns>
		// Token: 0x060015C0 RID: 5568 RVA: 0x00057499 File Offset: 0x00055699
		public AsnEncodedDataEnumerator GetEnumerator()
		{
			return new AsnEncodedDataEnumerator(this);
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.AsnEncodedDataEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsnEncodedDataEnumerator" /> object that can be used to navigate the collection.</returns>
		// Token: 0x060015C1 RID: 5569 RVA: 0x000574A1 File Offset: 0x000556A1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object into an array.</summary>
		/// <param name="array">The array that the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object is to be copied into.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is a multidimensional array, which is not supported by this method.</exception>
		/// <exception cref="T:System.ArgumentException">The length for <paramref name="index" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length for <paramref name="index" /> is out of range.</exception>
		// Token: 0x060015C2 RID: 5570 RVA: 0x000574AC File Offset: 0x000556AC
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
			if (this.Count > array.Length - index)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object into an array.</summary>
		/// <param name="array">The array that the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object is to be copied into.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		// Token: 0x060015C3 RID: 5571 RVA: 0x00057537 File Offset: 0x00055737
		public void CopyTo(AsnEncodedData[] array, int index)
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

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object is thread safe.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>An object used to synchronize access to the <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</returns>
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x000075E1 File Offset: 0x000057E1
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000C51 RID: 3153
		private readonly List<AsnEncodedData> _list;
	}
}
