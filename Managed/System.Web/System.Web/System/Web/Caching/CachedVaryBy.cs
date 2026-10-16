using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Web.Caching
{
	// Token: 0x02000686 RID: 1670
	[Serializable]
	internal sealed class CachedVaryBy
	{
		// Token: 0x06004736 RID: 18230 RVA: 0x000C6250 File Offset: 0x000C4450
		internal CachedVaryBy(HttpCachePolicy policy, string key)
		{
			this.prms = policy.VaryByParams.GetParamNames();
			this.headers = policy.VaryByHeaders.GetHeaderNames(policy.OmitVaryStar);
			this.custom = policy.GetVaryByCustom();
			this.key = key;
			this.item_list = new List<string>();
			this.wildCardParams = policy.VaryByParams["*"];
		}

		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x06004737 RID: 18231 RVA: 0x000C62BF File Offset: 0x000C44BF
		internal List<string> ItemList
		{
			get
			{
				return this.item_list;
			}
		}

		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x06004738 RID: 18232 RVA: 0x000C62C7 File Offset: 0x000C44C7
		internal string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x000C62D0 File Offset: 0x000C44D0
		internal string CreateKey(string file_path, HttpContext context)
		{
			if (string.IsNullOrEmpty(file_path))
			{
				throw new ArgumentNullException("file_path");
			}
			StringBuilder stringBuilder = new StringBuilder("vbk");
			HttpRequest httpRequest = ((context != null) ? context.Request : null);
			stringBuilder.Append(file_path);
			if (httpRequest == null)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append(httpRequest.HttpMethod);
			if (this.wildCardParams)
			{
				stringBuilder.Append("WQ");
				foreach (object obj in httpRequest.QueryString)
				{
					string text = (string)obj;
					if (text != null)
					{
						stringBuilder.Append('N');
						stringBuilder.Append(text.ToLowerInvariant());
						string text2 = httpRequest.QueryString[text];
						if (!string.IsNullOrEmpty(text2))
						{
							stringBuilder.Append('V');
							stringBuilder.Append(text2);
						}
					}
				}
				stringBuilder.Append('F');
				using (IEnumerator enumerator = httpRequest.Form.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						string text3 = (string)obj2;
						if (text3 != null)
						{
							stringBuilder.Append('N');
							stringBuilder.Append(text3.ToLowerInvariant());
							string text2 = httpRequest.Form[text3];
							if (!string.IsNullOrEmpty(text2))
							{
								stringBuilder.Append('V');
								stringBuilder.Append(text2);
							}
						}
					}
					goto IL_0256;
				}
			}
			if (this.prms != null)
			{
				StringBuilder stringBuilder2 = null;
				stringBuilder.Append("SQ");
				for (int i = 0; i < this.prms.Length; i++)
				{
					string text4 = this.prms[i];
					if (!string.IsNullOrEmpty(text4))
					{
						string text2 = httpRequest.QueryString[text4];
						if (text2 != null)
						{
							stringBuilder.Append('N');
							stringBuilder.Append(text4.ToLowerInvariant());
							if (text2.Length > 0)
							{
								stringBuilder.Append('V');
								stringBuilder.Append(text2);
							}
						}
						text2 = httpRequest.Form[text4];
						if (text2 != null)
						{
							if (stringBuilder2 == null)
							{
								stringBuilder2 = new StringBuilder(70);
							}
							stringBuilder.Append('N');
							stringBuilder.Append(text4.ToLowerInvariant());
							if (text2.Length > 0)
							{
								stringBuilder.Append('V');
								stringBuilder.Append(text2);
							}
						}
					}
				}
				if (stringBuilder2 != null)
				{
					stringBuilder.Append(stringBuilder2.ToString());
				}
			}
			IL_0256:
			if (this.headers != null)
			{
				stringBuilder.Append('H');
				for (int j = 0; j < this.headers.Length; j++)
				{
					stringBuilder.Append('N');
					string text4 = this.headers[j];
					stringBuilder.Append(text4.ToLowerInvariant());
					string text2 = httpRequest.Headers[text4];
					if (!string.IsNullOrEmpty(text2))
					{
						stringBuilder.Append('V');
						stringBuilder.Append(text2);
					}
				}
			}
			if (this.custom != null)
			{
				stringBuilder.Append('C');
				string varyByCustomString = context.ApplicationInstance.GetVaryByCustomString(context, this.custom);
				stringBuilder.Append('N');
				stringBuilder.Append(this.custom);
				stringBuilder.Append('V');
				stringBuilder.Append((varyByCustomString != null) ? varyByCustomString : "__null__");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040025A4 RID: 9636
		private string[] prms;

		// Token: 0x040025A5 RID: 9637
		private string[] headers;

		// Token: 0x040025A6 RID: 9638
		private string custom;

		// Token: 0x040025A7 RID: 9639
		private string key;

		// Token: 0x040025A8 RID: 9640
		private List<string> item_list;

		// Token: 0x040025A9 RID: 9641
		private bool wildCardParams;
	}
}
