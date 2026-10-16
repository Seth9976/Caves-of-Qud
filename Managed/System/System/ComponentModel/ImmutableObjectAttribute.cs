using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that an object has no subproperties capable of being edited. This class cannot be inherited.</summary>
	// Token: 0x02000375 RID: 885
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ImmutableObjectAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ImmutableObjectAttribute" /> class.</summary>
		/// <param name="immutable">true if the object is immutable; otherwise, false. </param>
		// Token: 0x06001D28 RID: 7464 RVA: 0x0006848F File Offset: 0x0006668F
		public ImmutableObjectAttribute(bool immutable)
		{
			this.Immutable = immutable;
		}

		/// <summary>Gets whether the object is immutable.</summary>
		/// <returns>true if the object is immutable; otherwise, false.</returns>
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x0006849E File Offset: 0x0006669E
		public bool Immutable { get; }

		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x06001D2A RID: 7466 RVA: 0x000684A8 File Offset: 0x000666A8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ImmutableObjectAttribute immutableObjectAttribute = obj as ImmutableObjectAttribute;
			bool? flag = ((immutableObjectAttribute != null) ? new bool?(immutableObjectAttribute.Immutable) : null);
			bool immutable = this.Immutable;
			return (flag.GetValueOrDefault() == immutable) & (flag != null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</returns>
		// Token: 0x06001D2B RID: 7467 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the value of this instance is the default value.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06001D2C RID: 7468 RVA: 0x000684F4 File Offset: 0x000666F4
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ImmutableObjectAttribute.Default);
		}

		/// <summary>Specifies that an object has no subproperties that can be edited. This static field is read-only.</summary>
		// Token: 0x04000EDF RID: 3807
		public static readonly ImmutableObjectAttribute Yes = new ImmutableObjectAttribute(true);

		/// <summary>Specifies that an object has at least one editable subproperty. This static field is read-only.</summary>
		// Token: 0x04000EE0 RID: 3808
		public static readonly ImmutableObjectAttribute No = new ImmutableObjectAttribute(false);

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</summary>
		// Token: 0x04000EE1 RID: 3809
		public static readonly ImmutableObjectAttribute Default = ImmutableObjectAttribute.No;
	}
}
