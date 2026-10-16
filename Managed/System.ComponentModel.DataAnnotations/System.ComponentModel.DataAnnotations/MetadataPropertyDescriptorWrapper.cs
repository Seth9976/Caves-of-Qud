using System;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
	// Token: 0x02000022 RID: 34
	internal class MetadataPropertyDescriptorWrapper : PropertyDescriptor
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x000038D4 File Offset: 0x00001AD4
		public MetadataPropertyDescriptorWrapper(PropertyDescriptor descriptor, Attribute[] newAttributes)
			: base(descriptor, newAttributes)
		{
			this._descriptor = descriptor;
			ReadOnlyAttribute readOnlyAttribute = newAttributes.OfType<ReadOnlyAttribute>().FirstOrDefault<ReadOnlyAttribute>();
			this._isReadOnly = readOnlyAttribute != null && readOnlyAttribute.IsReadOnly;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000390E File Offset: 0x00001B0E
		public override void AddValueChanged(object component, EventHandler handler)
		{
			this._descriptor.AddValueChanged(component, handler);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000391D File Offset: 0x00001B1D
		public override bool CanResetValue(object component)
		{
			return this._descriptor.CanResetValue(component);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000392B File Offset: 0x00001B2B
		public override Type ComponentType
		{
			get
			{
				return this._descriptor.ComponentType;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003938 File Offset: 0x00001B38
		public override object GetValue(object component)
		{
			return this._descriptor.GetValue(component);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003946 File Offset: 0x00001B46
		public override bool IsReadOnly
		{
			get
			{
				return this._isReadOnly || this._descriptor.IsReadOnly;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000395D File Offset: 0x00001B5D
		public override Type PropertyType
		{
			get
			{
				return this._descriptor.PropertyType;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000396A File Offset: 0x00001B6A
		public override void RemoveValueChanged(object component, EventHandler handler)
		{
			this._descriptor.RemoveValueChanged(component, handler);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003979 File Offset: 0x00001B79
		public override void ResetValue(object component)
		{
			this._descriptor.ResetValue(component);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003987 File Offset: 0x00001B87
		public override void SetValue(object component, object value)
		{
			this._descriptor.SetValue(component, value);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003996 File Offset: 0x00001B96
		public override bool ShouldSerializeValue(object component)
		{
			return this._descriptor.ShouldSerializeValue(component);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000039A4 File Offset: 0x00001BA4
		public override bool SupportsChangeEvents
		{
			get
			{
				return this._descriptor.SupportsChangeEvents;
			}
		}

		// Token: 0x04000082 RID: 130
		private PropertyDescriptor _descriptor;

		// Token: 0x04000083 RID: 131
		private bool _isReadOnly;
	}
}
