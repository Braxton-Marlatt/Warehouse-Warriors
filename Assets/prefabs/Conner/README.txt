//Conner Puopolo Prefab README

What is it?
	A customizable Main Menu inspired by the Costco aesthetic.

How to Use It (and integrate it into your code)
To use this prefab:

	Download the prefab from the asset store

	Drag and drop it into your Prefabs folder in Unity

		If the Prefabs folder doesn�t exist: go to Assets > Create > Folder and name it Prefabs

	Create a new scene (or use an existing one) for your main menu

	Drag the Main Menu prefab into the Hierarchy

	The menu should now appear in your scene

	Edit the title and buttons to fit your game ("Warehouse Warriors" recommended!)

Contents
This prefab includes:

	Costco-style Title - WAREHOUSE WARRIORS (UI Image)

	3 Buttons (UI Button Objects):

		Start � Loads the Game scene

		Help � Opens the Help Scene

		Quit � Exits the application

	Help Scene
		Includes a Close Help button

		Includes  visuals and gameplay instructions in a fun and readable layout

	MainMenu Script:

		Loads the next scene

		Opens/closes Help Menu

MenuManager Script:

	Manages toggling between menus

Troubleshooting Tips

	Start Button not working?

		Check if your intended game scene is listed in Build Settings and matches the value in the Main Menu script

			File > Build Settings > Add Open Scenes

	Help Button not working?

		Ensure the correct Help Menu is assigned in the Inspector under the Main Menu script

		Help Button must call changeScene.loadHelpMenu() in its OnClick() function

	Quit Button not working?
		Make sure your quit button is calling the correct method in your OnClick() script:

		//Start Quit Function
			public void quitGame()
		{
		#if UNITY_EDITOR
			EditorApplication.ExitPlaymode();
		#else
			Application.Quit();
		#endif
		}
		//End Quit Function
		
This prefab brings warehouse-style charm to your game's intro experience, blending humor, retro menus, and Costco-inspired UI for maximum player engagement.
