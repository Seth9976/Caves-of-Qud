using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B3D RID: 2877
	[GenerateSerializationPartial]
	public class OpinionAttackAlly : IOpinionCombat
	{
		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06007438 RID: 29752 RVA: 0x002BE07D File Offset: 0x002BC27D
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007439 RID: 29753 RVA: 0x002BE080 File Offset: 0x002BC280
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Ally);
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x0600743A RID: 29754 RVA: 0x002BE0A6 File Offset: 0x002BC2A6
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Ally = Reader.ReadOptimizedString();
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600743B RID: 29755 RVA: 0x002BE0CC File Offset: 0x002BC2CC
		public override int BaseValue
		{
			get
			{
				return -75;
			}
		}

		// Token: 0x0600743C RID: 29756 RVA: 0x002BE0D0 File Offset: 0x002BC2D0
		public override void Initialize(GameObject Actor, GameObject Subject, GameObject Object)
		{
			if (Object.HasProperName)
			{
				this.Ally = Object.Render.DisplayName;
				return;
			}
			this.Ally = null;
		}

		// Token: 0x0600743D RID: 29757 RVA: 0x002BE0F3 File Offset: 0x002BC2F3
		public override string GetText(GameObject Actor)
		{
			return "Attacked " + (this.Ally ?? "my ally") + ".";
		}

		// Token: 0x04003586 RID: 13702
		public string Ally;
	}
}
