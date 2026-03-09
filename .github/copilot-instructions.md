- [x] Verify that the copilot-instructions.md file in the .github directory is created.
  Summary: File exists in `.github/copilot-instructions.md`.

- [x] Clarify Project Requirements
  Summary: Confirmed Windows desktop app in C# WPF with tray icon, mic volume lock, and always-on-top overlay.

- [x] Scaffold the Project
  Summary: Created `MicVolumeGuard.sln` and `MicVolumeGuard.App` WPF project in the current root directory.

- [x] Customize the Project
  Summary: Added mic monitoring/lock services, tray icon service, topmost overlay window, settings window behavior, and project folders (`Services`, `UI`, `Models`, `Utils`, `Assets`).

- [x] Install Required Extensions
  Summary: No extensions needed because project setup info did not specify any.

- [x] Compile the Project
  Summary: `dotnet build .\MicVolumeGuard.sln` completed successfully with 0 errors.

- [x] Create and Run Task
  Summary: Created and ran VS Code task `Build MicVolumeGuard` that builds the solution.

- [x] Launch the Project
  Summary: User confirmed launch; app started with `dotnet run --project .\MicVolumeGuard.App\MicVolumeGuard.App.csproj`.

- [x] Ensure Documentation is Complete
  Summary: Added `README.md` with current project usage and this instruction file is up to date and comment-free.

- Work through each checklist item systematically.
- Keep communication concise and focused.
- Follow development best practices.
