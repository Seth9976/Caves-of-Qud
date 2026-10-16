using System;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000023 RID: 35
	// (Invoke) Token: 0x06000099 RID: 153
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal delegate bool EventConsumer(in Event ev);
}
