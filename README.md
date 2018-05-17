# ImageLevel

Just a simple tool for generating tilemap-like 2D levels from a Texture2D/Sprite Image.



### Why

I was looking for an easy & faster way to create lots of levels for 2D games, avoiding the use of 3rd party tools or importers like Tiled, which add some complexity to the Scenes, and make them more script-dependant. 

What's the fastest & easiest tool for an Artist to design levels? A painting program.  ;-)

This tool aims for keeping everything "vanilla", with no additional scripts required. Easy to remove and maintain your project clean.

Great for JAM's and quick prototypes.



### Author

Just me.



### Current Features

- Wizard creates a palette of Tiles*(color+prefab)* for every color found.
- Wizard reads from a Sprite and generates the Tilemap level.
- Supports Multiple Sprites packed within an Atlas.
- Allows to take special Tiles *(animated/Player/Dynamics)* apart from the common ones *(Statics)*.
- Marks the desired tiles as *Static* GameObjects for latter optimization.
- Auto-generates a *CompositeCollider2D* for the *Statics* group.



### Planned features

- Extra Tool : Extract the *CompositeCollider2D* point data and dump it into an empty GameObject.
- A toggle option in the Wizard to allow collider generation or not, for just decoration.
- 9-Slice like rules, to have nice borders according to your tilemap.
- Combine Statics into a single GameObject *(dunno how, atm)*.



### Expansion possibilites (non-planned)

You can adapt the whole thing to support 3D levels.

You could also remove the empty-tile mask, in order to include background tiles, but I don't recommend my tool for this purpose. The ideal thing is to have as few GO's as possible...

Convert to native Tilemap *(Unity2D)* structure, so you end with a single GameObject per level. I've removed this planned feature because it creates more garbage in your project. 

A 9-Slice feature would replace this last one...



### Usage

1. Use tool #1 for generating LevelPalettes from you Levels, stored as Texture2D.
2. Use tool #2 to generate the Tilemap from a Sprite and a global LevelPalette.

*(**NOTE**: All levels packed within an atlas SHOULD share same color for the same type of Tile)*

*(**NOTE**: Also, you should provide your tiles with their own Collider already)*