using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001347 RID: 4935
	[Serializable]
	public abstract class ISupportAbilitySkill : BaseSkill
	{
		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600C081 RID: 49281
		public abstract string AbilityCommand { get; }

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x0600C082 RID: 49282 RVA: 0x00468047 File Offset: 0x00466247
		public virtual string SupportType
		{
			get
			{
				return this.AbilityCommand;
			}
		}

		// Token: 0x0600C083 RID: 49283
		public abstract ActivatedAbilityEntry RequireAbility();

		// Token: 0x0600C084 RID: 49284 RVA: 0x0046804F File Offset: 0x0046624F
		public override void Initialize()
		{
			base.Initialize();
			this.AbilityEntry = this.ParentObject.GetActivatedAbilityByCommand(this.AbilityCommand) ?? this.RequireAbility();
		}

		// Token: 0x0600C085 RID: 49285 RVA: 0x00468078 File Offset: 0x00466278
		public override void Remove()
		{
			base.Remove();
			NeedPartSupportEvent.Send(this.ParentObject, this.SupportType, this);
		}

		// Token: 0x0600C086 RID: 49286 RVA: 0x00468092 File Offset: 0x00466292
		public override void FinalizeRead(SerializationReader Reader)
		{
			base.FinalizeRead(Reader);
			if (this.AbilityEntry == null)
			{
				this.AbilityEntry = this.ParentObject.GetActivatedAbilityByCommand(this.AbilityCommand);
			}
		}

		// Token: 0x0600C087 RID: 49287 RVA: 0x004680BA File Offset: 0x004662BA
		public override void FinalizeCopy(GameObject Source, bool CopyEffects, bool CopyID, Func<GameObject, GameObject> MapInv)
		{
			base.FinalizeCopy(Source, CopyEffects, CopyID, MapInv);
			if (this.AbilityEntry == null)
			{
				this.AbilityEntry = this.ParentObject.GetActivatedAbilityByCommand(this.AbilityCommand);
			}
		}

		// Token: 0x0600C088 RID: 49288 RVA: 0x004680E6 File Offset: 0x004662E6
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<PartSupportEvent>.ID;
		}

		// Token: 0x0600C089 RID: 49289 RVA: 0x004680FC File Offset: 0x004662FC
		public override bool HandleEvent(PartSupportEvent E)
		{
			return (E.Skip == this || !(E.Type == this.SupportType)) && base.HandleEvent(E);
		}

		// Token: 0x04004D4D RID: 19789
		[NonSerialized]
		public ActivatedAbilityEntry AbilityEntry;
	}
}
