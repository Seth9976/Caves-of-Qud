using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the <see cref="T:System.Web.Configuration.HttpModulesSection" /> modules. This class cannot be inherited.</summary>
	// Token: 0x020005AA RID: 1450
	public sealed class HttpModuleAction : ConfigurationElement
	{
		// Token: 0x06003E06 RID: 15878 RVA: 0x000A26F4 File Offset: 0x000A08F4
		static HttpModuleAction()
		{
			HttpModuleAction.properties.Add(HttpModuleAction.nameProp);
			HttpModuleAction.properties.Add(HttpModuleAction.typeProp);
			HttpModuleAction.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(HttpModuleAction), new ValidatorCallback(HttpModuleAction.ValidateElement)));
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal HttpModuleAction()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpModuleAction" /> class using the passed parameters.</summary>
		/// <param name="name">The module name.</param>
		/// <param name="type">A comma-separated list containing the module type name and the assembly information. </param>
		// Token: 0x06003E08 RID: 15880 RVA: 0x000A279C File Offset: 0x000A099C
		public HttpModuleAction(string name, string type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x00003946 File Offset: 0x00001B46
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06003E0A RID: 15882 RVA: 0x000A27B2 File Offset: 0x000A09B2
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return HttpModuleAction.elementProperty;
			}
		}

		/// <summary>Gets or sets the module name.</summary>
		/// <returns>The module name.</returns>
		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06003E0B RID: 15883 RVA: 0x000A27B9 File Offset: 0x000A09B9
		// (set) Token: 0x06003E0C RID: 15884 RVA: 0x000A27CB File Offset: 0x000A09CB
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[HttpModuleAction.nameProp];
			}
			set
			{
				base[HttpModuleAction.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the module type.</summary>
		/// <returns>A comma-separated list containing the module type name and the assembly information. </returns>
		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x06003E0D RID: 15885 RVA: 0x000A27D9 File Offset: 0x000A09D9
		// (set) Token: 0x06003E0E RID: 15886 RVA: 0x000A27EB File Offset: 0x000A09EB
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[HttpModuleAction.typeProp];
			}
			set
			{
				base[HttpModuleAction.typeProp] = value;
			}
		}

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x06003E0F RID: 15887 RVA: 0x000A27F9 File Offset: 0x000A09F9
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpModuleAction.properties;
			}
		}

		// Token: 0x0400220C RID: 8716
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400220D RID: 8717
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400220E RID: 8718
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "hoho", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400220F RID: 8719
		private static ConfigurationElementProperty elementProperty;
	}
}
