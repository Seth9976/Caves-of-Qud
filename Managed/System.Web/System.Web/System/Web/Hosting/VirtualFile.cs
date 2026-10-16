using System;
using System.IO;

namespace System.Web.Hosting
{
	/// <summary>Represents a file object in a virtual file or resource space.</summary>
	// Token: 0x02000553 RID: 1363
	public abstract class VirtualFile : VirtualFileBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.VirtualFile" /> class. </summary>
		/// <param name="virtualPath">The virtual path to the resource represented by this instance. </param>
		// Token: 0x06003AF1 RID: 15089 RVA: 0x0009C565 File Offset: 0x0009A765
		protected VirtualFile(string virtualPath)
		{
			base.SetVirtualPath(virtualPath);
		}

		/// <summary>Gets a value that indicates that this is a virtual resource that should be treated as a file.</summary>
		/// <returns>Always false. </returns>
		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool IsDirectory
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, returns a read-only stream to the virtual resource.</summary>
		/// <returns>A read-only stream to the virtual file.</returns>
		// Token: 0x06003AF3 RID: 15091
		public abstract Stream Open();
	}
}
