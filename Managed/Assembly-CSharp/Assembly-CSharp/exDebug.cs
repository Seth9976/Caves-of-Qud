using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020000F6 RID: 246
public static class exDebug
{
	// Token: 0x0600095F RID: 2399 RVA: 0x0003B34B File Offset: 0x0003954B
	[Conditional("EX_DEBUG")]
	public static void Assert(bool _test, string _msg = "", bool _logError = true, global::UnityEngine.Object _context = null)
	{
		if (!_test)
		{
			if (_logError)
			{
				global::UnityEngine.Debug.LogError("Assert Failed! " + _msg, _context);
				return;
			}
			global::UnityEngine.Debug.LogWarning("Assert Failed! " + _msg, _context);
		}
	}
}
