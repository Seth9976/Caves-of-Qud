using System;
using System.Net;
using System.Net.Mail;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x02000265 RID: 613
	internal interface ISmtpClient : IDisposable
	{
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001632 RID: 5682
		// (set) Token: 0x06001633 RID: 5683
		string Host { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001634 RID: 5684
		// (set) Token: 0x06001635 RID: 5685
		int Port { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001636 RID: 5686
		// (set) Token: 0x06001637 RID: 5687
		int Timeout { get; set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001638 RID: 5688
		// (set) Token: 0x06001639 RID: 5689
		ICredentialsByHost Credentials { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600163A RID: 5690
		// (set) Token: 0x0600163B RID: 5691
		bool EnableSsl { get; set; }

		// Token: 0x0600163C RID: 5692
		void Send(MailMessage msg);
	}
}
