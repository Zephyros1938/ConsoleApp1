#version 330 core
layout (triangles) in;
layout (triangle_strip, max_vertices = 6) out;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
  
void main() {    
    gl_Position = gl_in[0].gl_Position + vec4(0.25, 0.25, 0.0, 0.0); 
    gl_Position = gl_Position  * model * view * projection;
    EmitVertex();
    gl_Position = gl_in[0].gl_Position + vec4(-0.25, 0.25, 0.0, 0.0);
    gl_Position = gl_Position  * model * view * projection;
    EmitVertex();
    gl_Position = gl_in[0].gl_Position + vec4(-0.25, -0.25, 0.0, 0.0);
    gl_Position = gl_Position  * model * view * projection;
    EmitVertex();
    EndPrimitive();

    gl_Position = gl_in[0].gl_Position + vec4(0.25, 0.25, 0.0, 0.0); 
    gl_Position = gl_Position  * model * view * projection;
    EmitVertex();
    gl_Position = gl_in[0].gl_Position + vec4(0.25, -0.25, 0.0, 0.0);
    gl_Position = gl_Position  * model * view * projection;
    EmitVertex();
    gl_Position = gl_in[0].gl_Position + vec4(-0.25, -0.25, 0.0, 0.0);
    gl_Position = gl_Position  * model * view * projection;
    EmitVertex();
    EndPrimitive();
}    