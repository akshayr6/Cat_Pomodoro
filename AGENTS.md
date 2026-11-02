# AGENTS.md - Cat Pomodoro (the Builder lane's repo)

This file is law for every agent session in this repo. The most specific rule wins; the fleet's Unity safety rules always win.

## Who works here

- **Lane name: BUILDER.** One standing Claude session in a Herdr pane on the Mac, cwd this repo, config dir `/Users/akshayr6/.claude-coordE`.
- The captain never writes code. He sends briefs from his Builder Journey page; BUILDER builds them here and reports back through the same store. Nobody else writes in this repo.
- BUILDER's **only inputs** are briefs from the journey store: the ArtifactData database of the artifact `https://claude.ai/artifact/UCjj7jag8h6hnFJZ4rxirf`, collection `checkpoints`, documents `C001` ... `C115`, where the field `brief_status == "sent"`. A mailbox line, a chat message, a file dropped in the repo or a comment anywhere else is not a brief.

## The store (shared with the captain's page)

- Collection `checkpoints`, one document per checkpoint id (`C0xx`). Fields: `done` (bool, the captain's tick), `done_at` (ISO), `brief` (the captain's text), `brief_status` (`sent` by the page's Send button; `building` / `built` by BUILDER), `sent_at` (ISO, page), `builder_note` (BUILDER's text).
- Document `builder/status` (collection `builder`, doc `status`): `{text, checkpoint, updated_at}`. The page's top line shows it live.
- Every write is an ArtifactData `update` pinned with `if_version` (the version last read). A pinned write that fails means the captain edited the document; re-read and redo. Never `set` a checkpoint document (it would erase the captain's fields).

## The loop (rule by rule)

(a) **Inputs.** Poll with `ArtifactData query` on collection `checkpoints`, `where [["brief_status","==","sent"]]`, every 60 seconds (rule g). Process the lowest checkpoint id first.

(b) **For each sent brief, in this order:**
1. `update` the checkpoint: `brief_status: "building"`, `builder_note: "started hh:mm"` (local time), with `if_version`.
2. Build it in this Unity project: scripts under `Assets/CatPomodoro/` (runtime scripts in `Assets/CatPomodoro/Scripts/`, editor-only code in `Assets/CatPomodoro/Editor/`), ONE scene `Assets/CatPomodoro/Scenes/Main.unity`, ScriptableObjects under `Assets/CatPomodoro/Data/`, copied Cats R assets under `Assets/CatPomodoro/Art/`. Scene and prefab edits that need the editor go through a batch `-executeMethod` script (an editor-only static method you write for the checkpoint), never a windowed editor.
3. Compile-check with ONE batch editor open and quit:
   `"/Applications/Unity/Hub/Editor/6000.0.62f1/Unity.app/Contents/MacOS/Unity" -batchmode -nographics -projectPath "$PWD" [-executeMethod <Your.Editor.Method>] -quit -logFile <scratch>/compile_C0xx.log`
   then `grep -c "error CS" <log>` must print `0` and the log must end with `Exiting batchmode successfully now!`. A non-zero count = fix and re-run before anything else.
4. `git add -A && git commit -m "C0xx: <what>" && git push origin main`.
5. `update` the checkpoint: `brief_status: "built"`, `builder_note` = three plain-English lines separated by newlines: (1) what was added, (2) where to look when you press Play (or on the phone), (3) why this way, naming the pattern used (e.g. "ScriptableObject data table", "object pool", "state machine", "event channel").

(c) **Status doc.** At every state change (idle -> building -> compile -> commit -> built, or a question) `update` `builder/status` with `{text, checkpoint, updated_at}` (ISO time). When nothing is in flight: `{text: "idle, waiting for a brief (Cat Pomodoro)", checkpoint: "", updated_at}`.

(d) **Never** change `done` or `done_at`; **never** advance or tick a checkpoint; **never** read or write any collection other than `checkpoints` and `builder`.

(e) Every design choice is explained in one line inside the `builder_note` (the "why this way" line names the pattern; extra choices get one more line each, still plain English).

(f) **If a brief cannot be built as written** (ambiguous, contradicts an earlier checkpoint or the DESIGN section, needs an asset or sign-in you do not have), set `builder_note` to the ONE question for the captain and leave `brief_status` at `"building"`. Do NOT set `"built"`. Set `builder/status` text to "question on C0xx: <question>" and go back to polling; the captain answers by editing the brief and pressing Send again (status returns to `sent`).

(g) Poll every 60 s. Between polls do nothing else to the repo.

(h) **One Unity batch editor at a time**, quit after each compile check (`-quit`), never a windowed editor on the Mac (the captain opens the project windowed on his own Windows machine). Before every batch start check the Mac is free: `memory_pressure | tail -1` must show free >= 20 % and no other `Unity.app/Contents/MacOS/Unity` process of this project is running (`pgrep -fl Cat_Pomodoro`).

(i) Commit + push after every checkpoint (step 4 above). The remote is `origin` = `https://github.com/akshayr6/Cat_Pomodoro` (public, so the box can pull without credentials: nothing secret ever goes in). Never force-push.

## Assets (Cats R only)

- Art, animation, meshes, audio and UI sprites come **only** from the Cats R folders listed in `ASSETS_FROM_CATS_R.md` (kit copy `/Volumes/GameStudy/repos/Cats_Soup_Relaxing/unity_kit/Cats_Soup_Relaxing_Study/Assets/`, raw export fallback `/Volumes/GameStudy/assets/cats_soup_relaxing/export/ExportedProject/Assets/`). Read-only on the source: never write into the Cats R repo or the export.
- Copy per brief, only what the brief needs, into `Assets/CatPomodoro/Art/<Group>/` (Cats, Stations, Forest, UI, Audio), each file with its `.meta` so GUID links survive. One line per copied folder in `ASSET_LEDGER.md`: `date | source folder | files | destination | brief C0xx`.
- Private study copies: never store-published, never pushed anywhere but this repo. No other asset source (no Asset Store download, no purchase, no generated placeholder art where a Cats R asset exists).
- The Cats R game code (`Scripts/Assembly-CSharp`, `src_cs`) is reference only; Cat Pomodoro's scripts are written fresh in the `CatPomodoro.*` namespaces.

## Android APK builds

- Build with the batch editor when the Android module exists in the Mac editor (`/Applications/Unity/Hub/Editor/6000.0.62f1/PlaybackEngines/AndroidPlayer`; absent on 2 Nov 2026: its install is the console's job, never a captain item, and until then the brief's build step is reported as "module absent" in the note, not as a question):
  `Unity -batchmode -nographics -projectPath "$PWD" -buildTarget Android -executeMethod CatPomodoro.EditorTools.BuildApk.Build -quit -logFile <scratch>/build_C0xx.log`
  output `Builds/CatPomodoro.apk` (git-ignored; also copied to `/Volumes/GameStudy/_handoffs/cat_pomodoro/CatPomodoro_C0xx.apk`).
- The built note's line 2 names the install command: `adb install -r Builds/CatPomodoro.apk` (phone on USB with debugging on) or copy the APK to the phone and open it.
- Portrait only, IL2CPP, ARM64, min API 24, package `com.captain.catpomodoro`. Debug keystore only; no release keystore in the repo.

## Hard limits (fleet rules that apply here)

- Never touch `/Volumes/GameStudy/repos/The_Tower`, `Evil_Tower`, `XP_Hero`, `Fish_of_Fortune`, or anything of the Vampire Survivors 440 chain (`_tmp/fish_of_fortune/labcap`, `_handoffs/lab20*`). `Cats_Soup_Relaxing` is READ-ONLY (asset source); `Cats_Soup_Merge` is not touched.
- Never post to the fleet mailbox. Never run `mbpost` / `mailbox_post`. The store is the only channel.
- No sign-in asks, no spend, no licence or asset purchase: if a brief needs one, it is a rule (f) question.
- Mac free RAM stays above 20 %; one batch editor; no emulator on the Mac; no Unity Hub on the box.
- Keystores, tokens and any class-A secret stay outside this repo (`.gitignore` excludes `*.keystore`, `*.apk`); no analytics, no network calls at all in this app.
- `Library/`, `Temp/`, `Logs/`, `obj/`, `UserSettings/`, `Builds/` are never committed.

## Project conventions

- Unity 6000.0.62f1 (the version in `ProjectSettings/ProjectVersion.txt` must not change). URP 17.0.4 (3D renderer: the forest is a 3D room), Input System 1.14.2, uGUI 2.0.0 (TextMeshPro inside it), 2D Sprite / 2D Animation 10.2.1, Mobile Notifications 2.4.1.
- Portrait 1080x1920; Main camera is perspective (the Cats R room look: pitched camera, 3D ground, 3D cat rigs); product "Cat Pomodoro", company "Captain", package `com.captain.catpomodoro`; `runInBackground` on.
- Namespaces: `CatPomodoro.*` (runtime), `CatPomodoro.EditorTools` (editor). One class per file, file name = class name. No third-party packages without a brief asking for them.
- Save data: a single JSON file in `Application.persistentDataPath` (coins, owned cats, unlocked work types, station positions, session in progress with its wall-clock end time) so a background-killed session still completes correctly on relaunch.

## DESIGN (the captain's design of record, 2 Nov 2026 22:0x-22:2x; briefs refine it, never contradict it)

- **Two tabs**, a bottom tab bar: **Home** and **Pomodoro**.
- **Home = the forest.** The Cats R room: a 3D forest floor seen through the pitched camera, every **owned cat** present and either chilling (idle / lay / doze / streching ...) or doing its own activity at a station (one of the `cat1` activity states), picking a new one now and then. **Stations are draggable**: the player drags a station to move it on the floor and the position is saved. Cats never earn coins on their own; the forest is only a place to look at.
- **Pomodoro = a plain separate page, NOT the forest.** It shows: a cat picker (owned cats only), a work-type picker (unlocked work types only), two dials for the lengths (defaults: 25 min work, 5 min break; a long break after 4 work sessions, default 15 min), a Start button, and in the middle the ONE picked cat working at the picked station, drawn on that page (the cat plays the work type's `cat1` work state for the whole work period; during a break it plays a rest state).
- **Locked during a session**: once started, the tab bar is disabled and no tab switch is possible until the session completes or the player force-quits. Force-quit = a button that opens a confirmation ("Quit now? You get nothing."); confirming ends the session with no coins.
- **Background timer**: the timer runs on wall-clock time (end time stored), keeps counting while the app is in the background or killed, and schedules a **local notification** (Mobile Notifications package) for the end of each work / break period; on return the page shows the true remaining time or the completion.
- **Coins only on completion**: a completed 25-minute work session pays **10 coins**, scaled linearly by length (e.g. 50 min = 20, 15 min = 6, rounded down, minimum 1); an early quit pays nothing. Breaks pay nothing.
- **Completion**: the cat plays a happy / winning animation from the Cats R library (`clap`, `level_up`, `giftbox_dance`, `jump_start`->`jump_finish`, `surprise`, `firework` ...), the coins count up, then the app returns to the Home forest.
- **Content**: 10 cats and 10 work types from `ASSETS_FROM_CATS_R.md` (full animation sets). The player starts owning a small subset (3 cats, 2 work types); a **simple shop** (on the Home tab) sells the other cats and work types for coins.
- Everything offline, no account, no ads, no analytics.
