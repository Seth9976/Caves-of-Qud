using System;
using System.Collections.Generic;
using System.Text;
using Qud.API;
using XRL.Names;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200123A RID: 4666
	[Serializable]
	public class SmartItem : IPart
	{
		// Token: 0x0600B993 RID: 47507 RVA: 0x0043C4ED File Offset: 0x0043A6ED
		public override bool SameAs(IPart p)
		{
			return !((p as SmartItem).Name != this.Name) && base.SameAs(p);
		}

		// Token: 0x0600B994 RID: 47508 RVA: 0x0043C510 File Offset: 0x0043A710
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600B995 RID: 47509 RVA: 0x0043C53E File Offset: 0x0043A73E
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "Killed");
			E.Actor.RegisterPartEvent(this, "DrinkingFrom");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B996 RID: 47510 RVA: 0x0043C569 File Offset: 0x0043A769
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "Killed");
			E.Actor.UnregisterPartEvent(this, "DrinkingFrom");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B997 RID: 47511 RVA: 0x0043C594 File Offset: 0x0043A794
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood())
			{
				E.AddAdjective("{{R|" + this.Name + "}} the", -60);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B998 RID: 47512 RVA: 0x0043C5C4 File Offset: 0x0043A7C4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.Append("\n{{R|").Append(this.Name).Append(this.ParentObject.Is)
				.Append(" sentient and ")
				.Append(this.ParentObject.it)
				.Append(' ')
				.Append(this.GetFeelingDescription())
				.Append(".}}");
			this.AppendLikesAndDislikes(E.Postfix);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B999 RID: 47513 RVA: 0x0043C647 File Offset: 0x0043A847
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600B99A RID: 47514 RVA: 0x0043C64C File Offset: 0x0043A84C
		public string GetFeelingDescription()
		{
			if (this.FeelingTowardsPlayer <= -200)
			{
				return "hates you";
			}
			if (this.FeelingTowardsPlayer <= -100)
			{
				return "dislikes you";
			}
			if (this.FeelingTowardsPlayer < 100)
			{
				return "feels neutral towards you";
			}
			if (this.FeelingTowardsPlayer >= 200)
			{
				return "loves you";
			}
			return "likes you";
		}

		// Token: 0x0600B99B RID: 47515 RVA: 0x0043C6A4 File Offset: 0x0043A8A4
		public string GetRandomAttitude()
		{
			int num = Stat.Random(1, 100);
			if (num <= 95)
			{
				return "faction:" + PopulationManager.GenerateOne("RandomFaction", null, null, null, null, null).Blueprint;
			}
			if (num <= 99)
			{
				return "drink:" + PopulationManager.GenerateOne("RandomLiquid", null, null, null, null, null).Blueprint;
			}
			return "violence:null";
		}

		// Token: 0x0600B99C RID: 47516 RVA: 0x0043C708 File Offset: 0x0043A908
		public string FormatAttitude(string A)
		{
			string[] array = A.Split(':', StringSplitOptions.None);
			if (array[0] == "faction")
			{
				return Faction.GetFormattedName(array[1]);
			}
			if (array[0] == "drink")
			{
				return "when its wielder drinks " + array[1];
			}
			if (array[0] == "violence")
			{
				return "violence";
			}
			return A;
		}

		// Token: 0x0600B99D RID: 47517 RVA: 0x0043C76C File Offset: 0x0043A96C
		public string AppendLikesAndDislikes(StringBuilder SB)
		{
			SB.Append("\n&R").Append(this.Name);
			SB.Append(" likes ");
			for (int i = 0; i < this.Likes.Count; i++)
			{
				if (this.Likes.Count > 1 && i == this.Likes.Count - 1)
				{
					SB.Append(" and ");
				}
				else if (i > 0)
				{
					SB.Append(", ");
				}
				SB.Append(this.FormatAttitude(this.Likes[i]));
			}
			SB.Append("; and hates ");
			for (int j = 0; j < this.Dislikes.Count; j++)
			{
				if (this.Dislikes.Count > 1 && j == this.Dislikes.Count - 1)
				{
					SB.Append(" and ");
				}
				else if (j > 0)
				{
					SB.Append(", ");
				}
				SB.Append(this.FormatAttitude(this.Dislikes[j]));
			}
			SB.Append(".");
			return SB.ToString();
		}

		// Token: 0x0600B99E RID: 47518 RVA: 0x0043C890 File Offset: 0x0043AA90
		public override void Initialize()
		{
			base.Initialize();
			this.SetUpSmartItem(-1, -1);
		}

		// Token: 0x0600B99F RID: 47519 RVA: 0x0043C8A0 File Offset: 0x0043AAA0
		public void SetUpSmartItem(int nLikes = -1, int nDislikes = -1)
		{
			if (this.Initialized)
			{
				return;
			}
			this.Initialized = true;
			this.Name = NameMaker.MakeName(EncountersAPI.GetACreature(null), null, null, null, null, null, null, null, null, null, null, null, null, false, false, null, null);
			if (nLikes == -1)
			{
				nLikes = Stat.Roll("1d5-2", null);
			}
			if (nLikes < 1)
			{
				nLikes = 1;
			}
			if (nDislikes == -1)
			{
				nDislikes = Stat.Roll("1d5-2", null);
			}
			if (nDislikes < 1)
			{
				nDislikes = 1;
			}
			for (int i = 0; i < nLikes; i++)
			{
				string randomAttitude = this.GetRandomAttitude();
				if (this.Likes.Contains(randomAttitude) || this.Dislikes.Contains(randomAttitude))
				{
					i--;
				}
				else
				{
					this.Likes.Add(randomAttitude);
				}
			}
			for (int j = 0; j < nDislikes; j++)
			{
				string randomAttitude2 = this.GetRandomAttitude();
				if (this.Likes.Contains(randomAttitude2) || this.Dislikes.Contains(randomAttitude2))
				{
					j--;
				}
				else
				{
					this.Dislikes.Add(randomAttitude2);
				}
			}
		}

		// Token: 0x0600B9A0 RID: 47520 RVA: 0x0043C9A4 File Offset: 0x0043ABA4
		public bool EquippedToPlayer()
		{
			return this.ParentObject.Equipped != null && this.ParentObject.Equipped.IsPlayer();
		}

		// Token: 0x04004A9B RID: 19099
		public new string Name;

		// Token: 0x04004A9C RID: 19100
		public int FeelingTowardsPlayer;

		// Token: 0x04004A9D RID: 19101
		public List<string> Likes = new List<string>();

		// Token: 0x04004A9E RID: 19102
		public List<string> Dislikes = new List<string>();

		// Token: 0x04004A9F RID: 19103
		public bool Initialized;
	}
}
