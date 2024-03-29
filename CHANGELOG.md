# Changelog

## [1.0.0-exp.6] - 2022-09-21

### Added

* An explicit dependency on the com.unity.modules.uielements module

### Changed

* Replaced now obsolete ResetValues call in BuildConfigurationScriptedImporterEditor with DiscardChanges
* Updated com.unity.properties to `2.0.0-exp.11`
* Updated com.unity.properties.ui to `2.0.0-exp.11`
* Updated com.unity.serialization to `2.0.0-exp.11`



### Fixed

* Platform selection drop-downs now close properly if you click away from them on macOS and Linux.
* Replaced Path.GetRandomFileName() usage in some unit tests to resolve occasional test failures stemming from a failed test setup involving folder creation with an invalid name

## [0.51.1] - 2022-06-27

### Changed
- Release preparation, no functional changes.

## [0.51.0] - 2022-05-04

### Changed

- Release preparation, no functional changes.

## [0.50.0] - 2021-09-17
### Changed
- Renamed `ScriptingDebuggerSettings` to `EnableScriptDebugging` to better represent its behaviour.

### Fixed
- Correctly initialize PlayerSettings.bundleVersion from GeneralSettings component.

## [0.13.0] - 2021-09-17
### Changed
- Renamed `ScriptingDebuggerSettings` to `EnableScriptDebugging` to better represent its behaviour.

## [0.13.0] - 2021-04-06
### Changed
- Updated `com.unity.platforms` version to 0.13.0.
### Fixed
- Correctly initialize PlayerSettings.bundleVersion from GeneralSettings component.

## [0.12.0] - 2021-03-03
### Changed
- Updated `com.unity.platforms` package dependency.

### Fixed
- Playmode tests via build configurations will correctly support split build and run.

## [0.11.0] - 2020-11-26
### Added
- Added method `GetIcon` on `Platform` as an extension available in `Unity.Build.Editor` namespace.
- Added `Show` serialized property to build configurations, which can be used to determine if it should appear in some areas of the user interface.
- The inspector for `ClassicBuildProfile` will now show if a platform package/module is missing and offer ways to install these missing dependencies.
- Added "active build configuration" functionality to `BuildConfiguration` class. See `SetActive` and `GetActive` for details.

### Changed
- Platform `WebGL` renamed to `Web` for consistency.
- Made `BuildTarget` derived classes sealed and private.
- Interface `IBuildPipelineComponent` now has a `Platform` property.
- Class `BoolResult` is now obsolete; it has been replaced by `ResultBase` and its derived classes.
- All build pipeline will report when the selected platform package is missing.
- Classic build pipeline will report when the selected platform module is missing.
- A `Platform` property is now available in `ContextBase` derived classes.

### Fixed
- Fix possible null reference exception when deleting build configuration asset with inspector open.

## [0.10.0] - 2020-11-12
### Added
- Static class `TypeConstructionUtility` is now public and provides various utilities to help instantiate types.
- Some `System.Type` extensions have been made public for convenience.
- Added missing inline documentation for `Platform`, `PlatformInfo` and `KnownPlatforms` classes.

### Changed
- Bump minimum Unity version to `2020.1.2f1`.
- Changed how "known platforms" are exposed in `KnownPlatforms` class.
- Provided platform classes are now sealed.
- Refactoring of build artifact concept:
  - Build artifacts file names are now based on the hash of the build configuration content, to prevent the same build configuration with different settings to overwrite itself.
  - Build artifacts can only be set/removed in `BuildContext`, and will be serialized to disk when the build completes, regardless if it succeeded or failed.
  - Build artifacts can be queried in any class deriving from `ContextBase`.
  - Added new API entry points to handle build artifacts on `ContextBase`, `BuildContext`, `RunContext`, `CleanContext` and `BuildConfiguration`. They should be used instead of `ContextBase` methods `GetValue`, `SetValue`, etc.
  - The new API entry points requires build artifacts to be classes deriving from `IBuildArtifact` and have default constructor.
  - Retrieving `BuildResult` on `BuildContext` is now clearly stated as unsupported (unavailable until build is completed).
  - It is still possible to retrieve `BuildResult` from `RunContext` and `CleanContext` using new method `GetBuildResult`.

