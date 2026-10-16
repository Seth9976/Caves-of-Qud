using System;

namespace Microsoft.Win32
{
	/// <summary>Represents the method that will handle the <see cref="E:Microsoft.Win32.SystemEvents.SessionEnded" /> event.</summary>
	/// <param name="sender">The source of the event. When this event is raised by the <see cref="T:Microsoft.Win32.SystemEvents" /> class, this object is always null. </param>
	/// <param name="e">A <see cref="T:Microsoft.Win32.SessionEndedEventArgs" /> that contains the event data. </param>
	// Token: 0x02000127 RID: 295
	// (Invoke) Token: 0x060006D6 RID: 1750
	public delegate void SessionEndedEventHandler(object sender, SessionEndedEventArgs e);
}
