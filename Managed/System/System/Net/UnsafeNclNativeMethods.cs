using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Net
{
	// Token: 0x02000662 RID: 1634
	internal static class UnsafeNclNativeMethods
	{
		// Token: 0x02000663 RID: 1635
		internal static class HttpApi
		{
			// Token: 0x04001E4D RID: 7757
			private const int HttpHeaderRequestMaximum = 41;

			// Token: 0x04001E4E RID: 7758
			private const int HttpHeaderResponseMaximum = 30;

			// Token: 0x04001E4F RID: 7759
			private static string[] m_Strings = new string[]
			{
				"Cache-Control", "Connection", "Date", "Keep-Alive", "Pragma", "Trailer", "Transfer-Encoding", "Upgrade", "Via", "Warning",
				"Allow", "Content-Length", "Content-Type", "Content-Encoding", "Content-Language", "Content-Location", "Content-MD5", "Content-Range", "Expires", "Last-Modified",
				"Accept-Ranges", "Age", "ETag", "Location", "Proxy-Authenticate", "Retry-After", "Server", "Set-Cookie", "Vary", "WWW-Authenticate"
			};

			// Token: 0x02000664 RID: 1636
			internal static class HTTP_REQUEST_HEADER_ID
			{
				// Token: 0x060033AE RID: 13230 RVA: 0x000B41BA File Offset: 0x000B23BA
				internal static string ToString(int position)
				{
					return UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.m_Strings[position];
				}

				// Token: 0x04001E50 RID: 7760
				private static string[] m_Strings = new string[]
				{
					"Cache-Control", "Connection", "Date", "Keep-Alive", "Pragma", "Trailer", "Transfer-Encoding", "Upgrade", "Via", "Warning",
					"Allow", "Content-Length", "Content-Type", "Content-Encoding", "Content-Language", "Content-Location", "Content-MD5", "Content-Range", "Expires", "Last-Modified",
					"Accept", "Accept-Charset", "Accept-Encoding", "Accept-Language", "Authorization", "Cookie", "Expect", "From", "Host", "If-Match",
					"If-Modified-Since", "If-None-Match", "If-Range", "If-Unmodified-Since", "Max-Forwards", "Proxy-Authorization", "Referer", "Range", "Te", "Translate",
					"User-Agent"
				};
			}

			// Token: 0x02000665 RID: 1637
			internal static class HTTP_RESPONSE_HEADER_ID
			{
				// Token: 0x060033B0 RID: 13232 RVA: 0x000B4348 File Offset: 0x000B2548
				static HTTP_RESPONSE_HEADER_ID()
				{
					for (int i = 0; i < 30; i++)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Hashtable.Add(UnsafeNclNativeMethods.HttpApi.m_Strings[i], i);
					}
				}

				// Token: 0x060033B1 RID: 13233 RVA: 0x000B4388 File Offset: 0x000B2588
				internal static int IndexOfKnownHeader(string HeaderName)
				{
					object obj = UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Hashtable[HeaderName];
					if (obj != null)
					{
						return (int)obj;
					}
					return -1;
				}

				// Token: 0x060033B2 RID: 13234 RVA: 0x000B43AC File Offset: 0x000B25AC
				internal static string ToString(int position)
				{
					return UnsafeNclNativeMethods.HttpApi.m_Strings[position];
				}

				// Token: 0x04001E51 RID: 7761
				private static Hashtable m_Hashtable = new Hashtable(30);
			}

			// Token: 0x02000666 RID: 1638
			internal enum Enum
			{
				// Token: 0x04001E53 RID: 7763
				HttpHeaderCacheControl,
				// Token: 0x04001E54 RID: 7764
				HttpHeaderConnection,
				// Token: 0x04001E55 RID: 7765
				HttpHeaderDate,
				// Token: 0x04001E56 RID: 7766
				HttpHeaderKeepAlive,
				// Token: 0x04001E57 RID: 7767
				HttpHeaderPragma,
				// Token: 0x04001E58 RID: 7768
				HttpHeaderTrailer,
				// Token: 0x04001E59 RID: 7769
				HttpHeaderTransferEncoding,
				// Token: 0x04001E5A RID: 7770
				HttpHeaderUpgrade,
				// Token: 0x04001E5B RID: 7771
				HttpHeaderVia,
				// Token: 0x04001E5C RID: 7772
				HttpHeaderWarning,
				// Token: 0x04001E5D RID: 7773
				HttpHeaderAllow,
				// Token: 0x04001E5E RID: 7774
				HttpHeaderContentLength,
				// Token: 0x04001E5F RID: 7775
				HttpHeaderContentType,
				// Token: 0x04001E60 RID: 7776
				HttpHeaderContentEncoding,
				// Token: 0x04001E61 RID: 7777
				HttpHeaderContentLanguage,
				// Token: 0x04001E62 RID: 7778
				HttpHeaderContentLocation,
				// Token: 0x04001E63 RID: 7779
				HttpHeaderContentMd5,
				// Token: 0x04001E64 RID: 7780
				HttpHeaderContentRange,
				// Token: 0x04001E65 RID: 7781
				HttpHeaderExpires,
				// Token: 0x04001E66 RID: 7782
				HttpHeaderLastModified,
				// Token: 0x04001E67 RID: 7783
				HttpHeaderAcceptRanges,
				// Token: 0x04001E68 RID: 7784
				HttpHeaderAge,
				// Token: 0x04001E69 RID: 7785
				HttpHeaderEtag,
				// Token: 0x04001E6A RID: 7786
				HttpHeaderLocation,
				// Token: 0x04001E6B RID: 7787
				HttpHeaderProxyAuthenticate,
				// Token: 0x04001E6C RID: 7788
				HttpHeaderRetryAfter,
				// Token: 0x04001E6D RID: 7789
				HttpHeaderServer,
				// Token: 0x04001E6E RID: 7790
				HttpHeaderSetCookie,
				// Token: 0x04001E6F RID: 7791
				HttpHeaderVary,
				// Token: 0x04001E70 RID: 7792
				HttpHeaderWwwAuthenticate,
				// Token: 0x04001E71 RID: 7793
				HttpHeaderResponseMaximum,
				// Token: 0x04001E72 RID: 7794
				HttpHeaderMaximum = 41
			}
		}

		// Token: 0x02000667 RID: 1639
		internal static class SecureStringHelper
		{
			// Token: 0x060033B3 RID: 13235 RVA: 0x000B43B8 File Offset: 0x000B25B8
			internal static string CreateString(SecureString secureString)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (secureString == null || secureString.Length == 0)
				{
					return string.Empty;
				}
				string text;
				try
				{
					intPtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
					text = Marshal.PtrToStringUni(intPtr);
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
					}
				}
				return text;
			}

			// Token: 0x060033B4 RID: 13236 RVA: 0x000B4414 File Offset: 0x000B2614
			internal unsafe static SecureString CreateSecureString(string plainString)
			{
				if (plainString == null || plainString.Length == 0)
				{
					return new SecureString();
				}
				SecureString secureString;
				fixed (string text = plainString)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					secureString = new SecureString(ptr, plainString.Length);
				}
				return secureString;
			}
		}
	}
}
