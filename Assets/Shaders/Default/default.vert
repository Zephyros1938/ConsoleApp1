#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in int aTileID;

out vec2 texCoord;
flat out int tileID;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    texCoord = aTexCoord;
    tileID = aTileID;
    gl_Position = vec4(aPosition, 1.0) * model * view * projection;
}