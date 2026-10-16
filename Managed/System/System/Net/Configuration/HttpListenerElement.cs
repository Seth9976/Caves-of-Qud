using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the HttpListener element in the configuration file. This class cannot be inherited.</summary>
	// Token: 0x02000878 RID: 2168
	public sealed class HttpListenerElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.HttpListenerElement" /> class.</summary>
		// Token: 0x060044B0 RID: 17584 RVA: 0x00013BAE File Offset: 0x00011DAE
		public HttpListenerElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the default timeout elements used for an <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Configuration.HttpListenerTimeoutsElement" />.The timeout elements used for an <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x060044B1 RID: 17585 RVA: 0x00032868 File Offset: 0x00030A68
		public HttpListenerTimeoutsElement Timeouts
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates if <see cref="T:System.Net.HttpListener" /> uses the raw unescaped URI instead of the converted URI.</summary>
		/// <returns>A Boolean value that indicates if <see cref="T:System.Net.HttpListener" /> uses the raw unescaped URI, rather than the converted URI.</returns>
		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x060044B2 RID: 17586 RVA: 0x000ED104 File Offset: 0x000EB304
		public bool UnescapeRequestUrl
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}
	}
}
