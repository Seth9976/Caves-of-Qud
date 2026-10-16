using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	// Token: 0x02000572 RID: 1394
	internal class TlsStream : NetworkStream
	{
		// Token: 0x06002CFD RID: 11517 RVA: 0x0009A1BA File Offset: 0x000983BA
		public TlsStream(NetworkStream stream, Socket socket, string host, X509CertificateCollection clientCertificates)
			: base(socket)
		{
			this._sslStream = new SslStream(stream, false, ServicePointManager.ServerCertificateValidationCallback);
			this._host = host;
			this._clientCertificates = clientCertificates;
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x0009A1E4 File Offset: 0x000983E4
		public void AuthenticateAsClient()
		{
			this._sslStream.AuthenticateAsClient(this._host, this._clientCertificates, (SslProtocols)ServicePointManager.SecurityProtocol, ServicePointManager.CheckCertificateRevocationList);
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x0009A207 File Offset: 0x00098407
		public IAsyncResult BeginAuthenticateAsClient(AsyncCallback asyncCallback, object state)
		{
			return this._sslStream.BeginAuthenticateAsClient(this._host, this._clientCertificates, (SslProtocols)ServicePointManager.SecurityProtocol, ServicePointManager.CheckCertificateRevocationList, asyncCallback, state);
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x0009A22C File Offset: 0x0009842C
		public void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			this._sslStream.EndAuthenticateAsClient(asyncResult);
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x0009A23A File Offset: 0x0009843A
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this._sslStream.BeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x0009A24E File Offset: 0x0009844E
		public override void EndWrite(IAsyncResult result)
		{
			this._sslStream.EndWrite(result);
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x0009A25C File Offset: 0x0009845C
		public override void Write(byte[] buffer, int offset, int size)
		{
			this._sslStream.Write(buffer, offset, size);
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x0009A26C File Offset: 0x0009846C
		public override int Read(byte[] buffer, int offset, int size)
		{
			return this._sslStream.Read(buffer, offset, size);
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x0009A27C File Offset: 0x0009847C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this._sslStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x0009A290 File Offset: 0x00098490
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._sslStream.EndRead(asyncResult);
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x0009A29E File Offset: 0x0009849E
		public override void Close()
		{
			base.Close();
			if (this._sslStream != null)
			{
				this._sslStream.Close();
			}
		}

		// Token: 0x04001891 RID: 6289
		private SslStream _sslStream;

		// Token: 0x04001892 RID: 6290
		private string _host;

		// Token: 0x04001893 RID: 6291
		private X509CertificateCollection _clientCertificates;
	}
}
