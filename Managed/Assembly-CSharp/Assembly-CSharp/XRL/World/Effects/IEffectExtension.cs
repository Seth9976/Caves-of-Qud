using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D4D RID: 3405
	[ComponentExtension]
	public abstract class IEffectExtension<T> : Effect where T : Effect
	{
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600869B RID: 34459 RVA: 0x00306650 File Offset: 0x00304850
		public override string ApplySound
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600869C RID: 34460 RVA: 0x00306653 File Offset: 0x00304853
		public override string RemoveSound
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600869D RID: 34461 RVA: 0x00306656 File Offset: 0x00304856
		protected IEffectExtension()
		{
			this.Duration = 9999;
		}

		// Token: 0x0600869E RID: 34462 RVA: 0x00306669 File Offset: 0x00304869
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x0600869F RID: 34463 RVA: 0x0030666C File Offset: 0x0030486C
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == EffectRemovedEvent.ID;
		}

		// Token: 0x060086A0 RID: 34464 RVA: 0x00306676 File Offset: 0x00304876
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			if (E.Effect == this.ParentEffect)
			{
				base.Object.RemoveEffect(this, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060086A1 RID: 34465 RVA: 0x003066A0 File Offset: 0x003048A0
		public override void FinalizeRead(SerializationReader Reader)
		{
			if (!base.Object.TryGetEffect<T>(this.ParentEffectID, out this.ParentEffect))
			{
				MetricsManager.LogAssemblyWarning(base.GetType(), "Lost parent effect reference, removing " + base.GetType().GetName(false) + " from " + base.Object.DebugName);
				base.Object.RemoveEffect(this, true);
			}
		}

		// Token: 0x040039EA RID: 14826
		public Guid ParentEffectID;

		// Token: 0x040039EB RID: 14827
		[NonSerialized]
		public T ParentEffect;
	}
}
