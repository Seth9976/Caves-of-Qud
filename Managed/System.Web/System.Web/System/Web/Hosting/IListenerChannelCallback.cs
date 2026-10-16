using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	/// <summary>Provides listener-channel notifications from protocol handlers to the worker process framework. Also enables protocol handlers to access additional parameters, such as the ID of the listener channel.</summary>
	// Token: 0x0200053D RID: 1341
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("dc3b0a85-9da7-47e4-ba1b-e27da9db8a1e")]
	[ComImport]
	public interface IListenerChannelCallback
	{
		/// <summary>Notifies the worker process framework that a listener channel has started.</summary>
		// Token: 0x06003A42 RID: 14914
		void ReportStarted();

		/// <summary>Notifies the worker process framework that a listener channel has stopped.</summary>
		/// <param name="hr">The HRESULT value that reports the status of a listener channel.</param>
		// Token: 0x06003A43 RID: 14915
		void ReportStopped(int hr);

		/// <summary>Notifies the worker process framework that a new message was received.</summary>
		// Token: 0x06003A44 RID: 14916
		void ReportMessageReceived();

		/// <summary>Gets the ID of a listener channel that has to be started.</summary>
		/// <returns>The ID of the listener channel.</returns>
		// Token: 0x06003A45 RID: 14917
		int GetId();

		/// <summary>Gets the size of the buffered data in the listener channel.</summary>
		/// <returns>The size, in bytes, of the data that is in the listener channel buffer.</returns>
		// Token: 0x06003A46 RID: 14918
		int GetBlobLength();

		/// <summary>Retrieves data that the protocol manager will pass to a listener channel when the protocol manager is starting that listener channel.</summary>
		/// <param name="buffer">The data that will be passed to the listener channel.</param>
		/// <param name="bufferSize">The amount of data, in bytes, that is in the buffer.</param>
		// Token: 0x06003A47 RID: 14919
		void GetBlob([MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] buffer, ref int bufferSize);
	}
}
