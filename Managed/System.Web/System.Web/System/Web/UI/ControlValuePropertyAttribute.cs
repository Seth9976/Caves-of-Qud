using System;

namespace System.Web.UI
{
	/// <summary>Specifies the default property of a control that a <see cref="T:System.Web.UI.WebControls.ControlParameter" /> object binds to at run time. This class cannot be inherited.</summary>
	// Token: 0x020001B7 RID: 439
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class ControlValuePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ControlValuePropertyAttribute" /> class using the specified property name.</summary>
		/// <param name="name">The default property for the control.</param>
		// Token: 0x060011C2 RID: 4546 RVA: 0x0002F180 File Offset: 0x0002D380
		public ControlValuePropertyAttribute(string name)
		{
			this.propertyName = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ControlValuePropertyAttribute" /> class using the specified property name and default value.</summary>
		/// <param name="name">The default property for the control.</param>
		/// <param name="defaultValue">The default value for the default property.</param>
		// Token: 0x060011C3 RID: 4547 RVA: 0x0002F18F File Offset: 0x0002D38F
		public ControlValuePropertyAttribute(string name, object defaultValue)
		{
			this.propertyName = name;
			this.propertyValue = defaultValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ControlValuePropertyAttribute" /> class using the specified property name and default value. The default value is also converted to the specified data type.</summary>
		/// <param name="name">The default property for the control.</param>
		/// <param name="type">The <see cref="T:System.Type" /> to which the default value is converted.</param>
		/// <param name="defaultValue">The default value for the default property.</param>
		// Token: 0x060011C4 RID: 4548 RVA: 0x0002F1A5 File Offset: 0x0002D3A5
		public ControlValuePropertyAttribute(string name, Type type, string defaultValue)
		{
			this.propertyName = name;
			this.propertyValue = defaultValue;
			this.propertyType = type;
		}

		/// <summary>Gets the default property for a control.</summary>
		/// <returns>The default property for a control.</returns>
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0002F1C2 File Offset: 0x0002D3C2
		public string Name
		{
			get
			{
				return this.propertyName;
			}
		}

		/// <summary>Gets the default value for the default property of a control.</summary>
		/// <returns>The default value for the default property of a control.</returns>
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0002F1CA File Offset: 0x0002D3CA
		public object DefaultValue
		{
			get
			{
				return this.propertyValue;
			}
		}

		/// <summary>Determines whether the current instance of the <see cref="T:System.Web.UI.ControlValuePropertyAttribute" /> object is equal to the specified object.</summary>
		/// <returns>true if the object contained in the <paramref name="obj" /> parameter is equal to the current instance of <see cref="T:System.Web.UI.ControlValuePropertyAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with this instance.</param>
		// Token: 0x060011C7 RID: 4551 RVA: 0x0002F1D4 File Offset: 0x0002D3D4
		public override bool Equals(object obj)
		{
			if (obj != null && obj is ControlValuePropertyAttribute)
			{
				ControlValuePropertyAttribute controlValuePropertyAttribute = (ControlValuePropertyAttribute)obj;
				return this.propertyName == controlValuePropertyAttribute.propertyName && this.propertyValue == controlValuePropertyAttribute.propertyValue && this.propertyType == controlValuePropertyAttribute.propertyType;
			}
			return false;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060011C8 RID: 4552 RVA: 0x0002F229 File Offset: 0x0002D429
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04001404 RID: 5124
		private string propertyName;

		// Token: 0x04001405 RID: 5125
		private object propertyValue;

		// Token: 0x04001406 RID: 5126
		private Type propertyType;
	}
}
