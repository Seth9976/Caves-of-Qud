using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	/// <summary>Stores a set of <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> types.</summary>
	// Token: 0x020006F2 RID: 1778
	public class MulticastIPAddressInformationCollection : ICollection<MulticastIPAddressInformation>, IEnumerable<MulticastIPAddressInformation>, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformationCollection" /> class.</summary>
		// Token: 0x06003934 RID: 14644 RVA: 0x000C82CE File Offset: 0x000C64CE
		protected internal MulticastIPAddressInformationCollection()
		{
		}

		/// <summary>Copies the elements in this collection to a one-dimensional array of type <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" />.</summary>
		/// <param name="array">A one-dimensional array that receives a copy of the collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which the copy begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> is greater than the available space from <paramref name="count" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06003935 RID: 14645 RVA: 0x000C82E1 File Offset: 0x000C64E1
		public virtual void CopyTo(MulticastIPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		/// <summary>Gets the number of <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> types in this collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06003936 RID: 14646 RVA: 0x000C82F0 File Offset: 0x000C64F0
		public virtual int Count
		{
			get
			{
				return this.addresses.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to this collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06003937 RID: 14647 RVA: 0x0000390E File Offset: 0x00001B0E
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because the collection is read-only and elements cannot be added to the collection.</summary>
		/// <param name="address">The object to be added to the collection.</param>
		// Token: 0x06003938 RID: 14648 RVA: 0x000C8173 File Offset: 0x000C6373
		public virtual void Add(MulticastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000C82FD File Offset: 0x000C64FD
		internal void InternalAdd(MulticastIPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> object exists in the collection; otherwise, false.</returns>
		/// <param name="address">The <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> object to be searched in the collection.</param>
		// Token: 0x0600393A RID: 14650 RVA: 0x000C830B File Offset: 0x000C650B
		public virtual bool Contains(MulticastIPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x0600393B RID: 14651 RVA: 0x000C8319 File Offset: 0x000C6519
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x0600393C RID: 14652 RVA: 0x000C8321 File Offset: 0x000C6521
		public virtual IEnumerator<MulticastIPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		/// <summary>Gets the <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> at the specific index of the collection.</summary>
		/// <returns>The <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> at the specific index in the collection.</returns>
		/// <param name="index">The index of interest.</param>
		// Token: 0x17000C65 RID: 3173
		public virtual MulticastIPAddressInformation this[int index]
		{
			get
			{
				return this.addresses[index];
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because the collection is read-only and elements cannot be removed.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="address">The object to be removed.</param>
		// Token: 0x0600393E RID: 14654 RVA: 0x000C8173 File Offset: 0x000C6373
		public virtual bool Remove(MulticastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because the collection is read-only and elements cannot be removed.</summary>
		// Token: 0x0600393F RID: 14655 RVA: 0x000C8173 File Offset: 0x000C6373
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		// Token: 0x040021AC RID: 8620
		private Collection<MulticastIPAddressInformation> addresses = new Collection<MulticastIPAddressInformation>();
	}
}
