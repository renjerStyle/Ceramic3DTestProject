using System.Collections.Generic;
using UnityEngine;

public static class CreaterPlane {
    public static Mesh Quad(Vector3 origin, Vector3 width, Vector3 length)
    {
        var normal = Vector3.Cross(length, width).normalized;
        var mesh = new Mesh
        {
            vertices = new[] { origin, origin + length, origin + length + width, origin + width },
            normals = new[] { normal, normal, normal, normal },
            uv = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) },
            triangles = new[] { 0, 1, 2, 0, 2, 3 }
        };
        return mesh;
    }
    public static Mesh Plane(Vector3 origin, Vector3 width, Vector3 length, int widthCount, int lengthCount, float thickness, float leftright)
    {
        var combine = new List<CombineInstance>();
        //List<Vector3> positionsBackPlanesWidth = new List<Vector3>();
        //List<Vector3> positionsBackPlanesLength = new List<Vector3>();
        var i = 0;
        for (var x = 0; x < widthCount; x++)
        {
            for (var y = 0; y < lengthCount; y++)
            {
                /*var mainWidth = new Vector3(width.x + thickness, width.y, width.z) * x;
                var mainLength = new Vector3(length.x, length.y, length.z + thickness) * y;
                var pos = origin + mainWidth + mainLength;
                combine[i].mesh = Quad(pos, width, length);*/


                /*var mainWidth = new Vector3(origin.x + width.x * x + thickness, width.y, width.z) * x;
                var pos = origin + mainWidth;
                combine[i].mesh = Quad(pos, width, length);*/

                var posX = new Vector3(origin.x + leftright*y + width.x * x + thickness * x, 0, origin.z + length.z * (y+1) + thickness*y);
                var mainWidth = new Vector3(posX.x - width.x, width.y, width.z);
                var mainLength = new Vector3(width.x, width.y, posX.z - length.z);
                var pos = origin + mainWidth + mainLength;

                var m = new Mesh();
                m = Quad(pos, width, length);
                var ci = new CombineInstance();
                ci.mesh = m;
                combine.Add(ci);

               /* var thicknessY = new Vector3(width.x, 0, 0);
                var thcknessYLength = new Vector3(0, 0, thickness);
                var posY = new Vector3(0, 0, origin.z + length.z * (y+1)+thickness*y);
                Debug.Log("Width: " + (combine.Length - i - 4) + " of " + combine.Length);
                combine[i*3].mesh = Quad(posY, thicknessY, thcknessYLength);*/
                //positionsBackPlanesWidth.Add(mainWidth);
                //positionsBackPlanesLength.Add(mainLength);
                i++;
            }
        }

       /* var k = 0;
        for (var x = 0; x < widthCount; x++)
        {
            for (var y = 0; y < lengthCount-1; y++)
            {
                var posX = new Vector3(origin.x + leftright * y + width.x * x + thickness * x, 0, origin.z + length.z * (y + 1) + thickness * y);
                var thicknessX = new Vector3(width.x, 0, 0);
                var thcknessXLength = new Vector3(0, 0, thickness);

                var m2 = new Mesh();
                m2 = Quad(posX, thicknessX, thcknessXLength);
                var ci = new CombineInstance();
                ci.mesh = m2;
                combine.Add(ci);
                k++;
            }
        }

        var k2 = 0;
        for (var x = 0; x < widthCount-1; x++)
        {
            for (var y = 0; y < lengthCount; y++)
            {
                var posX = new Vector3(origin.x + leftright * y + width.x * (x+1) + thickness * x, 0, origin.z + length.z * y + thickness * y);
                var thicknessX = new Vector3(thickness, 0, 0);
                var thcknessXLength = new Vector3(0, 0, length.z);

                var m3 = new Mesh();
                m3 = Quad(posX, thicknessX, thcknessXLength);
                var ci = new CombineInstance();
                ci.mesh = m3;
                combine.Add(ci);
                k++;
            }
        }*/
        /* var thicknessX = new Vector3(thickness, 0, 0);
         var thcknessXLength = new Vector3(0, 0, length.z);
         Debug.Log("Width: " + (combine.Length - i - 1) + " of " + combine.Length);
         combine[i * 5].mesh = Quad(posX, thicknessX, thcknessXLength);*/
        //var posX = positionsBackPlanesWidth[x];



        /*for (var x1 = 0; x1 < widthCount; x1++) {
            for (var y1 = 0; y1 < lengthCount; y1++) {
                var posThickness = origin + width * x1 + length * y1;
                combine[i].mesh = Quad(posThickness, new Vector3(thickness, width.y, width.z), new Vector3(length.x, length.y, thickness));
                i++;
            }
        }*/

        var mesh = new Mesh();
        //mesh.CombineMeshes(combine, true, false);
        //mesh.CombineMeshes(combine2, true, false);
        mesh.subMeshCount = 2;
        mesh.CombineMeshes(combine.ToArray(), true, false);
        return mesh;
    }

    public static float AreaOfMesh(Mesh mesh)
    {
        var triangles = mesh.triangles;
        var vertices = mesh.vertices;

        double sum = 0.0;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 corner = vertices[triangles[i]];
            Vector3 a = vertices[triangles[i + 1]] - corner;
            Vector3 b = vertices[triangles[i + 2]] - corner;

            sum += Vector3.Cross(a, b).magnitude;
        }

        return (float)(sum / 2.0);
    }
}