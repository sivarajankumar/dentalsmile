using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using HelixToolkit.Wpf;

namespace smileUp.Algorithm
{
    class SegmentationB
    {
        private static Point3DCollection snakes = new Point3DCollection();

        void main()
        {

        }
        //ref: Snake-Based Segmentation of Teeth from Virtual Dental Casts
        //pLane located manually, set of verteks D and P from manual cutting plane
        //the plan is: teeth and gum separated then teeth segmented using snake below
        public static void snake(MeshGeometry3D mesh)
        {
            Dictionary<Int32, SmileEdge> neighbours = SmileVisual3D.findNeighbours(mesh);
            Point3DCollection pos= mesh.Positions;
            Int32Collection ind = mesh.TriangleIndices;
            Vector3DCollection t = new Vector3DCollection();
            Vector3DCollection b = new Vector3DCollection();
            Vector3DCollection n = new Vector3DCollection();
            double eThreshold = -0.3;
            double nThreshold = 0.8;
            double minEnergy = 10;
            double alpha = 1;
            double betha = 1;
            double ballon = 1;
            double lambda = 1;
            Vector3DCollection e = new Vector3DCollection();
            Vector3DCollection eInt = new Vector3DCollection();
            Vector3DCollection eExt = new Vector3DCollection();

            Vector3DCollection delta = new Vector3DCollection();
            List<Vector3DCollection> N = new List<Vector3DCollection>();
            Vector3DCollection f = new Vector3DCollection();
            Vector3DCollection tETF = new Vector3DCollection();

            Vector3DCollection k = new Vector3DCollection();
            Vector3DCollection kH = new Vector3DCollection();
            Vector3DCollection kMin = new Vector3DCollection();
            Vector3DCollection kMax = new Vector3DCollection();
            for (int i = 0; i < ind.Count; i++)
            {
                Point3D pi = pos[i];
               //kij =  2ni · (pi - pj)/|pi- pj |2
                kH[i] = (kMin[i] + kMax[i]) / 2;
            }

            for (int i = 0; i < ind.Count; i++)
            {
                Int32Collection nbrs = neighbours[i].neighbours;
               
                Point3D pi = pos[i];
                int i0 = i;
                if (i - 1 >= 0)  i0 = i - 1;
                Point3D pi0 = pos[i0]; 
                int i1 = i;
                if (i + i < ind.Count)   i = i + 1;
                Point3D pi1 = pos[i1];

                t[i] = (pi1 - pi0) / (Point3D.Subtract(pi1,pi0).Length);
                //t[i] = (pi1 - pi0) / Math.Sqrt((Point3D.Subtract(pi1, pi0).Length));
                n[i] = SmileVisual3D.CalculateNormal(pi0,pi,pi1);
                b[i] = Vector3D.CrossProduct(t[i], n[i]);


                Vector3D eIntV = eInt[i];
                Vector3D eExtV = eExt[i];
                Vector3D eV = e[i];
                int maxNbrs = (nbrs.Count > 2 ? 2 : nbrs.Count);
                for (int j = 0; j < maxNbrs; j++)
                {
                    Point3D nbj = pos[nbrs[j]];
                    double eIntj = alpha * (Point3D.Subtract(pi0, nbj).Length) + betha * (Point3D.Subtract(pi0, Point3D.Add(pi1, (Vector3D)nbj)).Length); //TODO: crosscheck


                    delta[i] = n[i];// 1 / determinant(N[i]); //TODO:implemenet
                    f[i] = n[i];// (1 - lambda) * (1 / (Math.Abs(nbrs.Count))) + lambda *delta[i]; //TODO:implemenet


                    double eFaej = ballon * (-(f[i].Length) - Vector3D.DotProduct( (f[i] / f[i].Length), (Point3D.Subtract(nbj, pi) / Point3D.Subtract(nbj, pi).Length))); //TODO: crosscheck
                    double ePressj = ballon * Vector3D.DotProduct(b[i], (Point3D.Subtract(pi0, nbj) / Point3D.Subtract(pi0, nbj).Length) ); 
                    double eExtj = eFaej + ePressj;
                    double ej = eIntj + eExtj;

                    if (j == 0)
                    {
                        eIntV.X = eIntj;
                        eExtV.X = eExtj;
                        eV.X = ej;
                    }
                    if (j == 1)
                    {
                        eIntV.Y = eIntj;
                        eExtV.Y = eExtj;
                        eV.Y = ej;
                    }
                    if (j == 2)
                    {
                        eIntV.Z = eIntj;
                        eExtV.Z = eExtj;
                        eV.Z = ej;
                    }

                    if (ej < minEnergy)
                    {
                        minEnergy = ej;
                        //TODO: snake movement
                        AddSnakeElement(nbj);
                    }
                    
                    eInt[i] = eIntV;
                    eExt[i] = eExtV;
                    e[i] = eV;
                }

                tETF[i] = Vector3D.CrossProduct(delta[i], n[i]); 

            }
        }

