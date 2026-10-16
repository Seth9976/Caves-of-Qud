using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Specifies a custom class that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class for use by Web Part connections.</summary>
	// Token: 0x020005DE RID: 1502
	public sealed class TransformerInfo : ConfigurationElement
	{
		// Token: 0x06004121 RID: 16673 RVA: 0x000A8CB0 File Offset: 0x000A6EB0
		static TransformerInfo()
		{
			TransformerInfo.properties.Add(TransformerInfo.nameProp);
			TransformerInfo.properties.Add(TransformerInfo.typeProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.TransformerInfo" /> class with the specified name and type reference.</summary>
		/// <param name="name">The name of this transformer type.</param>
		/// <param name="type">A reference to a type that extends the transformer <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</param>
		// Token: 0x06004122 RID: 16674 RVA: 0x000A8D4B File Offset: 0x000A6F4B
		public TransformerInfo(string name, string type)
		{
			this.Name = name;
			this.Type = type;
		}

		/// <summary>Compares the current <see cref="T:System.Web.Configuration.TransformerInfo" /> object to another <see cref="T:System.Web.Configuration.TransformerInfo" /> object.</summary>
		/// <returns>true if the passed object is equal to the current object; otherwise, false.</returns>
		/// <param name="o">The object to compare to the current object.</param>
		// Token: 0x06004123 RID: 16675 RVA: 0x000A8D64 File Offset: 0x000A6F64
		public override bool Equals(object o)
		{
			TransformerInfo transformerInfo = o as TransformerInfo;
			return this.Name == transformerInfo.Name && this.Type == transformerInfo.Type;
		}

		/// <summary>Generates a hash code for the collection.</summary>
		/// <returns>Unique integer hash code for the current object.</returns>
		// Token: 0x06004124 RID: 16676 RVA: 0x000A8D9E File Offset: 0x000A6F9E
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() + this.Type.GetHashCode();
		}

		/// <summary>Gets or sets a friendly name for a type that that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</summary>
		/// <returns>A friendly name for a type that that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</returns>
		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x06004125 RID: 16677 RVA: 0x000A8DB7 File Offset: 0x000A6FB7
		// (set) Token: 0x06004126 RID: 16678 RVA: 0x000A8DC9 File Offset: 0x000A6FC9
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[TransformerInfo.nameProp];
			}
			set
			{
				base[TransformerInfo.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the type reference for a class that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</summary>
		/// <returns>A type reference for a class that extends the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartTransformer" /> class.</returns>
		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x06004127 RID: 16679 RVA: 0x000A8DD7 File Offset: 0x000A6FD7
		// (set) Token: 0x06004128 RID: 16680 RVA: 0x000A8DE9 File Offset: 0x000A6FE9
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[TransformerInfo.typeProp];
			}
			set
			{
				base[TransformerInfo.typeProp] = value;
			}
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x06004129 RID: 16681 RVA: 0x000A8DF7 File Offset: 0x000A6FF7
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TransformerInfo.properties;
			}
		}

		// Token: 0x04002329 RID: 9001
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400232A RID: 9002
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400232B RID: 9003
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
