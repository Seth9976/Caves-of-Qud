using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FE3 RID: 4067
	[ComponentExtension]
	public abstract class IPartExtension<T> : IPart where T : IPart
	{
		// Token: 0x0600A1BB RID: 41403 RVA: 0x003AFCA4 File Offset: 0x003ADEA4
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == PooledEvent<PartRemovedEvent>.ID;
		}

		// Token: 0x0600A1BC RID: 41404 RVA: 0x003AFCAE File Offset: 0x003ADEAE
		public override bool HandleEvent(PartRemovedEvent E)
		{
			if (E.Part == this.ParentPart)
			{
				E.Actor.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A1BD RID: 41405 RVA: 0x003AFCD8 File Offset: 0x003ADED8
		public override void FinalizeRead(SerializationReader Reader)
		{
			if (!this.ParentObject.TryGetPart<T>(out this.ParentPart))
			{
				MetricsManager.LogAssemblyWarning(base.GetType(), "Lost parent part reference, removing " + base.GetType().GetName(false) + " from " + this.ParentObject.DebugName);
				this.ParentObject.RemovePart(this);
			}
		}

		// Token: 0x04004334 RID: 17204
		[NonSerialized]
		public T ParentPart;
	}
}