        private static void AddSnakeElement(Point3D nbj)
        {
            snakes.Add(nbj);
        }

        public static void drawSnakes(ModelVisual3D container)
        {
            container.Children.Clear();
            TubeVisual3D snake = new TubeVisual3D { Diameter = 2.00, Path = snakes, Fill = Brushes.LightGoldenrodYellow };
            container.Children.Add(snake);
        }

        public static double determinant(Vector3DCollection list)
        {
            Matrix3D m = new Matrix3D();
            for (int i = 0; i < list.Count;i++ )
            {
                Vector3D v = list[i];
                if (i == 0)
                {
                    m.M11 = v.X;
                    m.M12 = v.Y;
                    m.M13 = v.Z;
                }
                if (i == 1)
                {
                    m.M21 = v.X;
                    m.M22 = v.Y;
                    m.M23 = v.Z;
                }
                if (i == 2)
                {
                    m.M31 = v.X;
                    m.M32 = v.Y;
                    m.M33 = v.Z;
                }
            }
            return m.Determinant;
        }
        float _infinity = float.MaxValue;
        public double __MeanCurvature(Mesh3D m3d, int verticeIndex) 
        {
            return 0;
            /*
        // Initializing all necessary local variables.    
        double meanCurvature = 0.0;
        double mixedArea = 0;
        double barycentricArea = 0;
        Vector3D normalOperator = new Vector3D();
        //double[] normalOperator = new double[3];
        
        //for(int i=0; i<3; i++){
        //  normalOperator[i] = 0.0;
        //}
            
        // no neighbors of this face.
        if(nbrs == null){
          return _infinity;
        }
        
        // If the index point is connected with only one triangle, the index point
        // is located on the edge of the surface. For example, for the ucf file the 
        // margin of contours might not lap around, therefore the contour  might have 
        // boundary.
        if(nbrs.Count== 1){
          return _infinity;
        }

        
        // Orientation Specification:
        // Look at surface from outside such a way that the pivotal vertex is 
        // located upward. Then I call a triangle in the left is left triangle 
        // and the other triangle in the right is right triangle.
        //
        //  Up       pivotal vertex
        //                *
        //             *  *  *  
        //          *     *     *
        //        *       *        *
        //      *   *  *  *  *  *  *  *
        //         Left      Right   
        //  Down
        //
        //
        // 1. Absolute Mean Curvature Computation Algorithm Summary:
        // Xo - pivot vertex
        // X1,..,Xj,..,Xn - the first ring neighbor vertices of the vertex o.
        // n - number of the first ring neighbor vertices of the vertex o.
        //
        //                                               Xo
        //          * * * *                               o
        //       *   *   *   *                        *   *   * 
        //     *      * *      *                   *      *      *
        //   * *  *  * O *  * *  * Xj+1     Xj-1  * Aoj   *     Boj* Xj+1
        //     *      *  *     *                    *     *     *
        //       *  *     *   *                        *  *  *
        //         *  *  * *                              *
        //        Xj-1      Xj                            Xj
        //
        //                                  Aoj is angle at the vertex Xj-1
        //                                  Boj is angle at the vertex Xj+1
        //                                   
        //
        // Mean Curvature Normal Operator is calculated by following 2 steps:
        // First, calculate summation of (cot Aoj + cot Boj)(Xo - Xj) over j 
        // from 1 to n, where Xo is the pivot vertex and Xj's are elements of 
        // first ring neighbors vertices of the pivot vertex.
        // Second, divide above value by 2*AreaMixed.
        //
        // Area Mixed can be calculated by either Barycentric method or
        // Voronoi method.
        // 1. Barycentric method calculate AreaMixed by adding one third of each
        // triangle area.
        // 
        // 2. Voronoi method is following
        // if there is obtuse angle at pivot vertex, add half of the triangle
        // area.
        // else if there is obtuse angle beside pivot vertex, add one fourth of
        // triangle area.
        // else if there is no obtuse angle, add Voronoi area of triangle.
        //
        // Finally the Mean Curvature value is computed by taking half of the 
        // magnitude of the Mean Curvature Normal Operator.
        
        // 2. Mean Curvature Computation Algorithm Summary: The mean curvature 
        // (non absolute mean curvature) is computed as a summation over the 
        // edges adjacent to a vertex of the angle between the faces adjacent to 
        // the edge multiplied by the edge length, and divided by four times the 
        // area associated with the vertex.
        
        
        
        // Go over two neighbor faces which are connected to pivotal point
        // and which share an edge between.
        for (int i = 0; i < nbrs.Count; i++) {
          
          // Obtain vertices which are consist of faces.    
          Point3D vertiesInLeftFace = points[nbrs[i]];
          Point3D vertiesInRightFace = points[nbrs[i]];

            if (i + 1 == nbrs.Count)
          {
            vertiesInRightFace = points[nbrs[0]];
          }
          else{
            vertiesInRightFace = points[nbrs[i+1]];  
          }
          
          // This point is also center point.
          int vertexInCommon1 = -1;
          
          // This point is the other end of common edge between two triangles.
          int vertexInCommon2 = -1;
          
          // These two points are not shared between two triangles.
          int vertexUniqueForLeftFace = -1;
          int vertexUniqueForRightFace = -1;
          
          // Find common vertexes in two adjacent triangles. 
          for(int j=0; j<3; j++){
            for(int k=0; k<3; k++){  
              
              // If found vertex is pivotal point, save it as vertexInCommon1.
              // If found vertex is not pivotal point, then this vertex is
              // the other end of the common edge of two triangle. Save it as 
              // vertexInCommon2. 
              if(vertiesInLeftFace[j] == vertiesInRightFace[k]){
                if(vertiesInLeftFace[j] == index){
                  vertexInCommon1 = vertiesInLeftFace[j];
                  break;
                }
                else{
                  vertexInCommon2 = vertiesInLeftFace[j];   
                  break;
                }
              }
              else{
                // If the vertex is not one of common vertexes, save it as
                // vertexUniqueForLeftFace.  
                if(k==2){
                  vertexUniqueForLeftFace = vertiesInLeftFace[j];                 
                }  
              }   
            }            
          }
          
          // If two triangles share only center point, the index point is located 
          // on the edge of the surface. For example, for the ucf file the margin 
          // of contours might not lap around, therefore the contour  might have 
          // boundary.
          if(vertexInCommon2 == -1){
            return _infinity;
          }
          
          // Now task is finding a vertex which is unique to right triangle.
          // Go over each vertex in right face and find a vertex which is 
          // not shared with left triangle. then save it as 
          // vertexUniqueForLeftFace. 
          for(int l=0; l<3; l++){
            if(vertiesInRightFace[l] == vertexInCommon1 ){
            }
            else if(vertiesInRightFace[l] == vertexInCommon2){
            }
            else{
              vertexUniqueForRightFace = vertiesInRightFace[l];   
              break;
            }
          }
          
          // Obtain vector represent of vertex locations.
          double[] pivotalCommonPoint = new  double[_dimension];
          double[] theOtherCommonPoint = new  double[_dimension];
          double[] leftUniquePoint = new  double[_dimension];
          double[] rightUniquePoint = new  double[_dimension];
          
          for(int dim=0; dim<_dimension; dim++){
        	  
            pivotalCommonPoint[dim] = _points.getPoint(vertexInCommon1)[dim];   
            theOtherCommonPoint[dim] = _points.getPoint(vertexInCommon2)[dim];  
            leftUniquePoint[dim] = _points.getPoint(vertexUniqueForLeftFace)[dim];   
            rightUniquePoint[dim] = _points.getPoint(vertexUniqueForRightFace)[dim];   	  
          }    

          double[]  centerEdgeVector = 
        	  VectorMathDouble.difference(theOtherCommonPoint, pivotalCommonPoint);
          
          if(!_isAbsoluteMeanCurvature){
        	  
            // The mean curvature (non absolute mean curvature) is computed as a 
            // summation over the edges adjacent to a vertex of the angle between 
            // the faces adjacent to the edge multiplied by the edge length, and 
            // divided by four times the area associated with the vertex.
        	
        	// Reference: 
        	// Anupama Jagannathan, "Segmentation and Recognition of 3D Point Clouds 
        	// within Graph-theoretic and Thermodynamic Frameworks.", the Department 
        	// of Electrical and Computer Engineering, 61-62, 2005.
        	// http://www.ece.neu.edu/faculty/elmiller/laisir/pdfs/jagannathan_phd.pdf
        	  
        	// Seungbum Koo, Kunwoo Lee, "Wrap-around operation to make 
        	// multi-resolution model of part and assembly.", department of Machanical 
        	// and Aerospace Engineering, Seoul National University, 3, 2002,
        	// http://www.ece.tufts.edu/~elmiller/laisr/pdfs/jagannathan_phd.pdf.
 
	        double[] v1 = VectorMathDouble.difference(leftUniquePoint, pivotalCommonPoint);
	        double[] v2 = VectorMathDouble.difference(rightUniquePoint, pivotalCommonPoint);
	        
            // Normal vector of the first face.
	        double[] normal1 = VectorMathDouble.cross(v1, centerEdgeVector);  
	        
            // Normal vector of the second face.
	        double[] normal2 = VectorMathDouble.cross(centerEdgeVector, v2);
	        
            // Dihedral angle between adjacent triangular faces and is computed
            // as the angle between the corresponding normals.
	        double dihedralAngle = 
	        	  Math.acos(VectorMathDouble.dot(normal1, normal2)/
	        			  (VectorMathDouble.magnitude(normal1)*VectorMathDouble.magnitude(normal2)));   
	        
	        // Edge is convex edge if the edge type is greater than 0, and edge 
	        // is concave edge if the edge type is smaller than 0, and edge is 
	       // planner if edge type is 0;
	        double edgeType = 
	        	  VectorMathDouble.dot(
	        			  VectorMathDouble.cross(normal1, normal2), centerEdgeVector);
		    
	        double edgeVectorEuclideanNorm = 
		    	   VectorMathDouble.magnitude(centerEdgeVector);   
	 
	        if(edgeType < 0){
	          meanCurvature += -1*edgeVectorEuclideanNorm*dihedralAngle;
	        }
	        else if(edgeType > 0){
	          meanCurvature += edgeVectorEuclideanNorm*dihedralAngle;  
	        }
	        else{
	          meanCurvature += 0;	
	        }
          }  
          
          // Calculate cot x and cot y where x and y are the angles opposite 
          // common edge in two incident triangles.
          try {
	          
        	  double angleAlpha = VectorMathDouble.vectorAngle(pivotalCommonPoint, 
	              leftUniquePoint, 
	              theOtherCommonPoint);
	          
        	  double angleBetha = VectorMathDouble.vectorAngle(pivotalCommonPoint, 
	              rightUniquePoint, 
	              theOtherCommonPoint);
	          
	          // angleAlpha, angleTheta, and angleOmega are three angles of left triangles.
	          // Test if any of these angles is obtuse.
        	  double angleTheta = VectorMathDouble.vectorAngle(leftUniquePoint, 
	              pivotalCommonPoint, 
	              theOtherCommonPoint);
        	  double angleOmega = VectorMathDouble.vectorAngle(leftUniquePoint, 
	              theOtherCommonPoint, 
	              pivotalCommonPoint);  
	          
	          // Obtain Mean Curvature Normal Operator.
	          double cotAlpha = 1/Math.tan(angleAlpha);
	          double cotBetha = 1/Math.tan(angleBetha);
	          double scalar = (cotAlpha + cotBetha);
	          double[] centerEdgeVectorScalarMultiplied = 
	               VectorMathDouble.multiply(centerEdgeVector, scalar); 
	          
	          normalOperator = 
	        	  VectorMath.add(normalOperator, centerEdgeVectorScalarMultiplied); 
	          
	          // Calculate area of the left triangle.
	          double[] leftEdgeVector = VectorMathDouble.difference(pivotalCommonPoint, 
	              leftUniquePoint);
	          double magnitudeOfLeftEdge = VectorMathDouble.magnitude(leftEdgeVector);
	          
	          double[] rightEdgeVector = VectorMathDouble.difference(theOtherCommonPoint, 
	              leftUniquePoint);
	          double magnitudeOfRightEdge = VectorMathDouble.magnitude(rightEdgeVector);
	          
	          double areaOfLeftTriangle = 
	        	  Math.sin(angleAlpha)*magnitudeOfLeftEdge*magnitudeOfRightEdge/2.0;
	          
	          if(_areaCalMethod == BARYCENTRIC_AREA){
	            barycentricArea = barycentricArea + areaOfLeftTriangle/3.0;
	          }
	          else if(_areaCalMethod ==  VORONOI_AREA){
	            // Obtuse angle exists at pivot vertex.
	            if(angleTheta > Math.PI/2){
	              mixedArea = mixedArea + areaOfLeftTriangle/2.0;  
	            }
	            // Obtuse angle exists at other vertex beside the pivot vertex.
	            else if(angleAlpha > Math.PI/2 || angleOmega > Math.PI/2){
	              mixedArea = mixedArea + areaOfLeftTriangle/4.0;
	            }
	            // No obtuse angle exists
	            else{
	              double magnitudeOfCenterVector = VectorMathDouble.magnitude(centerEdgeVector);   
	              
	              double voronoiAreaInATriangle = magnitudeOfLeftEdge*
	              magnitudeOfLeftEdge*
	              (1/Math.tan(angleOmega)) +
	              magnitudeOfCenterVector*
	              magnitudeOfCenterVector*
	              (1/Math.tan(angleAlpha));
	              
	              mixedArea = mixedArea + voronoiAreaInATriangle/8.0;
	            }
	          }
          } 
          catch (InappropriateGeometryException ige) {
            throw new CalculationException(
                 "Unable to calculate curvature of face # " +i +
                 " Reason " + ige.getMessage());
          }
        }

        if(_areaCalMethod == BARYCENTRIC_AREA){
          normalOperator = VectorMathDouble.multiply(normalOperator, 1.0/(2.0*barycentricArea)); 
          meanCurvature =  meanCurvature*(1.0/(4.0*barycentricArea));
        }
        else if(_areaCalMethod == VORONOI_AREA){
          
          normalOperator = VectorMathDouble.multiply(normalOperator, 1.0/(2.0*mixedArea)); 
          meanCurvature =  meanCurvature*(1.0/(4.0*mixedArea));
        }
        
        double curvature = meanCurvature;
        
        if(_isAbsoluteMeanCurvature){
        	
          // The mean curvature value is half of the magnitude of Normal Operator.	
          curvature =  VectorMath.magnitude(normalOperator) / 2;
        }
        
        return curvature;
             * */
      }

