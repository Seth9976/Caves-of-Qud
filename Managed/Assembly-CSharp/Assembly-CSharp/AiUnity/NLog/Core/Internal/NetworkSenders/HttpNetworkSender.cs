using System;
using System.IO;
using System.Net;
using AiUnity.NLog.Core.Common;

namespace AiUnity.NLog.Core.Internal.NetworkSenders
{
	// Token: 0x02000274 RID: 628
	internal class HttpNetworkSender : NetworkSender
	{
		// Token: 0x06001688 RID: 5768 RVA: 0x00078FEF File Offset: 0x000771EF
		public HttpNetworkSender(string url)
			: base(url)
		{
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00078FF8 File Offset: 0x000771F8
		protected override void DoSend(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation)
		{
			WebRequest webRequest = WebRequest.Create(new Uri(base.Address));
			webRequest.Method = "POST";
			AsyncCallback onResponse = delegate(IAsyncResult r)
			{
				try
				{
					using (webRequest.EndGetResponse(r))
					{
					}
					asyncContinuation(null);
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrown())
					{
						throw;
					}
					asyncContinuation(ex);
				}
			};
			AsyncCallback asyncCallback = delegate(IAsyncResult r)
			{
				try
				{
					using (Stream stream = webRequest.EndGetRequestStream(r))
					{
						stream.Write(bytes, offset, length);
					}
					webRequest.BeginGetResponse(onResponse, null);
				}
				catch (Exception ex2)
				{
					if (ex2.MustBeRethrown())
					{
						throw;
					}
					asyncContinuation(ex2);
				}
			};
			webRequest.BeginGetRequestStream(asyncCallback, null);
		}
	}
}
