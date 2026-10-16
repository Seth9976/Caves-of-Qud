using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that this property can be combined with properties belonging to other objects in a Properties window.</summary>
	// Token: 0x02000379 RID: 889
	[AttributeUsage(AttributeTargets.All)]
	public sealed class MergablePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MergablePropertyAttribute" /> class.</summary>
		/// <param name="allowMerge">true if this property can be combined with properties belonging to other objects in a Properties window; otherwise, false. </param>
		// Token: 0x06001D3B RID: 7483 RVA: 0x00068622 File Offset: 0x00066822
		public MergablePropertyAttribute(bool allowMerge)
		{
			this.AllowMerge = allowMerge;
		}

		/// <summary>Gets a value indicating whether this property can be combined with properties belonging to other objects in a Properties window.</summary>
		/// <returns>true if this property can be combined with properties belonging to other objects in a Properties window; otherwise, false.</returns>
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x00068631 File Offset: 0x00066831
		public bool AllowMerge { get; }

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x06001D3D RID: 7485 RVA: 0x0006863C File Offset: 0x0006683C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			MergablePropertyAttribute mergablePropertyAttribute = obj as MergablePropertyAttribute;
			bool? flag = ((mergablePropertyAttribute != null) ? new bool?(mergablePropertyAttribute.AllowMerge) : null);
			bool allowMerge = this.AllowMerge;
			return (flag.GetValueOrDefault() == allowMerge) & (flag != null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.MergablePropertyAttribute" />.</returns>
		// Token: 0x06001D3E RID: 7486 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06001D3F RID: 7487 RVA: 0x00068688 File Offset: 0x00066888
		public override bool IsDefaultAttribute()
		{
			return this.Equals(MergablePropertyAttribute.Default);
		}

		/// <summary>Specifies that a property can be combined with properties belonging to other objects in a Properties window. This static field is read-only.</summary>
		// Token: 0x04000EE8 RID: 3816
		public static readonly MergablePropertyAttribute Yes = new MergablePropertyAttribute(true);

		/// <summary>Specifies that a property cannot be combined with properties belonging to other objects in a Properties window. This static field is read-only.</summary>
		// Token: 0x04000EE9 RID: 3817
		public static readonly MergablePropertyAttribute No = new MergablePropertyAttribute(false);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.MergablePropertyAttribute.Yes" />, that is a property can be combined with properties belonging to other objects in a Properties window. This static field is read-only.</summary>
		// Token: 0x04000EEA RID: 3818
		public static readonly MergablePropertyAttribute Default = MergablePropertyAttribute.Yes;
	}
}