### Deprecated
- Method `GetLastBuildArtifact` of class `ContextBase` has been renamed to `GetBuildArtifact`.
- Method `GetLastBuildResult` will be removed from `ContextBase` to be re-introduced as `GetBuildResult` in `RunContext` and `CleanContext`.

### Fixed
- Error dialog box in `ClassicBuildProfile` inspector should now expand properly.

## [0.9.0] - 2020-10-07
### Added
- New `BuildConfiguration` APIs:
  - `GetComponentSource` allows to retrieve which container a component value comes from.
  - `IsComponentUsed` determine if a component is used by the build pipeline of a build configuration.
  - New interface `IBuildComponentInitialize` can be used to provide build components inititalization method.

### Changed
- Update platforms packages to 0.9.0-preview
- Updated `com.unity.properties`, `com.unity.properties.ui` and `com.unity.serialization` to `1.5.0-preview`.
- Build configuration inspector has been refactored:
  - Style is now similar to other Unity inspectors.
  - Added a search field to browse for build component or their field names.
  - The search field will be improved with filters if the `QuickSeach` package is installed.
  - `Dependencies` list has been renamed to `Shared Configurations`.
  - `Shared Configurations` list can now be re-ordered.
  - Build components styling improved for readability.
  - `Suggested Components` are now listed with other components, with a read-only greyed out style.
  - `Suggested Components` are hidden by default; they can be shown from the option menu on build configuration inspector, next to the asset name.
  - Added option button to build components, allowing to reset value, remove component/overrides, or go to the configuration from which the value is inherited.
  - Made the `Add Component` button the same as other Unity inspectors.
- Method `BuildConfiguration.IsComponentOverridden` is now obsolete; it has been renamed to `BuildConfiguration.IsComponentOverriding`.
- Method `BuildConfiguration.SetComponent(Type, IBuildComponent)` is now obsolete. Use `BuildConfiguration.SetComponent(IBuildComponent)` instead.
- Remove bee.dll assembly, the platforms are now implemented in Unity.Build.Classic namespace


### Fixed
- Reverting removing build component overrides for array fields will properly bring back previous value.
- Alignment of build components fields should not longer push the value out when the name is long.
- Fixed "Unapplied import settings" popup in Unity 2020.2.
- When a build configuration is read-only, foldouts of build component fields will remain functional to allow inspection.
- Fixed several refresh issues.
- Optimized component lookup performance when build configurations have one or more dependencies.
- Fixed no error reported when build configuration failed to deserialize build components.
- Fixed create build configuration menu items that were sometimes disabled.

### Removed
- Obsolete class `BuildTypeCache` has expired; it has been removed.

## [0.8.0] - 2020-08-27
### Changed
- Update platforms packages to 0.8.0-preview
- Updated `com.unity.properties`, `com.unity.properties.ui` and `com.unity.serialization` to `1.4.3-preview`.

## [0.7.0] - 2020-07-13
### Changed
- Update platforms packages to 0.7.0-preview

## [0.6.0] - 2020-07-07

### Changed
- Updated `com.unity.properties` package version to `1.3.1-preview`.
- Updated `com.unity.properties.ui` package version to `1.3.1-preview`.
- Updated `com.unity.serialization` package version to `1.3.1-preview`.

### Fixed
- Compilation fixes for Unity 2020.2.0a17 or newer.

### Removed
- Removed obsolete code that expired on 2020-07-01.
- Removed obsolete assembly `Unity.Build.Common`. Types have been merged in `Unity.Build`, but are still in `Unity.Build.Common` namespace.

## [0.5.0] - 2020-06-08

### Changed
- Tiny build specific classes moved from `Unity.Platforms` to `Unity.Build.DotsRuntime`.

### Fixed
- Fix issue where ClassicBuildProfile accidentally would pick up a wrong pipeline used for testing purposes.

## [0.4.1] - 2020-05-27

### Changed
- Update platforms packages to 0.4.1-preview
- Updated minimum Unity version to 2020.1.

## [0.4.0] - 2020-05-20

### Added
- New component `ClassicCodeStrippingOptions`, exposes Classic's StripEngineCode and ManagedStrippingLevel values.
- New class `RunTargetBase`, can be used to specify deploy targets for pipelines.
- Added `com.unity.scriptablebuildpipeline` package dependency, required for upcoming incremental pipeline.
- Added `BuildConfiguration.DeserializationContext` which can be retrieved in `IJsonMigration` derived classes from the `JsonMigrationContext.UserData` member.

