using System;

namespace System.Web.Configuration
{
	/// <summary>Defines the encryption format for storing passwords.</summary>
	// Token: 0x02000560 RID: 1376
	public enum FormsAuthPasswordFormat
	{
		/// <summary>Specifies that passwords are not encrypted. This field is constant.</summary>
		// Token: 0x04002010 RID: 8208
		Clear,
		/// <summary>Specifies that passwords are encrypted using the SHA1 hash algorithm. This field is constant.</summary>
		// Token: 0x04002011 RID: 8209
		SHA1,
		/// <summary>Specifies that passwords are encrypted using the MD5 hash algorithm. This field is constant.</summary>
		// Token: 0x04002012 RID: 8210
		MD5,
		// Token: 0x04002013 RID: 8211
		SHA256,
		// Token: 0x04002014 RID: 8212
		SHA384,
		// Token: 0x04002015 RID: 8213
		SHA512
	}
}
