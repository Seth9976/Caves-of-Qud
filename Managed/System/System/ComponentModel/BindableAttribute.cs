using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a member is typically used for binding. This class cannot be inherited.</summary>
	// Token: 0x02000385 RID: 901
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class with a Boolean value.</summary>
		/// <param name="bindable">true to use property for binding; otherwise, false.</param>
		// Token: 0x06001D89 RID: 7561 RVA: 0x00069119 File Offset: 0x00067319
		public BindableAttribute(bool bindable)
			: this(bindable, BindingDirection.OneWay)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <param name="bindable">true to use property for binding; otherwise, false.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.BindingDirection" /> values.</param>
		// Token: 0x06001D8A RID: 7562 RVA: 0x00069123 File Offset: 0x00067323
		public BindableAttribute(bool bindable, BindingDirection direction)
		{
			this.Bindable = bindable;
			this.Direction = direction;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class with one of the <see cref="T:System.ComponentModel.BindableSupport" /> values.</summary>
		/// <param name="flags">One of the <see cref="T:System.ComponentModel.BindableSupport" /> values. </param>
		// Token: 0x06001D8B RID: 7563 RVA: 0x00069139 File Offset: 0x00067339
		public BindableAttribute(BindableSupport flags)
			: this(flags, BindingDirection.OneWay)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <param name="flags">One of the <see cref="T:System.ComponentModel.BindableSupport" /> values. </param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.BindingDirection" /> values.</param>
		// Token: 0x06001D8C RID: 7564 RVA: 0x00069143 File Offset: 0x00067343
		public BindableAttribute(BindableSupport flags, BindingDirection direction)
		{
			this.Bindable = flags > BindableSupport.No;
			this._isDefault = flags == BindableSupport.Default;
			this.Direction = direction;
		}

		/// <summary>Gets a value indicating that a property is typically used for binding.</summary>
		/// <returns>true if the property is typically used for binding; otherwise, false.</returns>
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x00069166 File Offset: 0x00067366
		public bool Bindable { get; }

		/// <summary>Gets a value indicating the direction or directions of this property's data binding.</summary>
		/// <returns>The direction of this property’s data binding.</returns>
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0006916E File Offset: 0x0006736E
		public BindingDirection Direction { get; }

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.BindableAttribute" /> objects are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.BindableAttribute" /> is equal to the current <see cref="T:System.ComponentModel.BindableAttribute" />; false if it is not equal.</returns>
		/// <param name="obj">The object to compare.</param>
		// Token: 0x06001D8F RID: 7567 RVA: 0x00069176 File Offset: 0x00067376
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is BindableAttribute && ((BindableAttribute)obj).Bindable == this.Bindable);
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.BindableAttribute" />.</returns>
		// Token: 0x06001D90 RID: 7568 RVA: 0x000691A0 File Offset: 0x000673A0
		public override int GetHashCode()
		{
			return this.Bindable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06001D91 RID: 7569 RVA: 0x000691BB File Offset: 0x000673BB
		public override bool IsDefaultAttribute()
		{
			return this.Equals(BindableAttribute.Default) || this._isDefault;
		}

		/// <summary>Specifies that a property is typically used for binding. This field is read-only.</summary>
		// Token: 0x04000EFE RID: 3838
		public static readonly BindableAttribute Yes = new BindableAttribute(true);

		/// <summary>Specifies that a property is not typically used for binding. This field is read-only.</summary>
		// Token: 0x04000EFF RID: 3839
		public static readonly BindableAttribute No = new BindableAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.BindableAttribute" />, which is <see cref="F:System.ComponentModel.BindableAttribute.No" />. This field is read-only.</summary>
		// Token: 0x04000F00 RID: 3840
		public static readonly BindableAttribute Default = BindableAttribute.No;

		// Token: 0x04000F01 RID: 3841
		private bool _isDefault;
	}
}
