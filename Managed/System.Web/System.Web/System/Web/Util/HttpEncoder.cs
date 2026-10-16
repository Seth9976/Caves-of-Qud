using System;
using System.IO;
using Unity;

namespace System.Web.Util
{
	/// <summary>Provides encoding and decoding logic.</summary>
	// Token: 0x020006E9 RID: 1769
	public class HttpEncoder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Util.HttpEncoder" /> class. </summary>
		// Token: 0x06004AD2 RID: 19154 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public HttpEncoder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or set the <see cref="T:System.Web.Util.HttpEncoder" /> type that will be used in an application.</summary>
		/// <returns>A reference to the encoder/decoder type that will be used. </returns>
		/// <exception cref="T:System.ArgumentNullException">The encoder instance that is being assigned to this property is null. </exception>
		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06004AD3 RID: 19155 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004AD4 RID: 19156 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static HttpEncoder Current
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a reference to the default encoder for ASP.NET. </summary>
		/// <returns>A reference to the default encoder.</returns>
		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x06004AD5 RID: 19157 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static HttpEncoder Default
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Encodes a header name and value into a string that can be used as an HTTP header.</summary>
		/// <param name="headerName">The HTTP header name to encode.</param>
		/// <param name="headerValue">The HTTP header value to encode. </param>
		/// <param name="encodedHeaderName">When this method returns, contains the encoded name to use for an HTTP header. This parameter is passed uninitialized.</param>
		/// <param name="encodedHeaderValue">When this method returns, contains the encoded value to use for an HTTP header. This parameter is passed uninitialized.</param>
		// Token: 0x06004AD6 RID: 19158 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal virtual void HeaderNameValueEncode(string headerName, string headerValue, out string encodedHeaderName, out string encodedHeaderValue)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Encodes an incoming value into a string that can be inserted into an HTML attribute that is delimited by using single or double quotation marks. </summary>
		/// <param name="value">The string to encode.</param>
		/// <param name="output">The text writer to write the encoded value to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="output" /> is null. </exception>
		// Token: 0x06004AD7 RID: 19159 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal virtual void HtmlAttributeEncode(string value, TextWriter output)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Decodes a value from an HTML-encoded string.</summary>
		/// <param name="value">The string to decode.</param>
		/// <param name="output">The text writer to write the decoded value to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="output" /> is null. </exception>
		// Token: 0x06004AD8 RID: 19160 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal virtual void HtmlDecode(string value, TextWriter output)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Encodes a string into an HTML-encoded string. </summary>
		/// <param name="value">The string to encode. </param>
		/// <param name="output">The text writer to write the encoded value to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="output" /> is null. </exception>
		// Token: 0x06004AD9 RID: 19161 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal virtual void HtmlEncode(string value, TextWriter output)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Encodes a string.</summary>
		/// <returns>An encoded string.</returns>
		/// <param name="value">The string to encode.</param>
		// Token: 0x06004ADA RID: 19162 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected internal virtual string JavaScriptStringEncode(string value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes an array of characters that are not allowed in a URL into a hexadecimal character-entity equivalent.</summary>
		/// <returns>An array of encoded characters.</returns>
		/// <param name="bytes">An array of bytes to encode. </param>
		/// <param name="offset">The position in the <paramref name="bytes" /> array at which to begin encoding.</param>
		/// <param name="count">The number of items in the <paramref name="bytes" /> array to encode.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than zero or greater than the length of the <paramref name="bytes" /> array. -or-<paramref name="count" /> is less than zero or <paramref name="count" /> plus <paramref name="offset" /> is greater than the length of the <paramref name="bytes" /> array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		// Token: 0x06004ADB RID: 19163 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected internal virtual byte[] UrlEncode(byte[] bytes, int offset, int count)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes a subsection of a URL.</summary>
		/// <returns>A URL-encoded string. </returns>
		/// <param name="value">The string to encode.</param>
		// Token: 0x06004ADC RID: 19164 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected internal virtual string UrlPathEncode(string value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
