using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Allows the event tracing for Windows (ETW) name to be defined independently of the name of the event source class.   </summary>
	// Token: 0x020009F9 RID: 2553
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class EventSourceAttribute : Attribute
	{
		/// <summary>Gets or sets the event source identifier.</summary>
		/// <returns>The event source identifier.</returns>
		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06005B18 RID: 23320 RVA: 0x00134842 File Offset: 0x00132A42
		// (set) Token: 0x06005B19 RID: 23321 RVA: 0x0013484A File Offset: 0x00132A4A
		public string Guid { get; set; }

		/// <summary>Gets or sets the name of the localization resource file.</summary>
		/// <returns>The name of the localization resource file, or null if the localization resource file does not exist.</returns>
		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06005B1A RID: 23322 RVA: 0x00134853 File Offset: 0x00132A53
		// (set) Token: 0x06005B1B RID: 23323 RVA: 0x0013485B File Offset: 0x00132A5B
		public string LocalizationResources { get; set; }

		/// <summary>Gets or sets the name of the event source.</summary>
		/// <returns>The name of the event source.</returns>
		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06005B1C RID: 23324 RVA: 0x00134864 File Offset: 0x00132A64
		// (set) Token: 0x06005B1D RID: 23325 RVA: 0x0013486C File Offset: 0x00132A6C
		public string Name { get; set; }
	}
}
