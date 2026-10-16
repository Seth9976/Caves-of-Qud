using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures Passport-based authentication in ASP.NET applications.</summary>
	// Token: 0x020005BF RID: 1471
	[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	public sealed class PassportAuthentication : ConfigurationElement
	{
		// Token: 0x06003F3F RID: 16191 RVA: 0x000A53DC File Offset: 0x000A35DC
		static PassportAuthentication()
		{
			PassportAuthentication.properties.Add(PassportAuthentication.redirectUrlProp);
			PassportAuthentication.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(PassportAuthentication), new ValidatorCallback(PassportAuthentication.ValidateElement)));
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x00003946 File Offset: 0x00001B46
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x06003F41 RID: 16193 RVA: 0x000A5445 File Offset: 0x000A3645
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return PassportAuthentication.elementProperty;
			}
		}

		/// <summary>Gets or sets the URL to which the request is redirected.</summary>
		/// <returns>The URL of the page to which the request is redirected.</returns>
		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x06003F42 RID: 16194 RVA: 0x000A544C File Offset: 0x000A364C
		// (set) Token: 0x06003F43 RID: 16195 RVA: 0x000A545E File Offset: 0x000A365E
		[StringValidator]
		[ConfigurationProperty("redirectUrl", DefaultValue = "internal")]
		public string RedirectUrl
		{
			get
			{
				return (string)base[PassportAuthentication.redirectUrlProp];
			}
			set
			{
				base[PassportAuthentication.redirectUrlProp] = value;
			}
		}

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x06003F44 RID: 16196 RVA: 0x000A546C File Offset: 0x000A366C
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PassportAuthentication.properties;
			}
		}

		// Token: 0x04002289 RID: 8841
		private static ConfigurationProperty redirectUrlProp = new ConfigurationProperty("redirectUrl", typeof(string), "internal");

		// Token: 0x0400228A RID: 8842
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400228B RID: 8843
		private static ConfigurationElementProperty elementProperty;
	}
}
