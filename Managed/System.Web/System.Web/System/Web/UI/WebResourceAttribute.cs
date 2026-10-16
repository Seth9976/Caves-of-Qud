using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI
{
	/// <summary>Defines the metadata attribute that enables an embedded resource in an assembly. This class cannot be inherited. </summary>
	// Token: 0x0200024A RID: 586
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class WebResourceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebResourceAttribute" /> class with the specified Web resource and resource content type.</summary>
		/// <param name="webResource">The name of the of Web resource.</param>
		/// <param name="contentType">The type of resource, such as "image/gif" or "text/javascript".</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="webResource" /> is null or an empty string ("").- or -<paramref name="contentType" /> is null or an empty string ("").</exception>
		// Token: 0x060017DC RID: 6108 RVA: 0x0003E40D File Offset: 0x0003C60D
		public WebResourceAttribute(string webResource, string contentType)
		{
			this.webResource = webResource;
			this.contentType = contentType;
		}

		/// <summary>Gets a string containing the MIME type of the resource that is referenced by the <see cref="T:System.Web.UI.WebResourceAttribute" /> class.</summary>
		/// <returns>The content type of the resource.</returns>
		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x0003E423 File Offset: 0x0003C623
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether, during processing of the embedded resource referenced by the <see cref="T:System.Web.UI.WebResourceAttribute" /> class, other Web resource URLs are parsed and replaced with the full path to the resource.</summary>
		/// <returns>true if embedded resources are resolved during processing of the resource; otherwise, false. The default is false.</returns>
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x0003E42B File Offset: 0x0003C62B
		// (set) Token: 0x060017DF RID: 6111 RVA: 0x0003E433 File Offset: 0x0003C633
		public bool PerformSubstitution
		{
			get
			{
				return this.performSubstitution;
			}
			set
			{
				this.performSubstitution = value;
			}
		}

		/// <summary>Gets a string containing the name of the resource that is referenced by the <see cref="T:System.Web.UI.WebResourceAttribute" /> class.</summary>
		/// <returns>The name of the resource.</returns>
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0003E43C File Offset: 0x0003C63C
		public string WebResource
		{
			get
			{
				return this.webResource;
			}
		}

		/// <summary>Gets or set the path of a Content Delivery Network (CDN) that contains Web resources.</summary>
		/// <returns>The path of a Content Delivery Network (CDN).</returns>
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060017E2 RID: 6114 RVA: 0x00003946 File Offset: 0x00001B46
		public string CdnPath
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
			}
		}

		/// <summary>Gets or set a value that indicates to the <see cref="T:System.Web.UI.ScriptManager" /> whether a script resource should be accessed using a secure connection to the content delivery network (CDN) path when the page is accessed using HTTPS.</summary>
		/// <returns>true if the CDN should be accessed using HTTPS; otherwise, false.</returns>
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x0003E444 File Offset: 0x0003C644
		// (set) Token: 0x060017E4 RID: 6116 RVA: 0x00003946 File Offset: 0x00001B46
		public bool CdnSupportsSecureConnection
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
			}
		}

		/// <summary>Gets or sets an expression that is used when a Web resource has successfully loaded.</summary>
		/// <returns>An expression that is used when a Web resource has successfully loaded.</returns>
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060017E6 RID: 6118 RVA: 0x00003946 File Offset: 0x00001B46
		public string LoadSuccessExpression
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x0400160A RID: 5642
		private bool performSubstitution;

		// Token: 0x0400160B RID: 5643
		private string webResource;

		// Token: 0x0400160C RID: 5644
		private string contentType;
	}
}
