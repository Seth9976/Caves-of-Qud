using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x0200012A RID: 298
	internal static class StateMachineUtility
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x0000FD7A File Offset: 0x0000DF7A
		public static int GetState(IAsyncStateMachine stateMachine)
		{
			return (int)stateMachine.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).First<FieldInfo>((FieldInfo x) => x.Name.EndsWith("__state"))
				.GetValue(stateMachine);
		}
	}
}
