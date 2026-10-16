using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x02000435 RID: 1077
	[Serializable]
	internal class PlayerEditorConnectionEvents
	{
		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x00050BF1 File Offset: 0x0004EDF1
		public IReadOnlyList<PlayerEditorConnectionEvents.MessageTypeSubscribers> messageTypeSubscribers
		{
			get
			{
				return this.m_MessageTypeSubscribers;
			}
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x00050BFC File Offset: 0x0004EDFC
		private void BuildLookup()
		{
			bool flag = this.m_SubscriberLookup == null;
			if (flag)
			{
				this.m_SubscriberLookup = new Dictionary<Guid, PlayerEditorConnectionEvents.MessageTypeSubscribers>();
				foreach (PlayerEditorConnectionEvents.MessageTypeSubscribers messageTypeSubscribers in this.messageTypeSubscribers)
				{
					this.m_SubscriberLookup.Add(messageTypeSubscribers.MessageTypeId, messageTypeSubscribers);
				}
			}
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x00050C74 File Offset: 0x0004EE74
		public void InvokeMessageIdSubscribers(Guid messageId, byte[] data, int playerId)
		{
			this.BuildLookup();
			PlayerEditorConnectionEvents.MessageTypeSubscribers messageTypeSubscribers;
			bool flag = !this.m_SubscriberLookup.TryGetValue(messageId, out messageTypeSubscribers);
			if (flag)
			{
				string text = "No actions found for messageId: ";
				Guid guid = messageId;
				Debug.LogError(text + guid.ToString());
			}
			else
			{
				MessageEventArgs e = new MessageEventArgs
				{
					playerId = playerId,
					data = data
				};
				messageTypeSubscribers.messageCallback.Invoke(e);
			}
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x00050CE4 File Offset: 0x0004EEE4
		public UnityEvent<MessageEventArgs> AddAndCreate(Guid messageId)
		{
			this.BuildLookup();
			PlayerEditorConnectionEvents.MessageTypeSubscribers messageTypeSubscribers;
			bool flag = !this.m_SubscriberLookup.TryGetValue(messageId, out messageTypeSubscribers);
			if (flag)
			{
				messageTypeSubscribers = new PlayerEditorConnectionEvents.MessageTypeSubscribers
				{
					MessageTypeId = messageId,
					messageCallback = new PlayerEditorConnectionEvents.MessageEvent()
				};
				this.m_MessageTypeSubscribers.Add(messageTypeSubscribers);
				this.m_SubscriberLookup.Add(messageId, messageTypeSubscribers);
			}
			messageTypeSubscribers.subscriberCount++;
			return messageTypeSubscribers.messageCallback;
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x00050D5C File Offset: 0x0004EF5C
		public void UnregisterManagedCallback(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			this.BuildLookup();
			PlayerEditorConnectionEvents.MessageTypeSubscribers messageTypeSubscribers;
			bool flag = !this.m_SubscriberLookup.TryGetValue(messageId, out messageTypeSubscribers);
			if (!flag)
			{
				messageTypeSubscribers.subscriberCount--;
				messageTypeSubscribers.messageCallback.RemoveListener(callback);
				bool flag2 = messageTypeSubscribers.subscriberCount <= 0;
				if (flag2)
				{
					this.m_MessageTypeSubscribers.Remove(messageTypeSubscribers);
					this.m_SubscriberLookup.Remove(messageId);
				}
			}
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x00050DD0 File Offset: 0x0004EFD0
		public void Clear()
		{
			bool flag = this.m_SubscriberLookup != null;
			if (flag)
			{
				this.m_SubscriberLookup.Clear();
			}
			this.m_MessageTypeSubscribers.Clear();
		}

		// Token: 0x04000BEC RID: 3052
		[SerializeField]
		private List<PlayerEditorConnectionEvents.MessageTypeSubscribers> m_MessageTypeSubscribers = new List<PlayerEditorConnectionEvents.MessageTypeSubscribers>();

		// Token: 0x04000BED RID: 3053
		private Dictionary<Guid, PlayerEditorConnectionEvents.MessageTypeSubscribers> m_SubscriberLookup;

		// Token: 0x04000BEE RID: 3054
		[SerializeField]
		public PlayerEditorConnectionEvents.ConnectionChangeEvent connectionEvent = new PlayerEditorConnectionEvents.ConnectionChangeEvent();

		// Token: 0x04000BEF RID: 3055
		[SerializeField]
		public PlayerEditorConnectionEvents.ConnectionChangeEvent disconnectionEvent = new PlayerEditorConnectionEvents.ConnectionChangeEvent();

		// Token: 0x02000436 RID: 1078
		[Serializable]
		public class MessageEvent : UnityEvent<MessageEventArgs>
		{
		}

		// Token: 0x02000437 RID: 1079
		[Serializable]
		public class ConnectionChangeEvent : UnityEvent<int>
		{
		}

		// Token: 0x02000438 RID: 1080
		[Serializable]
		public class MessageTypeSubscribers
		{
			// Token: 0x17000695 RID: 1685
			// (get) Token: 0x060029E8 RID: 10728 RVA: 0x00050E44 File Offset: 0x0004F044
			// (set) Token: 0x060029E9 RID: 10729 RVA: 0x00050E61 File Offset: 0x0004F061
			public Guid MessageTypeId
			{
				get
				{
					return new Guid(this.m_messageTypeId);
				}
				set
				{
					this.m_messageTypeId = value.ToString();
				}
			}

			// Token: 0x04000BF0 RID: 3056
			[SerializeField]
			private string m_messageTypeId;

			// Token: 0x04000BF1 RID: 3057
			public int subscriberCount = 0;

			// Token: 0x04000BF2 RID: 3058
			public PlayerEditorConnectionEvents.MessageEvent messageCallback = new PlayerEditorConnectionEvents.MessageEvent();
		}
	}
}
