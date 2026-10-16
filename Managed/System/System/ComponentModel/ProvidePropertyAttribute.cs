using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the name of the property that an implementer of <see cref="T:System.ComponentModel.IExtenderProvider" /> offers to other components. This class cannot be inherited</summary>
	// Token: 0x020003E5 RID: 997
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class ProvidePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProvidePropertyAttribute" /> class with the name of the property and its <see cref="T:System.Type" />.</summary>
		/// <param name="propertyName">The name of the property extending to an object of the specified type. </param>
		/// <param name="receiverType">The <see cref="T:System.Type" /> of the data type of the object that can receive the property. </param>
		// Token: 0x060020AD RID: 8365 RVA: 0x00070C0C File Offset: 0x0006EE0C
		public ProvidePropertyAttribute(string propertyName, Type receiverType)
		{
			this.PropertyName = propertyName;
			this.ReceiverTypeName = receiverType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProvidePropertyAttribute" /> class with the name of the property and the type of its receiver.</summary>
		/// <param name="propertyName">The name of the property extending to an object of the specified type. </param>
		/// <param name="receiverTypeName">The name of the data type this property can extend. </param>
		// Token: 0x060020AE RID: 8366 RVA: 0x00070C27 File Offset: 0x0006EE27
		public ProvidePropertyAttribute(string propertyName, string receiverTypeName)
		{
			this.PropertyName = propertyName;
			this.ReceiverTypeName = receiverTypeName;
		}

		/// <summary>Gets the name of a property that this class provides.</summary>
		/// <returns>The name of a property that this class provides.</returns>
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060020AF RID: 8367 RVA: 0x00070C3D File Offset: 0x0006EE3D
		public string PropertyName { get; }

		/// <summary>Gets the name of the data type this property can extend.</summary>
		/// <returns>The name of the data type this property can extend.</returns>
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x00070C45 File Offset: 0x0006EE45
		public string ReceiverTypeName { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.ProvidePropertyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x060020B1 RID: 8369 RVA: 0x00070C50 File Offset: 0x0006EE50
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ProvidePropertyAttribute providePropertyAttribute = obj as ProvidePropertyAttribute;
			return providePropertyAttribute != null && providePropertyAttribute.PropertyName == this.PropertyName && providePropertyAttribute.ReceiverTypeName == this.ReceiverTypeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ProvidePropertyAttribute" />.</returns>
		// Token: 0x060020B2 RID: 8370 RVA: 0x00070C93 File Offset: 0x0006EE93
		public override int GetHashCode()
		{
			return this.PropertyName.GetHashCode() ^ this.ReceiverTypeName.GetHashCode();
		}

		/// <summary>Gets a unique identifier for this attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x00070CAC File Offset: 0x0006EEAC
		public override object TypeId
		{
			get
			{
				return base.GetType().FullName + this.PropertyName;
			}
		}
	}
}
