using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012A0 RID: 4768
	[Serializable]
	public class WalltrapAcid : IPart
	{
		// Token: 0x0600BC70 RID: 48240 RVA: 0x0044E3C5 File Offset: 0x0044C5C5
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC71 RID: 48241 RVA: 0x0044E3C8 File Offset: 0x0044C5C8
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WalltrapTrigger");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC72 RID: 48242 RVA: 0x0044E3E0 File Offset: 0x0044C5E0
		public void Flame(Cell C)
		{
			if (C != null)
			{
				foreach (GameObject gameObject in C.GetObjectsWithPart("Combat", (GameObject obj) => obj.PhaseMatches(this.ParentObject)))
				{
					gameObject.TakeDamage(this.JetDamage.RollCached(), "from a plume of acid!", "Acid", null, null, null, this.ParentObject, null, null, null, false, true, false, false, false, false, false, false, false, 5, null);
				}
				GameObject gameObject2 = GameObject.Create("AcidPool");
				gameObject2.AddPart<Temporary>(new Temporary(3, "AcidGas10"), true, false);
				C.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
		}

		// Token: 0x0600BC73 RID: 48243 RVA: 0x0044E4AC File Offset: 0x0044C6AC
		public void FireJet(string D)
		{
			Cell cell = this.ParentObject.CurrentCell;
			for (int i = 0; i < this.JetLength; i++)
			{
				cell = cell.GetCellFromDirection(D, true);
				if (cell == null || cell.IsSolid(true))
				{
					break;
				}
				this.Flame(cell);
				if (cell.ParentZone.IsActive() && cell.IsVisible())
				{
					for (int j = 0; j < 3; j++)
					{
						string text = "&C";
						int num = Stat.Random(1, 2);
						if (num == 1)
						{
							text = "&G";
						}
						if (num == 2)
						{
							text = "&g";
						}
						int num2 = Stat.Random(1, 2);
						if (num2 == 1)
						{
							text += "^g";
						}
						if (num2 == 2)
						{
							text += "^G";
						}
						ParticleManager particleManager = XRLCore.ParticleManager;
						string text2 = ".";
						int num3 = Stat.Random(1, 3);
						if (num3 == 1)
						{
							text2 = "ø";
						}
						if (num3 == 2)
						{
							text2 = "ù";
						}
						if (num3 == 3)
						{
							text2 = "ú";
						}
						particleManager.Add(text + text2, (float)cell.X, (float)cell.Y, 0f, 0f, 10 + 2 * i + (6 - 2 * j));
					}
				}
			}
		}

		// Token: 0x0600BC74 RID: 48244 RVA: 0x0044E5D8 File Offset: 0x0044C7D8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WalltrapTrigger")
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null && !base.IsBroken() && !base.IsRusted() && !base.IsEMPed())
				{
					foreach (string text in Directions.CardinalDirectionList)
					{
						Cell cellFromDirection = currentCell.GetCellFromDirection(text, true);
						if (cellFromDirection != null && !cellFromDirection.IsSolid(true))
						{
							this.FireJet(text);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B85 RID: 19333
		public int JetLength = 6;

		// Token: 0x04004B86 RID: 19334
		public string JetDamage = "3d6";
	}
}
