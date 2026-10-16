using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000035 RID: 53
	public abstract class InputProcessor
	{
		// Token: 0x06000469 RID: 1129
		public abstract object ProcessAsObject(object value, InputControl control);

		// Token: 0x0600046A RID: 1130
		public unsafe abstract void Process(void* buffer, int bufferSize, InputControl control);

		// Token: 0x0600046B RID: 1131 RVA: 0x00013858 File Offset: 0x00011A58
		internal static Type GetValueTypeFromType(Type processorType)
		{
			if (processorType == null)
			{
				throw new ArgumentNullException("processorType");
			}
			return TypeHelpers.GetGenericTypeArgumentFromHierarchy(processorType, typeof(InputProcessor<>), 0);
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001387F File Offset: 0x00011A7F
		public virtual InputProcessor.CachingPolicy cachingPolicy
		{
			get
			{
				return InputProcessor.CachingPolicy.CacheResult;
			}
		}

		// Token: 0x0400014D RID: 333
		internal static TypeTable s_Processors;

		// Token: 0x0200019B RID: 411
		public enum CachingPolicy
		{
			// Token: 0x040008FB RID: 2299
			CacheResult,
			// Token: 0x040008FC RID: 2300
			EvaluateOnEveryRead
		}
	}
}
