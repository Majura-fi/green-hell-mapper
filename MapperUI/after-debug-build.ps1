. ".\utils.ps1"

New-Release `
    -Path ".\bin\Debug\net8.0-windows" `
    -ReleasePath ".\Releases_Debug" `
    -ModInfoPath "..\Mapper\Info.json"
