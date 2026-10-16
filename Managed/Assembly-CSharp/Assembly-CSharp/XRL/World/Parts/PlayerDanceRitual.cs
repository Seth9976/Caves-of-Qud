using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using XRL.Core;
using XRL.Messages;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200121C RID: 4636
	[Serializable]
	public class PlayerDanceRitual : IPart
	{
		// Token: 0x0600B822 RID: 47138 RVA: 0x0042F283 File Offset: 0x0042D483
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B823 RID: 47139 RVA: 0x0042F288 File Offset: 0x0042D488
		public void InitRituals()
		{
			this._RitualTypes = new Dictionary<string, RitualTypeEntry>();
			Random random = new Random(XRLCore.Core.Game.GetWorldSeed("DanceRitual"));
			List<string> list = new List<string>();
			list.Add("B");
			list.Add("R");
			list.Add("C");
			list.Add("W");
			list.Add("Y");
			list.Add("K");
			list.Add("M");
			list.Add("b");
			list.Add("r");
			list.Add("c");
			list.Add("w");
			list.Add("y");
			list.Add("g");
			list.Add("m");
			Algorithms.RandomShuffleInPlace<string>(list, random);
			this._Rituals = new List<RitualTypeEntry>();
			this._Rituals.Add(new RitualTypeEntry("Attack", list[0]));
			this._Rituals.Add(new RitualTypeEntry("Mimic", list[1]));
			this._Rituals.Add(new RitualTypeEntry("Mirror", list[2]));
			for (int i = 0; i < this._Rituals.Count; i++)
			{
				this._RitualTypes.Add(this._Rituals[i].Name, this._Rituals[i]);
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x0600B824 RID: 47140 RVA: 0x0042F3FE File Offset: 0x0042D5FE
		public List<RitualTypeEntry> Rituals
		{
			get
			{
				if (this._Rituals == null)
				{
					this.InitRituals();
				}
				return this._Rituals;
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600B825 RID: 47141 RVA: 0x0042F414 File Offset: 0x0042D614
		public Dictionary<string, RitualTypeEntry> RitualTypes
		{
			get
			{
				if (this._RitualTypes == null)
				{
					this.InitRituals();
				}
				return this._RitualTypes;
			}
		}

		// Token: 0x0600B826 RID: 47142 RVA: 0x0042F42C File Offset: 0x0042D62C
		public void ExecuteMove(string Actor, string Direction)
		{
			MessageQueue.AddPlayerMessage("&K" + Actor + " steps " + Direction, null, true);
			if (Actor == "Opponent")
			{
				this.OpponentMovementLog.Add(Direction);
			}
			if (Actor == "Player")
			{
				this.PlayerMovementLog.Add(Direction);
			}
		}

		// Token: 0x0600B827 RID: 47143 RVA: 0x0042F483 File Offset: 0x0042D683
		public void EndTurn(string Actor)
		{
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600B828 RID: 47144 RVA: 0x0042F485 File Offset: 0x0042D685
		private GameObject Opponent
		{
			get
			{
				if (this._Opponent == null)
				{
					this._Opponent = this.ParentObject.Physics.CurrentCell.ParentZone.FindObject("Angor");
				}
				return this._Opponent;
			}
		}

		// Token: 0x0600B829 RID: 47145 RVA: 0x0042F4BA File Offset: 0x0042D6BA
		public void PassStep(string Reason = "")
		{
			MessageQueue.AddPlayerMessage("&GYou executed that step correctly! [" + Reason + "]", null, true);
			this.StepsPassed++;
		}

		// Token: 0x0600B82A RID: 47146 RVA: 0x0042F4E1 File Offset: 0x0042D6E1
		public void FailStep(string Reason = "")
		{
			MessageQueue.AddPlayerMessage("&RYou executed that step incorrectly! [" + Reason + "]", null, true);
			this.StepsFailed++;
		}

		// Token: 0x0600B82B RID: 47147 RVA: 0x0042F508 File Offset: 0x0042D708
		public void FailDance(string Reason = "")
		{
			Popup.Show("The dance ended in failure! [" + Reason + "]", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.Opponent.RemovePart<DanceRitualOpponent>();
			this.ParentObject.RemovePart(this);
		}

		// Token: 0x0600B82C RID: 47148 RVA: 0x0042F542 File Offset: 0x0042D742
		public void SuccessDance(string Reason = "")
		{
			Popup.Show("The dance ended in success! [" + Reason + "]", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.Opponent.RemovePart<DanceRitualOpponent>();
			this.ParentObject.RemovePart(this);
		}

		// Token: 0x0600B82D RID: 47149 RVA: 0x0042F57C File Offset: 0x0042D77C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeTakeAction");
			Registrar.Register("AfterMoved");
			Registrar.Register("CanHaveConversation");
			Registrar.Register("EndAction");
			Registrar.Register("EndTurn");
			Registrar.Register("DanceOpponentDied");
			if (this.Opponent == null)
			{
				this.FailDance("Angor couldn't be found here");
			}
			else
			{
				this.Opponent.AddPart<DanceRitualOpponent>(new DanceRitualOpponent(), true, false);
			}
			this.CurrentLeader = "Angor";
			this.CurrentState = "#MakeChoice";
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B82E RID: 47150 RVA: 0x0042F614 File Offset: 0x0042D814
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndAction")
			{
				this.ParentObject.Energy.BaseValue = 999;
				this.Opponent.Energy.BaseValue = 1001;
				return true;
			}
			if (E.ID == "BeforeTakeAction")
			{
				return true;
			}
			if (E.ID == "AfterMoved")
			{
				this.ExecuteMove("Player", E.GetStringParameter("Direction", null));
				return true;
			}
			if (E.ID == "DanceOpponentDied")
			{
				this.SuccessDance("Opponent died!");
				return true;
			}
			if (E.ID == "DanceOpponentDied")
			{
				this.SuccessDance("Your opponent perished!");
				return true;
			}
			if (!(E.ID == "EndTurn"))
			{
				return true;
			}
			this.EndTurn("Player");
			MessageQueue.AddPlayerMessage("&KDebug: Dance party turn tick " + this.n.ToString() + " Current Approval:" + this.Approval.ToString(), null, true);
			this.n++;
			if (this.ParentObject.Physics.CurrentCell.ParentZone.FindObject("Angor") == null)
			{
				this.FailDance("You left the dance early!");
				return true;
			}
			return true;
		}

		// Token: 0x04004A13 RID: 18963
		public int n;

		// Token: 0x04004A14 RID: 18964
		public int Approval;

		// Token: 0x04004A15 RID: 18965
		public string CurrentLeader = "";

		// Token: 0x04004A16 RID: 18966
		public string CurrentState = "";

		// Token: 0x04004A17 RID: 18967
		public List<string> PlayerMovementLog = new List<string>();

		// Token: 0x04004A18 RID: 18968
		public List<string> OpponentMovementLog = new List<string>();

		// Token: 0x04004A19 RID: 18969
		public int TurnsLeft;

		// Token: 0x04004A1A RID: 18970
		public int StepsFailed;

		// Token: 0x04004A1B RID: 18971
		public int StepsPassed;

		// Token: 0x04004A1C RID: 18972
		[NonSerialized]
		private Dictionary<string, RitualTypeEntry> _RitualTypes;

		// Token: 0x04004A1D RID: 18973
		[NonSerialized]
		private List<RitualTypeEntry> _Rituals;

		// Token: 0x04004A1E RID: 18974
		[NonSerialized]
		private GameObject _Opponent;
	}
}
