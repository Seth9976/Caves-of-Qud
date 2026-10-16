using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000256 RID: 598
	[RequiredByNativeCode]
	internal class AsyncInstantiateOperationHelper
	{
		// Token: 0x06001F34 RID: 7988 RVA: 0x0003BED0 File Offset: 0x0003A0D0
		[RequiredByNativeCode]
		public static Object[] CreateAsyncInstantiateOperationResultArray(AsyncInstantiateOperation op, int size)
		{
			return op.CreateResultArray(size);
		}
	}
}
