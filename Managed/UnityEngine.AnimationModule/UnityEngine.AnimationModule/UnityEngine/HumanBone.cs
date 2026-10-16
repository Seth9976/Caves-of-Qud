using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000037 RID: 55
	[NativeType(CodegenOptions.Custom, "MonoHumanBone")]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	public struct HumanBone
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00006F50 File Offset: 0x00005150
		// (set) Token: 0x06000320 RID: 800 RVA: 0x00006F68 File Offset: 0x00005168
		public string boneName
		{
			get
			{
				return this.m_BoneName;
			}
			set
			{
				this.m_BoneName = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00006F74 File Offset: 0x00005174
		// (set) Token: 0x06000322 RID: 802 RVA: 0x00006F8C File Offset: 0x0000518C
		public string humanName
		{
			get
			{
				return this.m_HumanName;
			}
			set
			{
				this.m_HumanName = value;
			}
		}

		// Token: 0x04000124 RID: 292
		private string m_BoneName;

		// Token: 0x04000125 RID: 293
		private string m_HumanName;

		// Token: 0x04000126 RID: 294
		[NativeName("m_Limit")]
		public HumanLimit limit;
	}
}
