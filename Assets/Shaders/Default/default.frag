#version 330 core
out vec4 FragColor;

in vec2 texCoord;

uniform sampler2D texture0;

uniform vec2 texSizes;
uniform uint texIndice;

void main()
{
    float x = mod(texIndice, texSizes.y);
    float y = floor(texIndice / texSizes.x);
    
    FragColor = texture(texture0, texCoord * vec2(x,y));
}