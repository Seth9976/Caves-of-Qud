using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;

namespace XRL.World
{
	// Token: 0x0200073E RID: 1854
	[GameEvent(Cache = MinEvent.Cache.Singleton)]
	public class GetDebugInternalsEvent : SingletonEvent<GetDebugInternalsEvent>
	{
		// Token: 0x06004CB1 RID: 19633 RVA: 0x0017A97B File Offset: 0x00178B7B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x0017A984 File Offset: 0x00178B84
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Entries.Clear();
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x0017A9A0 File Offset: 0x00178BA0
		public static string GetFor(GameObject Object)
		{
			SingletonEvent<GetDebugInternalsEvent>.Instance.Object = Object;
			SingletonEvent<GetDebugInternalsEvent>.Instance.Entries.Clear();
			if (Object.HandleEvent(SingletonEvent<GetDebugInternalsEvent>.Instance) && Object.HasRegisteredEvent("GetDebugInternals"))
			{
				Object.FireEvent(Event.New("GetDebugInternals", "Object", Object, "Event", SingletonEvent<GetDebugInternalsEvent>.Instance));
			}
			return GetDebugInternalsEvent.BuildSummary(SingletonEvent<GetDebugInternalsEvent>.Instance.Entries);
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x0017AA14 File Offset: 0x00178C14
		public void AddEntry(string Source, string Name, string Value)
		{
			List<string> list;
			if (!this.Entries.TryGetValue(Source, out list))
			{
				list = new List<string>();
				this.Entries[Source] = list;
			}
			if (Value == null)
			{
				Value = "NULL";
			}
			else if (ColorUtility.HasFormatting(Value))
			{
				Value = ColorUtility.EscapeFormatting(Value);
			}
			if (Value.IndexOf('\n') != -1)
			{
				list.Add(Name + ":\n" + Value);
				return;
			}
			list.Add(Name + ": " + Value);
		}

		// Token: 0x06004CB5 RID: 19637 RVA: 0x0017AA90 File Offset: 0x00178C90
		public void AddEntry(string Source, string Name, int Value)
		{
			this.AddEntry(Source, Name, Value.ToString());
		}

		// Token: 0x06004CB6 RID: 19638 RVA: 0x0017AAA4 File Offset: 0x00178CA4
		public void AddEntry(string Source, string Name, int? Value)
		{
			this.AddEntry(Source, Name, ((Value != null) ? Value.GetValueOrDefault().ToString() : null) ?? "null");
		}

		// Token: 0x06004CB7 RID: 19639 RVA: 0x0017AADD File Offset: 0x00178CDD
		public void AddEntry(string Source, string Name, float Value)
		{
			this.AddEntry(Source, Name, Value.ToString());
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x0017AAF0 File Offset: 0x00178CF0
		public void AddEntry(string Source, string Name, float? Value)
		{
			this.AddEntry(Source, Name, ((Value != null) ? Value.GetValueOrDefault().ToString() : null) ?? "null");
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x0017AB29 File Offset: 0x00178D29
		public void AddEntry(string Source, string Name, double Value)
		{
			this.AddEntry(Source, Name, Value.ToString());
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x0017AB3C File Offset: 0x00178D3C
		public void AddEntry(string Source, string Name, double? Value)
		{
			this.AddEntry(Source, Name, ((Value != null) ? Value.GetValueOrDefault().ToString() : null) ?? "null");
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x0017AB75 File Offset: 0x00178D75
		public void AddEntry(string Source, string Name, bool Value)
		{
			this.AddEntry(Source, Name, Value.ToString());
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x0017AB88 File Offset: 0x00178D88
		public void AddEntry(string Source, string Name, bool? Value)
		{
			this.AddEntry(Source, Name, ((Value != null) ? Value.GetValueOrDefault().ToString() : null) ?? "null");
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x0017ABC1 File Offset: 0x00178DC1
		public void AddEntry(string Source, string Name, GameObject Value)
		{
			this.AddEntry(Source, Name, ((Value != null) ? Value.DebugName : null) ?? "null");
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x0017ABE0 File Offset: 0x00178DE0
		public void AddEntry(IPart Part, string Name, string Value)
		{
			this.AddEntry(Part.Name, Name, Value);
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x0017ABF0 File Offset: 0x00178DF0
		public void AddEntry(IPart Part, string Name, int Value)
		{
			this.AddEntry(Part, Name, Value.ToString());
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x0017AC04 File Offset: 0x00178E04
		public void AddEntry(IPart Part, string Name, int? Value)
		{
			this.AddEntry(Part, Name, ((Value != null) ? Value.GetValueOrDefault().ToString() : null) ?? "null");
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x0017AC3D File Offset: 0x00178E3D
		public void AddEntry(IPart Part, string Name, float Value)
		{
			this.AddEntry(Part, Name, Value.ToString());
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x0017AC50 File Offset: 0x00178E50
		public void AddEntry(IPart Part, string Name, float? Value)
		{
			this.AddEntry(Part, Name, ((Value != null) ? Value.GetValueOrDefault().ToString() : null) ?? "null");
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x0017AC89 File Offset: 0x00178E89
		public void AddEntry(IPart Part, string Name, double Value)
		{
			this.AddEntry(Part, Name, Value.ToString());
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x0017AC9C File Offset: 0x00178E9C
		public void AddEntry(IPart Part, string Name, double? Value)
		{
			this.AddEntry(Part, Name, ((Value != null) ? Value.GetValueOrDefault().ToString() : null) ?? "null");
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x0017ACD5 File Offset: 0x00178ED5
		public void AddEntry(IPart Part, string Name, bool Value)
		{
			this.AddEntry(Part, Name, Value.ToString());
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x0017ACE8 File Offset: 0x00178EE8
		public void AddEntry(IPart Part, string Name, bool? Value)
		{
			this.AddEntry(Part, Name, ((Value != null) ? Value.GetValueOrDefault().ToString() : null) ?? "null");
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x0017AD21 File Offset: 0x00178F21
		public void AddEntry(IPart Part, string Name, GameObject Value)
		{
			this.AddEntry(Part, Name, ((Value != null) ? Value.DebugName : null) ?? "null");
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x0017AD40 File Offset: 0x00178F40
		public void AddEntry(Effect FX, string Name, string Value)
		{
			this.AddEntry(FX.ClassName, Name, Value);
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x0017AD50 File Offset: 0x00178F50
		public void AddEntry(Effect FX, string Name, int Value)
		{
			this.AddEntry(FX, Name, Value.ToString());
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x0017AD64 File Offset: 0x00178F64
		public void AddEntry(Effect FX, string Name, int? Value)
		{
			this.AddEntry(FX, Name, ((Value != null) ? Value.GetValueOrDefault().ToString() : null) ?? "null");
		}

		// Token: 0x06004CCB RID: 19659 RVA: 0x0017AD9D File Offset: 0x00178F9D
		public void AddEntry(Effect FX, string Name, float Value)
		{
			this.AddEntry(FX, Name, Value.ToString());
		}

		// Token: 0x06004CCC RID: 19660 RVA: 0x0017ADB0 File Offset: 0x00178FB0
		public void AddEntry(Effect FX, string Name, float? Value)
		{
			this.AddEntry(FX, Name, ((Value != null) ? Value.GetValueOrDefault().ToString() : null) ?? "null");
		}

		// Token: 0x06004CCD RID: 19661 RVA: 0x0017ADE9 File Offset: 0x00178FE9
		public void AddEntry(Effect FX, string Name, double Value)
		{
			this.AddEntry(FX, Name, Value.ToString());
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x0017ADFC File Offset: 0x00178FFC
		public void AddEntry(Effect FX, string Name, double? Value)
		{
			this.AddEntry(FX, Name, ((Value != null) ? Value.GetValueOrDefault().ToString() : null) ?? "null");
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x0017AE35 File Offset: 0x00179035
		public void AddEntry(Effect FX, string Name, bool Value)
		{
			this.AddEntry(FX, Name, Value.ToString());
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x0017AE48 File Offset: 0x00179048
		public void AddEntry(Effect FX, string Name, bool? Value)
		{
			this.AddEntry(FX, Name, ((Value != null) ? Value.GetValueOrDefault().ToString() : null) ?? "null");
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x0017AE81 File Offset: 0x00179081
		public void AddEntry(Effect FX, string Name, GameObject Value)
		{
			this.AddEntry(FX, Name, ((Value != null) ? Value.DebugName : null) ?? "null");
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x0017AEA0 File Offset: 0x001790A0
		private static string BuildSummary(Dictionary<string, List<string>> Entries)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			List<string> list = new List<string>(Entries.Keys);
			list.Sort();
			stringBuilder.Append("{{internals|");
			foreach (string text in list)
			{
				List<string> list2 = Entries[text];
				stringBuilder.Append('Ú').Append(' ').Append(text)
					.Append('\n');
				int i = 0;
				int count = list2.Count;
				while (i < count)
				{
					string text2 = list2[i];
					if (text2.IndexOf('\n') != -1)
					{
						List<string> list3 = new List<string>(text2.Split('\n', StringSplitOptions.None));
						if (list3[1] == "")
						{
							list3.RemoveAt(1);
						}
						if (list3[0] == "")
						{
							list3.RemoveAt(0);
						}
						if (list3[list3.Count - 1] == "")
						{
							list3.RemoveAt(list3.Count - 1);
						}
						stringBuilder.Append((i == count - 1) ? 'À' : 'Ã').Append('Ä').Append('Â')
							.Append(' ')
							.Append(list3[0])
							.Append('\n');
						int j = 1;
						int count2 = list3.Count;
						while (j < count2)
						{
							stringBuilder.Append((i == count - 1) ? 'ÿ' : '³').Append(' ').Append((j == count2 - 1) ? 'À' : 'Ã')
								.Append('Ä')
								.Append('Ä')
								.Append(' ')
								.Append(list3[j])
								.Append('\n');
							j++;
						}
					}
					else
					{
						stringBuilder.Append((i == count - 1) ? 'À' : 'Ã').Append('Ä').Append('Ä')
							.Append(' ')
							.Append(text2)
							.Append('\n');
					}
					i++;
				}
			}
			stringBuilder.Append("}}");
			return stringBuilder.ToString();
		}

		// Token: 0x04002529 RID: 9513
		public GameObject Object;

		// Token: 0x0400252A RID: 9514
		public Dictionary<string, List<string>> Entries = new Dictionary<string, List<string>>();
	}
}
