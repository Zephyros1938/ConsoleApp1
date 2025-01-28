#version 330 core
out vec4 FragColor;

flat in int tileID;
in vec2 texCoord;

uniform sampler2D solid;
uniform sampler2D solidSpecular;
uniform sampler2D solidNormal;
uniform sampler2D transparent;
uniform sampler2D transparentSpecular;
uniform sampler2D transparentNormal;
uniform int debug;

int textureTransparentID = 80;

void main()
{
    int ID = tileID;
    if(ID==-1){
        FragColor = vec4(0.,0.,0.,0.);
    }
    else {
        float x = mod(float(mod(ID, 80)), 8.0);
        float y = floor(float(mod(ID, 80)) / 8.0);
        if(ID>=80)
        {
            FragColor = texture(transparent, (texCoord + vec2(x,y)) * vec2(0.125,0.1));
        }
        else
        {
            FragColor = texture(solid, (texCoord + vec2(x,y)) * vec2(0.125,0.1));
        }
    };
    if(debug==1)
    {
        FragColor = vec4(texCoord.x, texCoord.y, 0., 1.);
    }
    //FragColor = vec4(texCoord.x, texCoord.y, 0. ,1.);
}