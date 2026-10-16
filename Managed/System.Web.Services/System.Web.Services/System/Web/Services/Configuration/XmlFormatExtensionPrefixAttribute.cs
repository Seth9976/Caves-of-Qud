using System;

namespace System.Web.Services.Configuration
{
	/// <summary>Specifies the XML namespace and XML namespace prefix to use for a service description format extension within a service description. This class cannot be inherited.</summary>
	// Token: 0x02000150 RID: 336
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class XmlFormatExtensionPrefixAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionPrefixAttribute" /> class.</summary>
		// Token: 0x06000A68 RID: 2664 RVA: 0x0000289F File Offset: 0x00000A9F
		public XmlFormatExtensionPrefixAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionPrefixAttribute" /> class, setting the XML namespace and XML namespace prefix for a service description format extension.</summary>
		/// <param name="prefix">The XML namespace prefix associated with a service description format extension.</param>
		/// <param name="ns">The XML namespace associated with a service description format extension.</param>
		// Token: 0x06000A69 RID: 2665 RVA: 0x000456E0 File Offset: 0x000438E0
		public XmlFormatExtensionPrefixAttribute(string prefix, string ns)
		{
			this.prefix = prefix;
			this.ns = ns;
		}

		/// <summary>Gets or sets the XML namespace prefix associated with a service description format extension.</summary>
		/// <returns>The XML namespace prefix associated with a service description format extension.</returns>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x000456F6 File Offset: 0x000438F6
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x0004570C File Offset: 0x0004390C
		public string Prefix
		{
			get
			{
				if (this.prefix != null)
				{
					return this.prefix;
				}
				return string.Empty;
			}
			set
			{
				this.prefix = value;
			}
		}

		/// <summary>Gets or sets the XML namespace associated with a service description format extension.</summary>
		/// <returns>The XML namespace associated with a service description format extension.</returns>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00045715 File Offset: 0x00043915
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x0004572B File Offset: 0x0004392B
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

		// Token: 0x040005D9 RID: 1497
		private string prefix;

		// Token: 0x040005DA RID: 1498
		private string ns;
	}
}
