using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000446 RID: 1094
	[RequiredByNativeCode]
	public interface INotificationReceiver
	{
		// Token: 0x06002A33 RID: 10803
		[RequiredByNativeCode]
		void OnNotify(Playable origin, INotification notification, object context);
	}
}
