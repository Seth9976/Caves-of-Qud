using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default binding property for a component. This class cannot be inherited.</summary>
	// Token: 0x0200039D RID: 925
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultBindingPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class using no parameters. </summary>
		// Token: 0x06001E3A RID: 7738 RVA: 0x00003D9F File Offset: 0x00001F9F
		public DefaultBindingPropertyAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class using the specified property name.</summary>
		/// <param name="name">The name of the default binding property.</param>
		// Token: 0x06001E3B RID: 7739 RVA: 0x0006B989 File Offset: 0x00069B89
		public DefaultBindingPropertyAttribute(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets the name of the default binding property for the component to which the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> is bound.</summary>
		/// <returns>The name of the default binding property for the component to which the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> is bound.</returns>
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x0006B998 File Offset: 0x00069B98
		public string Name { get; }

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> instance. </summary>
		/// <returns>true if the object is equal to the current instance; otherwise, false, indicating they are not equal.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> instance</param>
		// Token: 0x06001E3D RID: 7741 RVA: 0x0006B9A0 File Offset: 0x00069BA0
		public override bool Equals(object obj)
		{
			DefaultBindingPropertyAttribute defaultBindingPropertyAttribute = obj as DefaultBindingPropertyAttribute;
			return defaultBindingPropertyAttribute != null && defaultBindingPropertyAttribute.Name == this.Name;
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001E3E RID: 7742 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class.</summary>
		// Token: 0x04000F39 RID: 3897
		public static readonly DefaultBindingPropertyAttribute Default = new DefaultBindingPropertyAttribute();
	}
}
