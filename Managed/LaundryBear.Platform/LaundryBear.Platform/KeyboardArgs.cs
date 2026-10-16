using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x0200004A RID: 74
	public struct KeyboardArgs
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00002638 File Offset: 0x00000838
		public static KeyboardArgs CreateKeyboardArgs()
		{
			return new KeyboardArgs
			{
				type = KeyboardType.Default,
				isPassword = false,
				isMultiline = false,
				minLength = -1,
				maxLength = -1,
				title = "",
				description = ""
			};
		}

		// Token: 0x04000091 RID: 145
		public KeyboardType type;

		// Token: 0x04000092 RID: 146
		public bool isPassword;

		// Token: 0x04000093 RID: 147
		public bool isMultiline;

		// Token: 0x04000094 RID: 148
		public int minLength;

		// Token: 0x04000095 RID: 149
		public int maxLength;

		// Token: 0x04000096 RID: 150
		public string title;

		// Token: 0x04000097 RID: 151
		public string description;
	}
}
