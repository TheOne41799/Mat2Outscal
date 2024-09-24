[![Watch the video](https://img.youtube.com/vi/F7qt3QWeDvA/maxresdefault.jpg)](https://www.youtube.com/watch?v=F7qt3QWeDvA&t=17s)

# 🔦 DIM - Top-Down 2D Shooter

A dark, atmospheric top-down shooter where light is as important as health! Keep shooting and stay alive before the light dims out completely.

---

## 🕹️ Game Features

### 🎮 Core Mechanics:
- **MVC Pattern**: Implemented for the player, enemies, gun, bullets, shells, and UI.
- **Singletons**:
    - **UIController**
    - **GameManager**
    - **InputManager**
    - **SoundManager**

### 🎯 Controls:
- **Movement**: 
    - Arrow keys or WASD for moving in any direction.
- **Aiming**: 
    - Use the **mouse position** to aim.
- **Shooting**: 
    - Left-click to shoot.
    - **Right-click** to switch gun modes.
- **Pause and Restart**: 
    - Press **Escape** to pause the game.

### 💡 Unique Mechanics:
- **Health & Light Management**:
    - The player has both **Health** and **Dim Value** (light radius).
    - If either **health** reaches 0 or the **light radius** shrinks to 0, the game is over.
    - Maintain light to stay alive even if you have health left!

### 🔫 Player Features:
- **Gun Modes**:
    - **Single Shot** (maximum damage)
    - **Burst Fire** (medium damage)
    - **Full Auto** (lowest damage)
- **Reload System**:
    - **Manual** reload with `R` or automatic when ammo runs out.
- **Gun Recoil**:
    - Dynamic recoil system based on gun mode.
- **Shell Ejection**:
    - Ejected shells add a **3D illusion** using:
        - **Random force**, **direction**, and **angular velocity**.

### 👾 Enemy Features:
- **3 Types of Enemies**:
    - Each enemy has **varying health** and **different damage values**.
- **Player Tracking**:
    - Enemies calculate player positions using **Action Events**.
    - A **static class** broadcasts player positions, enemy deaths, light radius changes, etc.
- **Enemy Spawner**:
    - Separate spawner system with customizable spawn rates.

### ⚡ Additional Game Features:
- **Mouse Crosshair** for precise shooting.
- **2D URP Lighting System**:
    - The environment features **sprite-lit backgrounds** with shadow casters for obstacles.
- **Visual Feedback**:
    - **Muzzle effects** using lights and 2D sprites.
    - Player and enemy sprites **flash** when hit.
    - **Particle systems** that collide with obstacles.
    - **Trail renderer** for bullets.
- **Coroutines** for timed events like reloading, powerups, etc.
- **UI Updates**:
    - Gun mode, ammo count, player health, and **Dim Value** displayed dynamically.
- **Score System**:
    - Score and high score saved with `PlayerPrefs`.
- **Customizable Variables**:
    - Every variable (ammo count, health, enemy stats, etc.) is customizable through the inspector. **No hardcoded values!**
- **Prefabs**:
    - Every object (player, enemies, bullets, shells, etc.) is modular and created as prefabs.

---

## 🖼️ Screenshots

### In-Game Shooting Mechanics with Shell Ejection
![image alt](https://github.com/TheOne41799/Mat2Outscal/blob/main/Mat%20II%20Project/Assets/Screenshots/Screenshot%2001.png?raw=true)

### Health and Dim Value UI in Action
![image alt](https://github.com/TheOne41799/Mat2Outscal/blob/main/Mat%20II%20Project/Assets/Screenshots/Screenshot%2002.png?raw=true)

### Game Over Screen with High Score
![image alt](https://github.com/TheOne41799/Mat2Outscal/blob/main/Mat%20II%20Project/Assets/Screenshots/Screenshot%2003.png?raw=true)

---

## 🎵 Music Track
RoleMusic - Juglar Street  
https://freemusicarchive.org/music/Rolemusic

