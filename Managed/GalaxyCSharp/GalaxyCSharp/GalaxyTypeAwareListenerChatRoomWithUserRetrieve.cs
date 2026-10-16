using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200001D RID: 29
	public abstract class GalaxyTypeAwareListenerChatRoomWithUserRetrieve : IGalaxyListener
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x000038F8 File Offset: 0x00001AF8
		internal GalaxyTypeAwareListenerChatRoomWithUserRetrieve(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.GalaxyTypeAwareListenerChatRoomWithUserRetrieve_SWIGUpcast(cPtr), cMemoryOwn)
		{
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00003914 File Offset: 0x00001B14
		public GalaxyTypeAwareListenerChatRoomWithUserRetrieve()
			: this(GalaxyInstancePINVOKE.new_GalaxyTypeAwareListenerChatRoomWithUserRetrieve(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00003932 File Offset: 0x00001B32
		internal static HandleRef getCPtr(GalaxyTypeAwareListenerChatRoomWithUserRetrieve obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00003950 File Offset: 0x00001B50
		~GalaxyTypeAwareListenerChatRoomWithUserRetrieve()
		{
			this.Dispose();
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00003980 File Offset: 0x00001B80
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_GalaxyTypeAwareListenerChatRoomWithUserRetrieve(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00003A08 File Offset: 0x00001C08
		public static ListenerType GetListenerType()
		{
			ListenerType listenerType = (ListenerType)GalaxyInstancePINVOKE.GalaxyTypeAwareListenerChatRoomWithUserRetrieve_GetListenerType();
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return listenerType;
		}

		// Token: 0x04000031 RID: 49
		private HandleRef swigCPtr;
	}
}
