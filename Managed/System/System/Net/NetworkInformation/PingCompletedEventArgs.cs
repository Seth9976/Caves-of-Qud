using System;
using System.ComponentModel;
using Unity;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides data for the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event.</summary>
	// Token: 0x02000724 RID: 1828
	public class PingCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003A54 RID: 14932 RVA: 0x000CA6E1 File Offset: 0x000C88E1
		internal PingCompletedEventArgs(Exception ex, bool cancelled, object userState, PingReply reply)
			: base(ex, cancelled, userState)
		{
			this.reply = reply;
		}

		/// <summary>Gets an object that contains data that describes an attempt to send an Internet Control Message Protocol (ICMP) echo request message and receive a corresponding ICMP echo reply message.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that describes the results of the ICMP echo request.</returns>
		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06003A55 RID: 14933 RVA: 0x000CA6F4 File Offset: 0x000C88F4
		public PingReply Reply
		{
			get
			{
				return this.reply;
			}
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal PingCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002267 RID: 8807
		private PingReply reply;
	}
}
