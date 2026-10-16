using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002CE RID: 718
	[RequiredByNativeCode]
	public interface ISerializationCallbackReceiver
	{
		// Token: 0x06002287 RID: 8839
		[RequiredByNativeCode]
		void OnBeforeSerialize();

		// Token: 0x06002288 RID: 8840
		[RequiredByNativeCode]
		void OnAfterDeserialize();
	}
}
