using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Specifies the default value for a property.</summary>
	// Token: 0x02000361 RID: 865
	[AttributeUsage(AttributeTargets.All)]
	public class DefaultValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class, converting the specified value to the specified type, and using an invariant culture as the translation context.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to convert the value to. </param>
		/// <param name="value">A <see cref="T:System.String" /> that can be converted to the type using the <see cref="T:System.ComponentModel.TypeConverter" /> for the type and the U.S. English culture. </param>
		// Token: 0x06001C9C RID: 7324 RVA: 0x000676FC File Offset: 0x000658FC
		public DefaultValueAttribute(Type type, string value)
		{
			try
			{
				object obj;
				if (DefaultValueAttribute.<.ctor>g__TryConvertFromInvariantString|2_0(type, value, out obj))
				{
					this._value = obj;
				}
				else if (type.IsSubclassOf(typeof(Enum)))
				{
					this._value = Enum.Parse(type, value, true);
				}
				else if (type == typeof(TimeSpan))
				{
					this._value = TimeSpan.Parse(value);
				}
				else
				{
					this._value = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
				}
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a Unicode character.</summary>
		/// <param name="value">A Unicode character that is the default value. </param>
		// Token: 0x06001C9D RID: 7325 RVA: 0x00067794 File Offset: 0x00065994
		public DefaultValueAttribute(char value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using an 8-bit unsigned integer.</summary>
		/// <param name="value">An 8-bit unsigned integer that is the default value. </param>
		// Token: 0x06001C9E RID: 7326 RVA: 0x000677A8 File Offset: 0x000659A8
		public DefaultValueAttribute(byte value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 16-bit signed integer.</summary>
		/// <param name="value">A 16-bit signed integer that is the default value. </param>
		// Token: 0x06001C9F RID: 7327 RVA: 0x000677BC File Offset: 0x000659BC
		public DefaultValueAttribute(short value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 32-bit signed integer.</summary>
		/// <param name="value">A 32-bit signed integer that is the default value. </param>
		// Token: 0x06001CA0 RID: 7328 RVA: 0x000677D0 File Offset: 0x000659D0
		public DefaultValueAttribute(int value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 64-bit signed integer.</summary>
		/// <param name="value">A 64-bit signed integer that is the default value. </param>
		// Token: 0x06001CA1 RID: 7329 RVA: 0x000677E4 File Offset: 0x000659E4
		public DefaultValueAttribute(long value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a single-precision floating point number.</summary>
		/// <param name="value">A single-precision floating point number that is the default value. </param>
		// Token: 0x06001CA2 RID: 7330 RVA: 0x000677F8 File Offset: 0x000659F8
		public DefaultValueAttribute(float value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a double-precision floating point number.</summary>
		/// <param name="value">A double-precision floating point number that is the default value. </param>
		// Token: 0x06001CA3 RID: 7331 RVA: 0x0006780C File Offset: 0x00065A0C
		public DefaultValueAttribute(double value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Boolean" /> that is the default value. </param>
		// Token: 0x06001CA4 RID: 7332 RVA: 0x00067820 File Offset: 0x00065A20
		public DefaultValueAttribute(bool value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a <see cref="T:System.String" />.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that is the default value. </param>
		// Token: 0x06001CA5 RID: 7333 RVA: 0x00067834 File Offset: 0x00065A34
		public DefaultValueAttribute(string value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that represents the default value. </param>
		// Token: 0x06001CA6 RID: 7334 RVA: 0x00067834 File Offset: 0x00065A34
		public DefaultValueAttribute(object value)
		{
			this._value = value;
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00067843 File Offset: 0x00065A43
		[CLSCompliant(false)]
		public DefaultValueAttribute(sbyte value)
		{
			this._value = value;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00067857 File Offset: 0x00065A57
		[CLSCompliant(false)]
		public DefaultValueAttribute(ushort value)
		{
			this._value = value;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0006786B File Offset: 0x00065A6B
		[CLSCompliant(false)]
		public DefaultValueAttribute(uint value)
		{
			this._value = value;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0006787F File Offset: 0x00065A7F
		[CLSCompliant(false)]
		public DefaultValueAttribute(ulong value)
		{
			this._value = value;
		}

		/// <summary>Gets the default value of the property this attribute is bound to.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the default value of the property this attribute is bound to.</returns>
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x00067893 File Offset: 0x00065A93
		public virtual object Value
		{
			get
			{
				return this._value;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultValueAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001CAC RID: 7340 RVA: 0x0006789C File Offset: 0x00065A9C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			if (defaultValueAttribute == null)
			{
				return false;
			}
			if (this.Value != null)
			{
				return this.Value.Equals(defaultValueAttribute.Value);
			}
			return defaultValueAttribute.Value == null;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Sets the default value for the property to which this attribute is bound.</summary>
		/// <param name="value">The default value.</param>
		// Token: 0x06001CAE RID: 7342 RVA: 0x000678E6 File Offset: 0x00065AE6
		protected void SetValue(object value)
		{
			this._value = value;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x000678F0 File Offset: 0x00065AF0
		[CompilerGenerated]
		internal static bool <.ctor>g__TryConvertFromInvariantString|2_0(Type typeToConvert, string stringValue, out object conversionResult)
		{
			conversionResult = null;
			if (DefaultValueAttribute.s_convertFromInvariantString == null)
			{
				Type type = Type.GetType("System.ComponentModel.TypeDescriptor, System.ComponentModel.TypeConverter", false);
				Volatile.Write<object>(ref DefaultValueAttribute.s_convertFromInvariantString, (type == null) ? new object() : Delegate.CreateDelegate(typeof(Func<Type, string, object>), type, "ConvertFromInvariantString", false));
			}
			Func<Type, string, object> func = DefaultValueAttribute.s_convertFromInvariantString as Func<Type, string, object>;
			if (func == null)
			{
				return false;
			}
			conversionResult = func(typeToConvert, stringValue);
			return true;
		}

		// Token: 0x04000EAC RID: 3756
		private object _value;

		// Token: 0x04000EAD RID: 3757
		private static object s_convertFromInvariantString;
	}
}
