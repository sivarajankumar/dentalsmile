using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp.Algorithm.HoleFilling
{
    class RobustHoleFillingWithPoisson
    {
        /**
         * Ref: http://www.cad.zju.edu.cn/home/hwlin/A-robust-hole-filling-algorithm-for-triangular-mesh.pdf
        Step 1. Identify holes in triangular mesh;
        Step 2. For each hole in mesh model:
            (1) Generate its initial patch mesh via the advancing front mesh (AFM) technique.
            (2) Refine the patch mesh based on the Poisson equation as follows:
                – Compute desirable normals using the harmonic equation or geodesic interpolation.
                – Rotate triangles by local rotation.
                – Solve Poisson equation and obtain the new coordinates of every vertex.
                – Update the coordinates and obtain the smoothed patch mesh.
         
        AFM
        Step 1. Initialize the front using the boundary vertices of the hole.
        Step 2. Calculate the angle θi between two adjacent boundary edges(ei and ei+1) at each vertex vi on the front.
        Step 3. Starting from the vertex vi with the smallest angle θi , create new triangles on the plane determined by ei and ei+1 with the three rules shown in Fig. 3.
        Step 4. Compute the distance between each newly created vertex and every related boundary vertex; if the distance between them is less than the given threshold, they are merged.
        Step 5. Update the front.
        Step 6. Repeat Steps 2 through 5 until the whole region has been patched by all newly created triangles.
        */

    }
}
