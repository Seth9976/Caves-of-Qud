using System;

namespace UnityEngine
{
	// Token: 0x0200013F RID: 319
	public interface IExposedPropertyTable
	{
		// Token: 0x06000961 RID: 2401
		void SetReferenceValue(PropertyName id, Object value);

		// Token: 0x06000962 RID: 2402
		Object GetReferenceValue(PropertyName id, out bool idValid);

		// Token: 0x06000963 RID: 2403
		void ClearReferenceValue(PropertyName id);
	}
}
