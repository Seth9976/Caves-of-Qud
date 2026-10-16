using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005E8 RID: 1512
	[Serializable]
	public sealed class VirtualDirectoryMappingCollection : NameObjectCollectionBase
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object to the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
		/// <param name="virtualDirectory">A <see cref="T:System.String" /> that contains the virtual directory path.</param>
		/// <param name="mapping">A <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mapping" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="virtualDirectory" /> already exists in the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> is read-only.</exception>
		// Token: 0x0600418A RID: 16778 RVA: 0x000A94B6 File Offset: 0x000A76B6
		public void Add(string virtualDirectory, VirtualDirectoryMapping mapping)
		{
			mapping.SetVirtualDirectory(virtualDirectory);
			base.BaseAdd(virtualDirectory, mapping);
		}

		/// <summary>Clears all <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> objects from the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
		// Token: 0x0600418B RID: 16779 RVA: 0x000103EC File Offset: 0x0000E5EC
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Copies the entire <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Array.Length" /> property of <paramref name="array" /> is less than the value of <see cref="P:System.Collections.Specialized.NameObjectCollectionBase.Count" /> plus <paramref name="index" />.</exception>
		// Token: 0x0600418C RID: 16780 RVA: 0x00052B27 File Offset: 0x00050D27
		public void CopyTo(VirtualDirectoryMapping[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Gets the specified <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> collection element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> element at the specified index.</returns>
		/// <param name="index">An integer value that specifies a particular <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object within the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</param>
		// Token: 0x0600418D RID: 16781 RVA: 0x000A94C7 File Offset: 0x000A76C7
		public VirtualDirectoryMapping Get(int index)
		{
			return (VirtualDirectoryMapping)base.BaseGet(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> collection element based on the specified virtual-directory name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> element based on the specified virtual-directory name.</returns>
		/// <param name="virtualDirectory">A string that contains the name of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</param>
		// Token: 0x0600418E RID: 16782 RVA: 0x000A94D5 File Offset: 0x000A76D5
		public VirtualDirectoryMapping Get(string virtualDirectory)
		{
			return (VirtualDirectoryMapping)base.BaseGet(virtualDirectory);
		}

		/// <summary>Gets the key of the entry at the specified index of the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
		/// <returns>A string that contains the name of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</returns>
		/// <param name="index">An integer value that specifies a particular <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object within the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</param>
		// Token: 0x0600418F RID: 16783 RVA: 0x00011067 File Offset: 0x0000F267
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object from the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
		/// <param name="virtualDirectory">A string that contains the name of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</param>
		// Token: 0x06004190 RID: 16784 RVA: 0x00010429 File Offset: 0x0000E629
		public void Remove(string virtualDirectory)
		{
			base.BaseRemove(virtualDirectory);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object at the specified index from the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</summary>
		/// <param name="index">An integer value that specifies a particular <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object within the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</param>
		// Token: 0x06004191 RID: 16785 RVA: 0x000A94E3 File Offset: 0x000A76E3
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Returns a string array that contains all the keys in the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</summary>
		/// <returns>A string array that contains all the keys in the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" /> instance.</returns>
		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x06004192 RID: 16786 RVA: 0x0001109E File Offset: 0x0000F29E
		public ICollection AllKeys
		{
			get
			{
				return base.BaseGetAllKeys();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object at the specified index location.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</returns>
		/// <param name="index">An integer value that specifies a particular <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object within the <see cref="T:System.Web.Configuration.VirtualDirectoryMappingCollection" />.</param>
		// Token: 0x170014F2 RID: 5362
		public VirtualDirectoryMapping this[int index]
		{
			get
			{
				return (VirtualDirectoryMapping)base.BaseGet(index);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object based on the specified virtual directory name.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</returns>
		/// <param name="virtualDirectory">A string that contains the name of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> object.</param>
		// Token: 0x170014F3 RID: 5363
		public VirtualDirectoryMapping this[string virtualDirectory]
		{
			get
			{
				return (VirtualDirectoryMapping)base.BaseGet(virtualDirectory);
			}
		}
	}
}