        public double _MeanCurvature(Mesh3D m3d, int index)
        {
            double meanCurvature = 0.0;
            double mixedArea = 0;
            double barycentricArea = 0;
            Vector3D normalOperator = new Vector3D();

            int[] nbrs = m3d.GetNeighbourVertices(index);
            HashSet<int> fi = new HashSet<int>();
            for (int i = 0; i < nbrs.Length; i++)
            {
                int fii = m3d.FindFaceFromEdge(index, nbrs[i]);
                fi.Add(fii);
            }
            int m = fi.Count;
            int[] vLeft = null;
            int[] vRight = null;
            if (m > 1)
            {
                vLeft = m3d.Faces.ElementAt(fi.ElementAt(0));
                //int[] vRight = m3d.Faces.ElementAt(fi.ElementAt((i + 1) % m));
                vRight = m3d.Faces.ElementAt(fi.ElementAt(1));
            }
            else if (m == 1)
            {
                //Console.WriteLine(m+" -- "+fi.ElementAt(i) + " - " + fi.ElementAt((i + 1) % m));
                vLeft = m3d.Faces.ElementAt(fi.ElementAt(0));
                vRight = m3d.Faces.ElementAt(fi.ElementAt(0));
            }
            else
            {
                return _infinity;

            }
                // This point is also center point.
                int vertexInCommon1 = -1;

                // This point is the other end of common edge between two triangles.
                int vertexInCommon2 = -1;

                // These two points are not shared between two triangles.
                int vertexUniqueForLeftFace = -1;
                int vertexUniqueForRightFace = -1;

                // Find common vertexes in two adjacent triangles. 
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {

                        // If found vertex is pivotal point, save it as vertexInCommon1.
                        // If found vertex is not pivotal point, then this vertex is
                        // the other end of the common edge of two triangle. Save it as 
                        // vertexInCommon2. 
                        if (vLeft[j] == vRight[k])
                        {
                            if (vLeft[j] == index)
                            {
                                vertexInCommon1 = vLeft[j];
                                break;
                            }
                            else
                            {
                                vertexInCommon2 = vLeft[j];
                                break;
                            }
                        }
                        else
                        {
                            // If the vertex is not one of common vertexes, save it as
                            // vertexUniqueForLeftFace.  
                            if (k == 2)
                            {
                                vertexUniqueForLeftFace = vLeft[j];
                            }
                        }
                    }
                }

