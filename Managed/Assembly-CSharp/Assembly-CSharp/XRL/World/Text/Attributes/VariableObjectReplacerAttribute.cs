using System;

namespace XRL.World.Text.Attributes
{
	// Token: 0x0200146C RID: 5228
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class VariableObjectReplacerAttribute : VariableReplacerAttribute
	{
		// Token: 0x0600CD9C RID: 52636 RVA: 0x004A7EBD File Offset: 0x004A60BD
		public VariableObjectReplacerAttribute()
		{
			this.Capitalization = true;
		}

		// Token: 0x0600CD9D RID: 52637 RVA: 0x004A7ECC File Offset: 0x004A60CC
		public VariableObjectReplacerAttribute(params string[] Keys)
			: this()
		{
			this.Keys = Keys;
		}
	}
}
