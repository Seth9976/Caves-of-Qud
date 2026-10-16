using System;

namespace System.ComponentModel
{
	/// <summary>Specifies which event is raised on initialization. This class cannot be inherited.</summary>
	// Token: 0x02000376 RID: 886
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class InitializationEventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InitializationEventAttribute" /> class.</summary>
		/// <param name="eventName">The name of the initialization event.</param>
		// Token: 0x06001D2E RID: 7470 RVA: 0x00068523 File Offset: 0x00066723
		public InitializationEventAttribute(string eventName)
		{
			this.EventName = eventName;
		}

		/// <summary>Gets the name of the initialization event.</summary>
		/// <returns>The name of the initialization event.</returns>
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x00068532 File Offset: 0x00066732
		public string EventName { get; }
	}
}
