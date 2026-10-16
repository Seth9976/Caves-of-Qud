using System;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200061A RID: 1562
	internal class BuilderLocation
	{
		// Token: 0x0600430A RID: 17162 RVA: 0x000B1088 File Offset: 0x000AF288
		public BuilderLocation(ControlBuilder builder, ILocation location)
		{
			this.Builder = builder;
			this.Location = new Location(location);
		}

		// Token: 0x040023F2 RID: 9202
		public ControlBuilder Builder;

		// Token: 0x040023F3 RID: 9203
		public ILocation Location;
	}
}
