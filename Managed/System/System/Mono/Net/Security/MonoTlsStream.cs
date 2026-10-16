using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security.Private;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x020000A9 RID: 169
	internal class MonoTlsStream : IDisposable
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000347 RID: 839 RVA: 0x000099A0 File Offset: 0x00007BA0
		internal HttpWebRequest Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000099A8 File Offset: 0x00007BA8
		internal SslStream SslStream
		{
			get
			{
				return this.sslStream;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000349 RID: 841 RVA: 0x000099B0 File Offset: 0x00007BB0
		internal WebExceptionStatus ExceptionStatus
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600034A RID: 842 RVA: 0x000099B8 File Offset: 0x00007BB8
		// (set) Token: 0x0600034B RID: 843 RVA: 0x000099C0 File Offset: 0x00007BC0
		internal bool CertificateValidationFailed { get; set; }

		// Token: 0x0600034C RID: 844 RVA: 0x000099CC File Offset: 0x00007BCC
		public MonoTlsStream(HttpWebRequest request, NetworkStream networkStream)
		{
			this.request = request;
			this.networkStream = networkStream;
			this.settings = request.TlsSettings;
			if (this.settings == null)
			{
				this.settings = MonoTlsSettings.CopyDefaultSettings();
			}
			if (this.settings.RemoteCertificateValidationCallback == null)
			{
				this.settings.RemoteCertificateValidationCallback = CallbackHelpers.PublicToMono(request.ServerCertificateValidationCallback);
			}
			this.provider = request.TlsProvider ?? MonoTlsProviderFactory.GetProviderInternal();
			this.status = WebExceptionStatus.SecureChannelFailure;
			ChainValidationHelper.Create(this.provider, ref this.settings, this);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00009A6C File Offset: 0x00007C6C
		internal async Task<Stream> CreateStream(WebConnectionTunnel tunnel, CancellationToken cancellationToken)
		{
			Socket socket = this.networkStream.InternalSocket;
			this.sslStream = new SslStream(this.networkStream, false, this.provider, this.settings);
			try
			{
				string text = this.request.Host;
				if (!string.IsNullOrEmpty(text))
				{
					int num = text.IndexOf(':');
					if (num > 0)
					{
						text = text.Substring(0, num);
					}
				}
				await this.sslStream.AuthenticateAsClientAsync(text, this.request.ClientCertificates, (SslProtocols)ServicePointManager.SecurityProtocol, ServicePointManager.CheckCertificateRevocationList).ConfigureAwait(false);
				this.status = WebExceptionStatus.Success;
				this.request.ServicePoint.UpdateClientCertificate(this.sslStream.LocalCertificate);
			}
			catch (Exception)
			{
				if (socket.CleanedUp)
				{
					this.status = WebExceptionStatus.RequestCanceled;
				}
				else if (this.CertificateValidationFailed)
				{
					this.status = WebExceptionStatus.TrustFailure;
				}
				else
				{
					this.status = WebExceptionStatus.SecureChannelFailure;
				}
				this.request.ServicePoint.UpdateClientCertificate(null);
				this.CloseSslStream();
				throw;
			}
			try
			{
				if (((tunnel != null) ? tunnel.Data : null) != null)
				{
					await this.sslStream.WriteAsync(tunnel.Data, 0, tunnel.Data.Length, cancellationToken).ConfigureAwait(false);
				}
			}
			catch
			{
				this.status = WebExceptionStatus.SendFailure;
				this.CloseSslStream();
				throw;
			}
			return this.sslStream;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00009ABF File Offset: 0x00007CBF
		public void Dispose()
		{
			this.CloseSslStream();
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00009AC8 File Offset: 0x00007CC8
		private void CloseSslStream()
		{
			object obj = this.sslStreamLock;
			lock (obj)
			{
				if (this.sslStream != null)
				{
					this.sslStream.Dispose();
					this.sslStream = null;
				}
			}
		}

		// Token: 0x040002A2 RID: 674
		private readonly MobileTlsProvider provider;

		// Token: 0x040002A3 RID: 675
		private readonly NetworkStream networkStream;

		// Token: 0x040002A4 RID: 676
		private readonly HttpWebRequest request;

		// Token: 0x040002A5 RID: 677
		private readonly MonoTlsSettings settings;

		// Token: 0x040002A6 RID: 678
		private SslStream sslStream;

		// Token: 0x040002A7 RID: 679
		private readonly object sslStreamLock = new object();

		// Token: 0x040002A8 RID: 680
		private WebExceptionStatus status;
	}
}
