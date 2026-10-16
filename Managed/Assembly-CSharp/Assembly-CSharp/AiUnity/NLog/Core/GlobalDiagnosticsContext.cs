using System;
using System.Collections.Generic;
using AiUnity.Common.Log;

namespace AiUnity.NLog.Core
{
	// Token: 0x02000209 RID: 521
	public sealed class GlobalDiagnosticsContext : IVariablesContext
	{
		// Token: 0x060012B3 RID: 4787 RVA: 0x0006E958 File Offset: 0x0006CB58
		public static void Set(string item, string value)
		{
			Dictionary<string, string> dictionary = GlobalDiagnosticsContext.contextVariables;
			lock (dictionary)
			{
				GlobalDiagnosticsContext.contextVariables[item] = value;
			}
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0006E9A0 File Offset: 0x0006CBA0
		void IVariablesContext.Set(string key, object value)
		{
			GlobalDiagnosticsContext.Set(key, value.ToString());
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0006E9B0 File Offset: 0x0006CBB0
		public static string Get(string item)
		{
			Dictionary<string, string> dictionary = GlobalDiagnosticsContext.contextVariables;
			string text;
			lock (dictionary)
			{
				string empty;
				if (!GlobalDiagnosticsContext.contextVariables.TryGetValue(item, out empty))
				{
					empty = string.Empty;
				}
				text = empty;
			}
			return text;
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0006EA04 File Offset: 0x0006CC04
		object IVariablesContext.Get(string key)
		{
			return GlobalDiagnosticsContext.Get(key);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0006EA0C File Offset: 0x0006CC0C
		public static bool Contains(string item)
		{
			Dictionary<string, string> dictionary = GlobalDiagnosticsContext.contextVariables;
			bool flag2;
			lock (dictionary)
			{
				flag2 = GlobalDiagnosticsContext.contextVariables.ContainsKey(item);
			}
			return flag2;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0006EA54 File Offset: 0x0006CC54
		bool IVariablesContext.Contains(string key)
		{
			return GlobalDiagnosticsContext.Contains(key);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0006EA5C File Offset: 0x0006CC5C
		public static void Remove(string item)
		{
			Dictionary<string, string> dictionary = GlobalDiagnosticsContext.contextVariables;
			lock (dictionary)
			{
				GlobalDiagnosticsContext.contextVariables.Remove(item);
			}
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0006EAA4 File Offset: 0x0006CCA4
		void IVariablesContext.Remove(string key)
		{
			GlobalDiagnosticsContext.Remove(key);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0006EAAC File Offset: 0x0006CCAC
		public static void Clear()
		{
			Dictionary<string, string> dictionary = GlobalDiagnosticsContext.contextVariables;
			lock (dictionary)
			{
				GlobalDiagnosticsContext.contextVariables.Clear();
			}
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0006EAF0 File Offset: 0x0006CCF0
		void IVariablesContext.Clear()
		{
			GlobalDiagnosticsContext.Clear();
		}

		// Token: 0x04000C8A RID: 3210
		private static Dictionary<string, string> contextVariables = new Dictionary<string, string>();
	}
}
