using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Permissions;
using Unity;

namespace System.Web
{
	/// <summary>Provides access to and organizes files uploaded by a client.</summary>
	// Token: 0x02000094 RID: 148
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpFileCollection : NameObjectCollectionBase
	{
		// Token: 0x0600073C RID: 1852 RVA: 0x00006641 File Offset: 0x00004841
		internal HttpFileCollection()
		{
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001105D File Offset: 0x0000F25D
		internal void AddFile(string name, HttpPostedFile file)
		{
			base.BaseAdd(name, file);
		}

		/// <summary>Copies members of the file collection to an <see cref="T:System.Array" /> beginning at the specified index of the array.</summary>
		/// <param name="dest">The destination <see cref="T:System.Array" />. </param>
		/// <param name="index">The index of the destination array where copying starts. </param>
		// Token: 0x0600073E RID: 1854 RVA: 0x000103F4 File Offset: 0x0000E5F4
		public void CopyTo(Array dest, int index)
		{
			base.BaseGetAllValues().CopyTo(dest, index);
		}

		/// <summary>Returns the name of the <see cref="T:System.Web.HttpFileCollection" /> member with the specified numerical index.</summary>
		/// <returns>The name of the <see cref="T:System.Web.HttpFileCollection" /> member specified by <paramref name="index" />.</returns>
		/// <param name="index">The index of the object name to be returned. </param>
		// Token: 0x0600073F RID: 1855 RVA: 0x00011067 File Offset: 0x0000F267
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		/// <summary>Returns the <see cref="T:System.Web.HttpPostedFile" /> object with the specified numerical index from the file collection.</summary>
		/// <returns>An <see cref="T:System.Web.HttpPostedFile" /> object.</returns>
		/// <param name="index">The index of the object to be returned from the file collection. </param>
		// Token: 0x06000740 RID: 1856 RVA: 0x00011070 File Offset: 0x0000F270
		public HttpPostedFile Get(int index)
		{
			return (HttpPostedFile)base.BaseGet(index);
		}

		/// <summary>Returns the <see cref="T:System.Web.HttpPostedFile" /> object with the specified name from the file collection.</summary>
		/// <returns>An <see cref="T:System.Web.HttpPostedFile" /> object. </returns>
		/// <param name="name">The name of the object to be returned from a file collection. </param>
		// Token: 0x06000741 RID: 1857 RVA: 0x0001107E File Offset: 0x0000F27E
		public HttpPostedFile Get(string name)
		{
			return (HttpPostedFile)base.BaseGet(name);
		}

		/// <summary>Gets the object with the specified name from the file collection.</summary>
		/// <returns>The <see cref="T:System.Web.HttpPostedFile" /> specified by <paramref name="name" />.</returns>
		/// <param name="name">Name of item to be returned. </param>
		// Token: 0x170002BF RID: 703
		public HttpPostedFile this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		/// <summary>Gets the object with the specified numerical index from the <see cref="T:System.Web.HttpFileCollection" />.</summary>
		/// <returns>The <see cref="T:System.Web.HttpPostedFile" /> specified by <paramref name="index" />.</returns>
		/// <param name="index">The index of the item to get from the file collection. </param>
		// Token: 0x170002C0 RID: 704
		public HttpPostedFile this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		/// <summary>Gets a string array containing the keys (names) of all members in the file collection.</summary>
		/// <returns>An array of file names.</returns>
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001109E File Offset: 0x0000F29E
		public string[] AllKeys
		{
			get
			{
				return base.BaseGetAllKeys();
			}
		}

		/// <summary>Returns all files that match the specified name.</summary>
		/// <returns>The collection of files.</returns>
		/// <param name="name">The name to match.</param>
		// Token: 0x06000745 RID: 1861 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public IList<HttpPostedFile> GetMultiple(string name)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
