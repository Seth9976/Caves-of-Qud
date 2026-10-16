using System;

namespace System.Web.Services
{
	/// <summary>Declares a binding that defines one or more XML Web service methods. This class cannot be inherited.</summary>
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
	public sealed class WebServiceBindingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebServiceBindingAttribute" /> class.</summary>
		// Token: 0x06000042 RID: 66 RVA: 0x0000289F File Offset: 0x00000A9F
		public WebServiceBindingAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebServiceBindingAttribute" /> class setting the name of the binding the XML Web service method is implementing.</summary>
		/// <param name="name">The name of the binding an XML Web service method is implementing an operation for. Sets the <see cref="P:System.Web.Services.WebServiceBindingAttribute.Name" /> property. </param>
		// Token: 0x06000043 RID: 67 RVA: 0x000028A7 File Offset: 0x00000AA7
		public WebServiceBindingAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebServiceBindingAttribute" /> class.</summary>
		/// <param name="name">The name of the binding an XML Web service method is implementing an operation for. Sets the <see cref="P:System.Web.Services.WebServiceBindingAttribute.Name" /> property. </param>
		/// <param name="ns">The namespace associated with the binding. Sets the <see cref="P:System.Web.Services.WebServiceBindingAttribute.Namespace" /> property. </param>
		// Token: 0x06000044 RID: 68 RVA: 0x000028B6 File Offset: 0x00000AB6
		public WebServiceBindingAttribute(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebServiceBindingAttribute" /> class.</summary>
		/// <param name="name">The name of the binding an XML Web service method is implementing an operation for. Sets the <see cref="P:System.Web.Services.WebServiceBindingAttribute.Name" /> property. </param>
		/// <param name="ns">The namespace associated with the binding. Sets the <see cref="P:System.Web.Services.WebServiceBindingAttribute.Namespace" /> property. </param>
		/// <param name="location">The location where the binding is defined. </param>
		// Token: 0x06000045 RID: 69 RVA: 0x000028CC File Offset: 0x00000ACC
		public WebServiceBindingAttribute(string name, string ns, string location)
		{
			this.name = name;
			this.ns = ns;
			this.location = location;
		}

		/// <summary>Gets or sets the Web Services Interoperability (WSI) specification to which the binding claims to conform.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.WsiProfiles" /> values, indicating a WSI specification.</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000028E9 File Offset: 0x00000AE9
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000028F1 File Offset: 0x00000AF1
		public WsiProfiles ConformsTo
		{
			get
			{
				return this.claims;
			}
			set
			{
				this.claims = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the binding emits conformance claims.</summary>
		/// <returns>true if the binding emits conformance claims; otherwise, false.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000028FA File Offset: 0x00000AFA
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002902 File Offset: 0x00000B02
		public bool EmitConformanceClaims
		{
			get
			{
				return this.emitClaims;
			}
			set
			{
				this.emitClaims = value;
			}
		}

		/// <summary>Gets or sets the location where the binding is defined.</summary>
		/// <returns>The location where the binding is defined. The default is the URL of the XML Web service to which the attribute is applied.</returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000290B File Offset: 0x00000B0B
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002921 File Offset: 0x00000B21
		public string Location
		{
			get
			{
				if (this.location != null)
				{
					return this.location;
				}
				return string.Empty;
			}
			set
			{
				this.location = value;
			}
		}

		/// <summary>Gets or sets the name of the binding.</summary>
		/// <returns>The name of the binding. The default is the name of the XML Web service with "Soap" appended.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000292A File Offset: 0x00000B2A
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002940 File Offset: 0x00000B40
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

		/// <summary>Gets or sets the namespace associated with the binding.</summary>
		/// <returns>The namespace for the binding. The default is http://tempuri.org/.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002949 File Offset: 0x00000B49
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000295F File Offset: 0x00000B5F
		public string Namespace
		{
			get
			{
				if (this.ns != null)
				{
					return this.ns;
				}
				return string.Empty;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x0400007C RID: 124
		private string name;

		// Token: 0x0400007D RID: 125
		private string ns;

		// Token: 0x0400007E RID: 126
		private string location;

		// Token: 0x0400007F RID: 127
		private WsiProfiles claims;

		// Token: 0x04000080 RID: 128
		private bool emitClaims;
	}
}
