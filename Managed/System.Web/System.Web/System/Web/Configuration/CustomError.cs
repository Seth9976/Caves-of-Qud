using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures a <see cref="T:System.Web.Configuration.CustomError" /> section to map an ASP.NET error code to a custom page. This class cannot be inherited.</summary>
	// Token: 0x02000591 RID: 1425
	public sealed class CustomError : ConfigurationElement
	{
		// Token: 0x06003C53 RID: 15443 RVA: 0x0009EAEC File Offset: 0x0009CCEC
		static CustomError()
		{
			CustomError.properties.Add(CustomError.redirectProp);
			CustomError.properties.Add(CustomError.statusCodeProp);
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal CustomError()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CustomError" /> class. </summary>
		/// <param name="statusCode">The HTTP status code that will result in redirection to the error page.</param>
		/// <param name="redirect">The URL of the custom page mapped to the error code.</param>
		// Token: 0x06003C55 RID: 15445 RVA: 0x0009EB87 File Offset: 0x0009CD87
		public CustomError(int statusCode, string redirect)
		{
			this.StatusCode = statusCode;
			this.Redirect = redirect;
		}

		/// <summary>Compares <see cref="T:System.Web.Configuration.CustomError" /> errors.</summary>
		/// <returns>true if the errors  are equal; otherwise, false.</returns>
		/// <param name="customError">The error to compare against.</param>
		// Token: 0x06003C56 RID: 15446 RVA: 0x0009EBA0 File Offset: 0x0009CDA0
		public override bool Equals(object customError)
		{
			CustomError customError2 = customError as CustomError;
			return customError2 != null && this.Redirect == customError2.Redirect && this.StatusCode == customError2.StatusCode;
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> object hash code.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> object hash code.</returns>
		// Token: 0x06003C57 RID: 15447 RVA: 0x0009EBDC File Offset: 0x0009CDDC
		public override int GetHashCode()
		{
			return this.Redirect.GetHashCode() + this.StatusCode;
		}

		/// <summary>Gets or sets the redirection URL.</summary>
		/// <returns>The URL to which the application is redirected when an error occurs.</returns>
		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x0009EBF0 File Offset: 0x0009CDF0
		// (set) Token: 0x06003C59 RID: 15449 RVA: 0x0009EC02 File Offset: 0x0009CE02
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("redirect", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Redirect
		{
			get
			{
				return (string)base[CustomError.redirectProp];
			}
			set
			{
				base[CustomError.redirectProp] = value;
			}
		}

		/// <summary>Gets or sets the HTTP error status code.</summary>
		/// <returns>The HTTP error status code that causes the redirection to the custom error page.</returns>
		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x0009EC10 File Offset: 0x0009CE10
		// (set) Token: 0x06003C5B RID: 15451 RVA: 0x0009EC22 File Offset: 0x0009CE22
		[ConfigurationProperty("statusCode", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		[IntegerValidator(MinValue = 100, MaxValue = 999)]
		public int StatusCode
		{
			get
			{
				return (int)base[CustomError.statusCodeProp];
			}
			set
			{
				base[CustomError.statusCodeProp] = value;
			}
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06003C5C RID: 15452 RVA: 0x0009EC35 File Offset: 0x0009CE35
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CustomError.properties;
			}
		}

		// Token: 0x040020C7 RID: 8391
		private static ConfigurationProperty redirectProp = new ConfigurationProperty("redirect", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), new StringValidator(1), ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020C8 RID: 8392
		private static ConfigurationProperty statusCodeProp = new ConfigurationProperty("statusCode", typeof(int), null, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(100, 999), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020C9 RID: 8393
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
