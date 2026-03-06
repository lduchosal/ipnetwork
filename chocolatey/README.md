# Chocolatey Package for IPNetwork

This directory contains the Chocolatey package configuration for the IPNetwork command-line tool.

## Package Structure

```
chocolatey/
├── ipnetwork.nuspec           # Chocolatey package manifest
├── tools/
│   ├── chocolateyinstall.ps1  # Installation script
│   ├── chocolateyuninstall.ps1 # Uninstallation script
│   └── VERIFICATION.txt       # Package verification info
└── README.md                   # This file
```

## How It Works

Publishing is fully automated via the `.github/workflows/chocolatey.yml` GitHub Action.

When you **publish a GitHub release** (e.g., tag `v3.4.0`), the workflow will:

1. Extract the version from the release tag
2. Publish the .NET application for Windows x64
3. Create a zip archive and upload it to the GitHub release
4. Calculate the SHA256 checksum
5. Replace `XXX_VERSION_XXX` placeholders and checksum in the package files
6. Pack and push the `.nupkg` to Chocolatey

### Setup

Add a `CHOCOLATEY_API_KEY` secret to your GitHub repository (Settings > Secrets > Actions).

## Dependencies

The package depends on:
- `dotnet-10.0-runtime` - Automatically installed by Chocolatey

## Troubleshooting

### Package Installation Fails

- Verify the download URL is accessible
- Check that the checksum matches the uploaded file
- Ensure .NET runtime dependency is correctly specified

### Command Not Found After Installation

- Chocolatey should automatically create shims
- Try refreshing the environment: `refreshenv`
- Restart PowerShell/Command Prompt

## Resources

- [Chocolatey Package Documentation](https://docs.chocolatey.org/en-us/create/create-packages)
- [IPNetwork GitHub Repository](https://github.com/lduchosal/ipnetwork)

## License

This Chocolatey package follows the same BSD-2-Clause license as the IPNetwork project.
