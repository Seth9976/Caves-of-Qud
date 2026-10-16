using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	/// <summary>Stores a set of <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types.</summary>
	// Token: 0x020006E4 RID: 1764
	public class IPAddressInformationCollection : ICollection<IPAddressInformation>, IEnumerable<IPAddressInformation>, IEnumerable
	{
		// Token: 0x06003884 RID: 14468 RVA: 0x000C8223 File Offset: 0x000C6423
		internal IPAddressInformationCollection()
		{
		}

		/// <summary>Copies the collection to the specified array.</summary>
		/// <param name="array">A one-dimensional array that receives a copy of the collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which the copy begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> is greater than the available space from <paramref name="offset" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06003885 RID: 14469 RVA: 0x000C8236 File Offset: 0x000C6436
		public virtual void CopyTo(IPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		/// <summary>Gets the number of <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types in this collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types in this collection.</returns>
		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06003886 RID: 14470 RVA: 0x000C8245 File Offset: 0x000C6445
		public virtual int Count
		{
			get
			{
				return this.addresses.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to this collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06003887 RID: 14471 RVA: 0x0000390E File Offset: 0x00001B0E
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <param name="address">The object to be added to the collection.</param>
		// Token: 0x06003888 RID: 14472 RVA: 0x000C8173 File Offset: 0x000C6373
		public virtual void Add(IPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000C8252 File Offset: 0x000C6452
		internal void InternalAdd(IPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> object exists in the collection; otherwise. false.</returns>
		/// <param name="address">The <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> object to be searched in the collection.</param>
		// Token: 0x0600388A RID: 14474 RVA: 0x000C8260 File Offset: 0x000C6460
		public virtual bool Contains(IPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types in this collection.</returns>
		// Token: 0x0600388B RID: 14475 RVA: 0x000C826E File Offset: 0x000C646E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types in this collection.</returns>
		// Token: 0x0600388C RID: 14476 RVA: 0x000C8276 File Offset: 0x000C6476
		public virtual IEnumerator<IPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		/// <summary>Gets the <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> at the specified index in the collection. </summary>
		/// <returns>The <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x17000BDC RID: 3036
		public virtual IPAddressInformation this[int index]
		{
			get
			{
				return this.addresses[index];
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="address">The object to be removed.</param>
		// Token: 0x0600388E RID: 14478 RVA: 0x000C8173 File Offset: 0x000C6373
		public virtual bool Remove(IPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		// Token: 0x0600388F RID: 14479 RVA: 0x000C8173 File Offset: 0x000C6373
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		// Token: 0x04002175 RID: 8565
		private Collection<IPAddressInformation> addresses = new Collection<IPAddressInformation>();
	}
}