                // If two triangles share only center point, the index point is located 
                // on the edge of the surface. For example, for the ucf file the margin 
                // of contours might not lap around, therefore the contour  might have 
                // boundary.
                if (vertexInCommon2 == -1)
                {
                    return _infinity;
                }

                // Now task is finding a vertex which is unique to right triangle.
                // Go over each vertex in right face and find a vertex which is 
                // not shared with left triangle. then save it as 
                // vertexUniqueForLeftFace. 
                for (int l = 0; l < 3; l++)
                {
                    if (vRight[l] == vertexInCommon1)
                    {
                    }
                    else if (vRight[l] == vertexInCommon2)
                    {
                    }
                    else
                    {
                        vertexUniqueForRightFace = vRight[l];
                        break;
                    }
                }

                if (vertexUniqueForLeftFace == -1) vertexUniqueForLeftFace = vertexInCommon1;

                // Obtain vector represent of vertex locations.
                Point3D pivotalCommonPoint = m3d.Vertices.ElementAt(vertexInCommon1);
                Point3D theOtherCommonPoint = m3d.Vertices.ElementAt(vertexInCommon2);
                Point3D leftUniquePoint = m3d.Vertices.ElementAt(vertexUniqueForLeftFace);
                Point3D rightUniquePoint = m3d.Vertices.ElementAt(vertexUniqueForRightFace);

