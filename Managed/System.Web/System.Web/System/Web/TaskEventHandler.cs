using System;
using System.Threading.Tasks;

namespace System.Web
{
	/// <summary>Represents the asynchronous task that is being processed by an instance of the <see cref="T:System.Web.EventHandlerTaskAsyncHelper" /> class.</summary>
	/// <returns>The asynchronous task.</returns>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	// Token: 0x020000DA RID: 218
	// (Invoke) Token: 0x06000BC5 RID: 3013
	public delegate Task TaskEventHandler(object sender, EventArgs e);
}
