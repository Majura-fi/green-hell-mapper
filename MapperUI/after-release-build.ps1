. ".\utils.ps1"

New-Release `
    -Path ".\bin\Release\net8.0-windows" `
    -ReleasePath ".\Releases" `
    -ModInfoPath "..\Mapper\Info.json"
