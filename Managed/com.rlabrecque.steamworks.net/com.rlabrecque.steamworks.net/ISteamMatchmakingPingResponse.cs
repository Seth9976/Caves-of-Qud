using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200000B RID: 11
	public class ISteamMatchmakingPingResponse
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002E0C File Offset: 0x0000100C
		public ISteamMatchmakingPingResponse(ISteamMatchmakingPingResponse.ServerResponded onServerResponded, ISteamMatchmakingPingResponse.ServerFailedToRespond onServerFailedToRespond)
		{
			if (onServerResponded == null || onServerFailedToRespond == null)
			{
				throw new ArgumentNullException();
			}
			this.m_ServerResponded = onServerResponded;
			this.m_ServerFailedToRespond = onServerFailedToRespond;
			this.m_VTable = new ISteamMatchmakingPingResponse.VTable
			{
				m_VTServerResponded = new ISteamMatchmakingPingResponse.InternalServerResponded(this.InternalOnServerResponded),
				m_VTServerFailedToRespond = new ISteamMatchmakingPingResponse.InternalServerFailedToRespond(this.InternalOnServerFailedToRespond)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingPingResponse.VTable)));
			Marshal.StructureToPtr<ISteamMatchmakingPingResponse.VTable>(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002EAC File Offset: 0x000010AC
		~ISteamMatchmakingPingResponse()
		{
			if (this.m_pVTable != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pVTable);
			}
			if (this.m_pGCHandle.IsAllocated)
			{
				this.m_pGCHandle.Free();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002F08 File Offset: 0x00001108
		private void InternalOnServerResponded(IntPtr thisptr, gameserveritem_t server)
		{
			this.m_ServerResponded(server);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F16 File Offset: 0x00001116
		private void InternalOnServerFailedToRespond(IntPtr thisptr)
		{
			this.m_ServerFailedToRespond();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F23 File Offset: 0x00001123
		public static explicit operator IntPtr(ISteamMatchmakingPingResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000015 RID: 21
		private ISteamMatchmakingPingResponse.VTable m_VTable;

		// Token: 0x04000016 RID: 22
		private IntPtr m_pVTable;

		// Token: 0x04000017 RID: 23
		private GCHandle m_pGCHandle;

		// Token: 0x04000018 RID: 24
		private ISteamMatchmakingPingResponse.ServerResponded m_ServerResponded;

		// Token: 0x04000019 RID: 25
		private ISteamMatchmakingPingResponse.ServerFailedToRespond m_ServerFailedToRespond;

		// Token: 0x020001DC RID: 476
		// (Invoke) Token: 0x06000BC2 RID: 3010
		public delegate void ServerResponded(gameserveritem_t server);

		// Token: 0x020001DD RID: 477
		// (Invoke) Token: 0x06000BC6 RID: 3014
		public delegate void ServerFailedToRespond();

		// Token: 0x020001DE RID: 478
		// (Invoke) Token: 0x06000BCA RID: 3018
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		private delegate void InternalServerResponded(IntPtr thisptr, gameserveritem_t server);

		// Token: 0x020001DF RID: 479
		// (Invoke) Token: 0x06000BCE RID: 3022
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		private delegate void InternalServerFailedToRespond(IntPtr thisptr);

		// Token: 0x020001E0 RID: 480
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x04000B51 RID: 2897
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPingResponse.InternalServerResponded m_VTServerResponded;

			// Token: 0x04000B52 RID: 2898
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPingResponse.InternalServerFailedToRespond m_VTServerFailedToRespond;
		}
	}
}
