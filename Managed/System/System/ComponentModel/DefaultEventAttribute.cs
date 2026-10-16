using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default event for a component.</summary>
	// Token: 0x0200039E RID: 926
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultEventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultEventAttribute" /> class.</summary>
		/// <param name="name">The name of the default event for the component this attribute is bound to. </param>
		// Token: 0x06001E40 RID: 7744 RVA: 0x0006B9D6 File Offset: 0x00069BD6
		public DefaultEventAttribute(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets the name of the default event for the component this attribute is bound to.</summary>
		/// <returns>The name of the default event for the component this attribute is bound to. The default value is null.</returns>
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x0006B9E5 File Offset: 0x00069BE5
		public string Name { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultEventAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001E42 RID: 7746 RVA: 0x0006B9F0 File Offset: 0x00069BF0
		public override bool Equals(object obj)
		{
			DefaultEventAttribute defaultEventAttribute = obj as DefaultEventAttribute;
			return defaultEventAttribute != null && defaultEventAttribute.Name == this.Name;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001E43 RID: 7747 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DefaultEventAttribute" />, which is null. This static field is read-only.</summary>
		// Token: 0x04000F3B RID: 3899
		public static readonly DefaultEventAttribute Default = new DefaultEventAttribute(null);
	}
}
