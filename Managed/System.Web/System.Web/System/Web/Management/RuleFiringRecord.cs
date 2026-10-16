using System;

namespace System.Web.Management
{
	/// <summary>Represents the firing record for an event that derives from the <see cref="T:System.Web.Management.WebManagementEvent" /> class and implements the <see cref="T:System.Web.Management.IWebEventCustomEvaluator" /> interface.</summary>
	// Token: 0x02000527 RID: 1319
	public sealed class RuleFiringRecord
	{
		// Token: 0x060039F3 RID: 14835 RVA: 0x00002050 File Offset: 0x00000250
		internal RuleFiringRecord()
		{
		}

		/// <summary>Gets the last time that the event was last fired.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object representing when the event was last fired.</returns>
		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x060039F4 RID: 14836 RVA: 0x00003A2B File Offset: 0x00001C2B
		public DateTime LastFired
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the total number of times that the event has been raised.</summary>
		/// <returns>The total number of times the event has been raised.</returns>
		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x060039F5 RID: 14837 RVA: 0x00003A2B File Offset: 0x00001C2B
		public int TimesRaised
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
