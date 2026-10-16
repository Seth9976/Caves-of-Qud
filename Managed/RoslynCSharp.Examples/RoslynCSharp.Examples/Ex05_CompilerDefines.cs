using System;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x02000007 RID: 7
	public class Ex05_CompilerDefines : MonoBehaviour
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021D8 File Offset: 0x000003D8
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			this.domain.RoslynCompilerService.DefineSymbols.Add("DEBUG");
			this.domain.RoslynCompilerService.DefineSymbols.Add("UNITY_EDITOR");
		}

		// Token: 0x04000005 RID: 5
		private ScriptDomain domain;
	}
}
