using System;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Provides a way to index and retrieve a collection of <see cref="T:System.Web.IHttpModule" /> objects.</summary>
	// Token: 0x02000099 RID: 153
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpModuleCollection : NameObjectCollectionBase
	{
		// Token: 0x06000760 RID: 1888 RVA: 0x00006641 File Offset: 0x00004841
		internal HttpModuleCollection()
		{
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001105D File Offset: 0x0000F25D
		internal void AddModule(string key, IHttpModule m)
		{
			base.BaseAdd(key, m);
		}

		/// <summary>Copies members of the module collection to an <see cref="T:System.Array" />, beginning at the specified index of the array.</summary>
		/// <param name="dest">The destination <see cref="T:System.Array" />. </param>
		/// <param name="index">The index of the destination <see cref="T:System.Array" /> where copying starts. </param>
		// Token: 0x06000762 RID: 1890 RVA: 0x000103F4 File Offset: 0x0000E5F4
		public void CopyTo(Array dest, int index)
		{
			base.BaseGetAllValues().CopyTo(dest, index);
		}

		/// <summary>Returns the key (name) of the <see cref="T:System.Web.IHttpModule" /> object at the specified numerical index.</summary>
		/// <returns>The name of the <see cref="T:System.Web.IHttpModule" /> member specified by the <paramref name="index" /> parameter.</returns>
		/// <param name="index">Index of the key to retrieve from the collection. </param>
		// Token: 0x06000763 RID: 1891 RVA: 0x00011067 File Offset: 0x0000F267
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		/// <summary>Returns the <see cref="T:System.Web.IHttpModule" /> object with the specified index from the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
		/// <returns>The <see cref="T:System.Web.IHttpModule" /> member specified by the <paramref name="index" /> parameter.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.IHttpModule" /> object to return from the collection. </param>
		// Token: 0x06000764 RID: 1892 RVA: 0x000112C1 File Offset: 0x0000F4C1
		public IHttpModule Get(int index)
		{
			return (IHttpModule)base.BaseGet(index);
		}

		/// <summary>Returns the <see cref="T:System.Web.IHttpModule" /> object with the specified name from the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
		/// <returns>The <see cref="T:System.Web.IHttpModule" /> member specified by the <paramref name="name" /> parameter.</returns>
		/// <param name="name">The key of the item to be retrieved. </param>
		// Token: 0x06000765 RID: 1893 RVA: 0x000112CF File Offset: 0x0000F4CF
		public IHttpModule Get(string name)
		{
			return (IHttpModule)base.BaseGet(name);
		}

		/// <summary>Gets the <see cref="T:System.Web.IHttpModule" /> object with the specified name from the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
		/// <returns>The <see cref="T:System.Web.IHttpModule" /> object module specified by the <paramref name="name" /> parameter.</returns>
		/// <param name="name">The key of the item to be retrieved. </param>
		// Token: 0x170002CC RID: 716
		public IHttpModule this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.IHttpModule" /> object with the specified numerical index from the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
		/// <returns>The <see cref="T:System.Web.IHttpModule" /> object module specified by the <paramref name="index" /> parameter.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.IHttpModule" /> object to retrieve from the collection. </param>
		// Token: 0x170002CD RID: 717
		public IHttpModule this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		/// <summary>Gets a string array containing all the keys (module names) in the <see cref="T:System.Web.HttpModuleCollection" />.</summary>
		/// <returns>An array of module names.</returns>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0001109E File Offset: 0x0000F29E
		public string[] AllKeys
		{
			get
			{
				return base.BaseGetAllKeys();
			}
		}
	}
}