### Changed
- Updated `com.unity.properties` package version to `1.3.0-preview`.
- Updated `com.unity.properties.ui` package version to `1.3.0-preview`.
- Updated `com.unity.serialization` package version to `1.3.0-preview`.
- Classic build pipelines will pick development players when ClassicBuildProfile Configuration is set to Debug or Development.
- `Unity.Build.Common.asmdef` has been deprecated. All components defined there previously are now available in `Unity.Build.asmdef`. Users should remove all references to `Unity.Build.Common` from their asmdefs. The type names and namespaces have been maintained. List of moved types:
    - `Unity.Build.Common.GeneralSettings`
    - `Unity.Build.Common.GraphicsSettings`
    - `Unity.Build.Common.OutputBuildDirectory`
    - `Unity.Build.Common.PlayerScriptingDefines`
    - `Unity.Build.Common.SceneList`
    - `Unity.Build.Common.ScriptingDebuggerSettings`
- Classic Components which are not explicitly added to the build configuration will get its default values from Player Settings, the following components are affected:
    - `Unity.Build.Classic.ClassicCodeStrippingOptions`
    - `Unity.Build.Classic.ClassicScriptingSettings`
    - `Unity.Platforms.Android.Build.AndroidAPILevels`
    - `Unity.Platforms.Android.Build.AndroidArchitectures`
    - `Unity.Platforms.Android.Build.ApplicationIdentifier`
- Change `Unity.Build.Classic.ClassicCodeStrippingOptions.ManagedStrippingLevel` default value to `ManagedStrippingLevel.Disabled`

### Removed
- Removed obsolete class `BuildSettings`.
- Removed obsolete interfaces `IBuildSettingsComponent` and `IRunStep`.
- Removed obsolete properties `flags`, `description` and `category` on class `BuildStepAttribute`, as well as the nested `Flags` enum.
- Removed obsolete property `BuildSettings` on class `BuildPipelineResult` and `RunStepResult`.
- Removed obsolete property `Scenes` on class `SceneList`.

### Fixed
- Fix issue where active build target wouldn't switch when trying to build using classic build configuration with mismatching active build target.
- Fix build action button in build configuration inspector that would sometimes not work at all, without any error or console messages.

## [0.3.1] - 2020-05-04

### Changed
- Update platforms packages to 0.3.1

## [0.3.0] - 2020-04-28

Build pipeline major overhaul: build pipelines are no longer asset based, and instead must be implemented in code by deriving from `BuildPipelineBase` class. Build steps are no longer mandatory but can still be used by deriving from `BuildStepBase`.

### Added
- New class `BuildPipelineBase` which is a class based replacement for `BuildPipeline` assets. Build steps can be used to organize build code, but is not mandatory anymore.
- New class `BuildStepBase` which is an optional replacement for now obsolete `BuildStep`.
- New class `BuildStepCollection` which represent a list of build steps that can be enumerated and executed.
- New class `BuildResult` and `RunResult` that derives from new base class `ResultBase`.
- New class `BuildProcess` which describe the state of an incremental build process.
- New class `RunContext` which holds the context when a pipeline is ran.
- Methods for querying build component values have been added to `ContextBase`.
- Methods for setting build component values are now available on `ContextBase`. Note that those values are only stored in memory; the build configuration asset is unchanged.
- New method `GetComponentOrDefault` on `BuildConfiguration` which returns the component value if found, otherwise a default instance of the component type without modifying the configuration.
- New method `GetComponentTypes` on `BuildConfiguration` which returns the flatten list of all component types from the configuration and its dependencies.
- New method `SetComponent` on `BuildConfiguration` that only takes a type and sets the component value to a default instance of the component type.
- New method `BuildIncremental` on `BuildPipelineBase` which can be used to implement build pipelines that run in background.
- New build component `ScriptingDebuggerSettings` which allows you to enable Scripting Debugging and Wait For Managed Debugger options.
- Successful build message will contain a hyperlink point towards build directory.
- New build component `PlayerScriptingDefines`, allows you to specify extra scripting defines for your scripts while doing a build.

### Changed
- Class `BuildContext` now derives from new base class `ContextBase`.
- The `RequiredComponents` and `OptionalComponents` lists previously available on `BuildStep` have been replaced with the merged list `UsedComponents` on `BuildStepBase`.
- Methods `CanBuild` and `CanRun` on `BuildConfiguration` no longer expect an out string parameter, and instead return a `BoolResult` that contains the result and the reason.

