$ErrorActionPreference = 'Stop'

$packageName = 'ipnetwork'
$toolsDir = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$version = 'XXX_VERSION_XXX'

# Package parameters
$packageArgs = @{
  packageName   = $packageName
  unzipLocation = $toolsDir
  url64bit      = "https://github.com/lduchosal/ipnetwork/releases/download/$version/ipnetwork-win-x64.zip"
  checksum64    = ''  # Will need to be filled in after creating the release
  checksumType64= 'sha256'
}

# Download and extract the package
Install-ChocolateyZipPackage @packageArgs

# Note: Chocolatey will automatically create shims for any .exe files in the tools directory
# The ipnetwork.exe will be available in PATH after installation

Write-Host "$packageName has been installed successfully." -ForegroundColor Green
Write-Host "You can now use 'ipnetwork' command from any location." -ForegroundColor Green
