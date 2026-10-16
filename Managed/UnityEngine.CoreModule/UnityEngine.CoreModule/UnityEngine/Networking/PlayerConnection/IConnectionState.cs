using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x0200042E RID: 1070
	[MovedFrom("UnityEngine.Experimental.Networking.PlayerConnection")]
	public interface IConnectionState : IDisposable
	{
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060029BA RID: 10682
		ConnectionTarget connectedToTarget { get; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060029BB RID: 10683
		string connectionName { get; }
	}
}
