using System;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Indicates whether a data field is editable.</summary>
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class EditableAttribute : Attribute
	{
		/// <summary>Gets a value that indicates whether a field is editable.</summary>
		/// <returns>true if the field is editable; otherwise, false.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000030BE File Offset: 0x000012BE
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000030C6 File Offset: 0x000012C6
		public bool AllowEdit { get; private set; }

		/// <summary>Gets or sets a value that indicates whether an initial value is enabled.</summary>
		/// <returns>true if an initial value is enabled; otherwise, false.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000030CF File Offset: 0x000012CF
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000030D7 File Offset: 0x000012D7
		public bool AllowInitialValue { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.EditableAttribute" /> class.</summary>
		/// <param name="allowEdit">true to specify that field is editable; otherwise, false.</param>
		// Token: 0x0600007E RID: 126 RVA: 0x000030E0 File Offset: 0x000012E0
		public EditableAttribute(bool allowEdit)
		{
			this.AllowEdit = allowEdit;
			this.AllowInitialValue = allowEdit;
		}
	}
}
