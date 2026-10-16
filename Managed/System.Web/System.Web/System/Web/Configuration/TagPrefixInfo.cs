using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines a configuration element containing tag-related information.</summary>
	// Token: 0x020005DC RID: 1500
	public sealed class TagPrefixInfo : ConfigurationElement
	{
		// Token: 0x060040FE RID: 16638 RVA: 0x000A8790 File Offset: 0x000A6990
		static TagPrefixInfo()
		{
			TagPrefixInfo.properties.Add(TagPrefixInfo.tagPrefixProp);
			TagPrefixInfo.properties.Add(TagPrefixInfo.namespaceProp);
			TagPrefixInfo.properties.Add(TagPrefixInfo.assemblyProp);
			TagPrefixInfo.properties.Add(TagPrefixInfo.tagNameProp);
			TagPrefixInfo.properties.Add(TagPrefixInfo.sourceProp);
			TagPrefixInfo.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(TagPrefixInfo), new ValidatorCallback(TagPrefixInfo.ValidateElement)));
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal TagPrefixInfo()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.TagPrefixInfo" /> class using the passed values.</summary>
		/// <param name="tagPrefix">The tag prefix being mapped to a source file or namespace and assembly. </param>
		/// <param name="nameSpace">The namespace associated with the tag prefix.</param>
		/// <param name="assembly">The assembly where the namespace resides.</param>
		/// <param name="tagName">The name of the control to be used in the page.</param>
		/// <param name="source">The name of the file that contains the user control.</param>
		// Token: 0x06004100 RID: 16640 RVA: 0x000A88AE File Offset: 0x000A6AAE
		public TagPrefixInfo(string tagPrefix, string nameSpace, string assembly, string tagName, string source)
		{
			this.TagPrefix = tagPrefix;
			this.Namespace = nameSpace;
			this.Assembly = assembly;
			this.TagName = tagName;
			this.Source = source;
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x00003946 File Offset: 0x00001B46
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x06004102 RID: 16642 RVA: 0x000A88DB File Offset: 0x000A6ADB
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return TagPrefixInfo.elementProperty;
			}
		}

		/// <summary>Compares this instance to another object.</summary>
		/// <returns>true if the objects are identical; otherwise, false.</returns>
		/// <param name="prefix">Object to compare.</param>
		// Token: 0x06004103 RID: 16643 RVA: 0x000A88E4 File Offset: 0x000A6AE4
		public override bool Equals(object prefix)
		{
			TagPrefixInfo tagPrefixInfo = prefix as TagPrefixInfo;
			return tagPrefixInfo != null && (this.Namespace == tagPrefixInfo.Namespace && this.Source == tagPrefixInfo.Source && this.TagName == tagPrefixInfo.TagName) && this.TagPrefix == tagPrefixInfo.TagPrefix;
		}

		/// <summary>Returns a hash value for the current instance.</summary>
		/// <returns>A hash value for the current instance.</returns>
		// Token: 0x06004104 RID: 16644 RVA: 0x000A8949 File Offset: 0x000A6B49
		public override int GetHashCode()
		{
			return this.Namespace.GetHashCode() + this.Source.GetHashCode() + this.TagName.GetHashCode() + this.TagPrefix.GetHashCode();
		}

		/// <summary>Gets or sets the name of the assembly containing the control implementation.</summary>
		/// <returns>The name of the assembly (without an extension). The default is an empty string.</returns>
		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x06004105 RID: 16645 RVA: 0x000A897A File Offset: 0x000A6B7A
		// (set) Token: 0x06004106 RID: 16646 RVA: 0x000A898C File Offset: 0x000A6B8C
		[ConfigurationProperty("assembly")]
		public string Assembly
		{
			get
			{
				return (string)base[TagPrefixInfo.assemblyProp];
			}
			set
			{
				base[TagPrefixInfo.assemblyProp] = value;
			}
		}

		/// <summary>Gets or sets the namespace in which the control resides.</summary>
		/// <returns>The name of the namespace. The default is an empty string.</returns>
		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x06004107 RID: 16647 RVA: 0x000A899A File Offset: 0x000A6B9A
		// (set) Token: 0x06004108 RID: 16648 RVA: 0x000A89AC File Offset: 0x000A6BAC
		[ConfigurationProperty("namespace")]
		public string Namespace
		{
			get
			{
				return (string)base[TagPrefixInfo.namespaceProp];
			}
			set
			{
				base[TagPrefixInfo.namespaceProp] = value;
			}
		}

		/// <summary>Gets or sets the name and path of the file containing the user control.</summary>
		/// <returns>The name and path of the file containing the user control.</returns>
		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x06004109 RID: 16649 RVA: 0x000A89BA File Offset: 0x000A6BBA
		// (set) Token: 0x0600410A RID: 16650 RVA: 0x000A89CC File Offset: 0x000A6BCC
		[ConfigurationProperty("src")]
		public string Source
		{
			get
			{
				return (string)base[TagPrefixInfo.sourceProp];
			}
			set
			{
				base[TagPrefixInfo.sourceProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the user control.</summary>
		/// <returns>The name of the user control.</returns>
		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x0600410B RID: 16651 RVA: 0x000A89DA File Offset: 0x000A6BDA
		// (set) Token: 0x0600410C RID: 16652 RVA: 0x000A89EC File Offset: 0x000A6BEC
		[ConfigurationProperty("tagName")]
		public string TagName
		{
			get
			{
				return (string)base[TagPrefixInfo.tagNameProp];
			}
			set
			{
				base[TagPrefixInfo.tagNameProp] = value;
			}
		}

		/// <summary>Gets or sets the tag prefix that is being associated with a source file or namespace and assembly. </summary>
		/// <returns>The tag prefix. </returns>
		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x0600410D RID: 16653 RVA: 0x000A89FA File Offset: 0x000A6BFA
		// (set) Token: 0x0600410E RID: 16654 RVA: 0x000A8A0C File Offset: 0x000A6C0C
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("tagPrefix", DefaultValue = "/", Options = ConfigurationPropertyOptions.IsRequired)]
		public string TagPrefix
		{
			get
			{
				return (string)base[TagPrefixInfo.tagPrefixProp];
			}
			set
			{
				base[TagPrefixInfo.tagPrefixProp] = value;
			}
		}

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x0600410F RID: 16655 RVA: 0x000A8A1A File Offset: 0x000A6C1A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TagPrefixInfo.properties;
			}
		}

		// Token: 0x0400231A RID: 8986
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400231B RID: 8987
		private static ConfigurationProperty tagPrefixProp = new ConfigurationProperty("tagPrefix", typeof(string), "/", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400231C RID: 8988
		private static ConfigurationProperty namespaceProp = new ConfigurationProperty("namespace", typeof(string));

		// Token: 0x0400231D RID: 8989
		private static ConfigurationProperty assemblyProp = new ConfigurationProperty("assembly", typeof(string));

		// Token: 0x0400231E RID: 8990
		private static ConfigurationProperty tagNameProp = new ConfigurationProperty("tagName", typeof(string));

		// Token: 0x0400231F RID: 8991
		private static ConfigurationProperty sourceProp = new ConfigurationProperty("src", typeof(string));

		// Token: 0x04002320 RID: 8992
		private static ConfigurationElementProperty elementProperty;
	}
}
