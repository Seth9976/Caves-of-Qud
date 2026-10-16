using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Configuration;
using System.Text;

namespace System.Net
{
	// Token: 0x020005D2 RID: 1490
	internal sealed class HttpListenerRequestUriBuilder
	{
		// Token: 0x0600301C RID: 12316 RVA: 0x000A5CCC File Offset: 0x000A3ECC
		private HttpListenerRequestUriBuilder(string rawUri, string cookedUriScheme, string cookedUriHost, string cookedUriPath, string cookedUriQuery)
		{
			this.rawUri = rawUri;
			this.cookedUriScheme = cookedUriScheme;
			this.cookedUriHost = cookedUriHost;
			this.cookedUriPath = HttpListenerRequestUriBuilder.AddSlashToAsteriskOnlyPath(cookedUriPath);
			if (cookedUriQuery == null)
			{
				this.cookedUriQuery = string.Empty;
				return;
			}
			this.cookedUriQuery = cookedUriQuery;
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000A5D19 File Offset: 0x000A3F19
		public static Uri GetRequestUri(string rawUri, string cookedUriScheme, string cookedUriHost, string cookedUriPath, string cookedUriQuery)
		{
			return new HttpListenerRequestUriBuilder(rawUri, cookedUriScheme, cookedUriHost, cookedUriPath, cookedUriQuery).Build();
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000A5D2C File Offset: 0x000A3F2C
		private Uri Build()
		{
			if (HttpListenerRequestUriBuilder.useCookedRequestUrl)
			{
				this.BuildRequestUriUsingCookedPath();
				if (this.requestUri == null)
				{
					this.BuildRequestUriUsingRawPath();
				}
			}
			else
			{
				this.BuildRequestUriUsingRawPath();
				if (this.requestUri == null)
				{
					this.BuildRequestUriUsingCookedPath();
				}
			}
			return this.requestUri;
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000A5D7C File Offset: 0x000A3F7C
		private void BuildRequestUriUsingCookedPath()
		{
			if (!Uri.TryCreate(string.Concat(new string[]
			{
				this.cookedUriScheme,
				Uri.SchemeDelimiter,
				this.cookedUriHost,
				this.cookedUriPath,
				this.cookedUriQuery
			}), UriKind.Absolute, out this.requestUri))
			{
				this.LogWarning("BuildRequestUriUsingCookedPath", "Can't create Uri from string '{0}://{1}{2}{3}'.", new object[] { this.cookedUriScheme, this.cookedUriHost, this.cookedUriPath, this.cookedUriQuery });
			}
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000A5E08 File Offset: 0x000A4008
		private void BuildRequestUriUsingRawPath()
		{
			this.rawPath = HttpListenerRequestUriBuilder.GetPath(this.rawUri);
			bool flag;
			if (this.rawPath == string.Empty)
			{
				string text = this.rawPath;
				if (text == string.Empty)
				{
					text = "/";
				}
				flag = Uri.TryCreate(string.Concat(new string[]
				{
					this.cookedUriScheme,
					Uri.SchemeDelimiter,
					this.cookedUriHost,
					text,
					this.cookedUriQuery
				}), UriKind.Absolute, out this.requestUri);
			}
			else
			{
				HttpListenerRequestUriBuilder.ParsingResult parsingResult = this.BuildRequestUriUsingRawPath(HttpListenerRequestUriBuilder.GetEncoding(HttpListenerRequestUriBuilder.EncodingType.Primary));
				if (parsingResult == HttpListenerRequestUriBuilder.ParsingResult.EncodingError)
				{
					Encoding encoding = HttpListenerRequestUriBuilder.GetEncoding(HttpListenerRequestUriBuilder.EncodingType.Secondary);
					parsingResult = this.BuildRequestUriUsingRawPath(encoding);
				}
				flag = parsingResult == HttpListenerRequestUriBuilder.ParsingResult.Success;
			}
			if (!flag)
			{
				this.LogWarning("BuildRequestUriUsingRawPath", "Can't create Uri from string '{0}://{1}{2}{3}'.", new object[] { this.cookedUriScheme, this.cookedUriHost, this.rawPath, this.cookedUriQuery });
			}
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000A5EFA File Offset: 0x000A40FA
		private static Encoding GetEncoding(HttpListenerRequestUriBuilder.EncodingType type)
		{
			if (type == HttpListenerRequestUriBuilder.EncodingType.Secondary)
			{
				return HttpListenerRequestUriBuilder.ansiEncoding;
			}
			return HttpListenerRequestUriBuilder.utf8Encoding;
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000A5F10 File Offset: 0x000A4110
		private HttpListenerRequestUriBuilder.ParsingResult BuildRequestUriUsingRawPath(Encoding encoding)
		{
			this.rawOctets = new List<byte>();
			this.requestUriString = new StringBuilder();
			this.requestUriString.Append(this.cookedUriScheme);
			this.requestUriString.Append(Uri.SchemeDelimiter);
			this.requestUriString.Append(this.cookedUriHost);
			HttpListenerRequestUriBuilder.ParsingResult parsingResult = this.ParseRawPath(encoding);
			if (parsingResult == HttpListenerRequestUriBuilder.ParsingResult.Success)
			{
				this.requestUriString.Append(this.cookedUriQuery);
				if (!Uri.TryCreate(this.requestUriString.ToString(), UriKind.Absolute, out this.requestUri))
				{
					parsingResult = HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
				}
			}
			if (parsingResult != HttpListenerRequestUriBuilder.ParsingResult.Success)
			{
				this.LogWarning("BuildRequestUriUsingRawPath", "Can't convert Uri path '{0}' using encoding '{1}'.", new object[] { this.rawPath, encoding.EncodingName });
			}
			return parsingResult;
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000A5FCC File Offset: 0x000A41CC
		private HttpListenerRequestUriBuilder.ParsingResult ParseRawPath(Encoding encoding)
		{
			int i = 0;
			while (i < this.rawPath.Length)
			{
				char c = this.rawPath[i];
				if (c == '%')
				{
					i++;
					c = this.rawPath[i];
					if (c == 'u' || c == 'U')
					{
						if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
						}
						if (!this.AppendUnicodeCodePointValuePercentEncoded(this.rawPath.Substring(i + 1, 4)))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
						}
						i += 5;
					}
					else
					{
						if (!this.AddPercentEncodedOctetToRawOctetsList(encoding, this.rawPath.Substring(i, 2)))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
						}
						i += 2;
					}
				}
				else
				{
					if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
					{
						return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
					}
					this.requestUriString.Append(c);
					i++;
				}
			}
			if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
			{
				return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
			}
			return HttpListenerRequestUriBuilder.ParsingResult.Success;
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000A6090 File Offset: 0x000A4290
		private bool AppendUnicodeCodePointValuePercentEncoded(string codePoint)
		{
			int num;
			if (!int.TryParse(codePoint, NumberStyles.HexNumber, null, out num))
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "Can't convert percent encoded value '{0}'.", new object[] { codePoint });
				return false;
			}
			string text = null;
			try
			{
				text = char.ConvertFromUtf32(num);
				HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, HttpListenerRequestUriBuilder.utf8Encoding.GetBytes(text));
				return true;
			}
			catch (ArgumentOutOfRangeException)
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "Can't convert percent encoded value '{0}'.", new object[] { codePoint });
			}
			catch (EncoderFallbackException ex)
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "Can't convert string '{0}' into UTF-8 bytes: {1}", new object[] { text, ex.Message });
			}
			return false;
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000A6150 File Offset: 0x000A4350
		private bool AddPercentEncodedOctetToRawOctetsList(Encoding encoding, string escapedCharacter)
		{
			byte b;
			if (!byte.TryParse(escapedCharacter, NumberStyles.HexNumber, null, out b))
			{
				this.LogWarning("AddPercentEncodedOctetToRawOctetsList", "Can't convert percent encoded value '{0}'.", new object[] { escapedCharacter });
				return false;
			}
			this.rawOctets.Add(b);
			return true;
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000A6198 File Offset: 0x000A4398
		private bool EmptyDecodeAndAppendRawOctetsList(Encoding encoding)
		{
			if (this.rawOctets.Count == 0)
			{
				return true;
			}
			string text = null;
			try
			{
				text = encoding.GetString(this.rawOctets.ToArray());
				if (encoding == HttpListenerRequestUriBuilder.utf8Encoding)
				{
					HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, this.rawOctets.ToArray());
				}
				else
				{
					HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, HttpListenerRequestUriBuilder.utf8Encoding.GetBytes(text));
				}
				this.rawOctets.Clear();
				return true;
			}
			catch (DecoderFallbackException ex)
			{
				this.LogWarning("EmptyDecodeAndAppendRawOctetsList", "Can't convert bytes '{0}' into UTF-16 characters: {1}", new object[]
				{
					HttpListenerRequestUriBuilder.GetOctetsAsString(this.rawOctets),
					ex.Message
				});
			}
			catch (EncoderFallbackException ex2)
			{
				this.LogWarning("EmptyDecodeAndAppendRawOctetsList", "Can't convert string '{0}' into UTF-8 bytes: {1}", new object[] { text, ex2.Message });
			}
			return false;
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000A6284 File Offset: 0x000A4484
		private static void AppendOctetsPercentEncoded(StringBuilder target, IEnumerable<byte> octets)
		{
			foreach (byte b in octets)
			{
				target.Append('%');
				target.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000A62E8 File Offset: 0x000A44E8
		private static string GetOctetsAsString(IEnumerable<byte> octets)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (byte b in octets)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000A6364 File Offset: 0x000A4564
		private static string GetPath(string uriString)
		{
			int num = 0;
			if (uriString[0] != '/')
			{
				int num2 = 0;
				if (uriString.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
				{
					num2 = 7;
				}
				else if (uriString.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
				{
					num2 = 8;
				}
				if (num2 > 0)
				{
					num = uriString.IndexOf('/', num2);
					if (num == -1)
					{
						num = uriString.Length;
					}
				}
				else
				{
					uriString = "/" + uriString;
				}
			}
			int num3 = uriString.IndexOf('?');
			if (num3 == -1)
			{
				num3 = uriString.Length;
			}
			return HttpListenerRequestUriBuilder.AddSlashToAsteriskOnlyPath(uriString.Substring(num, num3 - num));
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000A63ED File Offset: 0x000A45ED
		private static string AddSlashToAsteriskOnlyPath(string path)
		{
			if (path.Length == 1 && path[0] == '*')
			{
				return "/*";
			}
			return path;
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000A640A File Offset: 0x000A460A
		private void LogWarning(string methodName, string message, params object[] args)
		{
			bool on = Logging.On;
		}

		// Token: 0x04001ABF RID: 6847
		private static readonly bool useCookedRequestUrl = SettingsSectionInternal.Section.HttpListenerUnescapeRequestUrl;

		// Token: 0x04001AC0 RID: 6848
		private static readonly Encoding utf8Encoding = new UTF8Encoding(false, true);

		// Token: 0x04001AC1 RID: 6849
		private static readonly Encoding ansiEncoding = Encoding.GetEncoding(0, new EncoderExceptionFallback(), new DecoderExceptionFallback());

		// Token: 0x04001AC2 RID: 6850
		private readonly string rawUri;

		// Token: 0x04001AC3 RID: 6851
		private readonly string cookedUriScheme;

		// Token: 0x04001AC4 RID: 6852
		private readonly string cookedUriHost;

		// Token: 0x04001AC5 RID: 6853
		private readonly string cookedUriPath;

		// Token: 0x04001AC6 RID: 6854
		private readonly string cookedUriQuery;

		// Token: 0x04001AC7 RID: 6855
		private StringBuilder requestUriString;

		// Token: 0x04001AC8 RID: 6856
		private List<byte> rawOctets;

		// Token: 0x04001AC9 RID: 6857
		private string rawPath;

		// Token: 0x04001ACA RID: 6858
		private Uri requestUri;

		// Token: 0x020005D3 RID: 1491
		private enum ParsingResult
		{
			// Token: 0x04001ACC RID: 6860
			Success,
			// Token: 0x04001ACD RID: 6861
			InvalidString,
			// Token: 0x04001ACE RID: 6862
			EncodingError
		}

		// Token: 0x020005D4 RID: 1492
		private enum EncodingType
		{
			// Token: 0x04001AD0 RID: 6864
			Primary,
			// Token: 0x04001AD1 RID: 6865
			Secondary
		}
	}
}
