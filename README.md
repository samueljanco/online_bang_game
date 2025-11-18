# Online Multiplayer BANG! – Console Edition

This application is an online multiplayer game based on **BANG!**, a Wild West–themed deduction card game. Since meeting in person with several players can be difficult (especially during lockdowns), this project provides an online alternative that preserves the feel and flow of the original game.

---

## 🖥️ Environment & Requirements

**Development Environment:**  
- Visual Studio 2019 for Mac

**Tested System Specifications:**  
- **CPU:** 2.3 GHz Dual-Core Intel Core i5  
- **Memory:** 8 GB 2133 MHz LPDDR3  
- **OS:** macOS Big Sur 11.2.2  

---

## 🚀 How to Run the Application

1. Open the project in **Visual Studio 2019 for Mac**.  
2. Build and run the project.  
3. When the application starts, choose whether you are the **host** or a **guest**.

   - Only **one player** can act as the host.
   - The host selects the total number of players (**4–7**).

4. Once the server is running, other players can start the program and enter their nicknames.

5. The application will tell each player how to resize their console window so the game renders correctly.

---

## 🎮 How to Play

### 1. Choose a Character
Each player receives two random characters and chooses one by typing the corresponding **number**.

### 2. Gameplay Flow
- The **Sheriff** always takes the first turn.
- At the start of a turn, the game automatically draws two cards for the player (unless their character’s ability modifies this).
- Players perform actions by typing specific commands (move-text).

---

## 📝 List of Actions (Input Format)

| Action | Command Format |
|-------|----------------|
| Beer | `Beer` |
| Cat Balou | `CatBalou Name-Of-Target` |
| Bang | `Bang Name-Of-Target` |
| Stagecoach | `Stagecoach` |
| Wells Fargo | `WellsFargo` |
| Duel | `Duel Name-Of-Target` |
| Emporio | `Emporio` |
| Salon | `Salon` |
| Indians | `Indians` |
| Gatling | `Gatling` |
| Missed | `Missed` |
| Panic | `Panic Name-Of-Target` |
| Jail | `Jail Name-Of-Target` |
| Mustang | `Mustang` |
| Scope | `Scope` |
| Barrel | `Barrel` |
| Dynamite | `Dynamite` |
| Volcanic | `Volcanic` |
| Schofield | `Schofield` |
| Remington | `Remington` |
| Rev. Carabine | `RevCarabine` |
| Winchester | `Winchester` |
| End Move | `EndMove` |
| Throw Away Card | `ThrowAway` |

---

## ⚔️ Special Interaction Rules

### When targeted by **Bang**
The player chooses:
- Play a **Missed** card → type `Missed`  
- Lose one life point → type `lose life`

### When targeted by **Duel**
The player chooses:
- Play a **Bang** card → type `Bang`  
- Lose one life point → type `lose life`

---

## 📜 Game Rules

The game follows the same rules as the physical BANG! card game.  
For full rule details, consult the official rules here:

https://www.ultraboardgames.com/bang/gamerules.php

---

## 💀 Player Death & End of Game
- When a player dies, they remain in the game as a spectator.
- After the game ends, the application reveals:
  - The winner  
  - Each player’s role  

---
