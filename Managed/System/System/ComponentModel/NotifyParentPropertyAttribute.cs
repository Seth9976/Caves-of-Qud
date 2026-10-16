using System;

namespace System.ComponentModel
{
	/// <summary>Indicates that the parent property is notified when the value of the property that this attribute is applied to is modified. This class cannot be inherited.</summary>
	// Token: 0x02000437 RID: 1079
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NotifyParentPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NotifyParentPropertyAttribute" /> class, using the specified value to determine whether the parent property is notified of changes to the value of the property.</summary>
		/// <param name="notifyParent">true if the parent should be notified of changes; otherwise, false. </param>
		// Token: 0x06002362 RID: 9058 RVA: 0x00080C32 File Offset: 0x0007EE32
		public NotifyParentPropertyAttribute(bool notifyParent)
		{
			this.notifyParent = notifyParent;
		}

		/// <summary>Gets or sets a value indicating whether the parent property should be notified of changes to the value of the property.</summary>
		/// <returns>true if the parent property should be notified of changes; otherwise, false.</returns>
		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x00080C41 File Offset: 0x0007EE41
		public bool NotifyParent
		{
			get
			{
				return this.notifyParent;
			}
		}

		/// <summary>Gets a value indicating whether the specified object is the same as the current object.</summary>
		/// <returns>true if the object is the same as this object; otherwise, false.</returns>
		/// <param name="obj">The object to test for equality. </param>
		// Token: 0x06002364 RID: 9060 RVA: 0x00080C49 File Offset: 0x0007EE49
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is NotifyParentPropertyAttribute && ((NotifyParentPropertyAttribute)obj).NotifyParent == this.notifyParent);
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x06002365 RID: 9061 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default value of the attribute; otherwise, false.</returns>
		// Token: 0x06002366 RID: 9062 RVA: 0x00080C71 File Offset: 0x0007EE71
		public override bool IsDefaultAttribute()
		{
			return this.Equals(NotifyParentPropertyAttribute.Default);
		}

		/// <summary>Indicates that the parent property is notified of changes to the value of the property. This field is read-only.</summary>
		// Token: 0x040010BA RID: 4282
		public static readonly NotifyParentPropertyAttribute Yes = new NotifyParentPropertyAttribute(true);

		/// <summary>Indicates that the parent property is not be notified of changes to the value of the property. This field is read-only.</summary>
		// Token: 0x040010BB RID: 4283
		public static readonly NotifyParentPropertyAttribute No = new NotifyParentPropertyAttribute(false);

		/// <summary>Indicates the default attribute state, that the property should not notify the parent property of changes to its value. This field is read-only.</summary>
		// Token: 0x040010BC RID: 4284
		public static readonly NotifyParentPropertyAttribute Default = NotifyParentPropertyAttribute.No;

		// Token: 0x040010BD RID: 4285
		private bool notifyParent;
	}
}
