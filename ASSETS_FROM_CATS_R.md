# Assets from Cats R (the Cats and Soup reconstruction) for Cat Pomodoro

Read-only inventory, written 2 Nov 2026 by the console from the Cats R repo. Nothing was copied. The Builder copies
exactly what a brief needs into `Assets/CatPomodoro/Art/<Group>/` and writes one ledger line per copied folder in
`ASSET_LEDGER.md`. Private study copies only: never store-published, never shared outside the captain's repos.

## Where the assets live (two copies of the same AssetRipper export, same GUIDs)

| Root | What | Note |
|---|---|---|
| `/Volumes/GameStudy/repos/Cats_Soup_Relaxing/unity_kit/Cats_Soup_Relaxing_Study/Assets/` | the kit project's Assets (the export wired into a Unity project; the one the fleet built against) | **copy from here**; git-ignored in the Cats R repo (Mac volume copy; the box mirror is `D:\GameStudy\...` only if the mirror carried it) |
| `/Volumes/GameStudy/assets/cats_soup_relaxing/export/ExportedProject/Assets/` | the raw AssetRipper export (2.4 GB), identical folder layout and GUIDs | fallback if a file is missing in the kit |
| `/Volumes/GameStudy/assets/cats_soup_relaxing/showcase/CatsShowcase/` | a 150-file Unity showcase (6000.4.11f1) that already plays 8 cats + 4 stations through the game's camera; `Assets/_LocalShowcase/Editor/CatsShowcase.cs` is the proven recipe for lifting the cat rig and skinning it; `_LocalShowcase/Shaders` holds 8 stand-in shaders with the game's shader/property names | read for the recipe; copy the stand-in shaders if the materials render pink |

Both copies have the same type-sorted layout: `AnimationClip/ AnimatorController/ AudioClip/ Avatar/ Font/ GameObject/ Material/ Mesh/ MonoBehaviour/ Resources/ Scenes/ Shader/ Sprite/ Texture2D/ ...`. Counts below are files without `.meta` (kit copy).
Keep the `.meta` next to every copied file so GUID references between prefab, mesh, material, clip and texture survive.

## IMPORTANT FACT (a DIFF against the brief): Cats R cats are 3D skinned rigs, not 2D sprites

The forest is a 3D scene with a pitched camera, and the cats are low-poly **skinned meshes** (`Bip001` biped bones,
`SkinnedMeshRenderer`, Avatar `cat1Avatar`) whose fur/species is a **texture swap** on the body material
(`Resources/body/body0_N.png`). Every cat action is a 3D animation clip in one master Animator Controller, `cat1`
(267 states). There are no 2D cat sprite sheets for the cats (`Sprite/` and `Texture2D/` hold UI, icons and props).
So "3D room with 2D cat sprites" is built in Cat Pomodoro as "3D room with the Cats R 3D cat rig and its clips",
which looks exactly like Cats R. The showcase proves this plays in Unity 6000.x.

## 1. Cats (rig, skins, animation library)

| What | Path (kit copy) | Count | Note |
|---|---|---|---|
| Master cat animator (all actions) | `AnimatorController/cat1.controller` | 1 (267 states) | parameters: finish, offset, airMove, moveType, justRun, speed_Work ... |
| Kitten animator | `AnimatorController/kittycat.controller` | 1 (65 states) | kittens (small cats): poses, sleep, grooming |
| Cat avatars | `Avatar/cat1Avatar.asset`, `Avatar/kittycatAvatar.asset` | 2 | |
| Cat body meshes | `Mesh/cat.asset`, `Mesh/body.asset`, `Mesh/body_0.asset` ... `body_8.asset`, `Mesh/kittycat.asset`, `kittycat_0.asset` | 12 | |
| Cat skeleton prefab | `GameObject/cat1.prefab`, `GameObject/kittycat1.prefab` | 2 | bones only reference 4 GUIDs; the complete rig with renderer + animator is inside the shop previews below |
| **Complete cat rig (proven)** | `Resources/ui/shop/list_goodsSpecial526.prefab`, `Resources/ui/shop/list_goodsSpecial548.prefab` (602 `list_goodsSpecial*.prefab` in the folder) | 602 | the showcase unpacks these, keeps the Animator whose controller is `cat1`, and re-skins it (see `CatsShowcase.ExtractCatRigs` / `Skin`) |
| Fur / species skins | `Resources/body/body0_0.png` ... `body0_88.png` (89), `body1_0.png` ... (16) | 105 | `MonoBehaviour/CatType.asset` row N -> `resName body0_N` (105 cat types; fields fatFactor, earSize, headSize, tail, eyeColor, isKitty, isFirst) |
| Body materials (special) | `Material/body_spring.mat`, `body_eventplace16/33/35.mat` ... | 10 | the plain cats use one body material with the texture swapped |
| Animation clips (whole library) | `AnimationClip/*.anim` | 2,084 | cat clips referenced by `cat1.controller`; station clips referenced by the place prefabs |
| Cat clip families (by file prefix) | `idle_*` 319, `move_*` 288, `special_*` 246, `get_*` 39, `kittycat_*` 32, `mode_*` 23, `jump_*` 16, `streching_*` 7 | | |
| Cat sound set | `MonoBehaviour/CatSFXSoundDatabase.asset` | 1 | |

