using System;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x02000003 RID: 3
	public class Ex01_CreateScriptDomain : MonoBehaviour
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public void Start()
		{
			bool flag = true;
			bool flag2 = true;
			AppDomain currentDomain = AppDomain.CurrentDomain;
			this.domain = ScriptDomain.CreateDomain("Example Domain", flag, flag2, currentDomain);
		}

		// Token: 0x04000001 RID: 1
		private ScriptDomain domain;
	}
}
