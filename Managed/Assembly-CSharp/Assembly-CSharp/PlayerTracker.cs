using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007B RID: 123
public class PlayerTracker : MonoBehaviour
{
	// Token: 0x06000492 RID: 1170 RVA: 0x0001DFDC File Offset: 0x0001C1DC
	public void setActiveDirection(string dir)
	{
		if (this.activeJoystickDirection != null)
		{
			this.activeJoystickDirection.SetActive(false);
			this.activeJoystickDirection = null;
		}
		if (dir == null || !this.joystickDirectionIndexes.ContainsKey(dir))
		{
			return;
		}
		this.activeJoystickDirection = this.joystickDirectionIndicators[this.joystickDirectionIndexes[dir]];
		if (!this.activeJoystickDirection.activeSelf)
		{
			this.activeJoystickDirection.SetActive(true);
		}
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0001E04E File Offset: 0x0001C24E
	private void Start()
	{
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0001E050 File Offset: 0x0001C250
	private void Update()
	{
	}

	// Token: 0x0400037C RID: 892
	public GameObject[] joystickDirectionIndicators;

	// Token: 0x0400037D RID: 893
	private Dictionary<string, int> joystickDirectionIndexes = new Dictionary<string, int>
	{
		{ "N", 0 },
		{ "NE", 1 },
		{ "E", 2 },
		{ "SE", 3 },
		{ "S", 4 },
		{ "SW", 5 },
		{ "W", 6 },
		{ "NW", 7 }
	};

	// Token: 0x0400037E RID: 894
	public GameObject activeJoystickDirection;
}
