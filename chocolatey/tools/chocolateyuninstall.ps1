$ErrorActionPreference = 'Stop'

$packageName = 'ipnetwork'
$toolsDir = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

# Chocolatey automatically removes shims, so we just need to clean up any remaining files
Write-Host "Uninstalling $packageName..." -ForegroundColor Yellow

# Remove any leftover files in the tools directory (if needed)
# The tools directory itself will be removed by Chocolatey

Write-Host "$packageName has been uninstalled successfully." -ForegroundColor Green
