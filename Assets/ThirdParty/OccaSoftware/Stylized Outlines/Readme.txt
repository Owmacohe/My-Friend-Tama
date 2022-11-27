README
================================

Contents
--------------------------------
About
Installation Instructions
Usage Instructions
Requirements
Support


About
--------------------------------
Stylized Outline is a lightweight, easy to use outline shader. To apply, you simply drag and drop the material into a second material input slot onto any mesh that you would like to receive an outline. 

It can be customized on a per-mesh basis. For example, you can have one object with a thin red outline and at the same time have a second object with a thick blue outline.

The outline is configured using world space units and is rendered in world space. In other words, you can easily set the exact size of the outline in world space units. As objects move closer or further from the camera, the outline remains the same size relative to the object.

You can set any outline color on a per-object (i.e., per material) basis.

You can also use the Vertex Color (R) channel to attenuate the outline thickness for more artistic control. A Vertex Color (R) value of 0 fully attenuates the Outline Thickness, whereas a Vertex Color (R) value of 1 applies no attenuation to the Outline Thickness. An example of this functionality is provided with the VP_Sphere in the demo.

You can also scale the outline size down as objects get further away from the camera if you'd like the effect to only appear for nearby objects.

Finally, you can use a noise texture to attenuate the outline thickness based on the vertex distance from the object center. The noise texture is sampled along the x axis only. The Noise Framerate configures how frequently we jump to a new randomly offset position along the x-axis. Set this to zero to disable offset jumping. The Noise Frequency configures the rate at which the distance samples across the x-axis of the noise texture (e.g., setting frequency to 1 causes the shader to sample at a rate of 1 : 1 in world space units - in other words, 1 unit of distance will be equivalent to 1 repeat of the noise texture).

Shader Properties include the following:
# _OutlineColor
# _OutlineThickness
# _UseVertexColors
# _AttenuateByDistance
# _CompleteFalloffDistance
# _NoiseTexture
# _NoiseFrequency
# _NoiseFramerate

These Shader Properties are all accessible as standard material properties are during runtime, except for the three toggles - _UseVertexColors, _AttenuateByDistance, and _RandomOffset, which are shader feature keywords compiled during build.


Installation Instructions
--------------------------------
1. Import the Stylized Outline asset into your project.
2. Identify an object that you would like to outline in your scene and open it in the Inspector.
3. In the Mesh Renderer component, click the + button under the Materials slot. 
4. Drag and drop the M_StylizedOutline 


Usage Instructions
--------------------------------
1. Apply the M_StylizedOutline material to the second material slot in the Mesh Renderer's materials inspector. This is typically labelled "Element 1", but for meshes with many submeshes, this can would be Element (submeshindex.length).
2. For complexes meshes with many submeshes, you will need to include multiple M_StylizedOutline materials - one for each submesh.

N.B., You will get a warning in the Inspector, "This renderer has more materials than the Mesh has submeshes. Multiple materials will be applied to the same submesh, which costs performance. Consider using multiple shader passes." This is expected.


Requirements
--------------------------------
This asset is designed for Unity 2020.3 Universal Render Pipeline. This asset is not guaranteed to work on other versions of Unity. This asset will not work on the Built-In Render Pipeline or the High Definition Render Pipeline.


Support
--------------------------------
If you're not happy, I'm not happy.
Please contact me at occasoftware@gmail.com for any support.

