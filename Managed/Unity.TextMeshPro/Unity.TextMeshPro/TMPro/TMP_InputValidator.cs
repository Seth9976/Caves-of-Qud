using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	public abstract class TMP_InputValidator : ScriptableObject
	{
		// Token: 0x060002B5 RID: 693
		public abstract char Validate(ref string text, ref int pos, char ch);
	}
}
