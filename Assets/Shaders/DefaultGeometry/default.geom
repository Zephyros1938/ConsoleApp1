#version 330 core
layout (triangles) in;
layout (triangle_strip, max_vertices = 36) out;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform float blockSize;

out vec2 texCoord;

const vec4 offsets[8] = vec4[8](
    vec4( 1.0,  1.0,  1.0,  0.0), //+++ 0
    vec4( 1.0,  1.0, -1.0,  0.0), //++- 1
    vec4( 1.0, -1.0,  1.0,  0.0), //+-+ 2
    vec4( 1.0, -1.0, -1.0,  0.0), //+-- 3
    vec4(-1.0,  1.0,  1.0,  0.0), //-++ 4
    vec4(-1.0,  1.0, -1.0,  0.0), //-+- 5
    vec4(-1.0, -1.0,  1.0,  0.0), //--+ 6
    vec4(-1.0, -1.0, -1.0,  0.0)  //--- 7
);

const vec2 texCoords[4] = vec2[](
    // Top-right
    vec2(1.0, 1.0), //++ 0
    // Bottom-right
    vec2(1.0, 0.0), //+- 1
    // Bottom-left
    vec2(0.0, 0.0), //-- 2
    // Top-left
    vec2(0.0, 1.0)  //-+ 3
);

const mat2x3 faceOffsetIndexes[12] = mat2x3[](
    //top face
    mat2x3(
        0, 1, 4,
        0, 1, 3
    ),
    mat2x3(
        1, 5, 4,
        1, 2, 3
    ),
    //bottom face
    mat2x3(
        2,6,3,
        0,3,1
    ),
    mat2x3(
        6,7,3,
        3,2,1
    ),
    //front face
    mat2x3(
        0,2,1,
        0,1,3
    ),
    mat2x3(
        2,3,1,
        1,2,3
    ),
    //back face
    mat2x3(
        4,5,6,
        0,3,1
    ),
    mat2x3(
        5,7,6,
        3,2,1
    ),
    //right face
    mat2x3(
        0,4,2,
        0,1,3
    ),
    mat2x3(
        4,6,2,
        1,2,3
    ),
    //left face
    mat2x3(
        1,3,5,
        0,3,1
    ),
    mat2x3(
        3,7,5,
        3,2,1
    )
);

void main() {
    for(int k = 0; k < gl_in.length(); k++)
    {
        for (int i = 0; i < faceOffsetIndexes.length(); i++) {
            mat2x3 facedata = faceOffsetIndexes[i];
            ivec3 offset = ivec3(facedata[0][0], facedata[0][1], facedata[0][2]);
            ivec3 texCoordIndex = ivec3(facedata[1][0], facedata[1][1], facedata[1][2]);
            
            // Perform transformation logic here
            for(int j = 0; j < 3; j++)
            {
                gl_Position = gl_in[k].gl_Position + offsets[offset[j]];
                gl_Position = gl_Position * model * view * projection;
                texCoord = texCoords[texCoordIndex[j]];
                EmitVertex();
            }
            EndPrimitive();
        }
    }
}