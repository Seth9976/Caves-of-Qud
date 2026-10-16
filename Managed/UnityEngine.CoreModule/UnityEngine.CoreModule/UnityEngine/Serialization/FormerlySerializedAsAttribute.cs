using System;
using UnityEngine.Scripting;

namespace UnityEngine.Serialization
{
	// Token: 0x02000357 RID: 855
	[RequiredByNativeCode]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	public class FormerlySerializedAsAttribute : Attribute
	{
		// Token: 0x0600287B RID: 10363 RVA: 0x0004E338 File Offset: 0x0004C538
		public FormerlySerializedAsAttribute(string oldName)
		{
			this.m_oldName = oldName;
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600287C RID: 10364 RVA: 0x0004E34C File Offset: 0x0004C54C
		public string oldName
		{
			get
			{
				return this.m_oldName;
			}
		}

		// Token: 0x04000B54 RID: 2900
		private string m_oldName;
	}
}
