using System;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Provides information about a running application. This class cannot be inherited.</summary>
	// Token: 0x02000544 RID: 1348
	[Serializable]
	public sealed class ApplicationInfo
	{
		// Token: 0x06003A58 RID: 14936 RVA: 0x0009B60F File Offset: 0x0009980F
		internal ApplicationInfo(string id, string phys, string virt)
		{
			this.id = id;
			this.physical_path = phys;
			this.virtual_path = virt;
		}

		/// <summary>Gets the unique identifier for the application.</summary>
		/// <returns>The unique identifier for the application specified when the application was created by using the <see cref="M:System.Web.Hosting.ApplicationManager.CreateObject(System.String,System.Type,System.String,System.String,System.Boolean)" /> method.</returns>
		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06003A59 RID: 14937 RVA: 0x0009B62C File Offset: 0x0009982C
		public string ID
		{
			get
			{
				return this.id;
			}
		}

		/// <summary>Gets the physical path corresponding to the application's root.</summary>
		/// <returns>The physical path corresponding to the application's root.</returns>
		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06003A5A RID: 14938 RVA: 0x0009B634 File Offset: 0x00099834
		public string PhysicalPath
		{
			get
			{
				return this.physical_path;
			}
		}

		/// <summary>Gets the virtual path corresponding to the application's root.</summary>
		/// <returns>The virtual path corresponding to the application's root.</returns>
		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06003A5B RID: 14939 RVA: 0x0009B63C File Offset: 0x0009983C
		public string VirtualPath
		{
			get
			{
				return this.virtual_path;
			}
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal ApplicationInfo()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001FCC RID: 8140
		private string id;

		// Token: 0x04001FCD RID: 8141
		private string physical_path;

		// Token: 0x04001FCE RID: 8142
		private string virtual_path;
	}
}
