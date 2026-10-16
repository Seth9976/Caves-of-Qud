using System;
using System.ComponentModel;
using System.Configuration;
using System.Xml;

namespace System.Web.Configuration
{
	/// <summary>Contains a single configuration tag remapping statement. This class cannot be inherited.</summary>
	// Token: 0x020005DA RID: 1498
	public sealed class TagMapInfo : ConfigurationElement
	{
		// Token: 0x060040E6 RID: 16614 RVA: 0x000A8590 File Offset: 0x000A6790
		static TagMapInfo()
		{
			TagMapInfo.properties.Add(TagMapInfo.mappedTagTypeProp);
			TagMapInfo.properties.Add(TagMapInfo.tagTypeProp);
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal TagMapInfo()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.TagMapInfo" /> class based on the passed property values.</summary>
		/// <param name="tagTypeName">The fully qualified name of the type for the tag that is being remapped.</param>
		/// <param name="mappedTagTypeName">The name of the type to which the tag is remapped, along with the supporting details.</param>
		// Token: 0x060040E8 RID: 16616 RVA: 0x000A8627 File Offset: 0x000A6827
		public TagMapInfo(string tagTypeName, string mappedTagTypeName)
		{
			this.TagType = tagTypeName;
			this.MappedTagType = mappedTagTypeName;
		}

		/// <summary>Compares this instance to another object.</summary>
		/// <returns>true if the objects are identical; otherwise, false.</returns>
		/// <param name="o">Object to compare.</param>
		// Token: 0x060040E9 RID: 16617 RVA: 0x000A8640 File Offset: 0x000A6840
		public override bool Equals(object o)
		{
			TagMapInfo tagMapInfo = o as TagMapInfo;
			return tagMapInfo != null && this.MappedTagType == tagMapInfo.MappedTagType && this.TagType == tagMapInfo.TagType;
		}

		/// <summary>Returns a hash value for the current instance.</summary>
		/// <returns>A hash value for the current instance.</returns>
		// Token: 0x060040EA RID: 16618 RVA: 0x000A867F File Offset: 0x000A687F
		public override int GetHashCode()
		{
			return this.MappedTagType.GetHashCode() + this.TagType.GetHashCode();
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x000A8698 File Offset: 0x000A6898
		protected internal override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey);
		}

		/// <summary>Gets or sets the name of the type to which the tag is remapped.</summary>
		/// <returns>The name of the type to which the tag is remapped. The default is an empty string.</returns>
		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x060040EC RID: 16620 RVA: 0x000A86A2 File Offset: 0x000A68A2
		// (set) Token: 0x060040ED RID: 16621 RVA: 0x000A86B4 File Offset: 0x000A68B4
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("mappedTagType")]
		public string MappedTagType
		{
			get
			{
				return (string)base[TagMapInfo.mappedTagTypeProp];
			}
			set
			{
				base[TagMapInfo.mappedTagTypeProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the original type for the tag that is being remapped.</summary>
		/// <returns>The name of the original type for the tag that is being remapped. </returns>
		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x060040EE RID: 16622 RVA: 0x000A86C2 File Offset: 0x000A68C2
		// (set) Token: 0x060040EF RID: 16623 RVA: 0x000A86D4 File Offset: 0x000A68D4
		[ConfigurationProperty("tagType", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 1)]
		public string TagType
		{
			get
			{
				return (string)base[TagMapInfo.tagTypeProp];
			}
			set
			{
				base[TagMapInfo.tagTypeProp] = value;
			}
		}

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x060040F0 RID: 16624 RVA: 0x000A86E2 File Offset: 0x000A68E2
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TagMapInfo.properties;
			}
		}

		// Token: 0x04002316 RID: 8982
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002317 RID: 8983
		private static ConfigurationProperty mappedTagTypeProp = new ConfigurationProperty("mappedTagType", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002318 RID: 8984
		private static ConfigurationProperty tagTypeProp = new ConfigurationProperty("tagType", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
