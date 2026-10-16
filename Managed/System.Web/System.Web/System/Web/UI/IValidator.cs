using System;

namespace System.Web.UI
{
	/// <summary>Defines the properties and methods that objects that participate in Web Forms validation must implement.</summary>
	// Token: 0x02000185 RID: 389
	public interface IValidator
	{
		/// <summary>When implemented by a class, gets or sets a value indicating whether the user-entered content in the specified control passes validation.</summary>
		/// <returns>true if the content is valid; otherwise, false.</returns>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000F63 RID: 3939
		// (set) Token: 0x06000F64 RID: 3940
		bool IsValid { get; set; }

		/// <summary>When implemented by a class, gets or sets the error message text generated when the condition being validated fails.</summary>
		/// <returns>The error message to generate.</returns>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000F65 RID: 3941
		// (set) Token: 0x06000F66 RID: 3942
		string ErrorMessage { get; set; }

		/// <summary>When implemented by a class, evaluates the condition it checks and updates the <see cref="P:System.Web.UI.IValidator.IsValid" /> property.</summary>
		// Token: 0x06000F67 RID: 3943
		void Validate();
	}
}
