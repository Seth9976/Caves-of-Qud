using System;

namespace System.ComponentModel
{
	/// <summary>Specifies values that succinctly describe the results of a masked text parsing operation.</summary>
	// Token: 0x020003DC RID: 988
	public enum MaskedTextResultHint
	{
		/// <summary>Unknown. The result of the operation could not be determined.</summary>
		// Token: 0x04000FC7 RID: 4039
		Unknown,
		/// <summary>Success. The operation succeeded because a literal, prompt or space character was an escaped character. For more information about escaped characters, see the <see cref="M:System.ComponentModel.MaskedTextProvider.VerifyEscapeChar(System.Char,System.Int32)" /> method.</summary>
		// Token: 0x04000FC8 RID: 4040
		CharacterEscaped,
		/// <summary>Success. The primary operation was not performed because it was not needed; therefore, no side effect was produced.</summary>
		// Token: 0x04000FC9 RID: 4041
		NoEffect,
		/// <summary>Success. The primary operation was not performed because it was not needed, but the method produced a side effect. For example, the <see cref="Overload:System.ComponentModel.MaskedTextProvider.RemoveAt" /> method can delete an unassigned edit position, which causes left-shifting of subsequent characters in the formatted string. </summary>
		// Token: 0x04000FCA RID: 4042
		SideEffect,
		/// <summary>Success. The primary operation succeeded.</summary>
		// Token: 0x04000FCB RID: 4043
		Success,
		/// <summary>Operation did not succeed.An input character was encountered that was not a member of the ASCII character set.</summary>
		// Token: 0x04000FCC RID: 4044
		AsciiCharacterExpected = -1,
		/// <summary>Operation did not succeed.An input character was encountered that was not alphanumeric. .</summary>
		// Token: 0x04000FCD RID: 4045
		AlphanumericCharacterExpected = -2,
		/// <summary>Operation did not succeed. An input character was encountered that was not a digit.</summary>
		// Token: 0x04000FCE RID: 4046
		DigitExpected = -3,
		/// <summary>Operation did not succeed. An input character was encountered that was not a letter.</summary>
		// Token: 0x04000FCF RID: 4047
		LetterExpected = -4,
		/// <summary>Operation did not succeed. An input character was encountered that was not a signed digit.</summary>
		// Token: 0x04000FD0 RID: 4048
		SignedDigitExpected = -5,
		/// <summary>Operation did not succeed. The program encountered an  input character that was not valid. For more information about characters that are not valid, see the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidInputChar(System.Char)" /> method.</summary>
		// Token: 0x04000FD1 RID: 4049
		InvalidInput = -51,
		/// <summary>Operation did not succeed. The prompt character is not valid at input, perhaps because the <see cref="P:System.ComponentModel.MaskedTextProvider.AllowPromptAsInput" /> property is set to false. </summary>
		// Token: 0x04000FD2 RID: 4050
		PromptCharNotAllowed = -52,
		/// <summary>Operation did not succeed. There were not enough edit positions available to fulfill the request.</summary>
		// Token: 0x04000FD3 RID: 4051
		UnavailableEditPosition = -53,
		/// <summary>Operation did not succeed. The current position in the formatted string is a literal character. </summary>
		// Token: 0x04000FD4 RID: 4052
		NonEditPosition = -54,
		/// <summary>Operation did not succeed. The specified position is not in the range of the target string; typically it is either less than zero or greater then the length of the target string.</summary>
		// Token: 0x04000FD5 RID: 4053
		PositionOutOfRange = -55
	}
}
