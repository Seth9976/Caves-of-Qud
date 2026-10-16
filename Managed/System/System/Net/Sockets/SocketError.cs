using System;

namespace System.Net.Sockets
{
	/// <summary>Defines error codes for the <see cref="T:System.Net.Sockets.Socket" /> class.</summary>
	// Token: 0x020007B5 RID: 1973
	public enum SocketError
	{
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> operation succeeded.</summary>
		// Token: 0x04002556 RID: 9558
		Success,
		/// <summary>An unspecified <see cref="T:System.Net.Sockets.Socket" /> error has occurred.</summary>
		// Token: 0x04002557 RID: 9559
		SocketError = -1,
		/// <summary>A blocking <see cref="T:System.Net.Sockets.Socket" /> call was canceled.</summary>
		// Token: 0x04002558 RID: 9560
		Interrupted = 10004,
		/// <summary>An attempt was made to access a <see cref="T:System.Net.Sockets.Socket" /> in a way that is forbidden by its access permissions.</summary>
		// Token: 0x04002559 RID: 9561
		AccessDenied = 10013,
		/// <summary>An invalid pointer address was detected by the underlying socket provider.</summary>
		// Token: 0x0400255A RID: 9562
		Fault,
		/// <summary>An invalid argument was supplied to a <see cref="T:System.Net.Sockets.Socket" /> member.</summary>
		// Token: 0x0400255B RID: 9563
		InvalidArgument = 10022,
		/// <summary>There are too many open sockets in the underlying socket provider.</summary>
		// Token: 0x0400255C RID: 9564
		TooManyOpenSockets = 10024,
		/// <summary>An operation on a nonblocking socket cannot be completed immediately.</summary>
		// Token: 0x0400255D RID: 9565
		WouldBlock = 10035,
		/// <summary>A blocking operation is in progress.</summary>
		// Token: 0x0400255E RID: 9566
		InProgress,
		/// <summary>The nonblocking <see cref="T:System.Net.Sockets.Socket" /> already has an operation in progress.</summary>
		// Token: 0x0400255F RID: 9567
		AlreadyInProgress,
		/// <summary>A <see cref="T:System.Net.Sockets.Socket" /> operation was attempted on a non-socket.</summary>
		// Token: 0x04002560 RID: 9568
		NotSocket,
		/// <summary>A required address was omitted from an operation on a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04002561 RID: 9569
		DestinationAddressRequired,
		/// <summary>The datagram is too long.</summary>
		// Token: 0x04002562 RID: 9570
		MessageSize,
		/// <summary>The protocol type is incorrect for this <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04002563 RID: 9571
		ProtocolType,
		/// <summary>An unknown, invalid, or unsupported option or level was used with a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04002564 RID: 9572
		ProtocolOption,
		/// <summary>The protocol is not implemented or has not been configured.</summary>
		// Token: 0x04002565 RID: 9573
		ProtocolNotSupported,
		/// <summary>The support for the specified socket type does not exist in this address family.</summary>
		// Token: 0x04002566 RID: 9574
		SocketNotSupported,
		/// <summary>The address family is not supported by the protocol family.</summary>
		// Token: 0x04002567 RID: 9575
		OperationNotSupported,
		/// <summary>The protocol family is not implemented or has not been configured.</summary>
		// Token: 0x04002568 RID: 9576
		ProtocolFamilyNotSupported,
		/// <summary>The address family specified is not supported. This error is returned if the IPv6 address family was specified and the IPv6 stack is not installed on the local machine. This error is returned if the IPv4 address family was specified and the IPv4 stack is not installed on the local machine.</summary>
		// Token: 0x04002569 RID: 9577
		AddressFamilyNotSupported,
		/// <summary>Only one use of an address is normally permitted.</summary>
		// Token: 0x0400256A RID: 9578
		AddressAlreadyInUse,
		/// <summary>The selected IP address is not valid in this context.</summary>
		// Token: 0x0400256B RID: 9579
		AddressNotAvailable,
		/// <summary>The network is not available.</summary>
		// Token: 0x0400256C RID: 9580
		NetworkDown,
		/// <summary>No route to the remote host exists.</summary>
		// Token: 0x0400256D RID: 9581
		NetworkUnreachable,
		/// <summary>The application tried to set <see cref="F:System.Net.Sockets.SocketOptionName.KeepAlive" /> on a connection that has already timed out.</summary>
		// Token: 0x0400256E RID: 9582
		NetworkReset,
		/// <summary>The connection was aborted by the .NET Framework or the underlying socket provider.</summary>
		// Token: 0x0400256F RID: 9583
		ConnectionAborted,
		/// <summary>The connection was reset by the remote peer.</summary>
		// Token: 0x04002570 RID: 9584
		ConnectionReset,
		/// <summary>No free buffer space is available for a <see cref="T:System.Net.Sockets.Socket" /> operation.</summary>
		// Token: 0x04002571 RID: 9585
		NoBufferSpaceAvailable,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is already connected.</summary>
		// Token: 0x04002572 RID: 9586
		IsConnected,
		/// <summary>The application tried to send or receive data, and the <see cref="T:System.Net.Sockets.Socket" /> is not connected.</summary>
		// Token: 0x04002573 RID: 9587
		NotConnected,
		/// <summary>A request to send or receive data was disallowed because the <see cref="T:System.Net.Sockets.Socket" /> has already been closed.</summary>
		// Token: 0x04002574 RID: 9588
		Shutdown,
		/// <summary>The connection attempt timed out, or the connected host has failed to respond.</summary>
		// Token: 0x04002575 RID: 9589
		TimedOut = 10060,
		/// <summary>The remote host is actively refusing a connection.</summary>
		// Token: 0x04002576 RID: 9590
		ConnectionRefused,
		/// <summary>The operation failed because the remote host is down.</summary>
		// Token: 0x04002577 RID: 9591
		HostDown = 10064,
		/// <summary>There is no network route to the specified host.</summary>
		// Token: 0x04002578 RID: 9592
		HostUnreachable,
		/// <summary>Too many processes are using the underlying socket provider.</summary>
		// Token: 0x04002579 RID: 9593
		ProcessLimit = 10067,
		/// <summary>The network subsystem is unavailable.</summary>
		// Token: 0x0400257A RID: 9594
		SystemNotReady = 10091,
		/// <summary>The version of the underlying socket provider is out of range.</summary>
		// Token: 0x0400257B RID: 9595
		VersionNotSupported,
		/// <summary>The underlying socket provider has not been initialized.</summary>
		// Token: 0x0400257C RID: 9596
		NotInitialized,
		/// <summary>A graceful shutdown is in progress.</summary>
		// Token: 0x0400257D RID: 9597
		Disconnecting = 10101,
		/// <summary>The specified class was not found.</summary>
		// Token: 0x0400257E RID: 9598
		TypeNotFound = 10109,
		/// <summary>No such host is known. The name is not an official host name or alias.</summary>
		// Token: 0x0400257F RID: 9599
		HostNotFound = 11001,
		/// <summary>The name of the host could not be resolved. Try again later.</summary>
		// Token: 0x04002580 RID: 9600
		TryAgain,
		/// <summary>The error is unrecoverable or the requested database cannot be located.</summary>
		// Token: 0x04002581 RID: 9601
		NoRecovery,
		/// <summary>The requested name or IP address was not found on the name server.</summary>
		// Token: 0x04002582 RID: 9602
		NoData,
		/// <summary>The application has initiated an overlapped operation that cannot be completed immediately.</summary>
		// Token: 0x04002583 RID: 9603
		IOPending = 997,
		/// <summary>The overlapped operation was aborted due to the closure of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04002584 RID: 9604
		OperationAborted = 995
	}
}
