using System;
using System.Collections;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x02000005 RID: 5
	public class Ex03_LoadAssemblyAsync : MonoBehaviour
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002130 File Offset: 0x00000330
		public IEnumerator Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			ScriptSecurityMode scriptSecurityMode = ScriptSecurityMode.UseSettings;
			AsyncLoadOperation assemblyLoad = this.domain.LoadAssemblyAsync("path/to/assembly.dll", scriptSecurityMode);
			yield return assemblyLoad;
			ScriptAssembly loadedAssembly = assemblyLoad.LoadedAssembly;
			yield break;
		}

		// Token: 0x04000003 RID: 3
		private ScriptDomain domain;
	}
}
