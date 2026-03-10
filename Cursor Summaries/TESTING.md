# Test-first development

We follow a **test-first** approach for ParkMeDrone2:

1. **Write a test** that defines the behaviour we want (the test should **fail**).
2. **Implement the feature** (or fix the code) until that test **passes**.
3. **Refactor** if needed, keeping tests green.

## Workflow

- Before adding or changing behaviour, add or update a test that describes the desired outcome.
- Run the test and confirm it fails (red).
- Implement the minimum change so the test passes (green).
- Commit when the new or updated test is green and the change is clean.

## What we test

- **Core logic and models** live in `ParkMeDrone2.Core` (.NET Standard 2.0) and are tested by `ParkMeDrone2.Tests` (MSTest, .NET 8).
- **UWP UI and DJI SDK integration** stay in the app project; testable rules and state transitions should be moved into Core (or a service abstraction) so they can be driven by tests first.

## Running tests

- From Visual Studio: **Test Explorer** → Run All (or run the test project).
- From command line (repo root):
  ```bash
  dotnet test ParkMeDrone2.Tests/ParkMeDrone2.Tests.csproj
  ```

## Example

1. We want: “A new parking marker has a non-empty display label.”
2. We add a test in `ParkMeDrone2.Tests` that creates a marker and asserts on the label → test **fails**.
3. We implement or adjust `ParkingSlotMarker` (in Core) so the label is non-empty → test **passes**.
4. We commit.
