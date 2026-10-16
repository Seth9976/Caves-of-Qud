using System;
using System.Collections;
using RoslynCSharp.Compiler;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x0200000A RID: 10
	public class Ex08_CompileAsync : MonoBehaviour
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002364 File Offset: 0x00000564
		public IEnumerator Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			AsyncCompileOperation compileRequest = this.domain.CompileAndLoadSourceAsync("\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            static void ExampleMethod()\r\n            {\r\n                Debug.Log(\"Hello World\");\r\n            }\r\n        }", ScriptSecurityMode.UseSettings, null);
			yield return compileRequest;
			if (!compileRequest.IsSuccessful)
			{
				foreach (CompilationError compilationError in compileRequest.CompileDomain.CompileResult.Errors)
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
			yield break;
		}

		// Token: 0x0400000A RID: 10
		private ScriptDomain domain;

		// Token: 0x0400000B RID: 11
		private const string sourceCode = "\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            static void ExampleMethod()\r\n            {\r\n                Debug.Log(\"Hello World\");\r\n            }\r\n        }";
	}
}
