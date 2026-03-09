#define MyAppName "Mic Volume Guard"
#define MyAppVersion "1.0.1"
#define MyAppPublisher "Rajan Jha"
#define MyAppURL "https://github.com/rajanjha13/windows-mic-auto-gain-fix"
#define MyAppExeName "MicVolumeGuard.App.exe"
#define BuildOutputDir "..\publish\selfcontained-optimized"

[Setup]
AppId={{1D3DF06A-C325-4E57-B78B-DA4E16B6C9A9}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={localappdata}\Programs\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableDirPage=no
DisableProgramGroupPage=yes
LicenseFile=..\LICENSE
OutputDir=..
OutputBaseFilename=MicVolumeGuard-Setup-win-x64
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
ArchitecturesInstallIn64BitMode=x64compatible
PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
UninstallDisplayIcon={app}\{#MyAppExeName}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "startup"; Description: "Start Mic Volume Guard when I sign in"; GroupDescription: "Additional options:"; Flags: unchecked

[Files]
Source: "{#BuildOutputDir}\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autoprograms}\Uninstall {#MyAppName}"; Filename: "{uninstallexe}"

[Registry]
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "MicVolumeGuard"; ValueData: """{app}\{#MyAppExeName}"""; Flags: uninsdeletevalue; Tasks: startup

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Launch {#MyAppName}"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
Type: filesandordirs; Name: "{localappdata}\MicVolumeGuard"

[Code]
function InitializeSetup(): Boolean;
begin
  Result := True;
end;
