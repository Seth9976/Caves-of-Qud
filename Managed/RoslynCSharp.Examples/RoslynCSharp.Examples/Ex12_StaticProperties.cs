using System;
using UnityEngine;

namespace RoslynCSharp.Example
{
	// Token: 0x0200000E RID: 14
	public class Ex12_StaticProperties : MonoBehaviour
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000025C0 File Offset: 0x000007C0
		public void Start()
		{
			this.domain = ScriptDomain.CreateDomain("Example Domain", true, true, null);
			ScriptType scriptType = this.domain.CompileAndLoadMainSource("\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            static string exampleField = \"Hello World\";\r\n\r\n            static string ExampleProperty\r\n            {\r\n                get { return exampleField; }\r\n            }\r\n        }", ScriptSecurityMode.UseSettings, null);
			string text = (string)scriptType.PropertiesStatic["ExampleProperty"];
			Debug.Log(text == "Hello World");
			scriptType.PropertiesStatic["ExampleProperty"] = "Goodbye World";
			text = (string)scriptType.SafePropertiesStatic["ExampleProperty"];
			scriptType.SafePropertiesStatic["ExampleProperty"] = text;
		}

		// Token: 0x04000012 RID: 18
		private ScriptDomain domain;

		// Token: 0x04000013 RID: 19
		private const string sourceCode = "\r\n        using UnityEngine;\r\n        class Example\r\n        {\r\n            static string exampleField = \"Hello World\";\r\n\r\n            static string ExampleProperty\r\n            {\r\n                get { return exampleField; }\r\n            }\r\n        }";
	}
}
