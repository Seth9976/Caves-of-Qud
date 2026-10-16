using System;
using System.Collections;
using System.Reflection;
using System.Security;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200028E RID: 654
	[RequiredByNativeCode]
	internal class SetupCoroutine
	{
		// Token: 0x06002088 RID: 8328 RVA: 0x0003EC18 File Offset: 0x0003CE18
		[SecuritySafeCritical]
		[RequiredByNativeCode]
		public unsafe static void InvokeMoveNext(IEnumerator enumerator, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			*(byte*)(void*)returnValueAddress = (enumerator.MoveNext() ? 1 : 0);
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x0003EC54 File Offset: 0x0003CE54
		[RequiredByNativeCode]
		public static object InvokeMember(object behaviour, string name, object variable)
		{
			object[] array = null;
			bool flag = variable != null;
			if (flag)
			{
				array = new object[] { variable };
			}
			return behaviour.GetType().InvokeMember(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, behaviour, array, null, null, null);
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x0003EC94 File Offset: 0x0003CE94
		public static object InvokeStatic(Type klass, string name, object variable)
		{
			object[] array = null;
			bool flag = variable != null;
			if (flag)
			{
				array = new object[] { variable };
			}
			return klass.InvokeMember(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, null, array, null, null, null);
		}
	}
}
