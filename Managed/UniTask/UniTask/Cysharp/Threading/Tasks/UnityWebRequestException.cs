using System;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200006B RID: 107
	public class UnityWebRequestException : Exception
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000A774 File Offset: 0x00008974
		public UnityWebRequest UnityWebRequest { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000A77C File Offset: 0x0000897C
		public UnityWebRequest.Result Result { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000A784 File Offset: 0x00008984
		public string Error { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000A78C File Offset: 0x0000898C
		public string Text { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000A794 File Offset: 0x00008994
		public long ResponseCode { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000A79C File Offset: 0x0000899C
		public Dictionary<string, string> ResponseHeaders { get; }

		// Token: 0x060002DB RID: 731 RVA: 0x0000A7A4 File Offset: 0x000089A4
		public UnityWebRequestException(UnityWebRequest unityWebRequest)
		{
			this.UnityWebRequest = unityWebRequest;
			this.Result = unityWebRequest.result;
			this.Error = unityWebRequest.error;
			this.ResponseCode = unityWebRequest.responseCode;
			if (this.UnityWebRequest.downloadHandler != null)
			{
				DownloadHandlerBuffer downloadHandlerBuffer = unityWebRequest.downloadHandler as DownloadHandlerBuffer;
				if (downloadHandlerBuffer != null)
				{
					this.Text = downloadHandlerBuffer.text;
				}
			}
			this.ResponseHeaders = unityWebRequest.GetResponseHeaders();
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000A818 File Offset: 0x00008A18
		public override string Message
		{
			get
			{
				if (this.msg == null)
				{
					if (!string.IsNullOrWhiteSpace(this.Text))
					{
						this.msg = this.Error + Environment.NewLine + this.Text;
					}
					else
					{
						this.msg = this.Error;
					}
				}
				return this.msg;
			}
		}

		// Token: 0x040000EB RID: 235
		private string msg;
	}
}
