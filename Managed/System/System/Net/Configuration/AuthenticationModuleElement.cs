using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the type information for an authentication module. This class cannot be inherited.</summary>
	// Token: 0x0200075E RID: 1886
	public sealed class AuthenticationModuleElement : ConfigurationElement
	{
		// Token: 0x06003B6C RID: 15212 RVA: 0x000CC3C2 File Offset: 0x000CA5C2
		static AuthenticationModuleElement()
		{
			AuthenticationModuleElement.properties.Add(AuthenticationModuleElement.typeProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> class. </summary>
		// Token: 0x06003B6D RID: 15213 RVA: 0x0003121C File Offset: 0x0002F41C
		public AuthenticationModuleElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> class with the specified type information.</summary>
		/// <param name="typeName">A string that identifies the type and the assembly that contains it.</param>
		// Token: 0x06003B6E RID: 15214 RVA: 0x000CC3F8 File Offset: 0x000CA5F8
		public AuthenticationModuleElement(string typeName)
		{
			this.Type = typeName;
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x000CC407 File Offset: 0x000CA607
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthenticationModuleElement.properties;
			}
		}

		/// <summary>Gets or sets the type and assembly information for the current instance.</summary>
		/// <returns>A string that identifies a type that implements an authentication module or null if no value has been specified.</returns>
		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06003B70 RID: 15216 RVA: 0x000CC40E File Offset: 0x000CA60E
		// (set) Token: 0x06003B71 RID: 15217 RVA: 0x000CC420 File Offset: 0x000CA620
		[ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Type
		{
			get
			{
				return (string)base[AuthenticationModuleElement.typeProp];
			}
			set
			{
				base[AuthenticationModuleElement.typeProp] = value;
			}
		}

		// Token: 0x0400239C RID: 9116
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400239D RID: 9117
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
