using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000D9 RID: 217
	public class ICustomNetworking : IDisposable
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x00017F5E File Offset: 0x0001615E
		internal ICustomNetworking(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00017F7A File Offset: 0x0001617A
		internal static HandleRef getCPtr(ICustomNetworking obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00017F98 File Offset: 0x00016198
		~ICustomNetworking()
		{
			this.Dispose();
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00017FC8 File Offset: 0x000161C8
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ICustomNetworking(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00018048 File Offset: 0x00016248
		public virtual void OpenConnection(string connectionString, IConnectionOpenListener listener)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_OpenConnection__SWIG_0(this.swigCPtr, connectionString, IConnectionOpenListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001806C File Offset: 0x0001626C
		public virtual void OpenConnection(string connectionString)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_OpenConnection__SWIG_1(this.swigCPtr, connectionString);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0001808A File Offset: 0x0001628A
		public virtual void CloseConnection(ulong connectionID, IConnectionCloseListener listener)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_CloseConnection__SWIG_0(this.swigCPtr, connectionID, IConnectionCloseListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x000180AE File Offset: 0x000162AE
		public virtual void CloseConnection(ulong connectionID)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_CloseConnection__SWIG_1(this.swigCPtr, connectionID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000180CC File Offset: 0x000162CC
		public virtual void SendData(ulong connectionID, byte[] data, uint dataSize)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_SendData(this.swigCPtr, connectionID, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000180EC File Offset: 0x000162EC
		public virtual uint GetAvailableDataSize(ulong connectionID)
		{
			uint num = GalaxyInstancePINVOKE.ICustomNetworking_GetAvailableDataSize(this.swigCPtr, connectionID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00018117 File Offset: 0x00016317
		public virtual void PeekData(ulong connectionID, byte[] dest, uint dataSize)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_PeekData(this.swigCPtr, connectionID, dest, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00018137 File Offset: 0x00016337
		public virtual void ReadData(ulong connectionID, byte[] dest, uint dataSize)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_ReadData(this.swigCPtr, connectionID, dest, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00018157 File Offset: 0x00016357
		public virtual void PopData(ulong connectionID, uint dataSize)
		{
			GalaxyInstancePINVOKE.ICustomNetworking_PopData(this.swigCPtr, connectionID, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0400015C RID: 348
		private HandleRef swigCPtr;

		// Token: 0x0400015D RID: 349
		protected bool swigCMemOwn;
	}
}
