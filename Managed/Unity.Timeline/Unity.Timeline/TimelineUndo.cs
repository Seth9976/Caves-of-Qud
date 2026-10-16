using System;
using System.Diagnostics;

namespace UnityEngine.Timeline
{
	// Token: 0x02000059 RID: 89
	internal static class TimelineUndo
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000BA01 File Offset: 0x00009C01
		internal static bool undoEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000BA04 File Offset: 0x00009C04
		public static void PushDestroyUndo(TimelineAsset timeline, Object thingToDirty, Object objectToDestroy)
		{
			if (objectToDestroy != null)
			{
				Object.Destroy(objectToDestroy);
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000BA15 File Offset: 0x00009C15
		[Conditional("UNITY_EDITOR")]
		public static void PushUndo(Object[] thingsToDirty, string operation)
		{
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000BA17 File Offset: 0x00009C17
		[Conditional("UNITY_EDITOR")]
		public static void PushUndo(Object thingToDirty, string operation)
		{
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000BA19 File Offset: 0x00009C19
		[Conditional("UNITY_EDITOR")]
		public static void RegisterCreatedObjectUndo(Object thingCreated, string operation)
		{
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000BA1B File Offset: 0x00009C1B
		internal static string UndoName(string name)
		{
			return "Timeline " + name;
		}
	}
}
