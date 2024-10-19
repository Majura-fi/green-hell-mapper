. ".\utils.ps1"

# App and mod info.
$modName = "Mapper"
$appId = 815370
$appName = "Green Hell"

# ----------

$gamePath = Get-SteamGamePath -AppId $appId -AppName $appName

# We need a path to the game.
if ($null -eq $gamePath) {
    Write-Warning "No game path found."
    return
}

$filesToInclude = $(".\Info.json", ".\bin\Debug\net48\Mapper.dll")
$releaseInfo = New-ModRelease -ModName $modName -FilesToInclude $filesToInclude -ReleasePath ".\Releases_Debug"

$copyDestination = Join-Path -Path $gamePath -ChildPath "Mods\$modName"
Write-Host "Path to mod: $copyDestination"

if (-not (Test-Path -Path $copyDestination)) {
    Write-Host "Creating new directory: $copyDestination"
    New-Item -ItemType Directory -Path $copyDestination -Force
}

Write-Host "Copying released mod to game mods: $copyDestination"
Copy-Item -Path $releaseInfo.Path -Destination $copyDestination -Recurse -Force

Write-Host "Debug release $($releaseInfo.Version) done!"
