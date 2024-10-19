# This variable controls if the script stops or continues when an error is encountered.
# We need to stop on errors for try-catch to actually catch errors.
$ErrorActionPreference = "Stop"

function Get-SteamPath {
    <#
    .SYNOPSIS
    Returns a path to the Steam installation. If no path is found (
    installation does not exist in registry) then a null is returned.
    #>

    $regKeySteam = "HKCU:\HKEY_CURRENT_USER\SOFTWARE\Valve\Steam"
    $regValueSteamPath = "SteamPath"

    try {
        $steamPathProp = Get-ItemProperty -Path $regKeySteam -Name $regValueSteamPath
        $steamPath = $steamPathProp.SteamPath

        Write-Host "Steam path: $steamPath"
        return $steamPath
    }
    catch {
        Write-Warning $_
    }

    return $null
}

function Get-SteamLibraryPaths {
    <#
    .SYNOPSIS
    Returns an array of paths to Steam game libraries.
    #>

    param (
        [string]$SteamPath
    )

    $libraryFoldersPath = Join-Path -Path $SteamPath -ChildPath "/steamapps/libraryfolders.vdf"
    Write-Host "Library folders info: $libraryFoldersPath"

    $libraryFoldersContent = Get-Content -Path $libraryFoldersPath
    $libraryPaths = @()

    Write-Host "Parsing library paths.."
    foreach ($line in $libraryFoldersContent) {
        if ($line -match '"path"\s+"(.+)"') {
            $path = Join-Path -Path $matches[1].Replace("\\", "/") -ChildPath "steamapps\common"
            $libraryPaths += $path
            Write-Host "    $path"
        }
    }

    return $libraryPaths
}

function Get-SteamGamePath {
    <#
    .SYNOPSIS
    Attempts to find a game installation location from different library paths.
    A game installation folder can be named either by appId or appName.
    #>

    param (
        [int]$AppId,
        [string]$AppName
    )

    $steamPath = Get-SteamPath

    # We need a path to the Steam installation.
    if ($null -eq $steamPath) {
        Write-Warning "Steam installation path was not found!"
        return $null
    }

    $libraryPaths = Get-SteamLibraryPaths -SteamPath $steamPath

    foreach ($path in $libraryPaths) {
        $gamePath = Join-Path -Path $path -ChildPath $AppId
        if (Test-Path -Path $gamePath) {
            Write-Host "Game path: $gamePath"
            return $gamePath
        }

        $gamePath = Join-Path -Path $path -ChildPath $AppName
        if (Test-Path -Path $gamePath) {
            Write-Host "Game path: $gamePath"
            return $gamePath
        }
    }

    Write-Warning "Game path was not found!"
    return $null
}

function New-ModRelease {
    <#
    .SYNOPSIS
    Creates a new release.

    .DESCRIPTION
    Creates a directory ".\Releases\ModName" and ".\Releases\ModName-x.y.z.zip"
    zip file. Returns an object with following properties:

    Path: Path to the release
    ZipPath: Path to the release zip
    Version: Version of the release

    .PARAMETER ModName
    Mod's name. Assembly and mod's name should match.

    .PARAMETER FilesToInclude
    List of files to include in the release.

    .PARAMETER ReleasePath
    Path that holds all releases.

    .NOTES
    If an included file is missing, then throws [System.IO.FileNotFoundException].
    #>
    param (
        [string]$ModName,
        [string[]]$FilesToInclude,
        [string]$ReleasePath
    )

    $info = Get-Content -Path ".\Info.json" -Raw | ConvertFrom-Json
    $versionNumber = $info.Version

    $outputPath = Join-Path -Path $ReleasePath -ChildPath $ModName
    $outputZipPath = $outputPath + "-$versionNumber.zip"

    if (-not (Test-Path -Path $outputPath)) {
        Write-Host "Creating a new folder: $outputPath"
        New-Item -ItemType Directory -Path $outputPath -Force
    }

    Write-Host "Copying files.."
    foreach ($file in $FilesToInclude) {
        if (-not (Test-Path -Path $file)) {
            throw [System.IO.FileNotFoundException] "$file not found."
        }

        Write-Host "    $file"

        $fileName = Get-ChildItem $file | Select-Object -ExpandProperty Name
        Copy-Item -Path $file -Destination "$outputPath\$fileName"
    }

    Write-Host "Creating zip file.."

    if (Test-Path -Path $outputZipPath) {
        Remove-Item -Path $outputZipPath -Force
    }

    Add-Type -Assembly "System.IO.Compression.FileSystem"
    [System.IO.Compression.ZipFile]::CreateFromDirectory($outputPath, $outputZipPath)

    Write-Host "Release zip created: $outputZipPath"
    return New-Object PSObject -Property @{
        Path = $outputPath
        ZipPath = $outputZipPath
        Version = $versionNumber
    }
}