### Deprecated
- Class `BuildPipeline` is now obsolete. It has been replaced by `BuildPipelineBase` which is no longer asset based. All build pipeline assets must be converted into a corresponding build pipeline class that derives from `BuildPipelineBase`.
- Class `BuildPipelineResult` is now obsolete. It has been replaced by `BuildResult`.
- Class `BuildStep` and `RunStep` are now obsolete. Class based build pipelines no longer enforce the use of build/run steps. Most interfaces and attributes related to `BuildStep` and `RunStep` are also obsolete.
- Class `BuildStepResult` and `RunStepResult` are now obsolete. They have been replaced by `BuildResult` and `RunResult` respectively.
- Property `BuildPipelineStatus` on `BuildContext` is now obsolete. `BuildPipelineResult` and `BuildStepResult` have been combined into `BuildResult`, removing the need for this intermediate status.

### Removed
- Removed optional mutator parameter on `BuildContext` class.

### Fixed
- Fixed a dependency ordering issue causing some components to not be found in some cases.
- Clicking Build/BuildAndRun/Run with unsaved changes will now properly refresh the build configuration asset before performing the action.

## [0.2.2] - 2020-03-23

### Added
- Added `com.unity.properties.ui` package version to `1.1.1-preview`.
- Added support for `LazyLoadReference` for deserializing asset references without loading them (requires Unity 2020.1).
- DotsBuildTarget now has an overridable TargetFramework property, which can be used to change target .NET framework.

