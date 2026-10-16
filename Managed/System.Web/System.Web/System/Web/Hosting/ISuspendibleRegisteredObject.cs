using System;

namespace System.Web.Hosting
{
	/// <summary>Listens for suspend and resume notifications.</summary>
	// Token: 0x02000540 RID: 1344
	public interface ISuspendibleRegisteredObject : IRegisteredObject
	{
		/// <summary>Called when ASP.NET notifies an application that a process is being suspended.</summary>
		// Token: 0x06003A4A RID: 14922
		Action Suspend();
	}
}
