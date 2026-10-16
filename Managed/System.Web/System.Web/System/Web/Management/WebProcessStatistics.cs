using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Provides information for assessing the health of a running process.</summary>
	// Token: 0x02000755 RID: 1877
	public class WebProcessStatistics
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebProcessStatistics" /> class. </summary>
		// Token: 0x06004CC7 RID: 19655 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebProcessStatistics()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the number of application domains in the process.</summary>
		/// <returns>The number of application domains.</returns>
		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x06004CC8 RID: 19656 RVA: 0x000C8734 File Offset: 0x000C6934
		public int AppDomainCount
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the size of the managed heap.</summary>
		/// <returns>The size of the managed heap.</returns>
		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x06004CC9 RID: 19657 RVA: 0x000C8750 File Offset: 0x000C6950
		public long ManagedHeapSize
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Gets the peak working set for the lifetime of the process to date.</summary>
		/// <returns>The peak working set of the process.</returns>
		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x06004CCA RID: 19658 RVA: 0x000C876C File Offset: 0x000C696C
		public long PeakWorkingSet
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Gets the time when the process started.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> when the process started.</returns>
		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x06004CCB RID: 19659 RVA: 0x000C8788 File Offset: 0x000C6988
		public DateTime ProcessStartTime
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
		}

		/// <summary>Gets the number of requests currently executing.</summary>
		/// <returns>The number of requests that the process is currently executing.</returns>
		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x06004CCC RID: 19660 RVA: 0x000C87A4 File Offset: 0x000C69A4
		public int RequestsExecuting
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the number of requests waiting to be processed.</summary>
		/// <returns>The number of requests waiting to be processed.</returns>
		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x06004CCD RID: 19661 RVA: 0x000C87C0 File Offset: 0x000C69C0
		public int RequestsQueued
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the number of rejected requests.</summary>
		/// <returns>The number of rejected requests.</returns>
		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x06004CCE RID: 19662 RVA: 0x000C87DC File Offset: 0x000C69DC
		public int RequestsRejected
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the total count of the process threads.</summary>
		/// <returns>The total count of the process threads.</returns>
		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x06004CCF RID: 19663 RVA: 0x000C87F8 File Offset: 0x000C69F8
		public int ThreadCount
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the working set for the process.</summary>
		/// <returns>The working set for the process.</returns>
		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x06004CD0 RID: 19664 RVA: 0x000C8814 File Offset: 0x000C6A14
		public long WorkingSet
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Formats the process statistics.</summary>
		/// <param name="formatter">The <see cref="T:System.Web.Management.WebEventFormatter" /> that contains the tab and indentation settings used to format the Web health event information.</param>
		// Token: 0x06004CD1 RID: 19665 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void FormatToString(WebEventFormatter formatter)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
