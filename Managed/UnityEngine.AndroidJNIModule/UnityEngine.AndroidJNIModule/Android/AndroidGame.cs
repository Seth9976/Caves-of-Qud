using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Android
{
	// Token: 0x02000028 RID: 40
	[StaticAccessor("GameStateHelper::Get()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/AndroidJNI/Public/GameStateHelper.h")]
	public static class AndroidGame
	{
		// Token: 0x0600029E RID: 670
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void StartLoading(int label);

		// Token: 0x0600029F RID: 671
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void StopLoading(int label);

		// Token: 0x060002A0 RID: 672 RVA: 0x0000AB20 File Offset: 0x00008D20
		private static AndroidJavaObject GetUnityGameManager()
		{
			bool flag = AndroidGame.m_UnityGameManager != null;
			AndroidJavaObject androidJavaObject;
			if (flag)
			{
				androidJavaObject = AndroidGame.m_UnityGameManager;
			}
			else
			{
				AndroidGame.m_UnityGameManager = new AndroidJavaClass("com.unity3d.player.UnityGameManager");
				androidJavaObject = AndroidGame.m_UnityGameManager;
			}
			return androidJavaObject;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000AB5C File Offset: 0x00008D5C
		private static AndroidJavaObject GetUnityGameState()
		{
			bool flag = AndroidGame.m_UnityGameState != null;
			AndroidJavaObject androidJavaObject;
			if (flag)
			{
				androidJavaObject = AndroidGame.m_UnityGameState;
			}
			else
			{
				AndroidGame.m_UnityGameState = new AndroidJavaClass("com.unity3d.player.UnityGameState");
				androidJavaObject = AndroidGame.m_UnityGameState;
			}
			return androidJavaObject;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000AB98 File Offset: 0x00008D98
		public static AndroidGameMode GameMode
		{
			get
			{
				return AndroidGameMode.Unsupported;
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A2A0 File Offset: 0x000084A0
		public static void SetGameState(bool isLoading, AndroidGameState gameState)
		{
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A2A0 File Offset: 0x000084A0
		public static void SetGameState(bool isLoading, AndroidGameState gameState, int label, int quality)
		{
		}

		// Token: 0x04000083 RID: 131
		private static AndroidJavaObject m_UnityGameManager;

		// Token: 0x04000084 RID: 132
		private static AndroidJavaObject m_UnityGameState;

		// Token: 0x02000029 RID: 41
		[StaticAccessor("GameStateHelper::Get()", StaticAccessorType.Dot)]
		public static class Automatic
		{
			// Token: 0x060002A5 RID: 677
			[NativeMethod("SetGameStateMode")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public static extern void SetGameState(AndroidGameState mode);
		}
	}
}
