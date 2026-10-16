using System;

namespace Unity.Properties
{
	// Token: 0x0200001D RID: 29
	public class DelegateProperty<TContainer, TValue> : Property<TContainer, TValue>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003035 File Offset: 0x00001235
		public override string Name { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000303D File Offset: 0x0000123D
		public override bool IsReadOnly
		{
			get
			{
				return this.m_Setter == null;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003048 File Offset: 0x00001248
		public DelegateProperty(string name, PropertyGetter<TContainer, TValue> getter, PropertySetter<TContainer, TValue> setter = null)
		{
			this.Name = name;
			if (getter == null)
			{
				throw new ArgumentException("getter");
			}
			this.m_Getter = getter;
			this.m_Setter = setter;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003078 File Offset: 0x00001278
		public override TValue GetValue(ref TContainer container)
		{
			return this.m_Getter(ref container);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003098 File Offset: 0x00001298
		public override void SetValue(ref TContainer container, TValue value)
		{
			bool isReadOnly = this.IsReadOnly;
			if (isReadOnly)
			{
				throw new InvalidOperationException("Property is ReadOnly.");
			}
			this.m_Setter(ref container, value);
		}

		// Token: 0x0400002B RID: 43
		private readonly PropertyGetter<TContainer, TValue> m_Getter;

		// Token: 0x0400002C RID: 44
		private readonly PropertySetter<TContainer, TValue> m_Setter;
	}
}
