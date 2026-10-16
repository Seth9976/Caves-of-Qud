using System;
using XRL.World.Parts;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A17 RID: 2583
	public class Animated : IObjectBuilder
	{
		// Token: 0x06006D32 RID: 27954 RVA: 0x0024937D File Offset: 0x0024757D
		public override void Apply(GameObject Object, string Context)
		{
			AnimateObject.Animate(Object, null, null);
		}
	}
}
