#version 330 core
out vec4 FragColor;
in vec2 outTexCoords;

uniform sampler2D texture1;

uniform sampler2D texture2;

uniform float mixRate;

void main()
{
    vec4 color1 = texture(texture1,vec2(outTexCoords.x,1-outTexCoords.y));
    vec4 color2 = texture(texture2,vec2(outTexCoords.x,1-outTexCoords.y));
    //FragColor = vec4(color1.x * mixRate + color2.x * (1-mixRate),color1.y * mixRate + color2.y * (1-mixRate),color1.z * mixRate + color2.z * (1-mixRate),1);
    FragColor = mix(color1,color2,mixRate);
    //FragColor = mix(color1,color2,0.2f);

}
