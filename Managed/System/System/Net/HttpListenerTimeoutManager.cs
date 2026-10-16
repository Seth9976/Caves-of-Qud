using System;

namespace System.Net
{
	/// <summary>The timeout manager to use for an <see cref="T:System.Net.HttpListener" /> object.</summary>
	// Token: 0x02000694 RID: 1684
	public class HttpListenerTimeoutManager
	{
		/// <summary>Gets or sets the time, in seconds, allowed for the request entity body to arrive.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the request entity body to arrive.</returns>
		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06003562 RID: 13666 RVA: 0x0000829A File Offset: 0x0000649A
		// (set) Token: 0x06003563 RID: 13667 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public TimeSpan EntityBody
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" />  to drain the entity body on a Keep-Alive connection.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" />  to drain the entity body on a Keep-Alive connection.</returns>
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06003564 RID: 13668 RVA: 0x0000829A File Offset: 0x0000649A
		// (set) Token: 0x06003565 RID: 13669 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public TimeSpan DrainEntityBody
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the request to remain in the request queue before the <see cref="T:System.Net.HttpListener" /> picks it up.</returns>
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06003566 RID: 13670 RVA: 0x0000829A File Offset: 0x0000649A
		// (set) Token: 0x06003567 RID: 13671 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public TimeSpan RequestQueue
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for an idle connection.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for an idle connection.</returns>
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06003568 RID: 13672 RVA: 0x0000829A File Offset: 0x0000649A
		// (set) Token: 0x06003569 RID: 13673 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public TimeSpan IdleConnection
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The time, in seconds, allowed for the <see cref="T:System.Net.HttpListener" /> to parse the request header.</returns>
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600356A RID: 13674 RVA: 0x0000829A File Offset: 0x0000649A
		// (set) Token: 0x0600356B RID: 13675 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public TimeSpan HeaderWait
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the minimum send rate, in bytes-per-second, for the response. </summary>
		/// <returns>Returns <see cref="T:System.Int64" />.The minimum send rate, in bytes-per-second, for the response.</returns>
		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600356C RID: 13676 RVA: 0x0000829A File Offset: 0x0000649A
		// (set) Token: 0x0600356D RID: 13677 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public long MinSendBytesPerSecond
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
