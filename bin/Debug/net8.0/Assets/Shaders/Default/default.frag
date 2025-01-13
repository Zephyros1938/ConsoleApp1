#version 330 core
out vec4 FragColor;

in vec2 texCoord;

uniform sampler2D texture0;

uniform vec2 texSizes;
uniform float texIndice;

void main()
{
    float x = floor(texIndice / texSizes.x);
    float y = mod(texIndice, texSizes.y);

    vec2 pos = vec2((1.0/texSizes.x)*x,(1.0/texSizes.y)*y);
    vec2 texCoordMag = texCoord*(vec2(1.0,1.0)/texSizes);
    
    FragColor = texture(texture0, texCoordMag+pos);
}