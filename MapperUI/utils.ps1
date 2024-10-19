# This variable controls if the script stops or continues when an error is encountered.
# We need to stop on errors for try-catch to actually catch errors.
$ErrorActionPreference = "Stop"

function New-Release {
    param (
        [string]$Path,
        [string]$ReleasePath,
        [string]$ModInfoPath
    )

    $modInfo = Get-Content -Path $ModInfoPath -Raw | ConvertFrom-Json
    $versionNumber = $modInfo.Version

    $tmpPath = ".\tmp\MapperUI"
    $releaseZipPath = Join-Path -Path $ReleasePath -ChildPath "MapperUI-$versionNumber.zip"

    if (-not (Test-Path -Path $ReleasePath)) {
        Write-Host "Creating new directory: $ReleasePath"
        New-Item -ItemType Directory -Path $ReleasePath
    }

    if (Test-Path -Path $releaseZipPath) {
        Remove-Item -Path $releaseZipPath -Force
    }

    Write-Host "Creating zip.."
    Add-Type -Assembly "System.IO.Compression.FileSystem"

    # Copy files to a temporary "MapperUI" folder to maintain the same zip
    # structure as before.
    if (Test-Path -Path $tmpPath) {
        Remove-Item -Path $tmpPath -Recurse -Force
    }
    New-Item -ItemType Directory -Path $tmpPath
    Copy-Item -Path "$Path\*.*" -Destination $tmpPath -Recurse -Force

    <#
    https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.compressionlevel?view=net-8.0

    0 - Optimal
    The compression operation should optimally balance compression speed and output size.

    1 - Fastest
    The compression operation should complete as quickly as possible, even if the
    resulting file is not optimally compressed.

    2 - NoCompression
    No compression should be performed on the file.

    3 - SmallestSize
    The compression operation should create output as small as possible, even if
    the operation takes a longer time to complete.
    #>
    $compressionLevel = 0
    $includeBaseDirectory = $true
    [System.IO.Compression.ZipFile]::CreateFromDirectory($tmpPath, $releaseZipPath, $compressionLevel, $includeBaseDirectory)

    Remove-Item -Path $tmpPath -Recurse -Force

    Write-Host "Done!"
}
