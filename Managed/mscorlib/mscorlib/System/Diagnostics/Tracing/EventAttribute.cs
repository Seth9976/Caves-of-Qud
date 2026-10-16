using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies additional event schema information for an event.</summary>
	// Token: 0x020009EC RID: 2540
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class EventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventAttribute" /> class with the specified event identifier.</summary>
		/// <param name="eventId">The event identifier for the event.</param>
		// Token: 0x06005AA2 RID: 23202 RVA: 0x001343E7 File Offset: 0x001325E7
		public EventAttribute(int eventId)
		{
			this.EventId = eventId;
		}

		/// <summary>Gets or sets the identifier for the event.</summary>
		/// <returns>The event identifier.</returns>
		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06005AA3 RID: 23203 RVA: 0x001343F6 File Offset: 0x001325F6
		// (set) Token: 0x06005AA4 RID: 23204 RVA: 0x001343FE File Offset: 0x001325FE
		public int EventId { get; private set; }

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06005AA5 RID: 23205 RVA: 0x00134407 File Offset: 0x00132607
		// (set) Token: 0x06005AA6 RID: 23206 RVA: 0x0013440F File Offset: 0x0013260F
		public EventActivityOptions ActivityOptions { get; set; }

		/// <summary>Gets or sets the level for the event.</summary>
		/// <returns>One of the enumeration values that specifies the level for the event.</returns>
		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06005AA7 RID: 23207 RVA: 0x00134418 File Offset: 0x00132618
		// (set) Token: 0x06005AA8 RID: 23208 RVA: 0x00134420 File Offset: 0x00132620
		public EventLevel Level { get; set; }

		/// <summary>Gets or sets the keywords for the event.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06005AA9 RID: 23209 RVA: 0x00134429 File Offset: 0x00132629
		// (set) Token: 0x06005AAA RID: 23210 RVA: 0x00134431 File Offset: 0x00132631
		public EventKeywords Keywords { get; set; }

		/// <summary>Gets or sets the operation code for the event.</summary>
		/// <returns>One of the enumeration values that specifies the operation code.</returns>
		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06005AAB RID: 23211 RVA: 0x0013443A File Offset: 0x0013263A
		// (set) Token: 0x06005AAC RID: 23212 RVA: 0x00134442 File Offset: 0x00132642
		public EventOpcode Opcode { get; set; }

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06005AAD RID: 23213 RVA: 0x0013444B File Offset: 0x0013264B
		// (set) Token: 0x06005AAE RID: 23214 RVA: 0x00134453 File Offset: 0x00132653
		public EventChannel Channel { get; set; }

		/// <summary>Gets or sets the message for the event.</summary>
		/// <returns>The message for the event.</returns>
		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06005AAF RID: 23215 RVA: 0x0013445C File Offset: 0x0013265C
		// (set) Token: 0x06005AB0 RID: 23216 RVA: 0x00134464 File Offset: 0x00132664
		public string Message { get; set; }

		/// <summary>Gets or sets the task for the event.</summary>
		/// <returns>The task for the event.</returns>
		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06005AB1 RID: 23217 RVA: 0x0013446D File Offset: 0x0013266D
		// (set) Token: 0x06005AB2 RID: 23218 RVA: 0x00134475 File Offset: 0x00132675
		public EventTask Task { get; set; }

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06005AB3 RID: 23219 RVA: 0x0013447E File Offset: 0x0013267E
		// (set) Token: 0x06005AB4 RID: 23220 RVA: 0x00134486 File Offset: 0x00132686
		public EventTags Tags { get; set; }

		/// <summary>Gets or sets the version of the event.</summary>
		/// <returns>The version of the event.</returns>
		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06005AB5 RID: 23221 RVA: 0x0013448F File Offset: 0x0013268F
		// (set) Token: 0x06005AB6 RID: 23222 RVA: 0x00134497 File Offset: 0x00132697
		public byte Version { get; set; }
	}
}
