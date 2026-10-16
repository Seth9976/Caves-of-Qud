using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Contains a single configuration namespace reference, similar to the Import directive. This class cannot be inherited.</summary>
	// Token: 0x020005B9 RID: 1465
	public sealed class NamespaceInfo : ConfigurationElement
	{
		// Token: 0x06003EC5 RID: 16069 RVA: 0x000A43A4 File Offset: 0x000A25A4
		static NamespaceInfo()
		{
			NamespaceInfo.properties.Add(NamespaceInfo.namespaceProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.NamespaceInfo" /> class with the specified namespace reference.</summary>
		/// <param name="name">A namespace reference for the new <see cref="T:System.Web.Configuration.NamespaceInfo" /> object.</param>
		// Token: 0x06003EC6 RID: 16070 RVA: 0x000A43F9 File Offset: 0x000A25F9
		public NamespaceInfo(string name)
		{
			this.Namespace = name;
		}

		/// <summary>Compares the current instance to the passed <see cref="T:System.Web.Configuration.NamespaceInfo" /> object.</summary>
		/// <returns>true if the two objects are identical. </returns>
		/// <param name="namespaceInformation">A <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to compare to.</param>
		// Token: 0x06003EC7 RID: 16071 RVA: 0x000A4408 File Offset: 0x000A2608
		public override bool Equals(object namespaceInformation)
		{
			NamespaceInfo namespaceInfo = namespaceInformation as NamespaceInfo;
			return namespaceInfo != null && this.Namespace == namespaceInfo.Namespace;
		}

		/// <summary>Returns a hash value for the current instance.</summary>
		/// <returns>A hash value for the current instance.</returns>
		// Token: 0x06003EC8 RID: 16072 RVA: 0x000A4432 File Offset: 0x000A2632
		public override int GetHashCode()
		{
			return this.Namespace.GetHashCode();
		}

		/// <summary>Gets or sets the namespace reference.</summary>
		/// <returns>A string that specifies the name of the namespace.</returns>
		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x000A443F File Offset: 0x000A263F
		// (set) Token: 0x06003ECA RID: 16074 RVA: 0x000A4451 File Offset: 0x000A2651
		[ConfigurationProperty("namespace", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 1)]
		public string Namespace
		{
			get
			{
				return (string)base[NamespaceInfo.namespaceProp];
			}
			set
			{
				base[NamespaceInfo.namespaceProp] = value;
			}
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x000A445F File Offset: 0x000A265F
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return NamespaceInfo.properties;
			}
		}

		// Token: 0x04002257 RID: 8791
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002258 RID: 8792
		private static ConfigurationProperty namespaceProp = new ConfigurationProperty("namespace", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
	}
}
