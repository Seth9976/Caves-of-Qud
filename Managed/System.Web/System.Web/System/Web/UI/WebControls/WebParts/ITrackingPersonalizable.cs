using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Allows Web Parts controls to track the specific phases of the personalization load and save process.</summary>
	// Token: 0x0200045C RID: 1116
	public interface ITrackingPersonalizable
	{
		/// <summary>Indicates whether the control tracks the status of its changes.</summary>
		/// <returns>true if the Web Parts control is responsible for determining when the control is considered changed ("dirty"); otherwise, false.</returns>
		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x060033A1 RID: 13217
		bool TracksChanges { get; }

		/// <summary>Represents the beginning of the load phase for personalization information. </summary>
		// Token: 0x060033A2 RID: 13218
		void BeginLoad();

		/// <summary>Represents the phase prior to extracting personalization data from a control. </summary>
		// Token: 0x060033A3 RID: 13219
		void BeginSave();

		/// <summary>Represents the phase after personalization data has been applied to a control. </summary>
		// Token: 0x060033A4 RID: 13220
		void EndLoad();

		/// <summary>Represents the phase after personalization data has been extracted from a control. </summary>
		// Token: 0x060033A5 RID: 13221
		void EndSave();
	}
}
