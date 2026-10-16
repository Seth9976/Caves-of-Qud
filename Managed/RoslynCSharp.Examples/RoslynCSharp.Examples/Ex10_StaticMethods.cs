using System;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x0200000C RID: 12
	public class Ex10_StaticMethods : MonoBehaviour
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000024B0 File Offset: 0x000006B0
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			ScriptType scriptType = this.domain.CompileAndLoadMainSource("\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            static void ExampleMethod(string input)\r\n            {\r\n                Debug.Log(\"Hello \" + input);\r\n            }\r\n        }", ScriptSecurityMode.UseSettings, null);
			scriptType.CallStatic("ExampleMethod", new object[] { "World" });
			scriptType.SafeCallStatic("ExampleMethod", new object[] { "Safe World" });
		}

		// Token: 0x0400000E RID: 14
		private ScriptDomain domain;

		// Token: 0x0400000F RID: 15
		private const string sourceCode = "\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            static void ExampleMethod(string input)\r\n            {\r\n                Debug.Log(\"Hello \" + input);\r\n            }\r\n        }";
	}
}
