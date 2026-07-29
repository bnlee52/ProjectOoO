# AI Coding Instructions for Spooky Ghost House Simulator

## Project Overview
Unity 6.0 (6000.5.6f1) game project named "spooky-ghost-house-simulator". Currently contains only tutorial infrastructure and placeholder scenes. The game is being developed for Windows standalone builds using URP (Universal Render Pipeline).

## Key Technologies & Setup
- **Engine**: Unity 6.0 LTS with C# (.NET Standard 2.1)
- **Rendering**: Universal Render Pipeline (URP) v17.5.0
- **Input System**: New Input System (com.unity.inputsystem 1.20.0)
- **Editor**: Configure IDE in ProjectSettings/EditorSettings.asset and associate .sln with Rider/Visual Studio
- **Solution Files**: Use `spooky-ghost-house-simulator.slnx` (modern format) not .sln for IDE integration

## Project Structure
```
Assets/
├── Scenes/           # Unity scenes (.unity files) - currently SampleScene only
├── Prefabs/          # Reusable GameObject templates (currently empty)
├── UI/               # UGUI canvas-based UI (currently empty)
├── Audio/            # Audio clips and settings
├── Art/              # Sprites, models, materials, textures
├── Settings/         # Project-wide settings (ScriptableObjects)
├── ThirdParty/       # External assets (if any)
└── TutorialInfo/     # Tutorial/readme system (keep as-is)

ScriptAssemblies/    # Compiled dlls in Library/ (auto-generated)
```

## Critical Developer Workflows

### Opening/Building the Project
- Open `.slnx` solution file in Rider or Visual Studio for full IntelliSense
- First scene: `Assets/Scenes/SampleScene.unity`
- Build target: Windows Standalone (set in File > Build Settings)
- Build output goes to root `Build/` directory (add to .gitignore if not present)

### Adding Game Scripts
1. **Namespace Convention**: No namespace required for simple scripts, but use hierarchy like `namespace Simulator.Gameplay` for organization
2. **File Location**: Place new scripts in `Assets/Scripts/` directory (create this folder first)
3. **MonoBehaviour Pattern**: For game objects, inherit from `MonoBehaviour`; keep domain logic separate in data classes
4. **ScriptableObject Pattern**: Use for game settings, dialogue, and serializable data structures in `Assets/Settings/`

### Testing & Debugging
- No explicit test framework configured yet (Unity Test Framework 1.7.0 is available)
- Debug by running scenes in Unity Editor (Play mode)
- Check Logs/trace-config.json and Logs/traces.jsonl for runtime diagnostics
- Use Debug.Log for prints (visible in Console window)

## Project-Specific Conventions

### Scene Organization
- Keep all interactive gameplay in scenes under `Assets/Scenes/`
- Each scene should have a clear purpose documented in a Readme GameObject or comment
- Link prefabs from `Assets/Prefabs/` into scenes, don't instantiate complex hierarchies directly in scenes

### Asset Naming
- Sprites/Models: PascalCase, e.g., `Ghost_Idle.png`, `ChandelierModel.fbx`
- Prefabs: Match GameObject name, e.g., `Assets/Prefabs/Ghost.prefab`
- Materials: `Material_[Surface]`, e.g., `Material_WoodenFloor.mat`
- ScriptableObjects: `[Type]_[Name]`, e.g., `GameSettings_Main.asset`

### UI Pattern (UGUI)
- UI canvases should be in their own scene or prefab, not mixed with gameplay
- Use UI prefabs for reusable components (buttons, panels)
- Reference UI controllers through singleton or dependency injection pattern

## Dependencies & Important Packages
| Package | Version | Usage |
|---------|---------|-------|
| URP | 17.5.0 | Rendering pipeline (don't downgrade to Built-in) |
| Input System | 1.20.0 | Player input handling (required for modern input) |
| Timeline | 1.8.12 | Cinematic/sequence authoring |
| Navigation | 2.0.14 | AI pathfinding (available if needed) |
| Visual Scripting | 1.9.11 | Node-based logic (optional tool) |

**Note**: All standard modules enabled (physics, animation, audio, etc.)

## Code Organization Principles

### Separation of Concerns
- **Game Logic**: Pure C# classes that don't inherit from MonoBehaviour (in `Scripts/Logic/`)
- **Scene Managers**: MonoBehaviours that orchestrate scene state (in `Scripts/Managers/`)
- **Utilities**: Reusable helper functions in `Scripts/Utilities/` with static methods

### Example Structure to Follow
```csharp
// Logic/GhostBehavior.cs - No MonoBehaviour dependency
public class GhostAI
{
    private Vector3 targetPosition;
    public void MoveToward(Vector3 target, float speed) { ... }
}

// Managers/GhostSpawner.cs - Scene orchestration
public class GhostSpawner : MonoBehaviour
{
    private GhostAI ghostLogic;
    public void SpawnGhost() { ghostLogic = new GhostAI(); }
}
```

### Serialization & Persistence
- Use `[SerializeField]` for editor tweaking only (not data persistence)
- Save/load data through explicit serialization methods or dedicated save system
- Avoid relying on scene state for critical game data

## Integration Points & External Dependencies
- **Input System**: New Input System required; create InputActions asset in `Assets/Settings/` and reference in player controller
- **URP Settings**: Stored in `ProjectSettings/URPProjectSettings.asset`; modify to change rendering quality
- **Project Settings**: Audio, Physics, Time (gravity), Quality tiers all configurable in ProjectSettings/

## Known Limitations & Gotchas
1. **Editor Only Code**: Place Editor scripts in `Assets/*/Editor/` folders (e.g., `Assets/TutorialInfo/Scripts/Editor/`)
2. **Asset Serialization**: Unity serializes only public fields and `[SerializeField]` on private fields; properties are NOT serialized
3. **.meta Files**: Git-tracked alongside assets; critical for GUID references—never delete manually
4. **Script Compilation**: Changes to .csproj or new assembly definitions require reloading—restart editor if IntelliSense breaks

## Recommended Next Steps for Implementation
1. Create `Assets/Scripts/` folder structure with subfolders: `Gameplay/`, `UI/`, `Managers/`, `Utilities/`
2. Set up Input Actions for ghost house interactions (look, move, interact)
3. Define core game loop: scene initialization → player input handling → update → render
4. Create a `GameManager` singleton for global state (game over, score, etc.)

## References
- Unity 6.0 Docs: https://docs.unity.com/6000.0/Documentation/Manual/
- URP Best Practices: ProjectSettings/URPProjectSettings.asset
- Current Test Framework: Unity Test Framework 1.7.0 (optional, add tests in Assets/Tests/)
