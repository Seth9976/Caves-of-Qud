using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.SceneManagement
{
	// Token: 0x02000385 RID: 901
	[NativeHeader("Runtime/Export/SceneManager/SceneUtility.bindings.h")]
	public static class SceneUtility
	{
		// Token: 0x0600296D RID: 10605 RVA: 0x0004FD44 File Offset: 0x0004DF44
		[StaticAccessor("SceneUtilityBindings", StaticAccessorType.DoubleColon)]
		public static string GetScenePathByBuildIndex(int buildIndex)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SceneUtility.GetScenePathByBuildIndex_Injected(buildIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x0004FD74 File Offset: 0x0004DF74
		[StaticAccessor("SceneUtilityBindings", StaticAccessorType.DoubleColon)]
		public unsafe static int GetBuildIndexByScenePath(string scenePath)
		{
			int buildIndexByScenePath_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(scenePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = scenePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				buildIndexByScenePath_Injected = SceneUtility.GetBuildIndexByScenePath_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return buildIndexByScenePath_Injected;
		}

		// Token: 0x0600296F RID: 10607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetScenePathByBuildIndex_Injected(int buildIndex, out ManagedSpanWrapper ret);

		// Token: 0x06002970 RID: 10608
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetBuildIndexByScenePath_Injected(ref ManagedSpanWrapper scenePath);
	}
}
