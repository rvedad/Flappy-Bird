# Flappy Bird

A remake of *Flappy Bird* built in **Unity 6**, playable on **Android** and **Windows**. Tap to flap, fly through the gaps, and chase your high score.

---

## Features

- **One-button gameplay** — tap, click or press Space to flap
- **Start screen** with the bird hovering until the first tap
- **Endless procedural pipes** with random gap positions
- **Difficulty ramp** — pipes speed up and spawn more often as your score climbs, up to a set limit
- **Day/night cycle** — the background fades between day and night every 10 points
- **Score and high score** shown with pixel-art digit sprites
- **Saved high score** that persists after closing the game
- **Gold score flash** when you beat your high score
- **Death animation** — the bird flashes, bounces, spins and falls
- **Tap to restart** after a short delay, so you can't restart by accident
- **Sound effects** for flapping, scoring, hitting and dying
- **Wing animation** and tilt based on vertical speed

---

## Controls

| Platform | Flap / Start / Restart |
|---|---|
| Android | Tap the screen |
| Windows | `Space` or left mouse click |

---

## Game States

The game runs on three states managed by `GameManager`:

```
Waiting  →  bird hovers, ground scrolls, no pipes
Playing  →  normal gameplay, pipes spawn, score counts
Dead     →  everything freezes, game over and high score shown
```

---

## Project Structure

```
Assets/
├── Scenes/
│   └── FlappyBird
├── Scripts/
│   ├── GameManager.cs      Game states, score, high score, restart (singleton)
│   ├── BirdController.cs   Flapping, hovering, animation, rotation, death
│   ├── PipeSpawner.cs      Spawns pipes and handles the difficulty ramp
│   ├── PipeController.cs   Pipe scrolling, scoring trigger, cleanup
│   ├── GroundScroller.cs   Infinite scrolling ground with two tiles
│   ├── ScoreDisplay.cs     Draws numbers using digit sprites
│   └── DayNightCycle.cs    Fades between day and night backgrounds
├── Prefabs/
│   └── Pipe
├── Sprites/
│   ├── Birds/
│   ├── Pipes/
│   ├── Background/
│   ├── Ground/
│   └── UI/
└── Audio/
```

---

## Running the Project

1. Open the project in **Unity 6** (Unity Hub → Add → select the project folder).
2. Open `Assets/Scenes/FlappyBird`.
3. Set the Game view to a **1080 × 1920** portrait resolution.
4. Press **Play**.

---

## What I Learned

- Gravity-based movement with Rigidbody2D
- Procedural generation with prefabs and `Instantiate`
- Infinite scrolling by recycling two ground tiles
- Game states with an `enum`
- Sprite animation from code
- Smooth rotation and fades with `Mathf.LerpAngle` and `Mathf.SmoothStep`
- Sorting layers for 2D draw order
- Saving data with `PlayerPrefs`
- Script execution order pitfalls, and fixing them with lazy initialization
- Building for Android and Windows, and creating a Windows installer

---

## Credits

- Built by **Vedad**
- Sprites and sound effects: **https://github.com/samuelcust/flappy-bird-assets**
- Based on the original *Flappy Bird* by Dong Nguyen (.GEARS Studios, 2013).
  This is a non-commercial fan remake made for learning purposes.
