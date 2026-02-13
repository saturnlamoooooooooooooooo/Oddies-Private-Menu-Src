namespace OddieMenuTemp
{
	internal class ButtonsHandler
	{
        public static int Page;

        public static void Next_Page()
		{
			Menu.Clear();
			Page++;
			if (Page == 1)
			{
				Page1();
				return;
			}
			if (Page == 2)
			{
				Page2();
				return;
			}
			if (Page == 3)
			{
				Page3();
				return;
			}
			if (Page == 4)
			{
				Page4();
				return;
			}
			Page = 0;
			Next_Page();
		}

		public static void Page1()
		{
			Menu.newOption("Page 1: Player", "header");
			Menu.newOption("Platforms", "toggle");
			Menu.newOption("Noclip", "toggle");
			Menu.newOption("U R Timmeh", "toggle");
			Menu.newOption("Fly", "toggle");
			Menu.newOption("Esp", "toggle");
			Menu.newOption("No Tag-Freeze", "toggle");
			Menu.newOption("No Stalker Spawn", "toggle");
			Menu.newOption("LongArms", "toggle");
			Menu.newOption("FullBright", "toggle");
			Menu.newOption("Gravity Flip", "toggle");
			Menu.newOption("Speedboost", "toggle");
			Menu.newOption("Racer's Speedboost", "toggle");
			Menu.newOption("No Death", "toggle");
			Menu.newOption("Next Page", "next");
		}

		public static void Page2()
		{
			Menu.newOption("Page 2: Photon", "header");
			Menu.newOption("Disconnect", "button");
			Menu.newOption("No Name", "broke");
			Menu.newOption("Set Master", "button");
			Menu.newOption("Crash", "button");
			Menu.newOption("Next Page", "next");
		}

		public static void Page3()
		{
			Menu.newOption("Page 3: Guns", "header");
			Menu.newOption("Crash Gun", "toggle");
			Menu.newOption("Destroy Gun", "toggle");
			Menu.newOption("TP Gun", "toggle");
			Menu.newOption("KILL Gun", "toggle");
			Menu.newOption("Next Page", "next");
		}

		public static void Page4()
		{
			Menu.newOption("Page 4: Debug", "header");
			Menu.newOption("List Hierarcy", "button");
			Menu.newOption("Next Page", "next");
		}
	}
}
