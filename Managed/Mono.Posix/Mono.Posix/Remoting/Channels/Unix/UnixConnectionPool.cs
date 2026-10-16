using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Threading;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200008B RID: 139
	internal class UnixConnectionPool
	{
		// Token: 0x060006C5 RID: 1733 RVA: 0x0000F01C File Offset: 0x0000D21C
		static UnixConnectionPool()
		{
			UnixConnectionPool._poolThread.Start();
			UnixConnectionPool._poolThread.IsBackground = true;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000F06F File Offset: 0x0000D26F
		public static void Shutdown()
		{
			if (UnixConnectionPool._poolThread != null)
			{
				UnixConnectionPool._poolThread.Abort();
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0000F082 File Offset: 0x0000D282
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x0000F089 File Offset: 0x0000D289
		public static int MaxOpenConnections
		{
			get
			{
				return UnixConnectionPool._maxOpenConnections;
			}
			set
			{
				if (value < 1)
				{
					throw new RemotingException("MaxOpenConnections must be greater than zero");
				}
				UnixConnectionPool._maxOpenConnections = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0000F0A0 File Offset: 0x0000D2A0
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x0000F0A7 File Offset: 0x0000D2A7
		public static int KeepAliveSeconds
		{
			get
			{
				return UnixConnectionPool._keepAliveSeconds;
			}
			set
			{
				UnixConnectionPool._keepAliveSeconds = value;
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
		public static UnixConnection GetConnection(string path)
		{
			Hashtable pools = UnixConnectionPool._pools;
			HostConnectionPool hostConnectionPool;
			lock (pools)
			{
				hostConnectionPool = (HostConnectionPool)UnixConnectionPool._pools[path];
				if (hostConnectionPool == null)
				{
					hostConnectionPool = new HostConnectionPool(path);
					UnixConnectionPool._pools[path] = hostConnectionPool;
				}
			}
			return hostConnectionPool.GetConnection();
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0000F118 File Offset: 0x0000D318
		private static void ConnectionCollector()
		{
			for (;;)
			{
				Thread.Sleep(3000);
				Hashtable pools = UnixConnectionPool._pools;
				lock (pools)
				{
					foreach (object obj in UnixConnectionPool._pools.Values)
					{
						((HostConnectionPool)obj).PurgeConnections();
					}
				}
			}
		}

		// Token: 0x040004D0 RID: 1232
		private static Hashtable _pools = new Hashtable();

		// Token: 0x040004D1 RID: 1233
		private static int _maxOpenConnections = int.MaxValue;

		// Token: 0x040004D2 RID: 1234
		private static int _keepAliveSeconds = 15;

		// Token: 0x040004D3 RID: 1235
		private static Thread _poolThread = new Thread(new ThreadStart(UnixConnectionPool.ConnectionCollector));
	}
}
