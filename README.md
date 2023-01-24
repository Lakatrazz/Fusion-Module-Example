# Fusion Module Example

This is an example project for a BONELAB: Fusion module.

# Fusion Source Code
- https://gitlab.com/Lakatrazz/bonelab-fusion

# Project Setup

Before cloning the project, you'll need:
- Legitimate copy of BONELAB
- Melonloader installation
- BONELAB: Fusion Mod DLL

In order to setup the project:
1. Clone the git repo into a folder
2. Setup a "managed" folder in the root, and a "dependencies" folder in the "FusionModuleExample" folder.
3. Drag the dlls from Melonloader/Managed into the managed folder.
4. Drag MelonLoader.dll and 0Harmony.dll into the managed folder.
5. Drag LabFusion.dll into FusionModuleExample/dependencies/.
6. You're done!

# Contains
- TestMod project (a basic MelonLoader mod that loads the module)
- FusionModuleExample project (the actual module itself)

The module example shows:
- Setting up a module
- Sending messages
- Receiving messages
- IFusionSerializable implemenation
- Hooking into multiplayer methods