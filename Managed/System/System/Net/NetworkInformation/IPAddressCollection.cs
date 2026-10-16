using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	/// <summary>Stores a set of <see cref="T:System.Net.IPAddress" /> types.</summary>
	// Token: 0x020006E2 RID: 1762
	public class IPAddressCollection : ICollection<IPAddress>, IEnumerable<IPAddress>, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> class.</summary>
		// Token: 0x06003874 RID: 14452 RVA: 0x000C81B5 File Offset: 0x000C63B5
		protected internal IPAddressCollection()
		{
		}

		/// <summary>Copies the elements in this collection to a one-dimensional array of type <see cref="T:System.Net.IPAddress" />.</summary>
		/// <param name="array">A one-dimensional array that receives a copy of the collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which the copy begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> is greater than the available space from <paramref name="offset" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06003875 RID: 14453 RVA: 0x000C81C8 File Offset: 0x000C63C8
		public virtual void CopyTo(IPAddress[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		/// <summary>Gets the number of <see cref="T:System.Net.IPAddress" /> types in this collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of <see cref="T:System.Net.IPAddress" /> types in this collection.</returns>
		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06003876 RID: 14454 RVA: 0x000C81D7 File Offset: 0x000C63D7
		public virtual int Count
		{
			get
			{
				return this.addresses.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to this collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x0000390E File Offset: 0x00001B0E
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <param name="address">The object to be added to the collection.</param>
		// Token: 0x06003878 RID: 14456 RVA: 0x000C8173 File Offset: 0x000C6373
		public virtual void Add(IPAddress address)
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x000C81E4 File Offset: 0x000C63E4
		internal void InternalAdd(IPAddress address)
		{
			this.addresses.Add(address);
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Net.IPAddress" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Net.IPAddress" /> object exists in the collection; otherwise, false.</returns>
		/// <param name="address">The <see cref="T:System.Net.IPAddress" /> object to be searched in the collection.</param>
		// Token: 0x0600387A RID: 14458 RVA: 0x000C81F2 File Offset: 0x000C63F2
		public virtual bool Contains(IPAddress address)
		{
			return this.addresses.Contains(address);
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> types in this collection.</returns>
		// Token: 0x0600387B RID: 14459 RVA: 0x000C8200 File Offset: 0x000C6400
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> types in this collection.</returns>
		// Token: 0x0600387C RID: 14460 RVA: 0x000C8208 File Offset: 0x000C6408
		public virtual IEnumerator<IPAddress> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		/// <summary>Gets the <see cref="T:System.Net.IPAddress" /> at the specific index of the collection.</summary>
		/// <returns>The <see cref="T:System.Net.IPAddress" /> at the specific index in the collection.</returns>
		/// <param name="index">The index of interest.</param>
		// Token: 0x17000BD6 RID: 3030
		public virtual IPAddress this[int index]
		{
			get
			{
				return this.addresses[index];
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="address">The object to be removed.</param>
		// Token: 0x0600387E RID: 14462 RVA: 0x000C8173 File Offset: 0x000C6373
		public virtual bool Remove(IPAddress address)
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		// Token: 0x0600387F RID: 14463 RVA: 0x000C8173 File Offset: 0x000C6373
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		// Token: 0x04002174 RID: 8564
		private Collection<IPAddress> addresses = new Collection<IPAddress>();
	}
}
