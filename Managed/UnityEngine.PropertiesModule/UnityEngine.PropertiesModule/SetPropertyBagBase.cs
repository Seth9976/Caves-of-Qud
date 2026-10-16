using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000055 RID: 85
	public class SetPropertyBagBase<TSet, TElement> : PropertyBag<TSet>, ISetPropertyBag<TSet, TElement>, ICollectionPropertyBag<TSet, TElement>, IPropertyBag<TSet>, IPropertyBag, ICollectionPropertyBagAccept<TSet>, ISetPropertyBagAccept<TSet>, ISetPropertyAccept<TSet>, IKeyedProperties<TSet, object> where TSet : ISet<TElement>
	{
		// Token: 0x0600019E RID: 414 RVA: 0x000062D4 File Offset: 0x000044D4
		public override PropertyCollection<TSet> GetProperties()
		{
			return PropertyCollection<TSet>.Empty;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000062EC File Offset: 0x000044EC
		public override PropertyCollection<TSet> GetProperties(ref TSet container)
		{
			return new PropertyCollection<TSet>(this.GetPropertiesEnumerable(container));
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000630F File Offset: 0x0000450F
		private IEnumerable<IProperty<TSet>> GetPropertiesEnumerable(TSet container)
		{
			foreach (TElement element in container)
			{
				this.m_Property.m_Value = element;
				yield return this.m_Property;
				element = default(TElement);
			}
			IEnumerator<TElement> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000054CE File Offset: 0x000036CE
		void ICollectionPropertyBagAccept<TSet>.Accept(ICollectionPropertyBagVisitor visitor, ref TSet container)
		{
			visitor.Visit<TSet, TElement>(this, ref container);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006326 File Offset: 0x00004526
		void ISetPropertyBagAccept<TSet>.Accept(ISetPropertyBagVisitor visitor, ref TSet container)
		{
			visitor.Visit<TSet, TElement>(this, ref container);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006334 File Offset: 0x00004534
		void ISetPropertyAccept<TSet>.Accept<TContainer>(ISetPropertyVisitor visitor, Property<TContainer, TSet> property, ref TContainer container, ref TSet dictionary)
		{
			using (new AttributesScope(this.m_Property, property))
			{
				visitor.Visit<TContainer, TSet, TElement>(property, ref container, ref dictionary);
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006380 File Offset: 0x00004580
		public bool TryGetProperty(ref TSet container, object key, out IProperty<TSet> property)
		{
			bool flag = container.Contains((TElement)((object)key));
			bool flag2;
			if (flag)
			{
				property = new SetPropertyBagBase<TSet, TElement>.SetElementProperty
				{
					m_Value = (TElement)((object)key)
				};
				flag2 = true;
			}
			else
			{
				property = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x04000085 RID: 133
		private readonly SetPropertyBagBase<TSet, TElement>.SetElementProperty m_Property = new SetPropertyBagBase<TSet, TElement>.SetElementProperty();

		// Token: 0x02000056 RID: 86
		private class SetElementProperty : Property<TSet, TElement>, ISetElementProperty<TElement>, ISetElementProperty, ICollectionElementProperty
		{
			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060001A6 RID: 422 RVA: 0x000063D8 File Offset: 0x000045D8
			public override string Name
			{
				get
				{
					return this.m_Value.ToString();
				}
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060001A7 RID: 423 RVA: 0x00005275 File Offset: 0x00003475
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060001A8 RID: 424 RVA: 0x000063EB File Offset: 0x000045EB
			public override TElement GetValue(ref TSet container)
			{
				return this.m_Value;
			}

			// Token: 0x060001A9 RID: 425 RVA: 0x000063F3 File Offset: 0x000045F3
			public override void SetValue(ref TSet container, TElement value)
			{
				throw new InvalidOperationException("Property is ReadOnly.");
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060001AA RID: 426 RVA: 0x000063EB File Offset: 0x000045EB
			public TElement Key
			{
				get
				{
					return this.m_Value;
				}
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060001AB RID: 427 RVA: 0x000063FF File Offset: 0x000045FF
			public object ObjectKey
			{
				get
				{
					return this.m_Value;
				}
			}

			// Token: 0x04000086 RID: 134
			internal TElement m_Value;
		}
	}
}
