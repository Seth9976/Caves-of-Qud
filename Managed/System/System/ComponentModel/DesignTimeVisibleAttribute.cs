using System;

namespace System.ComponentModel
{
	/// <summary>
	///   <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> marks a component's visibility. If <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Yes" /> is present, a visual designer can show this component on a designer.</summary>
	// Token: 0x020003A6 RID: 934
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public sealed class DesignTimeVisibleAttribute : Attribute
	{
		/// <summary>Creates a new <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> with the <see cref="P:System.ComponentModel.DesignTimeVisibleAttribute.Visible" /> property set to the given value in <paramref name="visible" />.</summary>
		/// <param name="visible">The value that the <see cref="P:System.ComponentModel.DesignTimeVisibleAttribute.Visible" /> property will be set against. </param>
		// Token: 0x06001E75 RID: 7797 RVA: 0x0006C098 File Offset: 0x0006A298
		public DesignTimeVisibleAttribute(bool visible)
		{
			this.Visible = visible;
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> set to the default value of false.</summary>
		// Token: 0x06001E76 RID: 7798 RVA: 0x00003D9F File Offset: 0x00001F9F
		public DesignTimeVisibleAttribute()
		{
		}

		/// <summary>Gets or sets whether the component should be shown at design time.</summary>
		/// <returns>true if this component should be shown at design time, or false if it shouldn't.</returns>
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x0006C0A7 File Offset: 0x0006A2A7
		public bool Visible { get; }

		/// <param name="obj">The object to compare.</param>
		// Token: 0x06001E78 RID: 7800 RVA: 0x0006C0B0 File Offset: 0x0006A2B0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignTimeVisibleAttribute designTimeVisibleAttribute = obj as DesignTimeVisibleAttribute;
			return designTimeVisibleAttribute != null && designTimeVisibleAttribute.Visible == this.Visible;
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x0006C0DD File Offset: 0x0006A2DD
		public override int GetHashCode()
		{
			return typeof(DesignTimeVisibleAttribute).GetHashCode() ^ (this.Visible ? (-1) : 0);
		}

		/// <summary>Gets a value indicating if this instance is equal to the <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Default" /> value.</summary>
		/// <returns>true, if this instance is equal to the <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Default" /> value; otherwise, false.</returns>
		// Token: 0x06001E7A RID: 7802 RVA: 0x0006C0FB File Offset: 0x0006A2FB
		public override bool IsDefaultAttribute()
		{
			return this.Visible == DesignTimeVisibleAttribute.Default.Visible;
		}

		/// <summary>Marks a component as visible in a visual designer.</summary>
		// Token: 0x04000F55 RID: 3925
		public static readonly DesignTimeVisibleAttribute Yes = new DesignTimeVisibleAttribute(true);

		/// <summary>Marks a component as not visible in a visual designer.</summary>
		// Token: 0x04000F56 RID: 3926
		public static readonly DesignTimeVisibleAttribute No = new DesignTimeVisibleAttribute(false);

		/// <summary>The default visibility which is Yes.</summary>
		// Token: 0x04000F57 RID: 3927
		public static readonly DesignTimeVisibleAttribute Default = DesignTimeVisibleAttribute.Yes;
	}
}
