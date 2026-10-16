using System;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x0200001F RID: 31
	public sealed class UnixProcess
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00006743 File Offset: 0x00004943
		internal UnixProcess(int pid)
		{
			this.pid = pid;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00006752 File Offset: 0x00004952
		public int Id
		{
			get
			{
				return this.pid;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000675A File Offset: 0x0000495A
		public bool HasExited
		{
			get
			{
				return Syscall.WIFEXITED(this.GetProcessStatus());
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006768 File Offset: 0x00004968
		private int GetProcessStatus()
		{
			int num2;
			int num = Syscall.waitpid(this.pid, out num2, WaitOptions.WNOHANG | WaitOptions.WUNTRACED);
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
			return num;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00006789 File Offset: 0x00004989
		public int ExitCode
		{
			get
			{
				if (!this.HasExited)
				{
					throw new InvalidOperationException(Locale.GetText("Process hasn't exited"));
				}
				return Syscall.WEXITSTATUS(this.GetProcessStatus());
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000067AE File Offset: 0x000049AE
		public bool HasSignaled
		{
			get
			{
				return Syscall.WIFSIGNALED(this.GetProcessStatus());
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000067BB File Offset: 0x000049BB
		public Signum TerminationSignal
		{
			get
			{
				if (!this.HasSignaled)
				{
					throw new InvalidOperationException(Locale.GetText("Process wasn't terminated by a signal"));
				}
				return Syscall.WTERMSIG(this.GetProcessStatus());
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000067E0 File Offset: 0x000049E0
		public bool HasStopped
		{
			get
			{
				return Syscall.WIFSTOPPED(this.GetProcessStatus());
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000067ED File Offset: 0x000049ED
		public Signum StopSignal
		{
			get
			{
				if (!this.HasStopped)
				{
					throw new InvalidOperationException(Locale.GetText("Process isn't stopped"));
				}
				return Syscall.WSTOPSIG(this.GetProcessStatus());
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00006812 File Offset: 0x00004A12
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000681F File Offset: 0x00004A1F
		public int ProcessGroupId
		{
			get
			{
				return Syscall.getpgid(this.pid);
			}
			set
			{
				UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.setpgid(this.pid, value));
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00006832 File Offset: 0x00004A32
		public int SessionId
		{
			get
			{
				int num = Syscall.getsid(this.pid);
				UnixMarshal.ThrowExceptionForLastErrorIf(num);
				return num;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006845 File Offset: 0x00004A45
		public static UnixProcess GetCurrentProcess()
		{
			return new UnixProcess(UnixProcess.GetCurrentProcessId());
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006851 File Offset: 0x00004A51
		public static int GetCurrentProcessId()
		{
			return Syscall.getpid();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006858 File Offset: 0x00004A58
		public void Kill()
		{
			this.Signal(Signum.SIGKILL);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006862 File Offset: 0x00004A62
		[CLSCompliant(false)]
		public void Signal(Signum signal)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.kill(this.pid, signal));
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006878 File Offset: 0x00004A78
		public void WaitForExit()
		{
			int num;
			do
			{
				int num2;
				num = Syscall.waitpid(this.pid, out num2, (WaitOptions)0);
			}
			while (UnixMarshal.ShouldRetrySyscall(num));
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
		}

		// Token: 0x04000083 RID: 131
		private int pid;
	}
}
