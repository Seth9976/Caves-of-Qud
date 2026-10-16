using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x02000649 RID: 1609
	internal abstract class WebProxyDataBuilder
	{
		// Token: 0x0600327E RID: 12926 RVA: 0x000AEAFA File Offset: 0x000ACCFA
		public WebProxyData Build()
		{
			this.m_Result = new WebProxyData();
			this.BuildInternal();
			return this.m_Result;
		}

		// Token: 0x0600327F RID: 12927
		protected abstract void BuildInternal();

		// Token: 0x06003280 RID: 12928 RVA: 0x000AEB14 File Offset: 0x000ACD14
		protected void SetProxyAndBypassList(string addressString, string bypassListString)
		{
			if (addressString != null)
			{
				addressString = addressString.Trim();
				if (addressString != string.Empty)
				{
					if (addressString.IndexOf('=') == -1)
					{
						this.m_Result.proxyAddress = WebProxyDataBuilder.ParseProxyUri(addressString);
					}
					else
					{
						this.m_Result.proxyHostAddresses = WebProxyDataBuilder.ParseProtocolProxies(addressString);
					}
					if (bypassListString != null)
					{
						bypassListString = bypassListString.Trim();
						if (bypassListString != string.Empty)
						{
							bool flag = false;
							this.m_Result.bypassList = WebProxyDataBuilder.ParseBypassList(bypassListString, out flag);
							this.m_Result.bypassOnLocal = flag;
						}
					}
				}
			}
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x000AEBA4 File Offset: 0x000ACDA4
		protected void SetAutoProxyUrl(string autoConfigUrl)
		{
			if (!string.IsNullOrEmpty(autoConfigUrl))
			{
				Uri uri = null;
				if (Uri.TryCreate(autoConfigUrl, UriKind.Absolute, out uri))
				{
					this.m_Result.scriptLocation = uri;
				}
			}
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000AEBD2 File Offset: 0x000ACDD2
		protected void SetAutoDetectSettings(bool value)
		{
			this.m_Result.automaticallyDetectSettings = value;
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000AEBE0 File Offset: 0x000ACDE0
		private static Uri ParseProxyUri(string proxyString)
		{
			if (proxyString.IndexOf("://") == -1)
			{
				proxyString = "http://" + proxyString;
			}
			Uri uri;
			try
			{
				uri = new Uri(proxyString);
			}
			catch (UriFormatException)
			{
				bool on = Logging.On;
				throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyString);
			}
			return uri;
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000AEC34 File Offset: 0x000ACE34
		private static Hashtable ParseProtocolProxies(string proxyListString)
		{
			string[] array = proxyListString.Split(';', StringSplitOptions.None);
			Hashtable hashtable = new Hashtable(CaseInsensitiveAscii.StaticInstance);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (!(text == string.Empty))
				{
					string[] array2 = text.Split('=', StringSplitOptions.None);
					if (array2.Length != 2)
					{
						throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyListString);
					}
					array2[0] = array2[0].Trim();
					array2[1] = array2[1].Trim();
					if (array2[0] == string.Empty || array2[1] == string.Empty)
					{
						throw WebProxyDataBuilder.CreateInvalidProxyStringException(proxyListString);
					}
					hashtable[array2[0]] = WebProxyDataBuilder.ParseProxyUri(array2[1]);
				}
			}
			return hashtable;
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000AECEE File Offset: 0x000ACEEE
		private static FormatException CreateInvalidProxyStringException(string originalProxyString)
		{
			string @string = SR.GetString("The system proxy settings contain an invalid proxy server setting: '{0}'.", new object[] { originalProxyString });
			bool on = Logging.On;
			return new FormatException(@string);
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000AED10 File Offset: 0x000ACF10
		private static string BypassStringEscape(string rawString)
		{
			Match match = new Regex("^(?<scheme>.*://)?(?<host>[^:]*)(?<port>:[0-9]{1,5})?$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).Match(rawString);
			string text;
			string text2;
			string text3;
			if (match.Success)
			{
				text = match.Groups["scheme"].Value;
				text2 = match.Groups["host"].Value;
				text3 = match.Groups["port"].Value;
			}
			else
			{
				text = string.Empty;
				text2 = rawString;
				text3 = string.Empty;
			}
			text = WebProxyDataBuilder.ConvertRegexReservedChars(text);
			text2 = WebProxyDataBuilder.ConvertRegexReservedChars(text2);
			text3 = WebProxyDataBuilder.ConvertRegexReservedChars(text3);
			if (text == string.Empty)
			{
				text = "(?:.*://)?";
			}
			if (text3 == string.Empty)
			{
				text3 = "(?::[0-9]{1,5})?";
			}
			return string.Concat(new string[] { "^", text, text2, text3, "$" });
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000AEDF0 File Offset: 0x000ACFF0
		private static string ConvertRegexReservedChars(string rawString)
		{
			if (rawString.Length == 0)
			{
				return rawString;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in rawString)
			{
				if ("#$()+.?[\\^{|".IndexOf(c) != -1)
				{
					stringBuilder.Append('\\');
				}
				else if (c == '*')
				{
					stringBuilder.Append('.');
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000AEE60 File Offset: 0x000AD060
		private static ArrayList ParseBypassList(string bypassListString, out bool bypassOnLocal)
		{
			string[] array = bypassListString.Split(';', StringSplitOptions.None);
			bypassOnLocal = false;
			if (array.Length == 0)
			{
				return null;
			}
			ArrayList arrayList = null;
			foreach (string text in array)
			{
				if (text != null)
				{
					string text2 = text.Trim();
					if (text2.Length > 0)
					{
						if (string.Compare(text2, "<local>", StringComparison.OrdinalIgnoreCase) == 0)
						{
							bypassOnLocal = true;
						}
						else
						{
							text2 = WebProxyDataBuilder.BypassStringEscape(text2);
							if (arrayList == null)
							{
								arrayList = new ArrayList();
							}
							if (!arrayList.Contains(text2))
							{
								arrayList.Add(text2);
							}
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x04001DA7 RID: 7591
		private const char addressListDelimiter = ';';

		// Token: 0x04001DA8 RID: 7592
		private const char addressListSchemeValueDelimiter = '=';

		// Token: 0x04001DA9 RID: 7593
		private const char bypassListDelimiter = ';';

		// Token: 0x04001DAA RID: 7594
		private WebProxyData m_Result;

		// Token: 0x04001DAB RID: 7595
		private const string regexReserved = "#$()+.?[\\^{|";
	}
}
