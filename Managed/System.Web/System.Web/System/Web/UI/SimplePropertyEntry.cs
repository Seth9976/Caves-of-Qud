using System;
using Unity;

namespace System.Web.UI
{
	/// <summary>Represents the definition of the control property and its value.</summary>
	// Token: 0x02000220 RID: 544
	public class SimplePropertyEntry : PropertyEntry
	{
		// Token: 0x0600162A RID: 5674 RVA: 0x00029E9A File Offset: 0x0002809A
		internal SimplePropertyEntry()
		{
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="M:System.Web.UI.IAttributeAccessor.SetAttribute(System.String,System.String)" /> method should be called for the property during code creation.</summary>
		/// <returns>true if <see cref="M:System.Web.UI.IAttributeAccessor.SetAttribute(System.String,System.String)" /> should be called; otherwise, false.</returns>
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x0003962A File Offset: 0x0003782A
		// (set) Token: 0x0600162C RID: 5676 RVA: 0x00039632 File Offset: 0x00037832
		public bool UseSetAttribute
		{
			get
			{
				return this.useSetAttribute;
			}
			set
			{
				this.useSetAttribute = value;
			}
		}

		/// <summary>Gets or sets the value of the property entry.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the value of the property entry.</returns>
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x0003963B File Offset: 0x0003783B
		// (set) Token: 0x0600162E RID: 5678 RVA: 0x00039643 File Offset: 0x00037843
		public object Value
		{
			get
			{
				return this.val;
			}
			set
			{
				this.val = value;
			}
		}

		/// <summary>Gets or sets the formatted string representation of the property entry.</summary>
		/// <returns>A <see cref="T:System.String" /> pertaining to the property entry.</returns>
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06001630 RID: 5680 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string PersistedValue
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001561 RID: 5473
		private bool useSetAttribute;

		// Token: 0x04001562 RID: 5474
		private object val;
	}
}
