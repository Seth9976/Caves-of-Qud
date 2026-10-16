using System;

namespace System.Web.UI.Design
{
	/// <summary>Indicates whether a control designer requires a preview instance of the control at design time. This class cannot be inherited.</summary>
	// Token: 0x020000A1 RID: 161
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SupportsPreviewControlAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" /> class and sets the initial value of the <see cref="P:System.Web.UI.Design.SupportsPreviewControlAttribute.SupportsPreviewControl" /> property.</summary>
		/// <param name="supportsPreviewControl">The initial value to assign for <see cref="P:System.Web.UI.Design.SupportsPreviewControlAttribute.SupportsPreviewControl" />.</param>
		// Token: 0x060004BF RID: 1215 RVA: 0x000092E7 File Offset: 0x000074E7
		public SupportsPreviewControlAttribute(bool supportsPreviewControl)
			: this(supportsPreviewControl, false)
		{
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000092F1 File Offset: 0x000074F1
		private SupportsPreviewControlAttribute(bool supportsPreviewControl, bool isDefault)
		{
			this.supports_preview = supportsPreviewControl;
			this.is_default = isDefault;
		}

		/// <summary>Gets a value indicating whether the control designer requires a temporary preview control at design time.</summary>
		/// <returns>true if the designer uses a temporary copy of the associated control for design-time preview; false if the designer uses an instance of the <see cref="P:System.ComponentModel.Design.ComponentDesigner.Component" /> control that is contained in the designer.</returns>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00009307 File Offset: 0x00007507
		public bool SupportsPreviewControl
		{
			get
			{
				return this.supports_preview;
			}
		}

		/// <summary>Determines whether the specified object represents the same preview attribute setting as the current instance of the <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" /> class.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" /> attribute and its value is the same as this instance of <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current instance of <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" />.</param>
		// Token: 0x060004C2 RID: 1218 RVA: 0x00009310 File Offset: 0x00007510
		public override bool Equals(object obj)
		{
			SupportsPreviewControlAttribute supportsPreviewControlAttribute = obj as SupportsPreviewControlAttribute;
			return supportsPreviewControlAttribute != null && supportsPreviewControlAttribute.supports_preview == this.supports_preview;
		}

		/// <summary>Returns the hash code for this instance of the <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" /> class.</summary>
		/// <returns>A 32-bit signed integer hash code for the current instance of <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" />.</returns>
		// Token: 0x060004C3 RID: 1219 RVA: 0x00009337 File Offset: 0x00007537
		public override int GetHashCode()
		{
			if (!this.supports_preview)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Indicates whether the current instance of the <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" /> class is set to the default preview attribute value.</summary>
		/// <returns>true if the current instance of <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" /> is equal to the default preview attribute value; otherwise, false.</returns>
		// Token: 0x060004C4 RID: 1220 RVA: 0x00009344 File Offset: 0x00007544
		public override bool IsDefaultAttribute()
		{
			return this.is_default;
		}

		// Token: 0x04000131 RID: 305
		private bool is_default;

		// Token: 0x04000132 RID: 306
		private bool supports_preview;

		/// <summary>Gets an instance of the <see cref="T:System.Web.UI.Design.SupportsPreviewControlAttribute" /> class that is set to the default preview value. This field is read-only.</summary>
		// Token: 0x04000133 RID: 307
		public static readonly SupportsPreviewControlAttribute Default = new SupportsPreviewControlAttribute(false, true);
	}
}
