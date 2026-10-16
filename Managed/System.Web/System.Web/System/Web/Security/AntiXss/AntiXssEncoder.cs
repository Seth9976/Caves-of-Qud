using System;
using System.IO;
using System.Text;
using System.Web.Util;
using Unity;

namespace System.Web.Security.AntiXss
{
	/// <summary>Encodes a string for use in HTML, XML, CSS, and URL strings.</summary>
	// Token: 0x020006E8 RID: 1768
	public class AntiXssEncoder : global::System.Web.Util.HttpEncoder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.AntiXss.AntiXssEncoder" /> class.</summary>
		// Token: 0x06004AC2 RID: 19138 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public AntiXssEncoder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Encodes the specified string for use in cascading style sheets (CSS).</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <exception cref="T:Microsoft.Security.Application.InvalidUnicodeValueException">
		///   <paramref name="input" /> contains a character that has an invalid Unicode value.</exception>
		/// <exception cref="T:Microsoft.Security.Application.InvalidSurrogatePairException">
		///   <paramref name="input" /> contained a high surrogate code point that was not followed by a low surrogate code point.-or-<paramref name="input" /> contained a low surrogate code point that was not preceded by a high surrogate code point.</exception>
		// Token: 0x06004AC3 RID: 19139 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static string CssEncode(string input)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes and outputs the specified string for use in an HTML attribute.</summary>
		/// <param name="value">The string to encode.</param>
		/// <param name="output">The text writer to use to output the string.</param>
		/// <exception cref="T:Microsoft.Security.Application.InvalidUnicodeValueException">
		///   <paramref name="input" /> contains a character that has an invalid Unicode value.</exception>
		/// <exception cref="T:Microsoft.Security.Application.InvalidSurrogatePairException">
		///   <paramref name="input" /> contained a high surrogate code point that was not followed by a low surrogate code point.-or-<paramref name="input" /> contained a low surrogate code point that was not preceded by a high surrogate code point.</exception>
		// Token: 0x06004AC4 RID: 19140 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal override void HtmlAttributeEncode(string value, TextWriter output)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Encodes the specified string for use as text in HTML markup and optionally specifies whether to use HTML 4.0 named entities.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <param name="useNamedEntities">true to use HTML 4.0 named entities for certain character encodings; false to encode by using only &amp;#DECIMAL; notation.</param>
		/// <exception cref="T:Microsoft.Security.Application.InvalidUnicodeValueException">
		///   <paramref name="input" /> contains a character that has an invalid Unicode value.</exception>
		/// <exception cref="T:Microsoft.Security.Application.InvalidSurrogatePairException">
		///   <paramref name="input" /> contained a high surrogate code point that was not followed by a low surrogate code point.-or-<paramref name="input" /> contained a low surrogate code point that was not preceded by a high surrogate code point.</exception>
		// Token: 0x06004AC5 RID: 19141 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static string HtmlEncode(string input, bool useNamedEntities)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for use as text in HTML markup and outputs the string by using the specified text writer.</summary>
		/// <param name="value">The string to encode.</param>
		/// <param name="output">The text writer to use to output the string.</param>
		/// <exception cref="T:Microsoft.Security.Application.InvalidUnicodeValueException">
		///   <paramref name="input" /> contains a character that has an invalid Unicode value.</exception>
		/// <exception cref="T:Microsoft.Security.Application.InvalidSurrogatePairException">
		///   <paramref name="input" /> contained a high surrogate code point that was not followed by a low surrogate code point.-or-<paramref name="input" /> contained a low surrogate code point that was not preceded by a high surrogate code point.</exception>
		// Token: 0x06004AC6 RID: 19142 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal override void HtmlEncode(string value, TextWriter output)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Encodes the specified string for use in form submissions whose MIME type is "application/x-www-form-urlencoded".</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		// Token: 0x06004AC7 RID: 19143 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static string HtmlFormUrlEncode(string input)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for use in form submissions whose MIME type is "application/x-www-form-urlencoded" by using the specified code page.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <param name="codePage">The code page to use to encode the <paramref name="input" /> string.</param>
		// Token: 0x06004AC8 RID: 19144 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static string HtmlFormUrlEncode(string input, int codePage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for form submissions whose MIME type is "application/x-www-form-urlencoded" by using the specified character encoding type.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <param name="inputEncoding">The input encoding type.</param>
		// Token: 0x06004AC9 RID: 19145 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static string HtmlFormUrlEncode(string input, Encoding inputEncoding)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Marks characters from the specified Unicode code charts as safe.</summary>
		/// <param name="lowerCodeCharts">The combination of lower code charts to mark as safe.</param>
		/// <param name="lowerMidCodeCharts">The combination of lower-middle code charts to mark as safe.</param>
		/// <param name="midCodeCharts">The combination of middle code charts to mark as safe.</param>
		/// <param name="upperMidCodeCharts">The combination of upper-middle code charts to mark as safe.</param>
		/// <param name="upperCodeCharts">The combination of upper code charts to mark as safe.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.AntiXss.AntiXssEncoder.MarkAsSafe(System.Web.Security.AntiXss.LowerCodeCharts,System.Web.Security.AntiXss.LowerMidCodeCharts,System.Web.Security.AntiXss.MidCodeCharts,System.Web.Security.AntiXss.UpperMidCodeCharts,System.Web.Security.AntiXss.UpperCodeCharts)" /> method was called outside the Application_Start method in the Global.asax file.</exception>
		// Token: 0x06004ACA RID: 19146 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static void MarkAsSafe(LowerCodeCharts lowerCodeCharts, LowerMidCodeCharts lowerMidCodeCharts, MidCodeCharts midCodeCharts, UpperMidCodeCharts upperMidCodeCharts, UpperCodeCharts upperCodeCharts)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Encodes the specified byte array for use in a URL, starting at the specified offset in the byte array and encoding the specified number of bytes.</summary>
		/// <returns>The encoded byte array.</returns>
		/// <param name="bytes">The byte array to encode.</param>
		/// <param name="offset">The index of the first byte to encode.</param>
		/// <param name="count">The number of bytes to encode.</param>
		// Token: 0x06004ACB RID: 19147 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected internal override byte[] UrlEncode(byte[] bytes, int offset, int count)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for use in a URL.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		// Token: 0x06004ACC RID: 19148 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static string UrlEncode(string input)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for use in a URL by using the specified code page.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <param name="codePage">The code page to use to encode the <paramref name="input" /> string.</param>
		// Token: 0x06004ACD RID: 19149 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static string UrlEncode(string input, int codePage)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for use in a URL by using the specified character encoding type.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <param name="inputEncoding">The input encoding type.</param>
		// Token: 0x06004ACE RID: 19150 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static string UrlEncode(string input, Encoding inputEncoding)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes path strings for use in a URL.</summary>
		/// <returns>The URL that contains the encoded path.</returns>
		/// <param name="value">The string to encode.</param>
		// Token: 0x06004ACF RID: 19151 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected internal override string UrlPathEncode(string value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for use in XML attributes.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <exception cref="T:Microsoft.Security.Application.InvalidUnicodeValueException">
		///   <paramref name="input" /> contains a character that has an invalid Unicode value.</exception>
		/// <exception cref="T:Microsoft.Security.Application.InvalidSurrogatePairException">
		///   <paramref name="input" /> contained a high surrogate code point that was not followed by a low surrogate code point.-or-<paramref name="input" /> contained a low surrogate code point that was not preceded by a high surrogate code point.</exception>
		// Token: 0x06004AD0 RID: 19152 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static string XmlAttributeEncode(string input)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Encodes the specified string for use in XML attributes.</summary>
		/// <returns>The encoded string.</returns>
		/// <param name="input">The string to encode.</param>
		/// <exception cref="T:Microsoft.Security.Application.InvalidUnicodeValueException">
		///   <paramref name="input" /> contains a character that has an invalid Unicode value.</exception>
		/// <exception cref="T:Microsoft.Security.Application.InvalidSurrogatePairException">
		///   <paramref name="input" /> contained a high surrogate code point that was not followed by a low surrogate code point.-or-<paramref name="input" /> contained a low surrogate code point that was not preceded by a high surrogate code point.</exception>
		// Token: 0x06004AD1 RID: 19153 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static string XmlEncode(string input)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
