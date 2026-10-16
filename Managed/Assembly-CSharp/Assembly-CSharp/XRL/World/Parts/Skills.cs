using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XRL.Collections;
using XRL.Messages;
using XRL.UI;
using XRL.Wish;
using XRL.World.Parts.Skill;
using XRL.World.Skills;

namespace XRL.World.Parts
{
	// Token: 0x02001233 RID: 4659
	[HasWishCommand]
	[Serializable]
	public class Skills : IPart
	{
		// Token: 0x0600B90D RID: 47373 RVA: 0x00438A20 File Offset: 0x00436C20
		public static BaseSkill GetGenericSkill(string Skill, GameObject Actor = null)
		{
			BaseSkill baseSkill = ((Actor != null) ? Actor.GetPart(Skill) : null) as BaseSkill;
			if (baseSkill != null)
			{
				return baseSkill;
			}
			if (Skills.GenericSkills.TryGetValue(Skill, out baseSkill))
			{
				return baseSkill;
			}
			Type type = ModManager.ResolveType("XRL.World.Parts.Skill." + Skill, false, false, true);
			if (type == null)
			{
				Debug.LogWarning("Cannot resolve skill type for " + Skill);
				return null;
			}
			baseSkill = Activator.CreateInstance(type) as BaseSkill;
			if (baseSkill == null)
			{
				Debug.LogWarning(Skill + " is not a skill");
				return null;
			}
			Skills.GenericSkills[Skill] = baseSkill;
			return baseSkill;
		}

		// Token: 0x0600B90E RID: 47374 RVA: 0x00438AB3 File Offset: 0x00436CB3
		public override IPart DeepCopy(GameObject Parent)
		{
			Skills skills = base.DeepCopy(Parent) as Skills;
			skills.SkillList = new List<BaseSkill>(this.SkillList);
			return skills;
		}

		// Token: 0x0600B90F RID: 47375 RVA: 0x00438AD2 File Offset: 0x00436CD2
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			base.Write(Basis, Writer);
			Writer.Write(this.SkillList.Count);
		}

