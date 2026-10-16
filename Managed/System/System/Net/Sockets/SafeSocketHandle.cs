using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Sockets
{
	// Token: 0x020007C3 RID: 1987
	internal sealed class SafeSocketHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003F5E RID: 16222 RVA: 0x000D874F File Offset: 0x000D694F
		public SafeSocketHandle(IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
			if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
			{
				this.threads_stacktraces = new Dictionary<Thread, StackTrace>();
			}
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x00013B50 File Offset: 0x00011D50
		internal SafeSocketHandle()
			: base(true)
		{
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x000D8774 File Offset: 0x000D6974
		protected override bool ReleaseHandle()
		{
			int num = 0;
			Socket.Blocking_icall(this.handle, false, out num);
			if (this.blocking_threads != null)
			{
				List<Thread> list = this.blocking_threads;
				lock (list)
				{
					int num2 = 0;
					while (this.blocking_threads.Count > 0)
					{
						if (num2++ >= 10)
						{
							if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
							{
								StringBuilder stringBuilder = new StringBuilder();
								stringBuilder.AppendLine("Could not abort registered blocking threads before closing socket.");
								foreach (Thread thread in this.blocking_threads)
								{
									stringBuilder.AppendLine("Thread StackTrace:");
									stringBuilder.AppendLine(this.threads_stacktraces[thread].ToString());
								}
								stringBuilder.AppendLine();
								throw new Exception(stringBuilder.ToString());
							}
							break;
						}
						else
						{
							if (this.blocking_threads.Count == 1 && this.blocking_threads[0] == Thread.CurrentThread)
							{
								break;
							}
							foreach (Thread thread2 in this.blocking_threads)
							{
								Socket.cancel_blocking_socket_operation(thread2);
							}
							this.in_cleanup = true;
							Monitor.Wait(this.blocking_threads, 100);
						}
					}
				}
			}
			Socket.Close_icall(this.handle, out num);
			return num == 0;
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x000D8934 File Offset: 0x000D6B34
		public void RegisterForBlockingSyscall()
		{
			if (this.blocking_threads == null)
			{
				Interlocked.CompareExchange<List<Thread>>(ref this.blocking_threads, new List<Thread>(), null);
			}
			bool flag = false;
			try
			{
				base.DangerousAddRef(ref flag);
			}
			finally
			{
				List<Thread> list = this.blocking_threads;
				lock (list)
				{
					this.blocking_threads.Add(Thread.CurrentThread);
					if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
					{
						this.threads_stacktraces.Add(Thread.CurrentThread, new StackTrace(true));
					}
				}
				if (flag)
				{
					base.DangerousRelease();
				}
				if (base.IsClosed)
				{
					throw new SocketException(10004);
				}
			}
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x000D89EC File Offset: 0x000D6BEC
		public void UnRegisterForBlockingSyscall()
		{
			List<Thread> list = this.blocking_threads;
			lock (list)
			{
				Thread currentThread = Thread.CurrentThread;
				this.blocking_threads.Remove(currentThread);
				if (SafeSocketHandle.THROW_ON_ABORT_RETRIES && this.blocking_threads.IndexOf(currentThread) == -1)
				{
					this.threads_stacktraces.Remove(currentThread);
				}
				if (this.in_cleanup && this.blocking_threads.Count == 0)
				{
					Monitor.Pulse(this.blocking_threads);
				}
			}
		}

		// Token: 0x040025F6 RID: 9718
		private List<Thread> blocking_threads;

		// Token: 0x040025F7 RID: 9719
		private Dictionary<Thread, StackTrace> threads_stacktraces;

		// Token: 0x040025F8 RID: 9720
		private bool in_cleanup;

		// Token: 0x040025F9 RID: 9721
		private const int SOCKET_CLOSED = 10004;

		// Token: 0x040025FA RID: 9722
		private const int ABORT_RETRIES = 10;

		// Token: 0x040025FB RID: 9723
		private static bool THROW_ON_ABORT_RETRIES = Environment.GetEnvironmentVariable("MONO_TESTS_IN_PROGRESS") == "yes";
	}
}
