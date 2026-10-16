using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;

namespace System.Net
{
	// Token: 0x020006C1 RID: 1729
	internal class WebConnection : IDisposable
	{
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06003777 RID: 14199 RVA: 0x000C2903 File Offset: 0x000C0B03
		public ServicePoint ServicePoint { get; }

		// Token: 0x06003778 RID: 14200 RVA: 0x000C290B File Offset: 0x000C0B0B
		public WebConnection(ServicePoint sPoint)
		{
			this.ServicePoint = sPoint;
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("MONO_WEB_DEBUG")]
		internal static void Debug(string message, params object[] args)
		{
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("MONO_WEB_DEBUG")]
		internal static void Debug(string message)
		{
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000C291A File Offset: 0x000C0B1A
		private bool CanReuse()
		{
			return !this.socket.Poll(0, SelectMode.SelectRead);
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000C292C File Offset: 0x000C0B2C
		private bool CheckReusable()
		{
			if (this.socket != null && this.socket.Connected)
			{
				try
				{
					if (this.CanReuse())
					{
						return true;
					}
				}
				catch
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x000C2974 File Offset: 0x000C0B74
		private async Task Connect(WebOperation operation, CancellationToken cancellationToken)
		{
			IPHostEntry hostEntry = this.ServicePoint.HostEntry;
			if (hostEntry == null || hostEntry.AddressList.Length == 0)
			{
				throw WebConnection.GetException(this.ServicePoint.UsesProxy ? WebExceptionStatus.ProxyNameResolutionFailure : WebExceptionStatus.NameResolutionFailure, null);
			}
			Exception connectException = null;
			foreach (IPAddress ipaddress in hostEntry.AddressList)
			{
				operation.ThrowIfDisposed(cancellationToken);
				try
				{
					this.socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				}
				catch (Exception ex)
				{
					throw WebConnection.GetException(WebExceptionStatus.ConnectFailure, ex);
				}
				IPEndPoint ipendPoint = new IPEndPoint(ipaddress, this.ServicePoint.Address.Port);
				this.socket.NoDelay = !this.ServicePoint.UseNagleAlgorithm;
				try
				{
					this.ServicePoint.KeepAliveSetup(this.socket);
				}
				catch
				{
				}
				if (!this.ServicePoint.CallEndPointDelegate(this.socket, ipendPoint))
				{
					Socket socket = Interlocked.Exchange<Socket>(ref this.socket, null);
					if (socket != null)
					{
						socket.Close();
					}
				}
				else
				{
					try
					{
						operation.ThrowIfDisposed(cancellationToken);
						await Task.Factory.FromAsync<IPEndPoint>((IPEndPoint targetEndPoint, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetEndPoint, callback, state), delegate(IAsyncResult asyncResult)
						{
							((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
						}, ipendPoint, this.socket).ConfigureAwait(false);
					}
					catch (ObjectDisposedException)
					{
						throw;
					}
					catch (Exception ex2)
					{
						Socket socket2 = Interlocked.Exchange<Socket>(ref this.socket, null);
						if (socket2 != null)
						{
							socket2.Close();
						}
						connectException = WebConnection.GetException(WebExceptionStatus.ConnectFailure, ex2);
						goto IL_0220;
					}
					if (this.socket != null)
					{
						return;
					}
				}
				IL_0220:;
			}
			IPAddress[] array = null;
			if (connectException == null)
			{
				connectException = WebConnection.GetException(WebExceptionStatus.ConnectFailure, null);
			}
			throw connectException;
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000C29C8 File Offset: 0x000C0BC8
		private async Task<bool> CreateStream(WebOperation operation, bool reused, CancellationToken cancellationToken)
		{
			bool flag;
			try
			{
				NetworkStream stream = new NetworkStream(this.socket, false);
				if (operation.Request.Address.Scheme == Uri.UriSchemeHttps)
				{
					if (!reused || this.monoTlsStream == null)
					{
						if (this.ServicePoint.UseConnect)
						{
							if (this.tunnel == null)
							{
								this.tunnel = new WebConnectionTunnel(operation.Request, this.ServicePoint.Address);
							}
							await this.tunnel.Initialize(stream, cancellationToken).ConfigureAwait(false);
							if (!this.tunnel.Success)
							{
								return false;
							}
						}
						this.monoTlsStream = new MonoTlsStream(operation.Request, stream);
						this.networkStream = await this.monoTlsStream.CreateStream(this.tunnel, cancellationToken).ConfigureAwait(false);
					}
					flag = true;
				}
				else
				{
					this.networkStream = stream;
					flag = true;
				}
			}
			catch (Exception ex)
			{
				ex = HttpWebRequest.FlattenException(ex);
				if (operation.Aborted || this.monoTlsStream == null)
				{
					throw WebConnection.GetException(WebExceptionStatus.ConnectFailure, ex);
				}
				throw WebConnection.GetException(this.monoTlsStream.ExceptionStatus, ex);
			}
			finally
			{
			}
			return flag;
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000C2A24 File Offset: 0x000C0C24
		internal async Task<WebRequestStream> InitConnection(WebOperation operation, CancellationToken cancellationToken)
		{
			bool flag = true;
			for (;;)
			{
				operation.ThrowIfClosedOrDisposed(cancellationToken);
				bool reused = this.CheckReusable();
				if (!reused)
				{
					this.CloseSocket();
					if (flag)
					{
						this.Reset();
					}
					try
					{
						await this.Connect(operation, cancellationToken).ConfigureAwait(false);
					}
					catch (Exception)
					{
						throw;
					}
				}
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.CreateStream(operation, reused, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					goto IL_0180;
				}
				WebConnectionTunnel webConnectionTunnel = this.tunnel;
				if (((webConnectionTunnel != null) ? webConnectionTunnel.Challenge : null) == null)
				{
					break;
				}
				if (this.tunnel.CloseConnection)
				{
					this.CloseSocket();
				}
				flag = false;
			}
			throw WebConnection.GetException(WebExceptionStatus.ProtocolError, null);
			IL_0180:
			this.networkStream.ReadTimeout = operation.Request.ReadWriteTimeout;
			return new WebRequestStream(this, operation, this.networkStream, this.tunnel);
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000C2A78 File Offset: 0x000C0C78
		internal static WebException GetException(WebExceptionStatus status, Exception error)
		{
			if (error == null)
			{
				return new WebException(string.Format("Error: {0}", status), status);
			}
			WebException ex = error as WebException;
			if (ex != null)
			{
				return ex;
			}
			return new WebException(string.Format("Error: {0} ({1})", status, error.Message), status, WebExceptionInternalStatus.RequestFatal, error);
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000C2ACC File Offset: 0x000C0CCC
		internal static bool ReadLine(byte[] buffer, ref int start, int max, ref string output)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (start < max)
			{
				int num2 = start;
				start = num2 + 1;
				num = (int)buffer[num2];
				if (num == 10)
				{
					if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\r')
					{
						StringBuilder stringBuilder2 = stringBuilder;
						num2 = stringBuilder2.Length;
						stringBuilder2.Length = num2 - 1;
					}
					flag = false;
					break;
				}
				if (flag)
				{
					StringBuilder stringBuilder3 = stringBuilder;
					num2 = stringBuilder3.Length;
					stringBuilder3.Length = num2 - 1;
					break;
				}
				if (num == 13)
				{
					flag = true;
				}
				stringBuilder.Append((char)num);
			}
			if (num != 10 && num != 13)
			{
				return false;
			}
			if (stringBuilder.Length == 0)
			{
				output = null;
				return num == 10 || num == 13;
			}
			if (flag)
			{
				StringBuilder stringBuilder4 = stringBuilder;
				int num2 = stringBuilder4.Length;
				stringBuilder4.Length = num2 - 1;
			}
			output = stringBuilder.ToString();
			return true;
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000C2B90 File Offset: 0x000C0D90
		internal bool CanReuseConnection(WebOperation operation)
		{
			bool flag2;
			lock (this)
			{
				if (this.Closed || this.currentOperation != null)
				{
					flag2 = false;
				}
				else if (!this.NtlmAuthenticated)
				{
					flag2 = true;
				}
				else
				{
					NetworkCredential ntlmCredential = this.NtlmCredential;
					HttpWebRequest request = operation.Request;
					ICredentials credentials = ((request.Proxy == null || request.Proxy.IsBypassed(request.RequestUri)) ? request.Credentials : request.Proxy.Credentials);
					NetworkCredential networkCredential = ((credentials != null) ? credentials.GetCredential(request.RequestUri, "NTLM") : null);
					if (ntlmCredential == null || networkCredential == null || ntlmCredential.Domain != networkCredential.Domain || ntlmCredential.UserName != networkCredential.UserName || ntlmCredential.Password != networkCredential.Password)
					{
						flag2 = false;
					}
					else
					{
						bool unsafeAuthenticatedConnectionSharing = request.UnsafeAuthenticatedConnectionSharing;
						bool unsafeAuthenticatedConnectionSharing2 = this.UnsafeAuthenticatedConnectionSharing;
						flag2 = unsafeAuthenticatedConnectionSharing && unsafeAuthenticatedConnectionSharing == unsafeAuthenticatedConnectionSharing2;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000C2CB8 File Offset: 0x000C0EB8
		private bool PrepareSharingNtlm(WebOperation operation)
		{
			if (operation == null || !this.NtlmAuthenticated)
			{
				return true;
			}
			bool flag = false;
			NetworkCredential ntlmCredential = this.NtlmCredential;
			HttpWebRequest request = operation.Request;
			ICredentials credentials = ((request.Proxy == null || request.Proxy.IsBypassed(request.RequestUri)) ? request.Credentials : request.Proxy.Credentials);
			NetworkCredential networkCredential = ((credentials != null) ? credentials.GetCredential(request.RequestUri, "NTLM") : null);
			if (ntlmCredential == null || networkCredential == null || ntlmCredential.Domain != networkCredential.Domain || ntlmCredential.UserName != networkCredential.UserName || ntlmCredential.Password != networkCredential.Password)
			{
				flag = true;
			}
			if (!flag)
			{
				bool unsafeAuthenticatedConnectionSharing = request.UnsafeAuthenticatedConnectionSharing;
				bool unsafeAuthenticatedConnectionSharing2 = this.UnsafeAuthenticatedConnectionSharing;
				flag = !unsafeAuthenticatedConnectionSharing || unsafeAuthenticatedConnectionSharing != unsafeAuthenticatedConnectionSharing2;
			}
			return flag;
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000C2D9C File Offset: 0x000C0F9C
		private void Reset()
		{
			lock (this)
			{
				this.tunnel = null;
				this.ResetNtlm();
			}
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000C2DE0 File Offset: 0x000C0FE0
		private void Close(bool reset)
		{
			lock (this)
			{
				this.CloseSocket();
				if (reset)
				{
					this.Reset();
				}
			}
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000C2E24 File Offset: 0x000C1024
		private void CloseSocket()
		{
			lock (this)
			{
				if (this.networkStream != null)
				{
					try
					{
						this.networkStream.Dispose();
					}
					catch
					{
					}
					this.networkStream = null;
				}
				if (this.monoTlsStream != null)
				{
					try
					{
						this.monoTlsStream.Dispose();
					}
					catch
					{
					}
					this.monoTlsStream = null;
				}
				if (this.socket != null)
				{
					try
					{
						this.socket.Dispose();
					}
					catch
					{
					}
					this.socket = null;
				}
				this.monoTlsStream = null;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06003787 RID: 14215 RVA: 0x000C2EE4 File Offset: 0x000C10E4
		public bool Closed
		{
			get
			{
				return this.disposed != 0;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06003788 RID: 14216 RVA: 0x000C2EEF File Offset: 0x000C10EF
		public bool Busy
		{
			get
			{
				return this.currentOperation != null;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06003789 RID: 14217 RVA: 0x000C2EFA File Offset: 0x000C10FA
		public DateTime IdleSince
		{
			get
			{
				return this.idleSince;
			}
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000C2F04 File Offset: 0x000C1104
		public bool StartOperation(WebOperation operation, bool reused)
		{
			lock (this)
			{
				if (this.Closed)
				{
					return false;
				}
				if (Interlocked.CompareExchange<WebOperation>(ref this.currentOperation, operation, null) != null)
				{
					return false;
				}
				this.idleSince = DateTime.UtcNow + TimeSpan.FromDays(3650.0);
				if (reused && !this.PrepareSharingNtlm(operation))
				{
					this.Close(true);
				}
				operation.RegisterRequest(this.ServicePoint, this);
			}
			operation.Run();
			return true;
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000C2FA0 File Offset: 0x000C11A0
		public bool Continue(WebOperation next)
		{
			lock (this)
			{
				if (this.Closed)
				{
					return false;
				}
				if (this.socket == null || !this.socket.Connected || !this.PrepareSharingNtlm(next))
				{
					this.Close(true);
					return false;
				}
				this.currentOperation = next;
				if (next == null)
				{
					return true;
				}
				next.RegisterRequest(this.ServicePoint, this);
			}
			next.Run();
			return true;
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000C3030 File Offset: 0x000C1230
		private void Dispose(bool disposing)
		{
			if (Interlocked.CompareExchange(ref this.disposed, 1, 0) != 0)
			{
				return;
			}
			this.Close(true);
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000C3049 File Offset: 0x000C1249
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000C3052 File Offset: 0x000C1252
		private void ResetNtlm()
		{
			this.ntlm_authenticated = false;
			this.ntlm_credentials = null;
			this.unsafe_sharing = false;
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x0600378F RID: 14223 RVA: 0x000C3069 File Offset: 0x000C1269
		// (set) Token: 0x06003790 RID: 14224 RVA: 0x000C3071 File Offset: 0x000C1271
		internal bool NtlmAuthenticated
		{
			get
			{
				return this.ntlm_authenticated;
			}
			set
			{
				this.ntlm_authenticated = value;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06003791 RID: 14225 RVA: 0x000C307A File Offset: 0x000C127A
		// (set) Token: 0x06003792 RID: 14226 RVA: 0x000C3082 File Offset: 0x000C1282
		internal NetworkCredential NtlmCredential
		{
			get
			{
				return this.ntlm_credentials;
			}
			set
			{
				this.ntlm_credentials = value;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000C308B File Offset: 0x000C128B
		// (set) Token: 0x06003794 RID: 14228 RVA: 0x000C3093 File Offset: 0x000C1293
		internal bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafe_sharing;
			}
			set
			{
				this.unsafe_sharing = value;
			}
		}

		// Token: 0x04002073 RID: 8307
		private NetworkCredential ntlm_credentials;

		// Token: 0x04002074 RID: 8308
		private bool ntlm_authenticated;

		// Token: 0x04002075 RID: 8309
		private bool unsafe_sharing;

		// Token: 0x04002076 RID: 8310
		private Stream networkStream;

		// Token: 0x04002077 RID: 8311
		private Socket socket;

		// Token: 0x04002078 RID: 8312
		private MonoTlsStream monoTlsStream;

		// Token: 0x04002079 RID: 8313
		private WebConnectionTunnel tunnel;

		// Token: 0x0400207A RID: 8314
		private int disposed;

		// Token: 0x0400207C RID: 8316
		internal readonly int ID;

		// Token: 0x0400207D RID: 8317
		private DateTime idleSince;

		// Token: 0x0400207E RID: 8318
		private WebOperation currentOperation;
	}
}
