using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000047 RID: 71
	[InputControlLayout(isGenericTypeOfDevice = true)]
	public class Sensor : InputDevice
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0002E10C File Offset: 0x0002C30C
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x0002E144 File Offset: 0x0002C344
		public float samplingFrequency
		{
			get
			{
				QuerySamplingFrequencyCommand querySamplingFrequencyCommand = QuerySamplingFrequencyCommand.Create();
				if (base.ExecuteCommand<QuerySamplingFrequencyCommand>(ref querySamplingFrequencyCommand) >= 0L)
				{
					return querySamplingFrequencyCommand.frequency;
				}
				throw new NotSupportedException(string.Format("Device '{0}' does not support querying sampling frequency", this));
			}
			set
			{
				SetSamplingFrequencyCommand setSamplingFrequencyCommand = SetSamplingFrequencyCommand.Create(value);
				base.ExecuteCommand<SetSamplingFrequencyCommand>(ref setSamplingFrequencyCommand);
			}
		}
	}
}
