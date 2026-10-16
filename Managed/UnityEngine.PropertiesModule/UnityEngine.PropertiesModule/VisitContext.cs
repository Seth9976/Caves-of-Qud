using System;
using Unity.Properties.Internal;

namespace Unity.Properties
{
	// Token: 0x0200007E RID: 126
	public readonly struct VisitContext<TContainer, TValue>
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00007048 File Offset: 0x00005248
		internal static VisitContext<TContainer, TValue> FromProperty(PropertyVisitor visitor, ReadOnlyAdapterCollection.Enumerator enumerator, Property<TContainer, TValue> property)
		{
			return new VisitContext<TContainer, TValue>(visitor, enumerator, property);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00007062 File Offset: 0x00005262
		public Property<TContainer, TValue> Property { get; }

		// Token: 0x06000203 RID: 515 RVA: 0x0000706A File Offset: 0x0000526A
		private VisitContext(PropertyVisitor visitor, ReadOnlyAdapterCollection.Enumerator enumerator, Property<TContainer, TValue> property)
		{
			this.m_Visitor = visitor;
			this.m_Enumerator = enumerator;
			this.Property = property;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007082 File Offset: 0x00005282
		public void ContinueVisitation(ref TContainer container, ref TValue value)
		{
			this.m_Visitor.ContinueVisitation<TContainer, TValue>(this.Property, this.m_Enumerator, ref container, ref value);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000709F File Offset: 0x0000529F
		public void ContinueVisitationWithoutAdapters(ref TContainer container, ref TValue value)
		{
			this.m_Visitor.ContinueVisitationWithoutAdapters<TContainer, TValue>(this.Property, this.m_Enumerator, ref container, ref value);
		}

		// Token: 0x0400009B RID: 155
		private readonly ReadOnlyAdapterCollection.Enumerator m_Enumerator;

		// Token: 0x0400009C RID: 156
		private readonly PropertyVisitor m_Visitor;
	}
}
