using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that a list can be used as a data source. A visual designer should use this attribute to determine whether to display a particular list in a data-binding picker. This class cannot be inherited.</summary>
	// Token: 0x020003CF RID: 975
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ListBindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListBindableAttribute" /> class using a value to indicate whether the list is bindable.</summary>
		/// <param name="listBindable">true if the list is bindable; otherwise, false. </param>
		// Token: 0x06001F74 RID: 8052 RVA: 0x0006D4A2 File Offset: 0x0006B6A2
		public ListBindableAttribute(bool listBindable)
		{
			this.ListBindable = listBindable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListBindableAttribute" /> class using <see cref="T:System.ComponentModel.BindableSupport" /> to indicate whether the list is bindable.</summary>
		/// <param name="flags">A <see cref="T:System.ComponentModel.BindableSupport" /> that indicates whether the list is bindable. </param>
		// Token: 0x06001F75 RID: 8053 RVA: 0x0006D4B1 File Offset: 0x0006B6B1
		public ListBindableAttribute(BindableSupport flags)
		{
			this.ListBindable = flags > BindableSupport.No;
			this._isDefault = flags == BindableSupport.Default;
		}

		/// <summary>Gets whether the list is bindable.</summary>
		/// <returns>true if the list is bindable; otherwise, false.</returns>
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x0006D4CD File Offset: 0x0006B6CD
		public bool ListBindable { get; }

		/// <summary>Returns whether the object passed is equal to this <see cref="T:System.ComponentModel.ListBindableAttribute" />.</summary>
		/// <returns>true if the object passed is equal to this <see cref="T:System.ComponentModel.ListBindableAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The object to test equality with. </param>
		// Token: 0x06001F77 RID: 8055 RVA: 0x0006D4D8 File Offset: 0x0006B6D8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ListBindableAttribute listBindableAttribute = obj as ListBindableAttribute;
			return listBindableAttribute != null && listBindableAttribute.ListBindable == this.ListBindable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ListBindableAttribute" />.</returns>
		// Token: 0x06001F78 RID: 8056 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns whether <see cref="P:System.ComponentModel.ListBindableAttribute.ListBindable" /> is set to the default value.</summary>
		/// <returns>true if <see cref="P:System.ComponentModel.ListBindableAttribute.ListBindable" /> is set to the default value; otherwise, false.</returns>
		// Token: 0x06001F79 RID: 8057 RVA: 0x0006D505 File Offset: 0x0006B705
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ListBindableAttribute.Default) || this._isDefault;
		}

		/// <summary>Specifies that the list is bindable. This static field is read-only.</summary>
		// Token: 0x04000F7B RID: 3963
		public static readonly ListBindableAttribute Yes = new ListBindableAttribute(true);

		/// <summary>Specifies that the list is not bindable. This static field is read-only.</summary>
		// Token: 0x04000F7C RID: 3964
		public static readonly ListBindableAttribute No = new ListBindableAttribute(false);

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ListBindableAttribute" />.</summary>
		// Token: 0x04000F7D RID: 3965
		public static readonly ListBindableAttribute Default = ListBindableAttribute.Yes;

		// Token: 0x04000F7E RID: 3966
		private bool _isDefault;
	}
}
