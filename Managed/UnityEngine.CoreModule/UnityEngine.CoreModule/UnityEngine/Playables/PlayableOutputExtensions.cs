using System;

namespace UnityEngine.Playables
{
	// Token: 0x0200045A RID: 1114
	public static class PlayableOutputExtensions
	{
		// Token: 0x06002B1E RID: 11038 RVA: 0x000526F4 File Offset: 0x000508F4
		public static bool IsOutputNull<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().IsNull();
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x0005271C File Offset: 0x0005091C
		public static bool IsOutputValid<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().IsValid();
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x00052744 File Offset: 0x00050944
		public static Object GetReferenceObject<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetReferenceObject();
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x0005276C File Offset: 0x0005096C
		public static void SetReferenceObject<U>(this U output, Object value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetReferenceObject(value);
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x00052794 File Offset: 0x00050994
		public static Object GetUserData<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetUserData();
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000527BC File Offset: 0x000509BC
		public static void SetUserData<U>(this U output, Object value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetUserData(value);
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000527E4 File Offset: 0x000509E4
		public static Playable GetSourcePlayable<U>(this U output) where U : struct, IPlayableOutput
		{
			return new Playable(output.GetHandle().GetSourcePlayable());
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x00052810 File Offset: 0x00050A10
		public static void SetSourcePlayable<U, V>(this U output, V value) where U : struct, IPlayableOutput where V : struct, IPlayable
		{
			output.GetHandle().SetSourcePlayable(value.GetHandle(), output.GetSourceOutputPort<U>());
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x00052848 File Offset: 0x00050A48
		public static void SetSourcePlayable<U, V>(this U output, V value, int port) where U : struct, IPlayableOutput where V : struct, IPlayable
		{
			output.GetHandle().SetSourcePlayable(value.GetHandle(), port);
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x0005287C File Offset: 0x00050A7C
		public static int GetSourceOutputPort<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetSourceOutputPort();
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000528A4 File Offset: 0x00050AA4
		public static float GetWeight<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetWeight();
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x000528CC File Offset: 0x00050ACC
		public static void SetWeight<U>(this U output, float value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetWeight(value);
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x000528F4 File Offset: 0x00050AF4
		public static void PushNotification<U>(this U output, Playable origin, INotification notification, object context = null) where U : struct, IPlayableOutput
		{
			output.GetHandle().PushNotification(origin.GetHandle(), notification, context);
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x00052924 File Offset: 0x00050B24
		public static INotificationReceiver[] GetNotificationReceivers<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetNotificationReceivers();
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x0005294C File Offset: 0x00050B4C
		public static void AddNotificationReceiver<U>(this U output, INotificationReceiver receiver) where U : struct, IPlayableOutput
		{
			output.GetHandle().AddNotificationReceiver(receiver);
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x00052974 File Offset: 0x00050B74
		public static void RemoveNotificationReceiver<U>(this U output, INotificationReceiver receiver) where U : struct, IPlayableOutput
		{
			output.GetHandle().RemoveNotificationReceiver(receiver);
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x0005299C File Offset: 0x00050B9C
		[Obsolete("Method GetSourceInputPort has been renamed to GetSourceOutputPort (UnityUpgradable) -> GetSourceOutputPort<U>(*)", false)]
		public static int GetSourceInputPort<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetSourceOutputPort();
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x000529C3 File Offset: 0x00050BC3
		[Obsolete("Method SetSourceInputPort has been deprecated. Use SetSourcePlayable(Playable, Port) instead.", false)]
		public static void SetSourceInputPort<U>(this U output, int value) where U : struct, IPlayableOutput
		{
			output.SetSourcePlayable(output.GetSourcePlayable<U>(), value);
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x000529C3 File Offset: 0x00050BC3
		[Obsolete("Method SetSourceOutputPort has been deprecated. Use SetSourcePlayable(Playable, Port) instead.", false)]
		public static void SetSourceOutputPort<U>(this U output, int value) where U : struct, IPlayableOutput
		{
			output.SetSourcePlayable(output.GetSourcePlayable<U>(), value);
		}
	}
}
