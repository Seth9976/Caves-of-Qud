using System;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x02000012 RID: 18
	public class Ex16_InstanceProperties : MonoBehaviour
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002860 File Offset: 0x00000A60
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			ScriptType scriptType = this.domain.CompileAndLoadMainSource("\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            string exampleField = \"Hello World\";\r\n\r\n            string ExampleProperty\r\n            {\r\n                get { return exampleField; }\r\n            }\r\n        }", ScriptSecurityMode.UseSettings, null);
			scriptType.CreateInstance(null);
			ScriptProxy scriptProxy = scriptType.CreateInstance(null);
			string text = (string)scriptProxy.Properties["ExampleProperty"];
			Debug.Log(text == "Hello World");
			scriptProxy.Properties["ExampleProperty"] = "Goodbye World";
			text = (string)scriptProxy.SafeProperties["ExampleProperty"];
			scriptProxy.SafeProperties["ExampleProperty"] = text;
		}

		// Token: 0x0400001A RID: 26
		private ScriptDomain domain;

		// Token: 0x0400001B RID: 27
		private const string sourceCode = "\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            string exampleField = \"Hello World\";\r\n\r\n            string ExampleProperty\r\n            {\r\n                get { return exampleField; }\r\n            }\r\n        }";
	}
}
