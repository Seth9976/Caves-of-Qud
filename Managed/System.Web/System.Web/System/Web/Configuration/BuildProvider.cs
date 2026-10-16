using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Provides functionality to parse a particular file type and generate code during compilation of a dynamic resource. This class cannot be inherited.</summary>
	// Token: 0x02000584 RID: 1412
	public sealed class BuildProvider : ConfigurationElement
	{
		// Token: 0x06003BA8 RID: 15272 RVA: 0x0009D914 File Offset: 0x0009BB14
		static BuildProvider()
		{
			BuildProvider.properties.Add(BuildProvider.extensionProp);
			BuildProvider.properties.Add(BuildProvider.typeProp);
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal BuildProvider()
		{
		}

		/// <summary>Creates an instance of a <see cref="T:System.Web.Configuration.BuildProvider" /> class, initialized to the provided values.</summary>
		/// <param name="extension">The file extension of the dynamic resource used during compilation.</param>
		/// <param name="type">The type that represents the <see cref="T:System.Web.Configuration.BuildProvider" /> instance to use when parsing and compiling the given extension.</param>
		// Token: 0x06003BAA RID: 15274 RVA: 0x0009D9AF File Offset: 0x0009BBAF
		public BuildProvider(string extension, string type)
		{
			this.Extension = extension;
			this.Type = type;
		}

		/// <summary>Gets or sets the file extension to map to during compilation of a dynamic resource.</summary>
		/// <returns>A string specifying the file extension to map to during compilation of a dynamic resource.</returns>
		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06003BAB RID: 15275 RVA: 0x0009D9C5 File Offset: 0x0009BBC5
		// (set) Token: 0x06003BAC RID: 15276 RVA: 0x0009D9D8 File Offset: 0x0009BBD8
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("extension", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Extension
		{
			get
			{
				return (string)base[BuildProvider.extensionProp];
			}
			set
			{
				string text;
				if (!string.IsNullOrEmpty(value))
				{
					text = value.ToLowerInvariant();
				}
				else
				{
					text = value;
				}
				base[BuildProvider.extensionProp] = text;
			}
		}

		/// <summary>Gets or set the comma-separated class and assembly combination that indicates the <see cref="T:System.Web.Configuration.BuildProvider" /> instance to use.</summary>
		/// <returns>A comma-separated class and assembly combination that indicates the <see cref="T:System.Web.Configuration.BuildProvider" /> instance to use.</returns>
		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06003BAD RID: 15277 RVA: 0x0009DA04 File Offset: 0x0009BC04
		// (set) Token: 0x06003BAE RID: 15278 RVA: 0x0009DA16 File Offset: 0x0009BC16
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[BuildProvider.typeProp];
			}
			set
			{
				base[BuildProvider.typeProp] = value;
			}
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06003BAF RID: 15279 RVA: 0x0009DA24 File Offset: 0x0009BC24
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BuildProvider.properties;
			}
		}

		/// <summary>Determines whether the specified build provider object is equal to the current object.</summary>
		/// <returns>true if the objects are equal; otherwise, false.</returns>
		/// <param name="provider">The build provider object to compare with the current object.</param>
		// Token: 0x06003BB0 RID: 15280 RVA: 0x0009DA2C File Offset: 0x0009BC2C
		public override bool Equals(object provider)
		{
			BuildProvider buildProvider = provider as BuildProvider;
			return buildProvider != null && this.Extension == buildProvider.Extension && this.Type == buildProvider.Type;
		}

		/// <summary>Generates the hash code for the build provider.</summary>
		/// <returns>An integer representing the hash code for the build provider.</returns>
		// Token: 0x06003BB1 RID: 15281 RVA: 0x0009DA6B File Offset: 0x0009BC6B
		public override int GetHashCode()
		{
			return this.Extension.GetHashCode() + this.Type.GetHashCode();
		}

		// Token: 0x04002099 RID: 8345
		private static ConfigurationProperty extensionProp = new ConfigurationProperty("extension", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400209A RID: 8346
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400209B RID: 8347
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
