using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Unity;

namespace System.Web
{
	/// <summary>Provides a type-safe way to set the <see cref="P:System.Web.HttpCachePolicy.VaryByContentEncodings" /> property of the <see cref="T:System.Web.HttpCachePolicy" /> class.</summary>
	// Token: 0x02000084 RID: 132
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpCacheVaryByContentEncodings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCacheVaryByContentEncodings" /> class.</summary>
		// Token: 0x060005E0 RID: 1504 RVA: 0x0000E97F File Offset: 0x0000CB7F
		public HttpCacheVaryByContentEncodings()
		{
			this.encodings = new Dictionary<string, bool>();
		}

		/// <summary>Gets or sets a value that indicates whether the cache varies according to the specified content encoding.</summary>
		/// <returns>true if the cache should vary by the specified content encoding; otherwise, false.</returns>
		/// <param name="contentEncoding">The name of the content encoding.</param>
		/// <exception cref="T:System.ArgumentNullException">The content encoding is null.</exception>
		// Token: 0x1700022C RID: 556
		public bool this[string contentEncoding]
		{
			get
			{
				if (contentEncoding == null)
				{
					throw new ArgumentNullException("contentEncoding");
				}
				return this.encodings.ContainsKey(contentEncoding) && this.encodings[contentEncoding];
			}
			set
			{
				if (contentEncoding == null)
				{
					throw new ArgumentNullException("contentEncoding");
				}
				this.encodings[contentEncoding] = value;
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string[] GetContentEncodings()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void SetContentEncodings(string[] contentEncodings)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000F1C RID: 3868
		private Dictionary<string, bool> encodings;
	}
}
