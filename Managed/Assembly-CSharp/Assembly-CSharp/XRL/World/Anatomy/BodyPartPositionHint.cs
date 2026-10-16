using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World.Anatomy
{
	// Token: 0x02000BC4 RID: 3012
	[Serializable]
	public class BodyPartPositionHint
	{
		// Token: 0x06007902 RID: 30978 RVA: 0x002DB419 File Offset: 0x002D9619
		public BodyPartPositionHint()
		{
		}

		// Token: 0x06007903 RID: 30979 RVA: 0x002DB424 File Offset: 0x002D9624
		public BodyPartPositionHint(BodyPart Part)
			: this()
		{
			this.Self = Part.Clone();
			BodyPart parentPart = Part.GetParentPart();
			this.Parent = ((parentPart != null) ? parentPart.Clone() : null);
			BodyPart previousPart = Part.GetPreviousPart();
			this.Previous = ((previousPart != null) ? previousPart.Clone() : null);
			BodyPart nextPart = Part.GetNextPart();
			this.Next = ((nextPart != null) ? nextPart.Clone() : null);
		}

		// Token: 0x06007904 RID: 30980 RVA: 0x002DB48C File Offset: 0x002D968C
		public List<BodyPartPositionSpec> GetRankedSpecs(List<BodyPart> List)
		{
			if (List == null || List.Count == 0)
			{
				return null;
			}
			List<BodyPartPositionSpec> list = new List<BodyPartPositionSpec>();
			foreach (BodyPart bodyPart in List)
			{
				int similarityScore = BodyPartPositionHint.GetSimilarityScore(this.Parent, bodyPart);
				if (similarityScore > 0)
				{
					if (bodyPart.Parts == null || bodyPart.Parts.Count == 0)
					{
						list.Add(new BodyPartPositionSpec(bodyPart, -1, similarityScore));
					}
					else
					{
						int i = 0;
						int count = bodyPart.Parts.Count;
						while (i <= count)
						{
							BodyPart bodyPart2 = ((i > 0) ? bodyPart.Parts[i - 1] : null);
							BodyPart bodyPart3 = ((i < count) ? bodyPart.Parts[i] : null);
							int num = ((bodyPart3 != null) ? bodyPart3.Position : (-1));
							int num2 = 0;
							if (bodyPart2 != null && bodyPart3 != null)
							{
								num2 = (similarityScore + BodyPartPositionHint.GetSimilarityScore(bodyPart2, this.Previous) + BodyPartPositionHint.GetSimilarityScore(bodyPart3, this.Next)) / 3;
							}
							else if (bodyPart2 != null)
							{
								num2 = (similarityScore + BodyPartPositionHint.GetSimilarityScore(bodyPart2, this.Previous)) / 2;
							}
							else if (bodyPart3 != null)
							{
								num2 = (similarityScore + BodyPartPositionHint.GetSimilarityScore(bodyPart3, this.Next)) / 2;
							}
							if (num2 > 0)
							{
								list.Add(new BodyPartPositionSpec(bodyPart, num, num2));
							}
							i++;
						}
					}
				}
			}
			if (list.Count > 1)
			{
				list.Sort((BodyPartPositionSpec a, BodyPartPositionSpec b) => b.Score.CompareTo(a.Score));
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x06007905 RID: 30981 RVA: 0x002DB648 File Offset: 0x002D9848
		public List<BodyPartPositionSpec> GetRankedSpecs(Body Body)
		{
			return this.GetRankedSpecs((Body != null) ? Body.GetParts() : null);
		}

		// Token: 0x06007906 RID: 30982 RVA: 0x002DB65C File Offset: 0x002D985C
		public List<BodyPartPositionSpec> GetRankedSpecs(GameObject Object)
		{
			return this.GetRankedSpecs((Object != null) ? Object.Body : null);
		}

		// Token: 0x06007907 RID: 30983 RVA: 0x002DB670 File Offset: 0x002D9870
		public void GetBestParentAndPosition(List<BodyPart> List, out BodyPart Parent, out int Position)
		{
			List<BodyPartPositionSpec> rankedSpecs = this.GetRankedSpecs(List);
			if (rankedSpecs != null && rankedSpecs.Count > 0)
			{
				Parent = rankedSpecs[0].Parent;
				Position = rankedSpecs[0].Position;
				return;
			}
			Parent = null;
			Position = -1;
		}

		// Token: 0x06007908 RID: 30984 RVA: 0x002DB6B4 File Offset: 0x002D98B4
		public void GetBestParentAndPosition(Body Body, out BodyPart Parent, out int Position)
		{
			this.GetBestParentAndPosition((Body != null) ? Body.GetParts() : null, out Parent, out Position);
		}

		// Token: 0x06007909 RID: 30985 RVA: 0x002DB6CA File Offset: 0x002D98CA
		public void GetBestParentAndPosition(GameObject Object, out BodyPart Parent, out int Position)
		{
			this.GetBestParentAndPosition((Object != null) ? Object.Body : null, out Parent, out Position);
		}

		// Token: 0x0600790A RID: 30986 RVA: 0x002DB6E0 File Offset: 0x002D98E0
		private static int GetSimilarityScore(BodyPart Part1, BodyPart Part2)
		{
			int num = 0;
			if (Part1 != null)
			{
				if (Part2 != null)
				{
					num++;
					List<BodyPart> parts = Part1.Parts;
					int num2 = ((parts != null) ? parts.Count : 0);
					List<BodyPart> parts2 = Part2.Parts;
					if (num2 == ((parts2 != null) ? parts2.Count : 0) - 1)
					{
						num++;
					}
					if (Part1.VariantType == Part2.VariantType)
					{
						num += 20;
					}
					else if (Part1.Type == Part2.Type)
					{
						num += 10;
					}
					if (Part1.Name == Part2.Name)
					{
						num += 10;
					}
					if (Part1.Description == Part2.Description)
					{
						num += 10;
					}
					if (Part1.DefaultBehaviorBlueprint == Part2.DefaultBehaviorBlueprint)
					{
						num += 5;
					}
					num += Math.Max(10 - Math.Abs(Part1.Position - Part2.Position), 0);
					if (Part1.SupportsDependent != Part2.SupportsDependent)
					{
						num -= 5;
					}
					if (Part1.DependsOn != Part2.DependsOn)
					{
						num -= 5;
					}
					if (Part1.RequiresType != Part2.RequiresType)
					{
						num -= 5;
					}
					if (Part1.Category != Part2.Category)
					{
						num -= 2;
					}
					if (Part1.Laterality != Part2.Laterality)
					{
						num--;
					}
					if (Part1.Mobility != Part2.Mobility)
					{
						num--;
					}
					if (Part1.Native != Part2.Native)
					{
						num--;
					}
					if (Part1.Appendage != Part2.Appendage)
					{
						num -= 5;
					}
					if (Part1.Integral != Part2.Integral)
					{
						num -= 5;
					}
					if (Part1.Mortal != Part2.Mortal)
					{
						num -= 5;
					}
					if (Part1.Extrinsic != Part2.Extrinsic)
					{
						num -= 5;
					}
					if (Part1.Dynamic != Part2.Dynamic)
					{
						num -= 5;
					}
					if (Part1.Contact != Part2.Contact)
					{
						num -= 5;
					}
					if (Part1.IgnorePosition != Part2.IgnorePosition)
					{
						num -= 5;
					}
				}
				else
				{
					num -= 10;
				}
			}
			else if (Part2 != null)
			{
				num -= 10;
			}
			else
			{
				num++;
			}
			return num;
		}

		// Token: 0x0400373E RID: 14142
		public BodyPart Self;

		// Token: 0x0400373F RID: 14143
		public BodyPart Parent;

		// Token: 0x04003740 RID: 14144
		public BodyPart Previous;

		// Token: 0x04003741 RID: 14145
		public BodyPart Next;
	}
}
