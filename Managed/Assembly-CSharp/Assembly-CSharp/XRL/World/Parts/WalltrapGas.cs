using System;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012A4 RID: 4772
	[Serializable]
	public class WalltrapGas : IPart
	{
		// Token: 0x0600BC88 RID: 48264 RVA: 0x0044EBC4 File Offset: 0x0044CDC4
		public override bool SameAs(IPart p)
		{
			WalltrapGas walltrapGas = p as WalltrapGas;
			return walltrapGas.JetLength == this.JetLength && !(walltrapGas.GasBlueprint != this.GasBlueprint) && !(walltrapGas.Density != this.Density) && !(walltrapGas.PrimaryColor != this.PrimaryColor) && !(walltrapGas.SecondaryColor != this.SecondaryColor);
		}

		// Token: 0x0600BC89 RID: 48265 RVA: 0x0044EC3D File Offset: 0x0044CE3D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WalltrapTrigger");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC8A RID: 48266 RVA: 0x0044EC54 File Offset: 0x0044CE54
		public void DeployGas(Cell C)
		{
			if (C == null)
			{
				return;
			}
			GameObject gameObject = GameObject.Create(this.GasBlueprint);
			Gas part = gameObject.GetPart<Gas>();
			if (part != null)
			{
				part.Density = this.Density.RollCached();
			}
			C.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}

		// Token: 0x0600BC8B RID: 48267 RVA: 0x0044ECA4 File Offset: 0x0044CEA4
		public void ReleaseGas(Cell C, string D)
		{
			for (int i = 0; i < this.JetLength; i++)
			{
				C = C.GetCellFromDirection(D, true);
				if (C == null || C.IsSolid(true))
				{
					break;
				}
				this.DeployGas(C);
				if (C.ParentZone.IsActive() && C.IsVisible())
				{
					for (int j = 0; j < 3; j++)
					{
						StringBuilder stringBuilder = Event.NewStringBuilder(null);
						stringBuilder.Append("&");
						stringBuilder.Append((Stat.Random(1, 2) == 1) ? this.PrimaryColor : this.SecondaryColor);
						stringBuilder.Append("^");
						stringBuilder.Append((Stat.Random(1, 2) == 1) ? this.PrimaryColor : this.SecondaryColor);
						ParticleManager particleManager = XRLCore.ParticleManager;
						char c = '.';
						int num = Stat.Random(1, 3);
						if (num == 1)
						{
							c = 'ø';
						}
						else if (num == 2)
						{
							c = 'ù';
						}
						else if (num == 3)
						{
							c = 'ú';
						}
						stringBuilder.Append(c);
						particleManager.Add(stringBuilder.ToString(), (float)C.X, (float)C.Y, 0f, 0f, 10 + 2 * i + (6 - 2 * j));
					}
				}
			}
		}

		// Token: 0x0600BC8C RID: 48268 RVA: 0x0044EDE4 File Offset: 0x0044CFE4
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
							this.ReleaseGas(currentCell, text);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B8F RID: 19343
		public int JetLength = 6;

		// Token: 0x04004B90 RID: 19344
		public string GasBlueprint = "AcidGas";

		// Token: 0x04004B91 RID: 19345
		public string Density = "1d80+20";

		// Token: 0x04004B92 RID: 19346
		public string PrimaryColor = "G";

		// Token: 0x04004B93 RID: 19347
		public string SecondaryColor = "g";
	}
}
