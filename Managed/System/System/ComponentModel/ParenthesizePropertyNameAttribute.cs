using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether the name of the associated property is displayed with parentheses in the Properties window. This class cannot be inherited.</summary>
	// Token: 0x02000438 RID: 1080
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ParenthesizePropertyNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class that indicates that the associated property should not be shown with parentheses.</summary>
		// Token: 0x06002368 RID: 9064 RVA: 0x00080CA0 File Offset: 0x0007EEA0
		public ParenthesizePropertyNameAttribute()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class, using the specified value to indicate whether the attribute is displayed with parentheses.</summary>
		/// <param name="needParenthesis">true if the name should be enclosed in parentheses; otherwise, false. </param>
		// Token: 0x06002369 RID: 9065 RVA: 0x00080CA9 File Offset: 0x0007EEA9
		public ParenthesizePropertyNameAttribute(bool needParenthesis)
		{
			this.needParenthesis = needParenthesis;
		}

		/// <summary>Gets a value indicating whether the Properties window displays the name of the property in parentheses in the Properties window.</summary>
		/// <returns>true if the property is displayed with parentheses; otherwise, false.</returns>
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x00080CB8 File Offset: 0x0007EEB8
		public bool NeedParenthesis
		{
			get
			{
				return this.needParenthesis;
			}
		}

		/// <summary>Compares the specified object to this object and tests for equality.</summary>
		/// <returns>true if equal; otherwise, false.</returns>
		/// <param name="o">The object to be compared. </param>
		// Token: 0x0600236B RID: 9067 RVA: 0x00080CC0 File Offset: 0x0007EEC0
		public override bool Equals(object o)
		{
			return o is ParenthesizePropertyNameAttribute && ((ParenthesizePropertyNameAttribute)o).NeedParenthesis == this.needParenthesis;
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x0600236C RID: 9068 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default value of the attribute; otherwise, false.</returns>
		// Token: 0x0600236D RID: 9069 RVA: 0x00080CDF File Offset: 0x0007EEDF
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ParenthesizePropertyNameAttribute.Default);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ParenthesizePropertyNameAttribute" /> class with a default value that indicates that the associated property should not be shown with parentheses. This field is read-only.</summary>
		// Token: 0x040010BE RID: 4286
		public static readonly ParenthesizePropertyNameAttribute Default = new ParenthesizePropertyNameAttribute();

		// Token: 0x040010BF RID: 4287
		private bool needParenthesis;
	}
}
