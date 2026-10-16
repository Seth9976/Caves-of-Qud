using System;

namespace System.ComponentModel
{
	/// <summary>Specifies when a component property can be bound to an application setting.</summary>
	// Token: 0x020003EE RID: 1006
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsBindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.SettingsBindableAttribute" /> class. </summary>
		/// <param name="bindable">true to specify that a property is appropriate to bind settings to; otherwise, false.</param>
		// Token: 0x060020E4 RID: 8420 RVA: 0x00071811 File Offset: 0x0006FA11
		public SettingsBindableAttribute(bool bindable)
		{
			this.Bindable = bindable;
		}

		/// <summary>Gets a value indicating whether a property is appropriate to bind settings to. </summary>
		/// <returns>true if the property is appropriate to bind settings to; otherwise, false.</returns>
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x00071820 File Offset: 0x0006FA20
		public bool Bindable { get; }

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.SettingsBindableAttribute" /> objects are equal.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">The value to compare to.</param>
		// Token: 0x060020E6 RID: 8422 RVA: 0x00071828 File Offset: 0x0006FA28
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is SettingsBindableAttribute && ((SettingsBindableAttribute)obj).Bindable == this.Bindable);
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060020E7 RID: 8423 RVA: 0x00071850 File Offset: 0x0006FA50
		public override int GetHashCode()
		{
			return this.Bindable.GetHashCode();
		}

		/// <summary>Specifies that a property is appropriate to bind settings to.</summary>
		// Token: 0x04001008 RID: 4104
		public static readonly SettingsBindableAttribute Yes = new SettingsBindableAttribute(true);

		/// <summary>Specifies that a property is not appropriate to bind settings to.</summary>
		// Token: 0x04001009 RID: 4105
		public static readonly SettingsBindableAttribute No = new SettingsBindableAttribute(false);
	}
}