                Vector3D centerEdgeVector = Vector3D.Subtract(theOtherCommonPoint.ToVector3D(), pivotalCommonPoint.ToVector3D());

                if (!_isAbsoluteMeanCurvature)
                {
                    Vector3D v1 = Vector3D.Subtract(leftUniquePoint.ToVector3D(), pivotalCommonPoint.ToVector3D());
                    Vector3D v2 = Vector3D.Subtract(rightUniquePoint.ToVector3D(), pivotalCommonPoint.ToVector3D());

                    // Normal vector of the first face.
                    Vector3D normal1 = Vector3D.CrossProduct(v1, centerEdgeVector);

                    // Normal vector of the second face.
                    Vector3D normal2 = Vector3D.CrossProduct(centerEdgeVector, v2);

                    // Dihedral angle between adjacent triangular faces and is computed
                    // as the angle between the corresponding normals.
                    double dihedralAngle =
                          Math.Acos(Vector3D.DotProduct(normal1, normal2) /
                                  (normal1.Length * normal2.Length));
                    // Edge is convex edge if the edge type is greater than 0, and edge 
                    // is concave edge if the edge type is smaller than 0, and edge is 
                    // planner if edge type is 0;
                    double edgeType =
                          Vector3D.DotProduct(
                                  Vector3D.CrossProduct(normal1, normal2), centerEdgeVector);

                    double edgeVectorEuclideanNorm =centerEdgeVector.Length;

                    if (edgeType < 0)
                    {
                        meanCurvature += -1 * edgeVectorEuclideanNorm * dihedralAngle;
                    }
                    else if (edgeType > 0)
                    {
                        meanCurvature += edgeVectorEuclideanNorm * dihedralAngle;
                    }
                    else
                    {
                        meanCurvature += 0;
                    }
                }
                // Calculate cot x and cot y where x and y are the angles opposite 
                // common edge in two incident triangles.
                try
                {

                    double angleAlpha = Vector3D.AngleBetween(pivotalCommonPoint.ToVector3D(), leftUniquePoint.ToVector3D());
                        //theOtherCommonPoint.ToVector3D()));

                    double angleBetha = Vector3D.AngleBetween(pivotalCommonPoint.ToVector3D(), rightUniquePoint.ToVector3D());
                        //,theOtherCommonPoint);

                    // angleAlpha, angleTheta, and angleOmega are three angles of left triangles.
                    // Test if any of these angles is obtuse.
                    double angleTheta = Vector3D.AngleBetween(leftUniquePoint.ToVector3D(),
                        pivotalCommonPoint.ToVector3D());
                        //,theOtherCommonPoint);
                    double angleOmega = Vector3D.AngleBetween(leftUniquePoint.ToVector3D(),
                        theOtherCommonPoint.ToVector3D());
                        //,pivotalCommonPoint);

                    // Obtain Mean Curvature Normal Operator.
                    double cotAlpha = 1 / Math.Tan(angleAlpha);
                    double cotBetha = 1 / Math.Tan(angleBetha);
                    double scalar = (cotAlpha + cotBetha);
                    Vector3D centerEdgeVectorScalarMultiplied = Vector3D.Multiply(centerEdgeVector, scalar);

                    normalOperator = Vector3D.Add(normalOperator, centerEdgeVectorScalarMultiplied);

                    // Calculate area of the left triangle.
                    Vector3D leftEdgeVector = Vector3D.Subtract(pivotalCommonPoint.ToVector3D(), leftUniquePoint.ToVector3D());
                    double magnitudeOfLeftEdge = leftEdgeVector.Length;

                    Vector3D rightEdgeVector = Vector3D.Subtract(theOtherCommonPoint.ToVector3D(), leftUniquePoint.ToVector3D());
                    double magnitudeOfRightEdge = rightEdgeVector.Length;

                    double areaOfLeftTriangle =
                        Math.Sin(angleAlpha) * magnitudeOfLeftEdge * magnitudeOfRightEdge / 2.0;

                    if (_areaCalMethod == BARYCENTRIC_AREA)
                    {
                        barycentricArea = barycentricArea + areaOfLeftTriangle / 3.0;
                    }
                    else if (_areaCalMethod == VORONOI_AREA)
                    {
                        // Obtuse angle exists at pivot vertex.
                        if (angleTheta > Math.PI / 2)
                        {
                            mixedArea = mixedArea + areaOfLeftTriangle / 2.0;
                        }
                        // Obtuse angle exists at other vertex beside the pivot vertex.
                        else if (angleAlpha > Math.PI / 2 || angleOmega > Math.PI / 2)
                        {
                            mixedArea = mixedArea + areaOfLeftTriangle / 4.0;
                        }
                        // No obtuse angle exists
                        else
                        {
                            double magnitudeOfCenterVector = centerEdgeVector.Length;

                            double voronoiAreaInATriangle = magnitudeOfLeftEdge *
                            magnitudeOfLeftEdge * (1 / Math.Tan(angleOmega)) +
                            magnitudeOfCenterVector * magnitudeOfCenterVector *
                            (1 / Math.Tan(angleAlpha));

                            mixedArea = mixedArea + voronoiAreaInATriangle / 8.0;
                        }
                    }
                }
                catch (Exception ige)
                {
                    Console.WriteLine(
                         "Unable to calculate curvature of face # " + index+
                         " Reason " + ige.Message);
                }
            

