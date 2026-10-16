using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Defines the configuration file mappings for a Web application. This class cannot be inherited.</summary>
	// Token: 0x020005E9 RID: 1513
	public sealed class WebConfigurationFileMap : ConfigurationFileMap
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> class.</summary>
		// Token: 0x06004196 RID: 16790 RVA: 0x000A94EC File Offset: 0x000A76EC
		public WebConfigurationFileMap()
		{
			this.virtualDirectories = new VirtualDirectoryMappingCollection();
		}

		/// <summary>Gets the listed collection of virtual directories for a Web application.</summary>
		/// <returns>A collection of <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> objects.</returns>
		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x06004197 RID: 16791 RVA: 0x000A94FF File Offset: 0x000A76FF
		public VirtualDirectoryMappingCollection VirtualDirectories
		{
			get
			{
				return this.virtualDirectories;
			}
		}

		/// <summary>Creates a new instance of a <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> class with the same value as the existing instance.</summary>
		/// <returns>A new instance of a <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> class.</returns>
		// Token: 0x06004198 RID: 16792 RVA: 0x000A9508 File Offset: 0x000A7708
		public override object Clone()
		{
			WebConfigurationFileMap webConfigurationFileMap = new WebConfigurationFileMap();
			webConfigurationFileMap.MachineConfigFilename = base.MachineConfigFilename;
			webConfigurationFileMap.virtualDirectories = new VirtualDirectoryMappingCollection();
			foreach (object obj in this.virtualDirectories)
			{
				VirtualDirectoryMapping virtualDirectoryMapping = (VirtualDirectoryMapping)obj;
				VirtualDirectoryMapping virtualDirectoryMapping2 = new VirtualDirectoryMapping(virtualDirectoryMapping.PhysicalDirectory, virtualDirectoryMapping.IsAppRoot, virtualDirectoryMapping.ConfigFileBaseName);
				webConfigurationFileMap.virtualDirectories.Add(virtualDirectoryMapping.VirtualDirectory, virtualDirectoryMapping2);
			}
			return webConfigurationFileMap;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> class by using the specified machine configuration file name.</summary>
		/// <param name="machineConfigFileName">The machine configuration file name with the complete physical path (for example, c:\Windows\Microsoft.NET\Framework\v2.0.50727\CONFIG\machine.config).</param>
		// Token: 0x06004199 RID: 16793 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebConfigurationFileMap(string machineConfigFileName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002342 RID: 9026
		private VirtualDirectoryMappingCollection virtualDirectories;
	}
}
