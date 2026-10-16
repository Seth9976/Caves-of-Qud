using System;

namespace AiUnity.Common.Attributes
{
	// Token: 0x020002E4 RID: 740
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class ClassImplementsAttribute : ClassTypeConstraintAttribute
	{
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x000814E7 File Offset: 0x0007F6E7
		// (set) Token: 0x06001968 RID: 6504 RVA: 0x000814EF File Offset: 0x0007F6EF
		public Type InterfaceType { get; private set; }

		// Token: 0x06001969 RID: 6505 RVA: 0x000814F8 File Offset: 0x0007F6F8
		public ClassImplementsAttribute(string labelName = null)
			: base(labelName, "")
		{
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00081506 File Offset: 0x0007F706
		public ClassImplementsAttribute(Type interfaceType, string labelName = null)
			: base(labelName, "")
		{
			this.InterfaceType = interfaceType;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0008151C File Offset: 0x0007F71C
		public override bool IsConstraintSatisfied(Type type)
		{
			if (base.IsConstraintSatisfied(type))
			{
				Type[] interfaces = type.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					if (interfaces[i] == this.InterfaceType)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
