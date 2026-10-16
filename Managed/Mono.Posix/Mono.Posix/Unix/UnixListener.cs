using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Mono.Unix
{
	// Token: 0x0200001A RID: 26
	public class UnixListener : MarshalByRefObject, IDisposable
	{
		// Token: 0x06000136 RID: 310 RVA: 0x000056A8 File Offset: 0x000038A8
		private void Init(UnixEndPoint ep)
		{
			this.listening = false;
			string filename = ep.Filename;
			if (File.Exists(filename))
			{
				Socket socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
				try
				{
					socket.Connect(ep);
					socket.Close();
					throw new InvalidOperationException("There's already a server listening on " + filename);
				}
				catch (SocketException)
				{
				}
				File.Delete(filename);
			}
			this.server = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
			this.server.Bind(ep);
			this.savedEP = this.server.LocalEndPoint;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005738 File Offset: 0x00003938
		public UnixListener(string path)
		{
			if (!Directory.Exists(Path.GetDirectoryName(path)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(path));
			}
			this.Init(new UnixEndPoint(path));
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005765 File Offset: 0x00003965
		public UnixListener(UnixEndPoint localEndPoint)
		{
			if (localEndPoint == null)
			{
				throw new ArgumentNullException("localendPoint");
			}
			this.Init(localEndPoint);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005782 File Offset: 0x00003982
		public EndPoint LocalEndpoint
		{
			get
			{
				return this.savedEP;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000578A File Offset: 0x0000398A
		protected Socket Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005792 File Offset: 0x00003992
		public Socket AcceptSocket()
		{
			this.CheckDisposed();
			if (!this.listening)
			{
				throw new InvalidOperationException("Socket is not listening");
			}
			return this.server.Accept();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000057B8 File Offset: 0x000039B8
		public UnixClient AcceptUnixClient()
		{
			this.CheckDisposed();
			if (!this.listening)
			{
				throw new InvalidOperationException("Socket is not listening");
			}
			return new UnixClient(this.AcceptSocket());
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000057E0 File Offset: 0x000039E0
		~UnixListener()
		{
			this.Dispose(false);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005810 File Offset: 0x00003A10
		public bool Pending()
		{
			this.CheckDisposed();
			if (!this.listening)
			{
				throw new InvalidOperationException("Socket is not listening");
			}
			return this.server.Poll(1000, SelectMode.SelectRead);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000583C File Offset: 0x00003A3C
		public void Start()
		{
			this.Start(5);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005845 File Offset: 0x00003A45
		public void Start(int backlog)
		{
			this.CheckDisposed();
			if (this.listening)
			{
				return;
			}
			this.server.Listen(backlog);
			this.listening = true;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005869 File Offset: 0x00003A69
		public void Stop()
		{
			this.CheckDisposed();
			this.Dispose(true);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005878 File Offset: 0x00003A78
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005888 File Offset: 0x00003A88
		protected void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				try
				{
					File.Delete(((UnixEndPoint)this.savedEP).Filename);
				}
				catch
				{
				}
				if (this.server != null)
				{
					this.server.Close();
				}
				this.server = null;
			}
			this.disposed = true;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000058EC File Offset: 0x00003AEC
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x04000077 RID: 119
		private bool disposed;

		// Token: 0x04000078 RID: 120
		private bool listening;

		// Token: 0x04000079 RID: 121
		private Socket server;

		// Token: 0x0400007A RID: 122
		private EndPoint savedEP;
	}
}
