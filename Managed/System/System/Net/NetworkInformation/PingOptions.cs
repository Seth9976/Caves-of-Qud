using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Used to control how <see cref="T:System.Net.NetworkInformation.Ping" /> data packets are transmitted.</summary>
	// Token: 0x020006FF RID: 1791
	public class PingOptions
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingOptions" /> class and sets the Time to Live and fragmentation values.</summary>
		/// <param name="ttl">An <see cref="T:System.Int32" /> value greater than zero that specifies the number of times that the <see cref="T:System.Net.NetworkInformation.Ping" /> data packets can be forwarded.</param>
		/// <param name="dontFragment">true to prevent data sent to the remote host from being fragmented; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ttl " />is less than or equal to zero.</exception>
		// Token: 0x06003979 RID: 14713 RVA: 0x000C8B34 File Offset: 0x000C6D34
		public PingOptions(int ttl, bool dontFragment)
		{
			if (ttl <= 0)
			{
				throw new ArgumentOutOfRangeException("ttl");
			}
			this.ttl = ttl;
			this.dontFragment = dontFragment;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingOptions" /> class.</summary>
		// Token: 0x0600397A RID: 14714 RVA: 0x000C8B64 File Offset: 0x000C6D64
		public PingOptions()
		{
		}

		/// <summary>Gets or sets the number of routing nodes that can forward the <see cref="T:System.Net.NetworkInformation.Ping" /> data before it is discarded.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the number of times the <see cref="T:System.Net.NetworkInformation.Ping" /> data packets can be forwarded. The default is 128.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero.</exception>
		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x0600397B RID: 14715 RVA: 0x000C8B77 File Offset: 0x000C6D77
		// (set) Token: 0x0600397C RID: 14716 RVA: 0x000C8B7F File Offset: 0x000C6D7F
		public int Ttl
		{
			get
			{
				return this.ttl;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ttl = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls fragmentation of the data sent to the remote host.</summary>
		/// <returns>true if the data cannot be sent in multiple packets; otherwise false. The default is false.</returns>
		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x0600397D RID: 14717 RVA: 0x000C8B97 File Offset: 0x000C6D97
		// (set) Token: 0x0600397E RID: 14718 RVA: 0x000C8B9F File Offset: 0x000C6D9F
		public bool DontFragment
		{
			get
			{
				return this.dontFragment;
			}
			set
			{
				this.dontFragment = value;
			}
		}

		// Token: 0x040021CA RID: 8650
		private const int DontFragmentFlag = 2;

		// Token: 0x040021CB RID: 8651
		private int ttl = 128;

		// Token: 0x040021CC RID: 8652
		private bool dontFragment;
	}
}