            if (_areaCalMethod == BARYCENTRIC_AREA)
            {
                normalOperator = Vector3D.Multiply(normalOperator, 1.0 / (2.0 * barycentricArea));
                meanCurvature = meanCurvature * (1.0 / (4.0 * barycentricArea));
            }
            else if (_areaCalMethod == VORONOI_AREA)
            {

                normalOperator = Vector3D.Multiply(normalOperator, 1.0 / (2.0 * mixedArea));
                meanCurvature = meanCurvature * (1.0 / (4.0 * mixedArea));
            }

            double curvature = meanCurvature;

            if (_isAbsoluteMeanCurvature)
            {

                // The mean curvature value is half of the magnitude of Normal Operator.	
                curvature = normalOperator.Length / 2;
            }
        
            return meanCurvature;
        }
        bool _isAbsoluteMeanCurvature = false;
        public const int BARYCENTRIC_AREA = 0; 
    
        /** 
         * The area calculation method that uses Voronoi cell of a 
         * finite-volume region on a triangle surface.
         **/
        public const int VORONOI_AREA = 1; 
    
        /** The area calculation method used. **/
        private int _areaCalMethod = BARYCENTRIC_AREA;
        internal MeshGeometry3D AutoSnake(MeshGeometry3D mesh)
        {
            //Dictionary<Int32, SmileEdge> neighbours = SmileVisual3D.findNeighbours(mesh);

            Point3DCollection positions = mesh.Positions;
            Int32Collection triangleIndices = mesh.TriangleIndices;
            Mesh3D m3d = new Mesh3D(positions, triangleIndices);

            Vector3DCollection t = new Vector3DCollection();
            Vector3DCollection b = new Vector3DCollection();
            Vector3DCollection n = new Vector3DCollection();
            double eThreshold = -0.3;
            double nThreshold = 0.8;
            double minEnergy = 10;
            double alpha = 1;
            double betha = 1;
            double ballon = 1;
            double lambda = 1;
            Vector3DCollection e = new Vector3DCollection();
            Vector3DCollection eInt = new Vector3DCollection();
            Vector3DCollection eExt = new Vector3DCollection();

            Vector3DCollection delta = new Vector3DCollection();
            List<Vector3DCollection> N = new List<Vector3DCollection>();
            Vector3DCollection f = new Vector3DCollection();
            Vector3DCollection tETF = new Vector3DCollection();

            Vector3DCollection k = new Vector3DCollection();
            DoubleCollection kH = new DoubleCollection();
            MeshBuilder newmb = new MeshBuilder(false,false); 
            for (int i = 0; i < triangleIndices.Count; i+=3)
            {
                for (int j = 0; j < 3; j++)
                {
                    int vi = triangleIndices[i+j];

                    //Console.WriteLine("start mean");
                    double kHi = _MeanCurvature(m3d, vi);
                    kH.Add(kHi);
                    //Console.WriteLine("end mean");
                    if (kHi < eThreshold)
                    {
                        newmb.Positions.Add(positions[i]);
                        newmb.TriangleIndices.Add(i);
                    }
                }
            }
            return newmb.ToMesh();
/*
            for (int i = 0; i < positions.Count; i++)
            {
                Int32Collection nbrs = neighbours[i].neighbours;

                Point3D pi = positions[i];
                int i0 = i;
                if (i - 1 >= 0) i0 = i - 1;
                Point3D pi0 = positions[i0];
                int i1 = i;
                if (i + i < triangleIndices.Count) i = i + 1;
                Point3D pi1 = positions[i1];

                t[i] = (pi1 - pi0) / (Point3D.Subtract(pi1, pi0).Length);
                //t[i] = (pi1 - pi0) / Math.Sqrt((Point3D.Subtract(pi1, pi0).Length));
                n[i] = SmileVisual3D.CalculateNormal(pi0, pi, pi1);
                b[i] = Vector3D.CrossProduct(t[i], n[i]);


                Vector3D eIntV = eInt[i];
                Vector3D eExtV = eExt[i];
                Vector3D eV = e[i];
                int maxNbrs = (nbrs.Count > 2 ? 2 : nbrs.Count);
                for (int j = 0; j < maxNbrs; j++)
                {
                    Point3D nbj = positions[nbrs[j]];
                    double eIntj = alpha * (Point3D.Subtract(pi0, nbj).Length) + betha * (Point3D.Subtract(pi0, Point3D.Add(pi1, (Vector3D)nbj)).Length); //TODO: crosscheck


                    delta[i] = n[i];// 1 / determinant(N[i]); //TODO:implemenet
                    f[i] = n[i];// (1 - lambda) * (1 / (Math.Abs(nbrs.Count))) + lambda *delta[i]; //TODO:implemenet


                    double eFaej = ballon * (-(f[i].Length) - Vector3D.DotProduct((f[i] / f[i].Length), (Point3D.Subtract(nbj, pi) / Point3D.Subtract(nbj, pi).Length))); //TODO: crosscheck
                    double ePressj = ballon * Vector3D.DotProduct(b[i], (Point3D.Subtract(pi0, nbj) / Point3D.Subtract(pi0, nbj).Length));
                    double eExtj = eFaej + ePressj;
                    double ej = eIntj + eExtj;

                    if (j == 0)
                    {
                        eIntV.X = eIntj;
                        eExtV.X = eExtj;
                        eV.X = ej;
                    }
                    if (j == 1)
                    {
                        eIntV.Y = eIntj;
                        eExtV.Y = eExtj;
                        eV.Y = ej;
                    }
                    if (j == 2)
                    {
                        eIntV.Z = eIntj;
                        eExtV.Z = eExtj;
                        eV.Z = ej;
                    }

                    if (ej < minEnergy)
                    {
                        minEnergy = ej;
                        //TODO: snake movement
                        AddSnakeElement(nbj);
                    }

                    eInt[i] = eIntV;
                    eExt[i] = eExtV;
                    e[i] = eV;
                }

                tETF[i] = Vector3D.CrossProduct(delta[i], n[i]);

            }
 * */
        }
    }
}
