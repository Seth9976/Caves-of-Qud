using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;
using XRL.World.Parts;

namespace XRL.World.AI
{
	// Token: 0x02000B3C RID: 2876
	[GenerateSerializationPartial]
	public class OpinionAttack : IOpinionCombat
	{
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06007431 RID: 29745 RVA: 0x002BDF55 File Offset: 0x002BC155
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007432 RID: 29746 RVA: 0x002BDF58 File Offset: 0x002BC158
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Weapon);
			Writer.WriteOptimized(this.WeaponArticle);
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x06007433 RID: 29747 RVA: 0x002BDF8A File Offset: 0x002BC18A
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Weapon = Reader.ReadOptimizedString();
			this.WeaponArticle = Reader.ReadOptimizedString();
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06007434 RID: 29748 RVA: 0x002BDFBC File Offset: 0x002BC1BC
		public override int BaseValue
		{
			get
			{
				return -75;
			}
		}

		// Token: 0x06007435 RID: 29749 RVA: 0x002BDFC0 File Offset: 0x002BC1C0
		public override void Initialize(GameObject Actor, GameObject Subject, GameObject Object)
		{
			if (Object != null)
			{
				this.Weapon = Object.BaseDisplayName;
				this.WeaponArticle = Object.IndefiniteArticle(false, null, false, false);
			}
			else
			{
				this.Weapon = null;
				this.WeaponArticle = null;
			}
			Brain brain = Subject.Brain;
			GameObject gameObject = ((brain != null) ? brain.GetFinalLeader() : null);
			if (gameObject != null)
			{
				Actor.Brain.AddOpinion<OpinionCompanionAttack>(gameObject, Object, 1f);
				GameObject finalLeader = Actor.Brain.GetFinalLeader();
				if (finalLeader == null)
				{
					return;
				}
				finalLeader.Brain.AddOpinion<OpinionCompanionAttackAlly>(gameObject, Actor, 1f);
			}
		}

		// Token: 0x06007436 RID: 29750 RVA: 0x002BE045 File Offset: 0x002BC245
		public override string GetText(GameObject Actor)
		{
			if (!this.Weapon.IsNullOrEmpty())
			{
				return "Attacked me with " + this.WeaponArticle + this.Weapon + ".";
			}
			return "Attacked me.";
		}

		// Token: 0x04003584 RID: 13700
		public string Weapon;

		// Token: 0x04003585 RID: 13701
		public string WeaponArticle;
	}
}
