using System;
using System.Collections.Generic;
using PlayFab.EventsModels;
using UnityEngine;

namespace PlayFab.Public
{
	// Token: 0x0200015D RID: 349
	public class ScreenTimeTracker : IScreenTimeTracker
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x00012E60 File Offset: 0x00011060
		public ScreenTimeTracker()
		{
			this.eventApi = new PlayFabEventsInstanceAPI(PlayFabSettings.staticPlayer);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00012EB8 File Offset: 0x000110B8
		public void ClientSessionStart(string entityId, string entityType, string playFabUserId)
		{
			this.gameSessionID = Guid.NewGuid();
			this.entityKey.Id = entityId;
			this.entityKey.Type = entityType;
			EventContents eventContents = new EventContents();
			eventContents.Name = "client_session_start";
			eventContents.EventNamespace = "com.playfab.events.sessions";
			eventContents.Entity = this.entityKey;
			eventContents.OriginalTimestamp = new DateTime?(DateTime.UtcNow);
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{ "UserID", playFabUserId },
				{
					"DeviceType",
					SystemInfo.deviceType
				},
				{
					"DeviceModel",
					SystemInfo.deviceModel
				},
				{
					"OS",
					SystemInfo.operatingSystem
				},
				{ "ClientSessionID", this.gameSessionID }
			};
			eventContents.Payload = dictionary;
			this.eventsRequests.Enqueue(eventContents);
			this.OnApplicationFocus(true);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00012F98 File Offset: 0x00011198
		public void OnApplicationFocus(bool isFocused)
		{
			EventContents eventContents = new EventContents();
			DateTime utcNow = DateTime.UtcNow;
			eventContents.Name = "client_focus_change";
			eventContents.EventNamespace = "com.playfab.events.sessions";
			eventContents.Entity = this.entityKey;
			double num = 0.0;
			if (this.initialFocus)
			{
				this.focusId = Guid.NewGuid();
			}
			if (isFocused)
			{
				this.focusOnDateTime = utcNow;
				this.focusId = Guid.NewGuid();
				if (!this.initialFocus)
				{
					num = (utcNow - this.focusOffDateTime).TotalSeconds;
					if (num < 0.0)
					{
						num = 0.0;
					}
				}
			}
			else
			{
				num = (utcNow - this.focusOnDateTime).TotalSeconds;
				if (num < 0.0)
				{
					num = 0.0;
				}
				this.focusOffDateTime = utcNow;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{ "FocusID", this.focusId },
				{ "FocusState", isFocused },
				{ "FocusStateDuration", num },
				{ "EventTimestamp", utcNow },
				{ "ClientSessionID", this.gameSessionID }
			};
			eventContents.OriginalTimestamp = new DateTime?(utcNow);
			eventContents.Payload = dictionary;
			this.eventsRequests.Enqueue(eventContents);
			this.initialFocus = false;
			if (!isFocused)
			{
				this.Send();
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00013104 File Offset: 0x00011304
		public void Send()
		{
			if (PlayFabSettings.staticPlayer.IsClientLoggedIn() && !this.isSending)
			{
				this.isSending = true;
				WriteEventsRequest writeEventsRequest = new WriteEventsRequest();
				writeEventsRequest.Events = new List<EventContents>();
				while (this.eventsRequests.Count > 0 && writeEventsRequest.Events.Count < 10)
				{
					EventContents eventContents = this.eventsRequests.Dequeue();
					writeEventsRequest.Events.Add(eventContents);
				}
				if (writeEventsRequest.Events.Count > 0)
				{
					this.eventApi.WriteEvents(writeEventsRequest, new Action<WriteEventsResponse>(this.EventSentSuccessfulCallback), new Action<PlayFabError>(this.EventSentErrorCallback), null, null);
				}
				this.isSending = false;
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000131B5 File Offset: 0x000113B5
		private void EventSentSuccessfulCallback(WriteEventsResponse response)
		{
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000131B7 File Offset: 0x000113B7
		private void EventSentErrorCallback(PlayFabError response)
		{
			Debug.LogWarning("Failed to send session data. Error: " + response.GenerateErrorReport());
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000131CE File Offset: 0x000113CE
		public void OnEnable()
		{
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000131D0 File Offset: 0x000113D0
		public void OnDisable()
		{
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000131D2 File Offset: 0x000113D2
		public void OnDestroy()
		{
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000131D4 File Offset: 0x000113D4
		public void OnApplicationQuit()
		{
			this.Send();
		}

		// Token: 0x040006FB RID: 1787
		private Guid focusId;

		// Token: 0x040006FC RID: 1788
		private Guid gameSessionID;

		// Token: 0x040006FD RID: 1789
		private bool initialFocus = true;

		// Token: 0x040006FE RID: 1790
		private bool isSending;

		// Token: 0x040006FF RID: 1791
		private DateTime focusOffDateTime = DateTime.UtcNow;

		// Token: 0x04000700 RID: 1792
		private DateTime focusOnDateTime = DateTime.UtcNow;

		// Token: 0x04000701 RID: 1793
		private Queue<EventContents> eventsRequests = new Queue<EventContents>();

		// Token: 0x04000702 RID: 1794
		private EntityKey entityKey = new EntityKey();

		// Token: 0x04000703 RID: 1795
		private const string eventNamespace = "com.playfab.events.sessions";

		// Token: 0x04000704 RID: 1796
		private const int maxBatchSizeInEvents = 10;

		// Token: 0x04000705 RID: 1797
		private PlayFabEventsInstanceAPI eventApi;
	}
}
