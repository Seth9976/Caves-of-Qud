using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000054 RID: 84
	internal static class NotificationUtilities
	{
		// Token: 0x06000305 RID: 773 RVA: 0x0000AC1B File Offset: 0x00008E1B
		public static ScriptPlayable<TimeNotificationBehaviour> CreateNotificationsPlayable(PlayableGraph graph, IEnumerable<IMarker> markers, PlayableDirector director)
		{
			return NotificationUtilities.CreateNotificationsPlayable(graph, markers, null, director);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000AC26 File Offset: 0x00008E26
		public static ScriptPlayable<TimeNotificationBehaviour> CreateNotificationsPlayable(PlayableGraph graph, IEnumerable<IMarker> markers, TimelineAsset timelineAsset)
		{
			return NotificationUtilities.CreateNotificationsPlayable(graph, markers, timelineAsset, null);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000AC34 File Offset: 0x00008E34
		private static ScriptPlayable<TimeNotificationBehaviour> CreateNotificationsPlayable(PlayableGraph graph, IEnumerable<IMarker> markers, IPlayableAsset asset, PlayableDirector director)
		{
			ScriptPlayable<TimeNotificationBehaviour> scriptPlayable = ScriptPlayable<TimeNotificationBehaviour>.Null;
			DirectorWrapMode directorWrapMode = ((director != null) ? director.extrapolationMode : DirectorWrapMode.None);
			bool flag = false;
			double num = 0.0;
			foreach (IMarker marker in markers)
			{
				INotification notification = marker as INotification;
				if (notification != null)
				{
					if (!flag)
					{
						num = ((director != null) ? director.playableAsset.duration : asset.duration);
						flag = true;
					}
					if (scriptPlayable.Equals(ScriptPlayable<TimeNotificationBehaviour>.Null))
					{
						scriptPlayable = TimeNotificationBehaviour.Create(graph, num, directorWrapMode);
					}
					DiscreteTime discreteTime = (DiscreteTime)marker.time;
					DiscreteTime discreteTime2 = (DiscreteTime)num;
					if (discreteTime >= discreteTime2 && discreteTime <= discreteTime2.OneTickAfter() && discreteTime2 != 0)
					{
						discreteTime = discreteTime2.OneTickBefore();
					}
					INotificationOptionProvider notificationOptionProvider = marker as INotificationOptionProvider;
					if (notificationOptionProvider != null)
					{
						scriptPlayable.GetBehaviour().AddNotification((double)discreteTime, notification, notificationOptionProvider.flags);
					}
					else
					{
						scriptPlayable.GetBehaviour().AddNotification((double)discreteTime, notification, NotificationFlags.Retroactive);
					}
				}
			}
			return scriptPlayable;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000AD7C File Offset: 0x00008F7C
		public static bool TrackTypeSupportsNotifications(Type type)
		{
			TrackBindingTypeAttribute trackBindingTypeAttribute = (TrackBindingTypeAttribute)Attribute.GetCustomAttribute(type, typeof(TrackBindingTypeAttribute));
			return trackBindingTypeAttribute != null && (typeof(Component).IsAssignableFrom(trackBindingTypeAttribute.type) || typeof(GameObject).IsAssignableFrom(trackBindingTypeAttribute.type));
		}
	}
}
