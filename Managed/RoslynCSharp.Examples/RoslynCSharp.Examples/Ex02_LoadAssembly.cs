using System;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x02000004 RID: 4
	public class Ex02_LoadAssembly : MonoBehaviour
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020F4 File Offset: 0x000002F4
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			ScriptSecurityMode scriptSecurityMode = ScriptSecurityMode.UseSettings;
			this.domain.LoadAssembly("path/to/assembly.dll", scriptSecurityMode);
		}

		// Token: 0x04000002 RID: 2
		private ScriptDomain domain;
	}
}
