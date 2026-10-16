using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>The <see cref="T:System.Web.Configuration.BrowserCapabilitiesFactoryBase" /> class is the base class from which <see cref="T:System.Web.Configuration.BrowserCapabilitiesFactory" /> is derived. It is used internally at run time by the configuration system to create request-specific instances of the <see cref="T:System.Web.Configuration.HttpCapabilitiesBase" /> class, publicly accessed through the ASP.NET intrinsic Request.Browser property.</summary>
	// Token: 0x02000776 RID: 1910
	public class BrowserCapabilitiesFactoryBase
	{
		/// <summary>Used internally by the configuration system to create a new instance of this class.</summary>
		// Token: 0x06004DC1 RID: 19905 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public BrowserCapabilitiesFactoryBase()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Used internally by the configuration system to represent a collection of information relating to various browser capabilities.</summary>
		/// <returns>A dictionary of browser capabilities.</returns>
		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x06004DC2 RID: 19906 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected IDictionary BrowserElements
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Used internally by the configuration system to represent a collection of request-header values.</summary>
		/// <returns>A collection of request headers.</returns>
		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x06004DC3 RID: 19907 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected IDictionary MatchedHeaders
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Used internally at run time to configure an <see cref="T:System.Web.HttpBrowserCapabilities" /> object.</summary>
		/// <param name="headers">A collection of request headers.</param>
		/// <param name="browserCaps">An <see cref="T:System.Web.HttpBrowserCapabilities" /> object.</param>
		// Token: 0x06004DC4 RID: 19908 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void ConfigureBrowserCapabilities(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Used internally at run time to configure custom hierarchies of browser capabilities.</summary>
		/// <param name="headers">A collection of request headers.</param>
		/// <param name="browserCaps">An object that specifies the capabilities of the browser.</param>
		// Token: 0x06004DC5 RID: 19909 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void ConfigureCustomCapabilities(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Used internally by the configuration system to determine whether the browser represented by the specified <see cref="T:System.Web.HttpBrowserCapabilities" /> object does not have an available adapter.</summary>
		/// <returns>true if the represented browser does not have an available adapter; otherwise, false.</returns>
		/// <param name="browserCaps">An <see cref="T:System.Web.HttpBrowserCapabilities" /> object.</param>
		// Token: 0x06004DC6 RID: 19910 RVA: 0x000C89B8 File Offset: 0x000C6BB8
		protected bool IsBrowserUnknown(HttpCapabilitiesBase browserCaps)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Used internally by the configuration system to populate a collection of browser capabilities based on the supported browser.</summary>
		/// <param name="dictionary">A collection of key/value pairs representing the browser capabilities.</param>
		// Token: 0x06004DC7 RID: 19911 RVA: 0x0000B3D8 File Offset: 0x000095D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void PopulateBrowserElements(IDictionary dictionary)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Used internally by the configuration system to populate a collection of request headers based on the supported browser.</summary>
		/// <param name="dictionary">A collection of key/value pairs representing the browser capabilities.</param>
		// Token: 0x06004DC8 RID: 19912 RVA: 0x0000B3D8 File Offset: 0x000095D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void PopulateMatchedHeaders(IDictionary dictionary)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
