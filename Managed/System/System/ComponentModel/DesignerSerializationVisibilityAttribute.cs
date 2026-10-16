using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the type of persistence to use when serializing a property on a component at design time.</summary>
	// Token: 0x0200036D RID: 877
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class DesignerSerializationVisibilityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerSerializationVisibilityAttribute" /> class using the specified <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> value.</summary>
		/// <param name="visibility">One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> values. </param>
		// Token: 0x06001D00 RID: 7424 RVA: 0x000681F9 File Offset: 0x000663F9
		public DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility visibility)
		{
			this.Visibility = visibility;
		}

		/// <summary>Gets a value indicating the basic serialization mode a serializer should use when determining whether and how to persist the value of a property.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> values. The default is <see cref="F:System.ComponentModel.DesignerSerializationVisibility.Visible" />.</returns>
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x00068208 File Offset: 0x00066408
		public DesignerSerializationVisibility Visibility { get; }

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x06001D02 RID: 7426 RVA: 0x00068210 File Offset: 0x00066410
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = obj as DesignerSerializationVisibilityAttribute;
			DesignerSerializationVisibility? designerSerializationVisibility = ((designerSerializationVisibilityAttribute != null) ? new DesignerSerializationVisibility?(designerSerializationVisibilityAttribute.Visibility) : null);
			DesignerSerializationVisibility visibility = this.Visibility;
			return (designerSerializationVisibility.GetValueOrDefault() == visibility) & (designerSerializationVisibility != null);
		}

		/// <summary>Returns the hash code for this object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001D03 RID: 7427 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the attribute is set to the default value; otherwise, false.</returns>
		// Token: 0x06001D04 RID: 7428 RVA: 0x0006825C File Offset: 0x0006645C
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DesignerSerializationVisibilityAttribute.Default);
		}

		/// <summary>Specifies that a serializer should serialize the contents of the property, rather than the property itself. This field is read-only.</summary>
		// Token: 0x04000ED3 RID: 3795
		public static readonly DesignerSerializationVisibilityAttribute Content = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content);

		/// <summary>Specifies that a serializer should not serialize the value of the property. This static field is read-only.</summary>
		// Token: 0x04000ED4 RID: 3796
		public static readonly DesignerSerializationVisibilityAttribute Hidden = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden);

		/// <summary>Specifies that a serializer should be allowed to serialize the value of the property. This static field is read-only.</summary>
		// Token: 0x04000ED5 RID: 3797
		public static readonly DesignerSerializationVisibilityAttribute Visible = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.DesignerSerializationVisibilityAttribute.Visible" />, that is, a visual designer uses default rules to generate the value of a property. This static field is read-only.</summary>
		// Token: 0x04000ED6 RID: 3798
		public static readonly DesignerSerializationVisibilityAttribute Default = DesignerSerializationVisibilityAttribute.Visible;
	}
}
