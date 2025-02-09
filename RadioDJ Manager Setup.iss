; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "RadioDJ Manager"
#define MyAppVersion "1.0"
#define MyAppExeName "RadioDJManager.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{6929023F-A6FA-4577-B078-81DA03AD7A32}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
; The [Icons] "quicklaunchicon" entry uses {userappdata} but its [Tasks] entry has a proper IsAdminInstallMode Check.
UsedUserAreasWarning=no
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=C:\Users\Darkstar\Desktop
OutputBaseFilename=RadioDJ Setup
SetupIconFile=C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\Icons\Kaba.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 6.1; Check: not IsAdminInstallMode

[Files]
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\RadioDJManager.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\BouncyCastle.Crypto.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\ControlzEx.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\ControlzEx.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Dragablz.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Dragablz.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Dragablz.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\EntityFramework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\EntityFramework.SqlServer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\EntityFramework.SqlServer.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\EntityFramework.xml"; DestDir: "{app}"; Flags: ignoreversion
;Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\FluentScheduler.dll"; DestDir: "{app}"; Flags: ignoreversion
;Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\FluentScheduler.pdb"; DestDir: "{app}"; Flags: ignoreversion
;Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\FluentScheduler.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Google.Protobuf.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Google.Protobuf.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Hardcodet.Wpf.TaskbarNotification.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Hardcodet.Wpf.TaskbarNotification.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Hardcodet.Wpf.TaskbarNotification.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\K4os.Compression.LZ4.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\K4os.Compression.LZ4.Streams.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\K4os.Compression.LZ4.Streams.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\K4os.Compression.LZ4.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\K4os.Hash.xxHash.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\K4os.Hash.xxHash.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\log4net.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\log4net.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MahApps.Metro.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MahApps.Metro.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MahApps.Metro.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MaterialDesignColors.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MaterialDesignColors.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MaterialDesignThemes.MahApps.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MaterialDesignThemes.Wpf.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MaterialDesignThemes.Wpf.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MaterialDesignThemes.Wpf.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Messaging.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Messaging.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Microsoft.CSharp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MySql.Data.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MySql.Data.Entity.EF5.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MySql.Data.Entity.EF6.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MySql.Data.Entity.EF6.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MySql.Data.EntityFramework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MySql.Data.EntityFramework.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\MySql.Data.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\PresentationCore.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\PresentationFramework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\RadioDJManager.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\RadioDJManager.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\RadioDJManager.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Renci.SshNet.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Renci.SshNet.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.Buffers.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.Buffers.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.ComponentModel.DataAnnotations.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.Configuration.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.Data.DataSetExtensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.Data.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.Memory.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.Memory.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.Runtime.CompilerServices.Unsafe.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.Runtime.CompilerServices.Unsafe.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.Windows.Interactivity.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.Xaml.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.Xml.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\System.Xml.Linq.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Ubiety.Dns.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\WindowsBase.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Darkstar\Documents\Visual Studio 2017\Projects\RadioDJManager\RadioDJManager\bin\Debug\Zstandard.Net.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