### Work / activity states in `cat1` (state names, all present in the controller)
cooking: `whip` (soup), `cut`, `slice`, `cut_rotate`, `cut_apple/bamboo/beet/flower/kiwi/memile/watermelon`, `slice_cucumber/pear/pomegranate/skyapple/tabasco/tomato`, `chopping_onion`, `break`, `rolling`, `extract`, `extract2`, `grind`, `grind2`, `grind3`, `mixer`, `fry`, `dish_soup`, `dish_fry`, `dish_juice`, `peeling`, `honey`, `lotus`, `grape_start/grape_repeat/grape_end`, `chew`, `riseCutter`, `cook_start3`
crafts / study: `knitting`, `stitch`, `paint`, `pottery`, `penpal` (letter writing), `flower_arranging`, `gardening`, `telescope`, `telescope_idle`, `netflix` (TV), `guitar`, `karaoke`, `play_timpani`, `diskjockey`, `footpiano_start/idle`, `magic_show`, `workout`, `yoga`, `gymnestics`, `fishing1`, `fishing2`, `grooming_start/grooming/grooming_end`, `teatime`, `drink`, `feed`
rest (Home tab idle activities): `idle0`, `nod`, `sit`, `lay`, `lay2`, `rest`, `doze`, `hammock`, `sunbed_idle`, `swing`, `box_lie`, `box_sleep`, `streching`, `sulk`, `scratcher`, `bath`, `campfire`, `wheel_run`, `catball`, `train_cat`, `car_ride`, `rocking_horse`, `huladance`

### Happy / winning states in `cat1` (for session completion)
`clap`, `level_up`, `giftbox_dance`, `gift`, `surprise`, `firework`, `huladance`, `jump_start` -> `jump_finish`, `nod`, `cat1_jazzparade`, `dance` (clip `AnimationClip/dance.anim`), `show_complete` (clip `AnimationClip/show_complete.anim`).

