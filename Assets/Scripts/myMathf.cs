using UnityEngine;

public class myMathf{
    public static float get360Angle(Vector2 vector) {
        if (vector.y >= 0) {
            return (Vector2.Angle(Vector2.right, vector));
        }
        else {
            return 360 - Vector2.Angle(Vector2.right, vector);
        }
    }

    public static Vector2 rotate(Vector2 vector, float radians) {
        float cs = Mathf.Cos(radians);
        float sn = Mathf.Sin(radians);

        float px = vector.x * cs - vector.y * sn;
        float py = vector.x * sn + vector.y * cs;

        vector.x = px;
        vector.y = py;
        return vector;
    }

    public static void zLessPosition(Transform a, Transform b) {
        float z = a.transform.position.z;
        a.transform.position = b.transform.position;
        a.transform.Translate(new Vector3(0, 0, z - b.transform.position.z));
    }
}
