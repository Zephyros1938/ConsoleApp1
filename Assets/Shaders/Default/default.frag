#version 330 core
out vec4 FragColor;

in float tileID;
in vec2 texCoord;

uniform sampler2D solid;
uniform sampler2D solidSpecular;
uniform sampler2D solidNormal;
uniform sampler2D transparent;
uniform sampler2D transparentSpecular;
uniform sampler2D transparentNormal;

vec2 textureDimensions = vec2(8., 10.);
float textureTransparentID = 80.;

// Tile coordinates stored as a 2D array of vec2 (x, y)
// The index of the tile is the ID of the tile, refer to the Tiles struct in the C# code for the names of each tile.
// You should only edit these if the size of the tilemap changes.
// The coordinates listed below are the bottom-left texture coordinate of each tile.
// These coordinates are used to calculate the texture coordinates of the tile in the texture atlas, do not modify them without expecting to break the texture mapping.
const vec2 tileCoords[80] = vec2[](
    // Row 0
    vec2(0., 0.), vec2(1., 0.), vec2(2., 0.), vec2(3., 0.), vec2(4., 0.), vec2(5., 0.), vec2(6., 0.), vec2(7., 0.),
    // Row 1
    vec2(0., 1.), vec2(1., 1.), vec2(2., 1.), vec2(3., 1.), vec2(4., 1.), vec2(5., 1.), vec2(6., 1.), vec2(7., 1.),
    // Row 2
    vec2(0., 2.), vec2(1., 2.), vec2(2., 2.), vec2(3., 2.), vec2(4., 2.), vec2(5., 2.), vec2(6., 2.), vec2(7., 2.),
    // Row 3
    vec2(0., 3.), vec2(1., 3.), vec2(2., 3.), vec2(3., 3.), vec2(4., 3.), vec2(5., 3.), vec2(6., 3.), vec2(7., 3.),
    // Row 4
    vec2(0., 4.), vec2(1., 4.), vec2(2., 4.), vec2(3., 4.), vec2(4., 4.), vec2(5., 4.), vec2(6., 4.), vec2(7., 4.),
    // Row 5
    vec2(0., 5.), vec2(1., 5.), vec2(2., 5.), vec2(3., 5.), vec2(4., 5.), vec2(5., 5.), vec2(6., 5.), vec2(7., 5.),
    // Row 6
    vec2(0., 6.), vec2(1., 6.), vec2(2., 6.), vec2(3., 6.), vec2(4., 6.), vec2(5., 6.), vec2(6., 6.), vec2(7., 6.),
    // Row 7
    vec2(0., 7.), vec2(1., 7.), vec2(2., 7.), vec2(3., 7.), vec2(4., 7.), vec2(5., 7.), vec2(6., 7.), vec2(7., 7.),
    // Row 8
    vec2(0., 8.), vec2(1., 8.), vec2(2., 8.), vec2(3., 8.), vec2(4., 8.), vec2(5., 8.), vec2(6., 8.), vec2(7., 8.),
    // Row 9
    vec2(0., 9.), vec2(1., 9.), vec2(2., 9.), vec2(3., 9.), vec2(4., 9.), vec2(5., 9.), vec2(6., 9.), vec2(7., 9.)
    // Row 10+ (if needed, extend the pattern here)
);

// Function to get the bottom-left corner of a tile by ID
vec2 getTileCoords(float ID) {
    return tileCoords[int(mod(floor(ID+.5),80.0))];
}

// Calculates the texture coordinates for tile on the tilemap
vec2 getTile(float ID)
{
    
    // Return the correct texture coordinates for the current tile
    return (getTileCoords(ID) / textureDimensions) + (texCoord / textureDimensions);
}

void main()
{
    vec2 targetTile = getTile(tileID);
    if(tileID >= textureTransparentID)
    {
        FragColor = texture(transparent, targetTile);
        //FragColor = vec4(1./tileID, 0.0, 0.0, 1.0);
    }
    else
    {
        FragColor = vec4(texture(solid, targetTile).xyz, 1.);
        //FragColor = vec4(0.0,1./tileID, 0.0, 1.0);
    }
    
}