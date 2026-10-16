using System;
using System.Runtime.CompilerServices;
using LaundryBear.PlatformServices;
using UnityEngine;

namespace Platform.IO
{
	// Token: 0x0200034B RID: 843
	public static class State
	{
		// Token: 0x06001F75 RID: 8053 RVA: 0x000990E4 File Offset: 0x000972E4
		public static void SetStorage(IStorage storage)
		{
			if (State.storage != null)
			{
				Debug.LogWarning("Trying to set storage to " + storage.GetType().Name + " but it has already been set");
				return;
			}
			Debug.Log("Setting Storage to " + storage.GetType().Name);
			State.storage = storage;
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x00099138 File Offset: 0x00097338
		public static IStorage GetStorage()
		{
			if (State.storage == null)
			{
				throw new Exception("Couldn't get IStorage service... are you calling into Platform.IO before it is initialized?");
			}
			return State.storage;
		}

		// Token: 0x04001027 RID: 4135
		[Nullable(2)]
		private static IStorage storage;
	}
}
