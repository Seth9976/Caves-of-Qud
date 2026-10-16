using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000005 RID: 5
	public sealed class Callback<T> : Callback, IDisposable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000013 RID: 19 RVA: 0x00002848 File Offset: 0x00000A48
		// (remove) Token: 0x06000014 RID: 20 RVA: 0x00002880 File Offset: 0x00000A80
		private event Callback<T>.DispatchDelegate m_Func;

		// Token: 0x06000015 RID: 21 RVA: 0x000028B5 File Offset: 0x00000AB5
		public static Callback<T> Create(Callback<T>.DispatchDelegate func)
		{
			return new Callback<T>(func, false);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000028BE File Offset: 0x00000ABE
		public static Callback<T> CreateGameServer(Callback<T>.DispatchDelegate func)
		{
			return new Callback<T>(func, true);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000028C7 File Offset: 0x00000AC7
		public Callback(Callback<T>.DispatchDelegate func, bool bGameServer = false)
		{
			this.m_bGameServer = bGameServer;
			this.Register(func);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000028E0 File Offset: 0x00000AE0
		~Callback()
		{
			this.Dispose();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000290C File Offset: 0x00000B0C
		public void Dispose()
		{
			if (this.m_bDisposed)
			{
				return;
			}
			GC.SuppressFinalize(this);
			if (this.m_bIsRegistered)
			{
				this.Unregister();
			}
			this.m_bDisposed = true;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002932 File Offset: 0x00000B32
		public void Register(Callback<T>.DispatchDelegate func)
		{
			if (func == null)
			{
				throw new Exception("Callback function must not be null.");
			}
			if (this.m_bIsRegistered)
			{
				this.Unregister();
			}
			this.m_Func = func;
			CallbackDispatcher.Register(this);
			this.m_bIsRegistered = true;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002964 File Offset: 0x00000B64
		public void Unregister()
		{
			CallbackDispatcher.Unregister(this);
			this.m_bIsRegistered = false;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002973 File Offset: 0x00000B73
		public override bool IsGameServer
		{
			get
			{
				return this.m_bGameServer;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000297B File Offset: 0x00000B7B
		internal override Type GetCallbackType()
		{
			return typeof(T);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002988 File Offset: 0x00000B88
		internal override void OnRunCallback(IntPtr pvParam)
		{
			try
			{
				this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))));
			}
			catch (Exception ex)
			{
				CallbackDispatcher.ExceptionHandler(ex);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000029D0 File Offset: 0x00000BD0
		internal override void SetUnregistered()
		{
			this.m_bIsRegistered = false;
		}

		// Token: 0x04000008 RID: 8
		private bool m_bGameServer;

		// Token: 0x04000009 RID: 9
		private bool m_bIsRegistered;

		// Token: 0x0400000A RID: 10
		private bool m_bDisposed;

		// Token: 0x020001D3 RID: 467
		// (Invoke) Token: 0x06000BA1 RID: 2977
		public delegate void DispatchDelegate(T param);
	}
}
