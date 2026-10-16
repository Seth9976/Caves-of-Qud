using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the mode for asynchronous requests.</summary>
	// Token: 0x02000557 RID: 1367
	[Flags]
	public enum AsyncPreloadModeFlags
	{
		/// <summary>No asynchronous upload of the request entity body will occur.</summary>
		// Token: 0x04001FF0 RID: 8176
		None = 0,
		/// <summary>Asynchronous uploads of the request entity body will occur only for form posts. This flag enables asynchronous preloading for MIME types that are specifically set to application/x-www-form-urlencoded.</summary>
		// Token: 0x04001FF1 RID: 8177
		Form = 1,
		/// <summary>Asynchronous uploads of the request entity body will occur only for multi-part form data. This flag enables asynchronous preloading for MIME types that are specifically set to multipart/form-data.</summary>
		// Token: 0x04001FF2 RID: 8178
		FormMultiPart = 2,
		/// <summary>Asynchronous uploads of the request entity body will occur only for non-form posts.</summary>
		// Token: 0x04001FF3 RID: 8179
		NonForm = 4,
		/// <summary>Asynchronous uploads of the request entity body will occur only for form posts.</summary>
		// Token: 0x04001FF4 RID: 8180
		AllFormTypes = 3,
		/// <summary>Asynchronous uploads of the request entity body will occur for all posts.</summary>
		// Token: 0x04001FF5 RID: 8181
		All = 7
	}
}
