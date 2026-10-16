using System;

namespace System.Web.Hosting
{
	/// <summary>Listens for GL_STOP_LISTENING notifications from IIS.</summary>
	// Token: 0x0200053F RID: 1343
	public interface IStopListeningRegisteredObject : IRegisteredObject
	{
		/// <summary>Stops listening for new requests.</summary>
		// Token: 0x06003A49 RID: 14921
		void StopListening();
	}
}
