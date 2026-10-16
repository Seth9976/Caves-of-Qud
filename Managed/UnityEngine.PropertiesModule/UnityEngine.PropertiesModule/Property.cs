using System;
using System.Collections.Generic;
using Unity.Properties.Internal;

namespace Unity.Properties
{
	// Token: 0x02000026 RID: 38
	public abstract class Property<TContainer, TValue> : IProperty<TContainer>, IProperty, IPropertyAccept<TContainer>, IAttributes
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000030CA File Offset: 0x000012CA
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000030D2 File Offset: 0x000012D2
		List<Attribute> IAttributes.Attributes
		{
			get
			{
				return this.m_Attributes;
			}
			set
			{
				this.m_Attributes = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000081 RID: 129
		public abstract string Name { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000082 RID: 130
		public abstract bool IsReadOnly { get; }

		// Token: 0x06000083 RID: 131 RVA: 0x000030DB File Offset: 0x000012DB
		public Type DeclaredValueType()
		{
			return typeof(TValue);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000030E7 File Offset: 0x000012E7
		public void Accept(IPropertyVisitor visitor, ref TContainer container)
		{
			visitor.Visit<TContainer, TValue>(this, ref container);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000030F2 File Offset: 0x000012F2
		object IProperty<TContainer>.GetValue(ref TContainer container)
		{
			return this.GetValue(ref container);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003100 File Offset: 0x00001300
		void IProperty<TContainer>.SetValue(ref TContainer container, object value)
		{
			this.SetValue(ref container, TypeConversion.Convert<object, TValue>(ref value));
		}

		// Token: 0x06000087 RID: 135
		public abstract TValue GetValue(ref TContainer container);

		// Token: 0x06000088 RID: 136
		public abstract void SetValue(ref TContainer container, TValue value);

		// Token: 0x06000089 RID: 137 RVA: 0x00003111 File Offset: 0x00001311
		protected void AddAttribute(Attribute attribute)
		{
			((IAttributes)this).AddAttribute(attribute);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000311B File Offset: 0x0000131B
		protected void AddAttributes(IEnumerable<Attribute> attributes)
		{
			((IAttributes)this).AddAttributes(attributes);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003128 File Offset: 0x00001328
		void IAttributes.AddAttribute(Attribute attribute)
		{
			bool flag = attribute == null || attribute.GetType() == typeof(CreatePropertyAttribute);
			if (!flag)
			{
				bool flag2 = this.m_Attributes == null;
				if (flag2)
				{
					this.m_Attributes = new List<Attribute>();
				}
				this.m_Attributes.Add(attribute);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000317C File Offset: 0x0000137C
		void IAttributes.AddAttributes(IEnumerable<Attribute> attributes)
		{
			bool flag = this.m_Attributes == null;
			if (flag)
			{
				this.m_Attributes = new List<Attribute>();
			}
			foreach (Attribute attribute in attributes)
			{
				bool flag2 = attribute == null;
				if (!flag2)
				{
					this.m_Attributes.Add(attribute);
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000031F4 File Offset: 0x000013F4
		public bool HasAttribute<TAttribute>() where TAttribute : Attribute
		{
			int num = 0;
			for (;;)
			{
				int num2 = num;
				List<Attribute> attributes = this.m_Attributes;
				int? num3 = ((attributes != null) ? new int?(attributes.Count) : null);
				if (!((num2 < num3.GetValueOrDefault()) & (num3 != null)))
				{
					goto Block_3;
				}
				bool flag = this.m_Attributes[num] is TAttribute;
				if (flag)
				{
					break;
				}
				num++;
			}
			return true;
			Block_3:
			return false;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003268 File Offset: 0x00001468
		public TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute
		{
			int num = 0;
			TAttribute tattribute;
			for (;;)
			{
				int num2 = num;
				List<Attribute> attributes = this.m_Attributes;
				int? num3 = ((attributes != null) ? new int?(attributes.Count) : null);
				if (!((num2 < num3.GetValueOrDefault()) & (num3 != null)))
				{
					goto Block_3;
				}
				tattribute = this.m_Attributes[num] as TAttribute;
				bool flag = tattribute != null;
				if (flag)
				{
					break;
				}
				num++;
			}
			return tattribute;
			Block_3:
			return default(TAttribute);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000032F1 File Offset: 0x000014F1
		public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute
		{
			int i = 0;
			for (;;)
			{
				int num = i;
				List<Attribute> attributes = this.m_Attributes;
				int? num2 = ((attributes != null) ? new int?(attributes.Count) : null);
				if (!((num < num2.GetValueOrDefault()) & (num2 != null)))
				{
					break;
				}
				Attribute attribute = this.m_Attributes[i];
				TAttribute typed = attribute as TAttribute;
				bool flag = typed != null;
				if (flag)
				{
					yield return typed;
				}
				typed = default(TAttribute);
				int num3 = i;
				i = num3 + 1;
			}
			yield break;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003301 File Offset: 0x00001501
		public IEnumerable<Attribute> GetAttributes()
		{
			int i = 0;
			for (;;)
			{
				int num = i;
				List<Attribute> attributes = this.m_Attributes;
				int? num2 = ((attributes != null) ? new int?(attributes.Count) : null);
				if (!((num < num2.GetValueOrDefault()) & (num2 != null)))
				{
					break;
				}
				yield return this.m_Attributes[i];
				int num3 = i;
				i = num3 + 1;
			}
			yield break;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003311 File Offset: 0x00001511
		AttributesScope IAttributes.CreateAttributesScope(IAttributes attributes)
		{
			return new AttributesScope(this, (attributes != null) ? attributes.Attributes : null);
		}

		// Token: 0x0400002E RID: 46
		private List<Attribute> m_Attributes;
	}
}
