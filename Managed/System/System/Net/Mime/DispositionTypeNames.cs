using System;

namespace System.Net.Mime
{
	/// <summary>Supplies the strings used to specify the disposition type for an e-mail attachment.</summary>
	// Token: 0x020007D3 RID: 2003
	public static class DispositionTypeNames
	{
		/// <summary>Specifies that the attachment is to be displayed as part of the e-mail message body.</summary>
		// Token: 0x04002692 RID: 9874
		public const string Inline = "inline";

		/// <summary>Specifies that the attachment is to be displayed as a file attached to the e-mail message.</summary>
		// Token: 0x04002693 RID: 9875
		public const string Attachment = "attachment";
	}
}
