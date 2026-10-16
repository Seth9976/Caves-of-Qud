using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000184 RID: 388
	public class IUtils : IDisposable
	{
		// Token: 0x06000E32 RID: 3634 RVA: 0x0001C36F File Offset: 0x0001A56F
		internal IUtils(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0001C38B File Offset: 0x0001A58B
		internal static HandleRef getCPtr(IUtils obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0001C3AC File Offset: 0x0001A5AC
		~IUtils()
		{
			this.Dispose();
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0001C3DC File Offset: 0x0001A5DC
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUtils(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0001C45C File Offset: 0x0001A65C
		public virtual void GetImageSize(uint imageID, ref int width, ref int height)
		{
			GalaxyInstancePINVOKE.IUtils_GetImageSize(this.swigCPtr, imageID, ref width, ref height);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0001C47C File Offset: 0x0001A67C
		public virtual void GetImageRGBA(uint imageID, byte[] buffer, uint bufferLength)
		{
			GalaxyInstancePINVOKE.IUtils_GetImageRGBA(this.swigCPtr, imageID, buffer, bufferLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0001C49C File Offset: 0x0001A69C
		public virtual void RegisterForNotification(string type)
		{
			GalaxyInstancePINVOKE.IUtils_RegisterForNotification(this.swigCPtr, type);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0001C4BC File Offset: 0x0001A6BC
		public virtual uint GetNotification(ulong notificationID, ref bool consumable, ref byte[] type, uint typeLength, byte[] content, uint contentSize)
		{
			uint num = GalaxyInstancePINVOKE.IUtils_GetNotification(this.swigCPtr, notificationID, ref consumable, type, typeLength, content, contentSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0001C4F0 File Offset: 0x0001A6F0
		public virtual void ShowOverlayWithWebPage(string url)
		{
			GalaxyInstancePINVOKE.IUtils_ShowOverlayWithWebPage(this.swigCPtr, url);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0001C510 File Offset: 0x0001A710
		public virtual bool IsOverlayVisible()
		{
			bool flag = GalaxyInstancePINVOKE.IUtils_IsOverlayVisible(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0001C53C File Offset: 0x0001A73C
		public virtual OverlayState GetOverlayState()
		{
			OverlayState overlayState = (OverlayState)GalaxyInstancePINVOKE.IUtils_GetOverlayState(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return overlayState;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0001C566 File Offset: 0x0001A766
		public virtual void DisableOverlayPopups(string popupGroup)
		{
			GalaxyInstancePINVOKE.IUtils_DisableOverlayPopups(this.swigCPtr, popupGroup);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0001C584 File Offset: 0x0001A784
		public virtual GogServicesConnectionState GetGogServicesConnectionState()
		{
			GogServicesConnectionState gogServicesConnectionState = (GogServicesConnectionState)GalaxyInstancePINVOKE.IUtils_GetGogServicesConnectionState(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return gogServicesConnectionState;
		}

		// Token: 0x040002F0 RID: 752
		private HandleRef swigCPtr;

		// Token: 0x040002F1 RID: 753
		protected bool swigCMemOwn;
	}
}
