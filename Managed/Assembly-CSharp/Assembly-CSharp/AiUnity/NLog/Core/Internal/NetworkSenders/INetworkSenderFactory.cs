using System;

namespace AiUnity.NLog.Core.Internal.NetworkSenders
{
	// Token: 0x02000275 RID: 629
	internal interface INetworkSenderFactory
	{
		// Token: 0x0600168A RID: 5770
		NetworkSender Create(string url, int maxQueueSize);
	}
}
