using System;
using System.Collections.Generic;
using System.Text;

namespace PlayFab
{
	// Token: 0x0200001B RID: 27
	public class PlayFabError
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0000F781 File Offset: 0x0000D981
		public override string ToString()
		{
			return this.GenerateErrorReport();
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000F78C File Offset: 0x0000D98C
		public string GenerateErrorReport()
		{
			if (PlayFabError._tempSb == null)
			{
				PlayFabError._tempSb = new StringBuilder();
			}
			PlayFabError._tempSb.Length = 0;
			if (string.IsNullOrEmpty(this.ErrorMessage))
			{
				PlayFabError._tempSb.Append(this.ApiEndpoint).Append(": ").Append("Http Code: ")
					.Append(this.HttpCode.ToString())
					.Append("\nHttp Status: ")
					.Append(this.HttpStatus)
					.Append("\nError: ")
					.Append(this.Error.ToString())
					.Append("\n");
			}
			else
			{
				PlayFabError._tempSb.Append(this.ApiEndpoint).Append(": ").Append(this.ErrorMessage);
			}
			if (this.ErrorDetails != null)
			{
				foreach (KeyValuePair<string, List<string>> keyValuePair in this.ErrorDetails)
				{
					foreach (string text in keyValuePair.Value)
					{
						PlayFabError._tempSb.Append("\n").Append(keyValuePair.Key).Append(": ")
							.Append(text);
					}
				}
			}
			return PlayFabError._tempSb.ToString();
		}

		// Token: 0x04000274 RID: 628
		public string ApiEndpoint;

		// Token: 0x04000275 RID: 629
		public int HttpCode;

		// Token: 0x04000276 RID: 630
		public string HttpStatus;

		// Token: 0x04000277 RID: 631
		public PlayFabErrorCode Error;

		// Token: 0x04000278 RID: 632
		public string ErrorMessage;

		// Token: 0x04000279 RID: 633
		public Dictionary<string, List<string>> ErrorDetails;

		// Token: 0x0400027A RID: 634
		public object CustomData;

		// Token: 0x0400027B RID: 635
		[ThreadStatic]
		private static StringBuilder _tempSb;
	}
}
