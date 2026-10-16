using System;
using UnityEngine;

namespace UnityEngineInternal
{
	// Token: 0x02000010 RID: 16
	public sealed class APIUpdaterRuntimeServices
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002121 File Offset: 0x00000321
		[Obsolete("Method is not meant to be used at runtime. Please, replace this call with GameObject.AddComponent<T>()/GameObject.AddComponent(Type).", true)]
		public static Component AddComponent(GameObject go, string sourceInfo, string name)
		{
			throw new Exception();
		}
	}
}
