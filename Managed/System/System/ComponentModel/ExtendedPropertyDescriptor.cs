using System;
using System.Collections.Generic;

namespace System.ComponentModel
{
	// Token: 0x020003AD RID: 941
	internal sealed class ExtendedPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06001EC3 RID: 7875 RVA: 0x0006C924 File Offset: 0x0006AB24
		public ExtendedPropertyDescriptor(ReflectPropertyDescriptor extenderInfo, Type receiverType, IExtenderProvider provider, Attribute[] attributes)
			: base(extenderInfo, attributes)
		{
			List<Attribute> list = new List<Attribute>(this.AttributeArray);
			list.Add(ExtenderProvidedPropertyAttribute.Create(extenderInfo, receiverType, provider));
			if (extenderInfo.IsReadOnly)
			{
				list.Add(ReadOnlyAttribute.Yes);
			}
			Attribute[] array = new Attribute[list.Count];
			list.CopyTo(array, 0);
			this.AttributeArray = array;
			this._extenderInfo = extenderInfo;
			this._provider = provider;
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x0006C990 File Offset: 0x0006AB90
		public ExtendedPropertyDescriptor(PropertyDescriptor extender, Attribute[] attributes)
			: base(extender, attributes)
		{
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = extender.Attributes[typeof(ExtenderProvidedPropertyAttribute)] as ExtenderProvidedPropertyAttribute;
			ReflectPropertyDescriptor reflectPropertyDescriptor = extenderProvidedPropertyAttribute.ExtenderProperty as ReflectPropertyDescriptor;
			this._extenderInfo = reflectPropertyDescriptor;
			this._provider = extenderProvidedPropertyAttribute.Provider;
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x0006C9DF File Offset: 0x0006ABDF
		public override bool CanResetValue(object comp)
		{
			return this._extenderInfo.ExtenderCanResetValue(this._provider, comp);
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x0006C9F3 File Offset: 0x0006ABF3
		public override Type ComponentType
		{
			get
			{
				return this._extenderInfo.ComponentType;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x0006CA00 File Offset: 0x0006AC00
		public override bool IsReadOnly
		{
			get
			{
				return this.Attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes);
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x0006CA21 File Offset: 0x0006AC21
		public override Type PropertyType
		{
			get
			{
				return this._extenderInfo.ExtenderGetType(this._provider);
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x0006CA34 File Offset: 0x0006AC34
		public override string DisplayName
		{
			get
			{
				string text = base.DisplayName;
				DisplayNameAttribute displayNameAttribute = this.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
				if (displayNameAttribute == null || displayNameAttribute.IsDefaultAttribute())
				{
					ISite site = MemberDescriptor.GetSite(this._provider);
					string text2 = ((site != null) ? site.Name : null);
					if (text2 != null && text2.Length > 0)
					{
						text = string.Format("{0} on {1}", text, text2);
					}
				}
				return text;
			}
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x0006CAA0 File Offset: 0x0006ACA0
		public override object GetValue(object comp)
		{
			return this._extenderInfo.ExtenderGetValue(this._provider, comp);
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x0006CAB4 File Offset: 0x0006ACB4
		public override void ResetValue(object comp)
		{
			this._extenderInfo.ExtenderResetValue(this._provider, comp, this);
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x0006CAC9 File Offset: 0x0006ACC9
		public override void SetValue(object component, object value)
		{
			this._extenderInfo.ExtenderSetValue(this._provider, component, value, this);
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x0006CADF File Offset: 0x0006ACDF
		public override bool ShouldSerializeValue(object comp)
		{
			return this._extenderInfo.ExtenderShouldSerializeValue(this._provider, comp);
		}

		// Token: 0x04000F66 RID: 3942
		private readonly ReflectPropertyDescriptor _extenderInfo;

		// Token: 0x04000F67 RID: 3943
		private readonly IExtenderProvider _provider;
	}
}
