using System;

namespace System.Web.Services
{
	/// <summary>Used to add additional information to an XML Web service, such as a string describing its functionality.</summary>
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public sealed class WebServiceAttribute : Attribute
	{
		/// <summary>A descriptive message for the XML Web service.</summary>
		/// <returns>The text describing the functionality of the XML Web service.</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000027C6 File Offset: 0x000009C6
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000027DC File Offset: 0x000009DC
		public string Description
		{
			get
			{
				if (this.description != null)
				{
					return this.description;
				}
				return string.Empty;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Gets or sets the default XML namespace to use for the XML Web service.</summary>
		/// <returns>The default XML namespace to use for the XML Web service. The default is specified in the <see cref="F:System.Web.Services.WebServiceAttribute.DefaultNamespace" /> property.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000027E5 File Offset: 0x000009E5
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000027ED File Offset: 0x000009ED
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		/// <summary>Gets or sets the name of the XML Web service.</summary>
		/// <returns>The name for the XML Web service. Default value is the name of the class implementing the XML Web service.</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000027F6 File Offset: 0x000009F6
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000280C File Offset: 0x00000A0C
		public string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x04000078 RID: 120
		private string description;

		// Token: 0x04000079 RID: 121
		private string ns = "http://tempuri.org/";

		// Token: 0x0400007A RID: 122
		private string name;

		/// <summary>The default value for the <see cref="P:System.Web.Services.WebServiceAttribute.Namespace" /> property. This field is constant.</summary>
		// Token: 0x0400007B RID: 123
		public const string DefaultNamespace = "http://tempuri.org/";
	}
}
