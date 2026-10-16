using System;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000E90 RID: 3728
	[Serializable]
	public class SpawnVessel : IPart
	{
		// Token: 0x06009622 RID: 38434 RVA: 0x00366174 File Offset: 0x00364374
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009623 RID: 38435 RVA: 0x00366178 File Offset: 0x00364378
		public override void TurnTick(long TimeTick, int Amount)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null)
			{
				this.Spawn(currentCell);
			}
		}

		// Token: 0x06009624 RID: 38436 RVA: 0x0036619C File Offset: 0x0036439C
		public void Spawn(Cell Cell)
		{
			if (this.SpawnTurns == -2147483648)
			{
				this.SpawnTurns = this.SpawnTime.RollCached();
			}
			this.SpawnTurns--;
			if (this.SpawnTurns > 0)
			{
				return;
			}
			if (!this.SpawnSound.IsNullOrEmpty())
			{
				Cell.PlayWorldSound(this.SpawnSound, 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			if (!this.SpawnMessage.IsNullOrEmpty())
			{
				base.EmitMessage(GameText.VariableReplace(this.SpawnMessage, this.ParentObject, this.SpawnedBy, false), ' ', false, false, false, null, null);
			}
			if (!this.SpawnVerb.IsNullOrEmpty())
			{
				base.DidX(this.SpawnVerb, null, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			}
			if (!this.ReplaceBlueprint.IsNullOrEmpty())
			{
				Cell.AddObject(this.ReplaceBlueprint, null, null, null, null);
			}
			if (this.SlimesplatterOnSpawn)
			{
				this.ParentObject.Slimesplatter(false, "&g.");
			}
			this.ParentObject.Destroy(null, false, false, null);
			int num = this.SpawnAmount.RollCached();
			for (int i = 0; i < num; i++)
			{
				if (this.SpawnChance.in100())
				{
					GameObject gameObject = GameObject.Create(this.SpawnBlueprint);
					if (this.AdjustAttitude && GameObject.Validate(ref this.SpawnedBy))
					{
						gameObject.TakeAllegiance<AllyBirth>(this.SpawnedBy);
					}
					Cell connectedSpawnLocation = Cell.GetConnectedSpawnLocation(true);
					if (connectedSpawnLocation == null)
					{
						break;
					}
					connectedSpawnLocation.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					if (gameObject.IsValid())
					{
						gameObject.PlayWorldSoundTag("AmbientIdleSound", null, null, 0.5f);
						gameObject.MakeActive(false);
					}
				}
			}
		}

		// Token: 0x04003E1F RID: 15903
		public int SpawnTurns = int.MinValue;

		// Token: 0x04003E20 RID: 15904
		public string SpawnBlueprint;

		// Token: 0x04003E21 RID: 15905
		public string ReplaceBlueprint;

		// Token: 0x04003E22 RID: 15906
		public string SpawnMessage;

		// Token: 0x04003E23 RID: 15907
		public string SpawnVerb;

		// Token: 0x04003E24 RID: 15908
		public string SpawnAmount = "1";

		// Token: 0x04003E25 RID: 15909
		public string SpawnTime = "10";

		// Token: 0x04003E26 RID: 15910
		public string SpawnSound;

		// Token: 0x04003E27 RID: 15911
		public GameObject SpawnedBy;

		// Token: 0x04003E28 RID: 15912
		public bool AdjustAttitude;

		// Token: 0x04003E29 RID: 15913
		public bool SlimesplatterOnSpawn;

		// Token: 0x04003E2A RID: 15914
		public bool SpawnOnEmpty;

		// Token: 0x04003E2B RID: 15915
		public int SpawnChance = 100;
	}
}
