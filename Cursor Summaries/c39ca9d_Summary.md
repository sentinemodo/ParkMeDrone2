# c39ca9d_Summary.md

## Test-first setup summary

### 1. **TESTING.md** (test-first policy)
- Describes the workflow: write a failing test → implement until it passes → refactor.
- States that core logic lives in **ParkMeDrone2.Core** and is tested by **ParkMeDrone2.Tests**.
- Includes a short example and how to run tests (`dotnet test ParkMeDrone2.Tests/...`).

### 2. **ParkMeDrone2.Core** (.NET Standard 2.0)
- Holds shared, testable models and (later) logic.
- **Models/ParkingSlotMarker.cs** moved here from the UWP app (same namespace `ParkMeDrone2.Models`).
- **IsValid** added: `true` when `RelativeX` and `RelativeY` are in [0, 1].

### 3. **ParkMeDrone2.Tests** (MSTest, .NET 8)
- References **ParkMeDrone2.Core** only (no UWP).
- **ParkingSlotMarkerTests**:
  - `NewMarker_HasNonEmptyDisplayLabel` – already passing.
  - `Marker_IsValid_WhenRelativeCoordinatesInZeroToOne` – was failing until `IsValid` existed, now passing.
  - `Marker_IsInvalid_WhenRelativeCoordinatesOutOfRange` – same.

### 4. **Solution and UWP**
- **ParkMeDrone2.sln** includes Core and Tests and their build configs.
- **ParkMeDrone2** (UWP) references **ParkMeDrone2.Core** and no longer has its own `ParkingSlotMarker.cs`; it uses the type (and `IsValid`) from Core.

### 5. **Test run**
- All 3 tests pass.

**Going forward:** for each new behaviour, add a test in **ParkMeDrone2.Tests** that describes the desired outcome and run it (it should fail). Then implement or change code in **ParkMeDrone2.Core** (or the app, with logic behind testable abstractions) until the test passes. Run tests with:

```bash
dotnet test ParkMeDrone2.Tests/ParkMeDrone2.Tests.csproj
```
