using System;
using System.Collections.Generic;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000031 RID: 49
	public static class TaskPool
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00003C38 File Offset: 0x00001E38
		static TaskPool()
		{
			try
			{
				string environmentVariable = Environment.GetEnvironmentVariable("UNITASK_MAX_POOLSIZE");
				int num;
				if (environmentVariable != null && int.TryParse(environmentVariable, out num))
				{
					TaskPool.MaxPoolSize = num;
					return;
				}
			}
			catch
			{
			}
			TaskPool.MaxPoolSize = int.MaxValue;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003C90 File Offset: 0x00001E90
		public static void SetMaxPoolSize(int maxPoolSize)
		{
			TaskPool.MaxPoolSize = maxPoolSize;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003C98 File Offset: 0x00001E98
		public static IEnumerable<ValueTuple<Type, int>> GetCacheSizeInfo()
		{
			Dictionary<Type, Func<int>> dictionary = TaskPool.sizes;
			lock (dictionary)
			{
				foreach (KeyValuePair<Type, Func<int>> keyValuePair in TaskPool.sizes)
				{
					yield return new ValueTuple<Type, int>(keyValuePair.Key, keyValuePair.Value());
				}
				Dictionary<Type, Func<int>>.Enumerator enumerator = default(Dictionary<Type, Func<int>>.Enumerator);
			}
			dictionary = null;
			yield break;
			yield break;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public static void RegisterSizeGetter(Type type, Func<int> getSize)
		{
			Dictionary<Type, Func<int>> dictionary = TaskPool.sizes;
			lock (dictionary)
			{
				TaskPool.sizes[type] = getSize;
			}
		}

		// Token: 0x0400006B RID: 107
		internal static int MaxPoolSize;

		// Token: 0x0400006C RID: 108
		private static Dictionary<Type, Func<int>> sizes = new Dictionary<Type, Func<int>>();
	}
}
