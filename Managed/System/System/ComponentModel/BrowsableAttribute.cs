using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property or event should be displayed in a Properties window.</summary>
	// Token: 0x02000367 RID: 871
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BrowsableAttribute" /> class.</summary>
		/// <param name="browsable">true if a property or event can be modified at design time; otherwise, false. The default is true. </param>
		// Token: 0x06001CDF RID: 7391 RVA: 0x00067E9D File Offset: 0x0006609D
		public BrowsableAttribute(bool browsable)
		{
			this.Browsable = browsable;
		}

		/// <summary>Gets a value indicating whether an object is browsable.</summary>
		/// <returns>true if the object is browsable; otherwise, false.</returns>
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x00067EAC File Offset: 0x000660AC
		public bool Browsable { get; }

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x06001CE1 RID: 7393 RVA: 0x00067EB4 File Offset: 0x000660B4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			BrowsableAttribute browsableAttribute = obj as BrowsableAttribute;
			bool? flag = ((browsableAttribute != null) ? new bool?(browsableAttribute.Browsable) : null);
			bool browsable = this.Browsable;
			return (flag.GetValueOrDefault() == browsable) & (flag != null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001CE2 RID: 7394 RVA: 0x00067F00 File Offset: 0x00066100
		public override int GetHashCode()
		{
			return this.Browsable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06001CE3 RID: 7395 RVA: 0x00067F1B File Offset: 0x0006611B
		public override bool IsDefaultAttribute()
		{
			return this.Equals(BrowsableAttribute.Default);
		}

		/// <summary>Specifies that a property or event can be modified at design time. This static field is read-only.</summary>
		// Token: 0x04000EC0 RID: 3776
		public static readonly BrowsableAttribute Yes = new BrowsableAttribute(true);

		/// <summary>Specifies that a property or event cannot be modified at design time. This static field is read-only.</summary>
		// Token: 0x04000EC1 RID: 3777
		public static readonly BrowsableAttribute No = new BrowsableAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.BrowsableAttribute" />, which is <see cref="F:System.ComponentModel.BrowsableAttribute.Yes" />. This static field is read-only.</summary>
		// Token: 0x04000EC2 RID: 3778
		public static readonly BrowsableAttribute Default = BrowsableAttribute.Yes;
	}
}
