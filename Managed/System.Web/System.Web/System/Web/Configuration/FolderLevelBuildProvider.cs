using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Represents configuration settings that enable use of the <see cref="T:System.Web.Compilation.BuildProvider" /> class for specific folders. </summary>
	// Token: 0x020006A0 RID: 1696
	public sealed class FolderLevelBuildProvider : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> class by using an identifier and the fully qualified name. </summary>
		/// <param name="name">The identifier for the <see cref="T:System.Web.Compilation.BuildProvider" /> to use.</param>
		/// <param name="type">The fully qualified name of the <see cref="T:System.Web.Compilation.BuildProvider" /> to use.</param>
		// Token: 0x060047C9 RID: 18377 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public FolderLevelBuildProvider(string name, string type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object.</summary>
		/// <returns>The name of the build provider object.</returns>
		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x060047CA RID: 18378 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060047CB RID: 18379 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string Name
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or set the fully qualified name of the build provider class.</summary>
		/// <returns>The fully qualified name of the build provider class.</returns>
		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x060047CC RID: 18380 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060047CD RID: 18381 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string Type
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