### Removed
- Removed `DotsConfig` as it now lives in `com.unity.dots.runtime-0.24.0`.
- Removed unused dependency on `com.unity.dots.runtime'.
- Removed dependency on newtonsoft json to use serialization package API instead.

### Changed
- Updated `com.unity.properties` package version to `1.1.1-preview`. This is a major overhaul, please refer to the package documentation.
- Updated `com.unity.serialization` package version to `1.1.1-preview`. This is a major overhaul, please refer to the package documentation.

### Fixed
- Show apply/revert/cancel dialog if build configuration is modified upon clicking Build and/or Run button.
- Fixed build configuration inspector when using Unity 2020.1 and above.
- Build progress bar will update after elapsed time even if no values changed.

## [0.2.1] - 2020-02-25

### Added
- Support for building testable players (`TestablePlayer` component) as a step towards integration with the Unity Test Framework.
- Add a UsesIL2CPP property to BuildTarget

### Changed
- Enable Burst for DotNet builds on Windows
- Revert namespace `Unity.Platforms.Build*` change back to `Unity.Build*`.

### Fixed
- Fix Build & Run fallback when build pipeline doesn't have a proper RunStep, BuildOption.AutoRunPlayer was being set too late, thus it didn't have any effect, this is now fixed.
- Build configuration/pipeline assets will now properly apply changes when clicking outside inspector focus.
- Fixed asset cannot be null exception when trying to store build result.

## [0.2.1-preview] - 2020-01-24

### Changed
- Modfied data format for SceneList to contain additional flags to support LiveLink.
- `BuildStepBuildClassicLiveLink` was moved into the `Unity.Scenes.Editor` assembly in `com.unity.entities` package due to dependencies on Entities.
- Refactored `BuildStepBuildClassicPlayer` since it no longer shares its implementation with `BuildStepBuildClassicLiveLink`
- `ClassicBuildProfile.GetExecutableExtension` made public so that it can be used from other packages.

## [0.2.0-preview.2] - 2020-01-17

### Fixed
- Fix `BuildStepBuildClassicLiveLink` build step to re-generate Live Link player required metadata file.

## [0.2.0-preview.1] - 2020-01-15

### Added
- Platform specific event processing support (new Unity.Platforms.Common assembly).

## [0.2.0-preview] - 2020-01-13

The package `com.unity.build` has been merged in the `com.unity.platforms` package, and includes the following changes since the release of `com.unity.build@0.1.0-preview`:

### Added
- New `BuildStepRunBefore` and `BuildStepRunAfter` attributes which can be optionally added to a `BuildStep` to declare which other steps must be run before or after that step.
- `BuildStep` attribute now support `Name`, `Description` and `Category` properties.
- Added new `RunStep` attribute to configure run step types various properties.

### Changed
- Updated `com.unity.properties` to version `0.10.4-preview`.
- Updated `com.unity.serialization` to version `0.6.4-preview`.
- All classes that should not be derived from are now properly marked as `sealed`.
- All UI related code has been moved into assembly `Unity.Build.Editor`.
- Added support for `[HideInInspector]` attribute for build components, build steps and run steps. Using that attribute will hide the corresponding type from the inspector view.
- Field `BuildStepAttribute.flags` is now obsolete. The attribute `[HideInInspector]` should now be used to hide build steps in inspector or searcher menu.
- Field `BuildStepAttribute.description` is now obsolete: it has been renamed to `BuildStepAttribute.Description`.
- Field `BuildStepAttribute.category` is now obsolete: it has been renamed to `BuildStepAttribute.Category`.
- Interface `IBuildSettingsComponent` is now obsolete: it has been renamed to `IBuildComponent`.
- Class `BuildSettings` is now obsolete: it has been renamed to `BuildConfiguration`.
- Asset extension `.buildsettings` is now obsolete: it has been renamed to `.buildconfiguration`.
- Because all build steps must derive from `BuildStep`, all methods and properties on `IBuildStep` are no longer necessary and have been removed.
- Property `BuildStep.Description` is no longer abstract, and can now be set from attribute `BuildStepAttribute(Description = "...")`.
- Enum `BuildConfiguration` is now obsolete: it has been renamed to `BuildType`.
- Interface `IRunStep` is now obsolete: run steps must derive from `RunStep`.
- Nested `BuildPipeline` build steps are now executed as a flat list from the main `BuildPipeline`, rather than calling `IBuildStep.RunBuildStep` recursively on them.
- Build step cleanup pass will only be executed if the default implementation is overridden, greatly reducing irrelevant logging in `BuildPipelineResult`.
- Class `ComponentContainer` should not be instantiated directly and thus has been properly marked as `abstract`.
- Class `ComponentContainer` is now obsolete: it has been renamed to `HierarchicalComponentContainer`.

### Fixed
- Empty dependencies in inspector are now properly supported again.
- Dependencies label in inspector will now as "Dependencies" again.

## [0.1.8-preview] - 2019-12-11

### Added
- Added Unity.Build.Common files, moved them from com.unity.entities.

## [0.1.7-preview.3] - 2019-12-09

### Changed
- Disabled burst for windows/dotnet/collections checks, because it was broken.

## [0.1.7-preview.2] - 2019-11-12

### Changed
- Changed the way platforms customize builds for dots runtime, in a way that makes buildsettings usage clearer and faster, and more reliable.

## [0.1.7-preview] - 2019-10-25

### Added
- Added `WriteBeeConfigFile` method to pass build target specifc configuration to Bee.

## [0.1.6-preview] - 2019-10-23

### Added
- Re-introduce the concept of "buildable" build targets with the `CanBuild` property.

### Changed
- `GetDisplayName` method changed for `DisplayName` property.
- `GetUnityPlatformName` method changed for `UnityPlatformName` property.
- `GetExecutableExtension` method changed for `ExecutableExtension` property.
- `GetBeeTargetName` method changed for `BeeTargetName` property.

## [0.1.5-preview] - 2019-10-22

### Added
- Added static method `GetBuildTargetFromUnityPlatformName` to find build target that match Unity platform name. If build target is not found, an `UnknownBuildTarget` will be returned.
- Added static method `GetBuildTargetFromBeeTargetName` to find build target that match Bee target name. If build target is not found, an `UnknownBuildTarget` will be returned.

### Changed
- `AvailableBuildTargets` will now contain all build targets regardless of `HideInBuildTargetPopup` value, as well as `UnknownBuildTarget` instances.

## [0.1.4-preview] - 2019-09-26
- Bug fixes
- Add iOS platform support
- Add desktop platforms package

## [0.1.3-preview] - 2019-09-03

- Bug fixes

## [0.1.2-preview] - 2019-08-13

### Added
- Added static `AvailableBuildTargets` property to `BuildTarget` class, which provides the list of available build targets for the running Unity editor platform.
- Added static `DefaultBuildTarget` property to `BuildTarget` class, which provides the default build target for the running Unity editor platform.

### Changed
- Support for Unity 2019.1.

## [0.1.1-preview] - 2019-06-10

- Initial release of *Unity.Platforms*.
