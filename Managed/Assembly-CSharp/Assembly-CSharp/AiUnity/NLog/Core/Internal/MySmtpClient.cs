using System;
using System.Net;
using System.Net.Mail;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x02000269 RID: 617
	internal class MySmtpClient : SmtpClient, ISmtpClient, IDisposable
	{
		// Token: 0x06001643 RID: 5699 RVA: 0x000780BF File Offset: 0x000762BF
		public new void Dispose()
		{
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000780C9 File Offset: 0x000762C9
		string ISmtpClient.get_Host()
		{
			return base.Host;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000780D1 File Offset: 0x000762D1
		void ISmtpClient.set_Host(string value)
		{
			base.Host = value;
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x000780DA File Offset: 0x000762DA
		int ISmtpClient.get_Port()
		{
			return base.Port;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x000780E2 File Offset: 0x000762E2
		void ISmtpClient.set_Port(int value)
		{
			base.Port = value;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x000780EB File Offset: 0x000762EB
		int ISmtpClient.get_Timeout()
		{
			return base.Timeout;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x000780F3 File Offset: 0x000762F3
		void ISmtpClient.set_Timeout(int value)
		{
			base.Timeout = value;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x000780FC File Offset: 0x000762FC
		ICredentialsByHost ISmtpClient.get_Credentials()
		{
			return base.Credentials;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00078104 File Offset: 0x00076304
		void ISmtpClient.set_Credentials(ICredentialsByHost value)
		{
			base.Credentials = value;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0007810D File Offset: 0x0007630D
		bool ISmtpClient.get_EnableSsl()
		{
			return base.EnableSsl;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00078115 File Offset: 0x00076315
		void ISmtpClient.set_EnableSsl(bool value)
		{
			base.EnableSsl = value;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0007811E File Offset: 0x0007631E
		void ISmtpClient.Send(MailMessage msg)
		{
			base.Send(msg);
		}
	}
}
