using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B50 RID: 2896
	[GenerateSerializationPartial]
	public class OpinionThief : IOpinionObject
	{
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060074B2 RID: 29874 RVA: 0x002BE776 File Offset: 0x002BC976
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060074B3 RID: 29875 RVA: 0x002BE77C File Offset: 0x002BC97C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Item);
			Writer.WriteOptimized(this.ItemArticle);
			Writer.WriteOptimized(this.From);
			Writer.WriteOptimized(this.FromArticle);
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x060074B4 RID: 29876 RVA: 0x002BE7D4 File Offset: 0x002BC9D4
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Item = Reader.ReadOptimizedString();
			this.ItemArticle = Reader.ReadOptimizedString();
			this.From = Reader.ReadOptimizedString();
			this.FromArticle = Reader.ReadOptimizedString();
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060074B5 RID: 29877 RVA: 0x002BE829 File Offset: 0x002BCA29
		public override int BaseValue
		{
			get
			{
				return -20;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060074B6 RID: 29878 RVA: 0x002BE82D File Offset: 0x002BCA2D
		public override float Limit
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x060074B7 RID: 29879 RVA: 0x002BE834 File Offset: 0x002BCA34
		public override int Cooldown
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060074B8 RID: 29880 RVA: 0x002BE838 File Offset: 0x002BCA38
		public override void Initialize(GameObject Actor, GameObject Subject, GameObject Object)
		{
			if (((Object != null) ? Object.Render : null) != null)
			{
				this.Item = Object.Render.DisplayName;
				this.ItemArticle = Object.IndefiniteArticle(false, null, true, false);
			}
			else
			{
				this.Item = null;
				this.ItemArticle = null;
			}
			Zone currentZone = Subject.GetCurrentZone();
			if (currentZone != null && currentZone.HasProperName)
			{
				this.From = currentZone.BaseDisplayName;
				this.FromArticle = null;
				return;
			}
			if (Object != null && !Object.Owner.IsNullOrEmpty())
			{
				Faction faction = Factions.Get(Object.Owner);
				this.From = faction.DisplayName;
				this.FromArticle = (faction.FormatWithArticle ? "the " : null);
				return;
			}
			this.From = null;
			this.FromArticle = null;
		}

		// Token: 0x060074B9 RID: 29881 RVA: 0x002BE8F8 File Offset: 0x002BCAF8
		public override string GetText(GameObject Actor)
		{
			return string.Concat(new string[]
			{
				"Stole ",
				this.ItemArticle,
				this.Item ?? "something",
				" from ",
				this.FromArticle,
				this.From ?? "us",
				"."
			});
		}

		// Token: 0x0400358A RID: 13706
		public string Item;

		// Token: 0x0400358B RID: 13707
		public string ItemArticle;

		// Token: 0x0400358C RID: 13708
		public string From;

		// Token: 0x0400358D RID: 13709
		public string FromArticle;
	}
}
