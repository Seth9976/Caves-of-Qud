using System;

namespace XRL.World.Parts
{
	// Token: 0x0200122F RID: 4655
	[Serializable]
	public abstract class ISupportAbilitySkillPart : IPart
	{
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x0600B8DA RID: 47322
		public abstract string AbilityCommand { get; }

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x0600B8DB RID: 47323 RVA: 0x004367AB File Offset: 0x004349AB
		public virtual string SupportType
		{
			get
			{
				return this.AbilityCommand;
			}
		}

		// Token: 0x0600B8DC RID: 47324
		public abstract ActivatedAbilityEntry RequireAbility();

		// Token: 0x0600B8DD RID: 47325 RVA: 0x004367B3 File Offset: 0x004349B3
		public override void Initialize()
		{
			base.Initialize();
			this.AbilityEntry = this.ParentObject.GetActivatedAbilityByCommand(this.AbilityCommand) ?? this.RequireAbility();
		}

		// Token: 0x0600B8DE RID: 47326 RVA: 0x004367DC File Offset: 0x004349DC
		public override void Remove()
		{
			base.Remove();
			this.ParentObject.RemoveActivatedAbilityByCommand(this.AbilityCommand);
		}

		// Token: 0x0600B8DF RID: 47327 RVA: 0x004367F6 File Offset: 0x004349F6
		public override void FinalizeRead(SerializationReader Reader)
		{
			base.FinalizeRead(Reader);
			if (this.AbilityEntry == null)
			{
				this.AbilityEntry = this.ParentObject.GetActivatedAbilityByCommand(this.AbilityCommand);
			}
		}

		// Token: 0x0600B8E0 RID: 47328 RVA: 0x0043681E File Offset: 0x00434A1E
		public override void FinalizeCopy(GameObject Source, bool CopyEffects, bool CopyID, Func<GameObject, GameObject> MapInv)
		{
			base.FinalizeCopy(Source, CopyEffects, CopyID, MapInv);
			if (this.AbilityEntry == null)
			{
				this.AbilityEntry = this.ParentObject.GetActivatedAbilityByCommand(this.AbilityCommand);
			}
		}

		// Token: 0x0600B8E1 RID: 47329 RVA: 0x0043684A File Offset: 0x00434A4A
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<NeedPartSupportEvent>.ID;
		}

		// Token: 0x0600B8E2 RID: 47330 RVA: 0x00436860 File Offset: 0x00434A60
		public override bool HandleEvent(NeedPartSupportEvent E)
		{
			if (E.Type == this.SupportType && !PartSupportEvent.Check(E, this))
			{
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004A69 RID: 19049
		[NonSerialized]
		public ActivatedAbilityEntry AbilityEntry;
	}
}
