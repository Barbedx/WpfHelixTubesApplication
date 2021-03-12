using System;
using System.Numerics;
using System.Windows.Media.Media3D;

using WpfAppDatagridGroupingHeader.Extensions;

namespace WpfAppDatagridGroupingHeader.Helpers
{

    public class Bezier
    {

        public Vector3 P0;
        public Vector3 P1;
        public Vector3 P2;
        public Vector3 P3;

        public float Length = 0;

        public Vector3[] points;
 
        public Bezier(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, int _calculatePoints = 0)
        {
            this.P0 = v0;
            this.P1 = v1;
            this.P2 = v2;
            this.P3 = v3;

            if (_calculatePoints > 0) CalculatePoints(_calculatePoints);
        }
        public Bezier(Point3D v0, Point3D v1, Point3D v2, Point3D v3, int _calculatePoints = 0) :
            this(v0.ToVector3(), v1.ToVector3(), v2.ToVector3(), v3.ToVector3(), _calculatePoints)
        { }
        public Bezier(Point3D v0, Point3D v1, Point3D v3, int _calculatePoints = 0) :
             this(v0.ToVector3(), v1.ToVector3(), v1.ToVector3(), v3.ToVector3(), _calculatePoints)
        { }


        // 0.0 >= t <= 1.0 her be magic and dragons
        private Vector3 GetPointAtTime(float t)
        {
            float u = 1f - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * P0; //first term
            p += 3 * uu * t * P1; //second term
            p += 3 * u * tt * P2; //third term
            p += ttt * P3; //fourth term

            return p;

        }

        //where _num is the desired output of points and _precision is how good we want matching to be
        private void CalculatePoints(int _num, int _precision = 100)
        {
            if (_num > _precision) throw new ArgumentException("_num must be less than _precision");

            //calculate the length using _precision to give a rough estimate, save lengths in array
            Length = 0;
            //store the lengths between PointsAtTime in an array
            float[] arcLengths = new float[_precision];

            Vector3 oldPoint = GetPointAtTime(0);

            for (int p = 1; p < arcLengths.Length; p++)
            {
                Vector3 newPoint = GetPointAtTime((float)p / _precision); //get next point
                arcLengths[p] = Vector3.Distance(oldPoint, newPoint); //find distance to old point
                Length += arcLengths[p]; //add it to the bezier's length
                oldPoint = newPoint; //new is old for next loop
            }

            //create our points array
            points = new Vector3[_num];
            //target length for spacing
            float segmentLength = Length / _num;

            //arc index is where we got up to in the array to avoid the Shlemiel error http://www.joelonsoftware.com/articles/fog0000000319.html
            int arcIndex = 0;

            float walkLength = 0; //how far along the path we've walked
            //oldPoint = GetPointAtTime(0);

            //iterate through points and set them
            for (int i = 0; i < points.Length; i++)
            {
                float iSegLength = i * segmentLength; //what the total length of the walkLength must equal to be valid
                                                      //run through the arcLengths until past it
                while (walkLength < iSegLength)
                {
                    walkLength += arcLengths[arcIndex]; //add the next arcLength to the walk
                    arcIndex++; //go to next arcLength
                }
                //walkLength has exceeded target, so lets find where between 0 and 1 it is
                points[i] = GetPointAtTime((float)arcIndex / arcLengths.Length);

            }
 
        }

    }
}
