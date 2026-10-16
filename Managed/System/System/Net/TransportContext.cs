using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	/// <summary>The <see cref="T:System.Net.TransportContext" /> class provides additional context about the underlying transport layer.</summary>
	// Token: 0x02000605 RID: 1541
	public abstract class TransportContext
	{
		/// <summary>Retrieves the requested channel binding. </summary>
		/// <returns>The requested <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" />, or null if the channel binding is not supported by the current transport or by the operating system.</returns>
		/// <param name="kind">The type of channel binding to retrieve.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="kind" /> is must be <see cref="F:System.Security.Authentication.ExtendedProtection.ChannelBindingKind.Endpoint" /> for use with the <see cref="T:System.Net.TransportContext" /> retrieved from the <see cref="P:System.Net.HttpListenerRequest.TransportContext" /> property.</exception>
		// Token: 0x060030AF RID: 12463
		public abstract ChannelBinding GetChannelBinding(ChannelBindingKind kind);

		// Token: 0x060030B0 RID: 12464 RVA: 0x000044FA File Offset: 0x000026FA
		public virtual IEnumerable<TokenBinding> GetTlsTokenBindings()
		{
			throw new NotSupportedException();
		}
	}
}
