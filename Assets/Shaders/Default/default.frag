#version 330 core
out vec4 FragColor;

in vec2 texCoord;

uniform vec3 texInfo;

uniform sampler2D solid;
uniform sampler2D solidSpecular;
uniform sampler2D solidNormal;
uniform sampler2D transparent;
uniform sampler2D transparentSpecular;
uniform sampler2D transparentNormal;

vec2 textureDimensions = vec2(8., 10.);
float textureTransparentID = 80.0;

vec2 getTile()
{
    return (texInfo.yz / textureDimensions) + (texCoord / textureDimensions);
}

void main()
{
    if(texInfo.x >= textureTransparentID)
    {
        FragColor = texture(transparent, (texInfo.yz + texCoord) / textureDimensions);
    }
    else
    {
        FragColor = texture(solid, (texInfo.yz + texCoord) / textureDimensions);
    }
}