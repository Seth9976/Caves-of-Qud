using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000007 RID: 7
	public sealed class CallResult<T> : CallResult, IDisposable
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000024 RID: 36 RVA: 0x000029E4 File Offset: 0x00000BE4
		// (remove) Token: 0x06000025 RID: 37 RVA: 0x00002A1C File Offset: 0x00000C1C
		private event CallResult<T>.APIDispatchDelegate m_Func;

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002A51 File Offset: 0x00000C51
		public SteamAPICall_t Handle
		{
			get
			{
				return this.m_hAPICall;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A59 File Offset: 0x00000C59
		public static CallResult<T> Create(CallResult<T>.APIDispatchDelegate func = null)
		{
			return new CallResult<T>(func);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A61 File Offset: 0x00000C61
		public CallResult(CallResult<T>.APIDispatchDelegate func = null)
		{
			this.m_Func = func;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A7C File Offset: 0x00000C7C
		~CallResult()
		{
			this.Dispose();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public void Dispose()
		{
			if (this.m_bDisposed)
			{
				return;
			}
			GC.SuppressFinalize(this);
			this.Cancel();
			this.m_bDisposed = true;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public void Set(SteamAPICall_t hAPICall, CallResult<T>.APIDispatchDelegate func = null)
		{
			if (func != null)
			{
				this.m_Func = func;
			}
			if (this.m_Func == null)
			{
				throw new Exception("CallResult function was null, you must either set it in the CallResult Constructor or via Set()");
			}
			if (this.m_hAPICall != SteamAPICall_t.Invalid)
			{
				CallbackDispatcher.Unregister(this.m_hAPICall, this);
			}
			this.m_hAPICall = hAPICall;
			if (hAPICall != SteamAPICall_t.Invalid)
			{
				CallbackDispatcher.Register(hAPICall, this);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002B2B File Offset: 0x00000D2B
		public bool IsActive()
		{
			return this.m_hAPICall != SteamAPICall_t.Invalid;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B3D File Offset: 0x00000D3D
		public void Cancel()
		{
			if (this.IsActive())
			{
				CallbackDispatcher.Unregister(this.m_hAPICall, this);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B53 File Offset: 0x00000D53
		internal override Type GetCallbackType()
		{
			return typeof(T);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B60 File Offset: 0x00000D60
		internal override void OnRunCallResult(IntPtr pvParam, bool bFailed, ulong hSteamAPICall_)
		{
			if ((SteamAPICall_t)hSteamAPICall_ == this.m_hAPICall)
			{
				try
				{
					this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))), bFailed);
				}
				catch (Exception ex)
				{
					CallbackDispatcher.ExceptionHandler(ex);
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002BBC File Offset: 0x00000DBC
		internal override void SetUnregistered()
		{
			this.m_hAPICall = SteamAPICall_t.Invalid;
		}

		// Token: 0x0400000C RID: 12
		private SteamAPICall_t m_hAPICall = SteamAPICall_t.Invalid;

		// Token: 0x0400000D RID: 13
		private bool m_bDisposed;

		// Token: 0x020001D4 RID: 468
		// (Invoke) Token: 0x06000BA5 RID: 2981
		public delegate void APIDispatchDelegate(T param, bool bIOFailure);
	}
}
