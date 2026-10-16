using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000045 RID: 69
	public sealed class InputRemoting : IObservable<InputRemoting.Message>, IObserver<InputRemoting.Message>
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0002D726 File Offset: 0x0002B926
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x0002D733 File Offset: 0x0002B933
		public bool sending
		{
			get
			{
				return (this.m_Flags & InputRemoting.Flags.Sending) == InputRemoting.Flags.Sending;
			}
			private set
			{
				if (value)
				{
					this.m_Flags |= InputRemoting.Flags.Sending;
					return;
				}
				this.m_Flags &= ~InputRemoting.Flags.Sending;
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0002D756 File Offset: 0x0002B956
		internal InputRemoting(InputManager manager, bool startSendingOnConnect = false)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			this.m_LocalManager = manager;
			if (startSendingOnConnect)
			{
				this.m_Flags |= InputRemoting.Flags.StartSendingOnConnect;
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0002D784 File Offset: 0x0002B984
		public void StartSending()
		{
			if (this.sending)
			{
				return;
			}
			this.m_LocalManager.onEvent += this.SendEvent;
			this.m_LocalManager.onDeviceChange += this.SendDeviceChange;
			this.m_LocalManager.onLayoutChange += this.SendLayoutChange;
			this.sending = true;
			this.SendInitialMessages();
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0002D7EC File Offset: 0x0002B9EC
		public void StopSending()
		{
			if (!this.sending)
			{
				return;
			}
			this.m_LocalManager.onEvent -= this.SendEvent;
			this.m_LocalManager.onDeviceChange -= this.SendDeviceChange;
			this.m_LocalManager.onLayoutChange -= this.SendLayoutChange;
			this.sending = false;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0002D850 File Offset: 0x0002BA50
		void IObserver<InputRemoting.Message>.OnNext(InputRemoting.Message msg)
		{
			switch (msg.type)
			{
			case InputRemoting.MessageType.Connect:
				InputRemoting.ConnectMsg.Process(this);
				return;
			case InputRemoting.MessageType.Disconnect:
				InputRemoting.DisconnectMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.NewLayout:
				InputRemoting.NewLayoutMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.NewDevice:
				InputRemoting.NewDeviceMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.NewEvents:
				InputRemoting.NewEventsMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.RemoveDevice:
				InputRemoting.RemoveDeviceMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.RemoveLayout:
				break;
			case InputRemoting.MessageType.ChangeUsages:
				InputRemoting.ChangeUsageMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.StartSending:
				InputRemoting.StartSendingMsg.Process(this);
				return;
			case InputRemoting.MessageType.StopSending:
				InputRemoting.StopSendingMsg.Process(this);
				break;
			default:
				return;
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0002D8D7 File Offset: 0x0002BAD7
		void IObserver<InputRemoting.Message>.OnError(Exception error)
		{
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0002D8D9 File Offset: 0x0002BAD9
		void IObserver<InputRemoting.Message>.OnCompleted()
		{
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0002D8DC File Offset: 0x0002BADC
		public IDisposable Subscribe(IObserver<InputRemoting.Message> observer)
		{
			if (observer == null)
			{
				throw new ArgumentNullException("observer");
			}
			InputRemoting.Subscriber subscriber = new InputRemoting.Subscriber
			{
				owner = this,
				observer = observer
			};
			ArrayHelpers.Append<InputRemoting.Subscriber>(ref this.m_Subscribers, subscriber);
			return subscriber;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0002D919 File Offset: 0x0002BB19
		private void SendInitialMessages()
		{
			this.SendAllGeneratedLayouts();
			this.SendAllDevices();
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0002D928 File Offset: 0x0002BB28
		private void SendAllGeneratedLayouts()
		{
			foreach (KeyValuePair<InternedString, Func<InputControlLayout>> keyValuePair in this.m_LocalManager.m_Layouts.layoutBuilders)
			{
				this.SendLayout(keyValuePair.Key);
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0002D990 File Offset: 0x0002BB90
		private void SendLayout(string layoutName)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			InputRemoting.Message? message = InputRemoting.NewLayoutMsg.Create(this, layoutName);
			if (message != null)
			{
				this.Send(message.Value);
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0002D9C4 File Offset: 0x0002BBC4
		private void SendAllDevices()
		{
			foreach (InputDevice inputDevice in this.m_LocalManager.devices)
			{
				this.SendDevice(inputDevice);
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002DA20 File Offset: 0x0002BC20
		private void SendDevice(InputDevice device)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			if (device.remote)
			{
				return;
			}
			InputRemoting.Message message = InputRemoting.NewDeviceMsg.Create(device);
			this.Send(message);
			InputRemoting.Message message2 = InputRemoting.NewEventsMsg.CreateStateEvent(device);
			this.Send(message2);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002DA5C File Offset: 0x0002BC5C
		private void SendEvent(InputEventPtr eventPtr, InputDevice device)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			if (device != null && device.remote)
			{
				return;
			}
			InputRemoting.Message message = InputRemoting.NewEventsMsg.Create(eventPtr.data, 1);
			this.Send(message);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0002DA94 File Offset: 0x0002BC94
		private void SendDeviceChange(InputDevice device, InputDeviceChange change)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			if (device.remote)
			{
				return;
			}
			InputRemoting.Message message;
			if (change != InputDeviceChange.Added)
			{
				if (change != InputDeviceChange.Removed)
				{
					switch (change)
					{
					case InputDeviceChange.UsageChanged:
						message = InputRemoting.ChangeUsageMsg.Create(device);
						break;
					case InputDeviceChange.ConfigurationChanged:
						return;
					case InputDeviceChange.SoftReset:
						message = InputRemoting.NewEventsMsg.CreateResetEvent(device, false);
						break;
					case InputDeviceChange.HardReset:
						message = InputRemoting.NewEventsMsg.CreateResetEvent(device, true);
						break;
					default:
						return;
					}
				}
				else
				{
					message = InputRemoting.RemoveDeviceMsg.Create(device);
				}
			}
			else
			{
				message = InputRemoting.NewDeviceMsg.Create(device);
			}
			this.Send(message);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0002DB0C File Offset: 0x0002BD0C
		private void SendLayoutChange(string layout, InputControlLayoutChange change)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			if (!this.m_LocalManager.m_Layouts.IsGeneratedLayout(new InternedString(layout)))
			{
				return;
			}
			if (change != InputControlLayoutChange.Added && change != InputControlLayoutChange.Replaced)
			{
				return;
			}
			InputRemoting.Message? message = InputRemoting.NewLayoutMsg.Create(this, layout);
			if (message != null)
			{
				this.Send(message.Value);
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0002DB64 File Offset: 0x0002BD64
		private void Send(InputRemoting.Message msg)
		{
			InputRemoting.Subscriber[] subscribers = this.m_Subscribers;
			for (int i = 0; i < subscribers.Length; i++)
			{
				subscribers[i].observer.OnNext(msg);
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0002DB94 File Offset: 0x0002BD94
		private int FindOrCreateSenderRecord(int senderId)
		{
			if (this.m_Senders != null)
			{
				int num = this.m_Senders.Length;
				for (int i = 0; i < num; i++)
				{
					if (this.m_Senders[i].senderId == senderId)
					{
						return i;
					}
				}
			}
			InputRemoting.RemoteSender remoteSender = new InputRemoting.RemoteSender
			{
				senderId = senderId
			};
			return ArrayHelpers.Append<InputRemoting.RemoteSender>(ref this.m_Senders, remoteSender);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0002DBF2 File Offset: 0x0002BDF2
		private static InternedString BuildLayoutNamespace(int senderId)
		{
			return new InternedString(string.Format("Remote::{0}", senderId));
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0002DC0C File Offset: 0x0002BE0C
		private int FindLocalDeviceId(int remoteDeviceId, int senderIndex)
		{
			InputRemoting.RemoteInputDevice[] devices = this.m_Senders[senderIndex].devices;
			if (devices != null)
			{
				int num = devices.Length;
				for (int i = 0; i < num; i++)
				{
					if (devices[i].remoteId == remoteDeviceId)
					{
						return devices[i].localId;
					}
				}
			}
			return 0;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0002DC5C File Offset: 0x0002BE5C
		private InputDevice TryGetDeviceByRemoteId(int remoteDeviceId, int senderIndex)
		{
			int num = this.FindLocalDeviceId(remoteDeviceId, senderIndex);
			return this.m_LocalManager.TryGetDeviceById(num);
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0002DC7E File Offset: 0x0002BE7E
		internal InputManager manager
		{
			get
			{
				return this.m_LocalManager;
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0002DC88 File Offset: 0x0002BE88
		public void RemoveRemoteDevices(int participantId)
		{
			int num = this.FindOrCreateSenderRecord(participantId);
			InputRemoting.RemoteInputDevice[] devices = this.m_Senders[num].devices;
			if (devices != null)
			{
				foreach (InputRemoting.RemoteInputDevice remoteInputDevice in devices)
				{
					InputDevice inputDevice = this.m_LocalManager.TryGetDeviceById(remoteInputDevice.localId);
					if (inputDevice != null)
					{
						this.m_LocalManager.RemoveDevice(inputDevice, false);
					}
				}
			}
			ArrayHelpers.EraseAt<InputRemoting.RemoteSender>(ref this.m_Senders, num);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0002DD00 File Offset: 0x0002BF00
		private static byte[] SerializeData<TData>(TData data)
		{
			string text = JsonUtility.ToJson(data);
			return Encoding.UTF8.GetBytes(text);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0002DD24 File Offset: 0x0002BF24
		private static TData DeserializeData<TData>(byte[] data)
		{
			return JsonUtility.FromJson<TData>(Encoding.UTF8.GetString(data));
		}

		// Token: 0x0400024A RID: 586
		private InputRemoting.Flags m_Flags;

		// Token: 0x0400024B RID: 587
		private InputManager m_LocalManager;

		// Token: 0x0400024C RID: 588
		private InputRemoting.Subscriber[] m_Subscribers;

		// Token: 0x0400024D RID: 589
		private InputRemoting.RemoteSender[] m_Senders;

		// Token: 0x0200019E RID: 414
		public enum MessageType
		{
			// Token: 0x04000911 RID: 2321
			Connect,
			// Token: 0x04000912 RID: 2322
			Disconnect,
			// Token: 0x04000913 RID: 2323
			NewLayout,
			// Token: 0x04000914 RID: 2324
			NewDevice,
			// Token: 0x04000915 RID: 2325
			NewEvents,
			// Token: 0x04000916 RID: 2326
			RemoveDevice,
			// Token: 0x04000917 RID: 2327
			RemoveLayout,
			// Token: 0x04000918 RID: 2328
			ChangeUsages,
			// Token: 0x04000919 RID: 2329
			StartSending,
			// Token: 0x0400091A RID: 2330
			StopSending
		}

		// Token: 0x0200019F RID: 415
		public struct Message
		{
			// Token: 0x0400091B RID: 2331
			public int participantId;

			// Token: 0x0400091C RID: 2332
			public InputRemoting.MessageType type;

			// Token: 0x0400091D RID: 2333
			public byte[] data;
		}

		// Token: 0x020001A0 RID: 416
		[Flags]
		private enum Flags
		{
			// Token: 0x0400091F RID: 2335
			Sending = 1,
			// Token: 0x04000920 RID: 2336
			StartSendingOnConnect = 2
		}

		// Token: 0x020001A1 RID: 417
		[Serializable]
		internal struct RemoteSender
		{
			// Token: 0x04000921 RID: 2337
			public int senderId;

			// Token: 0x04000922 RID: 2338
			public InternedString[] layouts;

			// Token: 0x04000923 RID: 2339
			public InputRemoting.RemoteInputDevice[] devices;
		}

		// Token: 0x020001A2 RID: 418
		[Serializable]
		internal struct RemoteInputDevice
		{
			// Token: 0x04000924 RID: 2340
			public int remoteId;

			// Token: 0x04000925 RID: 2341
			public int localId;

			// Token: 0x04000926 RID: 2342
			public InputDeviceDescription description;
		}

		// Token: 0x020001A3 RID: 419
		internal class Subscriber : IDisposable
		{
			// Token: 0x06001440 RID: 5184 RVA: 0x0005E2A9 File Offset: 0x0005C4A9
			public void Dispose()
			{
				ArrayHelpers.Erase<InputRemoting.Subscriber>(ref this.owner.m_Subscribers, this);
			}

			// Token: 0x04000927 RID: 2343
			public InputRemoting owner;

			// Token: 0x04000928 RID: 2344
			public IObserver<InputRemoting.Message> observer;
		}

		// Token: 0x020001A4 RID: 420
		private static class ConnectMsg
		{
			// Token: 0x06001442 RID: 5186 RVA: 0x0005E2C5 File Offset: 0x0005C4C5
			public static void Process(InputRemoting receiver)
			{
				if (receiver.sending)
				{
					receiver.SendInitialMessages();
					return;
				}
				if ((receiver.m_Flags & InputRemoting.Flags.StartSendingOnConnect) == InputRemoting.Flags.StartSendingOnConnect)
				{
					receiver.StartSending();
				}
			}
		}

		// Token: 0x020001A5 RID: 421
		private static class StartSendingMsg
		{
			// Token: 0x06001443 RID: 5187 RVA: 0x0005E2E7 File Offset: 0x0005C4E7
			public static void Process(InputRemoting receiver)
			{
				receiver.StartSending();
			}
		}

		// Token: 0x020001A6 RID: 422
		private static class StopSendingMsg
		{
			// Token: 0x06001444 RID: 5188 RVA: 0x0005E2EF File Offset: 0x0005C4EF
			public static void Process(InputRemoting receiver)
			{
				receiver.StopSending();
			}
		}

		// Token: 0x020001A7 RID: 423
		private static class DisconnectMsg
		{
			// Token: 0x06001445 RID: 5189 RVA: 0x0005E2F7 File Offset: 0x0005C4F7
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				Debug.Log("DisconnectMsg.Process");
				receiver.RemoveRemoteDevices(msg.participantId);
				receiver.StopSending();
			}
		}

		// Token: 0x020001A8 RID: 424
		private static class NewLayoutMsg
		{
			// Token: 0x06001446 RID: 5190 RVA: 0x0005E318 File Offset: 0x0005C518
			public static InputRemoting.Message? Create(InputRemoting sender, string layoutName)
			{
				InputControlLayout inputControlLayout;
				try
				{
					inputControlLayout = sender.m_LocalManager.TryLoadControlLayout(new InternedString(layoutName));
					if (inputControlLayout == null)
					{
						Debug.Log(string.Format("Could not find layout '{0}' meant to be sent through remote connection; this should not happen", layoutName));
						return null;
					}
				}
				catch (Exception ex)
				{
					Debug.Log(string.Format("Could not load layout '{0}'; not sending to remote listeners (exception: {1})", layoutName, ex));
					return null;
				}
				InputRemoting.NewLayoutMsg.Data data = new InputRemoting.NewLayoutMsg.Data
				{
					name = layoutName,
					layoutJson = inputControlLayout.ToJson(),
					isOverride = inputControlLayout.isOverride
				};
				return new InputRemoting.Message?(new InputRemoting.Message
				{
					type = InputRemoting.MessageType.NewLayout,
					data = InputRemoting.SerializeData<InputRemoting.NewLayoutMsg.Data>(data)
				});
			}

			// Token: 0x06001447 RID: 5191 RVA: 0x0005E3DC File Offset: 0x0005C5DC
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				InputRemoting.NewLayoutMsg.Data data = InputRemoting.DeserializeData<InputRemoting.NewLayoutMsg.Data>(msg.data);
				int num = receiver.FindOrCreateSenderRecord(msg.participantId);
				InternedString internedString = new InternedString(data.name);
				receiver.m_LocalManager.RegisterControlLayout(data.layoutJson, data.name, data.isOverride);
				ArrayHelpers.Append<InternedString>(ref receiver.m_Senders[num].layouts, internedString);
			}

			// Token: 0x02000278 RID: 632
			[Serializable]
			public struct Data
			{
				// Token: 0x04000D0C RID: 3340
				public string name;

				// Token: 0x04000D0D RID: 3341
				public string layoutJson;

				// Token: 0x04000D0E RID: 3342
				public bool isOverride;
			}
		}

		// Token: 0x020001A9 RID: 425
		private static class NewDeviceMsg
		{
			// Token: 0x06001448 RID: 5192 RVA: 0x0005E444 File Offset: 0x0005C644
			public static InputRemoting.Message Create(InputDevice device)
			{
				InputRemoting.NewDeviceMsg.Data data = default(InputRemoting.NewDeviceMsg.Data);
				data.name = device.name;
				data.layout = device.layout;
				data.deviceId = device.deviceId;
				data.description = device.description;
				data.usages = device.usages.Select<InternedString, string>((InternedString x) => x.ToString()).ToArray<string>();
				InputRemoting.NewDeviceMsg.Data data2 = data;
				return new InputRemoting.Message
				{
					type = InputRemoting.MessageType.NewDevice,
					data = InputRemoting.SerializeData<InputRemoting.NewDeviceMsg.Data>(data2)
				};
			}

			// Token: 0x06001449 RID: 5193 RVA: 0x0005E4E8 File Offset: 0x0005C6E8
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				int num = receiver.FindOrCreateSenderRecord(msg.participantId);
				InputRemoting.NewDeviceMsg.Data data = InputRemoting.DeserializeData<InputRemoting.NewDeviceMsg.Data>(msg.data);
				InputRemoting.RemoteInputDevice[] devices = receiver.m_Senders[num].devices;
				if (devices != null)
				{
					InputRemoting.RemoteInputDevice[] array = devices;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].remoteId == data.deviceId)
						{
							Debug.LogError(string.Format("Already received device with id {0} (layout '{1}', description '{3}) from remote {2}", new object[] { data.deviceId, data.layout, msg.participantId, data.description }));
							return;
						}
					}
				}
				InputDevice inputDevice;
				try
				{
					InternedString internedString = new InternedString(data.layout);
					inputDevice = receiver.m_LocalManager.AddDevice(internedString, data.name, default(InternedString));
					inputDevice.m_ParticipantId = msg.participantId;
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Format("Could not create remote device '{0}' with layout '{1}' locally (exception: {2})", data.description, data.layout, ex));
					return;
				}
				inputDevice.m_Description = data.description;
				inputDevice.m_DeviceFlags |= InputDevice.DeviceFlags.Remote;
				foreach (string text in data.usages)
				{
					receiver.m_LocalManager.AddDeviceUsage(inputDevice, new InternedString(text));
				}
				InputRemoting.RemoteInputDevice remoteInputDevice = new InputRemoting.RemoteInputDevice
				{
					remoteId = data.deviceId,
					localId = inputDevice.deviceId,
					description = data.description
				};
				ArrayHelpers.Append<InputRemoting.RemoteInputDevice>(ref receiver.m_Senders[num].devices, remoteInputDevice);
			}

			// Token: 0x02000279 RID: 633
			[Serializable]
			public struct Data
			{
				// Token: 0x04000D0F RID: 3343
				public string name;

				// Token: 0x04000D10 RID: 3344
				public string layout;

				// Token: 0x04000D11 RID: 3345
				public int deviceId;

				// Token: 0x04000D12 RID: 3346
				public string[] usages;

				// Token: 0x04000D13 RID: 3347
				public InputDeviceDescription description;
			}
		}

		// Token: 0x020001AA RID: 426
		private static class NewEventsMsg
		{
			// Token: 0x0600144A RID: 5194 RVA: 0x0005E6AC File Offset: 0x0005C8AC
			public unsafe static InputRemoting.Message CreateResetEvent(InputDevice device, bool isHardReset)
			{
				DeviceResetEvent deviceResetEvent = DeviceResetEvent.Create(device.deviceId, isHardReset, -1.0);
				return InputRemoting.NewEventsMsg.Create((InputEvent*)UnsafeUtility.AddressOf<DeviceResetEvent>(ref deviceResetEvent), 1);
			}

			// Token: 0x0600144B RID: 5195 RVA: 0x0005E6DC File Offset: 0x0005C8DC
			public static InputRemoting.Message CreateStateEvent(InputDevice device)
			{
				InputEventPtr inputEventPtr;
				InputRemoting.Message message;
				using (StateEvent.From(device, out inputEventPtr, Allocator.Temp))
				{
					message = InputRemoting.NewEventsMsg.Create(inputEventPtr.data, 1);
				}
				return message;
			}

			// Token: 0x0600144C RID: 5196 RVA: 0x0005E724 File Offset: 0x0005C924
			public unsafe static InputRemoting.Message Create(InputEvent* events, int eventCount)
			{
				uint num = 0U;
				InputEventPtr inputEventPtr = new InputEventPtr(events);
				int i = 0;
				while (i < eventCount)
				{
					num = num.AlignToMultipleOf(4U) + inputEventPtr.sizeInBytes;
					i++;
					inputEventPtr = inputEventPtr.Next();
				}
				byte[] array = new byte[num];
				byte[] array2;
				byte* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				UnsafeUtility.MemCpy((void*)ptr, (void*)events, (long)((ulong)num));
				array2 = null;
				return new InputRemoting.Message
				{
					type = InputRemoting.MessageType.NewEvents,
					data = array
				};
			}

			// Token: 0x0600144D RID: 5197 RVA: 0x0005E7AC File Offset: 0x0005C9AC
			public unsafe static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				InputManager localManager = receiver.m_LocalManager;
				byte[] array;
				byte* ptr;
				if ((array = msg.data) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				IntPtr intPtr = new IntPtr((void*)(ptr + msg.data.Length));
				int num = 0;
				InputEventPtr inputEventPtr = new InputEventPtr((InputEvent*)ptr);
				int num2 = receiver.FindOrCreateSenderRecord(msg.participantId);
				while (inputEventPtr.data < (InputEvent*)intPtr.ToPointer())
				{
					int deviceId = inputEventPtr.deviceId;
					int num3 = receiver.FindLocalDeviceId(deviceId, num2);
					inputEventPtr.deviceId = num3;
					if (num3 != 0)
					{
						localManager.QueueEvent(inputEventPtr);
					}
					num++;
					inputEventPtr = inputEventPtr.Next();
				}
				array = null;
			}
		}

		// Token: 0x020001AB RID: 427
		private static class ChangeUsageMsg
		{
			// Token: 0x0600144E RID: 5198 RVA: 0x0005E854 File Offset: 0x0005CA54
			public static InputRemoting.Message Create(InputDevice device)
			{
				InputRemoting.ChangeUsageMsg.Data data = default(InputRemoting.ChangeUsageMsg.Data);
				data.deviceId = device.deviceId;
				data.usages = device.usages.Select<InternedString, string>((InternedString x) => x.ToString()).ToArray<string>();
				InputRemoting.ChangeUsageMsg.Data data2 = data;
				return new InputRemoting.Message
				{
					type = InputRemoting.MessageType.ChangeUsages,
					data = InputRemoting.SerializeData<InputRemoting.ChangeUsageMsg.Data>(data2)
				};
			}

			// Token: 0x0600144F RID: 5199 RVA: 0x0005E8D4 File Offset: 0x0005CAD4
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				int num = receiver.FindOrCreateSenderRecord(msg.participantId);
				InputRemoting.ChangeUsageMsg.Data data = InputRemoting.DeserializeData<InputRemoting.ChangeUsageMsg.Data>(msg.data);
				InputDevice inputDevice = receiver.TryGetDeviceByRemoteId(data.deviceId, num);
				if (inputDevice != null)
				{
					foreach (InternedString internedString in inputDevice.usages)
					{
						if (!data.usages.Contains(internedString))
						{
							receiver.m_LocalManager.RemoveDeviceUsage(inputDevice, new InternedString(internedString));
						}
					}
					foreach (string text in data.usages)
					{
						InternedString internedString2 = new InternedString(text);
						if (!inputDevice.usages.Contains(internedString2))
						{
							receiver.m_LocalManager.AddDeviceUsage(inputDevice, new InternedString(text));
						}
					}
				}
			}

			// Token: 0x0200027B RID: 635
			[Serializable]
			public struct Data
			{
				// Token: 0x04000D16 RID: 3350
				public int deviceId;

				// Token: 0x04000D17 RID: 3351
				public string[] usages;
			}
		}

		// Token: 0x020001AC RID: 428
		private static class RemoveDeviceMsg
		{
			// Token: 0x06001450 RID: 5200 RVA: 0x0005E9CC File Offset: 0x0005CBCC
			public static InputRemoting.Message Create(InputDevice device)
			{
				return new InputRemoting.Message
				{
					type = InputRemoting.MessageType.RemoveDevice,
					data = BitConverter.GetBytes(device.deviceId)
				};
			}

			// Token: 0x06001451 RID: 5201 RVA: 0x0005E9FC File Offset: 0x0005CBFC
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				int num = receiver.FindOrCreateSenderRecord(msg.participantId);
				int num2 = BitConverter.ToInt32(msg.data, 0);
				InputDevice inputDevice = receiver.TryGetDeviceByRemoteId(num2, num);
				if (inputDevice != null)
				{
					receiver.m_LocalManager.RemoveDevice(inputDevice, false);
				}
			}
		}
	}
}
