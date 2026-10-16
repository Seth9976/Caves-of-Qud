using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020004F2 RID: 1266
	[NativeHeader("Runtime/Graphics/DrawSplashScreenAndWatermarks.h")]
	public class SplashScreen
	{
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06003198 RID: 12696
		public static extern bool isFinished
		{
			[FreeFunction("IsSplashScreenFinished")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06003199 RID: 12697
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CancelSplashScreen();

		// Token: 0x0600319A RID: 12698
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSplashScreenFade();

		// Token: 0x0600319B RID: 12699
		[FreeFunction("BeginSplashScreen_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Begin();

		// Token: 0x0600319C RID: 12700 RVA: 0x0005F5B4 File Offset: 0x0005D7B4
		public static void Stop(SplashScreen.StopBehavior stopBehavior)
		{
			bool flag = stopBehavior == SplashScreen.StopBehavior.FadeOut;
			if (flag)
			{
				SplashScreen.BeginSplashScreenFade();
			}
			else
			{
				SplashScreen.CancelSplashScreen();
			}
		}

		// Token: 0x0600319D RID: 12701
		[FreeFunction("DrawSplashScreen_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Draw();

		// Token: 0x0600319E RID: 12702
		[FreeFunction("SetSplashScreenTime")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetTime(float time);

		// Token: 0x020004F3 RID: 1267
		public enum StopBehavior
		{
			// Token: 0x04000F52 RID: 3922
			StopImmediate,
			// Token: 0x04000F53 RID: 3923
			FadeOut
		}
	}
}
