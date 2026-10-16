using System;

namespace AiUnity.Common.Attributes
{
	// Token: 0x020002E3 RID: 739
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class ClassExtendsAttribute : ClassTypeConstraintAttribute
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x0008149E File Offset: 0x0007F69E
		// (set) Token: 0x06001964 RID: 6500 RVA: 0x000814A6 File Offset: 0x0007F6A6
		public Type BaseType { get; private set; }

		// Token: 0x06001965 RID: 6501 RVA: 0x000814AF File Offset: 0x0007F6AF
		public override bool IsConstraintSatisfied(Type type)
		{
			return base.IsConstraintSatisfied(type) && this.BaseType.IsAssignableFrom(type) && type != this.BaseType;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x000814D6 File Offset: 0x0007F6D6
		public ClassExtendsAttribute(Type baseType, string labelName = null, string tooltip = null)
			: base(labelName, tooltip)
		{
			this.BaseType = baseType;
		}
	}
}
