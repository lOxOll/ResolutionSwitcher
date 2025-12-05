# Resolution Switcher

## Description
Resolution Switcher is a Windows Command Palette Extension that allows you to quickly switch your display resolution directly from the Command Palette.
It provides a convenient way to toggle between common resolutions without navigating through Windows Settings.

## Features
- Switch to **FHD** (1920x1080)
- Switch to **WQHD** (2560x1440)
- Switch to **4K** (3840x2160)

## Functionality
The application registers as a COM server extension for the Command Palette. When invoked, it utilizes native Windows APIs (`user32.dll`) to change the display settings safely.

## Build Instructions

### Prerequisites
- **OS**: Windows 10 (Build 19041) or later
- **SDK**: .NET 9.0 SDK
- **IDE**: Visual Studio 2022 with:
  - .NET Desktop Development workload
  - Windows App SDK

### How to Build
1. Clone the repository to your local machine.
2. Open the solution file `ResolutionSwitcher.sln` in Visual Studio.
3. Restore NuGet packages (this should happen automatically upon opening or building).
4. Build the solution:
   - Select the configuration (Debug/Release) and platform (x64/arm64).
   - Press **Build > Build Solution** (or `Ctrl+Shift+B`).

   Alternatively, you can build via command line:
   ```bash
   dotnet build
   ```

## Usage
Once built and registered as an extension, the commands will be available in your Command Palette environment.
1. Open the Command Palette which supports extensions.
2. Search for "Resolution Switcher" or the specific resolution commands (e.g., "FHD", "4K").
3. Select the desired resolution to apply it immediately.

## License
See the [LICENSE](LISENCE) file for details.
