# Builder Journey on this Windows machine - README (3 Nov 2026)

Everything for the journey is on this machine. You do not need the Mac for anything except the one iPhone step at the very end (C111).

## The three pages (open in Chrome; they are also saved here)

- Builder Journey (start here): https://claude.ai/artifact/UCjj7jag8h6hnFJZ4rxirf
- Unity Course Companion: https://claude.ai/artifact/3SL4vHDwcWfDZgLY1ciTjs
- Games Companion: https://claude.ai/artifact/CF77MTBovGykUC7vVeagD2
- LAB20 Weapon Pictures (gallery): https://claude.ai/artifact/6pxjobUmfQHAQYNccCgUvC
- Local copies, if the internet is off: `D:\GameStudy\journey\pages` (builder_journey.html, unity_course_helper.html with its frames folder, games_companion.html). The local copies do not have the shared store (ticks and briefs), so use the online pages for the journey itself.

## Where everything is

| What | Where on this machine |
|---|---|
| The Cat Pomodoro project (your real first project; a clone of https://github.com/akshayr6/Cat_Pomodoro) | `D:\GameStudy\cat_pomodoro` |
| Unity Hub | `C:\Program Files\Unity Hub\Unity Hub.exe` |
| The editor Cat Pomodoro uses | `C:\Program Files\Unity\Hub\Editor\6000.0.62f1` (6000.0.62f1) |
| Android module of that editor (SDK, NDK, OpenJDK) | `C:\Program Files\Unity\Hub\Editor\6000.0.62f1\Editor\Data\PlaybackEngines\AndroidPlayer` |
| adb (talks to your phone) | `C:\Program Files\Unity\Hub\Editor\6000.0.62f1\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\platform-tools\adb.exe` |
| The Cats R study kit (the asset folder the Builder pulls from; Unity 6000.0.60f1, never opened in 62f1) | `D:\GameStudy\unity_kits\Cats_Soup_Relaxing_Study` |
| The Cats R Part VIII chapters (the Builder's reading on every build step) | `D:\GameStudy\journey\Cats_Soup_Relaxing\reports\part8` |
| The five handoff packages (L6) + the rebuilt XP Hero APK | `D:\GameStudy\journey\handoffs` |
| Where you unzip a package when a kit checkpoint says so | `D:\GameStudy\journey\kits` |
| The checker that proves every path and link on the pages | `D:\GameStudy\journey\journey_check_box.py` (PowerShell: `python D:\GameStudy\journey\journey_check_box.py`) |

Packages in `D:\GameStudy\journey\handoffs`:
- `D:\GameStudy\journey\handoffs\Cats_Soup_Relaxing_Handoff_20261019_2124.zip`
- `D:\GameStudy\journey\handoffs\Evil_Tower_Handoff_20261014_1024.zip`
- `D:\GameStudy\journey\handoffs\The_Tower_Handoff_20261014_0003.zip`
- `D:\GameStudy\journey\handoffs\XP_Hero_Handoff_20261030_1727.zip`
- `D:\GameStudy\journey\handoffs\VS_LAB20_c47g_20261102.zip`
- `D:\GameStudy\journey\handoffs\XPHero_rebuilt_apk_b6n_a7e9dcb5.apk` (the rebuilt XP Hero APK, beside its package)

## The copy-paste rule

The pages are web pages (https). A web page cannot open a folder on your disk, so every disk path on the pages has a **copy** button. Tap copy, then paste the path into the address bar of Explorer (opens the folder) or into PowerShell. Never retype a path.

## Day 1, step by step

1. Open Chrome and the Builder Journey page. Its first card is C001. The trail at the top shows all 115 squares; the outlined one is next.
2. C001 to C003 (level L0) check this machine: Unity Hub, the 6000.0.62f1 editor with its Android module, the Cat Pomodoro project, and how the Builder works. Do them in order; tick each when its three done-test questions pass.
3. From C004 on, the page tells you exactly what to read (one tap opens the card), what to watch (one tap opens the video at the timestamp) and what to do.
4. Build checkpoints (green, L4) have a starter brief and a **Send** button. Edit the brief in your words, press Send. Watch the line at the top of the page ("What the Builder is doing now").

## How to talk to the Builder from this machine

- **The Send button (always works).** Your brief lands in the page's shared store. The Builder lane (a Claude Code session of the fleet, on the Mac) reads it from there, builds, and pushes a commit `C0xx: what it made` to GitHub. Its note appears under your brief ("built", with where to look). Then on this machine: open PowerShell inside `D:\GameStudy\cat_pomodoro` (in Explorer, type `powershell` in the address bar while you are in that folder) and run `git pull`; open the project from Unity Hub (Projects > Add > Add project from disk, once) and press Play.
- **`claude` in PowerShell (when a pool is live).** In `D:\GameStudy\cat_pomodoro` type `claude` and a Builder runs on this machine itself. Honest state on 3 Nov 2026: every Claude account configured here is at its weekly cap; the first resets come on Fri 6 Nov. Until then use the Send button. Nothing to buy, nothing to sign in to: the accounts are already configured in their folders.
- The Builder never ticks your checkpoints, never posts anywhere else, never asks you to sign in or buy anything.

## Your phone (Android), once

1. Settings > About phone > tap **Build number** seven times (Developer options unlock).
2. Settings > System > Developer options > **USB debugging** ON.
3. Plug the phone into this machine with a USB cable; tap **Allow** on the phone.
4. PowerShell: `& "C:\Program Files\Unity\Hub\Editor\6000.0.62f1\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\platform-tools\adb.exe" devices` lists the phone as `device`.
5. In Unity: File > Build Profiles > Android > **Build And Run**. The app installs and opens on the phone. (Build steps that ask for an APK are yours: the Builder's machine has no Android module; this one has.)

## Where a topic is taught (tool-written from JOURNEY_MAP.tsv + CHECKPOINTS.tsv; the number is how many mapped rows matched)

- **🎞️ Animation** (25 checkpoints): C018 Chapter 12: Animations (48), C094 Chapter 46: Polish (12), C072 Chapter 38: Sound Effects (5), C019 Game slice: ⚔️ XP Hero · 🎬 animation + VFX + audio (4), C037 Chapter 22: Container Counter (4), C025 Chapter 16: Clear Counter (3), C061 Game slice: 😈 Evil Tower · 🎬 animation + VFX + audio (3), C073 Game slice: 🗼 The Tower · 🎬 animation + VFX + audio (3) … and 17 more on the page
- **🎨 Shaders and materials** (21 checkpoints): C060 Chapter 34: Delivery Counter, Shader Graph (31), C014 Chapter 9: Post Processing (18), C007 Chapter 4: Create Project (12), C017 Chapter 11: Character Visual, Rotation (10), C074 Chapter 39: Game Start (8), C004 Chapter 1: Intro, Overview (6), C099 Game slice: 😈 Evil Tower · ⚡ performance + build (4), C019 Game slice: ⚔️ XP Hero · 🎬 animation + VFX + audio (3) … and 13 more on the page
- **✨ VFX and particles** (15 checkpoints): C048 Chapter 29: Stove Counter, State Machine (11), C061 Game slice: 😈 Evil Tower · 🎬 animation + VFX + audio (4), C094 Chapter 46: Polish (4), C059 Game slice: 😈 Evil Tower · 📱 UI + feedback (3), C073 Game slice: 🗼 The Tower · 🎬 animation + VFX + audio (3), C005 Chapter 2: Final Game Preview (2), C019 Game slice: ⚔️ XP Hero · 🎬 animation + VFX + audio (2), C021 Game slice: 🐱 Cats R · 🎬 animation + VFX + audio (2) … and 7 more on the page
- **🔊 Audio** (15 checkpoints): C072 Chapter 38: Sound Effects (49), C071 Chapter 37: Music (24), C085 Chapter 43: Options, Audio Levels (5), C014 Chapter 9: Post Processing (4), C061 Game slice: 😈 Evil Tower · 🎬 animation + VFX + audio (4), C004 Chapter 1: Intro, Overview (3), C021 Game slice: 🐱 Cats R · 🎬 animation + VFX + audio (3), C094 Chapter 46: Polish (3) … and 7 more on the page
- **⚡ Optimisation and the Profiler** (19 checkpoints): C014 Chapter 9: Post Processing (7), C095 Game slice: ⚔️ XP Hero · ⚡ performance + build (6), C099 Game slice: 😈 Evil Tower · ⚡ performance + build (6), C101 Game slice: 🗼 The Tower · ⚡ performance + build (3), C018 Chapter 12: Animations (2), C021 Game slice: 🐱 Cats R · 🎬 animation + VFX + audio (2), C045 Chapter 27: Look At Camera (2), C103 Game slice: 🐱 Cats R · ⚡ performance + build (2) … and 11 more on the page
- **📦 Build, APK and keystore** (24 checkpoints): C079 Chapter 41: Main Menu, Loading (5), C099 Game slice: 😈 Evil Tower · ⚡ performance + build (2), C101 Game slice: 🗼 The Tower · ⚡ performance + build (2), C109 Build-along 14 of 14: Android build, profiling on the phone, APK install (Cat Pomodoro) (2), C009 Build-along 1 of 14: Project + Android setup (Cat Pomodoro) (1), C015 Chapter 10: Character Controller (1), C025 Chapter 16: Clear Counter (1), C032 Build-along 2 of 14: Boot + data tables (Cat Pomodoro) (1) … and 16 more on the page
- **🔔 Notifications** (5 checkpoints): C083 Build-along 8 of 14: The Pomodoro page: pick cat + work, dials, background timer, notification, lock (Cat Pomodoro) (2), C065 Game slice: 🐱 Cats R · 🪙 economy + monetisation + retention (1), C075 Game slice: 😈 Evil Tower · 🪙 economy + monetisation + retention (1), C089 Game slice: 🗼 The Tower · 🪙 economy + monetisation + retention (1), C106 Build-along 13 of 14: Retention: streaks, daily summary, notifications (Cat Pomodoro) (1)
- **💾 Save and load** (41 checkpoints): C088 Chapter 44: Options, Key Rebinding (11), C079 Chapter 41: Main Menu, Loading (10), C085 Chapter 43: Options, Audio Levels (9), C057 Chapter 32: Plate Complete Visual (7), C014 Chapter 9: Post Processing (6), C023 Chapter 14: Input System Refactor (6), C015 Chapter 10: Character Controller (5), C031 Game slice: 🐱 Cats R · 🚦 boot + data + save (5) … and 33 more on the page
- **🪟 UI and the safe area** (32 checkpoints): C067 Chapter 36: Delivery Manager UI (28), C058 Chapter 33: Plate World UI Icons (16), C043 Chapter 26: Cutting Progress, World Canvas (14), C094 Chapter 46: Polish (14), C088 Chapter 44: Options, Key Rebinding (11), C008 Chapter 5: Unity Layout (9), C091 Chapter 45: Controller Input, Menu Navigation (8), C004 Chapter 1: Intro, Overview (6) … and 24 more on the page
- **👆 Input** (45 checkpoints): C023 Chapter 14: Input System Refactor (18), C015 Chapter 10: Character Controller (17), C091 Chapter 45: Controller Input, Menu Navigation (12), C039 Chapter 24: Cutting Counter, Interact Alternate (11), C048 Chapter 29: Stove Counter, State Machine (10), C088 Chapter 44: Options, Key Rebinding (9), C018 Chapter 12: Animations (7), C027 Chapter 17: Interact Action, C# Events (7) … and 37 more on the page

## The iPhone step (C111) only

That single mastery step needs a Mac with Xcode and runs there later. Everything else in this journey happens on this machine.

## Honest notes

- The Cats R study kit on this machine is the asset folder of record; every kit path named on the page was checked here on 3 Nov 2026 by the checker.
- Kit checkpoints (L6) say "unzip the package": right-click the zip > Extract All into `D:\GameStudy\journey\kits`; open the kit folder from Unity Hub with exactly the editor version its HANDOFF.md names (2022.3.62f2 and 6000.0.60f1 are installed here).
- Emulators: the kit checkpoints say "on your phone or emulator"; the phone over USB is the one that is set up here.
