using System;
using System.Collections;
using System.Globalization;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Provides trace event data specific to a thread and a process.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022F RID: 559
	public class TraceEventCache
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x00047311 File Offset: 0x00045511
		internal Guid ActivityId
		{
			get
			{
				return Trace.CorrelationManager.ActivityId;
			}
		}

		/// <summary>Gets the call stack for the current thread.</summary>
		/// <returns>A string containing stack trace information. This value can be an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" />
		/// </PermissionSet>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0004731D File Offset: 0x0004551D
		public string Callstack
		{
			get
			{
				if (this.stackTrace == null)
				{
					this.stackTrace = Environment.StackTrace;
				}
				return this.stackTrace;
			}
		}

		/// <summary>Gets the correlation data, contained in a stack. </summary>
		/// <returns>A <see cref="T:System.Collections.Stack" /> containing correlation data.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x00047338 File Offset: 0x00045538
		public Stack LogicalOperationStack
		{
			get
			{
				return Trace.CorrelationManager.LogicalOperationStack;
			}
		}

		/// <summary>Gets the date and time at which the event trace occurred.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> structure whose value is a date and time expressed in Coordinated Universal Time (UTC).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00047344 File Offset: 0x00045544
		public DateTime DateTime
		{
			get
			{
				if (this.dateTime == DateTime.MinValue)
				{
					this.dateTime = DateTime.UtcNow;
				}
				return this.dateTime;
			}
		}

		/// <summary>Gets the unique identifier of the current process.</summary>
		/// <returns>The system-generated unique identifier of the current process.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x00047369 File Offset: 0x00045569
		public int ProcessId
		{
			get
			{
				return TraceEventCache.GetProcessId();
			}
		}

		/// <summary>Gets a unique identifier for the current managed thread.  </summary>
		/// <returns>A string that represents a unique integer identifier for this managed thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00047370 File Offset: 0x00045570
		public string ThreadId
		{
			get
			{
				return TraceEventCache.GetThreadId().ToString(CultureInfo.InvariantCulture);
			}
		}

		/// <summary>Gets the current number of ticks in the timer mechanism.</summary>
		/// <returns>The tick counter value of the underlying timer mechanism.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0004738F File Offset: 0x0004558F
		public long Timestamp
		{
			get
			{
				if (this.timeStamp == -1L)
				{
					this.timeStamp = Stopwatch.GetTimestamp();
				}
				return this.timeStamp;
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x000473AC File Offset: 0x000455AC
		private static void InitProcessInfo()
		{
			if (TraceEventCache.processName == null)
			{
				Process currentProcess = Process.GetCurrentProcess();
				try
				{
					TraceEventCache.processId = currentProcess.Id;
					TraceEventCache.processName = currentProcess.ProcessName;
				}
				finally
				{
					currentProcess.Dispose();
				}
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x000473FC File Offset: 0x000455FC
		internal static int GetProcessId()
		{
			TraceEventCache.InitProcessInfo();
			return TraceEventCache.processId;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0004740A File Offset: 0x0004560A
		internal static string GetProcessName()
		{
			TraceEventCache.InitProcessInfo();
			return TraceEventCache.processName;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00047418 File Offset: 0x00045618
		internal static int GetThreadId()
		{
			return Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x040009F5 RID: 2549
		private static volatile int processId;

		// Token: 0x040009F6 RID: 2550
		private static volatile string processName;

		// Token: 0x040009F7 RID: 2551
		private long timeStamp = -1L;

		// Token: 0x040009F8 RID: 2552
		private DateTime dateTime = DateTime.MinValue;

		// Token: 0x040009F9 RID: 2553
		private string stackTrace;
	}
}
