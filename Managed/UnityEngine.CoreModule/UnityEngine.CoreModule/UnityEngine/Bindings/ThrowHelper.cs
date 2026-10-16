using System;
using System.Diagnostics.CodeAnalysis;

namespace UnityEngine.Bindings
{
	// Token: 0x02000363 RID: 867
	[VisibleToOtherModules]
	internal static class ThrowHelper
	{
		// Token: 0x060028B7 RID: 10423 RVA: 0x0004E960 File Offset: 0x0004CB60
		[DoesNotReturn]
		public static void ThrowArgumentNullException(object obj, string parameterName)
		{
			Object @object = obj as Object;
			bool flag = @object != null;
			if (flag)
			{
				Object.MarshalledUnityObject.TryThrowEditorNullExceptionObject(@object, parameterName);
			}
			throw new ArgumentNullException(parameterName);
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x0004E98C File Offset: 0x0004CB8C
		[DoesNotReturn]
		public static void ThrowNullReferenceException(object obj)
		{
			Object @object = obj as Object;
			bool flag = @object != null;
			if (flag)
			{
				Object.MarshalledUnityObject.TryThrowEditorNullExceptionObject(@object, null);
			}
			throw new NullReferenceException();
		}
	}
}
