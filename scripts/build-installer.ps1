$ErrorActionPreference = 'Stop'

Write-Host "Publishing self-contained executable..."
dotnet publish .\MicVolumeGuard.App\MicVolumeGuard.App.csproj -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:EnableCompressionInSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=false /p:DebugType=None /p:DebugSymbols=false /p:PublishTrimmed=false -o .\publish\selfcontained-optimized

$possibleIscc = @(
    "$Env:LOCALAPPDATA\Programs\Inno Setup 6\ISCC.exe",
    "$Env:ProgramFiles(x86)\Inno Setup 6\ISCC.exe",
    "$Env:ProgramFiles\Inno Setup 6\ISCC.exe"
)

$isccPath = $possibleIscc | Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not $isccPath) {
    throw "ISCC.exe not found. Install Inno Setup 6 first."
}

Write-Host "Building installer with Inno Setup..."
& $isccPath .\installer\MicVolumeGuard.iss

Write-Host "Installer created: .\MicVolumeGuard-Setup-win-x64.exe"
