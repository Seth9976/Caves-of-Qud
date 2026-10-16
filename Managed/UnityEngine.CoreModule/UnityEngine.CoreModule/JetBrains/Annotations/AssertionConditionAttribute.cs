using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000E7 RID: 231
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class AssertionConditionAttribute : Attribute
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x00008747 File Offset: 0x00006947
		public AssertionConditionAttribute(AssertionConditionType conditionType)
		{
			this.ConditionType = conditionType;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00008758 File Offset: 0x00006958
		public AssertionConditionType ConditionType { get; }
	}
}
