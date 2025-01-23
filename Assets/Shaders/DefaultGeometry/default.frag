#version 330 core
out vec4 FragColor;

in vec2 texCoord;

uniform sampler2D solid;
uniform sampler2D solidSpecular;
uniform sampler2D solidNormal;
uniform sampler2D transparent;
uniform sampler2D transparentSpecular;
uniform sampler2D transparentNormal;

void main()
{
    FragColor = vec4(texCoord.x, texCoord.y,0.,1.);
}