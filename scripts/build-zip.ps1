$ErrorActionPreference = 'Stop'

Write-Host "Publishing single-file lite ZIP build..."
dotnet publish .\MicVolumeGuard.App\MicVolumeGuard.App.csproj -c Release -r win-x64 --self-contained false /p:PublishSingleFile=true /p:DebugType=None /p:DebugSymbols=false /p:PublishTrimmed=false -o .\publish\zip-lite

Write-Host "Publishing single-file full ZIP build..."
dotnet publish .\MicVolumeGuard.App\MicVolumeGuard.App.csproj -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:EnableCompressionInSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=false /p:DebugType=None /p:DebugSymbols=false /p:PublishTrimmed=false -o .\publish\zip-full

$liteExe = ".\publish\zip-lite\MicVolumeGuard.exe"
$fullExe = ".\publish\zip-full\MicVolumeGuard.exe"

if (-not (Test-Path $liteExe)) {
    throw "Lite EXE not found at $liteExe"
}

if (-not (Test-Path $fullExe)) {
    throw "Full EXE not found at $fullExe"
}

$liteStaging = ".\publish\zip-lite-single"
$fullStaging = ".\publish\zip-full-single"

if (Test-Path $liteStaging) {
    Remove-Item $liteStaging -Recurse -Force
}
if (Test-Path $fullStaging) {
    Remove-Item $fullStaging -Recurse -Force
}

New-Item -ItemType Directory -Path $liteStaging | Out-Null
New-Item -ItemType Directory -Path $fullStaging | Out-Null

Copy-Item $liteExe -Destination (Join-Path $liteStaging "MicVolumeGuard.exe") -Force
Copy-Item $fullExe -Destination (Join-Path $fullStaging "MicVolumeGuard.exe") -Force

$liteZip = ".\MicVolumeGuard-Windows-lite-win-x64.zip"
$fullZip = ".\MicVolumeGuard-Windows-win-x64.zip"

if (Test-Path $liteZip) {
    Remove-Item $liteZip -Force
}
if (Test-Path $fullZip) {
    Remove-Item $fullZip -Force
}

Write-Host "Creating lite ZIP (single file)..."
Compress-Archive -Path (Join-Path $liteStaging "*") -DestinationPath $liteZip -Force

Write-Host "Creating full ZIP (single file)..."
Compress-Archive -Path (Join-Path $fullStaging "*") -DestinationPath $fullZip -Force

Write-Host "Done."
Get-ChildItem $liteZip, $fullZip | Select-Object Name, @{Name='SizeMB';Expression={[math]::Round($_.Length / 1MB, 2)}} | Format-Table -AutoSize
