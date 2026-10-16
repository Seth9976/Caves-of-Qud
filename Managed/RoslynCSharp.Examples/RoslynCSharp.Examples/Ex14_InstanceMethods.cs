using System;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x02000010 RID: 16
	public class Ex14_InstanceMethods : MonoBehaviour
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002730 File Offset: 0x00000930
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			ScriptType scriptType = this.domain.CompileAndLoadMainSource("\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            void ExampleMethod(string input)\r\n            {\r\n                Debug.Log(\"Hello \" + input);\r\n            }\r\n        }", ScriptSecurityMode.UseSettings, null);
			ScriptProxy scriptProxy = scriptType.CreateInstance(null);
			scriptProxy = scriptType.CreateInstance(null);
			scriptProxy.Call("ExampleMethod", new object[] { "World" });
			scriptProxy.SafeCall("ExampleMethod", new object[] { "Safe World" });
		}

		// Token: 0x04000016 RID: 22
		private ScriptDomain domain;

		// Token: 0x04000017 RID: 23
		private const string sourceCode = "\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            void ExampleMethod(string input)\r\n            {\r\n                Debug.Log(\"Hello \" + input);\r\n            }\r\n        }";
	}
}
