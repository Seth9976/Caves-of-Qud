using System;

namespace UnityEngineInternal
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Method)]
	[Serializable]
	public class TypeInferenceRuleAttribute : Attribute
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002129 File Offset: 0x00000329
		public TypeInferenceRuleAttribute(TypeInferenceRules rule)
			: this(rule.ToString())
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002140 File Offset: 0x00000340
		public TypeInferenceRuleAttribute(string rule)
		{
			this._rule = rule;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002154 File Offset: 0x00000354
		public override string ToString()
		{
			return this._rule;
		}

		// Token: 0x04000027 RID: 39
		private readonly string _rule;
	}
}
