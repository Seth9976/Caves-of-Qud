using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>References a directory location that is used during compilation of a dynamic resource. This class cannot be inherited.</summary>
	// Token: 0x0200058D RID: 1421
	public sealed class CodeSubDirectory : ConfigurationElement
	{
		// Token: 0x06003C02 RID: 15362 RVA: 0x0009E130 File Offset: 0x0009C330
		static CodeSubDirectory()
		{
			CodeSubDirectory.properties.Add(CodeSubDirectory.directoryNameProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CodeSubDirectory" /> class.</summary>
		/// <param name="directoryName">A string value specifying the <see cref="T:System.Web.Configuration.CodeSubDirectory" /> reference.</param>
		// Token: 0x06003C03 RID: 15363 RVA: 0x0009E17F File Offset: 0x0009C37F
		public CodeSubDirectory(string directoryName)
		{
			this.DirectoryName = directoryName;
		}

		/// <summary>Gets or sets the name of the directory that contains files compiled at run time.</summary>
		/// <returns>A string value specifying the name of the directory reference used during compilation.</returns>
		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06003C04 RID: 15364 RVA: 0x0009E18E File Offset: 0x0009C38E
		// (set) Token: 0x06003C05 RID: 15365 RVA: 0x0009E1A0 File Offset: 0x0009C3A0
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		[ConfigurationProperty("directoryName", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string DirectoryName
		{
			get
			{
				return (string)base[CodeSubDirectory.directoryNameProp];
			}
			set
			{
				base[CodeSubDirectory.directoryNameProp] = value;
			}
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06003C06 RID: 15366 RVA: 0x0009E1AE File Offset: 0x0009C3AE
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CodeSubDirectory.properties;
			}
		}

		// Token: 0x040020AA RID: 8362
		private static ConfigurationProperty directoryNameProp = new ConfigurationProperty("directoryName", typeof(string), "", PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020AB RID: 8363
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
