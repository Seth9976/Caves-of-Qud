using System;

namespace XRL.World.ObjectBuilders
{
	/// <summary>A game object builder singleton.</summary>
	// Token: 0x02000A19 RID: 2585
	public abstract class IObjectBuilder
	{
		// Token: 0x06006D37 RID: 27959 RVA: 0x002496B3 File Offset: 0x002478B3
		public virtual void Initialize()
		{
		}

		// Token: 0x06006D38 RID: 27960
		public abstract void Apply(GameObject Object, string Context);
	}
}
