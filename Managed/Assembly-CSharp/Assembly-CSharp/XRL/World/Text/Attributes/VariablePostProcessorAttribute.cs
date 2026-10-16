using System;

namespace XRL.World.Text.Attributes
{
	// Token: 0x0200146D RID: 5229
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class VariablePostProcessorAttribute : VariableReplacerAttribute
	{
		// Token: 0x0600CD9E RID: 52638 RVA: 0x004A7EDB File Offset: 0x004A60DB
		public VariablePostProcessorAttribute()
		{
		}

		// Token: 0x0600CD9F RID: 52639 RVA: 0x004A7EE3 File Offset: 0x004A60E3
		public VariablePostProcessorAttribute(params string[] Keys)
			: this()
		{
			this.Keys = Keys;
		}
	}
}
