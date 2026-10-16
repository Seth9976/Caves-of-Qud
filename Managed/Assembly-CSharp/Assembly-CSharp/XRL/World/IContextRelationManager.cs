using System;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x02000956 RID: 2390
	public interface IContextRelationManager
	{
		// Token: 0x060062E5 RID: 25317
		bool RestoreContextRelation(GameObject Object, GameObject ObjectContext, Cell CellContext, BodyPart BodyPartContext, int Relation, bool Silent = true);
	}
}
