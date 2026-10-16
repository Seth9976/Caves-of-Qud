using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that a property or method is viewable in an editor. This class cannot be inherited.</summary>
	// Token: 0x02000362 RID: 866
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
	public sealed class EditorBrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorBrowsableAttribute" /> class with an <see cref="T:System.ComponentModel.EditorBrowsableState" />.</summary>
		/// <param name="state">The <see cref="T:System.ComponentModel.EditorBrowsableState" /> to set <see cref="P:System.ComponentModel.EditorBrowsableAttribute.State" /> to. </param>
		// Token: 0x06001CB0 RID: 7344 RVA: 0x0006795E File Offset: 0x00065B5E
		public EditorBrowsableAttribute(EditorBrowsableState state)
		{
			this.browsableState = state;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorBrowsableAttribute" /> class with <see cref="P:System.ComponentModel.EditorBrowsableAttribute.State" /> set to the default state.</summary>
		// Token: 0x06001CB1 RID: 7345 RVA: 0x0006796D File Offset: 0x00065B6D
		public EditorBrowsableAttribute()
			: this(EditorBrowsableState.Always)
		{
		}

		/// <summary>Gets the browsable state of the property or method.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EditorBrowsableState" /> that is the browsable state of the property or method.</returns>
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x00067976 File Offset: 0x00065B76
		public EditorBrowsableState State
		{
			get
			{
				return this.browsableState;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.EditorBrowsableAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001CB3 RID: 7347 RVA: 0x00067980 File Offset: 0x00065B80
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			EditorBrowsableAttribute editorBrowsableAttribute = obj as EditorBrowsableAttribute;
			return editorBrowsableAttribute != null && editorBrowsableAttribute.browsableState == this.browsableState;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000EAE RID: 3758
		private EditorBrowsableState browsableState;
	}
}
