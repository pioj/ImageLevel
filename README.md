# ImageLevel

Just a simple tool for generating tilemap-like 2D levels from a Texture2D/Sprite Image.



### Why

I was looking for an easy & faster way to create lots of levels for 2D games, avoiding the use of 3rd party tools or importers like Tiled, which add some complexity. 

What's the fastest & easiest tool for an Artist to design levels? A painting program.  ;-)

Great for JAM's and quick prototypes.



### Author

Just me.



### Current Features

- Wizard creates a palette of Tiles(color+prefab) for every color found.
- Wizard reads from a Sprite and generates the Tilemap level.
- Support for Multiple Sprites within an Atlas.



### Planned features

- Group common tiles & separate them from the special ones *(player, enemies, etc.)*
- Create & Add unified colliders for the common tiles.
- Convert to native Tilemap *(Unity2D)* structure, so you end with a single GameObject per level.



### Expansion possibilites (non-planned)

You can adapt the whole thing to support 3D levels.

You could also remove the empty-tile mask, in order to include background tiles, but I don't recommend my tool for this purpose. The ideal thing is to have as few GO's as possible...



### Usage

1. Use tool #1 for generating LevelPalettes from you Levels, stored as Texture2D.
2. Use tool #2 to generate the Tilemap from a Sprite and a global LevelPalette.

*(**NOTE**: All levels packed within an atlas SHOULD share same color for the same type of Tile)*
