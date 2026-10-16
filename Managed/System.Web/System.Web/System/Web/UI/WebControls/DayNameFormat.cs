using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the display format for the days of the week on a <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	// Token: 0x02000298 RID: 664
	public enum DayNameFormat
	{
		/// <summary>The days of the week displayed in full format. For example, Monday.</summary>
		// Token: 0x040016AA RID: 5802
		Full,
		/// <summary>The days of the week displayed in abbreviated format. For example, Mon represents Monday.</summary>
		// Token: 0x040016AB RID: 5803
		Short,
		/// <summary>The days of the week displayed with just the first letter. For example, M represents Monday.</summary>
		// Token: 0x040016AC RID: 5804
		FirstLetter,
		/// <summary>The days of the week displayed with just the first two letters. For example, Mo represents Monday.</summary>
		// Token: 0x040016AD RID: 5805
		FirstTwoLetters,
		/// <summary>The days of the week displayed in the shortest abbreviation format possible for the current culture.</summary>
		// Token: 0x040016AE RID: 5806
		Shortest
	}
}
