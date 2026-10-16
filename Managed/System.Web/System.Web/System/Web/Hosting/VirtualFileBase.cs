using System;

namespace System.Web.Hosting
{
	/// <summary>Provides the core implementation for the <see cref="T:System.Web.Hosting.VirtualFile" /> and <see cref="T:System.Web.Hosting.VirtualDirectory" /> objects. An abstract class, it cannot be instantiated.</summary>
	// Token: 0x02000554 RID: 1364
	public abstract class VirtualFileBase : MarshalByRefObject
	{
		// Token: 0x06003AF5 RID: 15093 RVA: 0x0009C574 File Offset: 0x0009A774
		internal void SetVirtualPath(string vpath)
		{
			this.vpath = vpath;
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the <see cref="T:System.Web.Hosting.VirtualFileBase" /> instance represents a virtual file or a virtual directory.</summary>
		/// <returns>true if the <see cref="T:System.Web.Hosting.VirtualFileBase" /> instance is a virtual directory; otherwise, false if the <see cref="T:System.Web.Hosting.VirtualFileBase" /> instance is a virtual file.</returns>
		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06003AF6 RID: 15094
		public abstract bool IsDirectory { get; }

		/// <summary>Gets the display name of the virtual resource.</summary>
		/// <returns>The display name of the virtual file.</returns>
		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x0009C57D File Offset: 0x0009A77D
		public virtual string Name
		{
			get
			{
				return VirtualPathUtility.GetFileName(this.vpath);
			}
		}

		/// <summary>Gets the virtual file path.</summary>
		/// <returns>The path to the virtual file. </returns>
		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x0009C58A File Offset: 0x0009A78A
		public string VirtualPath
		{
			get
			{
				return this.vpath;
			}
		}

		/// <summary>Gives a <see cref="T:System.Web.Hosting.VirtualFileBase" /> instance an infinite lifetime by preventing a lease from being created.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x06003AF9 RID: 15097 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x04001FED RID: 8173
		private string vpath;
	}
}
