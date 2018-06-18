# ImageLevel

Just a simple tool for generating tilemap-like 2D levels from a Texture2D/Sprite Image. It also works with 2.5D levels made with 3D prefabs.



### Why

I was looking for an easy & faster way to create lots of levels for 2D games, avoiding the use of 3rd party tools or importers like Tiled, which add some complexity to the Scenes, and make them more script-dependant. 

What's the fastest & easiest tool for an Artist to design levels? A painting program.  ;-)

This tool aims for keeping everything "vanilla", with no additional scripts required. Easy to remove and maintain your project clean.

Great for JAM's and quick prototypes.



### Author

Just me.



### Current Features

- Wizard #1 creates a palette of Tiles*(color+prefab)* for every color found.
- Wizard #2 reads from a Sprite and generates the Tilemap level.
- Wizard #3 extracts shapes from CompositeCollider2D and creates several colliders.
- **NEW:** Option toggle to ignore colliders generation. Useful for backgrounds or painting tiles.
- Supports Multiple Sprites packed within an Atlas.
- Allows to take special Tiles *(animated/Player/Dynamics)* apart from the common ones *(Statics)*.
- Marks the desired tiles as *Static* GameObjects for latter optimization.
- Auto-generates a *CompositeCollider2D* for the *Statics* group.



### Planned features

- 9-Slice like rules, to have nice borders according to your tilemap.
- Combine Statics into a single GameObject *(WIP, be patient, please)*.



### Expansion possibilites (non-planned)

You can adapt the whole thing to support 3D levels.

You could also remove the empty-tile mask, in order to include background tiles, but I don't recommend my tool for this purpose. The ideal thing is to have as few GO's as possible...



### Usage

1. Use tool #1 for generating LevelPalettes from you Levels, stored as Texture2D.
2. Use tool #2 to generate the Tilemap from a Sprite and a global LevelPalette.
3. use tool #3 to break the default CompositeCollider into a few, simpler colliders.

*(**NOTE**: All levels packed within an atlas SHOULD share same color for the same type of Tile)*

*(**NOTE**: Also, you should provide your tiles with their own Collider already, unless you want a background)*