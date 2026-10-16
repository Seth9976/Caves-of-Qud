using System;
using System.Collections.Generic;
using XRL.World;

namespace XRL.CharacterCreation
{
	// Token: 0x02000576 RID: 1398
	public interface ICustomChargenClass
	{
		// Token: 0x06003C66 RID: 15462
		void BuildCharacterBody(GameObject body);

		// Token: 0x06003C67 RID: 15463
		IEnumerable<string> GetChargenInfo();
	}
}
