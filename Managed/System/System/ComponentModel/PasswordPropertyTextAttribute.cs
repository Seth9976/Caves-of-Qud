using System;

namespace System.ComponentModel
{
	/// <summary>Indicates that an object's text representation is obscured by characters such as asterisks. This class cannot be inherited.</summary>
	// Token: 0x020003E1 RID: 993
	[AttributeUsage(AttributeTargets.All)]
	public sealed class PasswordPropertyTextAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> class. </summary>
		// Token: 0x0600204D RID: 8269 RVA: 0x0006FC8E File Offset: 0x0006DE8E
		public PasswordPropertyTextAttribute()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> class, optionally showing password text. </summary>
		/// <param name="password">true to indicate that the property should be shown as password text; otherwise, false. The default is false.</param>
		// Token: 0x0600204E RID: 8270 RVA: 0x0006FC97 File Offset: 0x0006DE97
		public PasswordPropertyTextAttribute(bool password)
		{
			this.Password = password;
		}

		/// <summary>Gets a value indicating if the property for which the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> is defined should be shown as password text.</summary>
		/// <returns>true if the property should be shown as password text; otherwise, false.</returns>
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x0006FCA6 File Offset: 0x0006DEA6
		public bool Password { get; }

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> instances are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> is equal to the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" /> to compare with the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</param>
		// Token: 0x06002050 RID: 8272 RVA: 0x0006FCAE File Offset: 0x0006DEAE
		public override bool Equals(object o)
		{
			return o is PasswordPropertyTextAttribute && ((PasswordPropertyTextAttribute)o).Password == this.Password;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</returns>
		// Token: 0x06002051 RID: 8273 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns an indication whether the value of this instance is the default value.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06002052 RID: 8274 RVA: 0x0006FCCD File Offset: 0x0006DECD
		public override bool IsDefaultAttribute()
		{
			return this.Equals(PasswordPropertyTextAttribute.Default);
		}

		/// <summary>Specifies that a text property is used as a password. This static (Shared in Visual Basic) field is read-only.</summary>
		// Token: 0x04000FDD RID: 4061
		public static readonly PasswordPropertyTextAttribute Yes = new PasswordPropertyTextAttribute(true);

		/// <summary>Specifies that a text property is not used as a password. This static (Shared in Visual Basic) field is read-only.</summary>
		// Token: 0x04000FDE RID: 4062
		public static readonly PasswordPropertyTextAttribute No = new PasswordPropertyTextAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.PasswordPropertyTextAttribute" />.</summary>
		// Token: 0x04000FDF RID: 4063
		public static readonly PasswordPropertyTextAttribute Default = PasswordPropertyTextAttribute.No;
	}
}
