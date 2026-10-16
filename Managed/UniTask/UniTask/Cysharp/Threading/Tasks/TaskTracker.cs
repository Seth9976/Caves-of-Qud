using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000025 RID: 37
	public static class TaskTracker
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00002D6F File Offset: 0x00000F6F
		[Conditional("UNITY_EDITOR")]
		public static void TrackActiveTask(IUniTaskSource task, int skipFrame)
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002D71 File Offset: 0x00000F71
		[Conditional("UNITY_EDITOR")]
		public static void RemoveTracking(IUniTaskSource task)
		{
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002D73 File Offset: 0x00000F73
		public static bool CheckAndResetDirty()
		{
			bool flag = TaskTracker.dirty;
			TaskTracker.dirty = false;
			return flag;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002D80 File Offset: 0x00000F80
		public static void ForEachActiveTask(Action<int, string, UniTaskStatus, DateTime, string> action)
		{
			List<KeyValuePair<IUniTaskSource, ValueTuple<string, int, DateTime, string>>> list = TaskTracker.listPool;
			lock (list)
			{
				int num = TaskTracker.tracking.ToList(ref TaskTracker.listPool, false);
				try
				{
					for (int i = 0; i < num; i++)
					{
						action(TaskTracker.listPool[i].Value.Item2, TaskTracker.listPool[i].Value.Item1, TaskTracker.listPool[i].Key.UnsafeGetStatus(), TaskTracker.listPool[i].Value.Item3, TaskTracker.listPool[i].Value.Item4);
						TaskTracker.listPool[i] = default(KeyValuePair<IUniTaskSource, ValueTuple<string, int, DateTime, string>>);
					}
				}
				catch
				{
					TaskTracker.listPool.Clear();
					throw;
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002E90 File Offset: 0x00001090
		private static void TypeBeautify(Type type, StringBuilder sb)
		{
			if (type.IsNested)
			{
				sb.Append(type.DeclaringType.Name.ToString());
				sb.Append(".");
			}
			if (type.IsGenericType)
			{
				int num = type.Name.IndexOf("`");
				if (num != -1)
				{
					sb.Append(type.Name.Substring(0, num));
				}
				else
				{
					sb.Append(type.Name);
				}
				sb.Append("<");
				bool flag = true;
				foreach (Type type2 in type.GetGenericArguments())
				{
					if (!flag)
					{
						sb.Append(", ");
					}
					flag = false;
					TaskTracker.TypeBeautify(type2, sb);
				}
				sb.Append(">");
				return;
			}
			sb.Append(type.Name);
		}

		// Token: 0x0400002A RID: 42
		[TupleElementNames(new string[] { "formattedType", "trackingId", "addTime", "stackTrace" })]
		private static List<KeyValuePair<IUniTaskSource, ValueTuple<string, int, DateTime, string>>> listPool = new List<KeyValuePair<IUniTaskSource, ValueTuple<string, int, DateTime, string>>>();

		// Token: 0x0400002B RID: 43
		[TupleElementNames(new string[] { "formattedType", "trackingId", "addTime", "stackTrace" })]
		private static readonly WeakDictionary<IUniTaskSource, ValueTuple<string, int, DateTime, string>> tracking = new WeakDictionary<IUniTaskSource, ValueTuple<string, int, DateTime, string>>(4, 0.75f, null);

		// Token: 0x0400002C RID: 44
		private static bool dirty;
	}
}
