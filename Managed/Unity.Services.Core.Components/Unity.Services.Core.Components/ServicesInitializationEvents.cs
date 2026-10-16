using System;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.Services.Core.Components
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class ServicesInitializationEvents
	{
		// Token: 0x0400000A RID: 10
		[SerializeField]
		public UnityEvent Initialized = new UnityEvent();

		// Token: 0x0400000B RID: 11
		[SerializeField]
		public UnityEvent<Exception> InitializeFailed = new UnityEvent<Exception>();
	}
}
