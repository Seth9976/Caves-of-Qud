using System;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Provides information on processes currently executing.</summary>
	// Token: 0x020000CA RID: 202
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ProcessInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ProcessInfo" /> class.</summary>
		// Token: 0x06000AEB RID: 2795 RVA: 0x00002050 File Offset: 0x00000250
		public ProcessInfo()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ProcessInfo" /> class and sets internal information indicating the status of the process.</summary>
		/// <param name="startTime">A <see cref="T:System.DateTime" /> that indicates the time at which the process started. </param>
		/// <param name="age">The <see cref="T:System.TimeSpan" /> that indicates the time elapsed since the process started. </param>
		/// <param name="processID">The ID number assigned to the process. </param>
		/// <param name="requestCount">The number of start requests for the process. </param>
		/// <param name="status">One of the <see cref="T:System.Web.ProcessStatus" /> values that indicates the current status of the process. </param>
		/// <param name="shutdownReason">One of the <see cref="T:System.Web.ProcessShutdownReason" /> values. </param>
		/// <param name="peakMemoryUsed">The maximum memory used, in kilobytes (KB). </param>
		// Token: 0x06000AEC RID: 2796 RVA: 0x0001C47C File Offset: 0x0001A67C
		public ProcessInfo(DateTime startTime, TimeSpan age, int processID, int requestCount, ProcessStatus status, ProcessShutdownReason shutdownReason, int peakMemoryUsed)
		{
			this.age = age;
			this.peakMemoryUsed = peakMemoryUsed;
			this.processID = processID;
			this.requestCount = requestCount;
			this.shutdownReason = shutdownReason;
			this.startTime = startTime;
			this.status = status;
		}

		/// <summary>Gets the length of time the process has been running.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the time elapsed since the process started.</returns>
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0001C4B9 File Offset: 0x0001A6B9
		public TimeSpan Age
		{
			get
			{
				return this.age;
			}
		}

		/// <summary>Gets the maximum amount of memory the process has used.</summary>
		/// <returns>The maximum memory used, in kilobytes (KB).</returns>
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0001C4C1 File Offset: 0x0001A6C1
		public int PeakMemoryUsed
		{
			get
			{
				return this.peakMemoryUsed;
			}
		}

		/// <summary>Gets the ID number assigned to the process.</summary>
		/// <returns>The process ID number assigned by Windows.</returns>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0001C4C9 File Offset: 0x0001A6C9
		public int ProcessID
		{
			get
			{
				return this.processID;
			}
		}

		/// <summary>Gets the number of start requests for the process.</summary>
		/// <returns>The number of requests executed by the process.</returns>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0001C4D1 File Offset: 0x0001A6D1
		public int RequestCount
		{
			get
			{
				return this.requestCount;
			}
		}

		/// <summary>Gets a value that indicates why the process shut down.</summary>
		/// <returns>On of the <see cref="T:System.Web.ProcessShutdownReason" /> values.</returns>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0001C4D9 File Offset: 0x0001A6D9
		public ProcessShutdownReason ShutdownReason
		{
			get
			{
				return this.shutdownReason;
			}
		}

		/// <summary>Gets the time at which the process started.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that indicates the time at which the process started.</returns>
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0001C4E1 File Offset: 0x0001A6E1
		public DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
		}

		/// <summary>Gets the current status of the process.</summary>
		/// <returns>One of the <see cref="T:System.Web.ProcessStatus" /> values that indicates the current status of the process.</returns>
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0001C4E9 File Offset: 0x0001A6E9
		public ProcessStatus Status
		{
			get
			{
				return this.status;
			}
		}

		/// <summary>Sets internal information indicating the status of the process.</summary>
		/// <param name="startTime">A <see cref="T:System.DateTime" /> that indicates the time at which the process started. </param>
		/// <param name="age">A <see cref="T:System.TimeSpan" /> that indicates the time elapsed since the process started. </param>
		/// <param name="processID">The ID number assigned to the process. </param>
		/// <param name="requestCount">The number of start requests for the process. </param>
		/// <param name="status">One of the <see cref="T:System.Web.ProcessStatus" /> values that indicates the time elapsed since the process started. </param>
		/// <param name="shutdownReason">One of the <see cref="T:System.Web.ProcessShutdownReason" /> values. </param>
		/// <param name="peakMemoryUsed">The maximum memory used, in kilobytes (KB). </param>
		// Token: 0x06000AF4 RID: 2804 RVA: 0x0001C4F1 File Offset: 0x0001A6F1
		public void SetAll(DateTime startTime, TimeSpan age, int processID, int requestCount, ProcessStatus status, ProcessShutdownReason shutdownReason, int peakMemoryUsed)
		{
			this.age = age;
			this.peakMemoryUsed = peakMemoryUsed;
			this.processID = processID;
			this.requestCount = requestCount;
			this.shutdownReason = shutdownReason;
			this.startTime = startTime;
			this.status = status;
		}

		// Token: 0x04001074 RID: 4212
		private TimeSpan age;

		// Token: 0x04001075 RID: 4213
		private int peakMemoryUsed;

		// Token: 0x04001076 RID: 4214
		private int processID;

		// Token: 0x04001077 RID: 4215
		private int requestCount;

		// Token: 0x04001078 RID: 4216
		private ProcessShutdownReason shutdownReason;

		// Token: 0x04001079 RID: 4217
		private DateTime startTime;

		// Token: 0x0400107A RID: 4218
		private ProcessStatus status;
	}
}
