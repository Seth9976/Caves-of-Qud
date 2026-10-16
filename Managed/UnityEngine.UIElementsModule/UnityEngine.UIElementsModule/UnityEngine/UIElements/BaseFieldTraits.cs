using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200011B RID: 283
	[Obsolete("BaseFieldTraits<TValueType, TValueUxmlAttributeType> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class BaseFieldTraits<TValueType, TValueUxmlAttributeType> : BaseField<TValueType>.UxmlTraits where TValueUxmlAttributeType : TypedUxmlAttributeDescription<TValueType>, new()
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x00027002 File Offset: 0x00025202
		public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
		{
			base.Init(ve, bag, cc);
			((INotifyValueChanged<TValueType>)ve).SetValueWithoutNotify(this.m_Value.GetValueFromBag(bag, cc));
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002702D File Offset: 0x0002522D
		public BaseFieldTraits()
		{
			TValueUxmlAttributeType tvalueUxmlAttributeType = new TValueUxmlAttributeType();
			tvalueUxmlAttributeType.name = "value";
			this.m_Value = tvalueUxmlAttributeType;
			base..ctor();
		}

		// Token: 0x04000510 RID: 1296
		private TValueUxmlAttributeType m_Value;
	}
}
