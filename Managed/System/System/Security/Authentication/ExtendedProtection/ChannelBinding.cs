using System;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> class encapsulates a pointer to the opaque data used to bind an authenticated transaction to a secure channel.</summary>
	// Token: 0x020002A7 RID: 679
	public abstract class ChannelBinding : SafeHandleZeroOrMinusOneIsInvalid
	{
		/// <summary>The <see cref="P:System.Security.Authentication.ExtendedProtection.ChannelBinding.Size" /> property gets the size, in bytes, of the channel binding token associated with the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> instance.</summary>
		/// <returns>The size, in bytes, of the channel binding token in the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> instance.</returns>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001535 RID: 5429
		public abstract int Size { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> class.</summary>
		// Token: 0x06001536 RID: 5430 RVA: 0x00055A8F File Offset: 0x00053C8F
		protected ChannelBinding()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> class.</summary>
		/// <param name="ownsHandle">A Boolean value that indicates if the application owns the safe handle to a native memory region containing the byte data that would be passed to native calls that provide extended protection for integrated windows authentication.</param>
		// Token: 0x06001537 RID: 5431 RVA: 0x00055A98 File Offset: 0x00053C98
		protected ChannelBinding(bool ownsHandle)
			: base(ownsHandle)
		{
		}
	}
}
