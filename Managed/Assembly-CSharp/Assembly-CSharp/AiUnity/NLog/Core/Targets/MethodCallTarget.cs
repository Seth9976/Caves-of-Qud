using System;
using System.Reflection;
using AiUnity.NLog.Core.Common;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x02000220 RID: 544
	[Target("MethodCall")]
	[Preserve]
	public sealed class MethodCallTarget : MethodCallTargetBase
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x00072B2C File Offset: 0x00070D2C
		// (set) Token: 0x06001428 RID: 5160 RVA: 0x00072B34 File Offset: 0x00070D34
		public string ClassName { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x00072B3D File Offset: 0x00070D3D
		// (set) Token: 0x0600142A RID: 5162 RVA: 0x00072B45 File Offset: 0x00070D45
		public string MethodName { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x00072B4E File Offset: 0x00070D4E
		// (set) Token: 0x0600142C RID: 5164 RVA: 0x00072B56 File Offset: 0x00070D56
		private MethodInfo Method { get; set; }

		// Token: 0x0600142D RID: 5165 RVA: 0x00072B60 File Offset: 0x00070D60
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			if (this.ClassName != null && this.MethodName != null)
			{
				Type type = Type.GetType(this.ClassName);
				this.Method = type.GetMethod(this.MethodName);
				return;
			}
			this.Method = null;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x00072BA9 File Offset: 0x00070DA9
		protected override void DoInvoke(object[] parameters)
		{
			if (this.Method != null)
			{
				this.Method.Invoke(null, parameters);
			}
		}
	}
}
