using System;
using System.Collections.Generic;
using System.Security;

namespace UnityEngine
{
	// Token: 0x0200002A RID: 42
	internal class GUIStateObjects
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0000C1A0 File Offset: 0x0000A3A0
		[SecuritySafeCritical]
		internal static object GetStateObject(Type t, int controlID)
		{
			object obj;
			bool flag = !GUIStateObjects.s_StateCache.TryGetValue(controlID, out obj) || obj.GetType() != t;
			if (flag)
			{
				obj = Activator.CreateInstance(t);
				GUIStateObjects.s_StateCache[controlID] = obj;
			}
			return obj;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		internal static object QueryStateObject(Type t, int controlID)
		{
			object obj = GUIStateObjects.s_StateCache[controlID];
			bool flag = t.IsInstanceOfType(obj);
			object obj2;
			if (flag)
			{
				obj2 = obj;
			}
			else
			{
				obj2 = null;
			}
			return obj2;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000C21B File Offset: 0x0000A41B
		internal static void Tests_ClearObjects()
		{
			GUIStateObjects.s_StateCache.Clear();
		}

		// Token: 0x040000CD RID: 205
		private static Dictionary<int, object> s_StateCache = new Dictionary<int, object>();
	}
}
