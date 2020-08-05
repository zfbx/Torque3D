//-----------------------------------------------------------------------------
// Module creation functions.
//-----------------------------------------------------------------------------

function inputTest::create( %this )
{
}

function inputTest::destroy( %this )
{
   
}

function inputTest::initClient( %this )
{
   %this.queueExec("/scripts/customProfiles.cs");
   %this.queueExec("/scripts/inputMonitor.cs");
   %this.queueExec("/scripts/gui/inputMonitor.gui");
   %this.queueExec("/scripts/joystickSettings.cs");
   %this.queueExec("/scripts/gui/joystickSettings.gui");
   %this.queueExec("/scripts/menuButtons.cs");
}

function onSDLDeviceConnected(%sdlIndex, %deviceName, %deviceType)
{
   echo("onSDLDeviceConnected(" @ %sdlIndex @ ", \"" @ %deviceName @ "\", \"" @ %deviceType @ "\") - Called");

   // Note: This is called before the device is automatically processed to allow
   // overrides, so refreshing the gui needs to happen after the device has been opened
   if (JoystickSettingsDlg.isAwake())
      JoystickSettingsDlg.schedule(250, "updateDevices");
   if (InputMonitorDlg.isAwake())
      InputMonitorDlg.schedule(250, "updateDevicesLine");
}

function onSDLDeviceDisconnected(%sdlIndex)
{
   echo("onSDLDeviceDisconnected(" @ %sdlIndex @ ") - Called");

   if (JoystickSettingsDlg.isAwake())
      JoystickSettingsDlg.schedule(250, "updateDevices");
   if (InputMonitorDlg.isAwake())
      InputMonitorDlg.schedule(250, "updateDevicesLine");
}

function listAllGCMappings()
{  // Lists all game controller device mappings that are currently installed
   %numMappings = SDLInputManager::GameControllerNumMappings();
   for (%i = 0; %i < %numMappings; %i++)
      echo(SDLInputManager::GameControllerMappingForIndex(%i));
}
