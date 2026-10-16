using System;

namespace System.ComponentModel
{
	/// <summary>Specifies a property that is offered by an extender provider. This class cannot be inherited.</summary>
	// Token: 0x020003AE RID: 942
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ExtenderProvidedPropertyAttribute : Attribute
	{
		// Token: 0x06001ECE RID: 7886 RVA: 0x0006CAF3 File Offset: 0x0006ACF3
		internal static ExtenderProvidedPropertyAttribute Create(PropertyDescriptor extenderProperty, Type receiverType, IExtenderProvider provider)
		{
			return new ExtenderProvidedPropertyAttribute
			{
				ExtenderProperty = extenderProperty,
				ReceiverType = receiverType,
				Provider = provider
			};
		}

		/// <summary>Gets the property that is being provided.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> encapsulating the property that is being provided.</returns>
		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x0006CB0F File Offset: 0x0006AD0F
		// (set) Token: 0x06001ED1 RID: 7889 RVA: 0x0006CB17 File Offset: 0x0006AD17
		public PropertyDescriptor ExtenderProperty { get; private set; }

		/// <summary>Gets the extender provider that is providing the property.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IExtenderProvider" /> that is providing the property.</returns>
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x0006CB20 File Offset: 0x0006AD20
		// (set) Token: 0x06001ED3 RID: 7891 RVA: 0x0006CB28 File Offset: 0x0006AD28
		public IExtenderProvider Provider { get; private set; }

		/// <summary>Gets the type of object that can receive the property.</summary>
		/// <returns>A <see cref="T:System.Type" /> describing the type of object that can receive the property.</returns>
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x0006CB31 File Offset: 0x0006AD31
		// (set) Token: 0x06001ED5 RID: 7893 RVA: 0x0006CB39 File Offset: 0x0006AD39
		public Type ReceiverType { get; private set; }

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x06001ED6 RID: 7894 RVA: 0x0006CB44 File Offset: 0x0006AD44
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = obj as ExtenderProvidedPropertyAttribute;
			return extenderProvidedPropertyAttribute != null && extenderProvidedPropertyAttribute.ExtenderProperty.Equals(this.ExtenderProperty) && extenderProvidedPropertyAttribute.Provider.Equals(this.Provider) && extenderProvidedPropertyAttribute.ReceiverType.Equals(this.ReceiverType);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001ED7 RID: 7895 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Provides an indication whether the value of this instance is the default value for the derived class.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06001ED8 RID: 7896 RVA: 0x0006CB9A File Offset: 0x0006AD9A
		public override bool IsDefaultAttribute()
		{
			return this.ReceiverType == null;
		}
	}
}
