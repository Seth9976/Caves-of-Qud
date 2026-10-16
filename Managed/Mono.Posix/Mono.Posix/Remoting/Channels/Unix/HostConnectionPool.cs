using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Threading;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200008E RID: 142
	internal class HostConnectionPool
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x0000F25E File Offset: 0x0000D45E
		public HostConnectionPool(string path)
		{
			this._path = path;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0000F278 File Offset: 0x0000D478
		public UnixConnection GetConnection()
		{
			UnixConnection unixConnection = null;
			ArrayList pool = this._pool;
			lock (pool)
			{
				for (;;)
				{
					if (this._pool.Count <= 0)
					{
						goto IL_006A;
					}
					unixConnection = (UnixConnection)this._pool[this._pool.Count - 1];
					this._pool.RemoveAt(this._pool.Count - 1);
					if (unixConnection.IsAlive)
					{
						goto IL_006A;
					}
					this.CancelConnection(unixConnection);
					unixConnection = null;
					IL_008B:
					if (unixConnection != null)
					{
						break;
					}
					continue;
					IL_006A:
					if (unixConnection == null && this._activeConnections < UnixConnectionPool.MaxOpenConnections)
					{
						break;
					}
					if (unixConnection == null)
					{
						Monitor.Wait(this._pool);
						goto IL_008B;
					}
					goto IL_008B;
				}
			}
			if (unixConnection == null)
			{
				return this.CreateConnection();
			}
			return unixConnection;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0000F33C File Offset: 0x0000D53C
		private UnixConnection CreateConnection()
		{
			UnixConnection unixConnection2;
			try
			{
				ReusableUnixClient reusableUnixClient = new ReusableUnixClient(this._path);
				UnixConnection unixConnection = new UnixConnection(this, reusableUnixClient);
				this._activeConnections++;
				unixConnection2 = unixConnection;
			}
			catch (Exception ex)
			{
				throw new RemotingException(ex.Message);
			}
			return unixConnection2;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0000F38C File Offset: 0x0000D58C
		public void ReleaseConnection(UnixConnection entry)
		{
			ArrayList pool = this._pool;
			lock (pool)
			{
				entry.ControlTime = DateTime.UtcNow;
				this._pool.Add(entry);
				Monitor.Pulse(this._pool);
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		private void CancelConnection(UnixConnection entry)
		{
			try
			{
				entry.Stream.Close();
				this._activeConnections--;
			}
			catch
			{
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0000F428 File Offset: 0x0000D628
		public void PurgeConnections()
		{
			ArrayList pool = this._pool;
			lock (pool)
			{
				for (int i = 0; i < this._pool.Count; i++)
				{
					UnixConnection unixConnection = (UnixConnection)this._pool[i];
					if ((DateTime.UtcNow - unixConnection.ControlTime).TotalSeconds > (double)UnixConnectionPool.KeepAliveSeconds)
					{
						this.CancelConnection(unixConnection);
						this._pool.RemoveAt(i);
						i--;
					}
				}
			}
		}

		// Token: 0x040004D9 RID: 1241
		private ArrayList _pool = new ArrayList();

		// Token: 0x040004DA RID: 1242
		private int _activeConnections;

		// Token: 0x040004DB RID: 1243
		private string _path;
	}
}
