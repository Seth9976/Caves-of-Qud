using System;
using RoslynCSharp.Compiler;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x02000009 RID: 9
	public class Ex07_CompileFromFile : MonoBehaviour
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000022CC File Offset: 0x000004CC
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			this.domain.CompileAndLoadFile("path/to/source/file.cs", ScriptSecurityMode.UseSettings, null);
			if (!this.domain.CompileResult.Success)
			{
				foreach (CompilationError compilationError in this.domain.CompileResult.Errors)
				{
					if (compilationError.IsError)
					{
						Debug.LogError(compilationError.ToString());
					}
					else if (compilationError.IsWarning)
					{
						Debug.LogWarning(compilationError.ToString());
					}
				}
			}
		}

		// Token: 0x04000008 RID: 8
		private ScriptDomain domain;

		// Token: 0x04000009 RID: 9
		private const string sourceFile = "path/to/source/file.cs";
	}
}
