# Cat Pomodoro

A portrait (1080x1920) Android focus app built in **Unity 6000.0.62f1**: a Cats and Soup-style forest full of cats that
turns into a Pomodoro timer. It is the captain's first real build-along project of the **Builder Journey**
(https://claude.ai/artifact/UCjj7jag8h6hnFJZ4rxirf): each roadmap step is sent as a brief to the Builder lane and built
here, checkpoint by checkpoint, so the captain can open the result, press Play (or install the APK) and judge it.

Art, animation, 3D meshes, UI and audio come from the fleet's Cats R reconstruction (a private study copy of Cats and
Soup); `ASSETS_FROM_CATS_R.md` lists where they live and which cats and work types to use first. Never store-published.

## The two tabs

- **Home - the forest.** Every cat you own is in the forest, chilling or doing its own activity at a station (the Cats R
  3D room with the game's cat rigs and animations). Drag a station to move it. A simple shop sells more cats and more
  work types for coins. Cats never earn coins on their own.
- **Pomodoro - the session page.** A plain page, not the forest. Pick ONE cat and ONE unlocked work type, set the lengths
  with the dials (25 min work / 5 min break by default, a long break after 4 sessions), press Start. The cat works at its
  station in the middle of the page for the whole work period. The page is locked (no tab switch) until the session
  completes or you force-quit with a confirmation. The timer keeps running in the background and a local notification
  fires at the end. On completion the cat plays a happy / winning animation, you get coins (10 for 25 minutes, scaled by
  length; nothing for quitting early) and the app returns to the forest.

Details of record are in `AGENTS.md`, section DESIGN.

## What is in the repo

- `Assets/CatPomodoro/Scenes/Main.unity` - the one scene; both tabs live in it.
- `Assets/CatPomodoro/Scripts/` - runtime code (namespace `CatPomodoro.*`).
- `Assets/CatPomodoro/Data/` - ScriptableObject tables (cats, work types, shop prices).
- `Assets/CatPomodoro/Art/` - the Cats R assets copied per brief (`ASSET_LEDGER.md` has one line per copied folder).
- `Assets/CatPomodoro/Editor/` - editor-only tools: `CatPomodoroSetup.cs` (the one-shot project setup) and the batch scene builders the Builder writes per checkpoint.
- `Packages/manifest.json` - URP 17.0.4, 2D Sprite, 2D Animation 10.2.1, Input System 1.14.2, uGUI 2.0.0 (TextMeshPro lives inside it in Unity 6), Mobile Notifications 2.4.1.
- `AGENTS.md` - the Builder lane's rules and the DESIGN of record (read it before changing anything).
- `ASSETS_FROM_CATS_R.md` - the Cats R asset inventory (paths, counts, the 10 candidate cats and 10 work types).

`Library/`, `Temp/`, `Logs/`, `obj/`, `UserSettings/`, `Builds/` are generated and not committed; Unity rebuilds them on first open.

## How the captain opens it on Windows

1. Clone: `git clone https://github.com/akshayr6/Cat_Pomodoro.git` (public repo).
2. Unity Hub -> **Add** -> **Add project from disk** -> choose the cloned folder.
3. The editor version **must be 6000.0.62f1** (installed on the box; the Hub shows it next to the project). Opening with another version re-imports and changes `ProjectVersion.txt`, which the Builder's checks then fail.
4. First open takes a few minutes (Library build). Then open `Assets/CatPomodoro/Scenes/Main.unity` and press Play. Set the Game view aspect to **Portrait 1080x1920** (the dropdown above the Game view).
5. After each checkpoint the Builder pushes a commit named `C0xx: <what>`; `git pull` and Unity picks the changes up automatically.

## How the captain gets it on his phone (Android)

- Everything happens on the Windows machine (`D:\GameStudy\cat_pomodoro`, Unity 6000.0.62f1 with the Android Build Support module installed on 3 Nov 2026; adb at `C:\Program Files\Unity\Hub\Editor\6000.0.62f1\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\platform-tools\adb.exe`). Phone on USB with USB debugging on, then **File > Build Profiles > Android > Build And Run**: the app installs and opens on the phone. `README_WINDOWS.md` (also at `D:\GameStudy\journey\README_WINDOWS.md`) has the phone setup step by step.
- The Builder's machine has no Android module: when a brief asks for an APK, the Builder's note says what was built in the editor and the captain makes the phone build himself with Build And Run after `git pull`.
- iPhone comes later (needs a Mac build with the iOS module and a signing identity; not part of the current roadmap).

## How the loop works

1. On a build checkpoint of the Journey page (open in Chrome on the Windows machine) you edit the starter brief and press **Send**. The page stores it in its shared database (collection `checkpoints`, doc `C0xx`, `brief_status = sent`).
2. The Builder lane (a Claude session on the Mac, pane name BUILDER) polls that database every 60 s. When it sees `sent`, it marks the checkpoint `building` with a "started hh:mm" note and the page's top line shows what it is doing.
3. It writes the code and scene changes here, copies the Cats R assets the brief needs, compiles with a headless Unity (no window), commits and pushes `C0xx: <what>`.
4. It marks the checkpoint `built` and writes a three-line note: what was added, where to look when you press Play or on the phone, why this way (the pattern's name). You read the note on the page, pull, press Play, and tick the checkpoint yourself - the Builder never ticks it.
5. If a brief cannot be built as written, the note becomes ONE question and the status stays `building`. Edit the brief to answer and press Send again.

The Builder never changes your ticks, never posts anywhere else, never asks you to sign in or buy anything.
