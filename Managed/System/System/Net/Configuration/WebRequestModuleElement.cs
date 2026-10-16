using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a URI prefix and the associated class that handles creating Web requests for the prefix. This class cannot be inherited.</summary>
	// Token: 0x02000781 RID: 1921
	public sealed class WebRequestModuleElement : ConfigurationElement
	{
		// Token: 0x06003C7A RID: 15482 RVA: 0x000CE344 File Offset: 0x000CC544
		static WebRequestModuleElement()
		{
			WebRequestModuleElement.properties.Add(WebRequestModuleElement.prefixProp);
			WebRequestModuleElement.properties.Add(WebRequestModuleElement.typeProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class. </summary>
		// Token: 0x06003C7B RID: 15483 RVA: 0x0003121C File Offset: 0x0002F41C
		public WebRequestModuleElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class using the specified URI prefix and type information. </summary>
		/// <param name="prefix">A string containing a URI prefix.</param>
		/// <param name="type">A string containing the type and assembly information for the class that handles creating requests for resources that use the <paramref name="prefix" /> URI prefix. For more information, see the Remarks section.</param>
		// Token: 0x06003C7C RID: 15484 RVA: 0x000CE3AD File Offset: 0x000CC5AD
		public WebRequestModuleElement(string prefix, string type)
		{
			base[WebRequestModuleElement.typeProp] = type;
			this.Prefix = prefix;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class using the specified URI prefix and type identifier.</summary>
		/// <param name="prefix">A string containing a URI prefix.</param>
		/// <param name="type">A <see cref="T:System.Type" /> that identifies the class that handles creating requests for resources that use the <paramref name="prefix" /> URI prefix. </param>
		// Token: 0x06003C7D RID: 15485 RVA: 0x000CE3C8 File Offset: 0x000CC5C8
		public WebRequestModuleElement(string prefix, Type type)
			: this(prefix, type.FullName)
		{
		}

		/// <summary>Gets or sets the URI prefix for the current Web request module.</summary>
		/// <returns>A string that contains a URI prefix.</returns>
		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06003C7E RID: 15486 RVA: 0x000CE3D7 File Offset: 0x000CC5D7
		// (set) Token: 0x06003C7F RID: 15487 RVA: 0x000CE3E9 File Offset: 0x000CC5E9
		[ConfigurationProperty("prefix", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Prefix
		{
			get
			{
				return (string)base[WebRequestModuleElement.prefixProp];
			}
			set
			{
				base[WebRequestModuleElement.prefixProp] = value;
			}
		}

		/// <summary>Gets or sets a class that creates Web requests.</summary>
		/// <returns>A <see cref="T:System.Type" /> instance that identifies a Web request module.</returns>
		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06003C80 RID: 15488 RVA: 0x000CE3F7 File Offset: 0x000CC5F7
		// (set) Token: 0x06003C81 RID: 15489 RVA: 0x000CE40E File Offset: 0x000CC60E
		[ConfigurationProperty("type")]
		[TypeConverter(typeof(TypeConverter))]
		public Type Type
		{
			get
			{
				return Type.GetType((string)base[WebRequestModuleElement.typeProp]);
			}
			set
			{
				base[WebRequestModuleElement.typeProp] = value.FullName;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06003C82 RID: 15490 RVA: 0x000CE421 File Offset: 0x000CC621
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebRequestModuleElement.properties;
			}
		}

		// Token: 0x040023EF RID: 9199
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040023F0 RID: 9200
		private static ConfigurationProperty prefixProp = new ConfigurationProperty("prefix", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040023F1 RID: 9201
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string));
	}
}
