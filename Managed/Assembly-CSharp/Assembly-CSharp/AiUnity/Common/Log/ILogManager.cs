using System;
using UnityEngine;

namespace AiUnity.Common.Log
{
	// Token: 0x020002D3 RID: 723
	public interface ILogManager
	{
		// Token: 0x060018ED RID: 6381
		ILogger GetLogger(string name, global::UnityEngine.Object context, IFormatProvider formatProvider = null);
	}
}
