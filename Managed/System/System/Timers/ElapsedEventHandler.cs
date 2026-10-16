using System;

namespace System.Timers
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Timers.Timer.Elapsed" /> event of a <see cref="T:System.Timers.Timer" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.Timers.ElapsedEventArgs" /> object that contains the event data. </param>
	// Token: 0x02000195 RID: 405
	// (Invoke) Token: 0x06000A8F RID: 2703
	public delegate void ElapsedEventHandler(object sender, ElapsedEventArgs e);
}
