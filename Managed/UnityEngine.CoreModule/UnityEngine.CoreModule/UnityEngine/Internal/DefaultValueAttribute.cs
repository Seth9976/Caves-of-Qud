using System;

namespace UnityEngine.Internal
{
	// Token: 0x0200043C RID: 1084
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.GenericParameter)]
	[Serializable]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x060029F0 RID: 10736 RVA: 0x00050EE8 File Offset: 0x0004F0E8
		public DefaultValueAttribute(string value)
		{
			this.DefaultValue = value;
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060029F1 RID: 10737 RVA: 0x00050EFC File Offset: 0x0004F0FC
		public object Value
		{
			get
			{
				return this.DefaultValue;
			}
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x00050F14 File Offset: 0x0004F114
		public override bool Equals(object obj)
		{
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			bool flag = defaultValueAttribute == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.DefaultValue == null;
				if (flag3)
				{
					flag2 = defaultValueAttribute.Value == null;
				}
				else
				{
					flag2 = this.DefaultValue.Equals(defaultValueAttribute.Value);
				}
			}
			return flag2;
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x00050F64 File Offset: 0x0004F164
		public override int GetHashCode()
		{
			bool flag = this.DefaultValue == null;
			int num;
			if (flag)
			{
				num = base.GetHashCode();
			}
			else
			{
				num = this.DefaultValue.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000BF5 RID: 3061
		private object DefaultValue;
	}
}
