using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002B6 RID: 694
	[NativeHeader("PlatformDependent/iPhonePlayer/IOSScriptBindings.h")]
	internal sealed class UnhandledExceptionHandler
	{
		// Token: 0x060021DC RID: 8668 RVA: 0x00042109 File Offset: 0x00040309
		[RequiredByNativeCode]
		private static void RegisterUECatcher()
		{
			AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
			{
				Debug.LogException(e.ExceptionObject as Exception);
			};
		}
	}
}
