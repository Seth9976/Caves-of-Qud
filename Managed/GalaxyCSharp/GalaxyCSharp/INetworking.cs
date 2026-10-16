using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200013A RID: 314
	public class INetworking : IDisposable
	{
		// Token: 0x06000BF0 RID: 3056 RVA: 0x00019DA5 File Offset: 0x00017FA5
		internal INetworking(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00019DC1 File Offset: 0x00017FC1
		internal static HandleRef getCPtr(INetworking obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00019DE0 File Offset: 0x00017FE0
		~INetworking()
		{
			this.Dispose();
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00019E10 File Offset: 0x00018010
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_INetworking(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00019E90 File Offset: 0x00018090
		public virtual bool SendP2PPacket(GalaxyID galaxyID, byte[] data, uint dataSize, P2PSendType sendType, byte channel)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_SendP2PPacket__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(galaxyID), data, dataSize, (int)sendType, channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00019EC8 File Offset: 0x000180C8
		public virtual bool SendP2PPacket(GalaxyID galaxyID, byte[] data, uint dataSize, P2PSendType sendType)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_SendP2PPacket__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(galaxyID), data, dataSize, (int)sendType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00019EFC File Offset: 0x000180FC
		public virtual bool PeekP2PPacket(byte[] dest, uint destSize, ref uint outMsgSize, ref GalaxyID outGalaxyID, byte channel)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_PeekP2PPacket__SWIG_0(this.swigCPtr, dest, destSize, ref outMsgSize, GalaxyID.getCPtr(outGalaxyID), channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00019F34 File Offset: 0x00018134
		public virtual bool PeekP2PPacket(byte[] dest, uint destSize, ref uint outMsgSize, ref GalaxyID outGalaxyID)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_PeekP2PPacket__SWIG_1(this.swigCPtr, dest, destSize, ref outMsgSize, GalaxyID.getCPtr(outGalaxyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00019F6C File Offset: 0x0001816C
		public virtual bool IsP2PPacketAvailable(ref uint outMsgSize, byte channel)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_IsP2PPacketAvailable__SWIG_0(this.swigCPtr, ref outMsgSize, channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00019F98 File Offset: 0x00018198
		public virtual bool IsP2PPacketAvailable(ref uint outMsgSize)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_IsP2PPacketAvailable__SWIG_1(this.swigCPtr, ref outMsgSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00019FC4 File Offset: 0x000181C4
		public virtual bool ReadP2PPacket(byte[] dest, uint destSize, ref uint outMsgSize, ref GalaxyID outGalaxyID, byte channel)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_ReadP2PPacket__SWIG_0(this.swigCPtr, dest, destSize, ref outMsgSize, GalaxyID.getCPtr(outGalaxyID), channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00019FFC File Offset: 0x000181FC
		public virtual bool ReadP2PPacket(byte[] dest, uint destSize, ref uint outMsgSize, ref GalaxyID outGalaxyID)
		{
			bool flag = GalaxyInstancePINVOKE.INetworking_ReadP2PPacket__SWIG_1(this.swigCPtr, dest, destSize, ref outMsgSize, GalaxyID.getCPtr(outGalaxyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0001A031 File Offset: 0x00018231
		public virtual void PopP2PPacket(byte channel)
		{
			GalaxyInstancePINVOKE.INetworking_PopP2PPacket__SWIG_0(this.swigCPtr, channel);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0001A04F File Offset: 0x0001824F
		public virtual void PopP2PPacket()
		{
			GalaxyInstancePINVOKE.INetworking_PopP2PPacket__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0001A06C File Offset: 0x0001826C
		public virtual int GetPingWith(GalaxyID galaxyID)
		{
			int num = GalaxyInstancePINVOKE.INetworking_GetPingWith(this.swigCPtr, GalaxyID.getCPtr(galaxyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0001A09C File Offset: 0x0001829C
		public virtual void RequestNatTypeDetection()
		{
			GalaxyInstancePINVOKE.INetworking_RequestNatTypeDetection(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0001A0BC File Offset: 0x000182BC
		public virtual NatType GetNatType()
		{
			NatType natType = (NatType)GalaxyInstancePINVOKE.INetworking_GetNatType(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return natType;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0001A0E8 File Offset: 0x000182E8
		public virtual ConnectionType GetConnectionType(GalaxyID userID)
		{
			ConnectionType connectionType = (ConnectionType)GalaxyInstancePINVOKE.INetworking_GetConnectionType(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return connectionType;
		}

		// Token: 0x0400023D RID: 573
		private HandleRef swigCPtr;

		// Token: 0x0400023E RID: 574
		protected bool swigCMemOwn;
	}
}
