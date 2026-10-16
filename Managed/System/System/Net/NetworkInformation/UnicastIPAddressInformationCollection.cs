using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Net.NetworkInformation
{
	/// <summary>Stores a set of <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types.</summary>
	// Token: 0x0200070D RID: 1805
	public class UnicastIPAddressInformationCollection : ICollection<UnicastIPAddressInformation>, IEnumerable<UnicastIPAddressInformation>, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> class.</summary>
		// Token: 0x060039C2 RID: 14786 RVA: 0x000C8E89 File Offset: 0x000C7089
		protected internal UnicastIPAddressInformationCollection()
		{
		}

		/// <summary>Copies the elements in this collection to a one-dimensional array of type <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" />.</summary>
		/// <param name="array">A one-dimensional array that receives a copy of the collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which the copy begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> is greater than the available space from <paramref name="offset" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x060039C3 RID: 14787 RVA: 0x000C8E9C File Offset: 0x000C709C
		public virtual void CopyTo(UnicastIPAddressInformation[] array, int offset)
		{
			this.addresses.CopyTo(array, offset);
		}

		/// <summary>Gets the number of <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060039C4 RID: 14788 RVA: 0x000C8EAB File Offset: 0x000C70AB
		public virtual int Count
		{
			get
			{
				return this.addresses.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to this collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x060039C5 RID: 14789 RVA: 0x0000390E File Offset: 0x00001B0E
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <param name="address">The object to be added to the collection.</param>
		// Token: 0x060039C6 RID: 14790 RVA: 0x000C8173 File Offset: 0x000C6373
		public virtual void Add(UnicastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000C8EB8 File Offset: 0x000C70B8
		internal void InternalAdd(UnicastIPAddressInformation address)
		{
			this.addresses.Add(address);
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> object exists in the collection; otherwise, false.</returns>
		/// <param name="address">The <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> object to be searched in the collection.</param>
		// Token: 0x060039C8 RID: 14792 RVA: 0x000C8EC6 File Offset: 0x000C70C6
		public virtual bool Contains(UnicastIPAddressInformation address)
		{
			return this.addresses.Contains(address);
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x060039C9 RID: 14793 RVA: 0x000C8ED4 File Offset: 0x000C70D4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x060039CA RID: 14794 RVA: 0x000C8EDC File Offset: 0x000C70DC
		public virtual IEnumerator<UnicastIPAddressInformation> GetEnumerator()
		{
			return this.addresses.GetEnumerator();
		}

		/// <summary>Gets the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> instance at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x17000CAB RID: 3243
		public virtual UnicastIPAddressInformation this[int index]
		{
			get
			{
				return this.addresses[index];
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because the collection is read-only and elements cannot be removed.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="address">The object to be removed.</param>
		// Token: 0x060039CC RID: 14796 RVA: 0x000C8173 File Offset: 0x000C6373
		public virtual bool Remove(UnicastIPAddressInformation address)
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		// Token: 0x060039CD RID: 14797 RVA: 0x000C8173 File Offset: 0x000C6373
		public virtual void Clear()
		{
			throw new NotSupportedException(SR.GetString("The collection is read-only."));
		}

		// Token: 0x040021FE RID: 8702
		private Collection<UnicastIPAddressInformation> addresses = new Collection<UnicastIPAddressInformation>();
	}
}
