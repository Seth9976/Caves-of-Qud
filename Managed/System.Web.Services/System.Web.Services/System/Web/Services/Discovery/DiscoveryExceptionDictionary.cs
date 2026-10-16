using System;
using System.Collections;

namespace System.Web.Services.Discovery
{
	/// <summary>Collects exceptions that occurred during XML Web services discovery. This class cannot be inherited.</summary>
	// Token: 0x020000AA RID: 170
	public sealed class DiscoveryExceptionDictionary : DictionaryBase
	{
		/// <summary>Gets or sets the <see cref="T:System.Exception" /> that occurred while discovering the specified URL from the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that was thrown discovering <paramref name="url" />.</returns>
		/// <param name="url">The URL of the discovery document that caused an exception to be thrown during XML Web services discovery. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. </exception>
		// Token: 0x1700012D RID: 301
		public Exception this[string url]
		{
			get
			{
				return (Exception)base.Dictionary[url];
			}
			set
			{
				base.Dictionary[url] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.ICollection" /> object with all the keys in the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />.</returns>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00012AC7 File Offset: 0x00010CC7
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.ICollection" /> object containing all the values in the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />.</returns>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00012AD4 File Offset: 0x00010CD4
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		/// <summary>Adds an <see cref="T:System.Exception" /> with a key of <paramref name="url" /> to the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />.</summary>
		/// <param name="url">The URL that caused an exception during XML Web services discovery. </param>
		/// <param name="value">The <see cref="T:System.Exception" /> that occurred during XML Web services discovery. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An entry with a key of <paramref name="url" /> already exists in the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />. </exception>
		// Token: 0x0600046E RID: 1134 RVA: 0x00012AE1 File Offset: 0x00010CE1
		public void Add(string url, Exception value)
		{
			base.Dictionary.Add(url, value);
		}

		/// <summary>Determines whether the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" /> contains an <see cref="T:System.Exception" /> with the specified URL.</summary>
		/// <returns>true if the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" /> contains an <see cref="T:System.Exception" /> with the specified URL; otherwise, false.</returns>
		/// <param name="url">The URL of the <see cref="T:System.Exception" /> to locate within the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. </exception>
		// Token: 0x0600046F RID: 1135 RVA: 0x00012AF0 File Offset: 0x00010CF0
		public bool Contains(string url)
		{
			return base.Dictionary.Contains(url);
		}

		/// <summary>Removes an <see cref="T:System.Exception" /> with the specified URL from the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />.</summary>
		/// <param name="url">The URL of the <see cref="T:System.Exception" /> to remove from the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. </exception>
		// Token: 0x06000470 RID: 1136 RVA: 0x00012AFE File Offset: 0x00010CFE
		public void Remove(string url)
		{
			base.Dictionary.Remove(url);
		}
	}
}
