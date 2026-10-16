using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200013E RID: 318
	[UsedByNativeCode(Name = "ExposedReference")]
	[Serializable]
	public struct ExposedReference<T> where T : Object
	{
		// Token: 0x06000960 RID: 2400 RVA: 0x0000FE1C File Offset: 0x0000E01C
		public T Resolve(IExposedPropertyTable resolver)
		{
			bool flag = resolver != null;
			if (flag)
			{
				bool flag2;
				Object referenceValue = resolver.GetReferenceValue(this.exposedName, out flag2);
				bool flag3 = flag2;
				if (flag3)
				{
					return referenceValue as T;
				}
			}
			return this.defaultValue as T;
		}

		// Token: 0x0400042C RID: 1068
		[SerializeField]
		public PropertyName exposedName;

		// Token: 0x0400042D RID: 1069
		[SerializeField]
		public Object defaultValue;
	}
}
