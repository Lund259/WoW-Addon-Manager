# WoW Addon Manager
This is an application that makes it easy to manage Addons in all versions of World of Warcraft.

I created this application as a fun way to learn about WPF and Caliburn Micro MWWM pattern. I have no idea if this application will be useful to anyone, but as i have found it very useful while using it myself, i am now releasing it for everyone to use and contribute to.

What the application currently does:

- Displays list of all installed addons with Title, Author, WoW version it was developed for, Website and description
  - The list can be sorted by clicking on the Column you want to sort it by.
  - You can also drag and drop the columns around if you don't like the default layout.
  - You can search and filter the list by typing in the search box. The list will update as you type.
- Install Addons by selecting a zip file
  - The application will automatically extract, move and rename all the folders accordingly. 
  - In the future i plan to implement some sort of online search to install addons from. 
- Uninstall Addons by selecting one or multiple addons from the list and clicking the "Uninstall Addons" button.
  - To select more than one addon you can: Drag the mouse, use CTRL + mouseclick or Shift.
- Change your WoW installation folder at any time in the Settings menu. 
  - When you change your installation folder the Addons list will automatically update.
  - I plan to implement the ability to have multiple WoW installations at a time later. 
- The menu is accessible by clicking on the "hamburger button" in the top left corner. 
- The application will display a quick notification if newer releases are available on Github.

If you have any suggestions or find any bugs please submit them here on github. And feel free to contribute if you want.

[Click here to download the latest release](https://github.com/Lund259/WoW-Addon-Manager/releases)
  
Some pictures showcasing the application:

![](https://i.imgur.com/FGPSu1e.png)
![](https://i.imgur.com/BodMa9B.png)
![](https://i.imgur.com/E5dxwHF.png)
