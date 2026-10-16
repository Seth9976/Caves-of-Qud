using System;

namespace XRL.CharacterBuilds
{
	// Token: 0x0200058E RID: 1422
	public class EmbarkBuilderModuleWindowBase<T> : AbstractBuilderModuleWindowBase where T : AbstractEmbarkBuilderModule
	{
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06003D26 RID: 15654 RVA: 0x0012BD0E File Offset: 0x00129F0E
		public T module
		{
			get
			{
				return this._module as T;
			}
		}
	}
}
