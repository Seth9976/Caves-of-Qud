using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>References an assembly to be linked to during compilation of a dynamic resource. This class cannot be inherited.</summary>
	// Token: 0x0200057D RID: 1405
	public sealed class AssemblyInfo : ConfigurationElement
	{
		// Token: 0x06003B4D RID: 15181 RVA: 0x0009CC18 File Offset: 0x0009AE18
		static AssemblyInfo()
		{
			AssemblyInfo.properties.Add(AssemblyInfo.assemblyProp);
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal AssemblyInfo()
		{
		}

		/// <summary>Creates an instance of an <see cref="T:System.Web.Configuration.AssemblyInfo" /> class.</summary>
		/// <param name="assemblyName">Specifies a comma-separated assembly name combination consisting of version, culture, and public-key tokens.</param>
		// Token: 0x06003B4F RID: 15183 RVA: 0x0009CC75 File Offset: 0x0009AE75
		public AssemblyInfo(string assemblyName)
		{
			this.Assembly = assemblyName;
		}

		/// <summary>Gets or sets an assembly reference to use during compilation of a dynamic resource.</summary>
		/// <returns>A comma-separated string value specifying the version, culture, and public-key tokens of an assembly.</returns>
		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06003B50 RID: 15184 RVA: 0x0009CC84 File Offset: 0x0009AE84
		// (set) Token: 0x06003B51 RID: 15185 RVA: 0x0009CC96 File Offset: 0x0009AE96
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("assembly", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Assembly
		{
			get
			{
				return (string)base[AssemblyInfo.assemblyProp];
			}
			set
			{
				base[AssemblyInfo.assemblyProp] = value;
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06003B52 RID: 15186 RVA: 0x0009CCA4 File Offset: 0x0009AEA4
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AssemblyInfo.properties;
			}
		}

		// Token: 0x04002080 RID: 8320
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002081 RID: 8321
		private static ConfigurationProperty assemblyProp = new ConfigurationProperty("assembly", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
