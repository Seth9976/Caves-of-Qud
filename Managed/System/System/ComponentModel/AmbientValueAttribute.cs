using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the value to pass to a property to cause the property to get its value from another source. This is known as ambience. This class cannot be inherited.</summary>
	// Token: 0x0200037D RID: 893
	[AttributeUsage(AttributeTargets.All)]
	public sealed class AmbientValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given the value and its type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the <paramref name="value" /> parameter. </param>
		/// <param name="value">The value for this attribute. </param>
		// Token: 0x06001D4F RID: 7503 RVA: 0x00068774 File Offset: 0x00066974
		public AmbientValueAttribute(Type type, string value)
		{
			try
			{
				this.Value = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value);
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a Unicode character for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06001D50 RID: 7504 RVA: 0x000687B0 File Offset: 0x000669B0
		public AmbientValueAttribute(char value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given an 8-bit unsigned integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06001D51 RID: 7505 RVA: 0x000687C4 File Offset: 0x000669C4
		public AmbientValueAttribute(byte value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 16-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06001D52 RID: 7506 RVA: 0x000687D8 File Offset: 0x000669D8
		public AmbientValueAttribute(short value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 32-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06001D53 RID: 7507 RVA: 0x000687EC File Offset: 0x000669EC
		public AmbientValueAttribute(int value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 64-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06001D54 RID: 7508 RVA: 0x00068800 File Offset: 0x00066A00
		public AmbientValueAttribute(long value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a single-precision floating point number for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06001D55 RID: 7509 RVA: 0x00068814 File Offset: 0x00066A14
		public AmbientValueAttribute(float value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a double-precision floating-point number for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06001D56 RID: 7510 RVA: 0x00068828 File Offset: 0x00066A28
		public AmbientValueAttribute(double value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a Boolean value for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06001D57 RID: 7511 RVA: 0x0006883C File Offset: 0x00066A3C
		public AmbientValueAttribute(bool value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a string for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06001D58 RID: 7512 RVA: 0x00068850 File Offset: 0x00066A50
		public AmbientValueAttribute(string value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given an object for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06001D59 RID: 7513 RVA: 0x00068850 File Offset: 0x00066A50
		public AmbientValueAttribute(object value)
		{
			this.Value = value;
		}

		/// <summary>Gets the object that is the value of this <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</summary>
		/// <returns>The object that is the value of this <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</returns>
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0006885F File Offset: 0x00066A5F
		public object Value { get; }

		/// <summary>Determines whether the specified <see cref="T:System.ComponentModel.AmbientValueAttribute" /> is equal to the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.AmbientValueAttribute" /> is equal to the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.AmbientValueAttribute" /> to compare with the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</param>
		// Token: 0x06001D5B RID: 7515 RVA: 0x00068868 File Offset: 0x00066A68
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			AmbientValueAttribute ambientValueAttribute = obj as AmbientValueAttribute;
			if (ambientValueAttribute == null)
			{
				return false;
			}
			if (this.Value == null)
			{
				return ambientValueAttribute.Value == null;
			}
			return this.Value.Equals(ambientValueAttribute.Value);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</returns>
		// Token: 0x06001D5C RID: 7516 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