### The 10 candidate cats (use first)
CatType rows 0..9 = skins `Resources/body/body0_0.png` ... `Resources/body/body0_9.png` (the first ten species in the game's own order; row 0 is the starter white cat). Rig from `Resources/ui/shop/list_goodsSpecial526.prefab` (showcase-proven), skin by swapping the body material's main texture. Starter owned subset suggestion: rows 0, 1, 2; the shop sells 3..9.

## 2. Stations and work types (the "places")

Work type N in `MonoBehaviour/Work.asset` (179 rows: fields name, workAnim, completeTime, type 1 cooking / 2 prep / 3 rest / 4 facility, cost ...) is drawn by the prefab `Resources/places/placeN.prefab` (168 `place*.prefab` + 50 `eventPlace*.prefab` + 17 vehicles / event rides = 250 files). 96 of the place prefabs carry their own legacy `Animation` clips (the station's moving parts); 6 embed a cat rig (`place24`, `place36`, `place87`, `place107`, `place405`, `place603`). Station skins: `MonoBehaviour/WorkSkin.asset`, `TimerSkin.asset`, `SignSkin.asset`; station 3D meshes `Mesh/prop_*` (667), `Mesh/tool_*` (96), `Mesh/pan_*` (48), `Mesh/mixer_*` (38), `Mesh/timer_*` (33), `Mesh/juice_*` (14).

### The 10 candidate work types (use first)

| Work row | Game name (Korean -> English) | cat1 work state | Station prefab | Station's own clips |
|---|---|---|---|---|
| 0 | 스프 끓이기 Soup boiling | `whip` | `Resources/places/place0.prefab` | creat, rot_y_slow, woodstock |
| 1 | 당근 썰기 Carrot slicing | `cut` | `Resources/places/place1.prefab` | (static) |
| 4 | 양배추채 썰기 Cabbage shredding | `slice` | `Resources/places/place4.prefab` | (static) |
| 7 | 쥬스 만들기 Juice making | `mixer` | `Resources/places/place7.prefab` | creat, pod_juice, rot_y, woodstock |
| 13 | 도토리 까기 Acorn cracking | `break` | `Resources/places/place13.prefab` | (static) |
| 17 | 볶음 만들기 Stir-fry | `fry` | `Resources/places/place17.prefab` | creat, frypan, woodstock |
| 19 | 레몬즙 짜기 Lemon squeezing | `extract` | `Resources/places/place19.prefab` | extractor |
| 26 | 그림 그리기 Painting | `paint` | `Resources/places/place26.prefab` | paper |
| 37 | 포도즙 짜기 Grape pressing | `grape_start` / `grape_repeat` / `grape_end` | `Resources/places/place37.prefab` | grape_juice_up_0, grape_juice_repeat2, grape_juice_down_0 |
| 64 | 뜨개질 Knitting | `knitting` | `Resources/places/place64.prefab` | knitting, knitting_0, place64_idle |

Good second picks: 28 Busking `guitar` (place28), 50 Telescope `telescope` (place50: deskplanetarium_move, telescope_move), 62 DJ `diskjockey` (place62), 21 Honey `honey` (place21), 9 Corn `rolling` (place9), 16 Broccoli `cut_rotate` (place16). Showcase-proven stations: `place24`, `place107`, `eventPlace35`, `eventPlace16`.

## 3. Forest / room (3D scene, ground, trees, furniture)

| What | Path (kit copy) | Count |
|---|---|---|
| The forest scene of record (camera, lights, layout) | `Scenes/main.unity` (10 MB, 1,210 asset refs); boot scenes `Scenes/load.unity`, `CnS/01_Scenes/...` | 8 scenes |
| Camera recipe (from the showcase, matches the game) | pitch 30 deg, orthographic, background RGB(0.427, 0.557, 0.329), flat white ambient, one directional light at (50, -30, 0) | |
| Ground tiles | `Resources/ground/ground_back*.png`, `ground_base*.png` | 55 |
| Trees (per theme) | `Resources/tree/*.png` + `.asset` (sprite assets) | 281 png |
| Forest size / deco blocks | `Resources/places/decoblock30xx.prefab`, `Resources/ui/forestsize/`, `Resources/ui/forestdecoblock/` | 15 + 6 + 5 |
| Furniture prefabs (decor the player places) | `Resources/furniture/*.prefab` | 1,397 |
| Lamps, props, ambience | `Resources/lamp/` 123, `Resources/towerprops/` 1,005 (cat tower rooms: `default_floor/wall`, `prop_*`), `Resources/balloon/` 28, `Resources/startree/` 10 | |
| Materials / meshes / shaders | `Material/` 1,598, `Mesh/` 3,458, `Shader/` 103 (AssetRipper placeholders; use the showcase stand-ins) | |
| NPC animals (optional forest life) | `AnimatorController/{bird0,bird05,gull2,penguin,redpanda,sloth,fairy,...}.controller`, `Resources/npc/` 112, `Avatar/*Avatar.asset` | |

## 4. UI sprites and fonts

The game's UI is NGUI (atlases in `Resources/nguiatlas/atlas_*.png` + `.mat`, 57 files; `Resources/nguiminigameatlas/` 36). Cat Pomodoro uses uGUI, so copy PNGs / sliced sprites, not the NGUI prefabs.

| What | Path (kit copy) | Count |
|---|---|---|
| UI textures by purpose | `Resources/images/ui/{common,shop,build,itemicon,propicon,equipicon,eventicon,fishicon,skillicon,tower,ef}/` | 9,280 |
| UI prefabs (layout reference only) | `Resources/ui/{commonpopup,facility,shop,guide,notice,emoticon,banner,kittyhouse,...}/` | 1,336 |
| Sprites (sliced) | `Sprite/` | 3,953 |
| Fonts | `Font/` : Anton, Bangers, Lato-Regular, Oswald-Bold, Roboto-Bold, LiberationSans, NotoSansJP/SC/TC, NanumBarunpenR, maple_lit, Mali-Medium_lit, Garuda, tour, Electronic Highway Sign | 16 |
| Cat / coin / shop icons | `Texture2D/bt_catplay.png`, `bg_cat.png`, `bg_catBox.png`, `Resources/machine/ball_coin.png`, `ball_gold.png`, `ball_gem.png` | |

## 5. Audio

`AudioClip/*.ogg` (203). Useful: BGM `001_field_07.ogg`, `001_Forest Night Time 3.ogg`, `005_field_05.ogg`, `015_field_09_catsSong.ogg`, `016_field_10_springBgm2.ogg`, `024_field_07_orchestra.ogg`; menu `002_menu_03.ogg`, `003_menu_02.ogg`; coins `002_Coins 2.ogg`, purchase `003_209578__zott820__purchase.ogg`; completion `017_397355__plasterbrain__tada-fanfare-a.ogg`, `Congrats Bell 01/02/03.ogg`, `004_Collect star 5.ogg`; timer `016_554056__gronkjaer__clockbeep.ogg`, `006_Notification sound 15.ogg`; cat purr `007_260881__trautwein__cat-purr.ogg`, `042_purring.ogg`; work foley `002_Chopping wood 1.ogg`, `009_Cutting on cutting board 3.ogg`, `010_Meat sizzling on frying pan 1.ogg`, `005_512763__tosha73__working-mixer.ogg`, `001_Water Boiling Loop 4.ogg`; mixer `AudioMixerController/`.

## 6. Data tables that explain the content (read, do not copy as-is: values are obfuscated with ACTk, read the `fakeValue` field)
`MonoBehaviour/CatType.asset` (105 cats), `Work.asset` (179 work types), `WorkSkin.asset`, `EventWork.asset`, `EventPlace.asset`, `CatUnlock.asset`, `CatGift.asset`, `Equip.asset` (costumes: `Resources/equip/acce*.prefab` 1,506), `Shop.asset`.
Game source for reference (never copied into Cat Pomodoro): `Scripts/Assembly-CSharp/` (2,011 files), the fleet's exact C# at `/Volumes/GameStudy/repos/Cats_Soup_Relaxing/src_cs/`.
