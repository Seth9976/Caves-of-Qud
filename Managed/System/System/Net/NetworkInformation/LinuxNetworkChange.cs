using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000719 RID: 1817
	internal sealed class LinuxNetworkChange : INetworkChange, IDisposable
	{
		// Token: 0x14000073 RID: 115
		// (add) Token: 0x060039FE RID: 14846 RVA: 0x000C9507 File Offset: 0x000C7707
		// (remove) Token: 0x060039FF RID: 14847 RVA: 0x000C9510 File Offset: 0x000C7710
		public event NetworkAddressChangedEventHandler NetworkAddressChanged
		{
			add
			{
				this.Register(value);
			}
			remove
			{
				this.Unregister(value);
			}
		}

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06003A00 RID: 14848 RVA: 0x000C9519 File Offset: 0x000C7719
		// (remove) Token: 0x06003A01 RID: 14849 RVA: 0x000C9522 File Offset: 0x000C7722
		public event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged
		{
			add
			{
				this.Register(value);
			}
			remove
			{
				this.Unregister(value);
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06003A02 RID: 14850 RVA: 0x000C952B File Offset: 0x000C772B
		public bool HasRegisteredEvents
		{
			get
			{
				return this.AddressChanged != null || this.AvailabilityChanged != null;
			}
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x00003917 File Offset: 0x00001B17
		public void Dispose()
		{
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000C9540 File Offset: 0x000C7740
		private bool EnsureSocket()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this.nl_sock != null)
				{
					return true;
				}
				IntPtr intPtr = LinuxNetworkChange.CreateNLSocket();
				if (intPtr.ToInt64() == -1L)
				{
					return false;
				}
				SafeSocketHandle safeSocketHandle = new SafeSocketHandle(intPtr, true);
				this.nl_sock = new Socket(AddressFamily.Unspecified, SocketType.Raw, ProtocolType.Udp, safeSocketHandle);
				this.nl_args = new SocketAsyncEventArgs();
				this.nl_args.SetBuffer(new byte[8192], 0, 8192);
				this.nl_args.Completed += this.OnDataAvailable;
				this.nl_sock.ReceiveAsync(this.nl_args);
			}
			return true;
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x000C960C File Offset: 0x000C780C
		private void MaybeCloseSocket()
		{
			if (this.nl_sock == null || this.AvailabilityChanged != null || this.AddressChanged != null)
			{
				return;
			}
			LinuxNetworkChange.CloseNLSocket(this.nl_sock.Handle);
			GC.SuppressFinalize(this.nl_sock);
			this.nl_sock = null;
			this.nl_args = null;
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x000C965C File Offset: 0x000C785C
		private bool GetAvailability()
		{
			foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback && networkInterface.OperationalStatus == OperationalStatus.Up)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x000C9698 File Offset: 0x000C7898
		private void OnAvailabilityChanged(object unused)
		{
			NetworkAvailabilityChangedEventHandler availabilityChanged = this.AvailabilityChanged;
			if (availabilityChanged != null)
			{
				availabilityChanged(null, new NetworkAvailabilityEventArgs(this.GetAvailability()));
			}
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x000C96C4 File Offset: 0x000C78C4
		private void OnAddressChanged(object unused)
		{
			NetworkAddressChangedEventHandler addressChanged = this.AddressChanged;
			if (addressChanged != null)
			{
				addressChanged(null, EventArgs.Empty);
			}
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x000C96E8 File Offset: 0x000C78E8
		private void OnEventDue(object unused)
		{
			object @lock = this._lock;
			LinuxNetworkChange.EventType eventType;
			lock (@lock)
			{
				eventType = this.pending_events;
				this.pending_events = (LinuxNetworkChange.EventType)0;
				this.timer.Change(-1, -1);
			}
			if ((eventType & LinuxNetworkChange.EventType.Availability) != (LinuxNetworkChange.EventType)0)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.OnAvailabilityChanged));
			}
			if ((eventType & LinuxNetworkChange.EventType.Address) != (LinuxNetworkChange.EventType)0)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.OnAddressChanged));
			}
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x000C976C File Offset: 0x000C796C
		private void QueueEvent(LinuxNetworkChange.EventType type)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this.timer == null)
				{
					this.timer = new Timer(new TimerCallback(this.OnEventDue));
				}
				if (this.pending_events == (LinuxNetworkChange.EventType)0)
				{
					this.timer.Change(150, -1);
				}
				this.pending_events |= type;
			}
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x000C97F0 File Offset: 0x000C79F0
		private unsafe void OnDataAvailable(object sender, SocketAsyncEventArgs args)
		{
			if (this.nl_sock == null)
			{
				return;
			}
			byte[] array;
			byte* ptr;
			if ((array = args.Buffer) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			LinuxNetworkChange.EventType eventType = LinuxNetworkChange.ReadEvents(this.nl_sock.Handle, new IntPtr((void*)ptr), args.BytesTransferred, 8192);
			array = null;
			this.nl_sock.ReceiveAsync(this.nl_args);
			if (eventType != (LinuxNetworkChange.EventType)0)
			{
				this.QueueEvent(eventType);
			}
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x000C9863 File Offset: 0x000C7A63
		private void Register(NetworkAddressChangedEventHandler d)
		{
			this.EnsureSocket();
			this.AddressChanged = (NetworkAddressChangedEventHandler)Delegate.Combine(this.AddressChanged, d);
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x000C9883 File Offset: 0x000C7A83
		private void Register(NetworkAvailabilityChangedEventHandler d)
		{
			this.EnsureSocket();
			this.AvailabilityChanged = (NetworkAvailabilityChangedEventHandler)Delegate.Combine(this.AvailabilityChanged, d);
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x000C98A4 File Offset: 0x000C7AA4
		private void Unregister(NetworkAddressChangedEventHandler d)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this.AddressChanged = (NetworkAddressChangedEventHandler)Delegate.Remove(this.AddressChanged, d);
				this.MaybeCloseSocket();
			}
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x000C98FC File Offset: 0x000C7AFC
		private void Unregister(NetworkAvailabilityChangedEventHandler d)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this.AvailabilityChanged = (NetworkAvailabilityChangedEventHandler)Delegate.Remove(this.AvailabilityChanged, d);
				this.MaybeCloseSocket();
			}
		}

		// Token: 0x06003A10 RID: 14864
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr CreateNLSocket();

		// Token: 0x06003A11 RID: 14865
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern LinuxNetworkChange.EventType ReadEvents(IntPtr sock, IntPtr buffer, int count, int size);

		// Token: 0x06003A12 RID: 14866
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr CloseNLSocket(IntPtr sock);

		// Token: 0x04002240 RID: 8768
		private object _lock = new object();

		// Token: 0x04002241 RID: 8769
		private Socket nl_sock;

		// Token: 0x04002242 RID: 8770
		private SocketAsyncEventArgs nl_args;

		// Token: 0x04002243 RID: 8771
		private LinuxNetworkChange.EventType pending_events;

		// Token: 0x04002244 RID: 8772
		private Timer timer;

		// Token: 0x04002245 RID: 8773
		private NetworkAddressChangedEventHandler AddressChanged;

		// Token: 0x04002246 RID: 8774
		private NetworkAvailabilityChangedEventHandler AvailabilityChanged;

		// Token: 0x0200071A RID: 1818
		[Flags]
		private enum EventType
		{
			// Token: 0x04002248 RID: 8776
			Availability = 1,
			// Token: 0x04002249 RID: 8777
			Address = 2
		}
	}
}
