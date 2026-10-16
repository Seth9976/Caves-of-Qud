using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000068 RID: 104
	public abstract class GameServerGlobalUserInformationRetrieveListener : IUserInformationRetrieveListener
	{
		// Token: 0x060006F7 RID: 1783 RVA: 0x0000C060 File Offset: 0x0000A260
		internal GameServerGlobalUserInformationRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GameServerGlobalUserInformationRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			GameServerGlobalUserInformationRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0000C088 File Offset: 0x0000A288
		public GameServerGlobalUserInformationRetrieveListener()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Register(GalaxyTypeAwareListenerUserInformationRetrieve.GetListenerType(), this);
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0000C0AA File Offset: 0x0000A2AA
		internal static HandleRef getCPtr(GameServerGlobalUserInformationRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		~GameServerGlobalUserInformationRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		public override void Dispose()
		{
			if (GalaxyInstance.GameServerListenerRegistrar() != null)
			{
				GalaxyInstance.GameServerListenerRegistrar().Unregister(GalaxyTypeAwareListenerUserInformationRetrieve.GetListenerType(), this);
			}
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GameServerGlobalUserInformationRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (GameServerGlobalUserInformationRetrieveListener.listeners.ContainsKey(handle))
					{
						GameServerGlobalUserInformationRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0400007C RID: 124
		private static Dictionary<IntPtr, GameServerGlobalUserInformationRetrieveListener> listeners = new Dictionary<IntPtr, GameServerGlobalUserInformationRetrieveListener>();

		// Token: 0x0400007D RID: 125
		private HandleRef swigCPtr;
	}
}
