using System;

namespace ConsoleLib.Console
{
	// Token: 0x02001538 RID: 5432
	[Flags]
	public enum Keys
	{
		// Token: 0x04005430 RID: 21552
		Modifiers = -65536,
		// Token: 0x04005431 RID: 21553
		None = 0,
		// Token: 0x04005432 RID: 21554
		LButton = 1,
		// Token: 0x04005433 RID: 21555
		RButton = 2,
		// Token: 0x04005434 RID: 21556
		Cancel = 3,
		// Token: 0x04005435 RID: 21557
		MButton = 4,
		// Token: 0x04005436 RID: 21558
		XButton1 = 5,
		// Token: 0x04005437 RID: 21559
		XButton2 = 6,
		// Token: 0x04005438 RID: 21560
		Back = 8,
		// Token: 0x04005439 RID: 21561
		Tab = 9,
		// Token: 0x0400543A RID: 21562
		LineFeed = 10,
		// Token: 0x0400543B RID: 21563
		Clear = 12,
		// Token: 0x0400543C RID: 21564
		Enter = 13,
		// Token: 0x0400543D RID: 21565
		Return = 13,
		// Token: 0x0400543E RID: 21566
		ShiftKey = 16,
		// Token: 0x0400543F RID: 21567
		ControlKey = 17,
		// Token: 0x04005440 RID: 21568
		Menu = 18,
		// Token: 0x04005441 RID: 21569
		Pause = 19,
		// Token: 0x04005442 RID: 21570
		CapsLock = 20,
		// Token: 0x04005443 RID: 21571
		Capital = 20,
		// Token: 0x04005444 RID: 21572
		KanaMode = 21,
		// Token: 0x04005445 RID: 21573
		HanguelMode = 21,
		// Token: 0x04005446 RID: 21574
		HangulMode = 21,
		// Token: 0x04005447 RID: 21575
		JunjaMode = 23,
		// Token: 0x04005448 RID: 21576
		FinalMode = 24,
		// Token: 0x04005449 RID: 21577
		KanjiMode = 25,
		// Token: 0x0400544A RID: 21578
		HanjaMode = 25,
		// Token: 0x0400544B RID: 21579
		Escape = 27,
		// Token: 0x0400544C RID: 21580
		IMEConvert = 28,
		// Token: 0x0400544D RID: 21581
		IMENonconvert = 29,
		// Token: 0x0400544E RID: 21582
		IMEAceept = 30,
		// Token: 0x0400544F RID: 21583
		IMEAccept = 30,
		// Token: 0x04005450 RID: 21584
		IMEModeChange = 31,
		// Token: 0x04005451 RID: 21585
		Space = 32,
		// Token: 0x04005452 RID: 21586
		Prior = 33,
		// Token: 0x04005453 RID: 21587
		PageUp = 33,
		// Token: 0x04005454 RID: 21588
		Next = 34,
		// Token: 0x04005455 RID: 21589
		PageDown = 34,
		// Token: 0x04005456 RID: 21590
		End = 35,
		// Token: 0x04005457 RID: 21591
		Home = 36,
		// Token: 0x04005458 RID: 21592
		Left = 37,
		// Token: 0x04005459 RID: 21593
		Up = 38,
		// Token: 0x0400545A RID: 21594
		Right = 39,
		// Token: 0x0400545B RID: 21595
		Down = 40,
		// Token: 0x0400545C RID: 21596
		Select = 41,
		// Token: 0x0400545D RID: 21597
		Print = 42,
		// Token: 0x0400545E RID: 21598
		Execute = 43,
		// Token: 0x0400545F RID: 21599
		PrintScreen = 44,
		// Token: 0x04005460 RID: 21600
		Snapshot = 44,
		// Token: 0x04005461 RID: 21601
		Insert = 45,
		// Token: 0x04005462 RID: 21602
		Delete = 46,
		// Token: 0x04005463 RID: 21603
		Help = 47,
		// Token: 0x04005464 RID: 21604
		D0 = 48,
		// Token: 0x04005465 RID: 21605
		D1 = 49,
		// Token: 0x04005466 RID: 21606
		D2 = 50,
		// Token: 0x04005467 RID: 21607
		D3 = 51,
		// Token: 0x04005468 RID: 21608
		D4 = 52,
		// Token: 0x04005469 RID: 21609
		D5 = 53,
		// Token: 0x0400546A RID: 21610
		D6 = 54,
		// Token: 0x0400546B RID: 21611
		D7 = 55,
		// Token: 0x0400546C RID: 21612
		D8 = 56,
		// Token: 0x0400546D RID: 21613
		D9 = 57,
		// Token: 0x0400546E RID: 21614
		A = 65,
		// Token: 0x0400546F RID: 21615
		B = 66,
		// Token: 0x04005470 RID: 21616
		C = 67,
		// Token: 0x04005471 RID: 21617
		D = 68,
		// Token: 0x04005472 RID: 21618
		E = 69,
		// Token: 0x04005473 RID: 21619
		F = 70,
		// Token: 0x04005474 RID: 21620
		G = 71,
		// Token: 0x04005475 RID: 21621
		H = 72,
		// Token: 0x04005476 RID: 21622
		I = 73,
		// Token: 0x04005477 RID: 21623
		J = 74,
		// Token: 0x04005478 RID: 21624
		K = 75,
		// Token: 0x04005479 RID: 21625
		L = 76,
		// Token: 0x0400547A RID: 21626
		M = 77,
		// Token: 0x0400547B RID: 21627
		N = 78,
		// Token: 0x0400547C RID: 21628
		O = 79,
		// Token: 0x0400547D RID: 21629
		P = 80,
		// Token: 0x0400547E RID: 21630
		Q = 81,
		// Token: 0x0400547F RID: 21631
		R = 82,
		// Token: 0x04005480 RID: 21632
		S = 83,
		// Token: 0x04005481 RID: 21633
		T = 84,
		// Token: 0x04005482 RID: 21634
		U = 85,
		// Token: 0x04005483 RID: 21635
		V = 86,
		// Token: 0x04005484 RID: 21636
		W = 87,
		// Token: 0x04005485 RID: 21637
		X = 88,
		// Token: 0x04005486 RID: 21638
		Y = 89,
		// Token: 0x04005487 RID: 21639
		Z = 90,
		// Token: 0x04005488 RID: 21640
		LWin = 91,
		// Token: 0x04005489 RID: 21641
		RWin = 92,
		// Token: 0x0400548A RID: 21642
		Apps = 93,
		// Token: 0x0400548B RID: 21643
		Sleep = 95,
		// Token: 0x0400548C RID: 21644
		NumPad0 = 96,
		// Token: 0x0400548D RID: 21645
		NumPad1 = 97,
		// Token: 0x0400548E RID: 21646
		NumPad2 = 98,
		// Token: 0x0400548F RID: 21647
		NumPad3 = 99,
		// Token: 0x04005490 RID: 21648
		NumPad4 = 100,
		// Token: 0x04005491 RID: 21649
		NumPad5 = 101,
		// Token: 0x04005492 RID: 21650
		NumPad6 = 102,
		// Token: 0x04005493 RID: 21651
		NumPad7 = 103,
		// Token: 0x04005494 RID: 21652
		NumPad8 = 104,
		// Token: 0x04005495 RID: 21653
		NumPad9 = 105,
		// Token: 0x04005496 RID: 21654
		Multiply = 106,
		// Token: 0x04005497 RID: 21655
		Add = 107,
		// Token: 0x04005498 RID: 21656
		Separator = 108,
		// Token: 0x04005499 RID: 21657
		Subtract = 109,
		// Token: 0x0400549A RID: 21658
		Decimal = 110,
		// Token: 0x0400549B RID: 21659
		Divide = 111,
		// Token: 0x0400549C RID: 21660
		F1 = 112,
		// Token: 0x0400549D RID: 21661
		F2 = 113,
		// Token: 0x0400549E RID: 21662
		F3 = 114,
		// Token: 0x0400549F RID: 21663
		F4 = 115,
		// Token: 0x040054A0 RID: 21664
		F5 = 116,
		// Token: 0x040054A1 RID: 21665
		F6 = 117,
		// Token: 0x040054A2 RID: 21666
		F7 = 118,
		// Token: 0x040054A3 RID: 21667
		F8 = 119,
		// Token: 0x040054A4 RID: 21668
		F9 = 120,
		// Token: 0x040054A5 RID: 21669
		F10 = 121,
		// Token: 0x040054A6 RID: 21670
		F11 = 122,
		// Token: 0x040054A7 RID: 21671
		F12 = 123,
		// Token: 0x040054A8 RID: 21672
		F13 = 124,
		// Token: 0x040054A9 RID: 21673
		F14 = 125,
		// Token: 0x040054AA RID: 21674
		F15 = 126,
		// Token: 0x040054AB RID: 21675
		F16 = 127,
		// Token: 0x040054AC RID: 21676
		F17 = 128,
		// Token: 0x040054AD RID: 21677
		F18 = 129,
		// Token: 0x040054AE RID: 21678
		F19 = 130,
		// Token: 0x040054AF RID: 21679
		F20 = 131,
		// Token: 0x040054B0 RID: 21680
		F21 = 132,
		// Token: 0x040054B1 RID: 21681
		F22 = 133,
		// Token: 0x040054B2 RID: 21682
		F23 = 134,
		// Token: 0x040054B3 RID: 21683
		F24 = 135,
		// Token: 0x040054B4 RID: 21684
		NumLock = 144,
		// Token: 0x040054B5 RID: 21685
		Scroll = 145,
		// Token: 0x040054B6 RID: 21686
		LShiftKey = 160,
		// Token: 0x040054B7 RID: 21687
		RShiftKey = 161,
		// Token: 0x040054B8 RID: 21688
		LControlKey = 162,
		// Token: 0x040054B9 RID: 21689
		RControlKey = 163,
		// Token: 0x040054BA RID: 21690
		LMenu = 164,
		// Token: 0x040054BB RID: 21691
		RMenu = 165,
		// Token: 0x040054BC RID: 21692
		BrowserBack = 166,
		// Token: 0x040054BD RID: 21693
		BrowserForward = 167,
		// Token: 0x040054BE RID: 21694
		BrowserRefresh = 168,
		// Token: 0x040054BF RID: 21695
		BrowserStop = 169,
		// Token: 0x040054C0 RID: 21696
		BrowserSearch = 170,
		// Token: 0x040054C1 RID: 21697
		BrowserFavorites = 171,
		// Token: 0x040054C2 RID: 21698
		BrowserHome = 172,
		// Token: 0x040054C3 RID: 21699
		VolumeMute = 173,
		// Token: 0x040054C4 RID: 21700
		VolumeDown = 174,
		// Token: 0x040054C5 RID: 21701
		VolumeUp = 175,
		// Token: 0x040054C6 RID: 21702
		MediaNextTrack = 176,
		// Token: 0x040054C7 RID: 21703
		MediaPreviousTrack = 177,
		// Token: 0x040054C8 RID: 21704
		MediaStop = 178,
		// Token: 0x040054C9 RID: 21705
		MediaPlayPause = 179,
		// Token: 0x040054CA RID: 21706
		LaunchMail = 180,
		// Token: 0x040054CB RID: 21707
		SelectMedia = 181,
		// Token: 0x040054CC RID: 21708
		LaunchApplication1 = 182,
		// Token: 0x040054CD RID: 21709
		LaunchApplication2 = 183,
		// Token: 0x040054CE RID: 21710
		Oem1 = 186,
		// Token: 0x040054CF RID: 21711
		OemSemicolon = 186,
		// Token: 0x040054D0 RID: 21712
		Oemplus = 187,
		// Token: 0x040054D1 RID: 21713
		Oemcomma = 188,
		// Token: 0x040054D2 RID: 21714
		OemMinus = 189,
		// Token: 0x040054D3 RID: 21715
		OemPeriod = 190,
		// Token: 0x040054D4 RID: 21716
		OemQuestion = 191,
		// Token: 0x040054D5 RID: 21717
		Oem2 = 191,
		// Token: 0x040054D6 RID: 21718
		Oemtilde = 192,
		// Token: 0x040054D7 RID: 21719
		Oem3 = 192,
		// Token: 0x040054D8 RID: 21720
		Oem4 = 219,
		// Token: 0x040054D9 RID: 21721
		OemOpenBrackets = 219,
		// Token: 0x040054DA RID: 21722
		OemPipe = 220,
		// Token: 0x040054DB RID: 21723
		Oem5 = 220,
		// Token: 0x040054DC RID: 21724
		Oem6 = 221,
		// Token: 0x040054DD RID: 21725
		OemCloseBrackets = 221,
		// Token: 0x040054DE RID: 21726
		Oem7 = 222,
		// Token: 0x040054DF RID: 21727
		OemQuotes = 222,
		// Token: 0x040054E0 RID: 21728
		Oem8 = 223,
		// Token: 0x040054E1 RID: 21729
		Oem102 = 226,
		// Token: 0x040054E2 RID: 21730
		OemBackslash = 226,
		// Token: 0x040054E3 RID: 21731
		ProcessKey = 229,
		// Token: 0x040054E4 RID: 21732
		Packet = 231,
		// Token: 0x040054E5 RID: 21733
		Attn = 246,
		// Token: 0x040054E6 RID: 21734
		Crsel = 247,
		// Token: 0x040054E7 RID: 21735
		Exsel = 248,
		// Token: 0x040054E8 RID: 21736
		EraseEof = 249,
		// Token: 0x040054E9 RID: 21737
		Play = 250,
		// Token: 0x040054EA RID: 21738
		Zoom = 251,
		// Token: 0x040054EB RID: 21739
		MouseEvent = 252,
		// Token: 0x040054EC RID: 21740
		Pa1 = 253,
		// Token: 0x040054ED RID: 21741
		OemClear = 254,
		// Token: 0x040054EE RID: 21742
		KeyCode = 65535,
		// Token: 0x040054EF RID: 21743
		Shift = 65536,
		// Token: 0x040054F0 RID: 21744
		Control = 131072,
		// Token: 0x040054F1 RID: 21745
		Alt = 262144
	}
}
