using System;
using UnityEngine;

namespace AiUnity.Common.Log
{
	// Token: 0x020002D4 RID: 724
	public interface ILogger
	{
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060018EE RID: 6382
		string Name { get; }

		// Token: 0x060018EF RID: 6383
		void Log(LogLevels level, global::UnityEngine.Object context, Exception exception, string message, params object[] args);
	}
}
