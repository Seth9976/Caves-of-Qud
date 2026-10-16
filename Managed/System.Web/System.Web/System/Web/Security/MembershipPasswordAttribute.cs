using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Unity;

namespace System.Web.Security
{
	/// <summary>Validates whether a password field meets the current password requirements for the membership provider.</summary>
	// Token: 0x020004C1 RID: 1217
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class MembershipPasswordAttribute : ValidationAttribute
	{
		/// <summary>Gets or sets the MinNonAlphanumericCharactersError attribute.</summary>
		/// <returns>The MinNonAlphanumericCharactersError attribute.</returns>
		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x0008D928 File Offset: 0x0008BB28
		// (set) Token: 0x060036C8 RID: 14024 RVA: 0x0008D930 File Offset: 0x0008BB30
		public string MinNonAlphanumericCharactersError { get; set; }

		/// <summary>Gets or sets the MinPasswordLengthError attribute.</summary>
		/// <returns>The MinPasswordLengthError attribute.</returns>
		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x060036C9 RID: 14025 RVA: 0x0008D939 File Offset: 0x0008BB39
		// (set) Token: 0x060036CA RID: 14026 RVA: 0x0008D941 File Offset: 0x0008BB41
		public string MinPasswordLengthError { get; set; }

		/// <summary>Gets or sets the minimum required non-alpha numeric characters the attribute uses for validation.</summary>
		/// <returns>The minimum required non-alpha numeric characters the attribute uses for validation.</returns>
		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x060036CB RID: 14027 RVA: 0x0008D94A File Offset: 0x0008BB4A
		// (set) Token: 0x060036CC RID: 14028 RVA: 0x0008D952 File Offset: 0x0008BB52
		public int MinRequiredNonAlphanumericCharacters { get; set; }

		/// <summary>Gets or sets the minimum required password length this attribute uses for validation.</summary>
		/// <returns>The minimum required password length this attribute uses for validation.</returns>
		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x060036CD RID: 14029 RVA: 0x0008D95B File Offset: 0x0008BB5B
		// (set) Token: 0x060036CE RID: 14030 RVA: 0x0008D963 File Offset: 0x0008BB63
		public int MinRequiredPasswordLength { get; set; }

		/// <summary>Gets or sets the PasswordStrengthError attribute.</summary>
		/// <returns>The PasswordStrengthError attribute.</returns>
		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x060036CF RID: 14031 RVA: 0x0008D96C File Offset: 0x0008BB6C
		// (set) Token: 0x060036D0 RID: 14032 RVA: 0x0008D974 File Offset: 0x0008BB74
		public string PasswordStrengthError { get; set; }

		/// <summary>Gets or sets the regular expression string representing the password strength the attribute uses for validation.</summary>
		/// <returns>The regular expression string representing the password strength the attribute uses for validation.</returns>
		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x060036D1 RID: 14033 RVA: 0x0008D97D File Offset: 0x0008BB7D
		// (set) Token: 0x060036D2 RID: 14034 RVA: 0x0008D985 File Offset: 0x0008BB85
		public string PasswordStrengthRegularExpression { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Type" /> that contains the resources for the <see cref="P:System.Web.Security.MembershipPasswordAttribute.MinPasswordLengthError" /> property, the <see cref="P:System.Web.Security.MembershipPasswordAttribute.MinNonAlphaNumericCharactersError" /> property, and the <see cref="P:System.Web.Security.MembershipPasswordAttribute.PasswordStrengthError" /> property.</summary>
		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x060036D3 RID: 14035 RVA: 0x0008D98E File Offset: 0x0008BB8E
		// (set) Token: 0x060036D4 RID: 14036 RVA: 0x0008D996 File Offset: 0x0008BB96
		public Type ResourceType { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipPasswordAttribute" /> class.</summary>
		// Token: 0x060036D5 RID: 14037 RVA: 0x0008D9A0 File Offset: 0x0008BBA0
		public MembershipPasswordAttribute()
		{
			if (Membership.Provider != null)
			{
				this.MinRequiredNonAlphanumericCharacters = Membership.Provider.MinRequiredNonAlphanumericCharacters;
				this.MinRequiredPasswordLength = Membership.Provider.MinRequiredPasswordLength;
				this.PasswordStrengthRegularExpression = Membership.Provider.PasswordStrengthRegularExpression;
			}
			else
			{
				this.MinRequiredPasswordLength = 7;
				this.MinRequiredNonAlphanumericCharacters = 1;
				this.PasswordStrengthRegularExpression = "(?=.{6,})(?=(.*\\d){1,})(?=(.*\\W){1,})";
			}
			this.MinNonAlphanumericCharactersError = "The '{0}' field is an invalid password. Password must have {1} or more non-alphanumeric characters.";
			this.MinPasswordLengthError = "The '{0}' field is an invalid password. Password must have {1} or more characters.";
			this.PasswordStrengthError = "The '{0}' field is an invalid password. It does not meet the password strength requirements";
			base.ErrorMessage = "The field {0} is invalid.";
		}

		/// <summary>Validates the specified value with respect to the current validation attribute.</summary>
		/// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.</returns>
		/// <param name="value">The value to validate.</param>
		/// <param name="validationContext">The context information about the validation operation.</param>
		// Token: 0x060036D6 RID: 14038 RVA: 0x0008DA34 File Offset: 0x0008BC34
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			string text = value as string;
			bool flag = false;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			string text2 = string.Empty;
			int num = 0;
			Regex regex = new Regex("\\W|_");
			if (this.MinRequiredPasswordLength > 0 && text.Length < this.MinRequiredPasswordLength)
			{
				text2 = this.MinPasswordLengthError;
				num = this.MinRequiredPasswordLength;
				flag = true;
			}
			if (!flag && this.MinRequiredNonAlphanumericCharacters > 0 && regex.Matches(text).Count < this.MinRequiredNonAlphanumericCharacters)
			{
				text2 = this.MinNonAlphanumericCharactersError;
				num = this.MinRequiredNonAlphanumericCharacters;
				flag = true;
			}
			if (!flag && !string.IsNullOrEmpty(this.PasswordStrengthRegularExpression) && new Regex(this.PasswordStrengthRegularExpression).IsMatch(text))
			{
				text2 = this.PasswordStrengthError;
				flag = true;
			}
			if (!flag)
			{
				return ValidationResult.Success;
			}
			if (validationContext == null)
			{
				return new ValidationResult("error");
			}
			return new ValidationResult(string.Format(text2, validationContext.DisplayName, num), new string[] { validationContext.MemberName });
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x060036D7 RID: 14039 RVA: 0x0008DB2C File Offset: 0x0008BD2C
		// (set) Token: 0x060036D8 RID: 14040 RVA: 0x00003946 File Offset: 0x00001B46
		public int? PasswordStrengthRegexTimeout
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}
	}
}
