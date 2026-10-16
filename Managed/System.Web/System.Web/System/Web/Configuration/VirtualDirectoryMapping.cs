using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies a custom virtual-directory hierarchy for a Web application. This class cannot be inherited.</summary>
	// Token: 0x020005E7 RID: 1511
	public sealed class VirtualDirectoryMapping
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> class based on supplied parameters.</summary>
		/// <param name="physicalDirectory">A string value that specifies the absolute path to a physical directory.</param>
		/// <param name="isAppRoot">A Boolean value that indicates whether the virtual directory is the application root of the Web application.</param>
		// Token: 0x06004180 RID: 16768 RVA: 0x000A943F File Offset: 0x000A763F
		public VirtualDirectoryMapping(string physicalDirectory, bool isAppRoot)
		{
			this.physicalDirectory = physicalDirectory;
			this.isAppRoot = isAppRoot;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> class based on supplied parameters.</summary>
		/// <param name="physicalDirectory">A string value that specifies the absolute path to a physical directory.</param>
		/// <param name="isAppRoot">A Boolean value that indicates whether the virtual directory is the application root of the Web application.</param>
		/// <param name="configFileBaseName">The name of the configuration file.</param>
		// Token: 0x06004181 RID: 16769 RVA: 0x000A9455 File Offset: 0x000A7655
		public VirtualDirectoryMapping(string physicalDirectory, bool isAppRoot, string configFileBaseName)
		{
			this.physicalDirectory = physicalDirectory;
			this.isAppRoot = isAppRoot;
			this.configFileBaseName = configFileBaseName;
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x000A9472 File Offset: 0x000A7672
		internal void SetVirtualDirectory(string dir)
		{
			this.virtualDirectory = dir;
		}

		/// <summary>Gets or sets the name of the configuration file.</summary>
		/// <returns>A value that indicates the name of the configuration file.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value is null or an empty string ("").</exception>
		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x06004183 RID: 16771 RVA: 0x000A947B File Offset: 0x000A767B
		// (set) Token: 0x06004184 RID: 16772 RVA: 0x000A9483 File Offset: 0x000A7683
		[global::System.MonoTODO("Do something with this")]
		public string ConfigFileBaseName
		{
			get
			{
				return this.configFileBaseName;
			}
			set
			{
				this.configFileBaseName = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the virtual directory should be treated as the application root.</summary>
		/// <returns>A value that indicates whether the virtual directory should be treated as the application root.</returns>
		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x06004185 RID: 16773 RVA: 0x000A948C File Offset: 0x000A768C
		// (set) Token: 0x06004186 RID: 16774 RVA: 0x000A9494 File Offset: 0x000A7694
		[global::System.MonoTODO("Do something with this")]
		public bool IsAppRoot
		{
			get
			{
				return this.isAppRoot;
			}
			set
			{
				this.isAppRoot = value;
			}
		}

		/// <summary>Gets or sets a value that specifies the full server path of a Web application.</summary>
		/// <returns>A value that indicates the full server path of a Web application.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value is invalid or fails internal security validation.</exception>
		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x06004187 RID: 16775 RVA: 0x000A949D File Offset: 0x000A769D
		// (set) Token: 0x06004188 RID: 16776 RVA: 0x000A94A5 File Offset: 0x000A76A5
		public string PhysicalDirectory
		{
			get
			{
				return this.physicalDirectory;
			}
			set
			{
				this.physicalDirectory = value;
			}
		}

		/// <summary>Gets a value that specifies the virtual directory relative to the root of the Web server.</summary>
		/// <returns>A value that indicates the relative Web-application directory.</returns>
		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x06004189 RID: 16777 RVA: 0x000A94AE File Offset: 0x000A76AE
		public string VirtualDirectory
		{
			get
			{
				return this.virtualDirectory;
			}
		}

		// Token: 0x0400233E RID: 9022
		private string physicalDirectory;

		// Token: 0x0400233F RID: 9023
		private bool isAppRoot;

		// Token: 0x04002340 RID: 9024
		private string configFileBaseName;

		// Token: 0x04002341 RID: 9025
		private string virtualDirectory;
	}
}
