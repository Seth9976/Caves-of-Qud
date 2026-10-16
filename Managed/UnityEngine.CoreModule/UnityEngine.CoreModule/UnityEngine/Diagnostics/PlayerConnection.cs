using System;
using System.ComponentModel;
using UnityEngine.Networking.PlayerConnection;

namespace UnityEngine.Diagnostics
{
	// Token: 0x02000461 RID: 1121
	public static class PlayerConnection
	{
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06002B6F RID: 11119 RVA: 0x000530DC File Offset: 0x000512DC
		[Obsolete("Use UnityEngine.Networking.PlayerConnection.PlayerConnection.instance.isConnected instead.")]
		public static bool connected
		{
			get
			{
				return PlayerConnection.instance.isConnected;
			}
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x00002925 File Offset: 0x00000B25
		[Obsolete("PlayerConnection.SendFile is no longer supported.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void SendFile(string remoteFilePath, byte[] data)
		{
		}
	}
}
