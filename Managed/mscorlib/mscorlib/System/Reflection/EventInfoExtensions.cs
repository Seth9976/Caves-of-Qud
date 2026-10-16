using System;

namespace System.Reflection
{
	// Token: 0x020008D6 RID: 2262
	public static class EventInfoExtensions
	{
		// Token: 0x06004B73 RID: 19315 RVA: 0x000F07A5 File Offset: 0x000EE9A5
		public static MethodInfo GetAddMethod(EventInfo eventInfo)
		{
			Requires.NotNull(eventInfo, "eventInfo");
			return eventInfo.GetAddMethod();
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x000F07B8 File Offset: 0x000EE9B8
		public static MethodInfo GetAddMethod(EventInfo eventInfo, bool nonPublic)
		{
			Requires.NotNull(eventInfo, "eventInfo");
			return eventInfo.GetAddMethod(nonPublic);
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x000F07CC File Offset: 0x000EE9CC
		public static MethodInfo GetRaiseMethod(EventInfo eventInfo)
		{
			Requires.NotNull(eventInfo, "eventInfo");
			return eventInfo.GetRaiseMethod();
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x000F07DF File Offset: 0x000EE9DF
		public static MethodInfo GetRaiseMethod(EventInfo eventInfo, bool nonPublic)
		{
			Requires.NotNull(eventInfo, "eventInfo");
			return eventInfo.GetRaiseMethod(nonPublic);
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x000F07F3 File Offset: 0x000EE9F3
		public static MethodInfo GetRemoveMethod(EventInfo eventInfo)
		{
			Requires.NotNull(eventInfo, "eventInfo");
			return eventInfo.GetRemoveMethod();
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x000F0806 File Offset: 0x000EEA06
		public static MethodInfo GetRemoveMethod(EventInfo eventInfo, bool nonPublic)
		{
			Requires.NotNull(eventInfo, "eventInfo");
			return eventInfo.GetRemoveMethod(nonPublic);
		}
	}
}
