using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200000D RID: 13
	public class ISteamMatchmakingRulesResponse
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00003088 File Offset: 0x00001288
		public ISteamMatchmakingRulesResponse(ISteamMatchmakingRulesResponse.RulesResponded onRulesResponded, ISteamMatchmakingRulesResponse.RulesFailedToRespond onRulesFailedToRespond, ISteamMatchmakingRulesResponse.RulesRefreshComplete onRulesRefreshComplete)
		{
			if (onRulesResponded == null || onRulesFailedToRespond == null || onRulesRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_RulesResponded = onRulesResponded;
			this.m_RulesFailedToRespond = onRulesFailedToRespond;
			this.m_RulesRefreshComplete = onRulesRefreshComplete;
			this.m_VTable = new ISteamMatchmakingRulesResponse.VTable
			{
				m_VTRulesResponded = new ISteamMatchmakingRulesResponse.InternalRulesResponded(this.InternalOnRulesResponded),
				m_VTRulesFailedToRespond = new ISteamMatchmakingRulesResponse.InternalRulesFailedToRespond(this.InternalOnRulesFailedToRespond),
				m_VTRulesRefreshComplete = new ISteamMatchmakingRulesResponse.InternalRulesRefreshComplete(this.InternalOnRulesRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingRulesResponse.VTable)));
			Marshal.StructureToPtr<ISteamMatchmakingRulesResponse.VTable>(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003144 File Offset: 0x00001344
		~ISteamMatchmakingRulesResponse()
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

		// Token: 0x06000049 RID: 73 RVA: 0x000031A0 File Offset: 0x000013A0
		private void InternalOnRulesResponded(IntPtr thisptr, IntPtr pchRule, IntPtr pchValue)
		{
			this.m_RulesResponded(InteropHelp.PtrToStringUTF8(pchRule), InteropHelp.PtrToStringUTF8(pchValue));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000031B9 File Offset: 0x000013B9
		private void InternalOnRulesFailedToRespond(IntPtr thisptr)
		{
			this.m_RulesFailedToRespond();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000031C6 File Offset: 0x000013C6
		private void InternalOnRulesRefreshComplete(IntPtr thisptr)
		{
			this.m_RulesRefreshComplete();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000031D3 File Offset: 0x000013D3
		public static explicit operator IntPtr(ISteamMatchmakingRulesResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000020 RID: 32
		private ISteamMatchmakingRulesResponse.VTable m_VTable;

		// Token: 0x04000021 RID: 33
		private IntPtr m_pVTable;

		// Token: 0x04000022 RID: 34
		private GCHandle m_pGCHandle;

		// Token: 0x04000023 RID: 35
		private ISteamMatchmakingRulesResponse.RulesResponded m_RulesResponded;

		// Token: 0x04000024 RID: 36
		private ISteamMatchmakingRulesResponse.RulesFailedToRespond m_RulesFailedToRespond;

		// Token: 0x04000025 RID: 37
		private ISteamMatchmakingRulesResponse.RulesRefreshComplete m_RulesRefreshComplete;

		// Token: 0x020001E8 RID: 488
		// (Invoke) Token: 0x06000BEC RID: 3052
		public delegate void RulesResponded(string pchRule, string pchValue);

		// Token: 0x020001E9 RID: 489
		// (Invoke) Token: 0x06000BF0 RID: 3056
		public delegate void RulesFailedToRespond();

		// Token: 0x020001EA RID: 490
		// (Invoke) Token: 0x06000BF4 RID: 3060
		public delegate void RulesRefreshComplete();

		// Token: 0x020001EB RID: 491
		// (Invoke) Token: 0x06000BF8 RID: 3064
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void InternalRulesResponded(IntPtr thisptr, IntPtr pchRule, IntPtr pchValue);

		// Token: 0x020001EC RID: 492
		// (Invoke) Token: 0x06000BFC RID: 3068
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void InternalRulesFailedToRespond(IntPtr thisptr);

		// Token: 0x020001ED RID: 493
		// (Invoke) Token: 0x06000C00 RID: 3072
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		public delegate void InternalRulesRefreshComplete(IntPtr thisptr);

		// Token: 0x020001EE RID: 494
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x04000B56 RID: 2902
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesResponded m_VTRulesResponded;

			// Token: 0x04000B57 RID: 2903
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesFailedToRespond m_VTRulesFailedToRespond;

			// Token: 0x04000B58 RID: 2904
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesRefreshComplete m_VTRulesRefreshComplete;
		}
	}
}
