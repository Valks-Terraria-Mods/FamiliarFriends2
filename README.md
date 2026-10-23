# FamiliarFriends2
## What is this?
Imagine if you had a bot that looked exactly like a player that followed you around in multiplayer or singleplayer. A bot that could build, mine and defend you. Imagine if this player had an inventory just like you, was able to pickup items and use them based on which ever was the best. Imagine being able to fully customize this player and having them fight alongside you during tough boss fights. Your no longer just upgrading your gear, but also your bot friends gear. That is the idea of this mod.

## Seeing what was possible
Development of this has started by myself half a year ago. I used AI to develop prototypes to see what was possible then fully scrapped those prototypes and started writing them from the ground up as 100% a human. I have not worked on this for half a year due to IRL stuff going on but I would like to revive the project.

## Full navigation of entire world not practical
During development of one of the prototypes (with AI) I wanted to see how feasible it would be to have the player bot navigate the entire world from the far left side of the world to the far right side of the world. Navigating every jump using a navigation graph. Although while it was working, the performance was awful and the project got way too complex. I'm sure it could be done (dividing the world navigation into chunks / asynchronous background threads) but this is way too complex at least for me. 

## Line of sight navigation
So I've decided that these player bots will only navigate the world of what's in front of them. They will be dumb in the sense that they will only move towards the player. Jump if there is a hole in front of them. Use a rope if the player is above them. 

## Precise realtime config editing
Due to the nature of getting the perfect bot walking / sprinting speeds, I have created a mod that allows you to edit config settings in realtime which should speed up development. <https://steamcommunity.com/sharedfiles/filedetails/?id=3719994375>

## Source code
Here is the old experimental prototype repo. Fully written by AI. 
<https://github.com/Valks-Terraria-Mods/FamiliarFriends>

Here is the new repo. None of this was written by AI. This is where all contributors should go. Only use the other repo as reference. 
<https://github.com/Valks-Terraria-Mods/FamiliarFriends2>

## My view on AI
You should not use AI to generate an entire mod as complex as this. The AI can make mistakes, things can easily go out of scope. Code can easily become very hard to make sense of. But if you want to see what is possible from a glance, by all means, do it in a separate project. If your completely lost on how to make for example a radial menu, see how the AI does it. Then use that as reference but do not copy that code line by line. In other words don't be lazy. Write the code yourself. Use AI as a reference if needed but do all the heavy lifting yourself. Or even better look up a YT video on how to make a radial menu or look at the wikipedia for the math formulas you need.

## Scope / Roadmap
The following has been figured out already:
- Bot looks like another player.
- Bot can walk naturally towards the player.
- Bot character creation UI almost done.

The following is what needs to be done:
- Basic navigation. Jumping over holes that are in front of the bot. Avoiding lava. Using ropes, minecarts. Teleporting to the player if their too far away. (See above for why full world navigation is not feasible.)
- Bot inventory. Picking up items. Using those items. Attacking nearby hostiles.
- Creating an API to use between this mod and WeaponOutLite to actually see weapons / tools being held by the bots.

## Recruitment
I am looking for C# programmers that are willing to get their feet wet in what can easily turn into a very complex mod. All skill levels are welcome, doesn't matter if you've never seen C# before, I'm willing to help you learn it.

I don't have much motivation to work on this but that would quickly change if others joined. I really don't want this project to die. Offline friends that can do anything any other player can do is just such a cool idea to me.

Here are some pics of what the character creator looks like on the non-AI repo. This is what will be used to customize the look of the bot. It is still very much a WIP.

<img width="909" height="539" alt="image" src="https://github.com/user-attachments/assets/55f16865-e0c8-46f9-ba61-34f456bc7f7f" />

<img width="869" height="683" alt="image" src="https://github.com/user-attachments/assets/d9d2e718-0eb5-4ef3-9630-aed3eb4f4e06" />

<img width="904" height="531" alt="image" src="https://github.com/user-attachments/assets/7dccecab-e09e-4e19-a8d0-f7b4f9835c5b" />

<img width="869" height="683" alt="image" src="https://github.com/user-attachments/assets/2a5d6953-3bbb-4e7e-b46c-96a408aa8ea5" />

The current non-AI repo (this repo) doesn't have much implemented. The bot can walk left and right but will immediately get stuck on any obstacle that is 1 tile in height in front of it. There are several config options right now for tweaking its walk movement.

<img width="821" height="262" alt="image" src="https://github.com/user-attachments/assets/4c270a4b-af24-4e6c-9769-7373950a9d8f" />

You can spawn a friend if you configure a keybind. The character creation UI will popup when you press this keybind. Then you will be able to click "Spawn Friend" in this UI. 

<img width="778" height="95" alt="image" src="https://github.com/user-attachments/assets/1399e65f-e511-4148-9990-6629bcb9c350" />

[Movement.webm](https://github.com/user-attachments/assets/0cfddb52-59ad-4728-8e20-3dcbe781f307)

As you can see friend movement as of right now is bare bones. I want to take development slow, with no help of AI, so we can all understand the code, and build a strong foundation for what's to come. 




