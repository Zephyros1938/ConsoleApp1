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
uniform int debug;

float textureTransparentID = 80.;

void main()
{
    float ID = tileID;
    float x = mod(ID, 8.0);
    float y = floor(ID / 8.0);
    FragColor = texture(solid, (texCoord + vec2(x,y)) * vec2(0.125,0.1));
    if(debug==1)
    {
        FragColor = vec4(texCoord.x, texCoord.y, (x+y)/2.0, 1.0);
    }
}