using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x02000431 RID: 1073
	[Serializable]
	public class PlayerConnection : ScriptableObject, IEditorPlayerConnection
	{
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x00050780 File Offset: 0x0004E980
		public static PlayerConnection instance
		{
			get
			{
				bool flag = PlayerConnection.s_Instance == null;
				PlayerConnection playerConnection;
				if (flag)
				{
					playerConnection = PlayerConnection.CreateInstance();
				}
				else
				{
					playerConnection = PlayerConnection.s_Instance;
				}
				return playerConnection;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060029C7 RID: 10695 RVA: 0x000507B0 File Offset: 0x0004E9B0
		public bool isConnected
		{
			get
			{
				return this.GetConnectionNativeApi().IsConnected();
			}
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000507D0 File Offset: 0x0004E9D0
		private static PlayerConnection CreateInstance()
		{
			PlayerConnection.s_Instance = ScriptableObject.CreateInstance<PlayerConnection>();
			PlayerConnection.s_Instance.hideFlags = HideFlags.HideAndDontSave;
			return PlayerConnection.s_Instance;
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x00050800 File Offset: 0x0004EA00
		public void OnEnable()
		{
			bool isInitilized = this.m_IsInitilized;
			if (!isInitilized)
			{
				this.m_IsInitilized = true;
				this.GetConnectionNativeApi().Initialize();
			}
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x00050830 File Offset: 0x0004EA30
		private IPlayerEditorConnectionNative GetConnectionNativeApi()
		{
			return PlayerConnection.connectionNative ?? new PlayerConnectionInternal();
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x00050850 File Offset: 0x0004EA50
		public void Register(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			bool flag2 = !this.m_PlayerEditorConnectionEvents.messageTypeSubscribers.Any<PlayerEditorConnectionEvents.MessageTypeSubscribers>((PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			if (flag2)
			{
				this.GetConnectionNativeApi().RegisterInternal(messageId);
			}
			this.m_PlayerEditorConnectionEvents.AddAndCreate(messageId).AddListener(callback);
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000508E0 File Offset: 0x0004EAE0
		public void Unregister(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			this.m_PlayerEditorConnectionEvents.UnregisterManagedCallback(messageId, callback);
			bool flag = !this.m_PlayerEditorConnectionEvents.messageTypeSubscribers.Any<PlayerEditorConnectionEvents.MessageTypeSubscribers>((PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			if (flag)
			{
				this.GetConnectionNativeApi().UnregisterInternal(messageId);
			}
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x00050948 File Offset: 0x0004EB48
		public void RegisterConnection(UnityAction<int> callback)
		{
			foreach (int num in this.m_connectedPlayers)
			{
				callback(num);
			}
			this.m_PlayerEditorConnectionEvents.connectionEvent.AddListener(callback);
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x000509B4 File Offset: 0x0004EBB4
		public void RegisterDisconnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.disconnectionEvent.AddListener(callback);
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000509C9 File Offset: 0x0004EBC9
		public void UnregisterConnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.connectionEvent.RemoveListener(callback);
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000509DE File Offset: 0x0004EBDE
		public void UnregisterDisconnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.disconnectionEvent.RemoveListener(callback);
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000509F4 File Offset: 0x0004EBF4
		public void Send(Guid messageId, byte[] data)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			this.GetConnectionNativeApi().SendMessage(messageId, data, 0);
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x00050A34 File Offset: 0x0004EC34
		public bool TrySend(Guid messageId, byte[] data)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			return this.GetConnectionNativeApi().TrySendMessage(messageId, data, 0);
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x00050A74 File Offset: 0x0004EC74
		public bool BlockUntilRecvMsg(Guid messageId, int timeout)
		{
			bool msgReceived = false;
			UnityAction<MessageEventArgs> unityAction = delegate(MessageEventArgs args)
			{
				msgReceived = true;
			};
			DateTime now = DateTime.Now;
			this.Register(messageId, unityAction);
			while ((DateTime.Now - now).TotalMilliseconds < (double)timeout && !msgReceived)
			{
				this.GetConnectionNativeApi().Poll();
			}
			this.Unregister(messageId, unityAction);
			return msgReceived;
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x00050AF6 File Offset: 0x0004ECF6
		public void DisconnectAll()
		{
			this.GetConnectionNativeApi().DisconnectAll();
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x00050B08 File Offset: 0x0004ED08
		[RequiredByNativeCode]
		private static void MessageCallbackInternal(IntPtr data, ulong size, ulong guid, string messageId)
		{
			byte[] array = null;
			bool flag = size > 0UL;
			if (flag)
			{
				array = new byte[size];
				Marshal.Copy(data, array, 0, (int)size);
			}
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.InvokeMessageIdSubscribers(new Guid(messageId), array, (int)guid);
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x00050B4F File Offset: 0x0004ED4F
		[RequiredByNativeCode]
		private static void ConnectedCallbackInternal(int playerId)
		{
			PlayerConnection.instance.m_connectedPlayers.Add(playerId);
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.connectionEvent.Invoke(playerId);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x00050B79 File Offset: 0x0004ED79
		[RequiredByNativeCode]
		private static void DisconnectedCallback(int playerId)
		{
			PlayerConnection.instance.m_connectedPlayers.Remove(playerId);
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.disconnectionEvent.Invoke(playerId);
		}

		// Token: 0x04000BE4 RID: 3044
		internal static IPlayerEditorConnectionNative connectionNative;

		// Token: 0x04000BE5 RID: 3045
		[SerializeField]
		private PlayerEditorConnectionEvents m_PlayerEditorConnectionEvents = new PlayerEditorConnectionEvents();

		// Token: 0x04000BE6 RID: 3046
		[SerializeField]
		private List<int> m_connectedPlayers = new List<int>();

		// Token: 0x04000BE7 RID: 3047
		private bool m_IsInitilized;

		// Token: 0x04000BE8 RID: 3048
		private static PlayerConnection s_Instance;
	}
}