		// Token: 0x0600B910 RID: 47376 RVA: 0x00438AED File Offset: 0x00436CED
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			Reader.ReadInt32();
		}

		// Token: 0x0600B911 RID: 47377 RVA: 0x00438AFE File Offset: 0x00436CFE
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600B912 RID: 47378 RVA: 0x00438B04 File Offset: 0x00436D04
		public void AddSkill(string Class)
		{
			if (this.ParentObject.HasSkill(Class))
			{
				return;
			}
			BaseSkill baseSkill = ModManager.CreateInstance("XRL.World.Parts.Skill." + Class) as BaseSkill;
			if (baseSkill != null)
			{
				this.AddSkill(baseSkill, null, null);
			}
		}

		// Token: 0x0600B913 RID: 47379 RVA: 0x00438B44 File Offset: 0x00436D44
		public bool AddSkill(BaseSkill NewSkill, GameObject Source = null, string Context = null)
		{
			if (this.ParentObject.HasSkill(NewSkill.Name))
			{
				return false;
			}
			NewSkill.ParentObject = this.ParentObject;
			BeforeAddSkillEvent beforeAddSkillEvent = BeforeAddSkillEvent.FromSkill(NewSkill, Source, Context);
			IBaseSkillEntry entry = beforeAddSkillEvent.Entry;
			BeforeAddSkillEvent.Send(beforeAddSkillEvent);
			if (!NewSkill.AddSkill(this.ParentObject))
			{
				return false;
			}
			this.ParentObject.AddPart<BaseSkill>(NewSkill, true, false);
			this.SkillList.Add(NewSkill);
			List<BaseSkill> list = null;
			if (!beforeAddSkillEvent.Include.IsNullOrEmpty<IBaseSkillEntry>())
			{
				list = new List<BaseSkill>(beforeAddSkillEvent.Include.Count);
				foreach (IBaseSkillEntry baseSkillEntry in beforeAddSkillEvent.Include)
				{
					BaseSkill baseSkill = ModManager.CreateInstance("XRL.World.Parts.Skill." + baseSkillEntry.Class) as BaseSkill;
					if (baseSkill != null)
					{
						list.Add(baseSkill);
					}
				}
			}
			BeforeAddSkillEvent.ResetTo(ref beforeAddSkillEvent);
			if (!list.IsNullOrEmpty<BaseSkill>())
			{
				string text = Context.AddDelimitedSubstring(',', "Inclusion", StringComparison.Ordinal);
				for (int i = list.Count - 1; i >= 0; i--)
				{
					if (!this.AddSkill(list[i], Source, text))
					{
						list.RemoveAt(i);
					}
				}
			}
			AfterAddSkillEvent.Send(this.ParentObject, NewSkill, entry, Source, Context, list);
			return true;
		}

		// Token: 0x0600B914 RID: 47380 RVA: 0x00438CA0 File Offset: 0x00436EA0
		public void RemoveSkill(BaseSkill Skill)
		{
			if (Skill == null)
			{
				return;
			}
			BeforeRemoveSkillEvent.Send(this.ParentObject, Skill);
			if (Skill.RemoveSkill(this.ParentObject))
			{
				this.ParentObject.RemovePart(Skill);
				this.SkillList.Remove(Skill);
				SkillEntry skillEntry;
				if (SkillFactory.Factory.SkillByClass.TryGetValue(Skill.Name, out skillEntry))
				{
					foreach (PowerEntry powerEntry in skillEntry.Powers.Values)
					{
						if (powerEntry.Cost == 0)
						{
							BaseSkill baseSkill = this.ParentObject.GetPart(powerEntry.Class) as BaseSkill;
							if (baseSkill != null)
							{
								this.RemoveSkill(baseSkill);
							}
						}
					}
				}
			}
			AfterRemoveSkillEvent.Send(this.ParentObject, Skill);
		}

		// Token: 0x0600B915 RID: 47381 RVA: 0x00438D7C File Offset: 0x00436F7C
		[WishCommand(null, null, Command = "skill")]
		public static void WishSkill(string argument)
		{
			if (Skills.WishSkillAdd(argument))
			{
				return;
			}
			foreach (SkillEntry skillEntry in SkillFactory.Factory.SkillByClass.Values)
			{
				if (skillEntry.Name.EqualsNoCase(argument) && Skills.WishSkillAdd(skillEntry.Class))
				{
					return;
				}
			}
			foreach (PowerEntry powerEntry in SkillFactory.Factory.PowersByClass.Values)
			{
				if (powerEntry.Name.EqualsNoCase(argument) && Skills.WishSkillAdd(powerEntry.Class))
				{
					return;
				}
			}
			Popup.Show("No skill by that name could be found.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x0600B916 RID: 47382 RVA: 0x00438E70 File Offset: 0x00437070
		[WishCommand(null, null, Command = "allskills")]
		public static void WishSkillAll()
		{
			Popup.Suppress = true;
			MessageQueue.Suppress = true;
			foreach (SkillEntry skillEntry in SkillFactory.Factory.SkillByClass.Values)
			{
				Skills.WishSkillAdd(skillEntry.Class);
			}
			foreach (PowerEntry powerEntry in SkillFactory.Factory.PowersByClass.Values)
			{
				Skills.WishSkillAdd(powerEntry.Class);
			}
			Popup.Suppress = false;
			MessageQueue.Suppress = false;
			IComponent<GameObject>.XDidY(IComponent<GameObject>.ThePlayer, "gain", "all the skills", "!", null, null, null, null, false, false, null, null, false, false, false, true, false, null);
		}

		// Token: 0x0600B917 RID: 47383 RVA: 0x00438F5C File Offset: 0x0043715C
		private static bool WishSkillAdd(string Class)
		{
			GameObject thePlayer = IComponent<GameObject>.ThePlayer;
			Skills skills = ((thePlayer != null) ? thePlayer.GetPart<Skills>() : null);
			if (skills == null)
			{
				return false;
			}
			Type type = ModManager.ResolveType("XRL.World.Parts.Skill." + Class, true, false, true);
			if (type == null)
			{
				return false;
			}
			BaseSkill baseSkill = Activator.CreateInstance(type) as BaseSkill;
			if (baseSkill == null)
			{
				return false;
			}
			IComponent<GameObject>.XDidY(IComponent<GameObject>.ThePlayer, "gain", "the skill " + baseSkill.DisplayName, "!", null, null, null, null, false, false, null, null, false, false, false, true, false, null);
			skills.AddSkill(baseSkill, null, null);
			return true;
		}

		// Token: 0x0600B918 RID: 47384 RVA: 0x00438FF0 File Offset: 0x004371F0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (BaseSkill baseSkill in this.SkillList)
			{
				stringBuilder.Append(baseSkill.Name).Append('\n');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04004A7A RID: 19066
		[NonSerialized]
		public List<BaseSkill> SkillList = new List<BaseSkill>();

		// Token: 0x04004A7B RID: 19067
		[NonSerialized]
		public static readonly StringMap<string> WeaponClassName = new StringMap<string>
		{
			{ "ShortBlades", "short-blade" },
			{ "LongBlades", "long-blade" },
			{ "Cudgel", "cudgel" },
			{ "Axe", "axe" },
			{ "Pistol", "pistol" },
			{ "Rifles", "bow and rifle" },
			{ "HeavyWeapons", "heavy weapon" }
		};

		// Token: 0x04004A7C RID: 19068
		[NonSerialized]
		private static Dictionary<string, BaseSkill> GenericSkills = new Dictionary<string, BaseSkill>(32);
	}
}
